<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerProspect.ascx.cs"
    Inherits="WealthERP.FP.CustomerProspect" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<script type="text/javascript">
    function Total() {    
        var assettotal = 0.0;
        var incometotal = 0.0;
        var expensetotal = 0.0;
        var liabilitiestotal = 0.0;
        var lifeInsuranceTotal = 0.0;
        var generalInsuranceTotal = 0.0;       
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
            assettotal +=parseFloat( document.getElementById("<%=txtMFDebt.ClientID%>").value);
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
        if (document.getElementById("<%=txtInsurance.ClientID%>").value != "") {
            expensetotal += parseFloat(document.getElementById("<%=txtInsurance.ClientID%>").value);
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
        if (document.getElementById("<%=txtTermSA.ClientID%>").value != "") {
            lifeInsuranceTotal += parseFloat(document.getElementById("<%=txtTermSA.ClientID%>").value);
        }
        if (document.getElementById("<%=txtEndowmentSA.ClientID%>").value != "") {
            lifeInsuranceTotal += parseFloat(document.getElementById("<%=txtEndowmentSA.ClientID%>").value);
        }
        if (document.getElementById("<%=txtWholeLifeSA.ClientID%>").value != "") {
            lifeInsuranceTotal += parseFloat(document.getElementById("<%=txtWholeLifeSA.ClientID%>").value);
        }
        if (document.getElementById("<%=txtMoneyBackSA.ClientID%>").value != "") {
            lifeInsuranceTotal += parseFloat(document.getElementById("<%=txtMoneyBackSA.ClientID%>").value);
        }
        if (document.getElementById("<%=txtULIPSA.ClientID%>").value != "") {
            lifeInsuranceTotal += parseFloat(document.getElementById("<%=txtULIPSA.ClientID%>").value);
        }
        if (document.getElementById("<%=txtOthersLISA.ClientID%>").value != "") {
            lifeInsuranceTotal += parseFloat(document.getElementById("<%=txtOthersLISA.ClientID%>").value);
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
        
        //Sectional Total
        document.getElementById("<%=txtAssetTotal.ClientID%>").value = assettotal.toString();
        document.getElementById("<%=txtIncomeTotal.ClientID%>").value = incometotal.toString();
        document.getElementById("<%=txtExpenseTotal.ClientID%>").value = expensetotal.toString();
        document.getElementById("<%=txtTotalLO.ClientID%>").value = liabilitiestotal.toString();
        document.getElementById("<%=txtTotalLISA.ClientID%>").value = lifeInsuranceTotal.toString();
        document.getElementById("<%=txtTotalGISA.ClientID%>").value = generalInsuranceTotal.toString();
        // Main Summary Page Totals
        document.getElementById("<%=txtAssets.ClientID%>").value = assettotal.toString();
        document.getElementById("<%=txtIncome.ClientID%>").value = incometotal.toString();
        document.getElementById("<%=txtExpense.ClientID%>").value = expensetotal.toString();
        document.getElementById("<%=txtLiabilities.ClientID%>").value = liabilitiestotal.toString();
        document.getElementById("<%=txtLifeInsurance.ClientID%>").value = lifeInsuranceTotal.toString();
        document.getElementById("<%=txtGeneralInsurance.ClientID%>").value = generalInsuranceTotal.toString();
        


    }
</script>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="All"
    Skin="Outlook" />
<asp:Label ID="headertitle" runat="server" CssClass="HeaderTextBig" Text="Finance Profile"></asp:Label>
<hr />
<telerik:RadToolBar ID="aplToolBar" runat="server" OnButtonClick="aplToolBar_ButtonClick"
    Skin="Outlook" EnableShadows="true" EnableRoundedCorners="true" Width="100%"
    Visible="false">
    <Items>    
        
        <telerik:RadToolBarButton runat="server" Text="Edit" Value="Edit" ImageUrl="~/Images/Telerik/EditButton.gif"
            ImagePosition="Left" ToolTip="Edit">
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
<telerik:RadTabStrip ID="RadTabStrip1" runat="server" EnableTheming="True" Skin="Outlook"
    MultiPageID="CustomerProspectMultiPage" SelectedIndex="1" Orientation="HorizontalTop" ReorderTabsOnSelect="false">
    <Tabs>
        <telerik:RadTab runat="server" ImageUrl="/Images/Telerik/FP/Summary.gif" Text="Summary"
            Value="Summary">
        </telerik:RadTab>
        <telerik:RadTab runat="server" ImageUrl="/Images/Telerik/FP/Investment.gif" Text="Investment"
            Value="Investment" Selected="True">
        </telerik:RadTab>
        <telerik:RadTab runat="server" ImageUrl="/Images/Telerik/FP/Income.gif" Text="Income"
            Value="Income">
        </telerik:RadTab>
        <telerik:RadTab runat="server" ImageUrl="/Images/Telerik/FP/Expense.gif" Text="Expense"
            Value="Expense">
        </telerik:RadTab>
        <telerik:RadTab runat="server" ImageUrl="/Images/Telerik/FP/Liabilities.gif" Text="Liabilities"
            Value="Liabilities">
        </telerik:RadTab>
        <telerik:RadTab runat="server" ImageUrl="/Images/Telerik/FP/LifeInsurance.gif" Text="Life Insurance"
            Value="LifeInsurance">
        </telerik:RadTab>
        <telerik:RadTab runat="server" ImageUrl="/Images/Telerik/FP/GeneralInsurance.gif"
            Text="General Insurance" Value="General Insurance">
        </telerik:RadTab>
    </Tabs>
</telerik:RadTabStrip>

<telerik:RadInputManager ID="RadInputManager1" runat="server" Skin="Outlook">
    <telerik:TextBoxSetting BehaviorID="TextBoxBehavior1" Validation-IsRequired="true">
        <TargetControls>
            <telerik:TargetInput ControlID="txtFirstName" />
            <telerik:TargetInput ControlID="txtEmail" />
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
    <telerik:NumericTextBoxSetting DecimalDigits="2" 
        DecimalSeparator="." ErrorMessage="Please type only numbers" 
         NegativePattern="-n" PositivePattern="n" Validation-IsRequired="false"
        AllowRounding="False" MaxValue="70368744177" MinValue="-70368744177" GroupSizes="3" 
        Type="Number" Culture="Hindi (India)" GroupSeparator="">
        <TargetControls>
            <telerik:TargetInput ControlID="txtDirectEquity" />
            <telerik:TargetInput ControlID="txtGold" />
            <telerik:TargetInput ControlID="txtMFEquity" />
            <telerik:TargetInput ControlID="txtCollectibles" />
            <telerik:TargetInput ControlID="txtMFDebt" />
            <telerik:TargetInput ControlID="txtCashAndSavings" />
            <telerik:TargetInput ControlID="txtMFHybridEquity" />
            <telerik:TargetInput ControlID="txtStructuredProduct" />
            <telerik:TargetInput ControlID="txtMFHybridDebt" />
            <telerik:TargetInput ControlID="txtCommodities" />
            <telerik:TargetInput ControlID="txtFixedIncome" />
            <telerik:TargetInput ControlID="txtPrivateEquity" />
            <telerik:TargetInput ControlID="txtGovtSavings" />
            <telerik:TargetInput ControlID="txtPMS" />
            <telerik:TargetInput ControlID="txtPensionGratuities" />
            <telerik:TargetInput ControlID="txtInvestmentsOthers" />
            <telerik:TargetInput ControlID="txtProperty" />
            <telerik:TargetInput ControlID="txtSalary" />
            <telerik:TargetInput ControlID="txtCapitalGains" />
            <telerik:TargetInput ControlID="txtRentalProperty" />
            <telerik:TargetInput ControlID="txtAgriculturalIncome" />
            <telerik:TargetInput ControlID="txtBusinessAndProfession" />
            <telerik:TargetInput ControlID="txtOthersIncome" />
            <telerik:TargetInput ControlID="txtFood" />
            <telerik:TargetInput ControlID="txtEntertainmentHolidays" />
            <telerik:TargetInput ControlID="txtRent" />
            <telerik:TargetInput ControlID="txtPersonalWear" />
            <telerik:TargetInput ControlID="txtUtilites" />
            <telerik:TargetInput ControlID="txtInsurance" />
            <telerik:TargetInput ControlID="txtHealthPersonalCare" />
            <telerik:TargetInput ControlID="txtDomesticHelp" />
            <telerik:TargetInput ControlID="txtConveyance" />
            <telerik:TargetInput ControlID="txtOthersExpense" />
            <telerik:TargetInput ControlID="txtHomeLoanLO" />
            <telerik:TargetInput ControlID="txtHomeLoanEMI" />
            <telerik:TargetInput ControlID="txtAutoLoanLO" />
            <telerik:TargetInput ControlID="txtAutoLoanEMI" />
            <telerik:TargetInput ControlID="txtPersonalLoanEMI" />
            <telerik:TargetInput ControlID="txtPersonalLoanLO" />
            <telerik:TargetInput ControlID="txtEducationLoanLO" />
            <telerik:TargetInput ControlID="txtEducationLoanEMI" />
            <telerik:TargetInput ControlID="txtOtherLoanLO" />
            <telerik:TargetInput ControlID="txtOtherLoanEMI" />
            <telerik:TargetInput ControlID="txtTermP" />
            <telerik:TargetInput ControlID="txtTermSA" />
            <telerik:TargetInput ControlID="txtEndowmentSA" />
            <telerik:TargetInput ControlID="txtEndowmentP" />
            <telerik:TargetInput ControlID="txtWholeLifeSA" />
            <telerik:TargetInput ControlID="txtWholeLifeP" />
            <telerik:TargetInput ControlID="txtMoneyBackSA" />
            <telerik:TargetInput ControlID="txtMoneyBackP" />
            <telerik:TargetInput ControlID="txtULIPP" />
            <telerik:TargetInput ControlID="txtULIPSA" />
            <telerik:TargetInput ControlID="txtOthersLISA" />
            <telerik:TargetInput ControlID="txtOthersLIP" />
            <telerik:TargetInput ControlID="txtHealthInsuranceCoverSA" />
            <telerik:TargetInput ControlID="txtHealthInsuranceCoverP" />
            <telerik:TargetInput ControlID="txtPropertyInsuranceCoverSA" />
            <telerik:TargetInput ControlID="txtPropertyInsuranceCoverP" />
            <telerik:TargetInput ControlID="txtPersonalAccidentSA" />
            <telerik:TargetInput ControlID="txtPersonalAccidentP" />
            <telerik:TargetInput ControlID="txtOthersGISA" />
            <telerik:TargetInput ControlID="txtOthersGIP" />
        </TargetControls>
    </telerik:NumericTextBoxSetting>
    <telerik:DateInputSetting  DateFormat="dd-MM-yyyy" DisplayDateFormat="dd-MM-yyyy"
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
    <telerik:NumericTextBoxSetting  DecimalDigits="0" DecimalSeparator="."
        GroupSeparator="," GroupSizes="3" PositivePattern="n" AllowRounding="false" ErrorMessage="Invalid Entry">
        <TargetControls>
            <telerik:TargetInput ControlID="txtHomeLoanT" />
            <telerik:TargetInput ControlID="txtAutoLoanT" />
            <telerik:TargetInput ControlID="txtPersonalLoanT" />
            <telerik:TargetInput ControlID="txtEducationLoanT" />
            <telerik:TargetInput ControlID="txtOtherLoanT" />
        </TargetControls>
    </telerik:NumericTextBoxSetting>
</telerik:RadInputManager>
<telerik:RadAjaxLoadingPanel ID="FamilyMemberDetailsLoading" runat="server" Skin="Outlook">
</telerik:RadAjaxLoadingPanel>
<telerik:RadInputManager ID="RadInputManager2" runat="server">
</telerik:RadInputManager>
<telerik:RadMultiPage ID="CustomerProspectMultiPage" runat="server" 
    SelectedIndex="1">
    <telerik:RadPageView ID="RadPageView1" runat="server">
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
                                    <span id="Span1" class="spnRequiredField">*</span>
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
                                    <telerik:RadDatePicker ID="dpDOB" runat="server" 
                                        ShowAnimation-Type="Fade" Skin="Outlook">
                                        <Calendar Skin="Outlook" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                            ViewSelectorText="x">
                                        </Calendar>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                        <DateInput DateFormat="M/d/yyyy" DisplayDateFormat="M/d/yyyy">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblEmailId" runat="server" CssClass="FieldName" Text="Email Id : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtEmail" runat="server" Text="" />
                                    <span id="Span2" class="spnRequiredField">*</span>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblPickBranch" runat="server" CssClass="FieldName" Text="Branch Name : "></asp:Label>
                                </td>
                                <td align="left">
                                    <telerik:RadComboBox ID="ddlPickBranch" runat="server" EmptyMessage="Pick a Branch here"
                                        ExpandAnimation-Type="Linear" ShowToggleImage="True" Skin="Outlook">
                                        <ExpandAnimation Type="InExpo" />
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlPickBranch"
                                        CssClass="validator" ErrorMessage="Please pick a branch" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" CssClass="HeaderText" Text="Family Member Details"></asp:Label>
                        <hr />
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <div id="msgNochildCustomer" runat="server" class="failure-msg" align="center" visible="false">
                                        There is no Child Customers for this Customer
                                        <br />
                                        (or)<br />
                                        This Customer itself might be a child Customer</div>
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td>
                                    <telerik:RadAjaxPanel ID="ChildCustomerGridPanel" runat="server" Width="100%" HorizontalAlign="NotSet"
                                        LoadingPanelID="FamilyMemberDetailsLoading" EnablePageHeadUpdate="False">
                                        <telerik:RadGrid ID="RadGrid1" runat="server" Width="96%" GridLines="None" AutoGenerateColumns="False"
                                            PageSize="13" AllowSorting="True" AllowPaging="True" OnNeedDataSource="RadGrid1_NeedDataSource"
                                            ShowStatusBar="True" OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
                                            Skin="Outlook" OnUpdateCommand="RadGrid1_UpdateCommand" OnItemDataBound="RadGrid1_ItemDataBound">
                                            <PagerStyle Mode="NextPrevAndNumeric" Position="Bottom" />
                                            <MasterTableView DataKeyNames="C_CustomerId" AllowMultiColumnSorting="True" Width="100%"
                                                CommandItemDisplay="Top" AutoGenerateColumns="false" EditMode="InPlace">
                                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                <Columns>
                                                    <telerik:GridEditCommandColumn UpdateText="Update" UniqueName="EditCommandColumn"
                                                        CancelText="Cancel" ButtonType="ImageButton">
                                                        <HeaderStyle Width="85px"></HeaderStyle>
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridDropDownColumn UniqueName="CustomerRelationship" HeaderText="Relationship"
                                                        DataField="CustomerRelationship" DataSourceID="SqlDataSourceCustomerRelation"
                                                        ColumnEditorID="GridDropDownColumnEditor1" ListTextField="XR_Relationship" ListValueField="XR_RelationshipCode"
                                                        DropDownControlType="RadComboBox" ReadOnly="false">
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
                                                        DataField="DOB" FooterText="DateTimeColumn footer" DataFormatString="{0:MM/dd/yyyy}"
                                                        EditDataFormatString="MMMM dd, yyyy">
                                                        <ItemStyle Width="120px" />
                                                    </telerik:GridDateTimeColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Email-Id" SortExpression="Email-Id" UniqueName="EmailId"
                                                        EditFormColumnIndex="1">
                                                        <HeaderStyle Width="80px" />
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblGridEmailId" Text='<%# Eval("EmailId")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtGridEmailId" Text='<%# Bind("EmailId") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <%--<telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                            ButtonType="ImageButton" />--%>
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
                                    <asp:TextBox ID="txtAssets" runat="server" Text="" Enabled="false" style="direction:rtl" />
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblLifeInsurance" runat="server" CssClass="FieldName" Text="Life Insurance : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLifeInsurance" runat="server" Text="" Enabled="false" style="direction:rtl" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblLiabilities" runat="server" CssClass="FieldName" Text="Liabilities(Out Standing) : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLiabilities" runat="server" Text="" Enabled="false" style="direction:rtl" />
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblGeneralInsurance" runat="server" CssClass="FieldName" Text="Genral Insurance : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtGeneralInsurance" runat="server" Text="" Enabled="false" style="direction:rtl" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblExpense" runat="server" CssClass="FieldName" Text="Expense(Per Month) : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtExpense" runat="server" Text="" Enabled="false" style="direction:rtl" />
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblIncome" runat="server" CssClass="FieldName" Text="Income(Per Month) : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtIncome" runat="server" Text="" Enabled="false" style="direction:rtl" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageView2" runat="server">
        <asp:Panel ID="pnlInvestment" runat="server">
            <table width="100%">
                <tr>
                    <td>
                        <table width="60%">
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblDirectEquity" runat="server" Text="Direct Equity : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtDirectEquity" runat="server" style="direction:rtl" onchange="Total()" ></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblGold" runat="server" Text="Gold : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtGold" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblMFEquity" runat="server" Text="MF-Equity : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtMFEquity" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblCollectibles" runat="server" Text="Collectibles : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtCollectibles" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblMFDebt" runat="server" Text="MF-Debt : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtMFDebt" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblCashAndSavings" runat="server" Text="Cash & Savings : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtCashAndSavings" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblMFHybridEquity" runat="server" Text="MF Hybrid - Equity : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtMFHybridEquity" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblStructuredProduct" runat="server" Text="Structured Product : "
                                        CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtStructuredProduct" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblMFHybridDebt" runat="server" Text="MF Hybrid - Debt : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtMFHybridDebt" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblCommodities" runat="server" Text="Commodities : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtCommodities" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblFixedIncome" runat="server" Text="Fixed Income : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFixedIncome" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblPrivateEquity" runat="server" Text="Private Equity : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPrivateEquity" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblGovtSavings" runat="server" Text="Govt Savings : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtGovtSavings" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblPMS" runat="server" Text="PMS : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPMS" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblPensionGratuities" runat="server" Text="Pension & Gratuities : "
                                        CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPensionGratuities" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblOthers" runat="server" Text="Others : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtInvestmentsOthers" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblProperty" runat="server" Text="Property : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtProperty" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                 <td align="right">                                    
                                </td>
                                <td align="left">                                    
                                    
                                </td>
                            </tr>
                             <tr>
                                <td align="right">
                                    <asp:Label ID="lblAssetTotal" runat="server" Text="Total : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAssetTotal" runat="server" style="direction:rtl" Enabled="false"></asp:TextBox>
                                </td>
                                 <td align="right">                                    
                                </td>
                                <td align="left">                                    
                                    
                                </td>
                            </tr>
                        </table>
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
                                <td align="right">
                                    <asp:Label ID="lblSalary" runat="server" Text="Salary : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSalary" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblCapitalGains" runat="server" Text="Capital Gains : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtCapitalGains" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblRentalProperty" runat="server" CssClass="FieldName" Text="Rental Property : "></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtRentalProperty" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblAgriculturalIncome" runat="server" Text="Agricultural Income : "
                                        CssClass="FieldName" ></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAgriculturalIncome" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblBusinessAndProfession" runat="server" Text="Business & Profession : "
                                        CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtBusinessAndProfession" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblOthersIncome" runat="server" Text="Others : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOthersIncome" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                            </tr>
                              <tr>
                                <td align="right">
                                    <asp:Label ID="lblIncomeTotal" runat="server" Text="Total : "
                                        CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtIncomeTotal" runat="server" style="direction:rtl" Enabled="false"></asp:TextBox>
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
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageView4" runat="server">
        <asp:Panel ID="pnlExpense" runat="server">
            <table width="100%">
                <tr>
                    <td>
                        <table width="60%">
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblFood" runat="server" Text="Food : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFood" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblEntertainmentHolidays" runat="server" Text="Entertainment-Holidays : "
                                        CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtEntertainmentHolidays" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblRent" runat="server" Text="Rent : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtRent" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblPersonalWear" runat="server" Text="Personal Wear : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPersonalWear" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblUtilites" runat="server" Text="Utilities : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtUtilites" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblInsurance" runat="server" Text="Insurance : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtInsurance" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblHealthPersonalCare" runat="server" Text="Health-Personal Care : "
                                        CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtHealthPersonalCare" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblDomesticHelp" runat="server" Text="Domestic Help : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtDomesticHelp" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblConveyance" runat="server" Text="Conveyance : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtConveyance" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblOthersExpense" runat="server" Text="Others : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOthersExpense" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                            </tr>
                              <tr>
                                <td align="right">
                                    <asp:Label ID="lblExpenseTotal" runat="server" Text="Total : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtExpenseTotal" runat="server" style="direction:rtl" Enabled="false"></asp:TextBox>
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
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageView5" runat="server">
        <asp:Panel ID="pnlLiabilities" runat="server">
            <table width="100%">
                <tr>
                    <td>
                        <table width="60%">
                            <tr>
                                <td>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lblLoanOutstanding" runat="server" Text="Loan Outstanding" CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lblTenure" runat="server" Text="Tenure(in months)" CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lblEMI" runat="server" Text="EMI" CssClass="FieldName"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblHomeLoan" runat="server" Text="Home Loan : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtHomeLoanLO" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtHomeLoanT" runat="server"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtHomeLoanEMI" runat="server" style="direction:rtl"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblAutoLoan" runat="server" Text="Auto Loan : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAutoLoanLO" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAutoLoanT" runat="server"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAutoLoanEMI" runat="server" style="direction:rtl"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblPersonalLoan" runat="server" Text="Perosnal Loan : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPersonalLoanLO" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPersonalLoanT" runat="server"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPersonalLoanEMI" runat="server" style="direction:rtl"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblEducationLoan" runat="server" Text="Education Loan : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtEducationLoanLO" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtEducationLoanT" runat="server"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtEducationLoanEMI" runat="server" style="direction:rtl"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblOtherLoan" runat="server" Text="Others : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOtherLoanLO" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOtherLoanT" runat="server"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOtherLoanEMI" runat="server" style="direction:rtl"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblTotal" runat="server" Text="Total : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtTotalLO" runat="server" style="direction:rtl" Enabled="false"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <%--<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>--%>
                                </td>
                                <td align="left">
                                    <%--<asp:TextBox ID="TextBox3" runat="server" style="direction:rtl"></asp:TextBox>--%>
                                </td>
                            </tr>
                            <%-- <tr>
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
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageView6" runat="server">
        <asp:Panel ID="pnlLifeInsurance" runat="server">
            <table width="100%">
                <tr>
                    <td>
                        <table width="60%">
                            <tr>
                                <td align="right">
                                </td>
                                <td align="center">
                                    <asp:Label ID="lblSumAssuredLI" runat="server" Text="Sum Assured" CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lblPremiumLI" runat="server" Text="Premium" CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lblMaturityDateLI" runat="server" Text="Maturity Date" CssClass="FieldName"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblTerm" runat="server" Text="Term : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtTermSA" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtTermP" runat="server" style="direction:rtl"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <telerik:RadDatePicker ID="dpTermLIMD" runat="server" 
                                        Skin="Outlook" ShowAnimation-Type="Fade">
                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                            Skin="Outlook">
                                        </Calendar>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                        <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblEndowment" runat="server" Text="Endowment : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtEndowmentSA" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtEndowmentP" runat="server" style="direction:rtl"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <telerik:RadDatePicker ID="dpEndowmentLIMD" runat="server" 
                                        Skin="Outlook" ShowAnimation-Type="Fade">
                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                            Skin="Outlook">
                                        </Calendar>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                        <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblWholeLife" runat="server" Text="Whole Life : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtWholeLifeSA" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtWholeLifeP" runat="server" style="direction:rtl"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <telerik:RadDatePicker ID="dpWholeLifeLIMD" runat="server" 
                                        Skin="Outlook" ShowAnimation-Type="Fade">
                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                            Skin="Outlook">
                                        </Calendar>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                        <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblMoneyBack" runat="server" Text="Money Back : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtMoneyBackSA" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtMoneyBackP" runat="server" style="direction:rtl"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <telerik:RadDatePicker ID="dpMoneyBackLIMD" runat="server" 
                                        Skin="Outlook" ShowAnimation-Type="Fade">
                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                            Skin="Outlook">
                                        </Calendar>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                        <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblULIP" runat="server" Text="ULIP : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtULIPSA" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtULIPP" runat="server" style="direction:rtl"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <telerik:RadDatePicker ID="dpULIPSLIMD" runat="server" 
                                        Skin="Outlook" ShowAnimation-Type="Fade">
                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                            Skin="Outlook">
                                        </Calendar>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                        <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblOthersLI" runat="server" Text="Others : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOthersLISA" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOthersLIP" runat="server" style="direction:rtl"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <telerik:RadDatePicker ID="dpOthersLIMD" runat="server" 
                                        Skin="Outlook" ShowAnimation-Type="Fade">
                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                            Skin="Outlook">
                                        </Calendar>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                        <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                          <tr>
                                            <td align="right">
                                                <asp:Label ID="lblTotalLI" runat="server" Text="Total : " CssClass="FieldName"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtTotalLISA" runat="server" style="direction:rtl" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                            </td>
                                        </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageView7" runat="server">
        <asp:Panel ID="pnlGeneralInsurance" runat="server">
            <table width="100%">
                <tr>
                    <td>
                        <table width="60%">
                            <tr>
                                <td align="right">
                                </td>
                                <td align="center">
                                    <asp:Label ID="lblSumAssuredGI" runat="server" Text="Sum Assured" CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lblPremiumGI" runat="server" Text="Premium" CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lblMaturityDateGI" runat="server" Text="Maturity Date" CssClass="FieldName"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblHealthInsuranceCover" runat="server" Text="Health Insurance Cover : "
                                        CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtHealthInsuranceCoverSA" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtHealthInsuranceCoverP" runat="server" style="direction:rtl"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <telerik:RadDatePicker ID="dpHealthInsuranceCoverGIMD" runat="server" 
                                        Skin="Outlook" ShowAnimation-Type="Fade">
                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                            Skin="Outlook">
                                        </Calendar>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                        <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblPropertyInsuranceCover" runat="server" Text="Property Insurance Cover : "
                                        CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPropertyInsuranceCoverSA" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPropertyInsuranceCoverP" runat="server" style="direction:rtl"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <telerik:RadDatePicker ID="dpPropertyInsuranceCoverGIMD" runat="server" 
                                        Skin="Outlook" ShowAnimation-Type="Fade">
                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                            Skin="Outlook">
                                        </Calendar>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                        <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblPersonalAccident" runat="server" Text="Personal Accident : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPersonalAccidentSA" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPersonalAccidentP" runat="server" style="direction:rtl"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <telerik:RadDatePicker ID="dpPersonalAccidentGIMD" runat="server" 
                                        Skin="Outlook" ShowAnimation-Type="Fade">
                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                            Skin="Outlook">
                                        </Calendar>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                        <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblOthersGI" runat="server" Text="Others : " CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOthersGISA" runat="server" style="direction:rtl" onchange="Total()"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOthersGIP" runat="server" style="direction:rtl"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <telerik:RadDatePicker ID="dpOthersGIMD" runat="server" 
                                        Skin="Outlook" ShowAnimation-Type="Fade">
                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                            Skin="Outlook">
                                        </Calendar>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                        <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                              <tr>
                                            <td align="right">
                                                <asp:Label ID="lblTotalGI" runat="server" Text="Total : " CssClass="FieldName"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtTotalGISA" runat="server" style="direction:rtl" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                            </td>
                                        </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
</telerik:RadMultiPage>
<asp:Button ID="btnCustomerProspect" runat="server" OnClick="btnCustomerProspect_Click"
    Text="Save" Style="height: 26px" />
