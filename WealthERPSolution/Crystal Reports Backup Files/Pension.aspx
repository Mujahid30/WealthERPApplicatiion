<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pension.aspx.cs" Inherits="WealthERP.CustomerPortfolio.Pension" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">


.HeaderTextSmall
{
	font-family:Verdana,tahoma;
	font-weight:bold;
	font-size:small;
	color:#3f82C1;
}

    .style3
    {
        width: 286px;
    }

.FieldName
{
	font-family:Verdana,tahoma;
	font-weight:bold;
	font-size:x-small;
	color:#16518A;
	
}

.FieldName
{
	font-family:Verdana,tahoma;
	font-weight:bold;
	font-size:x-small;
	color:#16518A;
	
}

    .style2
    {
        width: 241px;
    }
    .cmbField
{
    font-family:Verdana,tahoma;
	font-weight:normal;
	font-size:x-small;
	color:#16518A;
	width:160px;
    margin-bottom: 0px;
}



.cmbField
{
    font-family:Verdana,tahoma;
	font-weight:normal;
	font-size:x-small;
	color:#16518A;
	width:160px;
}



.txtField
{
    font-family:Verdana,tahoma;
	font-weight:normal;
	font-size:x-small;
	color:#16518A;
	width:160px
}
.txtField
{
    font-family:Verdana,tahoma;
	font-weight:normal;
	font-size:x-small;
	color:#16518A;
	width:160px
}

.ButtonField
{
    color:Black;
    font-family:Verdana,tahoma;
    font-size:x-small;
    font-weight:bold;
    width:90px;
    height: 26px;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
<table style="width:100%;">
    <tr>
        <td class="style1" colspan="3">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextSmall" 
                Text="Pension And Gratuities"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="style1" colspan="3">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td class="style3">
            <asp:Label ID="lblOrgName" runat="server" CssClass="FieldName" 
                Text="Organization Name"></asp:Label>
        </td>
        <td class="style2">
            <asp:TextBox ID="txtOrganizationName" runat="server" Width="158px"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" 
                Text=" Category"></asp:Label>
        </td>
        <td class="style2">
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" 
                Height="16px" Width="162px">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            <asp:Label ID="lblAccountId" runat="server" CssClass="FieldName" 
                Text="Account Id"></asp:Label>
        </td>
        <td class="style2">
            <asp:DropDownList ID="ddlAccountId" runat="server" CssClass="cmbField" 
                Height="16px" Width="162px">
                <asp:ListItem>1008</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            <asp:Label ID="blDebtIssuerCode" runat="server" CssClass="FieldName" 
                Text="Debt Issuer Code"></asp:Label>
        </td>
        <td class="style2">
            <asp:DropDownList ID="ddlDebtIssuerCode" runat="server" CssClass="cmbField" 
                Height="16px" Width="162px">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            <asp:Label ID="Label3" runat="server" CssClass="FieldName" 
                Text="Fiscal Year Code"></asp:Label>
        </td>
        <td class="style2">
            <asp:DropDownList ID="ddlFiscalYearCode" runat="server" CssClass="cmbField" 
                Height="16px" Width="162px">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            <asp:Label ID="lblDepositAmount" runat="server" CssClass="FieldName" 
                Text="Deposit Amount"></asp:Label>
        </td>
        <td class="style2">
            <asp:TextBox ID="txtDepositAmount" runat="server" Width="158px"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            <asp:Label ID="lblInterestBasis" runat="server" CssClass="FieldName" 
                Text="Interest Basis"></asp:Label>
        </td>
        <td class="style2">
            <asp:DropDownList ID="ddlInterestBasis" runat="server" CssClass="cmbField" 
                Height="16px" Width="162px" AutoPostBack="True" 
                onselectedindexchanged="ddlInterestBasis_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            <asp:Label ID="Label6" runat="server" CssClass="FieldName" 
                Text="Interest Frequency Code"></asp:Label>
        </td>
        <td class="style2">
            <asp:DropDownList ID="ddlSIFrequencyCode" runat="server" CssClass="cmbField" 
                Height="16px" Width="162px" AutoPostBack="True" 
                onselectedindexchanged="ddlInterestBasis_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            <asp:Label ID="Label5" runat="server" CssClass="FieldName" 
                Text="Compound Interest Frequency Code"></asp:Label>
        </td>
        <td class="style2">
            <asp:DropDownList ID="ddlCIInterestFrequencyCode" runat="server" CssClass="cmbField" 
                Height="16px" Width="162px" AutoPostBack="True" 
                onselectedindexchanged="ddlInterestBasis_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            <asp:Label ID="lblInterestRate" runat="server" CssClass="FieldName" 
                Text="Interest Rate"></asp:Label>
        </td>
        <td class="style2">
            <asp:TextBox ID="txtInterstRate" runat="server" Width="158px" 
                CssClass="txtField"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            <asp:Label ID="lblPurchaseDate" runat="server" CssClass="FieldName" 
                Text="Purchase Date"></asp:Label>
        </td>
        <td class="style2">
            <asp:DropDownList ID="ddlPurchaseDay" runat="server" CssClass="cmbField" 
                Height="16px" Width="72px">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlProductMonth" runat="server" CssClass="cmbField" 
                Height="16px" 
                Width="72px">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlProductYear" runat="server" CssClass="cmbField" 
                Height="16px" Width="72px" >
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            <asp:Label ID="lblCurrentValue" runat="server" CssClass="FieldName" 
                Text="Current Value"></asp:Label>
        </td>
        <td class="style2">
            <asp:TextBox ID="txtCurrentValue" runat="server" Width="158px" 
                CssClass="txtField"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            <asp:Label ID="lblMaturityDate" runat="server" CssClass="FieldName" 
                Text="Maturity Date"></asp:Label>
        </td>
        <td class="style2">
            <asp:DropDownList ID="ddlMaturityDay" runat="server" CssClass="cmbField" 
                Height="16px" Width="72px">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlMaturityMonth" runat="server" CssClass="cmbField" 
                Height="16px" Width="72px">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlMaturityYear" runat="server" CssClass="cmbField" 
                Height="16px" Width="72px">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            <asp:Label ID="lblMaturityValue" runat="server" CssClass="FieldName" 
                Text="Maturity Value"></asp:Label>
        </td>
        <td class="style2">
            <asp:TextBox ID="txtMaturityValue" runat="server" Width="158px"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            <asp:Label ID="lblInterestAccumulated" runat="server" CssClass="FieldName" 
                Text="Is Interest Accumulated ?"></asp:Label>
        </td>
        <td class="style2">
            <asp:RadioButton ID="rbtnAccumulated" runat="server" CssClass="txtField" 
                Text="Yes" Width="100px" oncheckedchanged="rbtnAccumulated_CheckedChanged" GroupName="Interest"
                ValidationGroup="InterestAccumulated" AutoPostBack="True" />
            <asp:RadioButton ID="rbtnPaidout" runat="server" CssClass="txtField" 
                Text="No" oncheckedchanged="rbtnPaidout_CheckedChanged" GroupName="Interest"
                ValidationGroup="InterestAccumulated" Width="122px" AutoPostBack="True" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            <asp:Label ID="lblAmountAccumulated" runat="server" CssClass="FieldName" 
                Text="Amount Accumulated"></asp:Label>
        </td>
        <td class="style2">
            <asp:TextBox ID="txtAmountAccumulated" runat="server" Width="158px" 
                CssClass="txtField"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            <asp:Label ID="lblAmountPaidout" runat="server" CssClass="FieldName" 
                Text="Amount Paidout"></asp:Label>
        </td>
        <td class="style2">
            <asp:TextBox ID="txtAmountPaidout" runat="server" Width="158px" 
                CssClass="txtField"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            <asp:Label ID="lblLoanStartDate" runat="server" CssClass="FieldName" 
                Text="Loan Start Date"></asp:Label>
        </td>
        <td class="style2">
            <asp:DropDownList ID="ddlLoanStartDay" runat="server" CssClass="cmbField" 
                Height="16px" Width="72px">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlLoanStartMonth" runat="server" CssClass="cmbField" 
                Height="16px" Width="72px">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlLoanStartYear" runat="server" CssClass="cmbField" 
                Height="16px" Width="72px">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            <asp:Label ID="lblLoanEndDate" runat="server" CssClass="FieldName" 
                Text="Loan End Date"></asp:Label>
        </td>
        <td class="style2">
            <asp:DropDownList ID="ddlLoanEndDay" runat="server" CssClass="cmbField" 
                Height="16px" Width="72px">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlLoanEndMonth" runat="server" CssClass="cmbField" 
                Height="16px" Width="72px">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlLoanEndYear" runat="server" CssClass="cmbField" 
                Height="16px" Width="72px">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            <asp:Label ID="lblLoanOutstandingAmount" runat="server" CssClass="FieldName" 
                Text="Loan Outstanding Amount"></asp:Label>
        </td>
        <td class="style2">
            <asp:TextBox ID="txtLoanOutstandingAmount" runat="server" Width="158px"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            &nbsp;</td>
        <td class="style2">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            &nbsp;</td>
        <td class="style2">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="ButtonField" 
                onclick="Button1_Click" />
        </td>
        <td class="style1">
            &nbsp;</td>
    </tr>
</table>
    
    </div>
    </form>
</body>
</html>
