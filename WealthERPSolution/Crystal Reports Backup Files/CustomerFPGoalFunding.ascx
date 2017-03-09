<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerFPGoalFunding.ascx.cs" Inherits="WealthERP.FP.CustomerFPGoalFunding" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>

<script type="text/javascript">
    function EnableDisableLoanAmountYear() {

        if (document.getElementById("<%=chkGoalFundByLoan.ClientID %>").checked == true) {
            document.getElementById('<%=txtStartLoanYr.ClientID %>').disabled = false;
            document.getElementById('<%=txtLoanAmountFunding.ClientID %>').disabled = false;

             
               
            }
            
        
        else {
            document.getElementById('<%=txtStartLoanYr.ClientID %>').disabled = true;
            document.getElementById('<%=txtLoanAmountFunding.ClientID %>').disabled = true;
            document.getElementById('<%=txtLoanAmountFunding.ClientID %>').value = "";
            document.getElementById('<%=txtStartLoanYr.ClientID %>').value = "";
        }
        }
</script>

<script type="text/javascript">
    function Validations() {
        if (document.getElementById("<%=chkGoalFundByLoan.ClientID %>").checked == true) {
            var loanYear = document.getElementById('<%=txtStartLoanYr.ClientID %>').value;
            var loanAmount = document.getElementById('<%=txtLoanAmountFunding.ClientID %>').value;
            if (loanYear == "") {
                alert("Please Enter Loan Year");
                return false;
            }
            if (loanYear == "dd/mm/yyyy") {
                alert("Please Enter Loan Year");
                return false;
            }
            if (loanAmount == 0) {
                alert("Please Enter Loan Amount");
                return false;
            }
            if (loanAmount == "") {
                alert("Please Enter Loan Amount");
                return false;
            }
        }
        var total = document.getElementById('<%=Label1.ClientID %>').innerHTML;

        var GoalAmount = document.getElementById('<%=txtGoalAmountReq.ClientID %>').value;
        if (parseFloat(total) > parseFloat(GoalAmount) ){
            alert("Total allocation amount can't be more than tha goal amount"); 
            return false;
        } 
    }
</script>


<script type="text/javascript">

    function roundNumberEquity(num, dec) {
        var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
        return result;
    }

    function EquityValidation() {
        var equityAvlAmount = 0;
        var count = document.getElementById("<%=hdnAlternate.ClientID %>").value;
        equityAvlAmount=document.getElementById("<%=txtEquityAvlCorps.ClientID %>").value;
        var equityAmountAllAmount = 0;
        equityAmountAllAmount = document.getElementById("<%=txtEquityAllAmt.ClientID %>").value;
        if (parseFloat(equityAmountAllAmount) != 0) {
            if (parseFloat(equityAmountAllAmount) > parseFloat(equityAvlAmount)) {
                //            alert(equityAmountAllAmount);
                //            alert(equityAvlAmount);
                alert("Can't be greater value");
                document.getElementById("<%=txtEquityAllPer.ClientID %>").value = "";
                document.getElementById("<%=txtEquityRemainCorpus.ClientID %>").value = "";
                document.getElementById("<%=txtEquityAllAmt.ClientID %>").value = "";
            }

            else {

                var equityAllPercent = equityAmountAllAmount * 100 / equityAvlAmount;
                var equityRemaingCorps = equityAvlAmount - equityAmountAllAmount;
                document.getElementById("<%=txtEquityAllPer.ClientID %>").value = roundNumberEquity(equityAllPercent, 2);
                document.getElementById("<%=txtEquityRemainCorpus.ClientID %>").value = equityRemaingCorps;
            } 
        }
        var equity = document.getElementById('<%=txtEquityAllAmt.ClientID %>').value;

        var debt = document.getElementById('<%=txtDebtAllAmt.ClientID %>').value;


        var cash = document.getElementById('<%=txtCashAllAmt.ClientID %>').value;
        var alternate = "";
        if (count == 4)
            var alternate = document.getElementById('<%=txtAlternateAllAmt.ClientID %>').value;
      
  
        var sum = 0; var sum1 = 0; var sum2 = 0; var sum3 = 0; var sum4 = 0;
        if (equity == "") {
            sum1 = 0;
        }
        else {
            sum1 = sum1 + parseFloat(equity);
        }
   
        if (debt == "") {
            sum2 = 0;
        }
        else {
            sum2 = sum2 + parseFloat(debt);
        }
  
        if (cash == "") {
            sum3 = 0;
        }
        else {
            sum3 = sum3 + parseFloat(cash);
        }
    
        if (alternate == "") {
            sum4 = 0;
        }
        else {
            sum4 = sum4 + parseFloat(alternate);
        }
      
        sum = sum1 + sum2 + sum3 + sum4;


        document.getElementById('<%=Label1.ClientID %>').innerHTML = sum;
        var total = document.getElementById('<%=Label1.ClientID %>').innerHTML;

        var GoalAmount = document.getElementById('<%=txtGoalAmountReq.ClientID %>').value;
     
        var Gap = GoalAmount - total;
        document.getElementById('<%=txtGapAfterAllocation.ClientID %>').value = Gap;
       
        
    }
</script>
<script type="text/javascript">

    function roundNumberDebt(num, dec) {
        var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
        return result;
    }

    function DebtValidation() {
    
        var debtAvlAmount = 0;
        debtAvlAmount = document.getElementById("<%=txtDebtAvlCorps.ClientID %>").value;
        var count = document.getElementById("<%=hdnAlternate.ClientID %>").value;
        var debtAmountAllAmount = 0;
         debtAmountAllAmount = document.getElementById("<%=txtDebtAllAmt.ClientID %>").value;
         if (parseFloat(debtAmountAllAmount) != 0) {
             if (parseFloat(debtAmountAllAmount) > parseFloat(debtAvlAmount)) {
                 //            alert(debtAmountAllAmount);
                 //            alert(debtAvlAmount);
                 alert("Can't be greater value");
                 document.getElementById("<%=txtDebtAllPer.ClientID %>").value = "";
                 document.getElementById("<%=txtDebtRemainCorpus.ClientID %>").value = "";
                 document.getElementById("<%=txtDebtAllAmt.ClientID %>").value = "";
             }

             else {

                 var debtAllPercent = debtAmountAllAmount * 100 / debtAvlAmount;
                 var debtRemaingCorps = debtAvlAmount - debtAmountAllAmount;
                 document.getElementById("<%=txtDebtAllPer.ClientID %>").value = roundNumberDebt(debtAllPercent, 2);
                 document.getElementById("<%=txtDebtRemainCorpus.ClientID %>").value = debtRemaingCorps;
             } 
         }
        var equity = document.getElementById('<%=txtEquityAllAmt.ClientID %>').value;
   
        var debt = document.getElementById('<%=txtDebtAllAmt.ClientID %>').value;


        var cash = document.getElementById('<%=txtCashAllAmt.ClientID %>').value;
        var alternate = "";
        if (count == 4)
            var alternate = document.getElementById('<%=txtAlternateAllAmt.ClientID %>').value;
    
       
        var sum = 0; var sum1 = 0; var sum2 = 0; var sum3 = 0; var sum4 = 0;
        if (equity == "") {
            sum1 = 0;
        }
        else {
            sum1 = sum1 + parseFloat(equity);
        }
  
        if (debt == "") {
            sum2 = 0;
        }
        else {
            sum2 = sum2 + parseFloat(debt);
        }
    
        if (cash == "") {
            sum3 = 0;
        }
        else {
            sum3 = sum3 + parseFloat(cash);
        }
    
        if (alternate == "") {
            sum4 = 0;
        }
        else {
            sum4 = sum4 + parseFloat(alternate);
        }
   
        sum = sum1 + sum2 + sum3 + sum4;


        document.getElementById('<%=Label1.ClientID %>').innerHTML = sum;
        var total = document.getElementById('<%=Label1.ClientID %>').innerHTML;

        var GoalAmount = document.getElementById('<%=txtGoalAmountReq.ClientID %>').value;

        var Gap = GoalAmount - total;
        document.getElementById('<%=txtGapAfterAllocation.ClientID %>').value = Gap;
    }
</script>
<script type="text/javascript">

    function roundNumberCash(num, dec) {
        var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
        return result;
    }

    function CashValidation() {
        var cashAvlAmount = 0;
        var count = document.getElementById("<%=hdnAlternate.ClientID %>").value;
        cashAvlAmount = document.getElementById("<%=txtCashAvlCorps.ClientID %>").value;
        var cashAmountAllAmount = 0;
        cashAmountAllAmount = document.getElementById("<%=txtCashAllAmt.ClientID %>").value;
        if (parseFloat(cashAmountAllAmount) != 0) {
            if (parseFloat(cashAmountAllAmount) > parseFloat(cashAvlAmount)) {
                //            alert(cashAmountAllAmount);
                //            alert(cashAvlAmount);
                alert("Can't be greater value");
                document.getElementById("<%=txtCashAllPer.ClientID %>").value = "";
                document.getElementById("<%=txtCashRemainCorpus.ClientID %>").value = "";
                document.getElementById("<%=txtCashAllAmt.ClientID %>").value = "";
            }

            else {

                var cashAllPercent = cashAmountAllAmount * 100 / cashAvlAmount;
                var cashRemaingCorps = cashAvlAmount - cashAmountAllAmount;
                document.getElementById("<%=txtCashAllPer.ClientID %>").value = roundNumberCash(cashAllPercent, 2);
                document.getElementById("<%=txtCashRemainCorpus.ClientID %>").value = cashRemaingCorps;
            } 
        }
        var equity = document.getElementById('<%=txtEquityAllAmt.ClientID %>').value;
   
        var debt = document.getElementById('<%=txtDebtAllAmt.ClientID %>').value;


        var cash = document.getElementById('<%=txtCashAllAmt.ClientID %>').value;
        var alternate = "";
        if (count == 4)
          alternate = document.getElementById('<%=txtAlternateAllAmt.ClientID %>').value;
      
      
      var sum = 0; var sum1 = 0; var sum2 = 0; var sum3 = 0;var sum4 = 0;
      if (equity == "") {
          sum1 = 0;
      }
      else {
          sum1 = sum1 + parseFloat(equity);
      }
    
      if (debt == "") {
          sum2 = 0;
      }
      else {
          sum2 = sum2 + parseFloat(debt);
      }
    
      if (cash == "") {
          sum3 = 0;
      }
      else {
          sum3 = sum3 + parseFloat(cash);
      }
    
      if (alternate == "") {
          sum4 = 0;
      }
      else {
          sum4 = sum4 + parseFloat(alternate);
      }
  
      sum = sum1 + sum2 + sum3 + sum4;


      document.getElementById('<%=Label1.ClientID %>').innerHTML = sum;
      var total = document.getElementById('<%=Label1.ClientID %>').innerHTML;

      var GoalAmount = document.getElementById('<%=txtGoalAmountReq.ClientID %>').value;

      var Gap = GoalAmount - total;
      document.getElementById('<%=txtGapAfterAllocation.ClientID %>').value = Gap;
    //  $(Label1).html(sum);

//     document.getElementById('<%=Label1.ClientID %>').text=sum;

    }
</script>
<script type="text/javascript">

    function roundNumberAlternate(num, dec) {
        var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
        return result;
    }

    function AlternateValidation() {

        var AlternateAvlAmount = 0;
        var count = document.getElementById("<%=hdnAlternate.ClientID %>").value;
        AlternateAvlAmount = document.getElementById("<%=txtAlternateAvlCorps.ClientID %>").value;
        var AlternateAmountAllAmount = 0;
        AlternateAmountAllAmount = document.getElementById("<%=txtAlternateAllAmt.ClientID %>").value;
        if (parseFloat(AlternateAmountAllAmount) != 0) {
            if (parseFloat(AlternateAmountAllAmount) > parseFloat(AlternateAvlAmount)) {
                //            alert(AlternateAmountAllAmount);
                //            alert(AlternateAvlAmount);
                alert("Can't be greater value");
                document.getElementById("<%=txtAlternateAllPer.ClientID %>").value = "";
                document.getElementById("<%=txtAlternateRemainCorpus.ClientID %>").value = "";
                document.getElementById("<%=txtAlternateAllAmt.ClientID %>").value = "";
            }

            else {

                var AlternateAllPercent = AlternateAmountAllAmount * 100 / AlternateAvlAmount;
                var AlternateRemaingCorps = AlternateAvlAmount - AlternateAmountAllAmount;
                document.getElementById("<%=txtAlternateAllPer.ClientID %>").value = roundNumberAlternate(AlternateAllPercent, 2);
                document.getElementById("<%=txtAlternateRemainCorpus.ClientID %>").value = AlternateRemaingCorps;
            }
        }
        var equity = document.getElementById('<%=txtEquityAllAmt.ClientID %>').value;
   
        var debt = document.getElementById('<%=txtDebtAllAmt.ClientID %>').value;


        var cash = document.getElementById('<%=txtCashAllAmt.ClientID %>').value;
        var alternate = "";
        if (count == 4)
                  alternate = document.getElementById('<%=txtAlternateAllAmt.ClientID %>').value;
 
  
        var sum = 0; var sum1 = 0; var sum2 = 0; var sum3 = 0; var sum4 = 0;
        if (equity == "") {
            sum1 = 0;
        }
        else {
            sum1 = sum1 + parseFloat(equity);
        }
      
        if (debt == "") {
            sum2 = 0;
        }
        else {
            sum2 = sum2 + parseFloat(debt);
        }
      
        if (cash == "") {
            sum3 = 0;
        }
        else {
            sum3 = sum3 + parseFloat(cash);
        }
    
        if (alternate == "") {
            sum4 = 0;
        }
        else {
            sum4 = sum4 + parseFloat(alternate);
        }
     
        sum = sum1 + sum2 + sum3 + sum4;


        document.getElementById('<%=Label1.ClientID %>').innerHTML = sum;

        var total = document.getElementById('<%=Label1.ClientID %>').innerHTML;

        var GoalAmount = document.getElementById('<%=txtGoalAmountReq.ClientID %>').value;

        var Gap = GoalAmount - total;
        document.getElementById('<%=txtGapAfterAllocation.ClientID %>').value = Gap;
    }
</script>

<asp:Label ID="headertitle" runat="server" CssClass="HeaderTextBig" Text="Goal Funding"></asp:Label>
<hr />
<telerik:RadToolBar ID="aplToolBar" runat="server" Skin="Telerik" EnableEmbeddedSkins="false" EnableShadows="true" EnableRoundedCorners="true"
    Width="100%"  OnButtonClick="aplToolBar_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ID="btnEdit" runat="server" Text="Edit" Value="Edit" ImageUrl="~/Images/Telerik/EditButton.gif"
            ImagePosition="Left" ToolTip="Edit">            
        </telerik:RadToolBarButton>
        
         <telerik:RadToolBarButton ID="btnView" runat="server" Text="View" Value="View" ImageUrl="~/Images/Telerik/EditButton.gif"
            ImagePosition="Left" ToolTip="VIew">            
        </telerik:RadToolBarButton>
        
    </Items>
</telerik:RadToolBar>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgRecordStatus" runat="server" class="success-msg" align="center" visible="false">
                Record saved Successfully
            </div>
        </td>
    </tr>
</table>
<table class="TableBackground">
<tr>
    <td></td>
    <td></td>
</tr><tr runat="server" id="trGoalName">
    <td class="leftField">
        <asp:Label ID="lblGoal" runat="server" CssClass="FieldName" Text="Goal:"></asp:Label>
    </td> 
    <td>
        <asp:Label ID="lblGoalName" runat="server" CssClass="FieldName"></asp:Label>
    </td>
</tr>
<tr runat="server" id="trSelectGoal">
    <td class="leftField">
        <asp:Label ID="lblPickGoal" runat="server" CssClass="FieldName" Text="Goal:"></asp:Label>
    </td> 
    <td>
        <asp:DropDownList ID="ddlPickGoal" CssClass="cmbField" runat="server" 
            onselectedindexchanged="ddlPickGoal_SelectedIndexChanged" AutoPostBack="true">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td class="leftField">
    <asp:Label ID="lblGoalAmount" runat="server" CssClass="FieldName" Text="Amount needed in"></asp:Label>
         <asp:Label ID="lblGoalYear" runat="server" CssClass="FieldName"></asp:Label>
    </td>
    <td>
         
        <asp:TextBox ID="txtGoalAmountReq" CssClass="txtField"  runat="server" 
             Style="direction: rtl" ReadOnly="True"></asp:TextBox>
        </td>
</tr>
<tr>
    <td class="leftField">
    <asp:Label ID="lblAmountFunded" runat="server" CssClass="FieldName" Text="Amount Funded:"></asp:Label>
    </td>
    <td>
         
        <asp:TextBox ID="txtAmountFunded" CssClass="txtField" runat="server" 
             Style="direction: rtl" ReadOnly="True"></asp:TextBox>
        </td>
</tr>
<tr>
    <td class="leftField">
    <asp:Label ID="lblAmountRemaining" runat="server" CssClass="FieldName" Text="Amount remaining:"></asp:Label>
        
    </td>
    <td>
         
        <asp:TextBox ID="txtAmountRemaining" CssClass="txtField" runat="server" 
             Style="direction: rtl" ReadOnly="True"></asp:TextBox>
        </td>
</tr>


<tr>
    <td class="leftField">
        <asp:Label ID="lblGap" runat="server" CssClass="FieldName" Text="Gap After Allocation:"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtGapAfterAllocation" CssClass="txtField" ReadOnly="true" runat="server"  Style="direction: rtl"></asp:TextBox>
    </td>
</tr>
<tr>
<td></td>
<td></td>
</tr>
<tr>
    <td class="leftField">
    <asp:Label ID="lblMoney" runat="server" CssClass="FieldName" Text="You Have Enough Money:"></asp:Label>
    </td>
    <td>
        <asp:Label ID="lblYesNo" runat="server" CssClass="FieldName"></asp:Label>
        </td></br>
</tr>
<tr><td></td>
<td></td></tr>
<tr><td></td>
<td></td></tr>
<tr><td></td>
<td></td></tr>
<tr><td></td>
<td></td></tr>
<tr>
    <td></td>
    <td align="center" runat="server">
    <asp:Label ID="lblAvailableCorpus" runat="server" CssClass="FieldName" Text="Available Funds in "></asp:Label>
    </td>
    <td align="center" runat="server">
        <asp:Label ID="lblAllocatedAmount" runat="server" CssClass="FieldName" Text="Funds marked for Goal"></asp:Label>
    </td>
    <td align="center" runat="server">
        <asp:Label ID="lblAllocation" runat="server" CssClass="FieldName" Text="Allocation(%)"></asp:Label>
    </td>
    <td align="center" runat="server" >
        <asp:Label ID="lblAfterAllocation" runat="server" CssClass="FieldName" Text="Funds left after allocation"></asp:Label>
    </td>
</tr>
<tr>
    <td align="right" runat="server">
        <asp:Label ID="lblEquity" runat="server" CssClass="FieldName" Text="Equity:"></asp:Label>
    </td>
     <td>
        <asp:TextBox ID="txtEquityAvlCorps" CssClass="txtField" runat="server" Style="direction: rtl" ReadOnly="True"></asp:TextBox>
    </td>
     <td>
        <asp:TextBox ID="txtEquityAllAmt" CssClass="txtField" OnBlur="return EquityValidation()"  runat="server" Style="direction: rtl"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" CssClass="cvPCG" ControlToValidate="txtEquityAllAmt"
                                  Display="Dynamic"  ErrorMessage="Please Enter Numeric Value" ValidationExpression="\d+\.?\d*"></asp:RegularExpressionValidator>
    </td>
     <td>
        <asp:TextBox ID="txtEquityAllPer" CssClass="txtField"  runat="server" ReadOnly="True" Style="direction: rtl"></asp:TextBox>
    </td>
     <td>
        <asp:TextBox ID="txtEquityRemainCorpus" CssClass="txtField"  runat="server" ReadOnly="True" Style="direction: rtl"></asp:TextBox>
    </td>
</tr>
<tr>
    <td align="right" runat="server">
        <asp:Label ID="lblDebt" runat="server" CssClass="FieldName" Text="Debt:"></asp:Label>
    </td>
     <td>
        <asp:TextBox ID="txtDebtAvlCorps" CssClass="txtField" runat="server" ReadOnly="True" Style="direction: rtl"></asp:TextBox>
    </td>
     <td>
        <asp:TextBox ID="txtDebtAllAmt" CssClass="txtField" OnBlur="return DebtValidation()" runat="server" Style="direction: rtl"></asp:TextBox>
   <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="cvPCG" ControlToValidate="txtDebtAllAmt"
                                  Display="Dynamic"  ErrorMessage="Please Enter Numeric Value" ValidationExpression="\d+\.?\d*"></asp:RegularExpressionValidator>
   
    </td>
     <td>
        <asp:TextBox ID="txtDebtAllPer" CssClass="txtField"  runat="server" ReadOnly="True" Style="direction: rtl"></asp:TextBox>
    </td>
     <td>
        <asp:TextBox ID="txtDebtRemainCorpus" CssClass="txtField"  runat="server" ReadOnly="True" Style="direction: rtl"></asp:TextBox>
    </td>
</tr>
<tr>
    <td align="right" runat="server">
        <asp:Label ID="lblCash" runat="server" CssClass="FieldName" Text="Cash:"></asp:Label>
    </td>
     <td>
        <asp:TextBox ID="txtCashAvlCorps" CssClass="txtField"  runat="server" ReadOnly="True" Style="direction: rtl"></asp:TextBox>
    </td>
     <td>
        <asp:TextBox ID="txtCashAllAmt" CssClass="txtField" OnBlur="return CashValidation()"  runat="server" Style="direction: rtl"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" CssClass="cvPCG" ControlToValidate="txtCashAllAmt"
                                  Display="Dynamic"  ErrorMessage="Please Enter Numeric Value" ValidationExpression="\d+\.?\d*"></asp:RegularExpressionValidator>
   
    </td>
     <td>
        <asp:TextBox ID="txtCashAllPer" CssClass="txtField"  runat="server" ReadOnly="True" Style="direction: rtl"></asp:TextBox>
    </td>
     <td>
        <asp:TextBox ID="txtCashRemainCorpus" CssClass="txtField" runat="server" ReadOnly="True" Style="direction: rtl"></asp:TextBox>
    </td>
</tr>
<tr id="trAlternate" runat="server" >
    <td align="right" runat="server">
        <asp:Label ID="lblAlternate" runat="server" CssClass="FieldName" Text="Alternate:"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtAlternateAvlCorps" CssClass="txtField"  runat="server" ReadOnly="True" Style="direction: rtl"></asp:TextBox>
    </td>
     <td>
        <asp:TextBox ID="txtAlternateAllAmt" CssClass="txtField"  OnBlur="return AlternateValidation()" runat="server" Style="direction: rtl"></asp:TextBox>
   <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" CssClass="cvPCG" ControlToValidate="txtAlternateAllAmt"
                                  Display="Dynamic"  ErrorMessage="Please Enter Numeric Value" ValidationExpression="\d+\.?\d*"></asp:RegularExpressionValidator>
   
    </td>
     <td>
        <asp:TextBox ID="txtAlternateAllPer" CssClass="txtField" runat="server" ReadOnly="True" Style="direction: rtl"></asp:TextBox>
    </td>
     <td>
        <asp:TextBox ID="txtAlternateRemainCorpus" CssClass="txtField" runat="server" ReadOnly="True" Style="direction: rtl"></asp:TextBox>
    </td>
</tr>
<tr>
    <td align="right" runat="server"><asp:Label ID="lblTotalSum" runat="server" CssClass="FieldName" Text="Total:"></asp:Label></td>
    <td align="right" runat="server">
        <asp:Label ID="lblTotal" runat="server" CssClass="FieldName"></asp:Label>
    </td>
    <td align="right" runat="server">
    <%-- <asp:TextBox ID="TextBox1" CssClass="txtField" Width="95%" runat="server" ReadOnly="True" Style="direction: rtl"></asp:TextBox>--%>
     <asp:Label ID="Label1" runat="server" CssClass="FieldName"></asp:Label>
    </td>
    <td></td>
    <td></td>
</tr>

<tr>
    <td></td>
    <td>&nbsp;</td>
</tr>



<tr>
    <td align="right"><asp:Label ID="lblCheckGoalFundByLoan" runat="server" CssClass="FieldName" Text="Goal to be Funded by Loan:"></asp:Label>
       </td>
    <td>
    <asp:Checkbox ID="chkGoalFundByLoan" runat="server" CssClass="FieldName" onClick="return EnableDisableLoanAmountYear()"
                AutoPostBack="false"  />
    </td>
</tr>
<tr>
    <td>
        &nbsp;</td>
    <td>
        &nbsp;</td>
</tr>
<tr>
    <td class="leftField">
        <asp:Label ID="lblLoanAmount" runat="server" CssClass="FieldName" Text="Loan Amount to be Taken<br/>for goal Funding:"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtLoanAmountFunding" runat="server" CssClass="txtField"  Style="direction: rtl"></asp:TextBox>
         <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" CssClass="cvPCG" ControlToValidate="txtLoanAmountFunding"
                                  Display="Dynamic"  ErrorMessage="Please Enter Numeric Value" ValidationExpression="\d+\.?\d*"></asp:RegularExpressionValidator>
   
        </td>
    <td class="leftField">
        <asp:Label ID="lblStartYear" runat="server" CssClass="FieldName" Text="Start Loan Year:"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtStartLoanYr" runat="server" CssClass="txtField"></asp:TextBox>
        <cc1:CalendarExtender ID="txtStartLoanYr_CalendarExtender" runat="server" TargetControlID="txtStartLoanYr"
                                        Format="dd/MM/yyyy" Enabled="True">
                                    </cc1:CalendarExtender>
                                    <cc1:TextBoxWatermarkExtender ID="txtStartLoanYr_TextBoxWatermarkExtender" runat="server"
                                        TargetControlID="txtStartLoanYr" WatermarkText="dd/mm/yyyy" Enabled="True">
                                    </cc1:TextBoxWatermarkExtender> 
                                    <br />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" Display="Dynamic" runat="server" CssClass="cvPCG" ErrorMessage="Please Enter Date"
                                        ControlToValidate="txtStartLoanYr" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$"></asp:RegularExpressionValidator>
 <%--<asp:RequiredFieldValidator ID="rfvSubject" CssClass="cvPCG" ControlToValidate="txtStartLoanYr" Display="Dynamic" ErrorMessage="You must enter a subject." ValidationGroup="vgnSubmit" runat="server" />--%>

    </td>
</tr>
<tr>
    <td></td>
    <td></td>
</tr>
<tr>
    <td></td>
    <td></td>
</tr>
<tr>
    <td></td>
    <td>
        <asp:Button ID="btnSubmit" CssClass="PCGButton" runat="server" OnClientClick="return Validations()" Text="Submit" OnClick="btnSubmit_OnClick" />
    </td>
</tr>
</table>
<asp:HiddenField ID="hdnAlternate" runat="server" />
<script language="javascript" type="text/javascript">
    var count = document.getElementById("<%=hdnAlternate.ClientID %>").value;
    var equity = document.getElementById('<%=txtEquityAvlCorps.ClientID %>').value;

    var debt = document.getElementById('<%=txtDebtAvlCorps.ClientID %>').value;


    var cash = document.getElementById('<%=txtCashAvlCorps.ClientID %>').value;
    var alternate = "";
if(count==4)
    alternate  = document.getElementById('<%=txtAlternateAvlCorps.ClientID %>').value;

    var sum = 0; var sum1 = 0; var sum2 = 0; var sum3 = 0; var sum4 = 0;
    if (equity == "") {
        sum1 = 0;
    }
    else {
        sum1 = sum1 + parseFloat(equity);
    }

    if (debt == "") {
        sum2 = 0;
    }
    else {
        sum2 = sum2 + parseFloat(debt);
    }

    if (cash == "") {
        sum3 = 0;
    }
    else {
        sum3 = sum3 + parseFloat(cash);
    }

    if (alternate == "") {
        sum4 = 0;
    }
    else {
        sum4 = sum4 + parseFloat(alternate);
    }

    sum = sum1 + sum2 + sum3 + sum4;


    document.getElementById('<%=lblTotal.ClientID %>').innerHTML = sum;
    var GoalAmount = document.getElementById('<%=txtGoalAmountReq.ClientID %>').value;
    if(sum>GoalAmount)
        document.getElementById('<%=lblYesNo.ClientID %>').innerHTML = "Yes";
        else
            document.getElementById('<%=lblYesNo.ClientID %>').innerHTML = "No"
 </script>
 