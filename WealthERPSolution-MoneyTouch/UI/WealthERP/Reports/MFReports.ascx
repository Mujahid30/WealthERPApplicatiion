<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFReports.ascx.cs" Inherits="WealthERP.Reports.MFReports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script type="text/javascript" language="javascript">
    function validate(type) {
        
        var panel = "";
        var isPorfolioSelected = false;

        //Report selection check
        if (type == 'mail') {
            if (document.getElementById("<%= chkMFSummary.ClientID %>").checked == false &&
           document.getElementById("<%= chkPortfolioReturns.ClientID %>").checked == false &&
           document.getElementById("<%= chkDividendDetail.ClientID %>").checked == false &&
           document.getElementById("<%= chkTransactionReport.ClientID %>").checked == false &&
           document.getElementById("<%= chkDividendSummary.ClientID %>").checked == false &&
           document.getElementById("<%= chkCapitalGainDetails.ClientID %>").checked == false &&
           document.getElementById("<%= chkCapitalGainSummary.ClientID %>").checked == false) {
                alert("Please select a report");
                return false;
            }
        }


        //Customer name check
        if (document.getElementById("<%= rdoGroup.ClientID %>").checked == true) {
            if (document.getElementById("<%= txtParentCustomer.ClientID %>").value == '' || document.getElementById("<%= txtParentCustomer.ClientID %>").value == 'Type the Customer Name') {
                alert("Please select a Customer");
                return false;
            }
        }
        else {
            if (document.getElementById("<%= txtCustomer.ClientID %>").value == '' || document.getElementById("<%= txtCustomer.ClientID %>").value == 'Type the Customer Name') {
                alert("Please select a Customer");
                return false;
            }
        }

        //Date validation.
        dateType = document.getElementById("<%= hidDateType.ClientID %>").value;

        if (type == 'mail') {
            if (document.getElementById("<%= chkDividendDetail.ClientID %>").checked == true || document.getElementById("<%= chkDividendSummary.ClientID %>").checked == true ||
                    document.getElementById("<%= chkTransactionReport.ClientID %>").checked == true || document.getElementById("<%= chkCapitalGainSummary.ClientID %>").checked == true ||
                    document.getElementById("<%= chkCapitalGainDetails.ClientID %>").checked == true) {
                if (document.getElementById("<%= rdoDateRange.ClientID %>").checked == true) {
                    dateVal = document.getElementById("<%= txtEmailFromDate.ClientID  %>").value;
                    if (dateVal == null || dateVal == "" || dateVal == 'dd/mm/yyyy') {
                        alert("Please select from date")
                        return false;
                    }
                    toDate = document.getElementById("<%= txtEmailToDate.ClientID  %>").value;
                    if (toDate == null || toDate == "" || toDate == 'dd/mm/yyyy') {
                        alert("Please select to date")
                        return false;
                    }
                    if (isFutureDate(toDate) == true) {
                        alert("To date cannot be greater than current date.")
                        return false;
                    }
                }
                else {
                    dateVal = document.getElementById("<%= ddlPeriod.ClientID  %>").selectedIndex;
                    dateVal1 = document.getElementById("<%= ddlEmailDatePeriod.ClientID  %>").selectedIndex;
                    if (dateVal < 1 && dateVal1 < 1) {
                        alert("Please select date")
                        return false;
                    }
                }
            }
            if (document.getElementById("<%= chkMFSummary.ClientID  %>").checked == true || document.getElementById("<%= chkPortfolioReturns.ClientID  %>").checked == true) {
                dateVal = document.getElementById("<%= txtEmailAsOnDate.ClientID  %>").value;
                if (dateVal == null || dateVal == "" || dateVal == 'dd/mm/yyyy') {
                    alert("Please select date")
                    return false;
                }
                if (isFutureDate(dateVal) == true) {
                    alert("As on date cannot be greater than current date.")
                    return false;
                }
            }
        }
        else {
            if (dateType == 'DATE_RANGE') {

                dateVal = document.getElementById("<%= txtFromDate.ClientID  %>").value;
                if (dateVal == null || dateVal == "" || dateVal == 'dd/mm/yyyy') {
                    alert("Please select from date")
                    return false;
                }
                toDate = document.getElementById("<%= txtToDate.ClientID  %>").value;
                if (toDate == null || toDate == "" || toDate == 'dd/mm/yyyy') {
                    alert("Please select to date")
                    return false;
                }
                if (isFutureDate(toDate) == true) {
                    alert("To date cannot be greater than current date.")
                    return false;
                }
            }
            else if (dateType == 'PERIOD') {
                dateVal = document.getElementById("<%= ddlPeriod.ClientID  %>").selectedIndex;
                dateVal1 = document.getElementById("<%= ddlEmailDatePeriod.ClientID  %>").selectedIndex;
                if (dateVal < 1 && dateVal1 < 1) {
                    alert("Please select date")
                    return false;
                }
            }
            else if (dateType == 'AS_ON') {

                dateVal = document.getElementById("<%= txtAsOnDate.ClientID  %>").value;
                if (dateVal == null || dateVal == "" || dateVal == 'dd/mm/yyyy') {
                    alert("Please select date")
                    return false;
                }
                if (isFutureDate(dateVal) == true) {
                    alert("As on date cannot be greater than current date.")
                    return false;
                }
            }
        }
        //**************************************************************************************************************
        window.document.forms[0].target = '_blank';
        if (type == 'mail') {
            window.document.forms[0].action = "/Reports/EmailReport.aspx?mail=1";
        }
        else {
            window.document.forms[0].action = "/Reports/Display.aspx?mail=0";
        }
        setTimeout(function() {
            window.document.forms[0].target = '';
            window.document.forms[0].action = "";
        }, 500);
        return true;
    }

    function GetCustomerId(source, eventArgs) {
       
        document.getElementById("<%= hdnCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    };

    function DisplayDates(type) {

        if (type == 'DATE_RANGE') {
            document.getElementById("<%= rbtnPickDate.ClientID %>").checked = true;
            document.getElementById("<%= rbtnPickDate.ClientID %>").checked = true;
            document.getElementById("tblPickDate").style.display = 'block';
            document.getElementById("<%= trRange.ClientID %>").style.display = 'block';
            document.getElementById("<%= trPeriod.ClientID %>").style.display = 'none';
            document.getElementById("<%= trAsOn.ClientID %>").style.display = 'none';
        }
        else if (type == 'PERIOD') {
            document.getElementById("tblPickDate").style.display = 'block';
            document.getElementById("<%= trRange.ClientID %>").style.display = 'none';
            document.getElementById("<%= trPeriod.ClientID %>").style.display = 'block';
            document.getElementById("<%= trAsOn.ClientID %>").style.display = 'none';
        }
        else if (type == 'AS_ON') {
            document.getElementById("tblPickDate").style.display = 'none';
            document.getElementById("<%= trRange.ClientID %>").style.display = 'none';
            document.getElementById("<%= trPeriod.ClientID %>").style.display = 'none';
            document.getElementById("<%= trAsOn.ClientID %>").style.display = 'block';
        }
        document.getElementById("<%= hidDateType.ClientID %>").value = type;
    }


    function DisplayEmailDates(type) {

        if (type == 'DATE_RANGE') {
            document.getElementById("tblEmailDateRange").style.display = 'block';
            document.getElementById("tblEmailDatePeriod").style.display = 'none';
        }
        else {
            document.getElementById("tblEmailDatePeriod").style.display = 'block';
            document.getElementById("tblEmailDateRange").style.display = 'none';
        }
        document.getElementById("<%= hidDateType.ClientID %>").value = type;
    }

    function isFutureDate(dateToCheck) {
        var currentDate = '<%= DateTime.Now.ToString("yyyyMMdd") %>'
        var yyyymmdddateToCheck = dateToCheck.substr(6, 4) + dateToCheck.substr(3, 2) + dateToCheck.substr(0, 2)
        if (currentDate < yyyymmdddateToCheck) {
            return true;
        }
        else
            return false;
    }
    function ChangeDates() {
        var arr = new Array();
        arr["CATEGORY_WISE"] = "AS_ON";
        arr["RETURNS_PORTFOLIO"] = "AS_ON";
        arr["RETURNS_TRANSACTION"] = "AS_ON";
        arr["TRANSACTION_REPORT"] = "DATE_RANGE";
        arr["DIVIDEND_STATEMENT"] = "DATE_RANGE";
        arr["DIVIDEND_SUMMARY"] = "DATE_RANGE";
        arr["CAPITAL_GAIN_SUMMARY"] = "DATE_RANGE";
        arr["CAPITAL_GAIN_DETAILS"] = "DATE_RANGE";
        var dropdown = document.getElementById("<%= ddlReportSubType.ClientID %>");
        selectedReport = dropdown.options[dropdown.selectedIndex].value

        DisplayDates(arr[selectedReport]);

    }

    function OnChanged(sender, args) {
        document.getElementById("<%= hidTabIndex.ClientID %>").value = sender.get_activeTab()._tabIndex;
        //uncheckallCehckBoxes();
        return false;
    }
    //Clear all the portfolio check boxes in Group/Individual
    function uncheckallCehckBoxes() {

        for (i = 0; i < document.forms[0].elements.length; i++) {
            if (document.forms[0].elements[i].type == "checkbox") {
                if (document.forms[0].elements[i].name.substr(0, 5) == "chk--") {
                    document.forms[0].elements[i].checked = false;
                }
            }
        }
    }


    function ChangeCustomerSelectionTextBox(value) {
        
        if (value == 'Group') {
            document.getElementById('trGroupCustomer').style.display = 'block';
            document.getElementById('trIndCustomer').style.display = 'none';

        }
        else {
            document.getElementById('trIndCustomer').style.display = 'block';
            document.getElementById('trGroupCustomer').style.display = 'none';
        }

        document.getElementById("<%= trCustomerDetails.ClientID %>").style.display = 'none';
        document.getElementById("<%= divGroupCustomers.ClientID %>").innerHTML = '';
        document.getElementById("<%= divPortfolios.ClientID %>").innerHTML = '';
        document.getElementById("<%= txtCustomer.ClientID %>").value = 'Type the Customer Name';
        document.getElementById("<%= txtParentCustomer.ClientID %>").value = 'Type the Customer Name';
    }
</script>

<script>
    function ShowProcesssing(btn) {

        document.getElementById('btnSend').value = "Sending Email.Please wait..";
        document.getElementById('btnSend').disabled = true;

    }
    function sendMail() {

        if (document.getElementById("txtTo").value == "") {
            alert("Please enter To Email Address.");
            return false;
        }
        if (document.getElementById("txtTo").value.indexOf("@") < 2 || document.getElementById("txtTo").value.indexOf(".") < 4) {
            alert("Please enter  a valid To Email Address.");
            document.getElementById("txtTo").focus();
            return false;
        }

        document.getElementById("hidTo").value = document.getElementById("txtTo").value
        document.getElementById("hidSubject").value = document.getElementById("txtSubject").value
        document.getElementById("hidFormat").value = document.getElementById("ddlFormat").options[document.getElementById("ddlFormat").selectedIndex].value
        document.getElementById("hidCC").value = document.getElementById("txtCC").value
        document.getElementById("hidBody").value = document.getElementById("txtBody").value
        // alert(document.getElementById("txtBody").value)

        document.getElementById("hidCCMe").value = document.getElementById("chkCopy").checked
        //window.document.forms[0].action = "Display.aspx?mail=0";
        //ConvertnlTobr();
        //document.getElementById("btnSendEmail").click()

    }

    function replaceSpecialChars() {
       
        while (document.getElementById("txtBody").value.indexOf("<br/>") > -1) {
            document.getElementById("txtBody").value = document.getElementById("txtBody").value.replace("<br/>", "\n");
            document.getElementById("hidBody").value = document.getElementById("hidBody").value.replace("<br/>", "\n");
        }
    }

    function ConvertnlTobr() {

        document.getElementById("txtBody").value = document.getElementById("txtBody").value.replace(/\n/g, '<br/>');
        document.getElementById("hidBody").value = document.getElementById("hidBody").value.replace(/\s/g, ' ').replace(/  ,/g, '<br/>'); ;

    }
    
</script>

<style type="text/css">
    .tblSection
    {
        border: solid 1px #B5C7DE;
    }
    input
    {
        display: inline;
    }
    .style1
    {
        width: 620px;
    }
    .style2
    {
        width: 280px;
    }
    .style3
    {
        width: 861px;
    }
</style>
<table border="0" width="100%">
    <tr>
        <td colspan="2">
            <asp:Label ID="Label7" runat="server" CssClass="HeaderTextSmall" Text="MF Reports"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblGrpOrInd" runat="server" CssClass="HeaderTextSmall" Style='font-weight: normal;'
                Text="Generate report for :"></asp:Label>
            <asp:RadioButton runat="server" ID="rdoGroup" Text="Group" Class="cmbField" GroupName="GrpOrInd"
                Checked="true" onClick="ChangeCustomerSelectionTextBox('Group');" />
            <asp:RadioButton runat="server" ID="rdoIndividual" Text="Individual" Class="cmbField"
                GroupName="GrpOrInd" onClick="ChangeCustomerSelectionTextBox('Individual');" />
        </td>
        <td>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr id="trIndCustomer" style="display: none">
        <td>
            <asp:Label ID="Label1" runat="server" Text="Select Customer:" CssClass="FieldName"></asp:Label>
            <asp:TextBox ID="txtCustomer" runat="server" CssClass="txtField" AutoComplete="Off" AutoPostBack="true"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtCustomer_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtCustomer" WatermarkText="Type the Customer Name">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtCustomer_autoCompleteExtender" runat="server"
                TargetControlID="txtCustomer" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="true" OnClientItemSelected="GetCustomerId" />
            <span id="Span1" class="spnRequiredField">* </span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCustomer"
                ErrorMessage="<br />Please Enter Customer Name" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
            <span style='font-size: 8px; font-weight: normal' class='FieldName'>
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                Enter few characters of customer name. </span>
        </td>
        <td>
            &nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr id="trGroupCustomer">
        <td>
            <asp:Label ID="Label5" runat="server" Text="Select Customer:" CssClass="FieldName"></asp:Label>
            <asp:TextBox ID="txtParentCustomer" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="true"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtParentCustomer_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtParentCustomer" WatermarkText="Type the Customer Name">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtParentCustomer_autoCompleteExtender" runat="server"
                TargetControlID="txtParentCustomer" ServiceMethod="GetParentCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="true" OnClientItemSelected="GetCustomerId" />
            <span id="Span2" class="spnRequiredField">* </span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtCustomer"
                ErrorMessage="<br />Please Enter Customer Name" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
            <span style='font-size: 8px; font-weight: normal' class='FieldName'>
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                Enter few characters of customer name. </span>
        </td>
        <td>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr id="trCustomerDetails" runat="server" style="display: none">
        <td id="Td1" runat="server" class="style1">
            <asp:Label ID="lblPAN" runat="server" CssClass="FieldName" Text="PAN: "></asp:Label>
            <asp:TextBox ID="txtCustomerPAN" runat="server" CssClass="txtField" BackColor="Transparent"
                BorderStyle="None"></asp:TextBox>
        </td>
        <td id="Td2" runat="server">
            <asp:Label ID="Label9" runat="server" CssClass="FieldName" Text="Address: " Visible="false"></asp:Label>
            <asp:TextBox ID="txtCustomerAddress" runat="server" CssClass="txtField" BackColor="Transparent"
                BorderStyle="None"></asp:TextBox>
        </td>
    </tr>
    <tr id="tr1" runat="server">
        <td id="Td3" runat="server" class="style1">
            &nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio: "></asp:Label>
            <asp:DropDownList ID="ddlPortfolioGroup" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlPortfolioGroup_SelectedIndexChanged"
                AutoPostBack="True">
                <asp:ListItem Text="Managed" Value="MANAGED" Selected="True"></asp:ListItem>
                <asp:ListItem Text="UnManaged" Value="UN_MANAGED"></asp:ListItem>
                <asp:ListItem Text="All" Value="ALL"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="left" class="Field">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlPortfolioGroup" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <div id="divPortfolios" runat="server">
                    </div>
                    <div id="divGroupCustomers" runat="server">
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <%--<tr>
    <td colspan="2" class="Field">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlPortfolioGroup" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
                <div id="divPortfolios" runat="server">
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </td>
    </tr>--%>
    <tr>
        <td width="100%">
            <table width="100%">
                <tr>
                    <td>
                        <ajaxToolkit:TabContainer ID="tabViewAndEmailReports" runat="server" ActiveTabIndex="1"
                            Width="100%" Style="visibility: visible" OnClientActiveTabChanged="OnChanged">
                            <ajaxToolkit:TabPanel ID="tabpnlViewReports" runat="server" HeaderText="View Reports"
                                Width="100%">
                                <HeaderTemplate>
                                    View Reports
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td class="style1">
                                                <table width="100%" class="tblSection" cellpadding="10" border="0">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" CssClass="HeaderTextSmall" Style='font-weight: normal;'
                                                                Text="Select Report Type "></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" CssClass="FieldName">Report Type:</asp:Label><asp:DropDownList
                                                                ID="ddlReportSubType" runat="server" CssClass="cmbField" onChange="ChangeDates()">
                                                                <asp:ListItem Text="Mutual Fund Summary" Value="CATEGORY_WISE" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Portfolio Returns" Value="RETURNS_PORTFOLIO"></asp:ListItem>
                                                                <asp:ListItem Text="Transaction Report" Value="TRANSACTION_REPORT"></asp:ListItem>
                                                                <asp:ListItem Text="Dividend Statement" Value="DIVIDEND_STATEMENT"></asp:ListItem>
                                                                <asp:ListItem Text="Dividend Summary" Value="DIVIDEND_SUMMARY"></asp:ListItem>
                                                                <asp:ListItem Text="Capital Gain Summary" Value="CAPITAL_GAIN_SUMMARY"></asp:ListItem>
                                                                <asp:ListItem Text="Capital Gain Details" Value="CAPITAL_GAIN_DETAILS"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                <br />
                                                <table width="100%" class="tblSection" cellspacing="10" border="0">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" CssClass="HeaderTextSmall" Text="Select Date Range"
                                                                Style='font-weight: normal;'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table id="tblPickDate" width="100%">
                                                                <tr>
                                                                    <td width="250px">
                                                                        <asp:RadioButton ID="rbtnPickDate" Checked="True" runat="server" GroupName="Date"
                                                                            onclick="DisplayDates('DATE_RANGE')" /><asp:Label ID="lblPickDate" runat="server"
                                                                                Text="Pick a date range" CssClass="Field"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="rbtnPickPeriod" runat="server" GroupName="Date" onclick="DisplayDates('PERIOD')" /><asp:Label
                                                                            ID="lblPickPeriod" runat="server" Text="Pick a Period" CssClass="Field"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table id="trRange" runat="server">
                                                                <tr runat="server">
                                                                    <td runat="server">
                                                                        <asp:Label ID="lblFromDate" runat="server" CssClass="FieldName">From:</asp:Label>&nbsp;&nbsp;
                                                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtField"></asp:TextBox><ajaxToolkit:CalendarExtender
                                                                            ID="txtFromDate_CalendarExtender" runat="server" TargetControlID="txtFromDate"
                                                                            Format="dd/MM/yyyy" Enabled="True">
                                                                        </ajaxToolkit:CalendarExtender>
                                                                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtFromDate_TextBoxWatermarkExtender" runat="server"
                                                                            TargetControlID="txtFromDate" WatermarkText="dd/mm/yyyy" Enabled="True">
                                                                        </ajaxToolkit:TextBoxWatermarkExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFromDate"
                                                                            CssClass="rfvPCG" ErrorMessage="<br />Please select a From Date" Display="Dynamic"
                                                                            runat="server" ValidationGroup="btnGo"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td runat="server">
                                                                        &nbsp;&nbsp;
                                                                        <asp:Label ID="lblToDate" runat="server" CssClass="FieldName">To:</asp:Label>&nbsp;&nbsp;
                                                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="txtField"></asp:TextBox>
                                                                        <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" TargetControlID="txtToDate"
                                                                            Format="dd/MM/yyyy" Enabled="True">
                                                                        </ajaxToolkit:CalendarExtender>
                                                                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtToDate_TextBoxWatermarkExtender" runat="server"
                                                                            TargetControlID="txtToDate" WatermarkText="dd/mm/yyyy" Enabled="True">
                                                                        </ajaxToolkit:TextBoxWatermarkExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtToDate"
                                                                            CssClass="rfvPCG" ErrorMessage="<br />Please select a To Date" Display="Dynamic"
                                                                            runat="server" ValidationGroup="btnGo"></asp:RequiredFieldValidator>
                                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="To Date should not be less than From Date"
                                                                            Type="Date" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual"
                                                                            CssClass="cvPCG" Display="Dynamic" ValidationGroup="btnGo"></asp:CompareValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table id="trPeriod" runat="server" style="display: none;">
                                                                <tr runat="server">
                                                                    <td runat="server">
                                                                        <asp:Label ID="lblPeriod" runat="server" CssClass="FieldName">Period: </asp:Label>
                                                                    </td>
                                                                    <td runat="server">
                                                                        <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="cmbField">
                                                                        </asp:DropDownList>
                                                                        <span id="Span4" class="spnRequiredField">*</span>
                                                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlPeriod"
                                                                            CssClass="rfvPCG" ErrorMessage="Please select a Period" Operator="NotEqual" Display="Dynamic"
                                                                            ValueToCompare="Select a Period" ValidationGroup="btnGo"></asp:CompareValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table id="trAsOn" runat="server" style='display: none'>
                                                                <tr runat="server">
                                                                    <td runat="server">
                                                                        <asp:Label ID="lblAsOnDate" runat="server" CssClass="FieldName">As on date: </asp:Label>
                                                                    </td>
                                                                    <td runat="server">
                                                                        <asp:TextBox ID="txtAsOnDate" runat="server" CssClass="txtField"></asp:TextBox><ajaxToolkit:CalendarExtender
                                                                            ID="CalendarExtender1" runat="server" TargetControlID="txtAsOnDate" Format="dd/MM/yyyy"
                                                                            Enabled="True">
                                                                        </ajaxToolkit:CalendarExtender>
                                                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                                                            TargetControlID="txtAsOnDate" WatermarkText="dd/mm/yyyy" Enabled="True">
                                                                        </ajaxToolkit:TextBoxWatermarkExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtAsOnDate"
                                                                            CssClass="rfvPCG" ErrorMessage="<br />Please select an As on Date" Display="Dynamic"
                                                                            runat="server" ValidationGroup="btnGo"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                            <asp:Button ID="btnViewReport" runat="server" Text="View Report" OnClientClick="return validate('')"
                                                                PostBackUrl="~/Reports/Display.aspx?mail=0" CssClass="PCGMediumButton" ValidationGroup="btnView" />&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="tabpnlEmailReports" runat="server" HeaderText="Email Reports"
                                Width="100%">
                                <HeaderTemplate>
                                    Email Reports
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <table width="100%" cellpadding="10">
                                        <tr>
                                            <td>
                                                <table id="tblAsOnDateReports" class="tblSection" width="100%" cellpadding="10">
                                                    <tr>
                                                        <td width="20%">
                                                            <asp:CheckBox ID="chkMFSummary" runat="server" Text="Mutual Fund Summary" class="cmbField" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblPickAsOnDate" runat="server" CssClass="FieldName">Pick As on Date :</asp:Label>&nbsp;&nbsp;<asp:TextBox
                                                                ID="txtEmailAsOnDate" runat="server" CssClass="txtField"></asp:TextBox><ajaxToolkit:CalendarExtender
                                                                    ID="txtEmailAsOnDate_CalendarExtender" runat="server" TargetControlID="txtEmailAsOnDate"
                                                                    Format="dd/MM/yyyy" Enabled="True">
                                                                </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:TextBoxWatermarkExtender ID="txtEmailAsOnDate_TextBoxWatermarkExtender"
                                                                runat="server" TargetControlID="txtEmailAsOnDate" WatermarkText="dd/mm/yyyy"
                                                                Enabled="True">
                                                            </ajaxToolkit:TextBoxWatermarkExtender>
                                                            <asp:RequiredFieldValidator ID="txtEmailAsOnDate_RequiredFieldValidator" ControlToValidate="txtEmailAsOnDate"
                                                                CssClass="rfvPCG" ErrorMessage="<br />Please select a From Date" Display="Dynamic"
                                                                runat="server" ValidationGroup="btnEmail"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:CheckBox ID="chkPortfolioReturns" runat="server" Text="Portfolio Level Returns"
                                                                class="cmbField" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table id="tblDateRangeReports" class="tblSection" width="100%" cellpadding="10">
                                                    <tr>
                                                        <td width="20%">
                                                            <asp:CheckBox ID="chkDividendDetail" runat="server" Text="Dividend Detail" class="cmbField" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdoDateRange" runat="server" GroupName="DatePick" Checked="True"
                                                                class="cmbField" onclick="DisplayEmailDates('DATE_RANGE')" />
                                                            <asp:Label ID="lblEmailDateRangeRdo" runat="server" Text="Pick a Date Range" CssClass="Field"></asp:Label>
                                                            <asp:RadioButton ID="rdoDatePeriod" runat="server" GroupName="DatePick" class="cmbField"
                                                                onclick="DisplayEmailDates('PERIOD')" /><asp:Label ID="lblEmailDatePeriodRdo" runat="server"
                                                                    Text="Pick a Period" CssClass="Field"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%">
                                                            <asp:CheckBox ID="chkTransactionReport" runat="server" Text="Transaction Report"
                                                                class="cmbField" />
                                                        </td>
                                                        <td align="left">
                                                            <table id="tblEmailDateRange" style="display: block">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblEmailFromDate" runat="server" CssClass="FieldName">From:</asp:Label>&nbsp;&nbsp;<asp:TextBox
                                                                            ID="txtEmailFromDate" runat="server" CssClass="txtField"></asp:TextBox><ajaxToolkit:CalendarExtender
                                                                                ID="txtEmailFromDate_CalendarExtender" runat="server" TargetControlID="txtEmailFromDate"
                                                                                Format="dd/MM/yyyy" Enabled="True">
                                                                            </ajaxToolkit:CalendarExtender>
                                                                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtEmailFromDate_TextBoxWatermarkExtender"
                                                                            runat="server" TargetControlID="txtEmailFromDate" WatermarkText="dd/mm/yyyy"
                                                                            Enabled="True">
                                                                        </ajaxToolkit:TextBoxWatermarkExtender>
                                                                        <asp:RequiredFieldValidator ID="txtEmailFromDate_RequiredFieldValidator" ControlToValidate="txtEmailFromDate"
                                                                            CssClass="rfvPCG" ErrorMessage="Please select From Date" Display="Dynamic"
                                                                            runat="server" ValidationGroup="btnEmail"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblEmailToDate" runat="server" CssClass="FieldName">To:</asp:Label>&nbsp;&nbsp;<asp:TextBox
                                                                            ID="txtEmailToDate" runat="server" CssClass="txtField"></asp:TextBox><ajaxToolkit:CalendarExtender
                                                                                ID="txtEmailToDate_CalendarExtender" runat="server" TargetControlID="txtEmailToDate"
                                                                                Format="dd/MM/yyyy" Enabled="True">
                                                                            </ajaxToolkit:CalendarExtender>
                                                                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtEmailToDate_TextBoxWatermarkExtender"
                                                                            runat="server" TargetControlID="txtEmailToDate" WatermarkText="dd/mm/yyyy" Enabled="True">
                                                                        </ajaxToolkit:TextBoxWatermarkExtender>
                                                                        <asp:RequiredFieldValidator ID="txtEmailToDate_RequiredFieldValidator" ControlToValidate="txtEmailToDate"
                                                                            CssClass="rfvPCG" ErrorMessage="Please select To Date" Display="Dynamic"
                                                                            runat="server" ValidationGroup="btnEmail"></asp:RequiredFieldValidator>
                                                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="To Date should not be less than From Date"
                                                                            Type="Date" ControlToValidate="txtEmailToDate" ControlToCompare="txtEmailFromDate"
                                                                            Operator="GreaterThanEqual" CssClass="cvPCG" Display="Dynamic" ValidationGroup="btnEmail"></asp:CompareValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table id="tblEmailDatePeriod" style="display: none">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblEmailDatePeriod" runat="server" CssClass="FieldName">Period: </asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlEmailDatePeriod" runat="server" CssClass="cmbField">
                                                                        </asp:DropDownList>
                                                                        <span id="Span3" class="spnRequiredField">*</span>
                                                                        <asp:CompareValidator ID="ddlEmailDatePeriod_CompareValidator" runat="server" ControlToValidate="ddlEmailDatePeriod"
                                                                            CssClass="rfvPCG" ErrorMessage="<br />Please select a Period" Operator="NotEqual"
                                                                            Display="Dynamic" ValueToCompare="Select a Period" ValidationGroup="btnEmail"></asp:CompareValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:CheckBox ID="chkDividendSummary" runat="server" Text="Dividend Summary" class="cmbField" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:CheckBox ID="chkCapitalGainDetails" runat="server" Text="Capital Gain Details"
                                                                class="cmbField" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:CheckBox ID="chkCapitalGainSummary" runat="server" Text="Capital Gain Summary"
                                                                class="cmbField" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                <asp:Button ID="btnEmailReport" runat="server" Text="Email Report" ValidationGroup="btnEmail"
                                                    PostBackUrl="~/Reports/EmailReport.aspx?mail=1" CssClass="PCGMediumButton" OnClientClick="return validate('mail')" />
                                                &nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                        </ajaxToolkit:TabContainer>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hidFromDate" Value="" runat="server" />
<asp:HiddenField ID="hidToDate" Value="" runat="server" />
<asp:HiddenField ID="hidDateType" Value="" runat="server" />
<asp:HiddenField ID="hidTabIndex" Value="0" runat="server" />
<asp:HiddenField ID="hdnCustomerId" runat="server" OnValueChanged="hdnCustomerId_ValueChanged" />
<asp:HiddenField ID="hdnCustomerId1" runat="server" />

<script type="text/javascript">
    if (document.getElementById("<%= rbtnPickDate.ClientID %>").checked) {
        document.getElementById("<%= rbtnPickDate.ClientID %>").style.display = 'inline';
        document.getElementById("<%= trRange.ClientID %>").style.display = 'block';
        document.getElementById("<%= trPeriod.ClientID %>").style.display = 'none';
        document.getElementById("<%= trAsOn.ClientID %>").style.display = 'none';
    }
    else if (document.getElementById("<%= rbtnPickPeriod.ClientID %>").checked) {
        document.getElementById("<%= rbtnPickDate.ClientID %>").style.display = 'inline';
        document.getElementById("<%= trRange.ClientID %>").style.display = 'none';
        document.getElementById("<%= trPeriod.ClientID %>").style.display = 'block';
        document.getElementById("<%= trAsOn.ClientID %>").style.display = 'none';
    }

    //Code to maintain the state of the customer selection textbox on postback
    if (document.getElementById("<%= rdoGroup.ClientID %>").checked) {
        document.getElementById('trGroupCustomer').style.display = 'block';
        document.getElementById('trIndCustomer').style.display = 'none';
    }
    else {
        document.getElementById('trIndCustomer').style.display = 'block';
        document.getElementById('trGroupCustomer').style.display = 'none';
    }

    ChangeDates();
    
</script>

