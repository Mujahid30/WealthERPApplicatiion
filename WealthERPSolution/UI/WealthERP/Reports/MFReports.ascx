﻿<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="MFReports.ascx.cs" Inherits="WealthERP.Reports.MFReports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="demo" Namespace="DanLudwig.Controls.Web" Assembly="DanLudwig.Controls.AspAjax.ListBox" %>
<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function() {
        $('#ctrl_MFReports_tabViewAndEmailReports_tabpnlViewReports_btnViewReport').bubbletip($('#div1'), { deltaDirection: 'left' });
        $('#ctrl_MFReports_tabViewAndEmailReports_tabpnlViewReports_btnExportToPDF').bubbletip($('#div2'), { deltaDirection: 'left' });
        $('#ctrl_MFReports_tabViewAndEmailReports_tabpnlViewReports_btnViewInDOC').bubbletip($('#div3'), { deltaDirection: 'left' });
        $('#ctrl_MFReports_tabViewAndEmailReports_tabpnlViewReports_btnCustomerViewReport').bubbletip($('#div4'), { deltaDirection: 'left' });
        $('#ctrl_MFReports_tabViewAndEmailReports_tabpnlViewReports_btnCustomerExportToPDF').bubbletip($('#div5'), { deltaDirection: 'left' });
        $('#ctrl_MFReports_tabViewAndEmailReports_tabpnlViewReports_btnCustomerViewInDOC').bubbletip($('#div6'), { deltaDirection: 'left' });
    });
</script>

<script type="text/javascript" language="javascript">


    // Check Number of Customers are more than 50 or not.
    function validate(type) {

        var panel = "";
        var isPorfolioSelected = false;

        //Get all customerId from ListBox For malling
        if (type == 'mail')
            getAllcustomerID(type);



        if (type == 'mail') {
            var ListBox = document.getElementById("<%= LBSelectCustomer.ClientID %>");
            if (ListBox.value == "") {
                alert("Please Select Customers");
                return false;
            }
        }




        //Customer Check in View Report

        if (type == '' || type == 'pdf') {
            if (document.getElementById("<%= rdoGroup.ClientID %>").checked == true) {
                if (document.getElementById("<%= txtParentCustomer.ClientID %>").value == '') {
                    alert("Please select a Customer to view the Report");
                    return false;
                }
            }

            else {
                if (document.getElementById("<%= txtCustomer.ClientID %>").value == '') {
                    alert("Please select a Customer to view the Report");
                    return false;
                }
            }
        }

        //No of customer restriction for Bulk Mail.
        if (type == 'mail') {
            var selectedCustomer = document.getElementById("<%=LBSelectCustomer.ClientID%>");
            var count = selectedCustomer.options.length;
            if (count > 20) {
                alert("You can select only 20 Customers at a time");
                return false;
            }
        }


        //Report selection check
        if (type == 'mail') {

            var chkBoxListRange = document.getElementById("<%=chkRangeReportList.ClientID%>");
            var chkBoxCountRange = chkBoxListRange.getElementsByTagName("input");
            var boolCheck = 'N';
            for (var i = 0; i < chkBoxCountRange.length; i++) {

                if (chkBoxCountRange[i].checked == true) {
                    boolCheck = 'Y';
                    break;
                }
                continue;
            }
            if (boolCheck == 'N') {
                var chkBoxListAsOn = document.getElementById("<%=chkAsOnReportList.ClientID%>");
                var chkBoxCountAsOn = chkBoxListAsOn.getElementsByTagName("input");
                for (var i = 0; i < chkBoxCountAsOn.length; i++) {

                    if (chkBoxCountAsOn[i].checked == true) {
                        boolCheck = 'Y';
                        break;
                    }
                    continue;
                }
            }
            

            if (boolCheck == 'N') {
                alert("Please select at lease one report");
                return false;
            } else return true;
                     
            
        }



        //Customer name check
        if (document.getElementById("<%= hndCustomerLogin.ClientID %>").value == '') {

            if (type == '' || type == 'pdf') {
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

            }
        }

        //Date validation.

        dateType = document.getElementById("<%= hidDateType.ClientID %>").value;

        if (type == 'mail') {
            if (
            "ctrl_MFReports_tabViewAndEmailReports_tabpnlEmailReports_chkAsOnReportList_0".checked == true
            || "ctrl_MFReports_tabViewAndEmailReports_tabpnlEmailReports_chkAsOnReportList_1".checked == true
            || "ctrl_MFReports_tabViewAndEmailReports_tabpnlEmailReports_chkAsOnReportList_2".checked == true
            || "ctrl_MFReports_tabViewAndEmailReports_tabpnlEmailReports_chkAsOnReportList_3".checked == true
            || "ctrl_MFReports_tabViewAndEmailReports_tabpnlEmailReports_chkAsOnReportList_4".checked == true

            ) {
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
            if (
            "ctrl_MFReports_tabViewAndEmailReports_tabpnlEmailReports_chkRangeReportList_1".checked == true
            || "ctrl_MFReports_tabViewAndEmailReports_tabpnlEmailReports_chkRangeReportList_2".checked == true
            || "ctrl_MFReports_tabViewAndEmailReports_tabpnlEmailReports_chkRangeReportList_0".checked == true
            || "ctrl_MFReports_tabViewAndEmailReports_tabpnlEmailReports_chkRangeReportList_4".checked == true
            || "ctrl_MFReports_tabViewAndEmailReports_tabpnlEmailReports_chkRangeReportList_3".checked == true) {
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
                if (document.getElementById("<%= hndCustomerLogin.ClientID  %>").value == 'true') {
                    dateVal1 = 2;
                }
                else
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





        if (type == 'mail') {
            return true;
        }
        else if (type == 'pdf') {
            window.document.forms[0].target = '_blank';
            window.document.forms[0].action = "/Reports/Display.aspx?mail=2";
        }
        else if (type == 'doc') {
            window.document.forms[0].target = '_blank';
            window.document.forms[0].action = "/Reports/Display.aspx?mail=4";
        }
        else {
            window.document.forms[0].target = '_blank';
            window.document.forms[0].action = "/Reports/Display.aspx?mail=0";
        }

        setTimeout(function() {
            window.document.forms[0].target = '';
            window.document.forms[0].action = "ControlHost.aspx?pageid=MFReports";
        }, 500);
        return true;
    }

    function GetCustomerId(source, eventArgs) {

        document.getElementById("<%= hdnCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    };



    //**********Customer Login MF Report Validation For ViewReport and Export To PDF Button
    function CustomerValidate(type) {
        if (type == 'view') {
            dateType = document.getElementById("<%= hidDateType.ClientID %>").value;
            if (dateType == 'PERIOD') {
                dateVal = document.getElementById("<%= ddlPeriod.ClientID  %>").selectedIndex;
                if (dateVal < 1) {
                    alert("Please select a period")
                    return false;
                }
            }
            window.document.forms[0].target = '_blank';
            window.document.forms[0].action = "/Reports/Display.aspx?mail=3";
        }
        else if (type == 'doc') {
            dateType = document.getElementById("<%= hidDateType.ClientID %>").value;
            if (dateType == 'PERIOD') {
                dateVal = document.getElementById("<%= ddlPeriod.ClientID  %>").selectedIndex;
                if (dateVal < 1) {
                    alert("Please select a period")
                    return false;
                }
            }
            window.document.forms[0].target = '_blank';
            window.document.forms[0].action = "/Reports/Display.aspx?mail=4";
        }
        else {
            dateType = document.getElementById("<%= hidDateType.ClientID %>").value;
            if (dateType == 'PERIOD') {
                dateVal = document.getElementById("<%= ddlPeriod.ClientID  %>").selectedIndex;
                if (dateVal < 1) {
                    alert("Please select a period")
                    return false;
                }
            }
            window.document.forms[0].target = '_blank';
            window.document.forms[0].action = "/Reports/Display.aspx?mail=2";
        }

        setTimeout(function() {
            window.document.forms[0].target = '';
            window.document.forms[0].action = "ControlHost.aspx?pageid=MFReports";
        }, 500);
        return true;

    }


    //.........................................................................
    function DisplayDates(type) {


        if (type == 'DATE_RANGE') {
            document.getElementById("<%= rbtnPickDate.ClientID %>").checked = true;
            document.getElementById("<%= rbtnPickDate.ClientID %>").checked = true;
            document.getElementById("tblPickDate").style.display = 'block';
            document.getElementById("<%= trRange.ClientID %>").style.display = 'block';
            document.getElementById("<%= trPeriod.ClientID %>").style.display = 'none';
            document.getElementById("<%= trAsOn.ClientID %>").style.display = 'none';
            document.getElementById("<%= lblNote1.ClientID %>").style.display = 'block';
            document.getElementById("<%= lblNote2.ClientID %>").style.display = 'none';

            var dropdown = document.getElementById("<%= ddlReportSubType.ClientID %>");
            selectedReport = dropdown.options[dropdown.selectedIndex].value

            if (selectedReport == 'TRANSACTION_REPORT') {

                document.getElementById("<%= trTranFilter1.ClientID %>").style.display = 'block';
                document.getElementById("<%= trTranFilter2.ClientID %>").style.display = 'block';
            }
            else {
                document.getElementById("<%= trTranFilter1.ClientID %>").style.display = 'none';
                document.getElementById("<%= trTranFilter2.ClientID %>").style.display = 'none';

            }
        }
        else if (type == 'PERIOD') {
            document.getElementById("tblPickDate").style.display = 'block';
            document.getElementById("<%= trRange.ClientID %>").style.display = 'none';
            document.getElementById("<%= trPeriod.ClientID %>").style.display = 'block';
            document.getElementById("<%= trAsOn.ClientID %>").style.display = 'none';
            document.getElementById("<%= lblNote1.ClientID %>").style.display = 'block';
            document.getElementById("<%= lblNote2.ClientID %>").style.display = 'none';
        }
        else if (type == 'AS_ON') {
            document.getElementById("<%= trAsOn.ClientID %>").style.display = 'block';
            document.getElementById("tblPickDate").style.display = 'none';
            document.getElementById("<%= trRange.ClientID %>").style.display = 'none';
            document.getElementById("<%= trPeriod.ClientID %>").style.display = 'none';
            document.getElementById("<%= lblNote1.ClientID %>").style.display = 'none';
            document.getElementById("<%= lblNote2.ClientID %>").style.display = 'block';


        }
        document.getElementById("<%= hidDateType.ClientID %>").value = type;
    };



    //Storing Selected Customer from ListBox For E-Mailing in Hiddenfiled
    function getAllcustomerID(type) {

        if (type == 'mail') {
            var listBoxRef = document.getElementById("<%= LBSelectCustomer.ClientID %>");
            var functionReturn = '';
            for (var i = 0; i < listBoxRef.options.length; i++) {


                // If you want the value property use this:
                functionReturn += listBoxRef.options[i].value;
                //add separator
                functionReturn += ',';
            }

            document.getElementById("<%= SelectedCustomets4Email.ClientID %>").value = functionReturn;


        }
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
        arr["COMPREHENSIVE"] = "AS_ON";
        arr["RETURNS_PORTFOLIO_REALIZED"] = "AS_ON";
        arr["ELIGIBLE_CAPITAL_GAIN_DETAILS"] = "AS_ON";
        arr["ELIGIBLE_CAPITAL_GAIN_SUMMARY"] = "AS_ON";
        arr["TRANSACTION_REPORT"] = "DATE_RANGE";
        arr["TRANSACTION_REPORT_OPEN_CLOSE_BALANCE"] = "DATE_RANGE";
        arr["DIVIDEND_STATEMENT"] = "DATE_RANGE";
        arr["DIVIDEND_SUMMARY"] = "DATE_RANGE";
        arr["CAPITAL_GAIN_SUMMARY"] = "DATE_RANGE";
        arr["CAPITAL_GAIN_DETAILS"] = "DATE_RANGE";
        arr["REALIZED_REPORT"] = "AS_ON";
        arr["COMPOSITION_REPORT"] = "AS_ON";

        var dropdown = document.getElementById("<%= ddlReportSubType.ClientID %>");
        selectedReport = dropdown.options[dropdown.selectedIndex].value



        if (selectedReport == 'TRANSACTION_REPORT') {

            document.getElementById("<%= trTranFilter1.ClientID %>").style.display = 'block';
            document.getElementById("<%= trTranFilter2.ClientID %>").style.display = 'block';
        }
        else {
            document.getElementById("<%= trTranFilter1.ClientID %>").style.display = 'none';
            document.getElementById("<%= trTranFilter2.ClientID %>").style.display = 'none';

        }

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

    //Advisor Login MFreport RadioButton Selection change Validation...................

    function ChangeCustomerSelectionTextBox(value) {
        switch (value) {
            case 'rdoGroup':
                {
                    document.getElementById("<%= trGroupCustomer.ClientID %>").style.display = 'block';
                    document.getElementById("<%= trIndCustomer.ClientID %>").style.display = 'none';
                    document.getElementById("<%= rbnAllCustomer.ClientID %>").style.display = 'none';
                    document.getElementById("<%= txtParentCustomer.ClientID %>").value = '';
                    break;
                }
            case 'rdoIndividual':
                {
                    document.getElementById("<%= trGroupCustomer.ClientID %>").style.display = 'none';
                    document.getElementById("<%= trIndCustomer.ClientID %>").style.display = 'block';
                    document.getElementById("<%= rbnAllCustomer.ClientID %>").style.display = 'none';
                    document.getElementById("<%= txtParentCustomer.ClientID %>").value = '';
                    break;
                }
            case 'chkCustomer':
                {
                    document.getElementById("<%= trGroupCustomer.ClientID %>").style.display = 'none';
                    document.getElementById("<%= trIndCustomer.ClientID %>").style.display = 'none';
                    document.getElementById("<%= rbnAllCustomer.ClientID %>").style.display = 'block';
                    document.getElementById("<%= txtParentCustomer.ClientID %>").value = '';
                    break;
                }
            default:
                {
                    document.getElementById("<%= trGroupCustomer.ClientID %>").style.display = 'block';
                    document.getElementById("<%= trIndCustomer.ClientID %>").style.display = 'none';
                    document.getElementById("<%= rbnAllCustomer.ClientID %>").style.display = 'none';
                    document.getElementById("<%= txtParentCustomer.ClientID %>").value = '';
                }

        }
        document.getElementById("<%= trCustomerDetails.ClientID %>").style.display = 'none';
        document.getElementById("<%= divGroupCustomers.ClientID %>").innerHTML = '';
        document.getElementById("<%= divPortfolios.ClientID %>").innerHTML = '';

    }

    //Customer Login MFreport RadioButton Selection change Validation

    function ChangeGroupOrSelf(value) {

        var temp = value;

        if (temp == 'rdoCustomerGroup') {

            document.getElementById("<%= divGroupCustomers.ClientID %>").style.display = 'block';
            document.getElementById("<%= hndSelfOrGroup.ClientID %>").value = '';

        }
        else {
            document.getElementById("<%= divGroupCustomers.ClientID %>").style.display = 'none';
            document.getElementById("<%= hndSelfOrGroup.ClientID %>").value = 'self';


        }



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
    .clearfix:after
    {
        content: ".";
        display: block;
        height: 0;
        clear: both;
        visibility: hidden;
    }
    /* all borwsers see this, IE-mac included */.clearfix
    {
        display: block;
    }
    /* Hide this style from IE-mac \*/* html .clearfix
    {
        height: 1%; /* apply to IE-win*/
    }
    /* End hide from IE-mac */body
    {
        font-family: Arial;
    }
    a:link, a:visited
    {
        color: Blue;
        text-decoration: underline;
    }
    a:hover
    {
        text-decoration: none;
    }
</style>
<style type="text/css">
    .tblSection
    {
        border: solid 1px #B5C7DE;
        height: 5px;
    }
    input
    {
        display: inline;
    }
    .style1
    {
        width: 620px;
    }
    .ddlReportType
    {
        font-family: Verdana,Tahoma;
        font-weight: normal;
        font-size: x-small;
        color: #16518A;
        width: 175px; /*margin-left: 0px;*/
    }
</style>
<table border="0" width="100%" height="100%" style="">
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
        <td colspan="2">
            <asp:Label ID="Label7" runat="server" CssClass="HeaderTextSmall" Text="MF Reports"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td width="100%">
            <table width="100%" cellpadding="0">
                <tr>
                    <td>
                        <ajaxToolkit:TabContainer ID="tabViewAndEmailReports" runat="server" ActiveTabIndex="1"
                            Width="100%" Style="visibility: visible" OnClientActiveTabChanged="OnChanged">
                            <ajaxToolkit:TabPanel ID="tabpnlViewReports" runat="server" HeaderText="View Reports"
                                Width="100%">
                                <HeaderTemplate>
                                    View Reports</HeaderTemplate>
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td width="100%">
                                                <table width="100%">
                                                    <tr id="trAdminRM" runat="server">
                                                        <td colspan="2" align="right">
                                                            <asp:Button ID="btnExportToPDF" runat="server" OnClientClick="return validate('pdf')"
                                                                PostBackUrl="~/Reports/Display.aspx?mail=2" CssClass="PDFButton" />&nbsp;&nbsp;
                                                            <div id="div2" style="display: none;">
                                                                <p class="tip">
                                                                    Click here to view MF report in pdf format.
                                                                </p>
                                                            </div>
                                                            <asp:Button ID="btnViewReport" runat="server" OnClientClick="return validate('')"
                                                                PostBackUrl="~/Reports/Display.aspx?mail=0" CssClass="CrystalButton" ValidationGroup="btnView" />&nbsp;&nbsp;
                                                            <div id="div1" style="display: none;">
                                                                <p class="tip">
                                                                    Click here to view Portfolio report.
                                                                </p>
                                                            </div>
                                                            <asp:Button ID="btnViewInDOC" runat="server" CssClass="DOCButton" OnClientClick="return validate('doc')"
                                                                PostBackUrl="~/Reports/Display.aspx?mail=4" />&nbsp;&nbsp;
                                                            <div id="div3" style="display: none;">
                                                                <p class="tip">
                                                                    Click here to view MF report in word doc.</p>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr id="trCustomer" runat="server">
                                                        <td colspan="2" align="right">
                                                            <asp:Button ID="btnCustomerExportToPDF" runat="server" OnClientClick="return CustomerValidate('pdf')"
                                                                PostBackUrl="~/Reports/Display.aspx?mail=2" CssClass="PDFButton" />&nbsp;&nbsp;
                                                            <div id="div5" style="display: none;">
                                                                <p class="tip">
                                                                    Click here to view MF report in pdf format.
                                                                </p>
                                                            </div>
                                                            <asp:Button ID="btnCustomerViewReport" runat="server" OnClientClick="return CustomerValidate('view')"
                                                                PostBackUrl="~/Reports/Display.aspx?mail=3" CssClass="CrystalButton" ValidationGroup="btnView" />&nbsp;&nbsp;
                                                            <div id="div4" style="display: none;">
                                                                <p class="tip">
                                                                    Click here to view MF report.
                                                                </p>
                                                            </div>
                                                            <asp:Button ID="btnCustomerViewInDOC" runat="server" CssClass="DOCButton" OnClientClick="return CustomerValidate('doc')"
                                                                PostBackUrl="~/Reports/Display.aspx?mail=4" />&nbsp;&nbsp;
                                                            <div id="div6" style="display: none;">
                                                                <p class="tip">
                                                                    Click here to view MF report in word doc.</p>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr id="trCustomerName" runat="server">
                                                        <td runat="server" colspan="2">
                                                            <asp:Label ID="lblCustomerName" CssClass="HeaderTextSmall" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="trAdvisorRadioList" runat="server">
                                                        <td runat="server">
                                                            <asp:Label ID="lblGrpOrInd" runat="server" CssClass="HeaderTextSmall" Style='font-weight: normal;'
                                                                Text="Generate report for :"></asp:Label>
                                                            <asp:RadioButton runat="server" ID="rdoGroup" Text="Group" Class="cmbField" GroupName="GrpOrInd"
                                                                Checked="True" onClick="ChangeCustomerSelectionTextBox('Group')" />
                                                            <asp:RadioButton runat="server" ID="rdoIndividual" Text="Individual" Class="cmbField"
                                                                GroupName="GrpOrInd" onClick="return ChangeCustomerSelectionTextBox('Individual')" />
                                                        </td>
                                                        <td runat="server">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr id="trCustomerRadioList" runat="server">
                                                        <td runat="server">
                                                            <asp:Label ID="lblCustometGroupOrInd" runat="server" CssClass="HeaderTextSmall" Style='font-weight: normal;'
                                                                Text="Generate report for :"></asp:Label>
                                                            <asp:RadioButton runat="server" ID="rdoCustomerGroup" Text="Group" Class="cmbField"
                                                                GroupName="GrpOrSelf" onClick="ChangeGroupOrSelf('group')" />
                                                            <asp:RadioButton runat="server" ID="rdoCustomerIndivisual" Text="Self" Class="cmbField"
                                                                GroupName="GrpOrSelf" onClick="return ChangeGroupOrSelf('Individual')" />
                                                        </td>
                                                        <td runat="server">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr id="trIndCustomer" style="display: none" runat="server">
                                                        <td runat="server">
                                                            <asp:Label ID="lblCustomer" runat="server" Text="Select Customer:" CssClass="FieldName"></asp:Label><asp:TextBox
                                                                ID="txtCustomer" runat="server" CssClass="txtField" AutoComplete="Off" AutoPostBack="True"></asp:TextBox>
                                                            <ajaxToolkit:AutoCompleteExtender ID="txtCustomer_autoCompleteExtender" runat="server"
                                                                TargetControlID="txtCustomer" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                                                                MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                                                                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                                                                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                                                                UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                                                                Enabled="True" />
                                                            <span id="Span1" class="spnRequiredField">* </span>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCustomer"
                                                                ErrorMessage="<br />Please Enter Customer Name" Display="Dynamic" runat="server"
                                                                CssClass="rfvPCG" ValidationGroup="btnSubmit"></asp:RequiredFieldValidator><span
                                                                    style='font-size: 9px; font-weight: normal' class='FieldName'><br />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    Enter few characters of Individual customer name. </span>
                                                        </td>
                                                        <td runat="server">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr id="trGroupCustomer" runat="server">
                                                        <td runat="server">
                                                            <asp:Label ID="lblGCustomer" runat="server" Text="Select Customer:" CssClass="FieldName"></asp:Label><asp:TextBox
                                                                ID="txtParentCustomer" runat="server" CssClass="txtField" AutoComplete="Off"
                                                                AutoPostBack="True"></asp:TextBox>
                                                            <ajaxToolkit:AutoCompleteExtender ID="txtParentCustomer_autoCompleteExtender" runat="server"
                                                                TargetControlID="txtParentCustomer" ServiceMethod="GetParentCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                                                                MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                                                                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                                                                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                                                                UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                                                                Enabled="True" />
                                                            <span id="Span2" class="spnRequiredField">* </span>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtCustomer"
                                                                ErrorMessage="<br />Please Enter Customer Name" Display="Dynamic" runat="server"
                                                                CssClass="rfvPCG" ValidationGroup="btnSubmit"></asp:RequiredFieldValidator><span
                                                                    style='font-size: 9px; font-weight: normal' class='FieldName'><br />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    Enter few characters of Group customer name. </span>
                                                        </td>
                                                        <td runat="server">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                                <tr id="trCustomerDetails" runat="server" style="display: none">
                                                    <td id="Td1" runat="server" class="style1">
                                                        <asp:Label ID="lblPAN" runat="server" CssClass="FieldName" Text="PAN: "></asp:Label><asp:TextBox
                                                            ID="txtCustomerPAN" runat="server" BackColor="Transparent" BorderStyle="None"
                                                            CssClass="txtField"></asp:TextBox>
                                                    </td>
                                                    <td id="Td2" runat="server">
                                                        <asp:Label ID="lblAddress" runat="server" CssClass="FieldName" Text="Address: " Visible="False"></asp:Label><asp:TextBox
                                                            ID="txtCustomerAddress" runat="server" BackColor="Transparent" BorderStyle="None"
                                                            CssClass="txtField"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="tr1" runat="server">
                                                    <td id="Td3" runat="server" class="style1">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio: "></asp:Label><asp:DropDownList
                                                            ID="ddlPortfolioGroup" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlPortfolioGroup_SelectedIndexChanged"
                                                            AutoPostBack="True">
                                                            <asp:ListItem Text="Managed" Value="MANAGED" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="UnManaged" Value="UN_MANAGED"></asp:ListItem>
                                                            <asp:ListItem Text="All" Value="ALL"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="Field" align="left" colspan="2">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="ddlPortfolioGroup" EventName="SelectedIndexChanged">
                                                                </asp:AsyncPostBackTrigger>
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
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                <table width="100%" class="tblSection" cellpadding="5" border="0">
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label2" runat="server" CssClass="HeaderTextSmall" Style='font-weight: normal;'
                                                                Text="Select Report Type "></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="13%" align="right">
                                                            <asp:Label ID="Label4" runat="server" Text="Report Type:" CssClass="FieldName"></asp:Label>
                                                        </td>
                                                        <td align="left" width="87%">
                                                            <asp:DropDownList ID="ddlReportSubType" runat="server" CssClass="ddlReportType" onChange="ChangeDates()">
                                                                <asp:ListItem Text="Mutual Fund Summary" Value="CATEGORY_WISE" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Portfolio Returns-Holding" Value="RETURNS_PORTFOLIO"></asp:ListItem>
                                                                <asp:ListItem Text="Comprehensive Report" Value="COMPREHENSIVE"></asp:ListItem>
                                                                <asp:ListItem Text="Transaction Report" Value="TRANSACTION_REPORT"></asp:ListItem>
                                                                <asp:ListItem Text="Dividend Statement" Value="DIVIDEND_STATEMENT"></asp:ListItem>
                                                                <asp:ListItem Text="Dividend Summary" Value="DIVIDEND_SUMMARY"></asp:ListItem>
                                                                <asp:ListItem Text="Capital Gain Summary" Value="CAPITAL_GAIN_SUMMARY"></asp:ListItem>
                                                                <asp:ListItem Text="Capital Gain Details" Value="CAPITAL_GAIN_DETAILS"></asp:ListItem>
                                                                <asp:ListItem Text="Eligible Capital Gain Details" Value="ELIGIBLE_CAPITAL_GAIN_DETAILS"></asp:ListItem>
                                                                <asp:ListItem Text="Eligible Capital Gain Summary" Value="ELIGIBLE_CAPITAL_GAIN_SUMMARY"></asp:ListItem>
                                                                <asp:ListItem Text="Closing Balance Report" Value="TRANSACTION_REPORT_OPEN_CLOSE_BALANCE"></asp:ListItem>
                                                                <asp:ListItem Text="Realized Report" Value="REALIZED_REPORT"></asp:ListItem>
                                                                <asp:ListItem Text="Portfolio Composition Report" Value="COMPOSITION_REPORT"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            <table width="100%">
                                                                <tr id="trTranFilter1" runat="server" style="display: none">
                                                                    <td id="Td4" runat="server" align="right">
                                                                        <asp:Label ID="lblsortby" runat="server" CssClass="FieldName" Text="Sort by:"></asp:Label>
                                                                    </td>
                                                                    <td id="Td5" runat="server" align="left">
                                                                        <asp:RadioButton ID="rddate" runat="server" CssClass="cmbField" GroupName="Transation"
                                                                            Text="Date" />
                                                                        <asp:RadioButton ID="rdScheme" runat="server" CssClass="cmbField" GroupName="Transation"
                                                                            Text="Scheme/Folio" />
                                                                    </td>
                                                                </tr>
                                                                <tr id="trTranFilter2" runat="server" style="display: none">
                                                                    <td id="Td6" runat="server" align="right">
                                                                        <asp:Label ID="lblFilterBy" runat="server" CssClass="FieldName" Text="Filter by:"></asp:Label>
                                                                    </td>
                                                                    <td id="Td7" runat="server" align="left">
                                                                        <asp:DropDownList ID="ddlMFTransactionType" runat="server" CssClass="cmbField">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
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
                                                            <table id="trRange" runat="server" style="display: none">
                                                                <tr runat="server">
                                                                    <td runat="server">
                                                                        <asp:Label ID="lblFromDate" runat="server" CssClass="FieldName">From:</asp:Label>&nbsp;&nbsp;
                                                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtField"></asp:TextBox><ajaxToolkit:CalendarExtender
                                                                            ID="txtFromDate_CalendarExtender" runat="server" TargetControlID="txtFromDate"
                                                                            Format="dd/MM/yyyy" Enabled="True" PopupPosition="TopRight">
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
                                                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="txtField"></asp:TextBox><ajaxToolkit:CalendarExtender
                                                                            ID="txtToDate_CalendarExtender" runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy"
                                                                            Enabled="True" PopupPosition="TopRight">
                                                                        </ajaxToolkit:CalendarExtender>
                                                                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtToDate_TextBoxWatermarkExtender" runat="server"
                                                                            TargetControlID="txtToDate" WatermarkText="dd/mm/yyyy" Enabled="True">
                                                                        </ajaxToolkit:TextBoxWatermarkExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtToDate"
                                                                            CssClass="rfvPCG" ErrorMessage="<br />Please select a To Date" Display="Dynamic"
                                                                            runat="server" ValidationGroup="btnGo"></asp:RequiredFieldValidator><asp:CompareValidator
                                                                                ID="CompareValidator1" runat="server" ErrorMessage="To Date should not be less than From Date"
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
                                                                            CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please select a Period" Operator="NotEqual"
                                                                            ValidationGroup="btnGo" ValueToCompare="Select a Period"></asp:CompareValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table id="trAsOn" runat="server" style='display: none'>
                                                                <tr runat="server">
                                                                    <td runat="server">
                                                                        <asp:Label ID="lblAsOnDate" runat="server" CssClass="FieldName">As on date: </asp:Label>
                                                                    </td>
                                                                    <td runat="server">
                                                                        <asp:TextBox ID="txtAsOnDate" OnTextChanged="ChckBussDate_Textchanged" AutoPostBack="true"
                                                                            runat="server" CssClass="txtField"></asp:TextBox><ajaxToolkit:CalendarExtender ID="CalendarExtender1"
                                                                                runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtAsOnDate">
                                                                            </ajaxToolkit:CalendarExtender>
                                                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                                                            Enabled="True" TargetControlID="txtAsOnDate" WatermarkText="dd/mm/yyyy">
                                                                        </ajaxToolkit:TextBoxWatermarkExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAsOnDate"
                                                                            CssClass="rfvPCG" Display="Dynamic" ErrorMessage="&lt;br /&gt;Please select an As on Date"
                                                                            ValidationGroup="btnGo"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <%--<tr id="trAdminRM" runat="server">
                                                        <td class="style1" runat="server">
                                                            <asp:Button ID="btnViewReport" runat="server" Text="View Report" OnClientClick="return validate('')"
                                                                PostBackUrl="~/Reports/Display.aspx?mail=0" CssClass="PCGMediumButton" ValidationGroup="btnView" />&nbsp;&nbsp;
                                                        </td>
                                                        <td class="style1" runat="server">
                                                            <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" OnClientClick="return validate('pdf')"
                                                                PostBackUrl="~/Reports/Display.aspx?mail=2" CssClass="PCGMediumButton" />&nbsp;&nbsp;
                                                        </td>
                                                    </tr>--%>
                                                    <%-- <tr id="trCustomer" runat="server">
                                                        <td class="style1" runat="server">
                                                            <asp:Button ID="btnCustomerViewReport" runat="server" Text="View Report" OnClientClick="return CustomerValidate('view')"
                                                                PostBackUrl="~/Reports/Display.aspx?mail=3" CssClass="PCGMediumButton" ValidationGroup="btnView" />&nbsp;&nbsp;
                                                        </td>
                                                        <td class="style1" runat="server">
                                                            <asp:Button ID="btnCustomerExportToPDF" runat="server" Text="Export To PDF" OnClientClick="return CustomerValidate('pdf')"
                                                                PostBackUrl="~/Reports/Display.aspx?mail=2" CssClass="PCGMediumButton" />&nbsp;&nbsp;
                                                        </td>
                                                    </tr>--%>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="tabpnlEmailReports" runat="server" HeaderText="Email Reports"
                                Width="100%">
                                <HeaderTemplate>
                                    Email Reports</HeaderTemplate>
                                <ContentTemplate>
                                    <table width="100%" height="100%" cellpadding="5">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" CssClass="HeaderTextSmall" Style='font-weight: normal;'
                                                    Text="E-Mail report for :"></asp:Label><asp:RadioButton runat="server" ID="rbnGroup"
                                                        Text="Group" Class="cmbField" GroupName="EmailGrpOrInd" Checked="True" OnCheckedChanged="rbnGroup_CheckedChanged"
                                                        AutoPostBack="True" /><asp:RadioButton runat="server" ID="rbnIndivisual" Text="Individual Without Group"
                                                            Class="cmbField" GroupName="EmailGrpOrInd" OnCheckedChanged="rbnIndivisual_CheckedChanged"
                                                            AutoPostBack="True" />
                                                <tr>
                                                    <td>
                                                        <asp:RadioButton runat="server" ID="rbnAllCustomer" AutoPostBack="True" Text="All Individual"
                                                            value="chkCustomer" GroupName="EmailGrpOrInd" CssClass="cmbField" OnCheckedChanged="rbnAllCustomer_CheckedChanged" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="clearfix" style="margin-bottom: 1em;">
                                                            <asp:Panel ID="PLCustomer" runat="server" DefaultButton="AddSelected" Style="float: left">
                                                                <asp:Label ID="lblSelectCustomer" runat="server" CssClass="HeaderTextSmall" Text="Select Customer"></asp:Label>
                                                                <asp:UpdatePanel ID="UPCustomer" runat="server">
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="AddSelected" EventName="Click" />
                                                                        <asp:AsyncPostBackTrigger ControlID="RemoveSelected" EventName="Click" />
                                                                        <asp:AsyncPostBackTrigger ControlID="SelectAll" EventName="Click" />
                                                                        <asp:AsyncPostBackTrigger ControlID="RemoveAll" EventName="Click" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <demo:ListBox ID="LBCustomer" runat="server" CssClass="DemoListBox" HorizontalScrollEnabled="true"
                                                                            Rows="12" ScrollStateEnabled="true" SelectionMode="Multiple">
                                                                        </demo:ListBox>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </asp:Panel>
                                                            <div style="float: left; margin: 0.5em; width: 138px; font-size: 0.9em; text-align: center;">
                                                                <p>
                                                                    <asp:Button ID="AddSelected" runat="server" CssClass="PCGButton" Font-Bold="True"
                                                                        OnClick="AddSelected_Click" Text="&gt;" />
                                                                </p>
                                                                <p>
                                                                    <asp:Button ID="RemoveSelected" runat="server" CssClass="PCGButton" Font-Bold="True"
                                                                        OnClick="RemoveSelected_Click" Text="&lt;" />
                                                                </p>
                                                                <p>
                                                                    <asp:Button ID="SelectAll" runat="server" CssClass="PCGButton" Font-Bold="True" OnClick="SelectAll_Click"
                                                                        Text="&gt;&gt;" />
                                                                </p>
                                                                <p>
                                                                    <asp:Button ID="RemoveAll" runat="server" CssClass="PCGButton" Font-Bold="True" OnClick="RemoveAll_Click"
                                                                        Text="&lt;&lt;" />
                                                                </p>
                                                            </div>
                                                            <asp:Panel ID="PLSelectCustomer" runat="server" DefaultButton="RemoveSelected" Style="float: left">
                                                                <asp:Label ID="lblSelectedCustomer" runat="server" CssClass="HeaderTextSmall" Text="Selected Customers"></asp:Label>
                                                                <asp:UpdatePanel ID="UPSelectCustomer" runat="server">
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="AddSelected" EventName="Click" />
                                                                        <asp:AsyncPostBackTrigger ControlID="RemoveSelected" EventName="Click" />
                                                                        <asp:AsyncPostBackTrigger ControlID="SelectAll" EventName="Click" />
                                                                        <asp:AsyncPostBackTrigger ControlID="RemoveAll" EventName="Click" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <demo:ListBox ID="LBSelectCustomer" runat="server" CssClass="DemoListBox" HorizontalScrollEnabled="true"
                                                                            Rows="12" ScrollStateEnabled="true" SelectionMode="Multiple">
                                                                        </demo:ListBox>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </asp:Panel>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table cellpadding="0" width="100%">
                                                            <tr>
                                                                <td width="40%">
                                                                    <table id="tblAsOnDateReports" cellpadding="8" cellpadding="0" class="tblSection"
                                                                        width="100%">
                                                                        <tr>
                                                                            <td width="100%">
                                                                                <asp:Label ID="lblPickAsOnDate" runat="server" CssClass="FieldName" Text="Pick As on Date :"></asp:Label>
                                                                                <asp:TextBox ID="txtEmailAsOnDate" runat="server" OnTextChanged="ChckBussDate_Textchanged"
                                                                                    AutoPostBack="True" CssClass="txtField"></asp:TextBox>
                                                                                <ajaxToolkit:CalendarExtender ID="txtEmailAsOnDate_CalendarExtender" runat="server"
                                                                                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtEmailAsOnDate">
                                                                                </ajaxToolkit:CalendarExtender>
                                                                                <ajaxToolkit:TextBoxWatermarkExtender ID="txtEmailAsOnDate_TextBoxWatermarkExtender"
                                                                                    runat="server" Enabled="True" TargetControlID="txtEmailAsOnDate" WatermarkText="dd/mm/yyyy">
                                                                                </ajaxToolkit:TextBoxWatermarkExtender>
                                                                                <asp:RequiredFieldValidator ID="txtEmailAsOnDate_RequiredFieldValidator" runat="server"
                                                                                    ControlToValidate="txtEmailAsOnDate" CssClass="rfvPCG" Display="Dynamic" ErrorMessage="&lt;br /&gt;Please select a From Date"
                                                                                    ValidationGroup="btnEmail"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="100%">
                                                                                <asp:CheckBoxList ID="chkAsOnReportList" runat="server" class="cmbField" Width="100%">
                                                                                    <asp:ListItem Text="Mutual Fund Summary" Value="CATEGORY_WISE"></asp:ListItem>
                                                                                    <asp:ListItem Text="Portfolio Return-Holdoing" Value="RETURNS_PORTFOLIO"></asp:ListItem>
                                                                                    <asp:ListItem Text="Comprehensive" Value="COMPREHENSIVE"></asp:ListItem>
                                                                                    <asp:ListItem Text="Eligible Capital Gain Details" Value="ELIGIBLE_CAPITAL_GAIN_DETAILS"></asp:ListItem>
                                                                                    <asp:ListItem Text="Eligible Capital Gains Summary" Value="ELIGIBLE_CAPITAL_GAIN_SUMMARY"></asp:ListItem>
                                                                                    <asp:ListItem Text="Portfolio Composition" Value="COMPOSITION_REPORT"></asp:ListItem>
                                                                                    <asp:ListItem Text="Realized" Value="REALIZED_REPORT"></asp:ListItem>
                                                                                </asp:CheckBoxList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td width="60%">
                                                                    <table id="tblDateRangeReports" cellpadding="8" cellspacing="1" class="tblSection"
                                                                        width="100%">
                                                                        <tr>
                                                                            <td width="100%">
                                                                                <asp:RadioButton ID="rdoDateRange" runat="server" Checked="True" class="cmbField"
                                                                                    GroupName="DatePick" onclick="DisplayEmailDates('DATE_RANGE')" />
                                                                                <asp:Label ID="lblEmailDateRangeRdo" runat="server" CssClass="Field" Text="Pick a Date Range"></asp:Label>
                                                                                <asp:RadioButton ID="rdoDatePeriod" runat="server" class="cmbField" GroupName="DatePick"
                                                                                    onclick="DisplayEmailDates('PERIOD')" />
                                                                                <asp:Label ID="lblEmailDatePeriodRdo" runat="server" CssClass="Field" Text="Pick a Period"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="100%">
                                                                                <table id="tblEmailDateRange" style="display: block">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lblEmailFromDate" runat="server" CssClass="FieldName" Text="From"></asp:Label>
                                                                                            <asp:TextBox ID="txtEmailFromDate" runat="server" CssClass="txtField"></asp:TextBox>
                                                                                            <ajaxToolkit:CalendarExtender ID="txtEmailFromDate_CalendarExtender" runat="server"
                                                                                                Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtEmailFromDate">
                                                                                            </ajaxToolkit:CalendarExtender>
                                                                                            <ajaxToolkit:TextBoxWatermarkExtender ID="txtEmailFromDate_TextBoxWatermarkExtender"
                                                                                                runat="server" Enabled="True" TargetControlID="txtEmailFromDate" WatermarkText="dd/mm/yyyy">
                                                                                            </ajaxToolkit:TextBoxWatermarkExtender>
                                                                                            <asp:RequiredFieldValidator ID="txtEmailFromDate_RequiredFieldValidator" runat="server"
                                                                                                ControlToValidate="txtEmailFromDate" CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please select From Date"
                                                                                                ValidationGroup="btnEmail"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblEmailToDate" runat="server" CssClass="FieldName" Text="To:"></asp:Label>
                                                                                            <asp:TextBox ID="txtEmailToDate" runat="server" CssClass="txtField"></asp:TextBox>
                                                                                            <ajaxToolkit:CalendarExtender ID="txtEmailToDate_CalendarExtender" runat="server"
                                                                                                Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtEmailToDate">
                                                                                            </ajaxToolkit:CalendarExtender>
                                                                                            <ajaxToolkit:TextBoxWatermarkExtender ID="txtEmailToDate_TextBoxWatermarkExtender"
                                                                                                runat="server" Enabled="True" TargetControlID="txtEmailToDate" WatermarkText="dd/mm/yyyy">
                                                                                            </ajaxToolkit:TextBoxWatermarkExtender>
                                                                                            <asp:RequiredFieldValidator ID="txtEmailToDate_RequiredFieldValidator" runat="server"
                                                                                                ControlToValidate="txtEmailToDate" CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please select To Date"
                                                                                                ValidationGroup="btnEmail"></asp:RequiredFieldValidator>
                                                                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToCompare="txtEmailFromDate"
                                                                                                ControlToValidate="txtEmailToDate" CssClass="cvPCG" Display="Dynamic" ErrorMessage="To Date should not be less than From Date"
                                                                                                Operator="GreaterThanEqual" Type="Date" ValidationGroup="btnEmail"></asp:CompareValidator>
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
                                                                                                CssClass="rfvPCG" Display="Dynamic" ErrorMessage="&lt;br /&gt;Please select a Period"
                                                                                                Operator="NotEqual" ValidationGroup="btnEmail" ValueToCompare="Select a Period"></asp:CompareValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:CheckBoxList ID="chkRangeReportList" runat="server" class="cmbField" Width="100%">
                                                                                    <asp:ListItem Text="Transaction Report" Value="TRANSACTION_REPORT"></asp:ListItem>
                                                                                    <asp:ListItem Text="Dividend Statement" Value="DIVIDEND_STATEMENT"></asp:ListItem>
                                                                                    <asp:ListItem Text="Dividend Summary" Value="DIVIDEND_SUMMARY"></asp:ListItem>
                                                                                    <asp:ListItem Text="Capital Gain Details" Value="CAPITAL_GAIN_DETAILS"></asp:ListItem>
                                                                                    <asp:ListItem Text="Capital Gain Summary" Value="CAPITAL_GAIN_SUMMARY"></asp:ListItem>
                                                                                    <asp:ListItem Text="Closing Balance Report" Value="TRANSACTION_REPORT_OPEN_CLOSE_BALANCE"></asp:ListItem>
                                                                                </asp:CheckBoxList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style1">
                                                        <asp:Button ID="btnEmailReport" runat="server" CssClass="PCGMediumButton" OnClientClick="return validate('mail')"
                                                            Text="Email Report" ValidationGroup="btnEmail" OnClick="btnEmailReport_Click" />
                                                        &nbsp;&nbsp;
                                                    </td>
                                                </tr>
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
<div style="margin: 6px">
    <label id="lblNote1" runat="server" class="HeaderTextSmall" style="display: none">
        Note: Only historical data is accessible from this screen. Recent data for the last
        2 Business day will not be available. To view the recent data View Dashboards &
        Net Positions.</label>
    <label id="lblNote2" runat="server" class="HeaderTextSmall" style="display: none">
        Note: You can view the recent data, as on last business date.</label>
</div>
<asp:HiddenField ID="hidFromDate" Value="" runat="server" />
<asp:HiddenField ID="hidToDate" Value="" runat="server" />
<asp:HiddenField ID="hidDateType" Value="" runat="server" />
<asp:HiddenField ID="hidTabIndex" Value="0" runat="server" />
<asp:HiddenField ID="hdnCustomerId" runat="server" OnValueChanged="hdnCustomerId_ValueChanged" />
<asp:HiddenField ID="hdnCustomerId1" runat="server" />
<asp:HiddenField ID="SelectedCustomets4Email" runat="server" />
<asp:HiddenField ID="hndCustomerLogin" runat="server" />
<asp:HiddenField ID="hndSelfOrGroup" runat="server" />
<asp:HiddenField ID="hidBMLogin" runat="server" />
<asp:HiddenField ID="hdnValuationDate" runat="server" />

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
    if (document.getElementById("<%= rdoGroup.ClientID %>") != null) {
        if (document.getElementById("<%= rdoGroup.ClientID %>").checked) {
            document.getElementById("<%= trGroupCustomer.ClientID %>").style.display = 'block';
            document.getElementById("<%= trIndCustomer.ClientID %>").style.display = 'none';
        }

        else {
            document.getElementById("<%= trGroupCustomer.ClientID %>").style.display = 'none';
            document.getElementById("<%= trIndCustomer.ClientID %>").style.display = 'block';
        }
    }

    ChangeDates();
</script>

