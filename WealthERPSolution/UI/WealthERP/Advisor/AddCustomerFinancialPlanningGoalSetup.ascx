<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddCustomerFinancialPlanningGoalSetup.ascx.cs"
    Inherits="WealthERP.Advisor.AddCustomerFinancialPlanningGoalSetup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script type="text/javascript">

    //
    function SetROI() {
        if (document.getElementById('<%=txtCurrentInvestPurpose.ClientID %>').value > 0)
            document.getElementById('<%=txtAboveRateOfInterst.ClientID %>').value = "8.5";

    };


    function validate() {        
        var goalCost = document.getElementById("<%=txtGoalCostToday.ClientID %>").value;
        var RetirementCorps = "-";
        if (document.getElementById("<%=txtROIFutureInvest.ClientID %>") != null)
            RetirementCorps = document.getElementById("<%=txtROIFutureInvest.ClientID %>").value;



//        if (parseInt(CurrentInvestPurpose) == 0 && parseInt(aboveRateOfInterest) > 0) {
//            alert("Fill correct investment amount")
//            return false;
        //        }
        if (document.getElementById('<%=ddlGoalType.ClientID %>').value == "0") {
            alert("Please Select a Goal")
            document.getElementById('<%=ddlGoalType.ClientID %>').focus();
            return false;
        }

        if (document.getElementById('<%=txtInflation.ClientID %>').value == "") {
            alert("Inflation(%) cannot be empty.")
            document.getElementById('<%=txtInflation.ClientID %>').focus();
            return false;
        }

        if (document.getElementById('<%=txtExpRateOfReturn.ClientID %>').value == "") {
            alert("Expected Rate of Return on new investment(%) cannot be empty.")
            document.getElementById('<%=txtExpRateOfReturn.ClientID %>').focus();
            return false;
        }

        if (document.getElementById('<%=txtGoalCostToday.ClientID %>').value == "") {
            alert("Cost today cannot be empty.")
            document.getElementById('<%=txtGoalCostToday.ClientID %>').focus();
            return false;
        }
        else if (document.getElementById('<%=txtGoalCostToday.ClientID %>').value == "0") {
            alert("Cost today cannot be 0.")
            document.getElementById('<%=txtGoalCostToday.ClientID %>').focus();
            return false;
        }
        if (RetirementCorps == "") {

            alert("Return on retirement corpus(%) not to be empty ")
            return false;
        }
        else if (parseInt(RetirementCorps) == 0) {
            alert("Return on retirement corpus(%) should be greater than 0 ")
            return false;
        }




        if (goalCost == "") {
            document.getElementById("<%=txtGoalCostToday.ClientID %>").focus();
            alert("Please enter Goal Cost Today");
            return false;
        }
        else
            return true;

    }
 
    function checkDate(sender, args) {

        var selectedDate = new Date();
        selectedDate = sender._selectedDate;

        var todayDate = new Date();
        var msg = "";

        if (selectedDate > todayDate) {
            sender._selectedDate = todayDate;
            sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            alert("Warning! - Date Cannot be in the future");
        }
    };



    function onCALShown() {
        var cal = $find("calendar1");

        //Setting the default mode to month

        cal._switchMode("years", true);


        //Iterate every month Item and attach click event to it

    };

    //Numeric Test
    function IsNumeric() //  check for valid numeric strings
    {
        alert("here");
        var Stringvalue = document.getElementById("<%= txtGoalCostToday.ClientID %>").value;
        if (!/\D/.test(Stringvalue)) return true; //IF NUMBER
        else if (/^\d+\.\d+$/.test(Stringvalue)) return true; //IF A DECIMAL NUMBER HAVING AN INTEGER ON EITHER SIDE OF THE DOT(.)
        alert("Warning! - Must be a Number or Numeric value");
    };
    function ShowImage() {
        document.getElementById('txtPickCustomer')
      .style.backgroundImage = 'url(\Images\PCGAjax-loader.gif)';

        document.getElementById('txtPickCustomer')
                    .style.backgroundRepeat = 'no-repeat';

        document.getElementById('txtPickCustomer')
                    .style.backgroundPosition = 'right';
    };
    function HideImage() {
        document.getElementById('txtPickCustomer')
                      .style.backgroundImage = 'none';
    };

    function DeleteConfirmation() {
        var panel = document.getElementById('<%= tpnlOutput.ClientID %>');
        var chkArray = panel.getElementsByTagName("input");
        var checked = 0;
        for (var i = 0; i < chkArray.length; i++) {
            if (chkArray[i].type == "checkbox" && chkArray[i].checked == true) {
                checked = 1;
                break;
            }
        }
        if (checked == 1) {
            if (confirm("Are you sure you want to delete selected Goals ?"))
                return true;
            else
                return false;
        }
        else {
            alert('Please select atleast one goal.');
            return false;
        }
    };
    function ActiveConfirmation() {
        var panel = document.getElementById('<%= tpnlOutput.ClientID %>');
        var chkArray = panel.getElementsByTagName("input");
        var checked = 0;
        for (var i = 0; i < chkArray.length; i++) {
            if (chkArray[i].type == "checkbox" && chkArray[i].checked == true) {
                checked = 1;
                break;
            }
        }
        if (checked == 1) {
            if (confirm("Are you sure you want to Activete selected Goals ?"))
                return true;
            else
                return false;
        }
        else {
            alert('Please select atleast one goal.');
            return false;
        }
    };
    function DeactiveConfirmation() {
        var panel = document.getElementById('<%= tpnlOutput.ClientID %>');
        var chkArray = panel.getElementsByTagName("input");
        var checked = 0;
        for (var i = 0; i < chkArray.length; i++) {
            if (chkArray[i].type == "checkbox" && chkArray[i].checked == true) {
                checked = 1;
                break;
            }
        }
        if (checked == 1) {
            if (confirm("Are you sure you want to Deactivete selected Goals ?"))
                return true;
            else
                return false;
        }
        else {
            alert('Please select atleast one goal.');
            return false;
        }
    }
</script>

<style type="text/css">
        .MyTabStyle .ajax__tab_header
        {
            font-family: "Helvetica Neue" , Arial, Sans-Serif;
            font-size: 14px;
            font-weight:bold;
            display: block;

        }
        .MyTabStyle .ajax__tab_header .ajax__tab_outer
        {
            border-color: #222;
            color: #222;
            padding-left: 10px;
            margin-right: 3px;
            border:solid 1px #d7d7d7;
        }
        .MyTabStyle .ajax__tab_header .ajax__tab_inner
        {
            border-color: #666;
            color: #666;
            padding: 3px 10px 2px 0px;
        }
        .MyTabStyle .ajax__tab_hover .ajax__tab_outer
        {
            background-color:#9c3;
        }
        .MyTabStyle .ajax__tab_hover .ajax__tab_inner
        {
            color: #fff;
        }
        .MyTabStyle .ajax__tab_active .ajax__tab_outer
        {
            border-bottom-color: #ffffff;
            background-color: #d7d7d7;
        }
        .MyTabStyle .ajax__tab_active .ajax__tab_inner
        {
            color: #000;
            border-color: #333;
        }
       

        
    </style>


<%--<table style="width:100%">
    <tr ><td colspan="2"><hr /></td></tr>
   <tr>
        <td align="right" width="150px">
            <asp:Label ID="lblPickCustomer" runat="server" CssClass="FieldName" Text="Pick a Customer :"></asp:Label>
        </td>
        <td>
           <asp:TextBox ID="txtPickCustomer" runat="server" AutoPostBack="True" AutoComplete="Off" 
                CssClass="txtField"></asp:TextBox>
                <ajaxToolkit:AutoCompleteExtender
                    ID="txtPickCustomer_autoCompleteExtender"
                    runat="server" CompletionInterval="100"
                    EnableCaching="true"
                    CompletionListCssClass="AutoCompleteExtender_CompletionList"
                    CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                    CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                    MinimumPrefixLength="1"
                    OnClientItemSelected="GetParentCustomerId"
                    ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                    TargetControlID="txtPickCustomer" UseContextKey="True" 
                    DelimiterCharacters=""
                    CompletionSetCount="5"
                    
                   
                    Enabled="True">
                </ajaxToolkit:AutoCompleteExtender>
            <span id="SpanPicCustomerReq" class="spnRequiredField" runat="server">*</span>
            <asp:RequiredFieldValidator ID="RFVPickCustomer" runat="server" ControlToValidate="txtParentCustomerType"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please Pick a Customer"></asp:RequiredFieldValidator>
        </td>
    </tr>
     <tr><td colspan="2"><hr /></td></tr>
</table>
--%>
<asp:HiddenField ID="txtParentCustomerType" runat="server" />
<asp:HiddenField ID="hidActiveTab" runat="server" />
<%--<asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="txtCustomerId_ValueChanged" />--%>
<asp:HiddenField ID="hidRTSaveReq" runat="server" />

<ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
    EnableViewState="true" AutoPostBack="false">
    <ajaxToolkit:TabPanel HeaderText="Goal Setup" ID="tpnlGoalSetup" BackColor="Blue"
        runat="server">
        <HeaderTemplate>
            Goal Setup
        </HeaderTemplate>
        <ContentTemplate>
        <table class="TableBackground" width="100%">
         <tr>
                    <td class="HeaderCell">
                        <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Goal Profile"></asp:Label>
                        
                    </td>
                 </tr>
         <tr>
                 <td>
                    <hr />
                 </td>
                 </tr>
        </table>
        <table class="TableBackground">
                
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblGoalbjective" runat="server" CssClass="FieldName" Text="Pick Goal Objective :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlGoalType" runat="server" AutoPostBack="True" CssClass="cmbField"
                              OnSelectedIndexChanged="ddlGoalType_SelectedIndexChanged">
                        </asp:DropDownList>
                        <span id="spanGoalType" class="spnRequiredField" runat="server">*</span>                      
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblGoalDate" runat="server" CssClass="FieldName" Text="Goal Entry Date :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtGoalDate" runat="server" AutoCompleteType="Disabled" CssClass="txtField"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtGoalDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                            OnClientDateSelectionChanged="checkDate" TargetControlID="txtGoalDate" Enabled="True">
                        </ajaxToolkit:CalendarExtender>
                        <span id="SpanGoalDateReq" class="spnRequiredField" runat="server">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtGoalDate"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please select a Date"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr id="trGoalDesc" runat="server">
                    <td id="Td2" class="leftField" runat="server">
                        <asp:Label ID="lblGoalDescription" runat="server" CssClass="FieldName" Text="Goal Description :"></asp:Label>
                    </td>
                    <td id="Td3" class="rightField" runat="server">
                        <asp:TextBox ID="txtGoalDescription" runat="server" AutoCompleteType="Disabled" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trPickChild" runat="server">
                    <td id="Td4" class="leftField" runat="server">
                        <asp:Label ID="lblPickChild" runat="server" CssClass="FieldName" Text="Select a child for Goal planning :"></asp:Label>
                    </td>
                    <td id="Td5" class="rightField" runat="server">
                        <asp:DropDownList ID="ddlPickChild" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblGoalCostToday" runat="server" CssClass="FieldName" Text="Goal Cost Today :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtGoalCostToday" runat="server" CssClass="txtField"></asp:TextBox>
                        <span id="SpanGoalCostTodayReq" class="spnRequiredField" runat="server">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtGoalCostToday" ValidationGroup="btnSave" CssClass="rfvPCG" ErrorMessage="Goal cost Today Required"></asp:RequiredFieldValidator>
                             <ajaxToolkit:FilteredTextBoxExtender ID="txtGoalCostToday_E" runat="server" Enabled="True" TargetControlID="txtGoalCostToday"
                                            FilterType="Custom, Numbers" ValidChars=".">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                        
                         
                        <asp:RangeValidator ID="RVtxtGoalCostToday" Display="Dynamic"  
                            SetFocusOnError="True" Type="Double" ErrorMessage="Value  should not be more than 15 digit"
                             ValidationGroup="btnSave" MinimumValue="0" MaximumValue="999999999999999" 
                            ControlToValidate="txtGoalCostToday" runat="server"></asp:RangeValidator>
                         
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblGoalYear" runat="server" CssClass="FieldName" Text="Goal Year :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlGoalYear" runat="server" CssClass="cmbField" CausesValidation="True">                                                       
                            <asp:ListItem Selected="True">2012</asp:ListItem>
                            <asp:ListItem>2013</asp:ListItem>
                            <asp:ListItem>2014</asp:ListItem>
                            <asp:ListItem>2015</asp:ListItem>
                            <asp:ListItem>2016</asp:ListItem>
                            <asp:ListItem>2017</asp:ListItem>
                            <asp:ListItem>2018</asp:ListItem>
                            <asp:ListItem>2019</asp:ListItem>
                            <asp:ListItem>2020</asp:ListItem>
                            <asp:ListItem>2021</asp:ListItem>
                            <asp:ListItem>2022</asp:ListItem>
                            <asp:ListItem>2023</asp:ListItem>
                            <asp:ListItem>2024</asp:ListItem>
                            <asp:ListItem>2025</asp:ListItem>
                            <asp:ListItem>2026</asp:ListItem>
                            <asp:ListItem>2027</asp:ListItem>
                            <asp:ListItem>2028</asp:ListItem>
                            <asp:ListItem>2029</asp:ListItem>
                            <asp:ListItem>2030</asp:ListItem>
                            <asp:ListItem>2031</asp:ListItem>
                            <asp:ListItem>2032</asp:ListItem>
                            <asp:ListItem>2033</asp:ListItem>
                            <asp:ListItem>2034</asp:ListItem>
                            <asp:ListItem>2035</asp:ListItem>
                            <asp:ListItem>2036</asp:ListItem>
                            <asp:ListItem>2037</asp:ListItem>
                            <asp:ListItem>2038</asp:ListItem>
                            <asp:ListItem>2039</asp:ListItem>
                            <asp:ListItem>2040</asp:ListItem>
                            <asp:ListItem>2041</asp:ListItem>
                            <asp:ListItem>2042</asp:ListItem>
                            <asp:ListItem>2043</asp:ListItem>
                            <asp:ListItem>2044</asp:ListItem>
                            <asp:ListItem>2045</asp:ListItem>
                            <asp:ListItem>2046</asp:ListItem>
                            <asp:ListItem>2047</asp:ListItem>
                            <asp:ListItem>2048</asp:ListItem>
                            <asp:ListItem>2049</asp:ListItem>
                            <asp:ListItem>2050</asp:ListItem>
                            <asp:ListItem>2051</asp:ListItem>
                            <asp:ListItem>2052</asp:ListItem>
                            <asp:ListItem>2053</asp:ListItem>
                            <asp:ListItem>2054</asp:ListItem>
                            <asp:ListItem>2055</asp:ListItem>
                            <asp:ListItem>2056</asp:ListItem>
                            <asp:ListItem>2057</asp:ListItem>
                            <asp:ListItem>2058</asp:ListItem>
                            <asp:ListItem>2059</asp:ListItem>
                            <asp:ListItem>2060</asp:ListItem>
                            <asp:ListItem>2061</asp:ListItem>
                            <asp:ListItem>2062</asp:ListItem>
                            <asp:ListItem>2063</asp:ListItem>
                            <asp:ListItem>2064</asp:ListItem>
                            <asp:ListItem>2065</asp:ListItem>
                            <asp:ListItem>2066</asp:ListItem>
                            <asp:ListItem>2067</asp:ListItem>
                            <asp:ListItem>2068</asp:ListItem>
                            <asp:ListItem>2069</asp:ListItem>
                            <asp:ListItem>2070</asp:ListItem>
                            <asp:ListItem>2071</asp:ListItem>
                            <asp:ListItem>2072</asp:ListItem>
                            <asp:ListItem>2073</asp:ListItem>
                            <asp:ListItem>2074</asp:ListItem>
                            <asp:ListItem>2075</asp:ListItem>
                        </asp:DropDownList>
                        <span id="SpanGoalYearReq" class="spnRequiredField" runat="server">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlGoalYear"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please Select Goal Year"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblCurrentInvestPurpose" runat="server" CssClass="FieldName" Text="Existing Investment Allocated :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtCurrentInvestPurpose" runat="server" AutoCompleteType="Disabled"
                            CssClass="txtField" MaxLength="15" OnBlur="SetROI();"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="txtCurrentInvestPurpose_E" runat="server" Enabled="True" TargetControlID="txtCurrentInvestPurpose"
                                            FilterType="Custom, Numbers" ValidChars=".">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                        <span id="SpanCurrInPurReq" class="spnRequiredField" runat="server">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCurrentInvestPurpose"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please enter some amount"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                              
                
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblRateOfInterstAbove" runat="server" CssClass="FieldName" Text="Expected return on the allocated investment(%) :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtAboveRateOfInterst" runat="server" AutoCompleteType="Disabled"
                            CssClass="txtField" MaxLength="15"></asp:TextBox>
                              <ajaxToolkit:FilteredTextBoxExtender ID="txtAboveRateOfInterst_E" runat="server" Enabled="True" TargetControlID="txtAboveRateOfInterst"              FilterType="Custom, Numbers" ValidChars=".">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                        <span id="SpanAboveROIReq" class="spnRequiredField" runat="server">*</span>
                         <asp:RangeValidator ID="RangeValidator1"   Display="Dynamic" 
                            SetFocusOnError="True" Type="Double" ErrorMessage="Value  should not be more than 100"
                            MinimumValue="0" MaximumValue="100" 
                            ControlToValidate="txtAboveRateOfInterst" runat="server"></asp:RangeValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4"    Display="Dynamic" runat="server" ControlToValidate="txtAboveRateOfInterst"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please enter some % value"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                
                <tr>
                    <td class="leftField">
                      <nobr>  <asp:Label ID="ExpRateOfReturn" runat="server" CssClass="FieldName" Text="Expected return on the future investment(%) :"></asp:Label></nobr>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtExpRateOfReturn" runat="server" AutoCompleteType="Disabled" CssClass="txtField"
                            MaxLength="15"></asp:TextBox>
                        <span id="SpanExpROI" class="spnRequiredField" runat="server">*</span>
                        <ajaxToolkit:FilteredTextBoxExtender ID="txtExpRateOfReturn_E" runat="server" Enabled="True" TargetControlID="txtExpRateOfReturn"
                                            FilterType="Custom, Numbers" ValidChars=".">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                         <asp:RangeValidator Display="Dynamic" ID="RangeValidator3" 
                            SetFocusOnError="True" Type="Double" ErrorMessage="Value  should not be more than 100"
                            MinimumValue="0" MaximumValue="100" ControlToValidate="txtExpRateOfReturn" 
                            runat="server"></asp:RangeValidator>
                        <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtExpRateOfReturn"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please enter some % value"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr id="trROIFutureInvestment" runat="server">
                    <td id="Td6" class="leftField" runat="server">
                        <asp:Label ID="lblROIFutureInvest" runat="server" CssClass="FieldName" Text="Return on retirement corpus(%) :"></asp:Label>
                    </td>
                    <td id="Td7" class="rightField" runat="server">
                        <asp:TextBox ID="txtROIFutureInvest" runat="server" AutoCompleteType="Disabled" CssClass="txtField"
                            MaxLength="15"></asp:TextBox>
                            <span id="SpanROIFutureInvest" class="spnRequiredField" runat="server">*</span>
                            
                            <ajaxToolkit:FilteredTextBoxExtender ID="txtROIFutureInvest_E" runat="server" Enabled="True" TargetControlID="txtROIFutureInvest"
                                            FilterType="Custom, Numbers" ValidChars=".">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                         <asp:RangeValidator ID="RangeValidator2"  Display="Dynamic" 
                            SetFocusOnError="True" Type="Double" ErrorMessage="Value  should not be more than 100"
                            MinimumValue="0" MaximumValue="100" ControlToValidate="txtROIFutureInvest" 
                            runat="server"></asp:RangeValidator>
                        <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtROIFutureInvest"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please enter some % value"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Inflation(%) :"></asp:Label>
                    </td>
                    <td class="rightField" colspan="2">
                        <asp:TextBox ID="txtInflation" runat="server" AutoCompleteType="Disabled" CssClass="txtField"
                            TextMode="SingleLine" ></asp:TextBox>
                              <span id="spnInflation" class="spnRequiredField" runat="server">*</span>                           
                            <asp:RangeValidator ID="RangeValidator4"  Display="Dynamic" SetFocusOnError="true" Type="Double" ErrorMessage="Value  should not be more than 100"
                            MinimumValue="0" MaximumValue="100" ControlToValidate="txtInflation" runat="server"></asp:RangeValidator>
                        <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtInflation"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please enter some % value"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblComment" runat="server" CssClass="FieldName" Text="Comments :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtComment" runat="server" AutoCompleteType="Disabled" CssClass="txtField"
                            TextMode="MultiLine" ></asp:TextBox>
                    </td>
                </tr>
            </table>
        <table class="TableBackground">
                <tr id="trchkApprove" runat="server">
                    <td id="Td8" runat="server">
                        <asp:CheckBox ID="chkApprove" runat="server" CssClass="FieldName" Text=" Approved by Customer" />
                        
                        
                    </td>
                </tr>
                <tr id="trlblApproveOn" runat="server">
                    <td id="Td9" runat="server">
                       
                        <asp:Label ID="lblApproveOn" runat="server" CssClass="FieldName" Text="Customer Approved On "></asp:Label>
                        
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnCancel" runat="server" CssClass="PCGButton" Text="Cancel" CausesValidation="False"
                            OnClick="btnCancel_Click" OnClientClick="return validate()"/>
                        <asp:Button ID="btnSaveAdd" runat="server" CssClass="PCGMediumButton" OnClick="btnSaveAdd_Click"
                            Text="Save & Add" ValidationGroup="btnSave" OnClientClick="return validate()" />
                        <asp:Button ID="btnNext" runat="server" CssClass="PCGMediumButton" Text="Save & Next" OnClick="btnNext_Click"
                            ValidationGroup="btnSave"  OnClientClick="return validate()"/>
                        <asp:Button ID="btnBackToAddMode" runat="server" CssClass="PCGButton" Text="AddNew"
                             ValidationGroup="btnSave" OnClick="btnBackToAddMode_Click"  OnClientClick="return validate()"/>
                         <asp:Button ID="btnBackToView" runat="server" CssClass="PCGButton" Text="Back" OnClick="btnBackToView_Click"/>
                         <asp:Button ID="btnEdit" runat="server" CssClass="PCGButton" Text="Edit" OnClick="btnEdit_Click"/>
                        
                         <asp:Button ID="btnUpdate" runat="server" CssClass="PCGButton" Text="Update" OnClick="btnUpdate_Click"/>
                        
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" CssClass="cmbField" style="font-size: small;" Text="Note :"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td id="Td1" class="tdRequiredText" runat="server">
                       <asp:Label ID="trRequiedNote" CssClass="cmbField" style="font-size: small;" runat="server" Text="1)Fields marked with ' * ' are compulsory."></asp:Label>
                       
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNote" runat="server" CssClass="cmbField" style="font-size: small;" Text="2)Expected rate of return is defaulted as per your risk assessment.If risk profile is not complete default risk profile will be 'MODERATE'."></asp:Label>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </ajaxToolkit:TabPanel>
    <ajaxToolkit:TabPanel ID="tpnlOutput" runat="server" HeaderText="Goals Overview" Style="width: 100%;
        height: 100%">
        <ContentTemplate>
            <table class="TableBackground" width="100%">
                
                <tr>
                    <td class="HeaderCell">
                        <asp:Label ID="lblHeaderOutPut" runat="server" CssClass="HeaderTextBig" 
                            Text="Goal Profile Details"></asp:Label>
                        
                    </td>
                </tr>
                <tr><td><hr /></td></tr>
               </table>
                 <table class="TableBackground">
                <tr>
                    <td>
                        <asp:GridView ID="gvGoalOutPut" runat="server" AllowSorting="True" 
                            AutoGenerateColumns="False" CellPadding="4" CssClass="GridViewStyle" 
                            DataKeyNames="GoalID" HorizontalAlign="Center" ShowFooter="True" EnableViewState="true">
                            <FooterStyle CssClass="FooterStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                            <Columns>
                                <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkGoalOutput" runat="server" CssClass="cmbField" />
                                    </ItemTemplate>                                  
                                </asp:TemplateField>
                                  
                                <asp:TemplateField HeaderText="Goal Type" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkGoalType" runat="server" CssClass="cmbField" 
                                            OnClick="lnkGoalType_Click" Text='<%# Eval("GoalName") %>'>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                      <FooterTemplate>
                                        <asp:Label ID="lblTotalText" runat="server" CssClass="Field" Font-Bold="true" 
                                            ForeColor="White" Text=" Total  =  Rs.">
                                        </asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name of Associate Customer" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssociateName" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("ChildName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cost Today(Rs.)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCostToday" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("CostToday")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblCostTodayTotal" runat="server" CssClass="Field" Font-Bold="true" 
                                            ForeColor="White" Text="">
                                        </asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Investment Amount Allocated(Rs.)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurrentInvestment" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("CurrentInvestment")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblCurrentInvestmentTotal" runat="server" CssClass="Field" Font-Bold="true" 
                                            ForeColor="White" Text="">
                                        </asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount To be Saved Per Month(Rs.)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSavingReq" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("SavingRequired") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblMonthlySavingTotal" runat="server" CssClass="Field" Font-Bold="true" 
                                            ForeColor="White" Text="">
                                        </asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Goal Amount(Rs.)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGaolAmount" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("GoalAmount")%>'>
                                        </asp:Label>
                                    </ItemTemplate>  
                                     <FooterTemplate>
                                        <asp:Label ID="lblGoalAmountTotal" runat="server" CssClass="Field" Font-Bold="true" 
                                            ForeColor="White" Text="">
                                        </asp:Label>
                                    </FooterTemplate>                                 
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Calculated On" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGoalDate" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("GoalPrifileDate", "{0:M-dd-yyyy}")  %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Required In Year" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGoalYear" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("GoalYear") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IsActive" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsActive" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("IsActive") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:DropDownList ID="ddlActiveFilter" runat="server" AutoPostBack="true" 
                                            CssClass="cmbField" OnPreRender="SetValue" 
                                            OnSelectedIndexChanged="ddlActiveFilter_SelectedIndexChanged">
                                            <asp:ListItem Text="Active" Value="1">
                                            </asp:ListItem>
                                            <asp:ListItem Text="InActive" Value="0">
                                            </asp:ListItem>
                                            <asp:ListItem Text="All" Value="2">
                                            </asp:ListItem>
                                        </asp:DropDownList>
                                        <br></br>
                                        <asp:Label ID="ActiveMessage" runat="server" BackColor="Transparent" 
                                            CssClass="cmbField" Font-Bold="true" ForeColor="White" Text="No Active Goals">
                                         </asp:Label>
                                    </HeaderTemplate>
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="Approved by customer On">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAporoveOn" runat="server" CssClass="cmbField" 
                                            Text='<%# Eval("CustomerApprovedOn", "{0:M-dd-yyyy}")  %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                            <EditRowStyle CssClass="EditRowStyle" HorizontalAlign="Left" 
                                VerticalAlign="Top" />
                            <HeaderStyle CssClass="HeaderStyle" />
                            <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                            <RowStyle CssClass="RowStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr id="trOtherGoalParagraph" runat="server" >
                    <td width="100%">
                    
                    <div id="divOtherGoal" style="width: 100%;height: 100%; border: Solid 1px #EBEFF9 !important; border-color: #EBEFF9 !important" >
                         <br />
                        <asp:Label ID="lblOtherGoalParagraph" runat="server" CssClass="cmbField" style="font-size:20 !important" Text="";>
                        </asp:Label>
                 
                    </div>
                    <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvRetirement" runat="server" AllowSorting="True" 
                            AutoGenerateColumns="False" CellPadding="4" CssClass="GridViewStyle" 
                            DataKeyNames="CG_GoalId" HorizontalAlign="Center" ShowFooter="True">
                            <FooterStyle CssClass="FooterStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                            <Columns>
                                   <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRTGoalOutput" runat="server" CssClass="cmbField" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Goal Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkRTGoalType" runat="server" CssClass="cmbField" 
                                            OnClick="lnkRTGoalType_Click" Text='<%# Eval("XG_GoalName") %>'>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Retirement Year" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGoalYear" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("CG_GoalYear") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Corpus Required(Rs.)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblCostToday" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("CG_FVofCostToday") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Investment Amount Allocated(Rs.)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvestmentCurrent" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("CG_CurrentInvestment")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lumpsum Investment Required(Rs.)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblLumpsumInvReq" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("CG_LumpsumInvestmentRequired") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Amount To be Saved Per Year(Rs.)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblYSavingReq" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("CG_YearlySavingsRequired") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount To be Saved Per Month(Rs.)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSavingReq" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("CG_MonthlySavingsRequired") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Calculated On" HeaderStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblGoalDate" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("CG_GoalProfileDate", "{0:M-dd-yyyy}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle CssClass="EditRowStyle" HorizontalAlign="Left" 
                                VerticalAlign="Top" />
                            <HeaderStyle CssClass="HeaderStyle" />
                            <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                            <RowStyle CssClass="RowStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr id="trRTParaGraph" runat="server" >
                <td width="100%" style="width: 100%;height: 100%; border-width:medium !important; border-color:Blue !important">
                <br />
                <asp:Label ID="lblRTParagraph" runat="server" CssClass="cmbField" style="font-size:20 !important" Text="";>
                </asp:Label>
                <br />
                </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="lblTotalText" runat="server" CssClass="FieldName" Text="">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                
                                <td>
                                    <asp:Button ID="Delete" runat="server" CssClass="PCGButton" OnClick="Delete_Click" OnClientClick="return DeleteConfirmation();" Text="Delete" >
                                    </asp:Button>
                                </td>
                                <td>
                                    <asp:Button ID="Activate" runat="server" CssClass="PCGMediumButton" OnClick="Activate_Click" OnClientClick="return ActiveConfirmation();" Text="Activate" >
                                    </asp:Button>
                                </td>
                                <td>
                                    <asp:Button ID="Deactive" runat="server" CssClass="PCGMediumButton" OnClick="Deactive_Click" OnClientClick="return DeactiveConfirmation();" Text="Deactivate" >
                                    </asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </ajaxToolkit:TabPanel>
    
    <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Goal Funding Status" Style="width: 100%;
        height: 100%">
        <ContentTemplate>
        <table class="TableBackground" width="65%">
          <tr>
                    <td class="HeaderCell">
                        <asp:Label ID="lblFundGapHeader" runat="server" CssClass="HeaderTextBig" Text="Goal Funding Status"></asp:Label>
                        
                    </td>
          </tr>
          
          <tr><td><hr /></td></tr>
          
        <tr>
        <td>
        <asp:GridView ID="gvGoalFundingGap" runat="server" AllowSorting="True" OnRowDataBound="gvGoalFundingGap_RowDataBound"
                            AutoGenerateColumns="False" CellPadding="4" CssClass="GridViewStyle" 
                            HorizontalAlign="Center" ShowFooter="True">
                            <FooterStyle CssClass="FooterStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                            <Columns>
                            <asp:TemplateField HeaderText="Available Funds" ItemStyle-HorizontalAlign="Left" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblMoneySource" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("Source") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                             </asp:TemplateField>
                             
                             <asp:TemplateField HeaderText="Goal Requirement" ItemStyle-HorizontalAlign="Left" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvestedAmount" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("GoalFunding") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                             </asp:TemplateField>
                             
                              <asp:TemplateField HeaderText="Difference" ItemStyle-HorizontalAlign="Left" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblGap" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("GoalGap") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                             </asp:TemplateField>
                             
                             <asp:TemplateField HeaderText="IsGap" ItemStyle-HorizontalAlign="Left" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsGap" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("IsGap") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                             </asp:TemplateField>
                             
                             <asp:TemplateField HeaderText="Indicator" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>                         
                                <asp:Image ID="imgGoalFundIndicator" ImageAlign="Middle" runat="server" />
                             </ItemTemplate>
                       
                       </asp:TemplateField> 
                           </Columns>
                           <EditRowStyle CssClass="EditRowStyle" HorizontalAlign="Left" 
                                VerticalAlign="Top" />
                            <HeaderStyle CssClass="HeaderStyle" />
                            <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                            <RowStyle CssClass="RowStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
        </asp:GridView>
        </td>
        </tr>
        <tr>
        <td>
        </td>
        </tr>
        
        <tr>
        
        <td>
          <asp:Label ID="lblNoteFunding" runat="server" CssClass="cmbField" style="font-size: small;" Text="Note :Data display is for indicating purpose only."></asp:Label>
        </td>
        </tr>
        </table>
        </ContentTemplate>
    </ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>

