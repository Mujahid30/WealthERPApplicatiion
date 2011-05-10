<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerFPGoalFunding.ascx.cs" Inherits="WealthERP.FP.CustomerFPGoalFunding" %>


<style type="text/css">
    .style1
    {
        height: 26px;
    }
</style>


<table width="100%">
<tr>
 <td class="leftField" style="width:15%">
  <asp:Label ID="lblGoals" runat="server" CssClass="FieldName" Text="Pick a Goal :">
  </asp:Label>
 </td>                    
                       
<td colspan="4" class="rightField">
 <asp:DropDownList ID="ddlGoals" runat="server" CssClass="cmbField">
 </asp:DropDownList>
</td>
</tr>

<tr>
<td colspan="5">
<asp:Label ID="lblGoalAmount" runat="server" CssClass="FieldName" Text="">
</asp:Label>
</td>
</tr>

<tr>
<td>
</td>
<td style="width:15%">
<asp:Label ID="lblAvailablecorpus" runat="server" CssClass="FieldName" Text="Available corpus in starting of goal year">
</asp:Label>
</td>
<td>
<asp:Label ID="lblAllocatedAmount" runat="server" CssClass="FieldName" Text="Allocated Amount">
</asp:Label>
</td>

<td>
<asp:Label ID="lblAllocationPercentage" runat="server" CssClass="FieldName" Text="(%)Allocation">
</asp:Label>
</td>

<td>
<asp:Label ID="lblRemainingCorpus" runat="server" CssClass="FieldName" Text="Remaining corpus after allocation">
</asp:Label>
</td>
</tr>


<tr>
<td style="width:15%">
<asp:Label ID="lblEquity" runat="server" CssClass="FieldName" Text="Equity">
</asp:Label>
</td>

<td class="style1">
 <asp:TextBox ID="txtEquityAvlCorpus" runat="server" AutoCompleteType="Disabled" CssClass="txtField">
 </asp:TextBox>
</td>
<td class="style1">
 <asp:TextBox ID="txtEquityAllocatedAmount" runat="server" AutoCompleteType="Disabled" CssClass="txtField">
 </asp:TextBox>
</td>
<td class="style1">
 <asp:TextBox ID="txtEquityAllocationPercentage" runat="server" AutoCompleteType="Disabled" CssClass="txtField">
 </asp:TextBox>
</td>
<td class="style1">
 <asp:TextBox ID="txtEquityRemainingCorpus" runat="server" AutoCompleteType="Disabled" CssClass="txtField">
 </asp:TextBox>
</td>
</tr>

<tr>
<td style="width:15%">
<asp:Label ID="lblDebt" runat="server" CssClass="FieldName" Text="Debt">
</asp:Label>
</td>

<td>
 <asp:TextBox ID="txtDebtAvlCorpus" runat="server" AutoCompleteType="Disabled" CssClass="txtField">
 </asp:TextBox>
</td>
<td>
 <asp:TextBox ID="txtDebtAllocatedAmount" runat="server" AutoCompleteType="Disabled" CssClass="txtField">
 </asp:TextBox>
</td>
<td>
 <asp:TextBox ID="txtDebtAllocationPercentage" runat="server" AutoCompleteType="Disabled" CssClass="txtField">
 </asp:TextBox>
</td>
<td>
 <asp:TextBox ID="txtDebtRemainingCorpus" runat="server" AutoCompleteType="Disabled" CssClass="txtField">
 </asp:TextBox>
</td>
</tr>

<tr>
<td style="width:15%">
<asp:Label ID="lblCash" runat="server" CssClass="FieldName" Text="Cash">
</asp:Label>
</td>

<td>
 <asp:TextBox ID="txtCashAvlCorpus" runat="server" AutoCompleteType="Disabled" CssClass="txtField">
 </asp:TextBox>
</td>
<td>
 <asp:TextBox ID="txtCashAllocatedAmount" runat="server" AutoCompleteType="Disabled" CssClass="txtField">
 </asp:TextBox>
</td>
<td>
 <asp:TextBox ID="txtCashAllocationPercentage" runat="server" AutoCompleteType="Disabled" CssClass="txtField">
 </asp:TextBox>
</td>
<td>
 <asp:TextBox ID="txtCashRemainingCorpus" runat="server" AutoCompleteType="Disabled" CssClass="txtField">
 </asp:TextBox>
</td>
</tr>

<tr>
<td>
<asp:Label ID="lblTotal" runat="server" CssClass="FieldName" Text="Total">
</asp:Label>
</td>

<td>
<asp:Label ID="lblTotalAvlCorpus" runat="server" CssClass="FieldName" Text="">
</asp:Label> 
</td>

<td>
<asp:Label ID="lblTotalAllocatedAmount" runat="server" CssClass="FieldName" Text="">
</asp:Label>  
</td>

<td>
<asp:Label ID="lblTotalAllocationPercentage" runat="server" CssClass="FieldName" Text="">
</asp:Label> 
</td>

<td>
<asp:Label ID="lblTotalRemainingCorpus" runat="server" CssClass="FieldName" Text="">
</asp:Label> 
</td>
</tr>

<tr>
<td colspan="5">
<asp:Label ID="lblGoalGap" runat="server" CssClass="FieldName" Text="Total">
</asp:Label>
</td>
</tr>

<tr>
<td class="leftField" style="width:15%">
  
 </td> 
<td colspan="4">
<asp:CheckBox ID="chkGoalFundByLoan" runat="server" Text="Goal to be funded by loan" CssClass="FieldName"/>
</td>
</tr>

<tr>
 <td class="leftField" style="width:15%">
  <asp:Label ID="lblLoanAmountGoalFund" runat="server" CssClass="FieldName" Text="Loan amount to be taken for goal funding :">
  </asp:Label>
 </td>                    
                       
<td colspan="4" class="rightField">
 <asp:TextBox ID="txtLoanAmountGoalFund" runat="server" AutoCompleteType="Disabled" CssClass="txtField">
 </asp:TextBox>
</td>
</tr>

<tr>
 <td class="leftField" style="width:15%">
  <asp:Label ID="lblStartLoanYear" runat="server" CssClass="FieldName" Text="start loan year :">
  </asp:Label>
 </td>                    
                       
<td colspan="4" class="rightField">
 <asp:TextBox ID="txtStartLoanYear" runat="server" AutoCompleteType="Disabled" CssClass="txtField">
 </asp:TextBox>
</td>
</tr>

<tr>
 <td class="leftField" style="width:15%">
  
 </td>                    
                       
<td colspan="4" class="rightField">
 <asp:Button ID="btnRTSave" runat="server" CssClass="PCGButton" 
                            Text="Submit" ValidationGroup="btnRTSave"  />
</td>
</tr>
</table>
