<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerIncome.ascx.cs"
    Inherits="WealthERP.Customer.ViewCustomerIncome" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<script>
    var content_Prefix = "ctrl_CustomerIncome_";
    function CalculateMonth(txtYear, txtMonthName) {
        txtMonthName = content_Prefix + txtMonthName;
        txtMonth = document.getElementById(txtMonthName)
        if (!isNaN(txtYear.value) && txtYear.value != 0) {

            txtMonth.value = roundNumber(parseFloat(txtYear.value) / 12, 2).toFixed(2);
            txtYear.value = parseFloat(txtYear.value).toFixed(2);
        }
        else {
            txtMonth.value = "0.00";
            txtYear.value = "0.00";
        }

    }
    function CalculateYear(txtMonth, txtYearName) {
        txtYearName = content_Prefix + txtYearName;
        txtYear = document.getElementById(txtYearName);
        if (!isNaN(txtMonth.value) && txtMonth.value != 0) {

            txtYear.value = roundNumber(parseFloat(txtMonth.value) * 12, 2).toFixed(2);
            txtMonth.value = parseFloat(txtMonth.value).toFixed(2);

        }
        else {

            txtYear.value = "0.00";
            txtMonth.value = "0.00";
        }
    }
    function EnablePropertyDropDown(txtBox, ddlRIProperty) {
        ddlRIProperty = content_Prefix + ddlRIProperty;
        ddlRIProp = document.getElementById(ddlRIProperty);
        if (txtBox.value != 0 && !isNaN(txtBox.value) && txtBox.value != 0.00) {
            ddlRIProp.disabled = false;
        }
        else {
            ddlRIProp.disabled = true;
            ddlRIProp.options.selectedIndex = 0;
        }
    }
    function roundNumber(num, dec) {
        var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
        return result;
    }
</script>

<asp:ScriptManager ID="scripmanager" runat="server">
</asp:ScriptManager>
<table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblView" runat="server" CssClass="HeaderTextBig" Text="Income Details"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table class="TableBackground">
    <tr>
        <td>
            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="PCGButton" CausesValidation="false"                
                OnClick="btnEdit_Click" />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label1" runat="server" Text="Income as of :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtDateOfEntry" runat="server" CssClass="txtField" Style="text-align: center"></asp:TextBox>
            <cc1:CalendarExtender ID="txtDateOfEntry_CalendarExtender" runat="server" TargetControlID="txtDateOfEntry"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <asp:CompareValidator ID="CompareValidator1" runat="server" Operator="LessThanEqual"
                ErrorMessage="Please Select a valid date" Type="Date" ControlToValidate="txtDateOfEntry"
                CssClass="cvPCG"></asp:CompareValidator>
            <cc1:TextBoxWatermarkExtender ID="txtDateOfEntry_TextBoxWatermarkExtender" WatermarkText="dd/mm/yyyy"
                TargetControlID="txtDateOfEntry" runat="server">
            </cc1:TextBoxWatermarkExtender>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp
        </td>
    </tr>
    <tr>
        <td align="right">            
            <asp:Label ID="Label2" runat="server" Text="Category" CssClass="FieldName"></asp:Label>
        </td>
        <td >
        &nbsp;&nbsp;
            <asp:Label ID="Label3" runat="server" Text="Amount/Month" CssClass="FieldName"></asp:Label>
        </td>
        <td >
        &nbsp;&nbsp;
            <asp:Label ID="Label4" runat="server" Text="Currency" CssClass="FieldName"></asp:Label>
        </td>
        <td >
        &nbsp;&nbsp;
            <asp:Label ID="Label5" runat="server" Text="Yearly" CssClass="FieldName"></asp:Label>
        </td>
    </tr>   
    <tr>
        <td class="leftField">
            <asp:Label ID="Label6" runat="server" Text="Gross Salary:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtGSMonthly" runat="server" CssClass="txtField" onblur="CalculateYear(this,'txtGSYearly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtGSMonthly_RegularExpressionValidator" ControlToValidate="txtGSMonthly"
                Display="Dynamic" runat="server" ErrorMessage="<br/>Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlGSCurrency" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtGSYearly" runat="server" CssClass="txtField" onblur="CalculateMonth(this,'txtGSMonthly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtGSYearly_RegularExpressionValidator" ControlToValidate="txtGSYearly"
                Display="Dynamic" runat="server" ErrorMessage="<br/>Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label7" runat="server" Text="Take Home Salary:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtTHSMonthly" runat="server" CssClass="txtField" onblur="CalculateYear(this,'txtTHSYearly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtTHSMonthly_RegularExpressionValidator" ControlToValidate="txtTHSMonthly"
                Display="Dynamic" runat="server" ErrorMessage="<br/>Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlTHSCurrency" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtTHSYearly" runat="server" CssClass="txtField" onblur="CalculateMonth(this,'txtTHSMonthly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtTHSYearly_RegularExpressionValidator" ControlToValidate="txtTHSYearly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label8" runat="server" Text="Rental Income:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtRIMonthly" runat="server" CssClass="txtField" onblur="CalculateYear(this,'txtRIYearly');EnablePropertyDropDown(this,'ddlRIProperty')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtRIMonthly_RegularExpressionValidator" ControlToValidate="txtRIMonthly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlRICurrency" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtRIYearly" runat="server" CssClass="txtField" onblur="CalculateMonth(this,'txtRIMonthly');EnablePropertyDropDown(this,'ddlRIProperty')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtRIYearly_RegularExpressionValidator" ControlToValidate="txtRIYearly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td colspan="1">
        </td>
        <td class="rightField" colspan="4">
            <asp:DropDownList ID="ddlRIProperty" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label9" runat="server" Text="Pension Income:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtPIMonthly" runat="server" CssClass="txtField" onblur="CalculateYear(this,'txtPIYearly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtPIMonthly_RegularExpressionValidator" ControlToValidate="txtPIMonthly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlPICurrency" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtPIYearly" runat="server" CssClass="txtField" onblur="CalculateMonth(this,'txtPIMonthly')"
                Style="text-align: right"></asp:TextBox>
            
            <asp:RegularExpressionValidator ID="txtPIYearly_RegularExpressionValidator" ControlToValidate="txtPIYearly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label10" runat="server" Text="Agricultural Income:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtAIMonthly" runat="server" CssClass="txtField" onblur="CalculateYear(this,'txtAIYearly')"
                Style="text-align: right"></asp:TextBox>
            
            <asp:RegularExpressionValidator ID="txtAIMonthly_RegularExpressionValidator" ControlToValidate="txtAIMonthly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlAICurrency" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtAIYearly" runat="server" CssClass="txtField" onblur="CalculateMonth(this,'txtAIMonthly')"
                Style="text-align: right"></asp:TextBox>
            
            <asp:RegularExpressionValidator ID="txtAIYearly_RegularExpressionValidator" ControlToValidate="txtAIYearly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label11" runat="server" Text="Business Income:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtBIMonthly" runat="server" CssClass="txtField" onblur="CalculateYear(this,'txtBIYearly')"
                Style="text-align: right"></asp:TextBox>
            
            <asp:RegularExpressionValidator ID="txtBIMonthly_RegularExpressionValidator" ControlToValidate="txtBIMonthly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlBICurrency" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtBIYearly" runat="server" CssClass="txtField" onblur="CalculateMonth(this,'txtBIMonthly')"
                Style="text-align: right"></asp:TextBox>
            
            <asp:RegularExpressionValidator ID="txtBIYearly_RegularExpressionValidator" ControlToValidate="txtBIYearly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label12" runat="server" Text="Income from other sources:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtOSIMonthly" runat="server" CssClass="txtField" onblur="CalculateYear(this,'txtOSIYearly')"
                Style="text-align: right"></asp:TextBox>
            
            <asp:RegularExpressionValidator ID="txtOSIMonthly_RegularExpressionValidator" ControlToValidate="txtOSIMonthly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlOSICurrency" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtOSIYearly" runat="server" CssClass="txtField" onblur="CalculateMonth(this,'txtOSIMonthly')"
                Style="text-align: right"></asp:TextBox>
            
            <asp:RegularExpressionValidator ID="txtOSIYearly_RegularExpressionValidator" ControlToValidate="txtOSIYearly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
    <td class="leftField">
    <asp:Label ID="lbltotal" runat="server" Text="Total Income:" CssClass="FieldName"></asp:Label>
    </td>
    <td>
    
<asp:TextBox ID="txttotal" runat="server" CssClass="txtField" Style="text-align: right" Enabled = "false"></asp:TextBox>
    </td>
    <td>
    <asp:DropDownList ID="ddlTotal" runat="server" CssClass="cmbField">
            </asp:DropDownList>
    </td>
    <td>
    <asp:TextBox ID="txttotalyear" runat="server" CssClass="txtField" Style="text-align: right" Enabled="false"></asp:TextBox>
    </td>
    </tr>
    <tr>
        <td colspan="4" align="left">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerIndividualAdd_btnSubmit');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerIndividualAdd_btnSubmit', 'S');"
                OnClick="btnSave_Click" />
        </td>
    </tr>
</table>
