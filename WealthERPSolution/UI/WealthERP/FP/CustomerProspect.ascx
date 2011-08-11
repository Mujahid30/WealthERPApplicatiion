<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="true" CodeBehind="CustomerProspect.ascx.cs"
     Inherits="WealthERP.FP.CustomerProspect"  %>
  
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<script type="text/javascript">

    function HideStatusMsg() {
        document.getElementById("<%=msgRecordStatus.ClientID%>").style.display = 'none';
    }

</script>

<script type="text/javascript">
    function Total() {
        var assettotal = 0.0;
        var incometotal = 0.0;
        var expensetotal = 0.0;
        var liabilitiestotal = 0.0;
        var lifeInsuranceTotal = 0.0;
        var generalInsuranceTotal = 0.0;
        var AllLIandGIPremiumTotal = 0.0;
        var allEMIsTotal = 0.0;
        var endowMentTotal = 0.0;

        
        //Assets
        if (document.getElementById("<%=txtDirectEquity.ClientID%>").value != "") {
            assettotal += parseFloat(document.getElementById("<%=txtDirectEquity.ClientID%>").value);
        }
        if (document.getElementById("<%=txtGold.ClientID%>").value != "") {
            assettotal += parseFloat(document.getElementById("<%=txtGold.ClientID%>").value);
        }
        if (document.getElementById("<%=txtMFEquity.ClientID%>").value != "") {
            assettotal += parseFloat(document.getElementById("<%=txtMFEquity.ClientID%>").value);
        }
        if (document.getElementById("<%=txtCollectibles.ClientID%>").value != "") {
            assettotal += parseFloat(document.getElementById("<%=txtCollectibles.ClientID%>").value);
        }
        if (document.getElementById("<%=txtMFDebt.ClientID%>").value != "") {
            assettotal += parseFloat(document.getElementById("<%=txtMFDebt.ClientID%>").value);
        }
        if (document.getElementById("<%=txtCashAndSavings.ClientID%>").value != "") {
            assettotal += parseFloat(document.getElementById("<%=txtCashAndSavings.ClientID%>").value);
        }
        if (document.getElementById("<%=txtMFHybridEquity.ClientID%>").value != "") {
            assettotal += parseFloat(document.getElementById("<%=txtMFHybridEquity.ClientID%>").value);
        }
        if (document.getElementById("<%=txtStructuredProduct.ClientID%>").value != "") {
            assettotal += parseFloat(document.getElementById("<%=txtStructuredProduct.ClientID%>").value);
        }
        if (document.getElementById("<%=txtMFHybridDebt.ClientID%>").value != "") {
            assettotal += parseFloat(document.getElementById("<%=txtMFHybridDebt.ClientID%>").value);
        }
        if (document.getElementById("<%=txtCommodities.ClientID%>").value != "") {
            assettotal += parseFloat(document.getElementById("<%=txtCommodities.ClientID%>").value);
        }
        if (document.getElementById("<%=txtFixedIncome.ClientID%>").value != "") {
            assettotal += parseFloat(document.getElementById("<%=txtFixedIncome.ClientID%>").value);
        }
        if (document.getElementById("<%=txtPrivateEquity.ClientID%>").value != "") {
            assettotal += parseFloat(document.getElementById("<%=txtPrivateEquity.ClientID%>").value);
        }
        if (document.getElementById("<%=txtGovtSavings.ClientID%>").value != "") {
            assettotal += parseFloat(document.getElementById("<%=txtGovtSavings.ClientID%>").value);
        }
        if (document.getElementById("<%=txtPMS.ClientID%>").value != "") {
            assettotal += parseFloat(document.getElementById("<%=txtPMS.ClientID%>").value);
        }
        if (document.getElementById("<%=txtPensionGratuities.ClientID%>").value != "") {
            assettotal += parseFloat(document.getElementById("<%=txtPensionGratuities.ClientID%>").value);
        }
        if (document.getElementById("<%=txtInvestmentsOthers.ClientID%>").value != "") {
            assettotal += parseFloat(document.getElementById("<%=txtInvestmentsOthers.ClientID%>").value);
        }
        if (document.getElementById("<%=txtProperty.ClientID%>").value != "") {
            assettotal += parseFloat(document.getElementById("<%=txtProperty.ClientID%>").value);
        }
        //Income
        if (document.getElementById("<%=txtSalary.ClientID%>").value != "") {
            incometotal += parseFloat(document.getElementById("<%=txtSalary.ClientID%>").value);
        }
        if (document.getElementById("<%=txtCapitalGains.ClientID%>").value != "") {
            incometotal += parseFloat(document.getElementById("<%=txtCapitalGains.ClientID%>").value);
        }
        if (document.getElementById("<%=txtRentalProperty.ClientID%>").value != "") {
            incometotal += parseFloat(document.getElementById("<%=txtRentalProperty.ClientID%>").value);
        }
        if (document.getElementById("<%=txtAgriculturalIncome.ClientID%>").value != "") {
            incometotal += parseFloat(document.getElementById("<%=txtAgriculturalIncome.ClientID%>").value);
        }
        if (document.getElementById("<%=txtBusinessAndProfession.ClientID%>").value != "") {
            incometotal += parseFloat(document.getElementById("<%=txtBusinessAndProfession.ClientID%>").value);
        }
        if (document.getElementById("<%=txtOthersIncome.ClientID%>").value != "") {
            incometotal += parseFloat(document.getElementById("<%=txtOthersIncome.ClientID%>").value);
        }
        //Expense
        if (document.getElementById("<%=txtFood.ClientID%>").value != "") {
            expensetotal += parseFloat(document.getElementById("<%=txtFood.ClientID%>").value);
        }
        if (document.getElementById("<%=txtEntertainmentHolidays.ClientID%>").value != "") {
            expensetotal += parseFloat(document.getElementById("<%=txtEntertainmentHolidays.ClientID%>").value);
        }
        if (document.getElementById("<%=txtRent.ClientID%>").value != "") {
            expensetotal += parseFloat(document.getElementById("<%=txtRent.ClientID%>").value);
        }
        if (document.getElementById("<%=txtPersonalWear.ClientID%>").value != "") {
            expensetotal += parseFloat(document.getElementById("<%=txtPersonalWear.ClientID%>").value);
        }
        if (document.getElementById("<%=txtUtilites.ClientID%>").value != "") {
            expensetotal += parseFloat(document.getElementById("<%=txtUtilites.ClientID%>").value);
        }
        if (document.getElementById("<%=txtHealthPersonalCare.ClientID%>").value != "") {
            expensetotal += parseFloat(document.getElementById("<%=txtHealthPersonalCare.ClientID%>").value);
        }
        if (document.getElementById("<%=txtDomesticHelp.ClientID%>").value != "") {
            expensetotal += parseFloat(document.getElementById("<%=txtDomesticHelp.ClientID%>").value);
        }
        if (document.getElementById("<%=txtConveyance.ClientID%>").value != "") {
            expensetotal += parseFloat(document.getElementById("<%=txtConveyance.ClientID%>").value);
        }
        if (document.getElementById("<%=txtOthersExpense.ClientID%>").value != "") {
            expensetotal += parseFloat(document.getElementById("<%=txtOthersExpense.ClientID%>").value);
        }
        // To add MF SIP and Reccuring Deposit value to total

        if (document.getElementById("<%=txtMFSIPMIS.ClientID%>").value != "") {
            expensetotal += parseFloat(document.getElementById("<%=txtMFSIPMIS.ClientID%>").value);
        }
        if (document.getElementById("<%=txtReccuringDeposit.ClientID%>").value != "") {
            expensetotal += parseFloat(document.getElementById("<%=txtReccuringDeposit.ClientID%>").value);
        }
        
        //Liabilities
        if (document.getElementById("<%=txtHomeLoanLO.ClientID%>").value != "") {
            liabilitiestotal += parseFloat(document.getElementById("<%=txtHomeLoanLO.ClientID%>").value);
        }
        if (document.getElementById("<%=txtAutoLoanLO.ClientID%>").value != "") {
            liabilitiestotal += parseFloat(document.getElementById("<%=txtAutoLoanLO.ClientID%>").value);
        }
        if (document.getElementById("<%=txtPersonalLoanLO.ClientID%>").value != "") {
            liabilitiestotal += parseFloat(document.getElementById("<%=txtPersonalLoanLO.ClientID%>").value);
        }
        if (document.getElementById("<%=txtEducationLoanLO.ClientID%>").value != "") {
            liabilitiestotal += parseFloat(document.getElementById("<%=txtEducationLoanLO.ClientID%>").value);
        }
        if (document.getElementById("<%=txtOtherLoanLO.ClientID%>").value != "") {
            liabilitiestotal += parseFloat(document.getElementById("<%=txtOtherLoanLO.ClientID%>").value);
        }
        //Life Insurance Details
        if (document.getElementById("<%=txtTotalTermSA.ClientID%>").value != "") {
            lifeInsuranceTotal += parseFloat(document.getElementById("<%=txtTotalTermSA.ClientID%>").value);
        }
        if (document.getElementById("<%=txtTotalEndowmentSA.ClientID%>").value != "") {
            lifeInsuranceTotal += parseFloat(document.getElementById("<%=txtTotalEndowmentSA.ClientID%>").value);
        }
        if (document.getElementById("<%=txtTotalWholeLifeSA.ClientID%>").value != "") {
            lifeInsuranceTotal += parseFloat(document.getElementById("<%=txtTotalWholeLifeSA.ClientID%>").value);
        }
        if (document.getElementById("<%=txtTotalMoneyBackSA.ClientID%>").value != "") {
            lifeInsuranceTotal += parseFloat(document.getElementById("<%=txtTotalMoneyBackSA.ClientID%>").value);
        }
        if (document.getElementById("<%=txtTotalULIPSA.ClientID%>").value != "") {
            lifeInsuranceTotal += parseFloat(document.getElementById("<%=txtTotalULIPSA.ClientID%>").value);
        }
        if (document.getElementById("<%=txtTotalOthersLISA.ClientID%>").value != "") {
            lifeInsuranceTotal += parseFloat(document.getElementById("<%=txtTotalOthersLISA.ClientID%>").value);
        }
        //General Insurance
        if (document.getElementById("<%=txtHealthInsuranceCoverSA.ClientID%>").value != "") {
            generalInsuranceTotal += parseFloat(document.getElementById("<%=txtHealthInsuranceCoverSA.ClientID%>").value);
        }
        if (document.getElementById("<%=txtPropertyInsuranceCoverSA.ClientID%>").value != "") {
            generalInsuranceTotal += parseFloat(document.getElementById("<%=txtPropertyInsuranceCoverSA.ClientID%>").value);
        }
        if (document.getElementById("<%=txtPersonalAccidentSA.ClientID%>").value != "") {
            generalInsuranceTotal += parseFloat(document.getElementById("<%=txtPersonalAccidentSA.ClientID%>").value);
        }
        if (document.getElementById("<%=txtOthersGISA.ClientID%>").value != "") {
            generalInsuranceTotal += parseFloat(document.getElementById("<%=txtOthersGISA.ClientID%>").value);
        }

        //LifeInsurance Premium total
        if (document.getElementById("<%=txtTotalTermPremium.ClientID%>").value != "") {
            AllLIandGIPremiumTotal += parseFloat(document.getElementById("<%=txtTotalTermPremium.ClientID%>").value);
        }
        if (document.getElementById("<%=txtTotalEndowmentPremium.ClientID%>").value != "") {
            AllLIandGIPremiumTotal += parseFloat(document.getElementById("<%=txtTotalEndowmentPremium.ClientID%>").value);
        }
        if (document.getElementById("<%=txtTotalWholeLifePremium.ClientID%>").value != "") {
            AllLIandGIPremiumTotal += parseFloat(document.getElementById("<%=txtTotalWholeLifePremium.ClientID%>").value);
        }
        if (document.getElementById("<%=txtTotalMoneyBackPremium.ClientID%>").value != "") {
            AllLIandGIPremiumTotal += parseFloat(document.getElementById("<%=txtTotalMoneyBackPremium.ClientID%>").value);
        }
        if (document.getElementById("<%=txtTotalULIPPremium.ClientID%>").value != "") {
            AllLIandGIPremiumTotal += parseFloat(document.getElementById("<%=txtTotalULIPPremium.ClientID%>").value);
        }
        if (document.getElementById("<%=txtTotalOthersPremium.ClientID%>").value != "") {
            AllLIandGIPremiumTotal += parseFloat(document.getElementById("<%=txtTotalOthersPremium.ClientID%>").value);
        }
        //GeneralInsurance Premium Total
        if (document.getElementById("<%=txtToalHealthInsurancePremium.ClientID%>").value != "") {
            AllLIandGIPremiumTotal += parseFloat(document.getElementById("<%=txtToalHealthInsurancePremium.ClientID%>").value);
        }
        if (document.getElementById("<%=txtTotalPropertyInsurancePremium.ClientID%>").value != "") {
            AllLIandGIPremiumTotal += parseFloat(document.getElementById("<%=txtTotalPropertyInsurancePremium.ClientID%>").value);
        }
        if (document.getElementById("<%=txtTotalPersonalAccidentPremium.ClientID%>").value != "") {
            AllLIandGIPremiumTotal += parseFloat(document.getElementById("<%=txtTotalPersonalAccidentPremium.ClientID%>").value);
        }
        if (document.getElementById("<%=txtTotalPremiumOthers.ClientID%>").value != "") {
            AllLIandGIPremiumTotal += parseFloat(document.getElementById("<%=txtTotalPremiumOthers.ClientID%>").value);
        }

        //All Liability Tab's EMI Total
        if (document.getElementById("<%=txtHomeLoanEMI.ClientID%>").value != "") {
            allEMIsTotal += parseFloat(document.getElementById("<%=txtHomeLoanEMI.ClientID%>").value);
        }
        if (document.getElementById("<%=txtAutoLoanEMI.ClientID%>").value != "") {
            allEMIsTotal += parseFloat(document.getElementById("<%=txtAutoLoanEMI.ClientID%>").value);
        }
        if (document.getElementById("<%=txtPersonalLoanEMI.ClientID%>").value != "") {
            allEMIsTotal += parseFloat(document.getElementById("<%=txtPersonalLoanEMI.ClientID%>").value);
        }
        if (document.getElementById("<%=txtEducationLoanEMI.ClientID%>").value != "") {
            allEMIsTotal += parseFloat(document.getElementById("<%=txtEducationLoanEMI.ClientID%>").value);
        }
        if (document.getElementById("<%=txtOtherLoanEMI.ClientID%>").value != "") {
            allEMIsTotal += parseFloat(document.getElementById("<%=txtOtherLoanEMI.ClientID%>").value);
        }

        
       
       
        //Insurance and EMI Total from LI and GI Premium & Liability EMI
        document.getElementById("<%=txtInsurance.ClientID%>").value = (AllLIandGIPremiumTotal / 12).toString();
        document.getElementById("<%=txtExpenseEMI.ClientID%>").value = (allEMIsTotal / 12).toString();
        //Sectional Total
        document.getElementById("<%=txtAssetTotal.ClientID%>").value = assettotal.toString();
        document.getElementById("<%=txtIncomeTotal.ClientID%>").value = incometotal.toString();
        
        document.getElementById("<%=txtExpenseTotal.ClientID%>").value = (expensetotal + (AllLIandGIPremiumTotal / 12) + (allEMIsTotal / 12)).toString();
        document.getElementById("<%=txtTotalLO.ClientID%>").value = liabilitiestotal.toString();
        document.getElementById("<%=txtTotalLISA.ClientID%>").value = lifeInsuranceTotal.toString();
        document.getElementById("<%=txtTotalGISA.ClientID%>").value = generalInsuranceTotal.toString();
        document.getElementById("<%=txtTotalEmILO.ClientID%>").value = allEMIsTotal.toString();
        
        
    }
    function SubTotal(columnstoadd1, columnstoadd2, columnstoadd3, columnstoadd4) {
        var tmp = 'ctrl_CustomerProspect_';

        var subtotalvalue = 0.0;
        var tmp1 = tmp + columnstoadd1;
        var tmp2 = tmp + columnstoadd2;
        if (columnstoadd3 != "NULL") {
            var tmp3 = tmp + columnstoadd3;
        }
        var tmp4 = tmp + columnstoadd4;
        if (document.getElementById(tmp1).value != "") {
            subtotalvalue += parseFloat(document.getElementById(tmp1).value);
        }
        else {
            document.getElementById(tmp1).value = "0";
        }
        if (document.getElementById(tmp2).value != "") {
            subtotalvalue += parseFloat(document.getElementById(tmp2).value);
        }
        else {
            document.getElementById(tmp2).value = "0";
        }
        if (columnstoadd3 != "NULL") {
            if (document.getElementById(tmp3).value != "") {
                subtotalvalue += parseFloat(document.getElementById(tmp3).value);
            }
            else {
                document.getElementById(tmp3).value = "0";
            }
        }

        document.getElementById(tmp4).value = subtotalvalue.toString();
        Total();
    }
    
</script>

<script type="text/javascript">

    function ClearTaxslabfields() {
        document.getElementById("<%= txtTaxableIncome.ClientID %>").value = "";
        document.getElementById("<%= txtTaxtoBePaid.ClientID %>").value = "";
    }
    
    function CallFuncOnTxtTaxableIncome() {
    
    var TaxSlab = document.getElementById("<%=txtSlabAsPerProfile.ClientID%>");
    var TaxableIncome = document.getElementById("<%=txtTaxableIncome.ClientID%>");
    var TaxToBePaid = document.getElementById("<%= txtTaxtoBePaid.ClientID %>");
    var TaxIncomePreTax =  document.getElementById("<%= txtIncomePreTax.ClientID %>");
    
        if ((parseInt(TaxSlab.value) != 0) && (parseInt(TaxableIncome.value) != 0)) {

            document.getElementById("<%= txtTaxtoBePaid.ClientID %>").value = (parseInt(TaxSlab.value) / 100) * parseInt(TaxableIncome.value);
        }
        if(TaxIncomePreTax.value == "")
        {
        parseInt(TaxIncomePreTax.value) = 0;
        }
    }

    function CallFunOnTxtIncomePreTax() {
        document.getElementById("<%= txtIncomePreTax.ClientID %>").disabled = true;
    }
    function CallFunOnTxtIncomePostTax() {
        document.getElementById("<%= txtIncomePostTax.ClientID %>").disabled = true;
    }
</script>

<script type="text/javascript">


    
    
    function MainCalculate() {
        var TaxToBePaid = document.getElementById("<%= txtTaxtoBePaid.ClientID %>").value;
        var TaxIncomePreTax = document.getElementById("<%= txtIncomePreTax.ClientID %>").value;
        
        if (TaxIncomePreTax != "") {
            document.getElementById("<%= txtIncomePostTax.ClientID %>").value = TaxIncomePreTax - TaxToBePaid;
        }

        
    }

    
</script>



<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<asp:Label ID="headertitle" runat="server" CssClass="HeaderTextBig" Text="Finance Profile"></asp:Label>
<hr />
 <asp:Panel ID="tbl" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
    <table width="100%" cellspacing="0" cellpadding="0">
    <tr>
        <td>
<telerik:RadToolBar ID="aplToolBar" runat="server" OnButtonClick="aplToolBar_ButtonClick"
    Skin="Telerik" EnableEmbeddedSkins="false" EnableShadows="true" EnableRoundedCorners="true"
    Width="100%" Visible="false">
    <Items>
        <telerik:RadToolBarButton ID="btnEdit" runat="server" Text="Edit" Value="Edit" ImageUrl="~/Images/Telerik/EditButton.gif"
            ImagePosition="Left" ToolTip="Edit">            
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton runat="server" Text="Synchronize" Value="Synchronize" ImageUrl="~/Images/Telerik/Synchronize.png"
            ImagePosition="Left" ToolTip="Synchronize">
            </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>
<hr />
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgRecordStatus" runat="server" class="success-msg" align="center" visible="false">
                Record saved Successfully
            </div>
        </td>
    </tr>
</table>



<telerik:RadTabStrip ID="RadTabStrip1" runat="server" EnableTheming="True" Skin="Telerik"
    EnableEmbeddedSkins="False" MultiPageID="CustomerProspectMultiPage" SelectedIndex="0" EnableViewState="true">
    <Tabs>
        <%--<telerik:RadTab ID="summeryTab" runat="server" PostBack="true" TabIndex="0" 
            ImageUrl="/Images/Telerik/FP/Summary.gif" Text="Summary"
            Value="Summary" ToolTip="Summery" Selected="True"> 
        </telerik:RadTab>--%>
        <telerik:RadTab runat="server" onclick="HideStatusMsg()" ImageUrl="/Images/Telerik/FP/Investment.gif" Text="Investment"
            Value="Investment" Selected="true" TabIndex="0">
        </telerik:RadTab>
        <telerik:RadTab runat="server" onclick="HideStatusMsg()" ImageUrl="/Images/Telerik/FP/Income.gif" Text="Income"
            Value="Income" TabIndex="1">
        </telerik:RadTab>
        <telerik:RadTab runat="server" onclick="HideStatusMsg()" ImageUrl="/Images/Telerik/FP/Expense.gif" Text="Expense"
            Value="Expense" TabIndex="2">
        </telerik:RadTab>
        <telerik:RadTab runat="server" onclick="HideStatusMsg()" ImageUrl="/Images/Telerik/FP/Liabilities.gif" Text="Liabilities"
            Value="Liabilities" TabIndex="3">
        </telerik:RadTab>
        <telerik:RadTab runat="server" onclick="HideStatusMsg()" ImageUrl="/Images/Telerik/FP/LifeInsurance.gif" Text="Life Insurance"
            Value="LifeInsurance" TabIndex="4">
        </telerik:RadTab>
        <telerik:RadTab runat="server" onclick="HideStatusMsg()" ImageUrl="/Images/Telerik/FP/GeneralInsurance.gif"
            Text="General Insurance" Value="General Insurance" TabIndex="5">
        </telerik:RadTab>
    </Tabs>
</telerik:RadTabStrip>
<telerik:RadInputManager ID="RadInputManager1" runat="server" Skin="Telerik" 
    EnableEmbeddedSkins="False">
    <telerik:TextBoxSetting BehaviorID="TextBoxBehavior1" Validation-IsRequired="true">
        <TargetControls>
            <telerik:TargetInput ControlID="txtFirstName" />
            <telerik:TargetInput ControlID="txtEmail" />
            <telerik:TargetInput ControlID="txtPanNumber" />
        </TargetControls>
        <Validation IsRequired="True"></Validation>
    </telerik:TextBoxSetting>
    <telerik:DateInputSetting BehaviorID="DateInputBehavior1" Validation-IsRequired="true"
        DateFormat="MM/dd/yyyy">
        <TargetControls>
            <telerik:TargetInput ControlID="dpDOB" />
        </TargetControls>
        <Validation IsRequired="True"></Validation>
    </telerik:DateInputSetting>
    <telerik:RegExpTextBoxSetting BehaviorID="RagExpBehavior1" Validation-IsRequired="true"
        ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" ErrorMessage="Invalid Email">
        <TargetControls>
            <telerik:TargetInput ControlID="txtEmail" />
        </TargetControls>
        <Validation IsRequired="True"></Validation>
    </telerik:RegExpTextBoxSetting>
    <telerik:RegExpTextBoxSetting BehaviorID="RagExpBehavior2" Validation-IsRequired="true"
        ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" ErrorMessage="Invalid Email">
        <TargetControls>
            <telerik:TargetInput ControlID="txtGridEmailId" />
        </TargetControls>
        <Validation IsRequired="True"></Validation>
    </telerik:RegExpTextBoxSetting>
    <telerik:NumericTextBoxSetting DecimalDigits="2" DecimalSeparator="." ErrorMessage="Numbers"
        NegativePattern="-n" PositivePattern="n" Validation-IsRequired="false" AllowRounding="False"
        MaxValue="70368744177" MinValue="-70368744177" GroupSizes="3" 
        Type="Number" Culture="Hindi (India)"
        GroupSeparator="" >
        <TargetControls>
            <telerik:TargetInput ControlID="txtDirectEquityA" />
            <telerik:TargetInput ControlID="txtGoldA" />
            <telerik:TargetInput ControlID="txtMFEquityA" />
            <telerik:TargetInput ControlID="txtCollectiblesA" />
            <telerik:TargetInput ControlID="txtMFDebtA" />
            <telerik:TargetInput ControlID="txtCashAndSavingsA" />
            <telerik:TargetInput ControlID="txtMFHybridEquityA" />
            <telerik:TargetInput ControlID="txtStructuredProductA" />
            <telerik:TargetInput ControlID="txtMFHybridDebtA" />
            <telerik:TargetInput ControlID="txtCommoditiesA" />
            <telerik:TargetInput ControlID="txtFixedIncomeA" />
            <telerik:TargetInput ControlID="txtPrivateEquityA" />
            <telerik:TargetInput ControlID="txtGovtSavingsA" />
            <telerik:TargetInput ControlID="txtPensionGratuitiesA" />
            <telerik:TargetInput ControlID="txtPMSA" />
            <telerik:TargetInput ControlID="txtInvestmentsOthersA" />
            <telerik:TargetInput ControlID="txtPropertyA" />
            <telerik:TargetInput ControlID="txtIncomeTotal" />
            <telerik:TargetInput ControlID="txtOthersIncome" />
            <telerik:TargetInput ControlID="txtBusinessAndProfession" />
            <telerik:TargetInput ControlID="txtAgriculturalIncome" />
            <telerik:TargetInput ControlID="txtRentalProperty" />
            <telerik:TargetInput ControlID="txtCapitalGains" />
            <telerik:TargetInput ControlID="txtSalary" />
            <telerik:TargetInput ControlID="txtFood" />
            <telerik:TargetInput ControlID="txtEntertainmentHolidays" />
            <telerik:TargetInput ControlID="txtRent" />
            <telerik:TargetInput ControlID="txtPersonalWear" />
            <telerik:TargetInput ControlID="txtUtilites" />
            <telerik:TargetInput ControlID="txtInsurance" />
            <telerik:TargetInput ControlID="txtHealthPersonalCare" />
            <telerik:TargetInput ControlID="txtDomesticHelp" />
            <telerik:TargetInput ControlID="txtConveyance" />
            <telerik:TargetInput ControlID="txtExpenseEMI" />
            <telerik:TargetInput ControlID="txtOthersExpense" />
            <telerik:TargetInput ControlID="txtExpenseTotal" />
            <telerik:TargetInput ControlID="txtHomeLoanA" />
            <telerik:TargetInput ControlID="txtHomeLoanEMI" />
            <telerik:TargetInput ControlID="txtAutoLoanA" />
            <telerik:TargetInput ControlID="txtAutoLoanEMI" />
            <telerik:TargetInput ControlID="txtPersonalLoanA" />
            <telerik:TargetInput ControlID="txtPersonalLoanEMI" />
            <telerik:TargetInput ControlID="txtEducationLoanA" />
            <telerik:TargetInput ControlID="txtEducationLoanEMI" />
            <telerik:TargetInput ControlID="txtOtherLoanA" />
            <telerik:TargetInput ControlID="txtOtherLoanEMI" />
            <telerik:TargetInput ControlID="txtAdjustedTermSA" />
            <telerik:TargetInput ControlID="txtTermP" />
            <telerik:TargetInput ControlID="txtAdjustedEndowmentSA" />
            <telerik:TargetInput ControlID="txtEndowmentP" />
            <telerik:TargetInput ControlID="txtAdjustedWholeLifeSA" />
            <telerik:TargetInput ControlID="txtWholeLifeP" />
            <telerik:TargetInput ControlID="txtAdjustedMoneyBackSA" />
            <telerik:TargetInput ControlID="txtMoneyBackP" />
            <telerik:TargetInput ControlID="txtAdjustedULIPSA" />
            <telerik:TargetInput ControlID="txtULIPP" />
            <telerik:TargetInput ControlID="txtULIPSurrMktVal" />
            <telerik:TargetInput ControlID="txtMoneyBackSurrMktVal" />
            <telerik:TargetInput ControlID="txtWholeLifeSurrMktVal" />
            <telerik:TargetInput ControlID="txtEndowmentSurrMktVal" />
            <telerik:TargetInput ControlID="txtTermSurrMktVal" />
            <telerik:TargetInput ControlID="txtOtherSurrMktVal" />
            <telerik:TargetInput ControlID="txtAdjustedOthersLISA" />
            <telerik:TargetInput ControlID="txtOthersLIP" />
            <telerik:TargetInput ControlID="txtHealthInsuranceCoverA" />
            <telerik:TargetInput ControlID="txtHealthInsuranceCoverP" />
            <telerik:TargetInput ControlID="txtPropertyInsuranceCoverA" />
            <telerik:TargetInput ControlID="txtPropertyInsuranceCoverP" />
            <telerik:TargetInput ControlID="txtPersonalAccidentA" />
            <telerik:TargetInput ControlID="txtPersonalAccidentP" />
            <telerik:TargetInput ControlID="txtOthersGIA" />
            <telerik:TargetInput ControlID="txtOthersGIP" />
            <telerik:TargetInput ControlID="txtMFSIPMIS" />
            <telerik:TargetInput ControlID="txtReccuringDeposit" />
            
            
            <telerik:TargetInput ControlID="txtSlabProfile" />
            
            <telerik:TargetInput ControlID="txtTaxableIncome" />
            <telerik:TargetInput ControlID="txtSlabAsPerProfile" />
            <telerik:TargetInput ControlID="txtTaxtoBePaid" />
            <telerik:TargetInput ControlID="txtIncomePreTax" />
            <telerik:TargetInput ControlID="txtIncomePostTax" />
            
            <%-- New Controls For LifeInsurance screen  --%>
            <telerik:TargetInput ControlID="txtAdjustedPremium" />
            <telerik:TargetInput ControlID="txtTotalTermPremium" />
            <telerik:TargetInput ControlID="txtAdjustedEndowmentPremium" />
            <telerik:TargetInput ControlID="txtTotalEndowmentPremium" />
            <telerik:TargetInput ControlID="txtAdjustedWholeLifePremium" />
            <telerik:TargetInput ControlID="txtTotalWholeLifePremium" />
            <telerik:TargetInput ControlID="txtAdjustedMoneyBackPremium" />
            <telerik:TargetInput ControlID="txtTotalMoneyBackPremium" />
            <telerik:TargetInput ControlID="txtAdjustedULIPPremium" />
            <telerik:TargetInput ControlID="txtTotalULIPPremium" />
            <telerik:TargetInput ControlID="txtAdjustedOthersLIPremium" />
            <telerik:TargetInput ControlID="txtTotalOthersPremium" />
            
            
            <telerik:TargetInput ControlID="txtDirectEquity" />
            <telerik:TargetInput ControlID="txtMFEquity" />
            <telerik:TargetInput ControlID="txtMFDebt" />
            <telerik:TargetInput ControlID="txtMFHybridEquity" />
            <telerik:TargetInput ControlID="txtMFHybridDebt" />
             <telerik:TargetInput ControlID="txtFixedIncome" />
            <telerik:TargetInput ControlID="txtGovtSavings" />
             <telerik:TargetInput ControlID="txtPensionGratuities" />
            <telerik:TargetInput ControlID="txtProperty" />
             <telerik:TargetInput ControlID="txtGold" />
            <telerik:TargetInput ControlID="txtCollectibles" />
            <telerik:TargetInput ControlID="txtCashAndSavings" />
            <telerik:TargetInput ControlID="txtStructuredProduct" />
            <telerik:TargetInput ControlID="txtCommodities" />
            <telerik:TargetInput ControlID="txtPrivateEquity" />
            <telerik:TargetInput ControlID="txtPMS" />
            <telerik:TargetInput ControlID="txtInvestmentsOthers" />
            
            <telerik:TargetInput ControlID="txtHomeLoanLO" />
            <telerik:TargetInput ControlID="txtAutoLoanLO" />
            <telerik:TargetInput ControlID="txtPersonalLoanLO" />
            <telerik:TargetInput ControlID="txtEducationLoanLO" />
            <telerik:TargetInput ControlID="txtOtherLoanLO" />
            
            <%-- Added new text boxes for General Insurance --%>
            
            <telerik:TargetInput ControlID="txtAdjustedHealthPremium" />
            <telerik:TargetInput ControlID="txtToalHealthInsurancePremium" />
            <telerik:TargetInput ControlID="txtAdjustedPropertyInsurancePremium" />
            <telerik:TargetInput ControlID="txtTotalPropertyInsurancePremium" />
            <telerik:TargetInput ControlID="txtPersonalAccidentPremium" />
            <telerik:TargetInput ControlID="txtTotalPersonalAccidentPremium" />
            <telerik:TargetInput ControlID="txtAdjustedOtherGIPremium" />
            <telerik:TargetInput ControlID="txtTotalPremiumOthers" />
            
            <telerik:TargetInput ControlID="txtTotalLO" />
            <telerik:TargetInput ControlID="txtTotalEmILO" />
            <telerik:TargetInput ControlID="txtAssetTotal" />
            <telerik:TargetInput ControlID="txtTotalLISA" />
            <telerik:TargetInput ControlID="txtTotalGISA" />
            
           
        </TargetControls>
    </telerik:NumericTextBoxSetting>
    <telerik:DateInputSetting DateFormat="dd-MM-yyyy" DisplayDateFormat="dd-MM-yyyy"
        ErrorMessage=" InValid Date Format">
        <TargetControls>
            <%--<telerik:TargetInput ControlID="dpDOB" />--%>
            <telerik:TargetInput ControlID="dpTermLIMD" />
            <telerik:TargetInput ControlID="dpEndowmentLIMD" />
            <telerik:TargetInput ControlID="dpWholeLifeLIMD" />
            <telerik:TargetInput ControlID="dpMoneyBackLIMD" />
            <telerik:TargetInput ControlID="dpULIPSLIMD" />
            <telerik:TargetInput ControlID="dpOthersLIMD" />
            <telerik:TargetInput ControlID="dpHealthInsuranceCoverGIMD" />
            <telerik:TargetInput ControlID="dpPropertyInsuranceCoverGIMD" />
            <telerik:TargetInput ControlID="dpPersonalAccidentGIMD" />
            <telerik:TargetInput ControlID="dpOthersGIMD" />
        </TargetControls>
    </telerik:DateInputSetting>
    <telerik:NumericTextBoxSetting DecimalDigits="0" DecimalSeparator="." GroupSeparator=","
        GroupSizes="3" PositivePattern="n" AllowRounding="false" ErrorMessage="Invalid Entry">
        <TargetControls>
            <telerik:TargetInput ControlID="txtHomeLoanT" />
            <telerik:TargetInput ControlID="txtAutoLoanT" />
            <telerik:TargetInput ControlID="txtPersonalLoanT" />
            <telerik:TargetInput ControlID="txtEducationLoanT" />
            <telerik:TargetInput ControlID="txtOtherLoanT" />
        </TargetControls>
    </telerik:NumericTextBoxSetting>
</telerik:RadInputManager>
<telerik:RadAjaxLoadingPanel ID="FamilyMemberDetailsLoading" runat="server" Skin="Telerik"
    EnableEmbeddedSkins="false">
</telerik:RadAjaxLoadingPanel>
<telerik:RadInputManager ID="RadInputManager2" runat="server">
    <telerik:NumericTextBoxSetting Culture="Hindi (India)" DecimalDigits="2" DecimalSeparator="."
        GroupSeparator="," GroupSizes="3" NegativePattern="-n" PositivePattern="n">
        <TargetControls>
            <telerik:TargetInput ControlID="txtAssets" />
            <telerik:TargetInput ControlID="txtLifeInsurance" />
            <telerik:TargetInput ControlID="txtLiabilities" />
            <telerik:TargetInput ControlID="txtGeneralInsurance" />
            <telerik:TargetInput ControlID="txtExpense" />
            <telerik:TargetInput ControlID="txtIncome" />
        </TargetControls>
    </telerik:NumericTextBoxSetting>
    <telerik:TextBoxSetting BehaviorID="TextBoxBehavior1" Validation-IsRequired="true"
        ErrorMessage="Is Required">
        <TargetControls>
            <telerik:TargetInput ControlID="txtFirstName" />
            <telerik:TargetInput ControlID="txtEmail" />
            <telerik:TargetInput ControlID="txtChildFirstName" />
            <telerik:TargetInput ControlID="txtGridEmailId" />
        </TargetControls>
        <Validation IsRequired="True"></Validation>
    </telerik:TextBoxSetting>
    <telerik:DateInputSetting BehaviorID="DateInputBehavior1" Validation-IsRequired="true"
        DateFormat="MM/dd/yyyy">
        <TargetControls>
            <telerik:TargetInput ControlID="dpDOB" />
        </TargetControls>
        <Validation IsRequired="True"></Validation>
    </telerik:DateInputSetting>
    <telerik:RegExpTextBoxSetting BehaviorID="RagExpBehavior1" Validation-IsRequired="true"
        ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" ErrorMessage="Invalid Email">
        <TargetControls>
            <telerik:TargetInput ControlID="txtEmail" />
            <telerik:TargetInput ControlID="txtGridEmailId" />
        </TargetControls>
    </telerik:RegExpTextBoxSetting>
</telerik:RadInputManager>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Telerik"
    EnableEmbeddedSkins="false">
</telerik:RadAjaxLoadingPanel>
<telerik:RadMultiPage ID="CustomerProspectMultiPage" EnableViewState="true"  runat="server" SelectedIndex="0">
    <%--<telerik:RadPageView ID="RadPageView1" runat="server">
        <asp:Panel ID="pnlSummary" runat="server">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" CssClass="HeaderText" Text="Self"></asp:Label>
                        <hr />
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblFirstName" runat="server" CssClass="FieldName" Text="First Name : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFirstName" runat="server" Text="" />
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblMiddleName" runat="server" CssClass="FieldName" Text="Middle Name : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtMiddleName" runat="server" Text="" />
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblLastName" runat="server" CssClass="FieldName" Text="Last Name : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLastName" runat="server" Text="" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblDateOfBirth" runat="server" CssClass="FieldName" Text="Date of Birth : "></asp:Label>
                                </td>
                                <td align="left">
                                    <telerik:RadDatePicker ID="dpDOB" runat="server" ShowAnimation-Type="Fade" Skin="Telerik"
                                        EnableEmbeddedSkins="false" MinDate="1900-01-01">
                                        <Calendar Skin="Telerik" EnableEmbeddedSkins="false" UseColumnHeadersAsSelectors="False"
                                            UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                        <DateInput DateFormat="M/d/yyyy" DisplayDateFormat="d/M/yyyy">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                   
                                    
                                    <span id="Span10" class="spnRequiredField">*</span>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblEmailId" runat="server" CssClass="FieldName" Text="Email Id : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtEmail" runat="server" Text="" />
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblPickBranch" runat="server" CssClass="FieldName" Text="Branch Name : "></asp:Label>
                                </td>
                                <td align="left">
                                    <telerik:RadComboBox ID="ddlPickBranch" runat="server" EmptyMessage="Pick a Branch here"
                                        ExpandAnimation-Type="Linear" ShowToggleImage="True" Skin="Telerik" EnableEmbeddedSkins="false">
                                        <ExpandAnimation Type="InExpo" />
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlPickBranch"
                                        CssClass="validator" ErrorMessage="Please pick a branch" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblPanNumber" runat="server" Text="PAN Number : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPanNumber" runat="server" Text="" MaxLength="10" />
                                    <span id="Span6" class="spnRequiredField">*</span>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblAddress1" runat="server" Text="Address 1 : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAddress1" runat="server" Text="" />
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblAddress2" runat="server" Text="Address 2 : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAddress2" runat="server" Text="" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblCity" runat="server" Text="City : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtCity" runat="server" Text="" />
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblState" runat="server" Text="State : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtState" runat="server" Text="" />
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblCountry" runat="server" Text="Country : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtCountry" runat="server" Text="" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblPinCode" runat="server" Text="PinCode : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPinCode" runat="server" Text="" MaxLength="6" />
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblMobileNo" runat="server" Text="MobileNo : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtMobileNo" runat="server" Text="" MaxLength="10" />
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblProspectAddDate" runat="server" Text="Prospect Add Date : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <telerik:RadDatePicker ID="dpProspectAddDate" runat="server" Culture="English (United States)"
                                        Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                            Skin="Telerik" EnableEmbeddedSkins="false">
                                        </Calendar>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                        <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table width="100%" id="tblChildCustomer" runat="server">
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" CssClass="HeaderText" Text="Family Member Details"></asp:Label>
                        <hr />
                        <table width="100%">
                            <tr>
                                <td>
                                    <telerik:RadAjaxPanel ID="ChildCustomerGridPanel" runat="server" Width="100%" HorizontalAlign="NotSet"
                                        LoadingPanelID="FamilyMemberDetailsLoading" EnablePageHeadUpdate="False">
                                        <telerik:RadGrid ID="RadGrid1" runat="server" Width="96%" GridLines="None" AutoGenerateColumns="False"
                                            PageSize="13" AllowSorting="True" AllowPaging="True" OnNeedDataSource="RadGrid1_NeedDataSource"
                                            ShowStatusBar="True" OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
                                            Skin="Telerik" EnableEmbeddedSkins="false" OnUpdateCommand="RadGrid1_UpdateCommand"
                                            OnItemDataBound="RadGrid1_ItemDataBound">
                                            <PagerStyle Mode="NextPrevAndNumeric" Position="Bottom" />
                                            <MasterTableView DataKeyNames="C_CustomerId" AllowMultiColumnSorting="True" Width="100%"
                                                CommandItemDisplay="Top" AutoGenerateColumns="false" EditMode="InPlace">
                                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                <Columns>
                                                    <telerik:GridEditCommandColumn UpdateText="Update" UniqueName="EditCommandColumn" CancelImageUrl="../Images/Telerik/Cancel.gif" 
                                                    InsertImageUrl="../Images/Telerik/Update.gif" UpdateImageUrl="../Images/Telerik/Update.gif" EditImageUrl="../Images/Telerik/Edit.gif"
                                                        CancelText="Cancel" ButtonType="ImageButton">
                                                        <HeaderStyle Width="85px"></HeaderStyle>
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridDropDownColumn UniqueName="CustomerRelationship" HeaderText="Relationship"
                                                        DataField="CustomerRelationship" DataSourceID="SqlDataSourceCustomerRelation"
                                                        HeaderStyle-HorizontalAlign="Center" ColumnEditorID="GridDropDownColumnEditor1"
                                                        ListTextField="XR_Relationship" ListValueField="XR_RelationshipCode" DropDownControlType="RadComboBox"
                                                        ReadOnly="false">
                                                    </telerik:GridDropDownColumn>
                                                    <telerik:GridTemplateColumn HeaderText="First Name" SortExpression="FirstName" UniqueName="FirstName"
                                                        EditFormColumnIndex="1" HeaderStyle-HorizontalAlign="Center">
                                                        <HeaderStyle Width="80px" />
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblChildFirstName" Text='<%# Eval("FirstName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtChildFirstName" Text='<%# Bind("FirstName") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn UniqueName="MiddleName" HeaderText="Middle Name" DataField="MiddleName"
                                                        HeaderStyle-HorizontalAlign="Center" />
                                                    <telerik:GridBoundColumn UniqueName="LastName" HeaderText="Last Name" DataField="LastName"
                                                        HeaderStyle-HorizontalAlign="Center" />
                                                    <telerik:GridDateTimeColumn UniqueName="DOB" PickerType="DatePicker" HeaderText="Date of Birth"
                                                        HeaderStyle-HorizontalAlign="Center" DataField="DOB" FooterText="DateTimeColumn footer"
                                                        DataFormatString="{0:dd/MM/yyyy}" EditDataFormatString="dd MMMM, yyyy" MinDate="1900-01-01"
                                                        DefaultInsertValue="">
                                                        <ItemStyle Width="120px" />
                                                    </telerik:GridDateTimeColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Email-Id" SortExpression="Email-Id" UniqueName="EmailId"
                                                        HeaderStyle-HorizontalAlign="Center" EditFormColumnIndex="1">
                                                        <HeaderStyle Width="80px" />
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblGridEmailId" Text='<%# Eval("EmailId")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtGridEmailId" Text='<%# Bind("EmailId") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete" ImageUrl="../Images/Telerik/Delete.gif"
                                                        ButtonType="ImageButton" />
                                                </Columns>
                                                <EditFormSettings CaptionFormatString="Edit details for employee with ID {0}" CaptionDataField="FirstName">
                                                    <FormTableItemStyle Width="100%" Height="29px"></FormTableItemStyle>
                                                    <FormTableStyle GridLines="None" CellSpacing="0" CellPadding="2"></FormTableStyle>
                                                    <FormStyle Width="100%" BackColor="#eef2ea"></FormStyle>
                                                    <EditColumn ButtonType="ImageButton" />
                                                </EditFormSettings>
                                            </MasterTableView>
                                            <ClientSettings>
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                        <asp:SqlDataSource ID="SqlDataSourceCustomerRelation" runat="server" ConnectionString="<%$ ConnectionStrings:wealtherp %>"
                                            SelectCommand="SP_GetCustomerRelation" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                    </telerik:RadAjaxPanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" CssClass="HeaderText" Text="Details"></asp:Label>
                        <hr />
                        <table width="60%">
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblAssets" runat="server" CssClass="FieldName" Text="Assets : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAssets" runat="server" Text="" Enabled="false" Style="direction: rtl" />
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblLifeInsurance" runat="server" CssClass="FieldName" Text="Life Insurance : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLifeInsurance" runat="server" Text="" Enabled="false" Style="direction: rtl" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblLiabilities" runat="server" CssClass="FieldName" Text="Liabilities(Out Standing) : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLiabilities" runat="server" Text="" Enabled="false" Style="direction: rtl" />
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblGeneralInsurance" runat="server" CssClass="FieldName" Text="Genral Insurance : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtGeneralInsurance" runat="server" Text="" Enabled="false" Style="direction: rtl" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblExpense" runat="server" CssClass="FieldName" Text="Expense(Per Month) : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtExpense" runat="server" Text="" Enabled="false" Style="direction: rtl" />
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblIncome" runat="server" CssClass="FieldName" Text="Income(Per Month) : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtIncome" runat="server" Text="" Enabled="false" Style="direction: rtl" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>--%>
    <telerik:RadPageView ID="RadPageView2"  runat="server">
        <asp:Panel ID="pnlInvestment" runat="server">
            <table width="100%">
                <tr>
                    <td>
                        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1"
                            HorizontalAlign="NotSet">
                            <table width="100%">                                
                                 <tr>
                                    <td align="right">
                                        
                                    </td>
                                    <td align="center">
                                         <asp:Label ID="Label6" runat="server" Text="Portfolio <br> (Managed)" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td align="center">
                                         <asp:Label ID="Label7" runat="server" Text="Portfolio <br> (UnManaged)" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td align="center">
                                         <asp:Label ID="Label8" runat="server" Text="<br />Adjustment" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td align="center">
                                         <asp:Label ID="Label9" runat="server" Text="<br />Total" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td align="right">
                                        
                                    </td>
                                    <td align="center">
                                         <asp:Label ID="Label5" runat="server" Text="Portfolio <br> (Managed)" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td align="center">
                                         <asp:Label ID="Label10" runat="server" Text="Portfolio <br> (UnManaged)" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td align="center">
                                         <asp:Label ID="Label11" runat="server" Text="<br />Adjustment" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td align="center">
                                         <asp:Label ID="Label12" runat="server" Text="<br />Total" CssClass="FieldName"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblDirectEquity" runat="server" Text="Direct Equity : " CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPDirectEquityM" runat="server" Style="direction: rtl" Width="75px"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPDirectEquityUM" runat="server" Style="direction: rtl" Width="75px"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtDirectEquityA" runat="server" Style="direction: rtl" Width="75px"
                                            onChange="SubTotal('txtWERPDirectEquityM','txtWERPDirectEquityUM','txtDirectEquityA','txtDirectEquity')"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtDirectEquity" runat="server" Style="direction: rtl" Width="75px" ></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblGold" runat="server" Text="Gold : " CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPGoldM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPGoldUM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtGoldA" runat="server" Style="direction: rtl" Width="75px" onChange="SubTotal('txtWERPGoldM','txtWERPGoldUM','txtGoldA','txtGold')"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtGold" runat="server" Style="direction: rtl" Width="75px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblMFEquity" runat="server" Text="MF-Equity : " CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPMFEquityM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPMFEquityUM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtMFEquityA" runat="server" Style="direction: rtl" Width="75px"
                                            onChange="SubTotal('txtWERPMFEquityM','txtWERPMFEquityUM','txtMFEquityA','txtMFEquity')"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtMFEquity" runat="server" Style="direction: rtl" Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblCollectibles" runat="server" Text="Collectibles : " CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPCollectiblesM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPCollectiblesUM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCollectiblesA" runat="server" Style="direction: rtl" Width="75px"
                                            onChange="SubTotal('txtWERPCollectiblesM','txtWERPCollectiblesUM','txtCollectiblesA','txtCollectibles')"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCollectibles" runat="server" Style="direction: rtl" Width="75px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblMFDebt" runat="server" Text="MF-Debt : " CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPMFDebtM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPMFDebtUM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtMFDebtA" runat="server" Style="direction: rtl" Width="75px" onChange="SubTotal('txtWERPMFDebtM','txtWERPMFDebtUM','txtMFDebtA','txtMFDebt')"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtMFDebt" runat="server" Style="direction: rtl" Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblCashAndSavings" runat="server" Text="Cash & Savings : " CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPCashAndSavingsM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPCashAndSavingsUM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCashAndSavingsA" runat="server" Style="direction: rtl" Width="75px"
                                            onChange="SubTotal('txtWERPCashAndSavingsM','txtWERPCashAndSavingsUM','txtCashAndSavingsA','txtCashAndSavings')"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCashAndSavings" runat="server" Style="direction: rtl" Width="75px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblMFHybridEquity" runat="server" Text="MF Hybrid - Equity : " CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPMFHybridEquityM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPMFHybridEquityUM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtMFHybridEquityA" runat="server" Style="direction: rtl" Width="75px"
                                            onChange="SubTotal('txtWERPMFHybridEquityM','txtWERPMFHybridEquityUM','txtMFHybridEquityA','txtMFHybridEquity')"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtMFHybridEquity" runat="server" Style="direction: rtl" Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblStructuredProduct" runat="server" Text="Structured Product : "
                                            CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPStructuredProductM" runat="server" Style="direction: rtl"
                                            Enabled="false" Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPStructuredProductUM" runat="server" Style="direction: rtl"
                                            Enabled="false" Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtStructuredProductA" runat="server" Style="direction: rtl" Width="75px"
                                            onChange="SubTotal('txtWERPStructuredProductM','txtWERPStructuredProductUM','txtStructuredProductA','txtStructuredProduct')"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtStructuredProduct" runat="server" Style="direction: rtl" Width="75px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblMFHybridDebt" runat="server" Text="MF Hybrid - Debt : " CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPMFHybridDebtM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPMFHybridDebtUM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtMFHybridDebtA" runat="server" Style="direction: rtl" Width="75px"
                                            onChange="SubTotal('txtWERPMFHybridDebtM','txtWERPMFHybridDebtUM','txtMFHybridDebtA','txtMFHybridDebt')"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtMFHybridDebt" runat="server" Style="direction: rtl" Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblCommodities" runat="server" Text="Commodities : " CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPCommoditiesM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPCommoditiesUM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCommoditiesA" runat="server" Style="direction: rtl" Width="75px"
                                            onChange="SubTotal('txtWERPCommoditiesM','txtWERPCommoditiesUM','txtCommoditiesA','txtCommodities')"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCommodities" runat="server" Style="direction: rtl" Width="75px" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblFixedIncome" runat="server" Text="Fixed Income : " CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPFixedIncomeM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPFixedIncomeUM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtFixedIncomeA" runat="server" Style="direction: rtl" Width="75px"
                                            onChange="SubTotal('txtWERPFixedIncomeM','txtWERPFixedIncomeUM','txtFixedIncomeA','txtFixedIncome')"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtFixedIncome" runat="server" Style="direction: rtl" Width="75px" ></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblPrivateEquity" runat="server" Text="Private Equity : " CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPPrivateEquityM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPPrivateEquityUM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPrivateEquityA" runat="server" Style="direction: rtl" Width="75px"
                                            onChange="SubTotal('txtWERPPrivateEquityM','txtWERPPrivateEquityUM','txtPrivateEquityA','txtPrivateEquity')"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPrivateEquity" runat="server" Style="direction: rtl" Width="75px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblGovtSavings" runat="server" Text="Govt Savings : " CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPGovtSavingsM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPGovtSavingsUM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtGovtSavingsA" runat="server" Style="direction: rtl" Width="75px"
                                            onChange="SubTotal('txtWERPGovtSavingsM','txtWERPGovtSavingsUM','txtGovtSavingsA','txtGovtSavings')"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtGovtSavings" runat="server" Style="direction: rtl" Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblPMS" runat="server" Text="PMS : " CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPPMSM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPPMSUM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPMSA" runat="server" Style="direction: rtl" Width="75px" onChange="SubTotal('txtWERPPMSM','txtWERPPMSUM','txtPMSA','txtPMS')"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPMS" runat="server" Style="direction: rtl" Width="75px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblPensionGratuities" runat="server" Text="Pension & Gratuities : "
                                            CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPPensionGratuitiesM" runat="server" Style="direction: rtl"
                                            Enabled="false" Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPPensionGratuitiesUM" runat="server" Style="direction: rtl"
                                            Enabled="false" Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPensionGratuitiesA" runat="server" Style="direction: rtl" Width="75px"
                                            onChange="SubTotal('txtWERPPensionGratuitiesM','txtWERPPensionGratuitiesUM','txtPensionGratuitiesA','txtPensionGratuities')"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPensionGratuities" runat="server" Style="direction: rtl" Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblOthers" runat="server" Text="Others : " CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPInvestmentsOthersM" runat="server" Style="direction: rtl"
                                            Enabled="false" Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPInvestmentsOthersUM" runat="server" Style="direction: rtl"
                                            Enabled="false" Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtInvestmentsOthersA" runat="server" Style="direction: rtl" Width="75px"
                                            onChange="SubTotal('txtWERPInvestmentsOthersM','txtWERPInvestmentsOthersUM','txtInvestmentsOthersA','txtInvestmentsOthers')"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtInvestmentsOthers" runat="server" Style="direction: rtl" Width="75px" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblProperty" runat="server" Text="Property : " CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPPropertyM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWERPPropertyUM" runat="server" Style="direction: rtl" Enabled="false"
                                            Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPropertyA" runat="server" Style="direction: rtl" Width="75px"
                                            onChange="SubTotal('txtWERPPropertyM','txtWERPPropertyUM','txtPropertyA','txtProperty')"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtProperty" runat="server" Style="direction: rtl" Width="75px"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                    </td>
                                    <td align="left">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td align="right">
                                      
                                    </td>
                                    <td align="left">
                                         
                                    </td>
                                   <td align="right">
                                    <asp:Label ID="lblAssetTotal" runat="server" Text="Grand Total : " CssClass="FieldName"></asp:Label>
                                   </td>
                                   
                                    <td align="left" colspan="2">
                                      <asp:TextBox ID="txtAssetTotal" runat="server" Style="direction: rtl" Enabled="false" width="175px"
                                             EnableViewState="true"></asp:TextBox> 
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                       &nbsp; 
                                    </td>
                                    <td align="left">
                                        &nbsp; 
                                    </td>
                                    <td align="right">
                                    </td>
                                    <td align="left">
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadAjaxPanel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageView3" runat="server">
           <asp:Panel ID="pnlIncome" runat="server">
            <table width="100%">
                <tr>
                    <td>
                        <table width="60%">
                        <tr>
                        <td colspan="4">
                        <asp:Label ID="lblincomenote" runat="server" Text="Please Enter Monthly Income Details." Font-Size="Small" CssClass="cmbField"></asp:Label>
                        </td>
                        </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblSalary" runat="server" Text="Gross Salary : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSalary" runat="server" Style="direction: rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                
                                <td align="right">
                                    <asp:Label ID="lblTxtSlabProfile" runat="server" Text="Your Tax slab: " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSlabProfile" runat="server" onblur="fillValueToPopup();" Style="direction: rtl" Enabled="false"
                                        EnableViewState="true"></asp:TextBox>
                                </td>
                                
                                
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblRentalProperty" runat="server" CssClass="FieldName" Text="Property Rental Income: "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtRentalProperty" runat="server" Style="direction: rtl" onchange="Total()"></asp:TextBox>
                                </td>  
                                
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblCapitalGains" runat="server" Text="Capital Gains : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtCapitalGains" runat="server" Style="direction: rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <%--<td align="right">
                                    <asp:Label ID="lblDisposable" runat="server" Text="Disposable Income (post tax) : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtDisposable" runat="server" Style="direction: rtl" ></asp:TextBox>
                                </td>--%>
                                
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblBusinessAndProfession" runat="server" Text="Business & Profession : "
                                        CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtBusinessAndProfession" runat="server" Style="direction: rtl"
                                        onchange="Total()"></asp:TextBox>
                                </td>
                                
                               <%-- <td colspan="2" align="right">
                                   <asp:LinkButton ID="btnSlabGettingCalculator" runat="server" 
                                        style="cursor: pointer;" Font-Underline="true" ForeColor="Blue" Font-Size="X-Small"
                                   OnClientClick="return CallFunOnCalculation()" 
                                        onclick="btnSlabGettingCalculator_Click">Disposable Income Calculator</asp:LinkButton>
                                </td>--%>
                                
                            </tr>
                            <tr>
                            <td align="right">
                                    <asp:Label ID="lblAgriculturalIncome" runat="server" Text="Agricultural Income : "
                                        CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAgriculturalIncome" runat="server" Style="direction: rtl" onchange="Total()"></asp:TextBox>
                                </td>
                             
                            
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblOthersIncome" runat="server" Text="Others : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOthersIncome" runat="server" Style="direction: rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                
                                <td align="right">
                                    <asp:Label ID="lblIncomeTotal" runat="server" Text="Grand Total :  " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtIncomeTotal" runat="server" Style="direction: rtl" 
                                        EnableViewState="true"></asp:TextBox>
                                </td>
                                
                            </tr>
                            <tr runat="server" visible="false">
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                                 <td align="left">
                                    <cc1:ModalPopupExtender ID="mdlPopupSlabCalculate" runat="server" PopupControlID="CalculatePopUp"
                                    TargetControlID="hdnModalPopupId" OkControlID="btnCalculationSubmit" CancelControlID="btnCancel" BackgroundCssClass="modalBackground" Enabled="true" Drag="true"></cc1:ModalPopupExtender>
                                    
                                </td>
                                <td>
                                    <asp:Panel ID="CalculatePopUp" onclick="MainCalculate()" Width="300px" CssClass="ModelPup" runat="server">
                                     <table>
                                         <tr>
                                            <td>
                                                <asp:Label ID="lblTaxableIncome" runat="server" Text="Enter Taxable Income" CssClass="FieldName"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtTaxableIncome" runat="server" onblur="MainCalculate()" onchange="CallFuncOnTxtTaxableIncome()" Style="direction: rtl" ></asp:TextBox>
                                                
                                            </td>
                                        </tr>
                                        <tr>    
                                            <td>
                                                <asp:Label ID="lbltxtSlabAsPerProfile" runat="server" Text="As per your profile your tax slab is" CssClass="FieldName"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtSlabAsPerProfile" runat="server" ReadOnly="false" Enabled="false" Style="direction: rtl" ></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTaxtoBePaid" runat="server" Text="Tax to be paid" CssClass="FieldName"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtTaxtoBePaid" runat="server" Style="direction: rtl" ></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblIncomePreTax" runat="server" Text="Disposable income (pre tax)" CssClass="FieldName"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtIncomePreTax" runat="server" onfocus="CallFunOnTxtIncomePreTax()" Style="direction: rtl" ></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblIncomePostTax" runat="server" Text="Disposable income (post tax)" CssClass="FieldName"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtIncomePostTax" ReadOnly="false" onfocus="CallFunOnTxtIncomePostTax()" runat="server" Style="direction: rtl" ></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnCalculationSubmit" runat="server" Text="Submit" 
                                                    CssClass="PCGButton" OnClientClick="SubmitButtonFun()" onclick="btnCalculationSubmit_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnCancel" runat="server" OnClientClick="ClearTaxslabfields()" Text="Cancel" CssClass="PCGButton" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblFinalResults" runat="server" Text="" CssClass="FieldName"></asp:Label> 
                                            </td>
                                        </tr>
                                    </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr runat="server" visible="false">
                            <td colspan="5">
                            <asp:Label ID="lblIncomeNote2" runat="server" Text="Notes: 1.If you have entered the pretax income then you can calculate the disposal <br />
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;income using Disposal income Calculator.
                             <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.Taxslab can be edited from the profile" Font-Size="Small" CssClass="cmbField">
                            </asp:Label>
                            </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageView4" runat="server">
        <asp:Panel ID="pnlExpense" runat="server">
            <table width="100%">
                <tr>
                    <td>
                        <table width="60%">
                         <tr>
                        <td colspan="4">
                        <asp:Label ID="lblexpensenote" runat="server" Text="Please Enter Monthly Expense Details." Font-Size="Small" CssClass="cmbField"></asp:Label>
                        </td>
                        </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblFood" runat="server" Text="Food : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFood" runat="server" Style="direction: rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                   <td align="right">
                                    <asp:Label ID="lblPersonalWear" runat="server" Text="Personal Wear : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPersonalWear" runat="server" Style="direction: rtl" onchange="Total()"></asp:TextBox>
                                </td>
                        
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblRent" runat="server" Text="Rent : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtRent" runat="server" Style="direction: rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="right">
                               <asp:Label ID="lblInsurance" runat="server" Text="Insurance Premium : " CssClass="FieldName"></asp:Label>
                               </td>
                               <td align="left">
                                    <asp:TextBox ID="txtInsurance" ReadOnly="false" Enabled="false" runat="server" 
                                        Style="direction: rtl" ></asp:TextBox>
                                </td>
                             <%--  <td align="right">
                                    <asp:Label ID="lblDomesticHelp" runat="server" Text="Domestic Help : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtDomesticHelp" runat="server" Style="direction: rtl" onchange="Total()"></asp:TextBox>
                                </td>--%>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblUtilites" runat="server" Text="Utilities : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtUtilites" runat="server" Style="direction: rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblMFMIS" runat="server" Text="MF SIP:" CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtMFSIPMIS" runat="server" ReadOnly="false" Enabled="false" Style="direction: rtl" onchange="Total()></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right"><asp:Label ID="lblHealthPersonalCare" runat="server" Text="Health-Personal Care : "
                                        CssClass="FieldName"></asp:Label></td><td align="left">
                                    <asp:TextBox ID="txtHealthPersonalCare" runat="server" Style="direction: rtl" onchange="Total()"></asp:TextBox></td><td align="right">
                                <asp:Label ID="lblReccuringDeposit" runat="server" Text="Recurring Deposit :" CssClass="FieldName"></asp:Label></td><td align="left">
                                    <asp:TextBox ID="txtReccuringDeposit" runat="server" ReadOnly="false" Enabled="false" Style="direction: rtl" onchange="Total()></asp:TextBox>
                                </td>
                              </tr>
                                    <tr>
                                <td align="right"><asp:Label ID="lblConveyance" runat="server" Text="Conveyance : " CssClass="FieldName"></asp:Label></td><td align="left">
                                    <asp:TextBox ID="txtConveyance" runat="server" Style="direction: rtl" onchange="Total()"></asp:TextBox></td><td align="right">
                                    <asp:Label ID="lblExpenseEMI" runat="server" Text="Loan EMI : " CssClass="FieldName"></asp:Label></td><td align="left">
                                    <asp:TextBox ID="txtExpenseEMI" runat="server" ReadOnly="false" Enabled="false" Style="direction: rtl"></asp:TextBox></td></tr><tr>
                             <td align="right">
                                    <asp:Label ID="lblEntertainmentHolidays" runat="server" Text="Entertainment-Holidays : "
                                        CssClass="FieldName"></asp:Label></td><td align="left">
                                    <asp:TextBox ID="txtEntertainmentHolidays" runat="server" Style="direction: rtl"
                                        onchange="Total()"></asp:TextBox></td><td align="right">
                                    <asp:Label ID="lblOthersExpense" runat="server" Text="Others : " CssClass="FieldName"></asp:Label></td><td align="left">
                                    <asp:TextBox ID="txtOthersExpense" runat="server" Style="direction: rtl" onchange="Total()"></asp:TextBox></td></tr><tr>
                                    <td align="right">
                                    <asp:Label ID="lblDomesticHelp" runat="server" Text="Domestic Help : " CssClass="FieldName"></asp:Label></td><td align="left">
                                    <asp:TextBox ID="txtDomesticHelp" runat="server" Style="direction: rtl" onchange="Total()"></asp:TextBox></td><%--  <td align="right">
                               <asp:Label ID="lblInsurance" runat="server" Text="Insurance Premium : " CssClass="FieldName"></asp:Label>
                               </td>
                               <td align="left">
                                    <asp:TextBox ID="txtInsurance" ReadOnly="false" Enabled="false" runat="server" 
                                        Style="direction: rtl" ></asp:TextBox>
                                </td>--%>
                               <td align="right">
                                <asp:Label ID="lblExpenseTotal" runat="server" Text="Grand Total :  " CssClass="FieldName"></asp:Label></td><td align="left">
                                    <asp:TextBox ID="txtExpenseTotal" runat="server" Style="direction: rtl" Enabled="false" EnableViewState="true"></asp:TextBox></td></tr><tr>
                                    <td colspan="4">
                                    <asp:Label ID="LabelMainNote" runat="server" Text="Note: 1.SIP is coming from MF screen.<br />
                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.Loan EMI is coming from Liabilities.<br />
                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.Insurance Premium is coming from Insurance" Font-Size="Small" CssClass="cmbField"></asp:Label></td></tr></table></td></tr></table></asp:Panel></telerik:RadPageView><telerik:RadPageView ID="RadPageView5" runat="server">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Panel ID="pnlLiabilities" runat="server">
            <table width="100%">
                <tr>
                    <td>
                        <table width="80%">
                            <tr>
                                <td>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lblWERPLiabilities" runat="server" CssClass="FieldName" 
                                        Text="Portfolio"></asp:Label></td><td align="center">
                                    <asp:Label ID="lblLoanOutstandingAdjustment" runat="server" 
                                        CssClass="FieldName" Text="Adjustment"></asp:Label></td><td 
                                    align="center">
                                    <asp:Label ID="lblLoanOutstanding" runat="server" CssClass="FieldName" 
                                        Text="Total"></asp:Label></td><td align="center">
                                    <asp:Label ID="lblEMI" runat="server" CssClass="FieldName" Text="EMI (Annual)"></asp:Label></td></tr><tr>
                                <td align="right">
                                    <asp:Label ID="lblHomeLoan" runat="server" CssClass="FieldName" 
                                        Text="Home Loan : "></asp:Label></td><td align="left">
                                    <asp:TextBox ID="txtWERPHomeLoan" runat="server" Enabled="false" 
                                        Style="direction: rtl"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtHomeLoanA" runat="server" 
                                        onchange="SubTotal('txtWERPHomeLoan','txtHomeLoanA','NULL','txtHomeLoanLO')" 
                                        Style="direction: rtl"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtHomeLoanLO" runat="server" 
                                        onchange="Total()" Style="direction: rtl"></asp:TextBox></td><td 
                                    align="left">
                                    <asp:TextBox ID="txtHomeLoanEMI" runat="server" onchange="Total()" 
                                        Style="direction: rtl"></asp:TextBox></td></tr><tr>
                                <td align="right">
                                    <asp:Label ID="lblAutoLoan" runat="server" CssClass="FieldName" 
                                        Text="Auto Loan : "></asp:Label></td><td align="left">
                                    <asp:TextBox ID="txtWERPAutoLoan" runat="server" Enabled="false" 
                                        Style="direction: rtl"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtAutoLoanA" runat="server" 
                                        onchange="SubTotal('txtWERPAutoLoan','txtAutoLoanA','NULL','txtAutoLoanLO')" 
                                        Style="direction: rtl"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtAutoLoanLO" runat="server" 
                                        onchange="Total()" Style="direction: rtl"></asp:TextBox></td><td 
                                    align="left">
                                    <asp:TextBox ID="txtAutoLoanEMI" runat="server" onchange="Total()" 
                                        Style="direction: rtl"></asp:TextBox></td></tr><tr>
                                <td align="right">
                                    <asp:Label ID="lblPersonalLoan" runat="server" CssClass="FieldName" 
                                        Text="Personal Loan : "></asp:Label></td><td align="left">
                                    <asp:TextBox ID="txtWERPPersonalLoan" runat="server" Enabled="false" 
                                        Style="direction: rtl"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtPersonalLoanA" runat="server" 
                                        onchange="SubTotal('txtWERPPersonalLoan','txtPersonalLoanA','NULL','txtPersonalLoanLO')" 
                                        Style="direction: rtl"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtPersonalLoanLO" runat="server"  
                                        onchange="Total()" Style="direction: rtl"></asp:TextBox></td><td 
                                    align="left">
                                    <asp:TextBox ID="txtPersonalLoanEMI" runat="server" onchange="Total()" 
                                        Style="direction: rtl"></asp:TextBox></td></tr><tr>
                                <td align="right">
                                    <asp:Label ID="lblEducationLoan" runat="server" CssClass="FieldName" 
                                        Text="Education Loan : "></asp:Label></td><td align="left">
                                    <asp:TextBox ID="txtWERPEducationLoan" runat="server" Enabled="false" 
                                        Style="direction: rtl"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtEducationLoanA" runat="server" 
                                        onchange="SubTotal('txtWERPEducationLoan','txtEducationLoanA','NULL','txtEducationLoanLO')" 
                                        Style="direction: rtl"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtEducationLoanLO" runat="server"
                                        onchange="Total()" Style="direction: rtl"></asp:TextBox></td><td 
                                    align="left">
                                    <asp:TextBox ID="txtEducationLoanEMI" runat="server" onchange="Total()" 
                                        Style="direction: rtl"></asp:TextBox></td></tr><tr>
                                <td align="right">
                                    <asp:Label ID="lblOtherLoan" runat="server" CssClass="FieldName" 
                                        Text="Others : "></asp:Label></td><td align="left">
                                    <asp:TextBox ID="txtWERPOtherLoan" runat="server" Enabled="false" 
                                        Style="direction: rtl"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtOtherLoanA" runat="server" 
                                        onchange="SubTotal('txtWERPOtherLoan','txtOtherLoanA','NULL','txtOtherLoanLO')" 
                                        Style="direction: rtl"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtOtherLoanLO" runat="server" 
                                        onchange="Total()" Style="direction: rtl"></asp:TextBox></td><td 
                                    align="left">
                                    <asp:TextBox ID="txtOtherLoanEMI" runat="server" onchange="Total()" 
                                        Style="direction: rtl"></asp:TextBox></td></tr><tr>
                                    <td align="right">
                                    </td><td 
                                    align="left">
                                    </td><td 
                                    align="right">
                                   <asp:Label ID="lblTotal" runat="server" CssClass="FieldName" Text="Grand Total : "></asp:Label></td><td align="left">
                                    <asp:TextBox ID="txtTotalLO" runat="server"  
                                        EnableViewState="true" Style="direction: rtl"></asp:TextBox></td><td align="left">
                                  <asp:TextBox ID="txtTotalEmILO" runat="server" 
                                        EnableViewState="true" onchange="Total()" Style="direction: rtl"></asp:TextBox></td></tr><%-- <tr>
                                            <td align="right">
                                                <asp:Label ID="lblLiabilitiesTotal" runat="server" Text="Total : " CssClass="FieldName"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtLiabilitiesTotalLO" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtLiabilitiesTotalEMI" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>--%>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
&nbsp;&nbsp;&nbsp;
</telerik:RadPageView>

 <telerik:RadPageView ID="RadPageView6" runat="server">
  <asp:Panel ID="pnlLifeInsurance" runat="server">
            <table width="100%">
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td> &nbsp; </td>
                                <td colspan="2" align="center" style="border-bottom: 1px solid gray;">
                                    <asp:Label ID="lblLifeInsurancePortfolio" runat="server" CssClass="FieldName" 
                                        Font-Bold="true" Font-Underline="False" Font-Size="11px" Text="Portfolio"></asp:Label>
                                        
                                 </td>
                                <td colspan="2" align="center"  style="border-bottom: 1px solid gray;">
                                    <asp:Label ID="lblLifeInsuranceAdjustments" Font-Size="11px" runat="server" CssClass="FieldName" 
                                        Font-Bold="true" Font-Underline="False" Text="Adjustments"></asp:Label></td>
                                <td colspan="2" align="center"  style="border-bottom: 1px solid gray;">
                                    <asp:Label ID="lblLifeInsuranceMainTotal" runat="server" CssClass="FieldName" 
                                        Font-Bold="true" Font-Underline="False" Font-Size="11px" Text="Total"></asp:Label></td>                               
                            </tr>
                        
                            <tr>
                                <td align="right">
                                </td>
                                 <td align="left">
                                    <asp:Label ID="lblWERPSumAssuredLI" runat="server" CssClass="FieldName" 
                                        Enabled="false" Text="Sum Assured"></asp:Label></td>
                                 <td align="left">
                                        <asp:Label ID="lblPremiumLI" runat="server" CssClass="FieldName" 
                                        Text="Premium (Annual)"></asp:Label>
                                  </td>
                                 <td align="left">
                                    <asp:Label ID="lblSumAssuredLI" runat="server" CssClass="FieldName" 
                                        Text="Sum Assured"></asp:Label>
                                   </td>  
                                 <td align="left">
                                        <asp:Label ID="Label1" runat="server" CssClass="FieldName" 
                                        Text="Premium (Annual)"></asp:Label>
                                 </td>                               
                                 <td align="left">
                                        <asp:Label ID="lblTotalSumAssuredLI" runat="server" CssClass="FieldName" 
                                        Text="Sum Assured"></asp:Label>
                                 </td>
                                 <td align="left">
                                        <asp:Label ID="lblTotalPremium" runat="server" CssClass="FieldName" 
                                        Text="Premium (Annual)"></asp:Label>
                                  </td>
                                 <td align="left">
                                        <asp:Label ID="lblSurrenderMarketValue" runat="server" CssClass="FieldName" 
                                        Text="Surrender/Market Value"></asp:Label>
                                  </td>
                                </tr>
                             
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblTerm" runat="server" CssClass="FieldName" Text="Term : "></asp:Label></td><td 
                                                align="left">
                                    <asp:TextBox ID="txtWERPTermSA" runat="server" Enabled="false" Style="direction: rtl" 
                                                    Width="90px"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtTermP" runat="server" Enabled="false" onchange="Total()" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtAdjustedTermSA" runat="server" 
                                                    onchange="SubTotal('txtWERPTermSA','txtAdjustedTermSA','NULL','txtTotalTermSA')" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtAdjustedPremium" runat="server" 
                                                    onchange="SubTotal('txtTermP','txtAdjustedPremium','NULL','txtTotalTermPremium')" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtTotalTermSA" runat="server" onchange="Total()" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtTotalTermPremium" runat="server" onchange="Total()" ReadOnly="false" 
                                                    style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtTermSurrMktVal" runat="server" Style="direction: rtl" Width="90px"></asp:TextBox></td></tr>
                             
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblEndowment" runat="server" CssClass="FieldName" 
                                        Text="Endowment : "></asp:Label></td><td align="left">
                                    <asp:TextBox ID="txtWERPEndowmentSA" runat="server" Enabled="false" Style="direction: rtl" 
                                                    Width="90px"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtEndowmentP" runat="server" Enabled="false" onchange="Total()" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtAdjustedEndowmentSA" runat="server" 
                                                    onchange="SubTotal('txtWERPEndowmentSA','txtAdjustedEndowmentSA','NULL','txtTotalEndowmentSA')" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtAdjustedEndowmentPremium" runat="server" 
                                                    onchange="SubTotal('txtEndowmentP','txtAdjustedEndowmentPremium','NULL','txtTotalEndowmentPremium')" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtTotalEndowmentSA" runat="server" onchange="Total()" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtTotalEndowmentPremium" runat="server" onchange="Total()" 
                                                    ReadOnly="false" Style="direction: rtl" Width="90px"></asp:TextBox></td><td 
                                                align="left">
                                    <asp:TextBox ID="txtEndowmentSurrMktVal" runat="server" Style="direction: rtl" Width="90px"></asp:TextBox></td></tr>
                             
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblWholeLife" runat="server" CssClass="FieldName" 
                                        Text="Whole Life : "></asp:Label></td><td align="left">
                                    <asp:TextBox ID="txtWERPWholeLifeSA" runat="server" Enabled="false" Style="direction: rtl" 
                                                    Width="90px"></asp:TextBox></td><td align="left">
                                        <asp:TextBox ID="txtWholeLifeP" runat="server" Enabled="false" onchange="Total()" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                        <asp:TextBox ID="txtAdjustedWholeLifeSA" runat="server" 
                                                    onchange="SubTotal('txtWERPWholeLifeSA','txtAdjustedWholeLifeSA','NULL','txtTotalWholeLifeSA')" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                        <asp:TextBox ID="txtAdjustedWholeLifePremium" runat="server" 
                                                    onchange="SubTotal('txtWholeLifeP','txtAdjustedWholeLifePremium','NULL','txtTotalWholeLifePremium')" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                        <asp:TextBox ID="txtTotalWholeLifeSA" runat="server" onchange="Total()" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtTotalWholeLifePremium" runat="server" onchange="Total()" 
                                                    ReadOnly="false" style="direction: rtl" Width="90px"></asp:TextBox></td><td 
                                                align="left">
                                        <asp:TextBox ID="txtWholeLifeSurrMktVal" runat="server" Style="direction: rtl" 
                                                    Width="90px"></asp:TextBox></td></tr>
                             
                            <tr>
                                    
                                   <td align="right">
                                        <asp:Label ID="lblMoneyBack" runat="server" CssClass="FieldName" 
                                           Text="Money Back : "></asp:Label></td><td align="left">
                                        <asp:TextBox ID="txtWERPMoneyBackSA" runat="server" Enabled="false" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                        <asp:TextBox ID="txtMoneyBackP" runat="server" Enabled="false" onchange="Total()" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                            <asp:TextBox ID="txtAdjustedMoneyBackSA" runat="server" 
                                                    onchange="SubTotal('txtWERPMoneyBackSA','txtAdjustedMoneyBackSA','NULL','txtTotalMoneyBackSA')" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                            <asp:TextBox ID="txtAdjustedMoneyBackPremium" runat="server" 
                                                    onchange="SubTotal('txtMoneyBackP','txtAdjustedMoneyBackPremium','NULL','txtTotalMoneyBackPremium')" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                            
                                            <asp:TextBox ID="txtTotalMoneyBackSA" runat="server" 
                                                    onchange="Total()" Style="direction: rtl" Width="90px"></asp:TextBox></td><td 
                                                align="left">
                                            <asp:TextBox ID="txtTotalMoneyBackPremium" runat="server" onchange="Total()" 
                                                    ReadOnly="false" style="direction: rtl" Width="90px"></asp:TextBox></td><td 
                                                align="left">
                                            <asp:TextBox ID="txtMoneyBackSurrMktVal" runat="server" Style="direction: rtl" 
                                                    Width="90px"></asp:TextBox></td></tr>
                             
                             <tr>
                                    <td align="right">
                                    <asp:Label ID="lblULIP" runat="server" CssClass="FieldName" Text="ULIP : "></asp:Label></td><td 
                                                align="left">
                                    <asp:TextBox ID="txtWERPULIPSA" runat="server" Enabled="false" Style="direction: rtl" 
                                                    Width="90px"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtULIPP" runat="server" Enabled="false" onchange="Total()" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtAdjustedULIPSA" runat="server" 
                                                    onchange="SubTotal('txtWERPULIPSA','txtAdjustedULIPSA','NULL','txtTotalULIPSA')" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                        <asp:TextBox ID="txtAdjustedULIPPremium" runat="server" Height="22px" 
                                                    onchange="SubTotal('txtULIPP','txtAdjustedULIPPremium','NULL','txtTotalULIPPremium')" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                    
                                    <asp:TextBox ID="txtTotalULIPSA" runat="server" onchange="Total()" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                        <asp:TextBox ID="txtTotalULIPPremium" runat="server" onchange="Total()" ReadOnly="false" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtULIPSurrMktVal" runat="server" Style="direction: rtl" Width="90px"></asp:TextBox></td></tr>
                              
                             <tr>
                                <td align="right">
                                    <asp:Label ID="lblOthersLI" runat="server" CssClass="FieldName" 
                                        Text="Others : "></asp:Label></td><td align="left">
                                    <asp:TextBox ID="txtWERPOthersLISA" runat="server" Enabled="false" Style="direction: rtl" 
                                                    Width="90px"></asp:TextBox></td><td align="left">
                                    <asp:TextBox ID="txtOthersLIP" runat="server" Enabled="false" onchange="Total()" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                        <asp:TextBox ID="txtAdjustedOthersLISA" runat="server" 
                                                    onchange="SubTotal('txtWERPOthersLISA','txtAdjustedOthersLISA','NULL','txtTotalOthersLISA')" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                        <asp:TextBox ID="txtAdjustedOthersLIPremium" runat="server" 
                                                    onchange="SubTotal('txtOthersLIP','txtAdjustedOthersLIPremium','NULL','txtTotalOthersPremium')" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                        
                                        <asp:TextBox ID="txtTotalOthersLISA" runat="server" onchange="Total()" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td><td align="left">
                                        <asp:TextBox ID="txtTotalOthersPremium" runat="server" onchange="Total()" 
                                                    ReadOnly="false" Style="direction: rtl" Width="90px"></asp:TextBox></td><td 
                                                align="left">
                                        <asp:TextBox ID="txtOtherSurrMktVal" runat="server" Style="direction: rtl" Width="90px"></asp:TextBox></td></tr>
                              
                              <tr><td></td></tr>
                                                               
                             <tr>
                                        <td></td><td></td>
                                <td align="right">
                                    </td><td align="left">
                                    </td><td align="right">
                               <asp:Label ID="lblTotalLI" runat="server" CssClass="FieldName" Text="Grand Total : "></asp:Label></td><td 
                                                align="left">
                               <asp:TextBox ID="txtTotalLISA" runat="server" Enabled="false" EnableViewState="true" 
                                                    Style="direction: rtl" Width="90px"></asp:TextBox></td>
                               <td></td><td></td>
                                                    
                              </tr>
                              </table>
                              </td>
                 </tr>
             </table>
            </asp:Panel>
  </telerik:RadPageView>
                              
   <telerik:RadPageView ID="RadPageView7" runat="server">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Panel ID="pnlGeneralInsurance" runat="server">
           <%-- <table width="100%">
                <tr>
                    <td>--%>
                <table width="100%">
                            <tr>
                                <td> &nbsp; </td>
                                <td colspan="2" align="center" style="border-bottom: 1px solid gray;">
                                    <asp:Label ID="lblGIPortFolio" runat="server" 
                                        CssClass="FieldName" Font-Size="11px" Font-Bold="true" Font-Underline="false" 
                                        Text="Portfolio"></asp:Label>
                               </td>
                                <td colspan="2" align="center" style="border-bottom: 1px solid gray;">
                                    <asp:Label ID="lblGIAdjustments" runat="server" Font-Size="11px" CssClass="FieldName" 
                                        Font-Bold="true" Font-Underline="False" Text="Adjustments"></asp:Label>
                                </td>
                                        
                                        
                                <td colspan="2" align="center" style="border-bottom: 1px solid gray;">
                                    <asp:Label ID="lblGITotalValues" runat="server" Font-Size="11px"
                                        CssClass="FieldName" Font-Bold="true" Font-Underline="false" Text="Total"></asp:Label>
                                </td>
                            
                            </tr>
                       
                            <tr>
                                <td align="right">
                                </td>
                                 <td align="center">
                                    <asp:Label ID="lblWERPSumAssuredGI" runat="server" CssClass="FieldName" 
                                         Text="Sum Assured"></asp:Label></td><td align="center">
                                    <asp:Label ID="lblPremiumGI" runat="server" CssClass="FieldName" 
                                        Text="Premium (Annual)"></asp:Label></td><td align="center">
                                    <asp:Label ID="lblAdjustedSumAssuredGI" runat="server" CssClass="FieldName" 
                                        Text="Sum Assured"></asp:Label></td><td align="center">
                                    <asp:Label ID="lblAdjustedSysPremium" runat="server" CssClass="FieldName" 
                                        Text="Premium (Annual)"></asp:Label></td><td align="center">
                                    <asp:Label ID="lblTotalSumAssuredGI" runat="server" CssClass="FieldName" 
                                        Text="Sum Assured"></asp:Label></td><td align="center">
                                    <asp:Label ID="lblTotalPremiumValue" runat="server" CssClass="FieldName" 
                                        Text="Premium (Annual)"></asp:Label></td></tr><tr>
                                <td align="right">
                                    <asp:Label ID="lblHealthInsuranceCover" runat="server" CssClass="FieldName" 
                                        Text="Health Insurance Cover :"></asp:Label></td><td align="left">
                                            <asp:TextBox ID="txtWERPHealthInsuranceCover" runat="server" 
                                        Enabled="false" Style="direction: rtl"></asp:TextBox></td><td 
                                    align="left">
                                            <asp:TextBox ID="txtHealthInsuranceCoverP" runat="server" 
                                        Enabled="false" onchange="Total()" Style="direction: rtl"></asp:TextBox></td><td 
                                    align="left">
                                                <asp:TextBox ID="txtHealthInsuranceCoverA" runat="server" 
                                        onchange="SubTotal('txtWERPHealthInsuranceCover','txtHealthInsuranceCoverA','NULL','txtHealthInsuranceCoverSA')" 
                                        Style="direction: rtl"></asp:TextBox></td><td align="left">
                                                <asp:TextBox ID="txtAdjustedHealthPremium" runat="server" 
                                        onchange="SubTotal('txtHealthInsuranceCoverP','txtAdjustedHealthPremium','NULL','txtToalHealthInsurancePremium')" 
                                        Style="direction: rtl"></asp:TextBox></td><td align="left">
                                                <asp:TextBox ID="txtHealthInsuranceCoverSA" runat="server" 
                                                 onchange="Total()" Style="direction: rtl"></asp:TextBox></td><td 
                                    align="left">
                                                <asp:TextBox ID="txtToalHealthInsurancePremium" runat="server" 
                                         onchange="Total()" Style="direction: rtl"></asp:TextBox></td></tr><tr>
                                <td align="right">
                                    <asp:Label ID="lblPropertyInsuranceCover" runat="server" CssClass="FieldName" 
                                        Text="Property Insurance Cover :"></asp:Label></td><td align="left">
                                            <asp:TextBox ID="txtWERPPropertyInsuranceCover" runat="server" 
                                        Enabled="false" Style="direction: rtl"></asp:TextBox></td><td 
                                    align="left">
                                            <asp:TextBox ID="txtPropertyInsuranceCoverP" runat="server" 
                                        Enabled="false" onchange="Total()" Style="direction: rtl"></asp:TextBox></td><td 
                                    align="left">
                                            <asp:TextBox ID="txtPropertyInsuranceCoverA" runat="server" 
                                        onchange="SubTotal('txtWERPPropertyInsuranceCover','txtPropertyInsuranceCoverA','NULL','txtPropertyInsuranceCoverSA')" 
                                        Style="direction: rtl"></asp:TextBox></td><td align="left">
                                            <asp:TextBox ID="txtAdjustedPropertyInsurancePremium" runat="server" 
                                        onchange="SubTotal('txtPropertyInsuranceCoverP','txtAdjustedPropertyInsurancePremium','NULL','txtTotalPropertyInsurancePremium')" 
                                        Style="direction: rtl"></asp:TextBox></td><td align="left">
                                            <asp:TextBox ID="txtPropertyInsuranceCoverSA" runat="server" 
                                                onchange="Total()" Style="direction: rtl"></asp:TextBox></td><td 
                                    align="left">
                                            <asp:TextBox ID="txtTotalPropertyInsurancePremium" runat="server" 
                                         onchange="Total()" Style="direction: rtl"></asp:TextBox></td></tr><tr>
                                <td align="right">
                                    <asp:Label ID="lblPersonalAccident" runat="server" CssClass="FieldName" 
                                        Text="Personal Accident :"></asp:Label></td><td align="left">
                                          <asp:TextBox ID="txtWERPPersonalAccident" runat="server" Enabled="false" 
                                        Style="direction: rtl"></asp:TextBox></td><td align="left">
                                        <asp:TextBox ID="txtPersonalAccidentP" runat="server" Enabled="false" 
                                        onchange="Total()" Style="direction: rtl"></asp:TextBox></td><td 
                                    align="left">
                                        <asp:TextBox ID="txtPersonalAccidentA" runat="server" 
                                        onchange="SubTotal('txtWERPPersonalAccident','txtPersonalAccidentA','NULL','txtPersonalAccidentSA')" 
                                        Style="direction: rtl"></asp:TextBox></td><td align="left">
                                        <asp:TextBox ID="txtPersonalAccidentPremium" runat="server" 
                                        onchange="SubTotal('txtPersonalAccidentP','txtPersonalAccidentPremium','NULL','txtTotalPersonalAccidentPremium')" 
                                        Style="direction: rtl"></asp:TextBox></td><td align="left">
                                        <asp:TextBox ID="txtPersonalAccidentSA" runat="server" 
                                        onchange="Total()" Style="direction: rtl"></asp:TextBox></td><td 
                                    align="left">
                                        <asp:TextBox ID="txtTotalPersonalAccidentPremium" runat="server" 
                                         onchange="Total()" Style="direction: rtl"></asp:TextBox></td></tr><tr>
                                <td align="right">
                                    <asp:Label ID="lblOthersGI" runat="server" CssClass="FieldName" 
                                        Text="Others : "></asp:Label></td><td align="left">
                                        <asp:TextBox ID="txtWERPOthersGI" runat="server" Enabled="false" 
                                        Style="direction: rtl"></asp:TextBox></td><td align="left">
                                        <asp:TextBox ID="txtOthersGIP" runat="server" Enabled="false" 
                                        onchange="Total()" Style="direction: rtl"></asp:TextBox></td><td 
                                    align="left">
                                        <asp:TextBox ID="txtOthersGIA" runat="server" 
                                        onchange="SubTotal('txtWERPOthersGI','txtOthersGIA','NULL','txtOthersGISA')" 
                                        Style="direction: rtl"></asp:TextBox></td><td align="left">
                                        <asp:TextBox ID="txtAdjustedOtherGIPremium" runat="server" 
                                        onchange="SubTotal('txtOthersGIP','txtAdjustedOtherGIPremium','NULL','txtTotalPremiumOthers')" 
                                        Style="direction: rtl"></asp:TextBox></td><td align="left">
                                        <asp:TextBox ID="txtOthersGISA" runat="server" 
                                        onchange="Total()" Style="direction: rtl"></asp:TextBox></td><td 
                                    align="left">
                                        <asp:TextBox ID="txtTotalPremiumOthers" runat="server" 
                                        onchange="Total()" Style="direction: rtl"></asp:TextBox></td></tr>
                                       
                                       <tr>
                                        <td></td><td></td> <td></td>
                                        <td align="right">
                                            </td>
                                           <td align="Right">
                                            <asp:Label ID="lblTotalGI" runat="server" CssClass="FieldName" 
                                        Text="Grand Total : "></asp:Label></td><td align="left">
                                          <asp:TextBox ID="txtTotalGISA" runat="server" Enabled="false" 
                                        EnableViewState="true" Style="direction: rtl"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                        </td>
                               <td></td>
                            </tr>
                        </table>
           <%--         </td>
                </tr>
            </table>--%>
        </asp:Panel>
&nbsp;&nbsp;&nbsp;</telerik:RadPageView></telerik:RadMultiPage><asp:Button ID="btnCustomerProspect" runat="server" OnClick="btnCustomerProspect_Click"
    ValidationGroup="btnCustProsp" Text="Save" Style="height: 26px" />
    </td>
    </tr>
    </table></asp:Panel>
<asp:HiddenField ID="totalIncome" runat="server" />
<asp:HiddenField ID="totalExpense" runat="server" />
<asp:HiddenField ID="totalLiabilities" runat="server" />
<asp:HiddenField ID="hdnIsActive" runat="server" />
<asp:HiddenField ID="hdnIsProspect" runat="server" />
<asp:HiddenField ID="hdnModalPopupId" runat="server" />
