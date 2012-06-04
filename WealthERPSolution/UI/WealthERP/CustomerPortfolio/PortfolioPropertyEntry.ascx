<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioPropertyEntry.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioPropertyEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--Javascript Calendar Controls - Required Files--%>

<script type="text/javascript" src="../Scripts/Calender/calendar.js"></script>

<script type="text/javascript" src="../Scripts/Calender/lang/calendar-en.js"></script>

<script type="text/javascript" src="../Scripts/Calender/calendar-setup.js"></script>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<link href="../Scripts/Calender/skins/aqua/theme.css" rel="stylesheet" type="text/css" />
<%--Javascript Calendar Controls - Required Files--%>
<asp:ScriptManager ID="scrptMgr" runat="server">

</asp:ScriptManager>
<script type="text/javascript">
    function SetCurrentValue() {
        

    };
</script>
<script type="text/javascript" language="javascript">
    var content_Prefix = "ctrl_PortfolioPropertyEntry_";
    function CalculateCost(txtPurchasePrice, txtQuantity, txtPurchaseValue) {
        TextboxesSwapingVales();
       

        txtQuantity = content_Prefix + txtQuantity;
        txtQty = document.getElementById(txtQuantity);
        txtPurchaseValue = content_Prefix + txtPurchaseValue;
        txtPurchaseVal = document.getElementById(txtPurchaseValue);
        if (!isNaN(txtPurchasePrice.value) && txtPurchasePrice.value != 0 && !isNaN(txtQty.value) && txtQty.value != 0) {
            txtPurchaseVal.value = (txtPurchasePrice.value * txtQty.value).toFixed(2);
            txtMonth.value = parseFloat(txtMonth.value).toFixed(2);
        }

        MoveValuetoCurrentVal()
    }

    function CalculateCostForCurrentVal(txtPurchasePrice, txtQuantity, txtCurrentValue) {
        
        txtQuantity = content_Prefix + txtQuantity;
        txtQty = document.getElementById(txtQuantity);
        txtCurrentValue = content_Prefix + txtCurrentValue;
        txtPurchaseVal = document.getElementById(txtCurrentValue);
        if (!isNaN(txtPurchasePrice.value) && txtPurchasePrice.value != 0 && !isNaN(txtQty.value) && txtQty.value != 0) {
            txtPurchaseVal.value = (txtPurchasePrice.value * txtQty.value).toFixed(2);
            txtMonth.value = parseFloat(txtMonth.value).toFixed(2);
        }
    }
    
    function TextboxesSwapingVales() {
        if (document.getElementById('<%=txtPurchasePrice.ClientID %>').value > 0)
            document.getElementById('<%=txtCurrentPrice.ClientID %>').value = document.getElementById('<%=txtPurchasePrice.ClientID %>').value;
    }

    function MoveValuetoCurrentVal() {
        if (document.getElementById('<%=txtPurchaseValue.ClientID %>').value > 0)
            document.getElementById('<%=txtCurrentValue.ClientID %>').value = document.getElementById('<%=txtPurchaseValue.ClientID %>').value;
    }

</script>
<%--<asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>--%>
<table class="TableBackground" style="width: 100%">
    <tr>
        <td colspan="6" class="HeaderCell">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Edit Property"></asp:Label>
        </td>
    </tr>
    <%--<tr>
                <td colspan="6" class="HeaderCell">
                <asp:Label ID="Label2" runat="server" CssClass="HeaderTextBig" Text="View Property"></asp:Label>
                    <hr />
                </td>
            </tr>--%>
    <tr>
        <td colspan="6">
            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                OnClick="lnkBtnBack_Click" CausesValidation="false"></asp:LinkButton>
            &nbsp; &nbsp; &nbsp; <span id="divEditButton" runat="server" visible="false">
                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CssClass="LinkButtons" CausesValidation="false"
                    OnClick="lnkEdit_Click">
                </asp:LinkButton>
            </span>
        </td>
    </tr>
    <tr>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblInstrumentCat" runat="server" Text="Asset Category:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlInstrumentCat" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlInstrumentCat_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblInstrumentSubCat" runat="server" Text="Sub Category:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlInstrumentSubCat" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Label ID="Label34" runat="server" Text="Account Details" CssClass="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="2" class="leftField">
            <asp:Label ID="Label35" runat="server" Text="Asset Identifier:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtAccountNum" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td>
        </td>
        <td colspan="2">
        </td>
    </tr>
    <tr>
        <td colspan="2" class="leftField">
            <asp:Label ID="Label36" runat="server" Text="Mode Of Holding:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtModeofHolding" runat="server" CssClass="txtField">
            </asp:TextBox>
        </td>
    </tr>
    <%--<tr>
        <td class="style15" colspan="2">
            <asp:Label ID="lblJointHolder" runat="server" Text="Joint Holder's Name" CssClass="FieldName" />
        </td>
        <td class="style22">
            Populate all Joint Holders Here
        </td>
        <td class="style15" colspan="2">
            <asp:Label ID="lblNominee" runat="server" Text="Nominee's Name" CssClass="FieldName" />
        </td>
        <td>
            Populate all Nominees Here
        </td>
    </tr>
    <tr>
        <td colspan="6" class="style15">
            &nbsp;
        </td>
    </tr>--%>
    <tr id="trEditSpace" runat="server" visible="false">
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2" class="leftField">
            <asp:Label ID="Label6" runat="server" Text="Asset Particulars:" CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="4" class="rightField">
            <asp:TextBox ID="txtPropertyName" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvPropertyName" ControlToValidate="txtPropertyName"
                ErrorMessage="Please enter the Asset Particulars" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Label ID="lblAssetLocation" runat="server" Text="Asset Location" CssClass="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="2" class="leftField">
            <asp:Label ID="Label17" runat="server" Text="Line1:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtAdrLine1" runat="server" CssClass="txtField" TextMode="MultiLine"
                Rows="2"></asp:TextBox>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvAdrLine1" ControlToValidate="txtAdrLine1" ErrorMessage="<br>Please enter the Address Line 1"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2" class="leftField">
            <asp:Label ID="Label18" runat="server" Text="Line2:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtAdrLine2" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2" class="leftField">
            <asp:Label ID="Label19" runat="server" Text="Line3 :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtAdrLine3" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2" class="leftField">
            <asp:Label ID="Label21" runat="server" Text="Pincode :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtPinCode" runat="server" CssClass="txtField" MaxLength="6"></asp:TextBox>
            <span id="Span5" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvPinCode" ControlToValidate="txtPinCode" ErrorMessage="<br>Please enter the Pincode"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvPincode" runat="server" ControlToValidate="txtPinCode"
                Type="Integer" CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please enter a valid Pincode"
                Operator="DataTypeCheck"></asp:CompareValidator>
        </td>
        <td colspan="2" class="leftField">
            <asp:Label ID="lblCity" runat="server" Text="City :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtCity" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span7" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCity"
                ErrorMessage="<br>Please enter City" Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label22" runat="server" Text="State:" CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="2" class="rightField">
            <asp:DropDownList ID="ddlState" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span6" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="cvState" runat="server" ControlToValidate="ddlState" ErrorMessage="<br>Please select a State"
                Operator="NotEqual" ValueToCompare="Select a State" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField">
            <asp:Label ID="Label23" runat="server" Text="Country:" CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="2" class="rightField">
            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="cmbField">
                <asp:ListItem>India</asp:ListItem>
            </asp:DropDownList>
            <%--<span id="Span8" class="spnRequiredField">*</span>--%>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Label ID="Label14" runat="server" Text="Purchase Details" CssClass="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="2" class="leftField">
            <asp:Label ID="Label7" runat="server" Text="Purchase Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtPurchaseDate" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="txtPurchaseDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                TargetControlID="txtPurchaseDate">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtPurchaseDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtPurchaseDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <span id="Span9" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvPurchaseDate" ControlToValidate="txtPurchaseDate"
                ErrorMessage="<br/>Please select a Purchase Date" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvPurchaseDate" runat="server" ErrorMessage="<br/>The date format should be dd/mm/yyyy"
                Type="Date" ControlToValidate="txtPurchaseDate" Operator="DataTypeCheck" CssClass="cvPCG"
                Display="Dynamic"></asp:CompareValidator>
            <asp:CompareValidator ID="cvPurchaseDate1" runat="server" ErrorMessage="<br/>The purchase date should not be greater than current date."
                Type="Date" ControlToValidate="txtPurchaseDate" CssClass="cvPCG" Operator="LessThanEqual"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField">
            <asp:Label ID="Label15" runat="server" Text="Area:" CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="2" class="rightField">
            <asp:TextBox ID="txtQuantity" runat="server" CssClass="txtField" Width="110px"></asp:TextBox>
            <asp:DropDownList ID="ddlMeasureCode" runat="server" CssClass="cmbField" Width="50px">
            </asp:DropDownList>
            <span id="Span10" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvQuantity" ControlToValidate="txtQuantity" ErrorMessage="<br>Please enter the Quantity"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvQuantity" runat="server" ErrorMessage="<br>Please enter an integer value"
                Type="Integer" ControlToValidate="txtQuantity" Operator="DataTypeCheck" CssClass="cvPCG"
                Display="Dynamic"></asp:CompareValidator>
            <asp:CompareValidator ID="cvMeasureCode" runat="server" ControlToValidate="ddlMeasureCode"
                ErrorMessage="<br>Please select a Measure Code" Operator="NotEqual" ValueToCompare="Select a Measure Code"
                CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="leftField">
            <asp:Label ID="Label8" runat="server" Text="Purchase Rate per Unit(Rs):" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtPurchasePrice" runat="server" CssClass="txtField"  onblur="MoveValuetoCurrentVal()" onchange="CalculateCost(this,'txtQuantity','txtPurchaseValue')"></asp:TextBox>
            <span id="Span11" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvPurchasePrice" ControlToValidate="txtPurchasePrice"
                ErrorMessage="<br>Please enter the Purchase Price" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvPurchasePrice" runat="server" ErrorMessage="<br>Please enter a numeric value"
                Type="Double" ControlToValidate="txtPurchasePrice" Operator="DataTypeCheck" CssClass="cvPCG"
                Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField">
            <asp:Label ID="Label9" runat="server" Text="Purchase Cost(Rs):" CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="2" class="rightField">
            <asp:TextBox ID="txtPurchaseValue" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span12" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvPurchaseValue" ControlToValidate="txtPurchaseValue"
                ErrorMessage="<br>Please enter the Purchase Value" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvPurchaseValue" runat="server" ErrorMessage="<br>Please enter a numeric value"
                Type="Double" ControlToValidate="txtPurchaseValue" Operator="DataTypeCheck" CssClass="cvPCG"
                Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Label ID="Label10" runat="server" Text="Current Valuation" CssClass="HeaderTextSmall"
                Font-Size="Small"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="2" class="leftField">
            <asp:Label ID="Label11" runat="server" Text="Current Rate per Unit(Rs):" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtCurrentPrice" onblur="CalculateCostForCurrentVal(this,'txtQuantity','txtCurrentValue')"  runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span13" class="spnRequiredField">*</span>--%>
            <%--<asp:RequiredFieldValidator ID="rfvCurrentPrice" ControlToValidate="txtCurrentPrice"
                        ErrorMessage="Please enter the Current Price" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="cvCurrentPrice" runat="server" ErrorMessage="<br>Please enter a numeric value"
                Type="Double" ControlToValidate="txtCurrentPrice" Operator="DataTypeCheck" CssClass="cvPCG"
                Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField">
            <asp:Label ID="Label12" runat="server" Text="Current Value(Rs):" CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="2" class="rightField">
            <asp:TextBox ID="txtCurrentValue" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span14" class="spnRequiredField">*</span>--%>
            <%--<asp:RequiredFieldValidator ID="rfvCurrentValue" ControlToValidate="txtCurrentValue"
                        ErrorMessage="Please enter the Current Value" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="cvCurrentValue" runat="server" ErrorMessage="<br>Please enter a numeric value"
                Type="Double" ControlToValidate="txtCurrentValue" Operator="DataTypeCheck" CssClass="cvPCG"
                Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Label ID="Label24" runat="server" Text="Sale Details" CssClass="HeaderTextSmall"
                Font-Size="Small"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="2" class="leftField">
            <asp:Label ID="Label25" runat="server" Text="Sale Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtSaleDate" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="txtSaleDate_CalendarExtender" runat="server" TargetControlID="txtSaleDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtSaleDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtSaleDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:CompareValidator ID="cvSaleDate" runat="server" ErrorMessage="<br>The date format should be dd/mm/yyyy"
                Type="Date" ControlToValidate="txtSaleDate" Operator="DataTypeCheck" CssClass="cvPCG"
                Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField">
            <asp:Label ID="Label27" runat="server" Text="Sale Rate per Unit(Rs):" CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="2" class="rightField">
            <asp:TextBox ID="txtSaleRate" runat="server" AutoPostBack="true" OnTextChanged="btn_costUpdate" CssClass="txtField"></asp:TextBox>
            <asp:CompareValidator ID="cvSaleRate" runat="server" ErrorMessage="<br>Please enter a numeric value"
                Type="Double" ControlToValidate="txtSaleRate" Operator="DataTypeCheck" CssClass="cvPCG"
                Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="leftField">
            <asp:Label ID="Label26" runat="server" Text="Sale Proceeds(Rs):" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="4">
            <asp:TextBox ID="txtSaleProceeds" runat="server" Enabled ="false" CssClass="txtField"></asp:TextBox>
            <asp:CompareValidator ID="cvSaleProceeds" runat="server" ErrorMessage="<br>Please enter a numeric value"
                Type="Double" ControlToValidate="txtSaleProceeds" Operator="DataTypeCheck" CssClass="cvPCG"
                Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2" class="leftField">
            <asp:Label ID="Label28" runat="server" Text="Remarks:" CssClass="FieldName" Font-Size="Small"></asp:Label>
        </td>
        <td colspan="4" rowspan="2" class="rightField">
            <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtField" TextMode="MultiLine"></asp:TextBox>
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
    </tr>
    <tr id="trSubmitButton" runat="server" visible="true">
        <td colspan="6" align="center">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" OnClick="btnSubmit_Click"
                onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_PortfolioPropertyEntry_btnSubmit');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_PortfolioPropertyEntry_btnSubmit');" />
        </td>
    </tr>
</table>
<%-- <asp:TextBox runat="server" ID="hdnCurrentDate" Value="01-01-2009" />--%>
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>
