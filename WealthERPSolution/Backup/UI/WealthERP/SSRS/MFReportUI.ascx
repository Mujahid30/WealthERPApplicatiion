<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFReportUI.ascx.cs"
    Inherits="WealthERP.SSRS.MFReportUI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
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
        $('#ctrl_MFReportUI_btnView').bubbletip($('#div1'), { deltaDirection: 'left' });
        $('#ctrl_MFReportUI_btnViewInPDF').bubbletip($('#div2'), { deltaDirection: 'left' });
        $('#ctrl_MFReportUI_btnViewInExcel').bubbletip($('#div3'), { deltaDirection: 'left' });
    });
</script>

<script type="text/javascript" language="javascript">
    function validate(type) {

        var panel = "";
        var isPorfolioSelected = false;

        //Check if atleast one portfolio Id is selected
        for (i = 0; i < document.forms[0].elements.length; i++) {
            if (document.forms[0].elements[i].type == "checkbox") {
                var checkboxName = document.forms[0].elements[i].name;
                if (checkboxName.substr(0, 5) == "chk--" && document.forms[0].elements[i].checked == true) {
                    isPorfolioSelected = true;
                }
            }
        }
        if (isPorfolioSelected == false) {
            alert("Please select atleast one portfolio")
            return false;
        }
        //Date validation.
        var dateType;
        if (document.getElementById("<%= trAsOn.ClientID %>").style.display == 'table-row') {
            dateType = 'AS_ON';
            document.getElementById("<%= hidDateType.ClientID %>").value = dateType;
        }
        else {
            dateType = 'DATE_RANGE';
            document.getElementById("<%= hidDateType.ClientID %>").value = dateType;
        }
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

        window.document.forms[0].target = '_blank';

        if (type == 'pdf')
            window.document.forms[0].action = "/SSRS/DisplayMFReport.aspx?format=pdf";
        else if (type == 'xls')
            window.document.forms[0].action = "/SSRS/DisplayMFReport.aspx?format=xls";
        else
            window.document.forms[0].action = "/SSRS/DisplayMFReport.aspx?format=view";


        setTimeout(function() {
            window.document.forms[0].target = '';
            window.document.forms[0].action = "";
        }, 500);
        return true;
    }

    function GetParentCustomerId(source, eventArgs) {
        document.getElementById("<%= txtParentCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    };

    function DisplayDates(type) {

        if (type == 'DATE_RANGE') {
            document.getElementById("<%= rbtnPickDate.ClientID %>").checked = true;
            document.getElementById("<%= tblPickDate.ClientID %>").style.display = 'table-row';
            document.getElementById("<%= trRange.ClientID %>").style.display = 'table-row';
            document.getElementById("<%= trAsOn.ClientID %>").style.display = 'none';
        }
        else if (type == 'AS_ON') {
            document.getElementById("<%= tblPickDate.ClientID %>").style.display = 'table-row';
            document.getElementById("<%= trRange.ClientID %>").style.display = 'none';
            document.getElementById("<%= trAsOn.ClientID %>").style.display = 'table-row';
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
        var dropdown = document.getElementById("<%= ddlReportSubType.ClientID %>");
        selectedReport = dropdown.options[dropdown.selectedIndex].value
        if (selectedReport != "MF_TRANSACTION" && selectedReport != "CAPITAL_GAIN_DETAILS" && selectedReport != "CAPITAL_GAIN_SUMMARY" && selectedReport != "MF_CLOSINGBALANCE") {
            document.getElementById("<%= tblPickDate.ClientID %>").style.display = 'none';
            document.getElementById("<%= trRange.ClientID %>").style.display = 'none';
            document.getElementById("<%= trAsOn.ClientID %>").style.display = 'table-row';
            document.getElementById("<%= rbtnPickPeriod.ClientID %>").checked = true;
        }
        else {
            document.getElementById("<%= tblPickDate.ClientID %>").style.display = 'table-row';
            if (document.getElementById("<%= rbtnPickPeriod.ClientID %>").checked) {
                document.getElementById("<%= trRange.ClientID %>").style.display = 'none';
                document.getElementById("<%= trAsOn.ClientID %>").style.display = 'table-row';
            }
            else {
                document.getElementById("<%= trRange.ClientID %>").style.display = 'table-row';
                document.getElementById("<%= trAsOn.ClientID %>").style.display = 'none';
            }
        }
    }

   
</script>

<style type="text/css">
    .tblSection
    {
        border: solid 1px #B5C7DE;
        margin-right: 1px;
        margin-left: 4px;
    }
</style>
<div class="divPageHeading" style="height: 30px;">
    <table border="0" width="100%">
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="MF Report"></asp:Label>
                <hr />
            </td>
        </tr>
    </table>
</div>
<table width="100%" cellspacing="5">
    <tr id="trAdminRmButton" runat="server">
        <td colspan="2" align="right">
            <asp:Button ID="btnViewInPDF" runat="server" OnClientClick="return validate('pdf')"
                PostBackUrl="~/SSRS/DisplayMFReport.aspx?format=pdf" CssClass="PDFButton" />&nbsp;&nbsp;
            <div id="div2" style="display: none;">
                <p class="tip">
                    Click here to download report in pdf format.
                </p>
            </div>
            <asp:Button ID="btnView" runat="server" OnClientClick="return validate('view')" PostBackUrl="~/SSRS/DisplayMFReport.aspx?format=view"
                CssClass="CrystalButton" />&nbsp;&nbsp;
            <div id="div1" style="display: none;">
                <p class="tip">
                    Click here to view report.
                </p>
            </div>
            <asp:Button ID="btnViewInExcel" runat="server" CssClass="ExcelButton" OnClientClick="return validate('xls')"
                PostBackUrl="~/SSRS/DisplayMFReport.aspx?format=xls" />&nbsp;&nbsp;
            <div id="div3" style="display: none;">
                <p class="tip">
                    Click here to download report in excel format.</p>
            </div>
        </td>
    </tr>
</table>
<ajaxToolkit:TabContainer ID="TabContainer1" runat="server" >
    <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Group" Visible="true">
        <HeaderTemplate>
            View Report</HeaderTemplate>
        <ContentTemplate>
            <table width="100%" class="tblSection" cellspacing="10">
                <tr id="trStepGrHead" runat="server">
                    <td colspan="2">
                        <asp:Label ID="lblReportStep" runat="server" CssClass="HeaderTextSmall" Style='font-weight: normal;'
                            Text="Step 1 : Select Customer"></asp:Label>
                    </td>
                </tr>
                <tr id="trCustomerType" runat="server" class="tblSection">
                    <td style="width: 110px">
                        <asp:Label ID="lblCustomerType" runat="server" Text="Report for :" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCustomerType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlCustomerType_SelectedIndexChanged"
                            AutoPostBack="true">
                            <asp:ListItem Text="Individual" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Group" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="trAdminCustomer" runat="server">
                    <td style="width: 110px">
                        <asp:Label ID="lblGroupHead" runat="server" CssClass="FieldName" Text="Customer :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtParentCustomer" runat="server" AutoComplete="Off" AutoPostBack="True"
                            Width="250px" Height="18px" CssClass="txtField"></asp:TextBox><asp:HiddenField ID="txtParentCustomerId"
                                runat="server" OnValueChanged="txtParentCustomerId_ValueChanged" />
                        <asp:RequiredFieldValidator ID="rfvGroupHead" runat="server" ControlToValidate="txtParentCustomer"
                            CssClass="rfvPCG" Display="Dynamic" ErrorMessage="&lt;br /&gt;Please select a group head"
                            ValidationGroup="btnGo"></asp:RequiredFieldValidator><ajaxToolkit:TextBoxWatermarkExtender
                                ID="txtParentCustomer_TextBoxWatermarkExtender" runat="server" Enabled="True"
                                TargetControlID="txtParentCustomer" WatermarkText="Type the Customer Name">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                        <ajaxToolkit:AutoCompleteExtender ID="txtParentCustomer_autoCompleteExtender" runat="server"
                            CompletionInterval="100" CompletionListCssClass="AutoCompleteExtender_CompletionList"
                            CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                            CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem" CompletionSetCount="5"
                            DelimiterCharacters="" EnableCaching="False" Enabled="True" MinimumPrefixLength="1"
                            OnClientItemSelected="GetParentCustomerId" ServiceMethod="GetParentCustomerName"
                            ServicePath="~/CustomerPortfolio/AutoComplete.asmx" TargetControlID="txtParentCustomer"
                            UseContextKey="True">
                        </ajaxToolkit:AutoCompleteExtender>
                    </td>
                </tr>
                <tr id="trGroupCustomerDetails" runat="server" visible="False">
                    <td id="Td1" runat="server">
                        <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="PAN :"></asp:Label>
                    </td>
                    <td id="Td2" runat="server">
                        <asp:TextBox ID="txtPAN" runat="server" CssClass="txtField" BackColor="Transparent"
                            BorderStyle="None"></asp:TextBox>
                        <%--  <asp:Label ID="Label9" runat="server" CssClass="FieldName" Text="Address:"></asp:Label>
            <asp:TextBox ID="txtGroupAddress" runat="server" CssClass="txtField" BackColor="Transparent"
                BorderStyle="None"></asp:TextBox>--%>
                    </td>
                </tr>
                <tr id="tr1" runat="server">
                    <td id="Td3" runat="server" style="width: 110px">
                        <asp:Label ID="lblGroupPortfolio" runat="server" CssClass="FieldName" Text="Portfolio :"></asp:Label>
                        &nbsp;
                    </td>
                    <td id="Td4" runat="server" class="rightfield">
                        <asp:DropDownList ID="ddlGroupPortfolioGroup" runat="server" CssClass="cmbField"
                            OnSelectedIndexChanged="ddlGroupPortfolioGroup_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Text="Managed" Value="MANAGED" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="UnManaged" Value="UN_MANAGED"></asp:ListItem>
                            <asp:ListItem Text="All" Value="ALL"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="Field">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlGroupPortfolioGroup" EventName="SelectedIndexChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <div id="divGroupPortfolios" runat="server">
                                </div>
                                <div id="divGroupCustomers" runat="server">
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <table width="100%" class="tblSection" cellspacing="10">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label2" runat="server" CssClass="HeaderTextSmall" Style='font-weight: normal;'
                            Text="Step 2 : Select Report Type "></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 110px">
                        <asp:Label ID="Label4" runat="server" CssClass="FieldName">Report sub type: </asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReportSubType" runat="server" CssClass="cmbField" onChange="ChangeDates()"
                            Width="200px" Font-Size="X-Small">
                            <asp:ListItem Text="" Value="PORTFOLIO_RETURNS_HOLDING" Selected="True">Portfolio Returns - Holding</asp:ListItem>
                            <asp:ListItem Text="" Value="COMPREHENSIVE_MF_REPORT">Comprehensive Report</asp:ListItem>
                            <asp:ListItem Text="" Value="CAPITAL_GAIN_DETAILS">Capital Gain Details</asp:ListItem>
                            <asp:ListItem Text="" Value="CAPITAL_GAIN_SUMMARY">Capital Gain Summary</asp:ListItem>
                            <asp:ListItem Text="" Value="ELIGIBLE_CAPITAL_GAIN_DETAILS">Eligible Capital Gain Details</asp:ListItem>
                            <asp:ListItem Text="" Value="ELIGIBLE_CAPITAL_GAIN_SUMMARY">Eligible Capital Gain Summary</asp:ListItem>
                            <asp:ListItem Text="" Value="MF_TRANSACTION">Transaction Report</asp:ListItem>
                            <asp:ListItem Text="" Value="MF_CLOSINGBALANCE">Closing Balance Report</asp:ListItem>
                            <asp:ListItem Text="" Value="MF_TranxnHolding">Holding Transactions Report</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table width="100%" class="tblSection" cellspacing="10">
                <tr>
                    <td colspan="2" width="8%">
                        <asp:Label ID="Label3" runat="server" CssClass="HeaderTextSmall" Text="Step 3:  Select Date"
                            Style='font-weight: normal;'></asp:Label>
                    </td>
                </tr>
                <tr id="tblPickDate" runat="server">
                    <td>
                        <asp:RadioButton ID="rbtnPickPeriod" runat="server" Checked="true" GroupName="Date"
                            onclick="DisplayDates('AS_ON')" />
                        <asp:Label ID="lblPickPeriod" runat="server" Text="As On" CssClass="FieldName"></asp:Label>
                        &nbsp;
                        <asp:RadioButton ID="rbtnPickDate" runat="server" GroupName="Date" onclick="DisplayDates('DATE_RANGE')" />
                        <asp:Label ID="lblPickDate" runat="server" Text="Range" CssClass="FieldName"></asp:Label>
                    </td>
                </tr>
                <tr id="trRange" runat="server">
                    <td>
                        <asp:Label ID="lblFromDate" runat="server" CssClass="FieldName">From :</asp:Label>
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtField"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" TargetControlID="txtFromDate"
                            Format="dd/MM/yyyy">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtFromDate_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtFromDate" WatermarkText="dd/mm/yyyy">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFromDate"
                            CssClass="rfvPCG" ErrorMessage="<br />Please select a From Date" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="btnGo"> </asp:RequiredFieldValidator>
                        &nbsp;
                        <asp:Label ID="lblToDate" runat="server" CssClass="FieldName">To:</asp:Label>
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="txtField"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" TargetControlID="txtToDate"
                            Format="dd/MM/yyyy">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtToDate_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtToDate" WatermarkText="dd/mm/yyyy">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtToDate"
                            CssClass="rfvPCG" ErrorMessage="<br />Please select a To Date" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="btnGo"> </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="To Date should not be less than From Date"
                            Type="Date" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual"
                            CssClass="cvPCG" Display="Dynamic" ValidationGroup="btnGo"></asp:CompareValidator>
                    </td>
                </tr>
                <tr id="trAsOn" runat="server">
                    <td>
                        <asp:Label ID="lblAsOnDate" runat="server" CssClass="FieldName">As on date :</asp:Label>
                        &nbsp;
                        <asp:TextBox ID="txtAsOnDate" runat="server" CssClass="txtField"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtAsOnDate"
                            Format="dd/MM/yyyy">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                            TargetControlID="txtAsOnDate" WatermarkText="dd/mm/yyyy">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtAsOnDate"
                            CssClass="rfvPCG" ErrorMessage="<br />Please select an As on Date" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="btnGo">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>
<table>
    <tr style="width: 100px">
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
</table>
<asp:HiddenField ID="hidFromDate" Value="" runat="server" />
<asp:HiddenField ID="hidToDate" Value="" runat="server" />
<asp:HiddenField ID="hidDateType" Value="" runat="server" />
<asp:HiddenField ID="hidTabIndex" Value="0" runat="server" />
<asp:HiddenField ID="hidPortfolioIds" Value="" runat="server" />
<asp:HiddenField ID="hndCustomerLogin" runat="server" />
<asp:HiddenField ID="hdnCustomerId1" runat="server" />
<asp:HiddenField ID="hdnValuationDate" runat="server" />

<script type="text/javascript">
    ChangeDates();
</script>

