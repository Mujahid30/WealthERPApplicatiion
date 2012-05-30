<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EquityReports.ascx.cs"
    Inherits="WealthERP.Reports.EquityReports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    $('#ctrl_EquityReports_btnView').bubbletip($('#div1'), { deltaDirection: 'left' });
    $('#ctrl_EquityReports_btnViewInPDF').bubbletip($('#div2'), { deltaDirection: 'left' });
    $('#ctrl_EquityReports_btnViewInDOC').bubbletip($('#div3'), { deltaDirection: 'left' });
    $('#ctrl_EquityReports_btnCustomerViewReport').bubbletip($('#div4'), { deltaDirection: 'left' });
    $('#ctrl_EquityReports_btnCustomerExportToPDF').bubbletip($('#div5'), { deltaDirection: 'left' });
    $('#ctrl_EquityReports_btnCustomerViewInDOC').bubbletip($('#div6'), { deltaDirection: 'left' }); 
    });
</script>

<script type="text/javascript" language="javascript">
    function validate(type) {

        var panel = "";
        var isPorfolioSelected = false;

        //uncheck other portfolio Ids in Group/Individual panel
        if (document.getElementById("<%= hidTabIndex.ClientID %>").value == 0)
            panel = document.getElementById('<%= TabPanel2.ClientID %>');
        else if (document.getElementById("<%= hidTabIndex.ClientID %>").value == 1)
            panel = document.getElementById('<%= TabPanel1.ClientID %>');

        var groupChkArray = panel.getElementsByTagName("input");
        for (var i = 0; i < groupChkArray.length; i++) {
            if (groupChkArray[i].type == "checkbox")
                groupChkArray[i].checked = false;
        }

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
            alert("Please choose customer or portfolio")
            return false;
        }
        //Date validation.
        dateType = document.getElementById("<%= hidDateType.ClientID %>").value;
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
            if (dateVal < 1) {
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

        window.document.forms[0].target = '_blank';
        if (type == 'mail')
            window.document.forms[0].action = "/Reports/Display.aspx?mail=1";
        else if (type == 'pdf')
            window.document.forms[0].action = "/Reports/Display.aspx?mail=2";
        else if(type=='doc')
             window.document.forms[0].action = "/Reports/Display.aspx?mail=4"; 
        else
            window.document.forms[0].action = "/Reports/Display.aspx?mail=0";
        
        setTimeout(function() {
            window.document.forms[0].target = '';
            window.document.forms[0].action = "";
        }, 500);
        return true;
    }
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();       
        return false;
    };

    function GetParentCustomerId(source, eventArgs) {
        document.getElementById("<%= txtParentCustomerId.ClientID %>").value = eventArgs.get_value();
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
        arr["EQUITY_SECTOR_WISE"] = "AS_ON";
        arr["EQ_TRANSACTION_REPORT_DELIVERY"] = "DATE_RANGE";
        arr["EQ_TRANSACTION_REPORT_SPECULATIVE"] = "DATE_RANGE";
        arr["EQUITY_TRANSACTION_WISE"] = "DATE_RANGE";
        arr["EQUITY_HOLDING_WISE"] = "AS_ON";
        arr["EQ_PORTFOLIO_RETURNS_REPORT"] = "AS_ON";

        var dropdown = document.getElementById("<%= ddlReportSubType.ClientID %>");
        selectedReport = dropdown.options[dropdown.selectedIndex].value

        if (selectedReport == 'EQ_TRANSACTION_REPORT' || selectedReport == 'EQ_PORTFOLIO_RETURNS_REPORT') {
            document.getElementById("spnFilter").style.display = 'block';
            DisplayDates(arr[selectedReport]);
        }
        else if (selectedReport == 'EQUITY_TRANSACTION_REPORT') {
        alert("equity");
        document.getElementById("spnFilter").style.display = 'none';
        DisplayDates(arr[selectedReport]);
        } else {
            document.getElementById("spnFilter").style.display = 'none';
            DisplayDates(arr[selectedReport]);
        }

    }
    function OnChanged(sender, args) {
        document.getElementById("<%= hidTabIndex.ClientID %>").value = sender.get_activeTab()._tabIndex;
        uncheckallCehckBoxes();
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

    //**********Customer Login Equity Report Validation For ViewReport and Export To PDF Button
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
</script>

<style>
    .tblSection
    {
        border: solid 1px #B5C7DE;
    }
</style>
<table border="0" width="100%">
    <tr  >
        <td colspan="2">
            <asp:Label ID="Label7" runat="server" CssClass="HeaderTextSmall" Text="Equity Reports"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr id="trAdminRmButton" runat="server">
        <td colspan="2" align="right">
         <asp:Button ID="btnView" runat="server"  OnClientClick="return validate('')"
                PostBackUrl="~/Reports/Display.aspx" CssClass="CrystalButton" />&nbsp;&nbsp;
                <div id="div1" style="display: none;">
                <p class="tip">
                    Click here to view equity report.
                </p>
            </div>
            <%--<asp:Button ID="btnMail" runat="server" Text="Email Report" OnClientClick="return validate('mail')"
                PostBackUrl="~/Reports/Display.aspx?mail=1" CssClass="PCGMediumButton" />--%>
            <asp:Button ID="btnViewInPDF" runat="server"   OnClientClick="return validate('pdf')"
                PostBackUrl="~/Reports/Display.aspx?mail=2" CssClass="PDFButton"  />&nbsp;&nbsp;
                 <div id="div2" style="display: none;">
                <p class="tip">
                   Click here to view equity report in pdf format.
                </p>
      </div>
            <asp:Button ID="btnViewInDOC" runat="server"  CssClass="DOCButton" OnClientClick="return validate('doc')"
                PostBackUrl="~/Reports/Display.aspx?mail=4" />&nbsp;&nbsp;
                    <div id="div3" style="display: none;">
                <p class="tip">
                    Click here to view equity report in word doc.</p>
     </div>  
    </td>
    </tr>
    <tr id="trCustomerButton" runat="server" >
        <td colspan="2" align="right">
         <asp:Button ID="btnCustomerViewReport" runat="server"  OnClientClick="return CustomerValidate('view')"
         PostBackUrl="~/Reports/Display.aspx?mail=3" CssClass="CrystalButton" ValidationGroup="btnView" />&nbsp;&nbsp;
                <div id="div4" style="display: none;">
                <p class="tip">
                    Click here to view equity report.
                </p>
            </div>
           
            <asp:Button ID="btnCustomerExportToPDF" runat="server"  OnClientClick="return CustomerValidate('pdf')"
             PostBackUrl="~/Reports/Display.aspx?mail=2" CssClass="PDFButton" />&nbsp;&nbsp;
                 <div id="div5" style="display: none;">
                <p class="tip">
                   Click here to view equity report in pdf format.
                </p>
      </div>
            <asp:Button ID="btnCustomerViewInDOC" runat="server"  CssClass="DOCButton" OnClientClick="return CustomerValidate('doc')"
                PostBackUrl="~/Reports/Display.aspx?mail=4" />&nbsp;&nbsp;
                    <div id="div6" style="display: none;">
                <p class="tip">
                    Click here to view equity report in word doc.</p>
     </div>  
    </td>
    </tr>
    <tr>
        <td colspan="2">
            <%--   <asp:RadioButton ID="rdoGroup" runat="server" Text="Group" GroupName="customer" CssClass="Field"
                Checked="true" onClick="DisplayCustomerSelection('GROUP')" />
            <asp:RadioButton ID="rdoIndividual" runat="server" Text="Individual" GroupName="customer"
                CssClass="Field" onClick="DisplayCustomerSelection('INDIVIDUAL')" />--%>
            <ajaxToolkit:TabContainer ID="TabContainer1" runat="server"
                OnClientActiveTabChanged="OnChanged" ActiveTabIndex="1">
                <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Group" Visible="true">
                    <HeaderTemplate>
                        Group</HeaderTemplate>
                    <ContentTemplate>
                        <table border="0" id="tblGroup" style="display: block;">
                            <tr id="trStepGrHead" runat="server">
                                <td colspan="2">
                                    <asp:Label ID="lblSelectCustomer" runat="server" CssClass="HeaderTextSmall" Style='font-weight: normal;'
                                        Text="Step 1: Select Customer"></asp:Label>
                                </td>
                            </tr>
                            <tr Id="trAdminCustomer" runat="server" >
                                <td style="width: 80px" align="right">
                                    <asp:Label ID="lblGroupHead" runat="server" CssClass="FieldName" Text="Group Head :"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtParentCustomer" runat="server" AutoComplete="Off" AutoPostBack="True"
                                        CssClass="txtField"></asp:TextBox><asp:HiddenField ID="txtParentCustomerId" runat="server"
                                            OnValueChanged="txtParentCustomerId_ValueChanged" />
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
                            <tr id="trCustomerGrHead" runat="server">
                            <td>
                            <asp:Label ID="lblCustomerGrHead" runat="server" Text="" CssClass="FieldName"></asp:Label>
                            </td>
                            <td></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr id="trGroupCustomerDetails" runat="server" visible="False">
                                <td id="Td1" runat="server">
                                    <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="PAN :"></asp:Label>
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="txtField" BackColor="Transparent"
                                        BorderStyle="None"></asp:TextBox>
                                </td>
                                <td id="Td2" runat="server">
                                    <asp:Label ID="Label9" runat="server" CssClass="FieldName" Text="Address:"></asp:Label>
                                    <asp:TextBox ID="txtGroupAddress" runat="server" CssClass="txtField" BackColor="Transparent"
                                        BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="tr1" runat="server">
                                <td id="Td3" runat="server" align="right">
                                    <asp:Label ID="lblGroupPortfolio" runat="server" CssClass="FieldName" Text="Portfolio :"></asp:Label>
                                </td>
                                <td id="Td4" runat="server" align="left">
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
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="Individual">
                    <ContentTemplate>
                        <table border="0" id="tblIndividual">
                            <tr id="trStepIndi" runat="server">
                                <td colspan="2">
                                    <asp:Label ID="Label5" runat="server" CssClass="HeaderTextSmall" Style='font-weight: normal;'
                                        Text="Step 1: Select Customer"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trAdminIndiCustomer" runat="server">
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Customer Name :" CssClass="FieldName"></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="txtCustomerId_ValueChanged" Visible="true" />
                                    <asp:TextBox ID="txtCustomer" runat="server" CssClass="txtField" AutoComplete="Off"
                                        AutoPostBack="true"></asp:TextBox><cc1:TextBoxWatermarkExtender ID="txtCustomer_TextBoxWatermarkExtender"
                                            runat="server" TargetControlID="txtCustomer" WatermarkText="Type the Customer Name">
                                        </cc1:TextBoxWatermarkExtender>
                                    <ajaxToolkit:AutoCompleteExtender ID="txtCustomer_autoCompleteExtender" runat="server"
                                        TargetControlID="txtCustomer" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                                        MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="5" CompletionInterval="100"
                                        CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                                        CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                                        UseContextKey="true" OnClientItemSelected="GetCustomerId" />
                                    <span id="Span1" class="spnRequiredField">*<br />
                                    </span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCustomer"
                                        ErrorMessage="Please Enter Customer Name" Display="Dynamic" runat="server" CssClass="rfvPCG"
                                        ValidationGroup="btnSubmit">
                                    </asp:RequiredFieldValidator><span style='font-size: 8px; font-weight: normal' class='FieldName'>Enter
                                        few characters of customer  name.</span>
                                </td>
                            </tr>
                            <tr id="trCustomerInd" runat="server">
                            <td>
                            <asp:Label ID="lblCustomerIndi" runat="server" CssClass="FieldName" Text=""></asp:Label>
                            </td>
                            <td></td>
                            </tr>
                        </table>
                        <table border="0">
                            <tr id="trCustomerDetails" runat="server" visible="false">
                                <td>
                                    <asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="PAN :"></asp:Label>
                                    <asp:TextBox ID="txtPanParent" runat="server" CssClass="txtField" BackColor="Transparent"
                                        BorderStyle="None"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblAddress" runat="server" CssClass="FieldName" Text="Address:"></asp:Label>
                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="txtField" BackColor="Transparent"
                                        BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trPortfolioDetails" runat="server" visible="false">
                                <td colspan="2">
                                    <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio :"></asp:Label>
                                      <asp:DropDownList ID="ddlPortfolioGroup" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlPortfolioGroup_SelectedIndexChanged"
                                        AutoPostBack="true">
                                        <asp:ListItem Text="Managed" Value="MANAGED" Selected="True">Managed</asp:ListItem>
                                        <asp:ListItem Text="UnManaged" Value="UN_MANAGED">UnManaged</asp:ListItem>
                                        <asp:ListItem Text="All" Value="ALL">All</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
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
                            </tr>
                        </table>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
        </td>

    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <table border="0" width="100%" class="tblSection" cellspacing="10">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label2" runat="server" CssClass="HeaderTextSmall" Style='font-weight: normal;'
                            Text="Step 2: Select Report Type "></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width:80px">
                        <asp:Label ID="Label4" runat="server" CssClass="FieldName">Report type:</asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlReportSubType" runat="server" CssClass="cmbField" onChange="ChangeDates()">
                            <asp:ListItem Text="" Value="EQUITY_SECTOR_WISE" Selected="True">Sectorwise Equity Reports</asp:ListItem>
                             <asp:ListItem Text="" Value="EQUITY_TRANSACTION_WISE">Transaction Equity Reports</asp:ListItem>
                             <asp:ListItem Text="" Value="EQUITY_HOLDING_WISE" >Holding Equity Reports</asp:ListItem>
                            <%--<asp:ListItem Text="" Value="EQ_TRANSACTION_REPORT">Transaction Report</asp:ListItem>
                            <asp:ListItem Text="" Value="EQ_PORTFOLIO_RETURNS_REPORT">Portfolio Returns Report</asp:ListItem>--%>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <span id="spnFilter" style="display: none;">
                            <asp:RadioButton ID="rdoAll" runat="server" Text="All" Checked="true" CssClass="Field"
                                GroupName="Filter" />
                            &nbsp; &nbsp;
                            <asp:RadioButton ID="rdoSpeculative" runat="server" Text="Speculative" CssClass="Field"
                                GroupName="Filter" />
                            &nbsp; &nbsp;
                            <asp:RadioButton ID="rdoDerivative" runat="server" Text="Derivative" CssClass="Field"
                                GroupName="Filter" />
                        </span>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <br />
            <table border="0" width="100%" class="tblSection" cellspacing="10">
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" CssClass="HeaderTextSmall" Text="Step 3: Select Date Range"
                            Style='font-weight: normal;'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="tblPickDate" width="100%" border="0">
                            <tr>
                                <td>
                                     <asp:Label ID="lblPickDate" runat="server" Text="Pick a date range" CssClass="Field"></asp:Label>
                                     <asp:RadioButton ID="rbtnPickDate" Checked="true" runat="server" GroupName="Date"
                                        onclick="DisplayDates('DATE_RANGE')" Text="" />
                                       
                                </td>
                                <td>
                                    <asp:RadioButton ID="rbtnPickPeriod" runat="server" GroupName="Date" onclick="DisplayDates('PERIOD')" />
                                    <asp:Label ID="lblPickPeriod" runat="server" Text="Pick a Period" CssClass="Field"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table id="trRange" runat="server">
                            <tr>
                                <td>
                                    <asp:Label ID="lblFromDate" runat="server" CssClass="FieldName">From:</asp:Label>
                                    &nbsp;
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
                                </td>
                                <td>
                                    <asp:Label ID="lblToDate" runat="server" CssClass="FieldName">To:</asp:Label>
                                    &nbsp;
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
                        </table>
                        <table id="trPeriod" runat="server" style="display: none;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblPeriod" runat="server" CssClass="FieldName">Period:</asp:Label>
                                </td>
                                <td>
                                    &nbsp; &nbsp;
                                    <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="cmbField">
                                    </asp:DropDownList>
                                    <span id="Span4" class="spnRequiredField">*</span>
                                    <br />
                                    <br />
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlPeriod"
                                        CssClass="rfvPCG" ErrorMessage="Please select a Period" Operator="NotEqual" ValueToCompare="Select a Period"
                                        ValidationGroup="btnGo">
                                    </asp:CompareValidator>
                                </td>
                            </tr>
                        </table>
                        <table id="trAsOn" runat="server" style='display: none'>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAsOnDate" runat="server" CssClass="FieldName">As on date :</asp:Label>
                                </td>
                                <td>
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
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
             
        </td>
    </tr>
</table>
<asp:HiddenField ID="hidFromDate" Value="" runat="server" />
<asp:HiddenField ID="hidToDate" Value="" runat="server" />
<asp:HiddenField ID="hidDateType" Value="" runat="server" />
<asp:HiddenField ID="hidTabIndex" Value="0" runat="server" />
<asp:HiddenField ID="hndCustomerLogin" runat="server" />
<asp:HiddenField ID="hdnCustomerId1" runat="server" />

<script>
    if (document.getElementById("<%= rbtnPickDate.ClientID %>").checked) {
        document.getElementById("<%= rbtnPickDate.ClientID %>").style.display = 'block';
        document.getElementById("<%= trRange.ClientID %>").style.display = 'block';
        document.getElementById("<%= trPeriod.ClientID %>").style.display = 'none';
        document.getElementById("<%= trAsOn.ClientID %>").style.display = 'none';
    }
    else if (document.getElementById("<%= rbtnPickPeriod.ClientID %>").checked) {
        document.getElementById("<%= rbtnPickDate.ClientID %>").style.display = 'block';
        document.getElementById("<%= trRange.ClientID %>").style.display = 'none';
        document.getElementById("<%= trPeriod.ClientID %>").style.display = 'block';
        document.getElementById("<%= trAsOn.ClientID %>").style.display = 'none';
    }
    ChangeDates();

    function DisableEnableTab(boolVal, tabNo) {

        var tab = $find('<%= TabContainer1.ClientID %>');

        if (boolVal == true) {
            tab.get_tabs()[tabNo].set_enabled(false);
        }
        else {
            tab.get_tabs()[tabNo].set_enabled(true);
        }
    }
   
</script>

