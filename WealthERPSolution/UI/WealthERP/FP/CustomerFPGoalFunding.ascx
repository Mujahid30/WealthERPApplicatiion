<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerFPGoalFunding.ascx.cs" Inherits="WealthERP.FP.CustomerFPGoalFunding" %>



<asp:Label ID="headertitle" runat="server" CssClass="HeaderTextBig" Text="Goal Funding"></asp:Label>
<hr />
<table class="TableBackground" style="width: 100%">
<tr>
    <td></td>
    <td></td>
</tr>
<tr>
    <td class="leftField">
        <asp:Label ID="lblPickGoal" runat="server" CssClass="FieldName" Text="Pick a Goal:"></asp:Label>
    </td> 
    <td>
        <asp:DropDownList ID="ddlPickGoal" CssClass="cmbField" runat="server">
            <asp:ListItem>Select</asp:ListItem>
            <asp:ListItem>Goal1</asp:ListItem>
            <asp:ListItem>Goal2</asp:ListItem>
            <asp:ListItem>Goal3</asp:ListItem>
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td class="leftField">
    <asp:Label ID="lblGoalAmount" runat="server" CssClass="FieldName" Text="Goal Amount requirement in"></asp:Label>
         <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="2025:"></asp:Label>
    </td>
    <td>
         
        <asp:TextBox ID="txtGoalAmountReq" CssClass="txtField"   Width="95%" runat="server" 
             Style="direction: rtl" ReadOnly="True"></asp:TextBox>
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
    <td align="center">
    <asp:Label ID="lblAvailableCorpus" runat="server" CssClass="FieldName" Text="Available Corpus in<br/>Starting of Goal Year"></asp:Label>
    </td>
    <td align="center">
        <asp:Label ID="lblAllocatedAmount" runat="server" CssClass="FieldName" Text="Allocated Amount"></asp:Label>
    </td>
    <td align="center">
        <asp:Label ID="lblAllocation" runat="server" CssClass="FieldName" Text="% Allocation"></asp:Label>
    </td>
    <td align="center">
        <asp:Label ID="lblAfterAllocation" runat="server" CssClass="FieldName" Text="Remaining Corpus<br/>After Allocation"></asp:Label>
    </td>
</tr>
<tr>
    <td class="leftField"  >
        <asp:Label ID="lblEquity" runat="server" CssClass="FieldName" Text="Equity:"></asp:Label>
    </td>
     <td>
        <asp:TextBox ID="TextBox3" CssClass="txtField"  Width="95%" runat="server" Style="direction: rtl" ReadOnly="True"></asp:TextBox>
    </td>
     <td>
        <asp:TextBox ID="TextBox7" CssClass="txtField"   Width="95%" runat="server" Style="direction: rtl"></asp:TextBox>
    </td>
     <td>
        <asp:TextBox ID="TextBox14" CssClass="txtField"   Width="95%" runat="server" ReadOnly="True" Style="direction: rtl"></asp:TextBox>
    </td>
     <td>
        <asp:TextBox ID="TextBox15" CssClass="txtField"   Width="95%" runat="server" ReadOnly="True" Style="direction: rtl"></asp:TextBox>
    </td>
</tr>
<tr>
    <td class="leftField"  >
        <asp:Label ID="lblDebt" runat="server" CssClass="FieldName" Text="Debt:"></asp:Label>
    </td>
     <td>
        <asp:TextBox ID="TextBox4" CssClass="txtField"   Width="95%" runat="server" ReadOnly="True" Style="direction: rtl"></asp:TextBox>
    </td>
     <td>
        <asp:TextBox ID="TextBox8" CssClass="txtField"   Width="95%" runat="server" Style="direction: rtl"></asp:TextBox>
    </td>
     <td>
        <asp:TextBox ID="TextBox13" CssClass="txtField"   Width="95%" runat="server" ReadOnly="True" Style="direction: rtl"></asp:TextBox>
    </td>
     <td>
        <asp:TextBox ID="TextBox16" CssClass="txtField"   Width="95%" runat="server" ReadOnly="True" Style="direction: rtl"></asp:TextBox>
    </td>
</tr>
<tr>
    <td class="leftField"  >
        <asp:Label ID="lblCash" runat="server" CssClass="FieldName" Text="Cash:"></asp:Label>
    </td>
     <td>
        <asp:TextBox ID="TextBox5" CssClass="txtField"   Width="95%" runat="server" ReadOnly="True" Style="direction: rtl"></asp:TextBox>
    </td>
     <td>
        <asp:TextBox ID="TextBox9" CssClass="txtField"   Width="95%" runat="server" Style="direction: rtl"></asp:TextBox>
    </td>
     <td>
        <asp:TextBox ID="TextBox12" CssClass="txtField"   Width="95%" runat="server" ReadOnly="True" Style="direction: rtl"></asp:TextBox>
    </td>
     <td>
        <asp:TextBox ID="TextBox17" CssClass="txtField"   Width="95%" runat="server" ReadOnly="True" Style="direction: rtl"></asp:TextBox>
    </td>
</tr>
<tr>
    <td class="leftField">
        <asp:Label ID="lblAlternate" runat="server" CssClass="FieldName" Text="Alternate:"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="TextBox6" CssClass="txtField"   Width="95%" runat="server" ReadOnly="True" Style="direction: rtl"></asp:TextBox>
    </td>
     <td>
        <asp:TextBox ID="TextBox10" CssClass="txtField"   Width="95%" runat="server" Style="direction: rtl"></asp:TextBox>
    </td>
     <td>
        <asp:TextBox ID="TextBox11" CssClass="txtField"   Width="95%" runat="server" ReadOnly="True" Style="direction: rtl"></asp:TextBox>
    </td>
     <td>
        <asp:TextBox ID="TextBox18" CssClass="txtField"   Width="95%" runat="server" ReadOnly="True" Style="direction: rtl"></asp:TextBox>
    </td>
</tr>
<tr>
    <td></td>
    <td align="center">
        <asp:Label ID="lblTotal" runat="server" CssClass="FieldName" Text="Total"></asp:Label>
    </td>
    <td align="center">
        <asp:Label ID="lblTotalAmount" runat="server" CssClass="FieldName" Text="(Total)"></asp:Label>
    </td>
    <td></td>
    <td></td>
</tr>

<tr>
    <td></td>
    <td>&nbsp;</td>
</tr>
<tr>
    <td class="leftField">
        <asp:Label ID="lblGap" runat="server" CssClass="FieldName" Text="Gap After Allocation:"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtGapAfterAllocation" CssClass="txtField"   Width="95%" runat="server" Style="direction: rtl"></asp:TextBox>
    </td>
</tr>
<tr>
    <td class="leftField">
        &nbsp;</td>
    <td>
        &nbsp;</td>    
</tr>
<tr>
    <td class="leftField">
    <asp:Label ID="lblMoney" runat="server" CssClass="FieldName" Text="You Have Enough Money:"></asp:Label>
    </td>
    <td>
        <asp:Label ID="lblYesNo" runat="server" CssClass="FieldName" Text="Yes/No"></asp:Label>
        </td>
</tr>
<tr>
    <td></td>
    <td></td>
</tr>
<tr>
    <td>
        &nbsp;</td>
    <td>
    <asp:Checkbox ID="chkGoalFundByLoan" runat="server" CssClass="FieldName" Text="Goal to be Funded by Loan"
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
        <asp:Label ID="lblLoanAmount" runat="server" CssClass="FieldName" Text="Loan Amount to be Taken for goal Funding:"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtLoanAmountFunding" runat="server"   Width="95%" Style="direction: rtl"></asp:TextBox></td>
    <td class="leftField">
        <asp:Label ID="lblStartYear" runat="server" CssClass="FieldName" Text="Start Loan Year:"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtStartLoanYr" runat="server"   Width="95%" Style="direction: rtl"></asp:TextBox>
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
        <asp:Button ID="btnSubmit" CssClass="PCGButton" runat="server" Text="Submit" />
    </td>
</tr>
</table>