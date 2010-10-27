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
        </td>
        <td>
            <asp:CompareValidator ID="cvSystematicType" runat="server" ErrorMessage="Select a Transaction Type"
                ValidationGroup="MFSubmit" ControlToValidate="ddlSystematicType" class="rfvPCG"
                Operator="NotEqual" ValueToCompare="Select" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField" width="25%">
            <asp:Label ID="lblMFAccount" runat="server" Text="Folio:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlMFAccount" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span6" class="spnRequiredField">*</span>
        </td>
        <td>
            <asp:CompareValidator ID="cvMFAccount" runat="server" ErrorMessage="Select a Folio"
                ValidationGroup="MFSubmit" ControlToValidate="ddlMFAccount" class="rfvPCG" Operator="NotEqual"
                ValueToCompare="Select" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField" width="25%">
            <asp:Label ID="lblScheme" runat="server" Text="Choose Scheme:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:HiddenField ID="txtSchemeCode" runat="server" OnValueChanged="txtSchemeCode_ValueChanged" />
                                    <asp:TextBox ID="txtSearchScheme" runat="server" CssClass="txtField" AutoComplete="Off"
                                        AutoPostBack="true"></asp:TextBox><cc1:TextBoxWatermarkExtender ID="txtSearchScheme_TextBoxWatermarkExtender"
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
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtSearchScheme"
                                        ErrorMessage="Please Enter Scheme Name" Display="Dynamic" runat="server" CssClass="rfvPCG"
                                        ValidationGroup="MFSubmit">
                                    </asp:RequiredFieldValidator><span style='font-size: 8px; font-weight: normal' class='FieldName'>Enter
                                        few characters of customer name.</span>
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
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtSwicthSchemeSearch"
                                        ErrorMessage="Please Enter Scheme Name" Display="Dynamic" runat="server" CssClass="rfvPCG"
                                        ValidationGroup="MFSubmit">
                                    </asp:RequiredFieldValidator><span style='font-size: 8px; font-weight: normal' class='FieldName'>Enter
                                        few characters of customer name.</span>
                    
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
            <asp:RequiredFieldValidator ID="rfvStartDate" ControlToValidate="txtStartDate" ErrorMessage="Please select a Start Date"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvStartDate" runat="server" ErrorMessage="The date format should be dd/mm/yyyy"
                Type="Date" ControlToValidate="txtStartDate" Operator="DataTypeCheck" CssClass="cvPCG"
                Display="Dynamic"></asp:CompareValidator>
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
        </td>
        <td>
            <asp:CompareValidator ID="cvFrequency" runat="server" ErrorMessage="Select Frequency"
                ValidationGroup="MFSubmit" ControlToValidate="ddlFrequency" class="rfvPCG" Operator="NotEqual"
                ValueToCompare="Select Frequency" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField" width="25%">
            <asp:Label ID="lblAmount" runat="server" Text="Amount:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span4" class="spnRequiredField">*</span>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="rfvAmount" ControlToValidate="txtAmount" ErrorMessage="Please Enter Amount"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField" width="25%">
            <asp:Label ID="lblPeriod" runat="server" Text="Period of Systematic Trx:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtPeriod" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span5" class="spnRequiredField">*</span>
            <asp:Label ID="lblMonths" runat="server" Text="in Months" CssClass="txtField"></asp:Label>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="rfvPeriod" ControlToValidate="txtPeriod" ErrorMessage="Please Enter a Period"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField" width="25%">
        </td>
        <td>
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
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>
