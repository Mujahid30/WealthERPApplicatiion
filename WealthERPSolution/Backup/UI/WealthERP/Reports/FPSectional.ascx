<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FPSectional.ascx.cs"
    Inherits="WealthERP.Reports.FPSectional" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function() {
        $('#ctrl_FPSectional_btnViewReport').bubbletip($('#divView'), { deltaDirection: 'left' });
        $('#ctrl_FPSectional_btnViewInPDF').bubbletip($('#divPdf'), { deltaDirection: 'left' });
        $('#ctrl_FPSectional_btnViewInDOC').bubbletip($('#divDoc'), { deltaDirection: 'left' });
    });
</script>

<script language="javascript" type="text/javascript">

    function CheckBoxListSelect() {
        var checkAll = document.getElementById("chkCheckAll");
        //    var unCheckAll = document.getElementById("chkUncheckAll");   
        //    if (unCheckAll.checked == true) {
        //        checkAll.checked = false;
        //        unCheckAll.checked = false;
        //        unCheckAll.disabled = true;
        //        checkAll.disabled = false;

        //    } else if (checkAll.checked == true ) {
        //        unCheckAll.checked = false;
        //        checkAll.checked = false;
        //        checkAll.disabled = true;       
        //        unCheckAll.disabled = false;
        //        
        //    }  
        //       
        var chkBoxList = document.getElementById("tblFPsectinal");
        var chkBoxCount = chkBoxList.getElementsByTagName("input");
        for (var i = 0; i < chkBoxCount.length; i++) {
            //        if (state == "false") {            
            //            chkBoxCount[i].checked = false;           
            //        }
            //        else
            //            chkBoxCount[i].checked = true;

            if (checkAll.checked == true) {
                if (chkBoxCount[i].checked == false) {
                    if (chkBoxCount[i].id != "ctrl_FPSectional_chkCover_page" || chkBoxCount[i].id != "ctrl_FPSectional_chkTableContent")
                        chkBoxCount[i].checked = true;
                }
            }
            else if (checkAll.checked == false) {
                if (chkBoxCount[i].checked == true) {
                    if (chkBoxCount[i].id != "ctrl_FPSectional_chkCover_page" || chkBoxCount[i].id != "ctrl_FPSectional_chkTableContent")
                        chkBoxCount[i].checked = false;

                }
            }
        }

        return false;
    }

    function CustomerValidate(type) {
        if (type == 'pdf') {
            window.document.forms[0].target = '_blank';
            window.document.forms[0].action = "/Reports/Display.aspx?mail=2";
        } else if (type == 'doc') {
            window.document.forms[0].target = '_blank';
            window.document.forms[0].action = "/Reports/Display.aspx?mail=4";
        }
        else {
            window.document.forms[0].target = '_blank';
            window.document.forms[0].action = "/Reports/Display.aspx?mail=3";
        }

        setTimeout(function() {
            window.document.forms[0].target = '';
            window.document.forms[0].action = "ControlHost.aspx?pageid=FPSectional";
        }, 500);
        return true;

    }
    function GetCustomerId(source, eventArgs) {

        document.getElementById("<%= hdnCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    };

  
 
</script>

<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<%--<telerik:RadScriptManager ID="RdScriptManager1" runat="server">
</telerik:RadScriptManager>--%>
<%--<asp:Label ID="headertitle" runat="server" CssClass="HeaderTextBig" Text="Financial Planning Reports"></asp:Label>
<br />--%>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Financial Planning Reports
                        </td>
                        <td align="right" id="tdReportButtons" runat="server" style="padding-bottom: 2px;">
                         <asp:Button ID="btnViewInPDF" runat="server" OnClientClick="return CustomerValidate('pdf')"
                                PostBackUrl="~/Reports/Display.aspx?mail=2" CssClass="PDFButton" />
                            &nbsp;&nbsp;
                            <div id="divPdf" style="display: none;">
                                <p class="tip">
                                    Click here to view FP sectional report in PDF format.
                                </p>
                            </div>
                            <asp:Button ID="btnViewReport" runat="server" PostBackUrl="~/Reports/Display.aspx?mail=0"
                                CssClass="CrystalButton" />&nbsp;&nbsp;
                            <div id="divView" style="display: none;">
                                <p class="tip">
                                    Click here to view FP sectional report.
                                </p>
                            </div>
                           
                            <asp:Button ID="btnViewInDOC" runat="server" CssClass="DOCButton" OnClientClick="return CustomerValidate('doc')"
                                PostBackUrl="~/Reports/Display.aspx?mail=4" />
                            &nbsp;&nbsp;
                            <div id="divDoc" style="display: none;">
                                <p class="tip">
                                    Click here to view FP sectional report in word doc.</p>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgRecordStatus" runat="server" class="success-msg" align="center" visible="false">
                Record saved Successfully
            </div>
        </td>
    </tr>
</table>
<table id="tblCustomer" runat="server" width="100%" visible="true">
<tr>
        <td colspan="2">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Select Customer
            </div>
        </td>
    </tr>
    <tr id="trIndCustomer" runat="server">
        <td id="Td2" runat="server" width="2%">
        </td>
        <td id="Td1" runat="server" width="98%">
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
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtCustomer"
                ErrorMessage="<br />Please Enter Customer Name" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="btnSubmit"></asp:RequiredFieldValidator><span
                    style='font-size: 9px; font-weight: normal' class='FieldName'><br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Enter few characters of customer name. </span>
        </td>
    </tr>
</table>
<table width="100%">
    <%--  <tr>
        <td colspan="3">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextSmall" Text="Financial Planning Reports"></asp:Label>
            <hr />
        </td>
        
    </tr>  --%>
    <tr>
        <td colspan="3">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
               Report Generation
            </div>
        </td>
    </tr>
    <tr>
        <td style="width: 3%">
        </td>
        <td style="width: 37%">
            <input id="chkCheckAll" name="Select All" value="Customer" type="checkbox" onclick="CheckBoxListSelect()" />
            <asp:Label ID="lblCheckAll" class="Field" Text="Check All" runat="server"></asp:Label>
        </td>
        <td style="width: 60%" align="right">
        </td>
    </tr>
    <tr>
        <td style="width: 3%">
        </td>
        <td colspan="2">
            <div id="Div1">
                <fieldset style="height: 30%; width: 50%;">
                    <legend class="HeaderTextSmall">Select the sections to generate the report</legend>
                    <table width="100%" id="tblFPsectinal">
                        <tr>
                            <td style="width: 3%">
                            </td>
                            <td style="width: 44%; white-space: nowrap">
                                <%-- <input type="checkbox" name="FPSectonal" value="1">Cover page<br>--%>
                                <asp:CheckBox ID="chkCover_page" runat="server" CssClass="cmbFielde" Text="Cover page"
                                    Checked="true" Enabled="false" />
                            </td>
                            <td style="width: 50%; white-space: nowrap">
                                <asp:CheckBox ID="chkIncome_Expense" runat="server" CssClass="cmbFielde" Text="Income and Expense Summary" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 3%">
                            </td>
                            <td style="width: 44%; white-space: nowrap">
                                <asp:CheckBox ID="chkRM_Messgae" runat="server" CssClass="cmbFielde" Text="RM Message" />
                            </td>
                            <td style="width: 50%; white-space: nowrap">
                                <asp:CheckBox ID="chkCash_Flows" runat="server" CssClass="cmbFielde" Text="Cash Flows" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 3%">
                            </td>
                            <td style="width: 44%; white-space: nowrap">
                                <asp:CheckBox ID="chkImage" runat="server" CssClass="cmbFielde" Text="Image" />
                            </td>
                            <td style="width: 50%; white-space: nowrap">
                                <asp:CheckBox ID="chkNetWorthSummary" runat="server" CssClass="cmbFielde" Text="Net Worth Summary" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 3%">
                            </td>
                            <td style="width: 44%; white-space: nowrap">
                                <asp:CheckBox ID="chkTableContent" runat="server" CssClass="cmbFielde" Text="Table of Content"
                                    Enabled="false" />
                            </td>
                            <td style="width: 50%; white-space: nowrap">
                                <asp:CheckBox ID="chkRiskProfile" runat="server" CssClass="cmbFielde" Text="Risk profile & Portfolio allocation" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 3%">
                            </td>
                            <td style="width: 44%; white-space: nowrap">
                                <asp:CheckBox ID="chkFPIntroduction" runat="server" CssClass="cmbFielde" Text="FP Introduction" />
                            </td>
                            <td style="width: 50%; white-space: nowrap">
                                <asp:CheckBox ID="chkInsurance" runat="server" CssClass="cmbFielde" Text="Life Insurance" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 3%">
                            </td>
                            <td style="width: 44%; white-space: nowrap">
                                <asp:CheckBox ID="chkProfileSummary" runat="server" CssClass="cmbFielde" Text="Profile Summary" />
                            </td>
                            <td style="width: 50%; white-space: nowrap">
                                <asp:CheckBox ID="chkGeneralInsurance" runat="server" CssClass="cmbFielde" Text="General Insurance" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 3%">
                            </td>
                            <td style="width: 44%; white-space: nowrap">
                                <asp:CheckBox ID="chkFinancialHealth" runat="server" CssClass="cmbFielde" Text="Financial Health" />
                            </td>
                            <td style="width: 50%; white-space: nowrap">
                                <asp:CheckBox ID="chkCurrentObservation" runat="server" CssClass="cmbFielde" Text="Current Situation and Observations" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 3%">
                            </td>
                            <td style="width: 44%; white-space: nowrap">
                                <%-- <asp:CheckBox ID="chkKeyAssumptions" runat="server" CssClass="cmbField" Text="Key Assumptions" />  --%>
                                <asp:CheckBox ID="chkGoalProfile" runat="server" CssClass="cmbFielde" Text="Goal Profiling" />
                            </td>
                            <td style="width: 50%; white-space: nowrap">
                                <asp:CheckBox ID="chkDisclaimer" runat="server" CssClass="cmbFielde" Text="Disclaimer" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 3%">
                            </td>
                            <td style="width: 44%; white-space: nowrap">
                                <%-- <asp:CheckBox ID="chkKeyAssumptions" runat="server" CssClass="cmbField" Text="Key Assumptions" />  --%>
                                <%-- <asp:CheckBox ID="CheckBox1" runat="server" CssClass="cmbField" Text="Goal Profiling" />   --%>
                            </td>
                            <td style="width: 50%; white-space: nowrap">
                                <asp:CheckBox ID="chkNotes" runat="server" CssClass="cmbFielde" Text="Notes" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </td>
    </tr>

</table>
</asp:Panel> </telerik:RadPageView> </telerik:RadMultiPage> </div>
<asp:HiddenField ID="hdnCustomerId" runat="server" OnValueChanged="hdnCustomerId_ValueChanged" />
