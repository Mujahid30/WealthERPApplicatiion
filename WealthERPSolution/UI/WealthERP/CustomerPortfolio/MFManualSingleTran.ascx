<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFManualSingleTran.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.MFManualSingleTran" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html>
<head>
    <META HTTP-EQUIV="CACHE-CONTROL" CONTENT="NO-CACHE">
</head>
<body>
</body>
</html>

<script type="text/javascript">
    function checkDate(sender, args) {

        var selectedDate = new Date();
        selectedDate = sender._selectedDate;

        var todayDate = new Date();
        var msg = "";

        if (selectedDate > todayDate) {
            sender._selectedDate = todayDate;
            sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            alert("Warning! - Date Cannot be in the future");
            return false;
        }

    }
    function GetSchemeCode(source, eventArgs) {

        document.getElementById("<%= txtSchemeCode.ClientID %>").value = eventArgs.get_value();

        return false;
    };
    function GetSwitchSchemeCode(source, eventArgs) {

        document.getElementById("<%= txtSwitchSchemeCode.ClientID %>").value = eventArgs.get_value();

        return false;
    };
    //****************************************************************
    

    
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<asp:UpdatePanel ID="upnlMFTrab" runat="server">
    <ContentTemplate>
        <style type="text/css">
            .style1
            {
                width: 197px;
            }
            .style2
            {
                width: 47px;
            }
        </style>
        <table width="100%" class="TableBackground">
            <tr>
                <td class="HeaderCell">
                    <asp:Label ID="lblView" runat="server" CssClass="HeaderTextBig" Text="Add MF Transaction"></asp:Label>
                    <hr />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td class="tdRequiredText">
                    <label id="lbl" class="lblRequiredText">
                        Note: Fields marked with a ' * ' are compulsory</label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <table style="width: 100%;" class="TableBackground">
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Select the Portfolio :"></asp:Label>
                </td>
                <td colspan="4" class="rightField">
                    <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="width:100%">
                <td class="leftField">
                    <asp:Label ID="lblSchemeSearch" runat="server" Text="Scheme Search :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="4">
                <asp:HiddenField ID="txtSchemeCode" runat="server" OnValueChanged="txtSchemeCode_ValueChanged" />
                                    <asp:TextBox ID="txtSearchScheme" runat="server" CssClass="txtSchemeName"  AutoComplete="Off"
                                        AutoPostBack="true"></asp:TextBox><cc1:TextBoxWatermarkExtender ID="txtSearchScheme_TextBoxWatermarkExtender"
                                            runat="server" TargetControlID="txtSearchScheme" WatermarkText="Type the Scheme Name">
                                        </cc1:TextBoxWatermarkExtender>
                                    <ajaxToolkit:AutoCompleteExtender ID="txtSearchScheme_autoCompleteExtender" runat="server"
                                        TargetControlID="txtSearchScheme" ServiceMethod="GetSchemeList" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                                        MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="5" CompletionInterval="100"
                                        CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                                        CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                                        UseContextKey="true" OnClientItemSelected="GetSchemeCode" />
                                    <span id="Span6" class="spnRequiredField">*<br />
                                    </span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtSearchScheme"
                                        ErrorMessage="Please Enter Scheme Name" Display="Dynamic" runat="server" CssClass="rfvPCG"
                                        ValidationGroup="MFSubmit">
                                    </asp:RequiredFieldValidator><span style='font-size: 8px; font-weight: normal' class='FieldName'>Enter
                                        few characters of customer name.</span>
                                        
                                 <asp:CompareValidator ID="cmpSchemeName" runat="server" ErrorMessage="<br />Please select a transaction type"
                                  ValidationGroup="MFSubmit" ControlToValidate="txtSearchScheme" Operator="NotEqual"
                                  CssClass="rfvPCG" ValueToCompare="Select" Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td style="display: run-in;">
                </td>
            </tr>
         
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label3" runat="server" Text="Transaction Type:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlTransactionType" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlTransactionType_SelectedIndexChanged">
                        <asp:ListItem>Select</asp:ListItem>
                        <asp:ListItem>Sell</asp:ListItem>
                        <asp:ListItem>Buy</asp:ListItem>
                        <asp:ListItem>Dividend Reinvestment</asp:ListItem>
                        <asp:ListItem>SIP</asp:ListItem>
                        <asp:ListItem>SWP</asp:ListItem>
                        <asp:ListItem>STP</asp:ListItem>
                        <asp:ListItem>Dividend Payout</asp:ListItem>
                        <asp:ListItem>Switch</asp:ListItem>
                        <asp:ListItem>Holdings</asp:ListItem>
                    </asp:DropDownList>
                    <span id="Span2" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />Please select a transaction type"
                        ValidationGroup="MFSubmit" ControlToValidate="ddlTransactionType" Operator="NotEqual"
                        CssClass="rfvPCG" ValueToCompare="lblScheme" Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label4" runat="server" Text="Folio Number :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlFolioNum" runat="server" CssClass="cmbField" AutoPostBack="true">
                    </asp:DropDownList>
                    <span id="Span3" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />Please select a folio number"
                        ValidationGroup="MFSubmit" ControlToValidate="ddlFolioNum" Operator="NotEqual"
                        CssClass="rfvPCG" ValueToCompare="Select a Folio Number" Display="Dynamic"></asp:CompareValidator>
                </td>
                <td class="style2">
                </td>
                <td class="style1">
                </td>
                <td>
                    <asp:Button ID="btnNewFolioAdd" runat="server" Text="Add New Folio Number" CssClass="PCGLongButton"
                        OnClick="btnNewFolioAdd_Click" Height="26px" Width="167px" />
                </td>
            </tr>
            <%--From here there is lots of work to do--%>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="leftField" id="tdSchemeNameLabel" runat="server">
                    <asp:Label ID="lblSchemeName" runat="server" CssClass="FieldName" Text="Scheme Name :"></asp:Label>
                </td>
                <td class="rightField" id="tdSchemeNameValue" runat="server" colspan="3">
                    <asp:Label ID="lblScheme" runat="server" Text="Label" CssClass="Field"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label6" runat="server" Text="Transaction Date :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtTransactionDate" runat="server" CssClass="txtField" OnTextChanged="txtTransactionDate_TextChanged"
                        AutoPostBack="true"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="txtTransactionDate_CalendarExtender" runat="server"
                        Format="dd/MM/yyyy" TargetControlID="txtTransactionDate" OnClientDateSelectionChanged="checkDate">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="txtTransactionDate_TextBoxWatermarkExtender"
                        runat="server" TargetControlID="txtTransactionDate" WatermarkText="dd/mm/yyyy">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                    <span id="Span4" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTransactionDate"
                        CssClass="rfvPCG" ValidationGroup="MFSubmit" ErrorMessage="<br />Please select a Transaction Date"
                        Display="Dynamic" runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>
                    <%--<asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="<br />The date format should be dd/MM/yyyy"
                ValidationGroup="MFSubmit" Type="Date" ControlToValidate="txtTransactionDate"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>--%>
                </td>
                <td class="style2">
                    <asp:Label ID="Label19" runat="server" Text="NAV :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtNAV" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnUseNAV" runat="server" Text="Use NAV" CssClass="PCGMediumButton"
                        OnClick="btnUseNAV_Click" />
                </td>
            </tr>
            <tr id="trDividentRate" runat="server">
                <td class="leftField">
                    <asp:Label ID="Label7" runat="server" Text="Dividend Rate(%) :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtDividentRate" runat="server" CssClass="txtField"></asp:TextBox>
                    
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtDividentRate"
                        CssClass="rfvPCG" ValidationGroup="MFSubmit" ErrorMessage="<br />Please enter a Dividend Rate"
                        Display="Dynamic" runat="server" InitialValue="">--%>
                   <%-- </asp:RequiredFieldValidator>--%>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="<br />Enter a numeric value"
                        CssClass="rfvPCG" ValidationGroup="MFSubmit" Type="Double" ControlToValidate="txtDividentRate"
                        Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td class="leftField" id="tdSchemeToLabel" runat="server" colspan="2">
                    <asp:Label ID="lblSchemeTo" runat="server" Text="Scheme To :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" id="tdSchemeToValue" runat="server" colspan="2" >
                 <asp:HiddenField ID="txtSwitchSchemeCode" runat="server" 
                        onvaluechanged="txtSwitchSchemeCode_ValueChanged" />
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
                                    <span id="Span1" class="spnRequiredField">*<br />
                                    </span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtSwicthSchemeSearch"
                                        ErrorMessage="Please Enter Scheme Name" Display="Dynamic" runat="server" CssClass="rfvPCG"
                                        ValidationGroup="MFSubmit">
                                    </asp:RequiredFieldValidator><span style='font-size: 8px; font-weight: normal' class='FieldName'>Enter
                                        few characters of customer name.</span>
                    
                </td>
            </tr>
            <tr runat="server" id="trPrice">
                <td class="leftField">
                    <asp:Label ID="Label18" runat="server" Text="Price :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtPrice" runat="server" CssClass="txtField" MaxLength="18"></asp:TextBox>
                    <span id="Span8" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtPrice"
                        ValidationGroup="MFSubmit" ErrorMessage="<br />Please enter a Price" Display="Dynamic"
                        CssClass="rfvPCG" runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtPrice"
                        CssClass="rfvPCG" ValidationGroup="MFSubmit" Display="Dynamic" runat="server" ErrorMessage="Not acceptable format"
                        ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                </td>
                </tr>
                <tr>
                <td class="leftField" id="tdNAVPurchasedLabel" runat="server" colspan="2">
                    <asp:Label ID="lblNAVPurchased" runat="server" Text="NAV of Scheme Purchased into :"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" id="tdNAVPurchasedValue" runat="server">
                    <asp:TextBox ID="txtNAVPurchased" runat="server" CssClass="txtField" MaxLength="18"></asp:TextBox>
                    <span id="Span9" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtNAVPurchased"
                        CssClass="rfvPCG" ValidationGroup="MFSubmit" ErrorMessage="<br />Please enter the purchased NAV"
                        Display="Dynamic" runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtNAVPurchased"
                        CssClass="rfvPCG" ValidationGroup="MFSubmit" Display="Dynamic" runat="server" ErrorMessage="Not acceptable format"
                        ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                </td>
                <td class="rightfield" runat="server" id="tdSwitchUseNAV" visible="false">                   
                 <asp:Button ID="btnSwitchNAV" runat="server" Text="Use NAV" 
                        CssClass="PCGMediumButton" onclick="btnSwitchNAV_Click"
                        />
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label9" runat="server" Text="Amount :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField" OnTextChanged="txtAmount_TextChanged"
                        MaxLength="18" AutoPostBack="true"></asp:TextBox>
                    <span id="Span10" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtAmount"
                        CssClass="rfvPCG" ValidationGroup="MFSubmit"  ErrorMessage="<br />Please enter an Amount"
                        Display="Dynamic" runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtAmount"
                        CssClass="rfvPCG" ValidationGroup="MFSubmit" Display="Dynamic" runat="server" ErrorMessage="Not acceptable format"
                        ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                </td>
                <td class="leftField" id="tdPricePurchasedLabel" runat="server" colspan="2">
                    <asp:Label ID="lblPricePurchased" runat="server" Text="Price of Scheme Purchased into :"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" id="tdPricePurchasedValue" runat="server" colspan="2">
                    <asp:TextBox ID="txtPricePurchased" runat="server" CssClass="txtField" MaxLength="18"></asp:TextBox>
                    <span id="Span11" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtPricePurchased"
                        CssClass="rfvPCG" ValidationGroup="MFSubmit" ErrorMessage="<br />Please enter the purchased price"
                        Display="Dynamic" runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtPricePurchased"
                        CssClass="rfvPCG" ValidationGroup="MFSubmit" Display="Dynamic" runat="server" ErrorMessage="Not acceptable format"
                        ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField" id="tdUnitsLabel" runat="server">
                    <asp:Label ID="Label10" runat="server" Text="Units :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" id="tdUnitsValue" runat="server">
                    <asp:TextBox ID="txtUnits" runat="server" CssClass="txtField" OnTextChanged="txtUnits_TextChanged"
                        MaxLength="18" AutoPostBack="true"></asp:TextBox>
                    <span id="Span12" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtUnits"
                        CssClass="rfvPCG" ValidationGroup="MFSubmit" ErrorMessage="<br />Please enter the number of units"
                        Display="Dynamic" runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="txtUnits"
                        CssClass="rfvPCG" ValidationGroup="MFSubmit" Display="Dynamic" runat="server" ErrorMessage="Not acceptable format"
                        ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                </td>
               <td class="leftField" id="tdAmtPurchasedLabel" runat="server" colspan="2">
                    <asp:Label ID="lblAmtPurchased" runat="server" Text="Amount :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" id="tdAmtPurchasedValue" runat="server" colspan="2">
                    <asp:TextBox ID="txtAmtPurchased" runat="server" CssClass="txtField" 
                        MaxLength="18" ontextchanged="txtAmtPurchased_TextChanged" AutoPostBack="true"></asp:TextBox>
                    <span id="Span15" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="txtAmtPurchased"
                        CssClass="rfvPCG" ValidationGroup="MFSubmit" ErrorMessage="<br />Please enter the Amount"
                        Display="Dynamic" runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ControlToValidate="txtAmtPurchased"
                        CssClass="rfvPCG" ValidationGroup="MFSubmit" Display="Dynamic" runat="server" ErrorMessage="Not acceptable format"
                        ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField" id="tdSTTLabel" runat="server">
                    <asp:Label ID="Label11" runat="server" Text="STT :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" id="tdSTTValue" runat="server">
                    <asp:TextBox ID="txtSTT" runat="server" CssClass="txtField" MaxLength="18"></asp:TextBox>
                    <span id="Span14" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtSTT"
                        CssClass="rfvPCG" ValidationGroup="MFSubmit" ErrorMessage="<br />Please enter the STT"
                        Display="Dynamic" runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ControlToValidate="txtSTT"
                        CssClass="rfvPCG" ValidationGroup="MFSubmit" Display="Dynamic" runat="server" ErrorMessage="Not acceptable format"
                        ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                </td>
                 <td class="leftField" id="tdUnitsAllotedLabel" runat="server" colspan="2">
                    <asp:Label ID="lblUnitsAlloted" runat="server" Text="Units Allotted :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" id="tdUnitsAllotedValue" runat="server"  colspan="2">
                    <asp:TextBox ID="txtUnitsAlloted" runat="server" CssClass="txtField" MaxLength="18"></asp:TextBox>
                    <span id="Span13" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator50" ControlToValidate="txtUnitsAlloted"
                        CssClass="rfvPCG" ValidationGroup="MFSubmit" ErrorMessage="<br />Please enter the units alloted"
                        Display="Dynamic" runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ValidationGroup="MFSubmit" ControlToValidate="txtUnitsAlloted"
                        CssClass="rfvPCG" Display="Dynamic" runat="server" ErrorMessage="Not acceptable format"
                        ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                </td>
                
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="SubmitCell" colspan="5">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_MFManualSingleTran_btnSubmit', 'S');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_MFManualSingleTran_btnSubmit', 'S');"
                        ValidationGroup="MFSubmit" OnClick="btnSubmit_Click" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
