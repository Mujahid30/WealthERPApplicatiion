<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioSystematicEntry.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioSystematicEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<link href="../CSS/ControlsStyleSheet.css" rel="stylesheet" type="text/css" />
<link href="../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function() {
        $(".flip").click(function() { $(".panel").slideToggle(); });
    });
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<%--Javascript Calendar Controls - Required Files--%>




   
  
<script type="text/javascript">
    //    $(document).ready(function() {
    //        $('.loadme').click(function() {
    //            $(".loadmediv").colorbox({ width: "240px",overlayClose:false, inline: true, open: true, href: "#LoadImage" });
    //        });
    //    });


    //   
    //    var chkArray = panel.getElementsByTagName("input");
    //    var checked = 0;
    //    for (var i = 0; i < chkArray.length; i++) {
    //        if (chkArray[i].type == "checkbox" && chkArray[i].checked == true) {
    //            checked = 1;
    //            break;
    //        }
    //    }

    $(document).ready(function() {
        $('.loadme').click(function() {

            for (var i = 0; i < chkArray1.length; i++) {
                if (chkArray1[i].type == "checkbox" && chkArray1[i].checked == true) {
                    checked1 = 1;
                    break;
                }
            }

            for (var i = 0; i < chkArray2.length; i++) {
                if (chkArray2[i].type == "checkbox" && chkArray2[i].checked == true) {
                    checked2 = 1;
                    break;
                }
            }

            for (var i = 0; i < chkArray3.length; i++) {
                if (chkArray3[i].type == "checkbox" && chkArray3[i].checked == true) {
                    checked3 = 1;
                    break;
                }
            }

            if (checked1 == 0 && checked2 == 0 && checked3 == 0) {
                alert('Please choose Atleast one Systematic Date');
                return false;
            }
            else {
                $(".loadmediv").colorbox({ width: "260px", overlayClose: false, inline: true, open: true, href: "#LoadImage" });
            }
        });

    });


    
</script>

<script type="text/javascript" src="../Scripts/Calender/calendar.js"></script>

<script type="text/javascript" src="../Scripts/Calender/lang/calendar-en.js"></script>

<script type="text/javascript" src="../Scripts/Calender/calendar-setup.js"></script>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<link href="../Scripts/Calender/skins/aqua/theme.css" rel="stylesheet" type="text/css" />
<%--Javascript Calendar Controls - Required Files--%>
<%--<asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>--%>

<script type="text/javascript">
    function ChkForMainPortFolio(source, args) {

        var hdnIsCustomerLogin = document.getElementById('ctrl_PortfolioSystematicEntry_hdnIsCustomerLogin').value;
        var hdnIsMainPortfolio = document.getElementById('ctrl_PortfolioSystematicEntry_hdnIsMainPortfolio').value;

        if (hdnIsCustomerLogin == "Customer" && hdnIsMainPortfolio == "1") {

            args.IsValid = false;
        }
        else {
            args.IsValid = true;
        }

    }    
</script>

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


    
    <script type="text/javascript">
        function checkDate(sender, args) {
                   var toDate = new Date();
           var selectedDate = sender._selectedDate;

            toDate.setMinutes(0);
            toDate.setSeconds(0);
            toDate.setHours(0);
            toDate.setMilliseconds(0);

            selectedDate.setMinutes(0);
            selectedDate.setSeconds(0);
            selectedDate.setHours(0);
            selectedDate.setMilliseconds(0);
            if (!(selectedDate > toDate)) {
                if (!(selectedDate < toDate)) {

                    alert("You can not select Today Date!");
                    selectedDate.setDate(selectedDate.getDate() + 1);
                    sender._textbox.set_Value(sender._selectedDate.format(sender._format))
                }
            }
            
          

        }
    
    </script>
    
 
    
    
   

<table class="TableBackground" style="width: 100%">
    <tr>
        <td colspan="3">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblHeader" runat="server" Text="Systematic Transactions Setup"></asp:Label>
                        </td>
                         <td align="right">
                            <asp:LinkButton ID="lnkEdit" Visible="false" runat="server" CssClass="LinkButtons"
                                OnClick="lnkEdit_Click">Edit</asp:LinkButton>
                        </td>
                        <td align="right" colspan="2">
                            <img src="../Images/helpImage.png" height="15px" width="20px" style="float: right;"
                                class="flip" />
                        </td>
                    </tr>
                </table>
            </div>
        </td>
        <%--<td colspan="3" class="rightField">
            <asp:Label ID="lblHeader" runat="server" Text="Systematic Transactions Setup" CssClass="HeaderTextBig"></asp:Label>
            <hr />
        </td>--%>
    </tr>
    <tr>
        <td colspan="3">
            <div class="panel">
                <p>
                    Note: Fields marked with a ' * ' are compulsory
                </p>
            </div>
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
            <asp:Label ID="lblPortfolio" runat="server" Text="Portfolio:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlportfolio" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlportfolio_SelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
            <br />
            <asp:CustomValidator ID="cvCheckForManageOrUnManage" runat="server" ValidationGroup="MFSubmit"
                Display="Dynamic" ClientValidationFunction="ChkForMainPortFolio" CssClass="revPCG"
                ErrorMessage="CustomValidator">Permisssion denied for Manage Portfolio !!</asp:CustomValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="leftField" width="25%">
            <asp:Label ID="lblSystematicType" runat="server" Text="Type of Systematic Trx:" CssClass="FieldName"></asp:Label>
        </td>
        <td width="50%">
            <asp:DropDownList ID="ddlSystematicType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlSystematicType_SelectedIndexChanged"
                AutoPostBack="true" >
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="cvSystematicType" runat="server" ErrorMessage="<br />Select a Transaction Type"
                ControlToValidate="ddlSystematicType" class="rfvPCG" Operator="NotEqual" ValueToCompare="Select"
                Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>
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
            <asp:TextBox ID="txtSearchScheme" runat="server" Style="width: 500px;" CssClass="txtField"
                AutoComplete="Off" AutoPostBack="true">
            </asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtSearchScheme_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtSearchScheme" WatermarkText="Type the Scheme Name">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtSearchScheme_autoCompleteExtender" runat="server"
                TargetControlID="txtSearchScheme" ServiceMethod="GetInvestorScheme" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="true" OnClientItemSelected="GetSchemeCode" />
            <span id="Span1" class="spnRequiredField">*<br />
            </span>
            <asp:RequiredFieldValidator ID="rfvtxtSearchScheme" ControlToValidate="txtSearchScheme"
                ErrorMessage="<br />Please select a Scheme" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="MFSubmit">
            </asp:RequiredFieldValidator>
            <%--<span style='font-size: 8px; font-weight: normal' class='FieldName'>Enter
                                        few characters of Scheme Name.</span>--%>
        </td>
    </tr>
    <tr id="trSwitchScheme" visible="false" runat="server">
        <td class="leftField" id="tdSchemeToLabel" runat="server" valign="top">
            <asp:Label ID="lblSchemeTo" runat="server" Text="Scheme To :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" id="tdSchemeToValue" runat="server" colspan="3">
            <asp:HiddenField ID="txtSwitchSchemeCode" runat="server" />
            <asp:TextBox ID="txtSwicthSchemeSearch" runat="server" CssClass="txtField" AutoComplete="Off"
                Style="width: 500px;" AutoPostBack="true">
            </asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtSwitchSchemeCode_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtSwicthSchemeSearch" WatermarkText="Type the Scheme Name">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtSwitchSchemeCode_AutoCompleteExtender" runat="server"
                TargetControlID="txtSwicthSchemeSearch" ServiceMethod="GetSwitchSchemeList" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="true" OnClientItemSelected="GetSwitchSchemeCode" />
            <span id="Span7" class="spnRequiredField">*<br />
            </span>
            <asp:RequiredFieldValidator ID="rfvtxtSwicthSchemeSearch" ControlToValidate="txtSwicthSchemeSearch"
                ErrorMessage="<br />Please select a Scheme" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="MFSubmit">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <span style='font-size: 8px; font-weight: normal' class='FieldName'>Enter few characters
                of Scheme Name.</span><br />
        </td>
    </tr>
    <tr>
        <td class="leftField" width="25%">
            <asp:Label ID="lblMFAccount" runat="server" Text="Folio:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlFolioNumber"  runat="server" CssClass="cmbField">
            </asp:DropDownList>
           <span id="Span6" class="spnRequiredField">* &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
            <asp:Button ID="btnAddFolio" runat="server" Text="AddFolio" CssClass="PCGButton"
                CausesValidation="false" OnClick="btnAddFolio_Click" Style="height: 26px" />
            <asp:CompareValidator ID="cvMFAccount" runat="server" ErrorMessage="<br />Select a Folio"
                ControlToValidate="ddlFolioNumber" class="rfvPCG" Operator="NotEqual" ValueToCompare="Select"
                Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>
        </td>
        <td>
            <%--   <asp:Button ID="btnAddFolio" runat="server" Text="AddFolio" CssClass="PCGButton" CausesValidation=false
                onclick="btnAddFolio_Click" style="height: 26px" 
                />--%>
        </td>
    </tr>
    <tr>
        <td class="leftField" width="25%" valign="top">
            <asp:Label ID="lblStartDate" runat="server" Text="Start date:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtField" AutoPostBack="true"  
                OnTextChanged="txtStartDate_TextChanged"  ></asp:TextBox>
                
                   
      
            <cc1:CalendarExtender ID="txtStartDate_CalendarExtender" runat="server" TargetControlID="txtStartDate"  Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate"  >
            </cc1:CalendarExtender>
            
      
            <cc1:TextBoxWatermarkExtender ID="txtStartDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtStartDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <span id="Span9" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfv_txtStartDate" ControlToValidate="txtStartDate"
                ValidationGroup="MFSubmit" ErrorMessage="<br />Please Enter Start Date" Display="Dynamic"
                runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvStartDate" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                Type="Date" ControlToValidate="txtStartDate" Operator="DataTypeCheck" CssClass="cvPCG"  ValueToCompare="" 
                Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr id="trSipChequeDate" runat="server" visible="false">
        <td class="leftField" width="25%">
            <asp:Label ID="lblSipChequeDate" runat="server" Text="First SIP Cheque Date: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtSipChequeDate" runat="server" CssClass="txtField" AutoPostBack="true"></asp:TextBox>
            <cc1:CalendarExtender ID="SipChequeDate_CalendarExtender" runat="server" TargetControlID="txtSipChequeDate"
                Format="dd/MM/yyyy" >
            </cc1:CalendarExtender>

            <cc1:TextBoxWatermarkExtender ID="SipChequeDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtSipChequeDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                Type="Date" ControlToValidate="txtSipChequeDate" Operator="DataTypeCheck" CssClass="cvPCG"
                Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>
                 <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="SipChequeDate should be greater than start date"
                    Type="Date" ControlToValidate="txtSipChequeDate" ControlToCompare="txtStartDate" ValidationGroup="MFSubmit"
                    Operator="GreaterThanEqual" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
            <%--<span id="Span8" class="spnRequiredField">*</span>--%>
            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtSipChequeDate"
                ValidationGroup="MFSubmit" ErrorMessage="<br />Please Enter SIP Cheque Date" Display="Dynamic"
                runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>--%>
        </td>
        <td>
        </td>
    </tr>
    <tr id="trSipChequeNo" runat="server" visible="false">
        <td class="leftField" width="25%">
            <asp:Label ID="lblSipChequeNo" runat="server" Text="First SIP Cheque No.: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtSipChecqueNo" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
            <asp:CompareValidator ID="cvBankPinCode" runat="server" ErrorMessage="<br/>Enter a numeric value"
                CssClass="rfvPCG" Type="Integer" ControlToValidate="txtSipChecqueNo" ValidationGroup="MFSubmit"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td>
        </td>
    </tr>
         <tr>
        <td width="25%" class="leftField">
             <asp:CheckBox ID="chkHistoricalCreated"   runat="server" CssClass="cmbFielde" Width="15px" onclick="this.checked = !this.checked"  ></asp:CheckBox>
    <asp:Label ID="lblHistoricalCreated" runat="server" Text="HistoricalCreated" CssClass="FieldName" ></asp:Label>
     
    </td>
    
    <td width="25%" >
     <asp:CheckBox ID="chkAutoTransaction" runat="server" CssClass="cmbFielde" Width="15px" ></asp:CheckBox>
  
    <asp:Label ID="lblAutoTransaction" runat="server" Text="AutoTransaction" CssClass="FieldName" ></asp:Label>
 
   </td>
    </tr>
    <tr>
        <td class="leftField" width="25%">
            <asp:Label ID="lblFrequency" runat="server" Text="Frequency:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlFrequency" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span3" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="cvFrequency" runat="server" ErrorMessage="<br />Select Frequency"
                ValidationGroup="MFSubmit" ControlToValidate="ddlFrequency" class="rfvPCG" Operator="NotEqual"
                ValueToCompare="Select" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="leftField" width="25%">
            <asp:Label ID="lblAmount" runat="server" Text="Amount:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField" MaxLength="10" ></asp:TextBox>
            <span id="Span4" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtAmount"
                ErrorMessage="<br />Please Enter Amount" Display="Dynamic" runat="server" CssClass="rfvPCG"
                ValidationGroup="MFSubmit">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cmvtxtAmount" runat="server" ControlToValidate="txtAmount"
                ErrorMessage="<br />Enter a numeric Value" Display="Dynamic" Operator="DataTypeCheck"
                Type="Double" ValidationGroup="MFSubmit" CssClass="rfvPCG">
            </asp:CompareValidator>
        </td>
        <td>
            <%--  <asp:RequiredFieldValidator ID="rfvAmount" ControlToValidate="txtAmount" ErrorMessage="Please Enter Amount"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>--%>
        </td>
    </tr>
    <tr>
        <td class="leftField" width="25%">
            <asp:Label ID="lblPeriod" runat="server" Text="Tenure:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtPeriod" runat="server" CssClass="txtField" AutoPostBack="true"
                ValidationGroup="MFSubmit" CausesValidation="true" OnTextChanged="txtPeriod_TextChanged"></asp:TextBox>
            <span id="Span5" class="spnRequiredField">*</span>
            <asp:DropDownList ID="ddlPeriodSelection" runat="server" AutoPostBack="true" CssClass="cmbField"
                CausesValidation="true" ValidationGroup="MFSubmit" OnSelectedIndexChanged="ddlPeriodSelection_SelectedIndexChanged">
                <%--<asp:ListItem>Select</asp:ListItem>  --%>
                <asp:ListItem Text="Days" Value="DA"></asp:ListItem>
                <asp:ListItem Text="Months" Value="MN"></asp:ListItem>
                <asp:ListItem Text="Years" Value="YR"></asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="lblUnits" runat="server" Text="&nbsp;&nbsp;(Units)" CssClass="FieldName"></asp:Label>
            <%-- <span id="Span8" class="spnRequiredField">*</span>--%>
            <%--<asp:Label ID="lblMonths" runat="server" Text="in Months" CssClass="txtField"></asp:Label>--%>
            <asp:RequiredFieldValidator ID="rfvPeriod" ControlToValidate="txtPeriod" ErrorMessage="<br />Please Enter a Period"
                Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="MFSubmit">
            </asp:RequiredFieldValidator>&nbsp;&nbsp;
            <asp:CompareValidator ID="CompareValidator_txtPeriod" runat="server" ControlToValidate="txtPeriod"
                ErrorMessage="<br />Please Enter a numeric Value" Operator="DataTypeCheck" Type="Integer"
                ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic" ValidationGroup="MFSubmit">
            </asp:CompareValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPeriod"
                ErrorMessage="<br />Please update the  value" Operator="GreaterThan" Type="Integer"
                ValueToCompare="0" CssClass="cvPCG" Display="Dynamic" ValidationGroup="MFSubmit">
            </asp:CompareValidator>
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
             
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtEndDate"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEndDate"
                ErrorMessage="<br />Please select a End Date" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvEndDate" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                Type="Date" ControlToValidate="txtEndDate" Operator="DataTypeCheck" CssClass="cvPCG"
                Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>
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
            <asp:Label ID="lblRegistrationDate" runat="server" Text="Registration Date in R&T system: "
                CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtRegistrationDate" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="RegistrationDate_CalendarExtender" runat="server" TargetControlID="txtRegistrationDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="RegistrationDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtRegistrationDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                Type="Date" ControlToValidate="txtRegistrationDate" Operator="DataTypeCheck"
                CssClass="cvPCG" ValidationGroup="MFSubmit" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td>
            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtRegistrationDate" ErrorMessage="Please select a Registration Date"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>--%>
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
    <tr id="trCeaseDate" runat="server">
        <td class="leftField" width="25%">
            <asp:Label ID="lblCeaseDate" runat="server" Text="Stopped Date: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtCeaseDate" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="CeaseDate_CalendarExtender" runat="server" TargetControlID="txtCeaseDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="CeaseDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtCeaseDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                Type="Date" ControlToValidate="txtCeaseDate" Operator="DataTypeCheck" SipChequeDate_CalendarExtender="cvPCG"
                Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr id="trRemarks" runat="server">
        <td class="leftField" width="25%">
            <asp:Label ID="Label2" runat="server" Text="Remarks: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtField" TextMode="MultiLine"></asp:TextBox>
        </td>
        <td>
        </td>
    </tr>
    <tr id="trSubBroker" runat="server">
        <td class="leftField" width="25%">
            <asp:Label ID="lblSubbroker" runat="server" Text="SubBrokerCode: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtSubbrokerCode" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
        </td>
        <td>
        </td>
    </tr>
    <tr id="trSipAutoTranx" runat="server">
        <td width="25%" class="leftField">
            <asp:Label ID="Label1" runat="server" Text="Enable Auto Transaction Creation:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:CheckBox ID="SipAutoTranx" Text="" runat="server" CssClass="cmbField" Width="40px" />
        </td>
    </tr>
    <tr>
        <td class="leftField" width="25%">
        </td>
        <td>
           
           <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="loadme PCGButton" ValidationGroup="MFSubmit" 
                OnClick="btnSubmit_Click" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_PortfolioPropertyEntry_btnSubmit');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_PortfolioPropertyEntry_btnSubmit');"  />
             
            
             
        </td>
        <td>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnddlPeriodSelection" runat="server" />
<asp:HiddenField ID="hdnIsMainPortfolio" runat="server" />
<asp:HiddenField ID="hdnIsCustomerLogin" runat="server" />
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>