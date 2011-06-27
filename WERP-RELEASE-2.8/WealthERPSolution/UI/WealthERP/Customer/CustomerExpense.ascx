<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerExpense.ascx.cs"
    Inherits="WealthERP.Customer.ViewCustomerExpense" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script>



    var content_Prefix = "ctrl_CustomerExpense_";
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
    function roundNumber(num, dec) {
        var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
        return result;
    }
    //    function EnablePropertyDropDown(txtBox, ddlRIProperty) {
    //        ddlRIProperty = content_Prefix + ddlRIProperty;
    //        ddlRIProp = document.getElementById(ddlRIProperty);
    //        if (txtBox.value != 0 && !isNaN(txtBox.value)) {
    //            ddlRIProp.disabled = false;
    //        }
    //        else {
    //            ddlRIProp.disabled = true;
    //            ddlRIProp.options.selectedIndex = 0;
    //        }
    //    }
</script>

<asp:ScriptManager ID="scripmanager" runat="server">
</asp:ScriptManager>
<table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblView" runat="server" CssClass="HeaderTextBig" Text="Expense Details"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table class="TableBackground">
    <tr>
        <td>
            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="PCGButton" CausesValidation="false"
                onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerIndividualAdd_btnSubmit');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerIndividualAdd_btnSubmit', 'S');"
                OnClick="btnEdit_Click" />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label1" runat="server" Text="Expense as of:" CssClass="FieldName"></asp:Label>
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
        <td class="HeaderCell">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label2" runat="server" Text="Category" CssClass="FieldName"></asp:Label>
        </td>
        <td class="HeaderCell">
            &nbsp;
            <asp:Label ID="Label3" runat="server" Text="Amount/Month" CssClass="FieldName"></asp:Label>
        </td>
        <td class="HeaderCell">
            &nbsp;
            <asp:Label ID="Label4" runat="server" Text="Currency" CssClass="FieldName"></asp:Label>
        </td>
        <td class="HeaderCell">
            &nbsp;
            <asp:Label ID="Label5" runat="server" Text="Yearly" CssClass="FieldName"></asp:Label>
        </td>
    </tr>    
    <tr>
        <td class="leftField">
            <asp:Label ID="Label6" runat="server" Text="Transportation:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtTranMonthly" runat="server" CssClass="txtField" onblur="CalculateYear(this,'txtTranYearly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtTranMonthly_RegularExpressionValidator" ControlToValidate="txtTranMonthly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlTranCurrency" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtTranYearly" runat="server" CssClass="txtField" onblur="CalculateMonth(this,'txtTranMonthly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtTranYearly_RegularExpressionValidator" ControlToValidate="txtTranYearly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label7" runat="server" Text="Food:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtFoodMonthly" runat="server" CssClass="txtField" onblur="CalculateYear(this,'txtFoodYearly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtFoodMonthly_RegularExpressionValidator" ControlToValidate="txtFoodMonthly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlFoodCurrency" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtFoodYearly" runat="server" CssClass="txtField" onblur="CalculateMonth(this,'txtFoodMonthly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtFoodYearly_RegularExpressionValidator" ControlToValidate="txtFoodYearly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label8" runat="server" Text="Clothing:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtCloMonthly" runat="server" CssClass="txtField" onblur="CalculateYear(this,'txtCloYearly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtCloMonthly_RegularExpressionValidator" ControlToValidate="txtCloMonthly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlCloCurrency" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtCloYearly" runat="server" CssClass="txtField" onblur="CalculateMonth(this,'txtCloMonthly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtCloYearly_RegularExpressionValidator" ControlToValidate="txtCloYearly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label9" runat="server" Text="Home:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtHomeMonthly" runat="server" CssClass="txtField" onblur="CalculateYear(this,'txtHomeYearly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtHomeMonthly_RegularExpressionValidator" ControlToValidate="txtHomeMonthly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlHomeCurrency" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtHomeYearly" runat="server" CssClass="txtField" onblur="CalculateMonth(this,'txtHomeMonthly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtHomeYearly_RegularExpressionValidator" ControlToValidate="txtHomeYearly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label10" runat="server" Text="Utilities:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtUtiMonthly" runat="server" CssClass="txtField" onblur="CalculateYear(this,'txtUtiYearly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtUtiMonthly_RegularExpressionValidator" ControlToValidate="txtUtiMonthly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlUtiCurrency" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtUtiYearly" runat="server" CssClass="txtField" onblur="CalculateMonth(this,'txtUtiMonthly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtUtiYearly_RegularExpressionValidator" ControlToValidate="txtUtiYearly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label11" runat="server" Text="Self Care:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtSCMonthly" runat="server" CssClass="txtField" onblur="CalculateYear(this,'txtSCYearly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtSCMonthly_RegularExpressionValidator" ControlToValidate="txtSCMonthly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlSCCurrency" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtSCYearly" runat="server" CssClass="txtField" onblur="CalculateMonth(this,'txtSCMonthly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtSCYearly_RegularExpressionValidator" ControlToValidate="txtSCYearly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label12" runat="server" Text="Health Care:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtHCMonthly" runat="server" CssClass="txtField" onblur="CalculateYear(this,'txtHCYearly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtHCMonthly_RegularExpressionValidator" ControlToValidate="txtHCMonthly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlHCCurrency" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtHCYearly" runat="server" CssClass="txtField" onblur="CalculateMonth(this,'txtHCMonthly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtHCYearly_RegularExpressionValidator" ControlToValidate="txtHCYearly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label13" runat="server" Text="Education:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtEduMonthly" runat="server" CssClass="txtField" onblur="CalculateYear(this,'txtEduYearly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtEduMonthly_RegularExpressionValidator" ControlToValidate="txtEduMonthly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlEduCurrency" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtEduYearly" runat="server" CssClass="txtField" onblur="CalculateMonth(this,'txtEduMonthly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtEduYearly_RegularExpressionValidator" ControlToValidate="txtEduYearly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label14" runat="server" Text="Pets:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtPetsMonthly" runat="server" CssClass="txtField" onblur="CalculateYear(this,'txtPetsYearly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtPetsMonthly_RegularExpressionValidator" ControlToValidate="txtPetsMonthly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlPetsCurrency" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtPetsYearly" runat="server" CssClass="txtField" onblur="CalculateMonth(this,'txtPetsMonthly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtPetsYearly_RegularExpressionValidator" ControlToValidate="txtPetsYearly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label15" runat="server" Text="Entertainment:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtEntMonthly" runat="server" CssClass="txtField" onblur="CalculateYear(this,'txtEntYearly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtEntMonthly_RegularExpressionValidator" ControlToValidate="txtEntMonthly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlEntCurrency" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtEntYearly" runat="server" CssClass="txtField" onblur="CalculateMonth(this,'txtEntMonthly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtEntYearly_RegularExpressionValidator" ControlToValidate="txtEntYearly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label16" runat="server" Text="Miscellaneous:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtMisMonthly" runat="server" CssClass="txtField" onblur="CalculateYear(this,'txtMisYearly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtMisMonthly_RegularExpressionValidator" ControlToValidate="txtMisMonthly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlMisCurrency" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtMisYearly" runat="server" CssClass="txtField" onblur="CalculateMonth(this,'txtMisMonthly')"
                Style="text-align: right"></asp:TextBox>
            <asp:RegularExpressionValidator ID="txtMisYearly_RegularExpressionValidator" ControlToValidate="txtMisYearly"
                Display="Dynamic" runat="server" ErrorMessage="<br />Invalid Number. Two decimal places only"
                ValidationExpression="^\d*(\.(\d{0,2}))?$" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
    <td class="leftField">
    <asp:Label ID="lbltotal" runat="server" Text="Total Expense:" CssClass="FieldName"></asp:Label>
    </td>
    <td>
    <asp:TextBox ID="txttotal" runat="server" CssClass="txtField" Style="text-align: right" Enabled="false"></asp:TextBox>
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
