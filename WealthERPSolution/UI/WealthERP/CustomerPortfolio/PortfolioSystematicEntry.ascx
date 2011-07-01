<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioSystematicEntry.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioSystematicEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<link href="../CSS/ControlsStyleSheet.css" rel="stylesheet" type="text/css" />
<link href="../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<%--Javascript Calendar Controls - Required Files--%>

<script type="text/javascript" src="../Scripts/Calender/calendar.js"></script>

<script type="text/javascript" src="../Scripts/Calender/lang/calendar-en.js"></script>

<script type="text/javascript" src="../Scripts/Calender/calendar-setup.js"></script>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<link href="../Scripts/Calender/skins/aqua/theme.css" rel="stylesheet" type="text/css" />
<%--Javascript Calendar Controls - Required Files--%>
<%--<asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>--%>
    <script type="text/javascript">
        function GetSchemeCode(source, eventArgs) {

            document.getElementById("<%= txtSchemeCode.ClientID %>").value = eventArgs.get_value();

            return false;
        };
        function GetSwitchSchemeCode(source, eventArgs) {

            document.getElementById("<%= txtSwitchSchemeCode.ClientID %>").value = eventArgs.get_value();

            return false;
        };

    </script>
<table class="TableBackground" style="width: 100%">
    <tr>
        <td colspan="3" class="rightField">
            <asp:Label ID="lblHeader" runat="server" Text="Systematic Transactions Setup" CssClass="HeaderTextBig"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="3" class="tdRequiredText">
            <label id="lbl" class="lblRequiredText">
                Note: Fields marked with a ' * ' are compulsory</label>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="leftField" width="25%">
            <asp:Label ID="lblSystematicType" runat="server" Text="Type of Systematic Trx:" CssClass="FieldName"></asp:Label>
        </td>
        <td width="50%">
            <asp:DropDownList ID="ddlSystematicType" runat="server" CssClass="cmbField" 
                onselectedindexchanged="ddlSystematicType_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="cvSystematicType" runat="server" ErrorMessage="<br />Select a Transaction Type"
                ControlToValidate="ddlSystematicType" class="rfvPCG" Operator="NotEqual"
                ValueToCompare="Select a Transaction Type" Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>
        </td>
        <td>
            <%--<asp:CompareValidator ID="cvSystematicType" runat="server" ErrorMessage="Select a Transaction Type"
                ValidationGroup="MFSubmit" ControlToValidate="ddlSystematicType" class="rfvPCG"
                Operator="NotEqual" ValueToCompare="Select" Display="Dynamic"></asp:CompareValidator>--%>
        </td>
    </tr>
    <tr>
        <td class="leftField" width="25%">
            <asp:Label ID="lblScheme" runat="server" Text="Choose Scheme:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:HiddenField ID="txtSchemeCode" runat="server" OnValueChanged="txtSchemeCode_ValueChanged" />
                                    <asp:TextBox ID="txtSearchScheme" runat="server" CssClass="txtField" AutoComplete="Off" AutoPostBack="true">
                                    </asp:TextBox>
                                    <cc1:TextBoxWatermarkExtender ID="txtSearchScheme_TextBoxWatermarkExtender"
                                            runat="server" TargetControlID="txtSearchScheme" WatermarkText="Type the Scheme Name">
                                    </cc1:TextBoxWatermarkExtender>
                                    <ajaxToolkit:AutoCompleteExtender ID="txtSearchScheme_autoCompleteExtender" runat="server"
                                        TargetControlID="txtSearchScheme" ServiceMethod="GetSchemeList" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                                        MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="5" CompletionInterval="100"
                                        CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                                        CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                                        UseContextKey="true" OnClientItemSelected="GetSchemeCode" />
                                    <span id="Span1" class="spnRequiredField">*<br />
                                    </span>
                                    <%--<span style='font-size: 8px; font-weight: normal' class='FieldName'>Enter
                                        few characters of Scheme Name.</span>--%>
        </td>
       
    </tr>
    <tr id="trSwitchScheme" visible="false" runat="server">
                
                <td class="leftField" id="tdSchemeToLabel" runat="server">
                    <asp:Label ID="lblSchemeTo" runat="server" Text="Scheme To :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" id="tdSchemeToValue" runat="server" colspan="3">
                 <asp:HiddenField ID="txtSwitchSchemeCode" runat="server" />
                                    <asp:TextBox ID="txtSwicthSchemeSearch" runat="server" CssClass="txtField" AutoComplete="Off"
                                        AutoPostBack="true"></asp:TextBox><cc1:TextBoxWatermarkExtender ID="txtSwitchSchemeCode_TextBoxWatermarkExtender"
                                            runat="server" TargetControlID="txtSwicthSchemeSearch" WatermarkText="Type the Scheme Name">
                                        </cc1:TextBoxWatermarkExtender>
                                    <ajaxToolkit:AutoCompleteExtender ID="txtSwitchSchemeCode_AutoCompleteExtender" runat="server"
                                        TargetControlID="txtSwicthSchemeSearch" ServiceMethod="GetSwitchSchemeList" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                                        MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="5" CompletionInterval="100"
                                        CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                                        CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                                        UseContextKey="true" OnClientItemSelected="GetSwitchSchemeCode" />
                                    <span id="Span7" class="spnRequiredField">*<br />
                                    </span>
                                    
                    
                </td>
            </tr>
    <tr>
            <td>&nbsp;</td>
            
            <td>
            <span style='font-size: 8px; font-weight: normal' class='FieldName'>Enter
                                        few characters of Scheme Name.</span><br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtSearchScheme"
                                        ErrorMessage="Please Enter Scheme Name" Display="Dynamic" runat="server" CssClass="rfvPCG"
                                        ValidationGroup="MFSubmit">
                                    </asp:RequiredFieldValidator>
                                       </td>
           
            </tr>
    <tr>
        <td class="leftField" width="25%">
            <asp:Label ID="lblMFAccount" runat="server" Text="Folio:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlFolioNumber" runat="server" CssClass="cmbField" >
            </asp:DropDownList>
            <span id="Span6" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="cvMFAccount" runat="server" ErrorMessage="<br />Select a Folio"
                ControlToValidate="ddlFolioNumber" class="rfvPCG" Operator="NotEqual"
                ValueToCompare="Select a Folio" Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>
        </td>
        <td>
         <asp:Button ID="btnAddFolio" runat="server" Text="AddFolio" CssClass="PCGButton" 
                onclick="btnAddFolio_Click" style="height: 26px" 
                />
        </td>
    </tr>
    <tr>
        <td class="leftField" width="25%">
            <asp:Label ID="lblStartDate" runat="server" Text="Start date:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="txtStartDate_CalendarExtender" runat="server" TargetControlID="txtStartDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtStartDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtStartDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <span id="Span9" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfv_txtStartDate" ControlToValidate="txtStartDate"
                ValidationGroup="MFSubmit" ErrorMessage="<br />Please Enter Start Date" Display="Dynamic"
                runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:CompareValidator ID="cvStartDate" runat="server" ErrorMessage="The date format should be dd/mm/yyyy"
                Type="Date" ControlToValidate="txtStartDate" Operator="DataTypeCheck" CssClass="cvPCG"
                Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trSipChequeDate" runat="server" visible="false">
       <td class="leftField" width="25%">
            <asp:Label ID="lblSipChequeDate" runat="server" Text="First SIP Cheque Date: " CssClass="FieldName"></asp:Label>
        
        </td>
        <td>
        <asp:TextBox ID="txtSipChequeDate" runat="server" CssClass="txtField"></asp:TextBox>
          <cc1:CalendarExtender ID="SipChequeDate_CalendarExtender" runat="server" TargetControlID="txtSipChequeDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="SipChequeDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtSipChequeDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <%--<span id="Span8" class="spnRequiredField">*</span>--%>
           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtSipChequeDate"
                ValidationGroup="MFSubmit" ErrorMessage="<br />Please Enter SIP Cheque Date" Display="Dynamic"
                runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>--%>
        </td>
        <td>

            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="The date format should be dd/mm/yyyy"
                Type="Date" ControlToValidate="txtSipChequeDate" Operator="DataTypeCheck" CssClass="cvPCG"
                Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
        <tr id="trSipChequeNo" runat="server" visible="false">
       <td class="leftField" width="25%">
            <asp:Label ID="lblSipChequeNo" runat="server" Text="First SIP Cheque No.: " CssClass="FieldName"></asp:Label>
        
        </td>
        <td>
        <asp:TextBox ID="txtSipChecqueNo" runat="server" CssClass="txtField"></asp:TextBox>
         <asp:CompareValidator ID="cvBankPinCode" runat="server" ErrorMessage="<br/>Enter a numeric value"
                CssClass="rfvPCG" Type="Integer" ControlToValidate="txtSipChecqueNo" ValidationGroup="btnSubmit" Operator="DataTypeCheck"
                Display="Dynamic"></asp:CompareValidator>
        </td>
        <td>
        </td>
    </tr>
    
    
            
    
    <tr id="trSystematicDateChk1" runat="server">
        <td width="25%" class="leftField">
            <asp:Label ID="lblSystematicDate" runat="server" Text="Date of Systematic Trx:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:CheckBox ID="chkDate1" Text="1" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate2" Text="2" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate3" Text="3" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate4" Text="4" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate5" Text="5" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate6" Text="6" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate7" Text="7" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate8" Text="8" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate9" Text="9" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate10" Text="10" runat="server" CssClass="cmbField" Width="40px" />
        </td>
        <td>
        </td>
    </tr>
    <tr id="trSystematicDateChk2" runat="server">
        <td>
        </td>
        <td>
            <asp:CheckBox ID="chkDate11" Text="11" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate12" Text="12" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate13" Text="13" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate14" Text="14" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate15" Text="15" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate16" Text="16" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate17" Text="17" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate18" Text="18" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate19" Text="19" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate20" Text="20" runat="server" CssClass="cmbField" Width="40px" />
        </td>
        <td>
        </td>
    </tr>
    <tr id="trSystematicDateChk3" runat="server">
        <td>
        </td>
        <td>
            <asp:CheckBox ID="chkDate21" Text="21" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate22" Text="22" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate23" Text="23" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate24" Text="24" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate25" Text="25" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate26" Text="26" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate27" Text="27" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate28" Text="28" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate29" Text="29" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate30" Text="30" runat="server" CssClass="cmbField" Width="40px" />
            <asp:CheckBox ID="chkDate31" Text="31" runat="server" CssClass="cmbField" Width="40px" />
        </td>
        <td>
        </td>
    </tr>
    <tr id="trSystematicDate" runat="server">
        <td class="leftField">
            <asp:Label ID="lblSystematicDateText" runat="server" Text="Date of Systematic Trx:"
                CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtSystematicDate" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td>
            <asp:CompareValidator ID="cvSystematicDate" runat="server" ErrorMessage="Please Enter Systematic Date Below 31"
                ValidationGroup="MFSubmit" ControlToValidate="ddlFrequency" class="rfvPCG" Operator="GreaterThan"
                ValueToCompare="31" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField" width="25%">
            <asp:Label ID="lblFrequency" runat="server" Text="Frequency of Trx:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlFrequency" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span3" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="cvFrequency" runat="server" ErrorMessage="<br />Select Frequency"
                ValidationGroup="MFSubmit" ControlToValidate="ddlFrequency" class="rfvPCG" Operator="NotEqual"
                ValueToCompare="Select Frequency" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td>
            
        </td>
    </tr>
    <tr>
        <td class="leftField" width="25%">
            <asp:Label ID="lblAmount" runat="server" Text="Amount:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
            <span id="Span4" class="spnRequiredField">*</span>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator5"  ControlToValidate="txtAmount" ErrorMessage="<br />Please Enter Amount"
                Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="MFSubmit">
            </asp:RequiredFieldValidator>
        </td>
        <td>
          <%--  <asp:RequiredFieldValidator ID="rfvAmount" ControlToValidate="txtAmount" ErrorMessage="Please Enter Amount"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>--%>
        </td>
    </tr>
    <tr>
        <td class="leftField" width="25%">
            <asp:Label ID="lblPeriod" runat="server" Text="Period of Systematic Trx:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtPeriod" runat="server" CssClass="txtField" 
                AutoPostBack="true" ValidationGroup="MFSubmit" CausesValidation="true" ontextchanged="txtPeriod_TextChanged" ></asp:TextBox>
            <span id="Span5" class="spnRequiredField">*</span>
            <asp:DropDownList ID="ddlPeriodSelection" runat="server" AutoPostBack="true" 
                CssClass="cmbField" CausesValidation="true" ValidationGroup="MFSubmit"
                onselectedindexchanged="ddlPeriodSelection_SelectedIndexChanged" >
            <%--<asp:ListItem>Select</asp:ListItem>  --%> 
            <asp:ListItem Text="Days" Value="DA"></asp:ListItem>
            <asp:ListItem Text="Months" Value="MN"></asp:ListItem>
            <asp:ListItem Text="Years" Value="YR"></asp:ListItem>
            </asp:DropDownList>
           <%-- <span id="Span8" class="spnRequiredField">*</span>--%>
            <%--<asp:Label ID="lblMonths" runat="server" Text="in Months" CssClass="txtField"></asp:Label>--%>
             <asp:RequiredFieldValidator ID="rfvPeriod" ControlToValidate="txtPeriod" ErrorMessage="<br />Please Enter a Period"
                Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="MFSubmit">
            </asp:RequiredFieldValidator>&nbsp;&nbsp;
<%--             <asp:CompareValidator ID="CompareValidator_ddlPeriodSelection" runat="server"
                ControlToValidate="ddlPeriodSelection" ErrorMessage="<br />Please select any Period Type from the Dropdown"
                Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic" ValidationGroup="MFSubmit">
            </asp:CompareValidator>--%>
        </td>
        <td>

        </td>
    </tr>
    <tr>
        <td class="leftField" width="25%">
            <asp:Label ID="lblEndDate" runat="server" Text="End Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
        <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtField"></asp:TextBox>
         <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEndDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                TargetControlID="txtEndDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
         <%--   <span id="Span8" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtStartDate"
                ValidationGroup="MFSubmit" ErrorMessage="<br />Please Enter Start Date" Display="Dynamic"
                runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>--%>
        </td>
        <td>
 <%--       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEndDate" ErrorMessage="Please select a End Date"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="The date format should be dd/mm/yyyy"
                Type="Date" ControlToValidate="txtEndDate" Operator="DataTypeCheck" CssClass="cvPCG"
                Display="Dynamic"></asp:CompareValidator>--%>
        </td>
    </tr>
        <tr>
        <td class="leftField" width="25%">
            <asp:Label ID="lblRegistrationDate" runat="server" Text="Registeration Date in R&T system: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
        <asp:TextBox ID="txtRegistrationDate" runat="server" CssClass="txtField"></asp:TextBox>
          <cc1:CalendarExtender ID="RegistrationDate_CalendarExtender" runat="server" TargetControlID="txtRegistrationDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="RegistrationDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtRegistrationDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
        </td>
        <td>
       <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtRegistrationDate" ErrorMessage="Please select a Registration Date"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="The date format should be dd/mm/yyyy"
                Type="Date" ControlToValidate="txtRegistrationDate" Operator="DataTypeCheck" CssClass="cvPCG"
                Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
     <tr id="trPaymentMode" visible="false" runat="server">
        <td class="leftField" width="25%">
            <asp:Label ID="lblPaymentMode" runat="server" Text="Payment mode: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
        <asp:HiddenField ID="hdnddlPaymentMode" runat="server" />
       <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="cmbField">
         <asp:ListItem Text="ECS" Value="ES"></asp:ListItem>
         <asp:ListItem Text="PDC" Value="PD"></asp:ListItem>
       </asp:DropDownList>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="leftField" width="25%">
        </td>
        <td>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" OnClick="btnSubmit_Click"
                onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_PortfolioPropertyEntry_btnSubmit');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_PortfolioPropertyEntry_btnSubmit');"
                ValidationGroup="MFSubmit" />
        </td>
        <td>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnddlPeriodSelection" runat="server"/>
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>