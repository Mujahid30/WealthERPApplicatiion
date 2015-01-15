<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFOrderSIPTransType.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.MFOrderSIPTransType" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.2.6.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="//ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.bxslider.js" type="text/javascript"></script>

<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<style type="text/css">
    tr.spaceUnder > td
    {
        padding-bottom: .5em;
    }
</style>

<script type="text/javascript">
    function ValidateTermsConditions(sender, args) {

        if (document.getElementById("<%=chkTermsCondition.ClientID %>").checked == true) {
            args.IsValid = true;
        } else {
            args.IsValid = false;
        }
    }
</script>

<script type="text/javascript">
    function EUINConfirm() {
        if (confirm("I/We here by confirm that this is an execution-only transaction without any iteraction or advice by the employee/relationship manager/sales person of the above distributor or notwithstanding the advice of in-appropriateness, if any, provided by the employee/relationship manager/sales person of the distributor and the distributor has not chargedany advisory fees on this transaction ")) {
            return true;
        }
        return false;
    }
</script>

<script type="text/javascript">
    function closeCustomConfirm() {
        $find("<%=rw_customConfirm.ClientID %>").close();
    }
</script>

<script language="javascript" type="text/javascript">
    var crnt = 0;
    function PreventClicks() {
        if (typeof (Page_ClientValidate('btnSubmit')) == 'function') {
            Page_ClientValidate();
        }

        if (Page_IsValid) {
            if (++crnt > 1) {
                alert(crnt);
                return false;
            }
            return true;
        }
        else {
            return false;
        }
    }
</script>

<script type="text/jscript">
    //     \Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    jQuery(document).ready(function($) {
        $('.bxslider').bxSlider(
    {
        auto: true,
        autoControls: true
    }
    );
    });
    

//    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
//    function EndRequestHandler(sender, args) {
//        if (args.get_error() == undefined) {
//            jQuery(document).ready(function($) {
//                $('.bxslider').bxSlider();
//            });
//        }
//    }
</script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table class="tblMessage" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div class="divOnlinePageHeading">
                        <div class="divClientAccountBalance" id="divClientAccountBalance" runat="server">
                            <asp:Label ID="lblUserAccount" runat="server" Text="" CssClass="BalanceAmount"></asp:Label>
                            <asp:Label ID="lblTest" runat="server" Text="" CssClass="BalanceAmount"></asp:Label>
                            <asp:Label ID="Label1" runat="server" Text="Available Limits:" CssClass="BalanceLabel"> </asp:Label>
                            <asp:Label ID="lblAvailableLimits" runat="server" Text="" CssClass="BalanceAmount"></asp:Label>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <table class="tblMessage" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center">
                    <div id="divMessage" align="center">
                    </div>
                    <div style="clear: both">
                    </div>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr align="center">
                <td align="center">
                    <div id="divValidationError" runat="server" class="failure-msg" align="center" visible="true">
                        <asp:ValidationSummary ID="vsSummary" runat="server" Visible="true" ValidationGroup="btnSubmit"
                            ShowSummary="true" DisplayMode="BulletList" />
                    </div>
                </td>
            </tr>
        </table>
        <div style="float: left;" id="divControlContainer" runat="server">
            <table>
                <tr class="spaceUnder">
                    <td>
                    </td>
                    <td align="right" style="vertical-align: top;">
                        <asp:Label ID="lblAmc" runat="server" Text="AMC:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAmc" runat="server" CssClass="cmbExtraLongField" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlAmc_OnSelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                        </asp:DropDownList>
                        <span id="Span7" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="rfvAmc" runat="server" CssClass="rfvPCG" ErrorMessage="Please Select an AMC"
                            Display="Dynamic" ControlToValidate="ddlAmc" InitialValue="0" ValidationGroup="btnSubmit">Please Select an AMC</asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="spaceUnder">
                    <td>
                    </td>
                    <td align="right" style="vertical-align: top;">
                        <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged">
                            <asp:ListItem Text="ALL" Value="ALL"></asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                        </asp:DropDownList>
                        <span id="Span8" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ErrorMessage="Please select a category"
                            CssClass="rfvPCG" ControlToValidate="ddlCategory" ValidationGroup="btnSubmit"
                            Display="Dynamic" InitialValue="0">Please select a category</asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="spaceUnder">
                    <td>
                    </td>
                    <td align="right" style="vertical-align: top;">
                        <asp:Label ID="lblScheme" runat="server" Text="Scheme:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbExtraLongField" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                            <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                        </asp:DropDownList>
                        <span id="Span1" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ErrorMessage="Please select a scheme"
                            CssClass="rfvPCG" ControlToValidate="ddlScheme" Display="Dynamic" InitialValue="0"
                            ValidationGroup="btnSubmit">Please select a scheme</asp:RequiredFieldValidator>
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
                <tr class="spaceUnder">
                    <td colspan="2">
                    </td>
                    <td align="left" style="vertical-align: top;" colspan="3">
                        <table width="100%" class="SchemeInfoTable">
                            <tr class="SchemeInfoTable">
                                <td align="left">
                                    <asp:Label ID="lblNav" runat="server" Text="Last Recorded NAV(Rs):" CssClass="FieldName"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblNavDisplay" runat="server" CssClass="readOnlyField"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblMinAmountrequired" runat="server" Text="Minimum Initial Amount:"
                                        CssClass="FieldName"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblMinAmountrequiredDisplay" runat="server" CssClass="readOnlyField"></asp:Label>
                                </td>
                                <td rowspan="4">
                                    <a href="#" class="popper" data-popbox="divSchemeRatingDetails"><span class="FieldName">
                                        Scheme Rating</span><br />
                                        <asp:Image runat="server" ID="imgSchemeRating" />
                                    </a>
                                </td>
                            </tr>
                            <tr class="SchemeInfoTable">
                                <td align="left">
                                    <asp:Label ID="lblCutOffTime" runat="server" Text="Cut-Off time:" CssClass="FieldName"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCutOffTimeDisplay" runat="server" CssClass="readOnlyField"></asp:Label>
                                </td>
                                <td style="vertical-align: top;" align="left">
                                    <asp:Label ID="lblMutiplesThereAfter" runat="server" CssClass="FieldName" Text="Subsequent Amount:</br>(In Multiples Of)"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblMutiplesThereAfterDisplay" runat="server" CssClass="readOnlyField"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" id="trSchemeRating">
                                <td colspan="5" align="center">
                                    <div id="divSchemeRatingDetails" class="popbox">
                                        <h2 class="popup-title">
                                            SCHEME RATING DETAILS
                                        </h2>
                                        <table border="1" cellpadding="1" cellspacing="2" style="border-collapse: collapse;">
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    <span class="readOnlyField">RATING</span>
                                                </td>
                                                <td>
                                                    <span class="readOnlyField">RETURN</span>
                                                </td>
                                                <td>
                                                    <span class="readOnlyField">RISK</span>
                                                </td>
                                                <td>
                                                    <span class="readOnlyField">RATING OVERALL</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span class="readOnlyField">3 YEAR</span>
                                                </td>
                                                <td>
                                                    <asp:Image runat="server" ID="imgRating3yr" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSchemeRetrun3yr" runat="server" CssClass="readOnlyField"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSchemeRisk3yr" runat="server" CssClass="readOnlyField"></asp:Label>
                                                </td>
                                                <td rowspan="3">
                                                    <asp:Image runat="server" ID="imgRatingDetails" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span class="readOnlyField">5 YEAR</span>
                                                </td>
                                                <td>
                                                    <asp:Image runat="server" ID="imgRating5yr" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSchemeRetrun5yr" runat="server" CssClass="readOnlyField"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSchemeRisk5yr" runat="server" CssClass="readOnlyField"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span class="readOnlyField">10 YEAR</span>
                                                </td>
                                                <td>
                                                    <asp:Image runat="server" ID="imgRating10yr" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSchemeRetrun10yr" runat="server" CssClass="readOnlyField"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSchemeRisk10yr" runat="server" CssClass="readOnlyField"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="popup-overlay">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="spaceUnder">
                    <td>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblFolioNumber" runat="server" Text="Folio Number:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList OnSelectedIndexChanged="ddlFolio_SelectedIndexChanged" ID="ddlFolio"
                            CssClass="cmbField" runat="server" AutoPostBack="True">
                            <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                        </asp:DropDownList>
                        <span id="Span2" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="rfvFolio" runat="server" ErrorMessage="Please select folio number"
                            CssClass="rfvPCG" ControlToValidate="ddlFolio" Display="Dynamic" InitialValue="0"
                            ValidationGroup="btnSubmit">Please select folio number</asp:RequiredFieldValidator>
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
                <tr class="spaceUnder" id="trNomineeJoint" runat="server">
                    <td colspan="2">
                    </td>
                    <td align="left" style="vertical-align: top;" colspan="3">
                        <table width="100%" class="SchemeInfoTable" id="tblNomineeJoint" runat="server">
                            <tr id="trNominee" runat="server">
                                <td align="left">
                                    <asp:Label ID="lblHolder" runat="server" Text="Joint Holder:" CssClass="FieldName"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblHolderDisplay" runat="server" CssClass="readOnlyField"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trJointHolder" runat="server">
                                <td align="left">
                                    <asp:Label ID="lblNominee" runat="server" Text="Nominee:" CssClass="FieldName"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblNomineeDisplay" runat="server" CssClass="readOnlyField"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="spaceUnder">
                    <td>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblUnitHeld" runat="server" Text="Unit Held:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblUnitHeldDisplay" runat="server" CssClass="readOnlyField"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="spaceUnder">
                    <td>
                    </td>
                    <td align="right" style="vertical-align: top;">
                        <asp:Label ID="lblFrequency" runat="server" Text="SIP Frequency:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFrequency" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlFrequency_OnSelectedIndexChanged"
                            AutoPostBack="True">
                            <asp:ListItem Text="--SELECT--" Value="0" Selected="True"></asp:ListItem>
                            <%--  <asp:ListItem Text="Quarterly" Value="QT"></asp:ListItem>--%>
                        </asp:DropDownList>
                        <span id="Span3" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="rfvFrequency" runat="server" ErrorMessage="Please select frequency"
                            CssClass="rfvPCG" ControlToValidate="ddlFrequency" Display="Dynamic" InitialValue="0"
                            ValidationGroup="btnSubmit">Please select frequency</asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="spaceUnder">
                    <td>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label3" runat="server" Text="Start Date:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStartDate" CssClass="cmbField" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlStartDate_SelectedIndexChanged" ValidationGroup="btnSubmit">
                            <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                        </asp:DropDownList>
                        <span id="Span4" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ErrorMessage="Please select a start date"
                            InitialValue="0" ControlToValidate="ddlStartDate" ValidationGroup="btnSubmit"
                            CssClass="rfvPCG" Display="Dynamic">Please select a start date</asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="spaceUnder">
                    <td>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblTotalInstallments" runat="server" Text="Total Installments:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTotalInstallments" CssClass="cmbField" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlTotalInstallments_SelectedIndexChanged">
                            <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                        </asp:DropDownList>
                        <span id="Span5" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="rfvInstallments" runat="server" ErrorMessage="Please select a value"
                            ControlToValidate="ddlTotalInstallments" InitialValue="0" ValidationGroup="btnSubmit"
                            CssClass="rfvPCG" Display="Dynamic">Please select a value</asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="spaceUnder">
                    <td>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblEndDate" runat="server" Text="End Date:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndDateDisplay" runat="server" CssClass="readOnlyField"></asp:Label>
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
                <tr class="spaceUnder">
                    <td>
                    </td>
                    <td align="right" style="vertical-align: top;">
                        <asp:Label ID="lblAmount" runat="server" Text="Amount:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField"></asp:TextBox>
                        <span id="Span6" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ErrorMessage="Please enter a valid amount (e.g. 500.0,  100.15)"
                            ValidationGroup="btnSubmit" ControlToValidate="txtAmount" Display="Dynamic" CssClass="rfvPCG">Please enter a valid amount</asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="rgvAmount" runat="server" ControlToValidate="txtAmount" ErrorMessage="You have entered the amount less than the Minimum Initial Amount"
                            Type="Double" ValidationGroup="btnSubmit" CssClass="rfvPCG" Display="Dynamic"> You should enter the amount in multiple of subsequent amount</asp:RangeValidator>
                    </td>
                    <td style="vertical-align: top;" colspan="2">
                        <asp:TextBox Style="display: none;" ID="txtMinAmtDisplay" CssClass="txtField" Enabled="false"
                            runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display: none;" id="trDividendType" runat="server" class="spaceUnder">
                    <td>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblDividendType" runat="server" Text="DividendType:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDividendTypeDisplay" runat="server" CssClass="readOnlyField"></asp:Label>
                    </td>
                    <td align="right">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr visible="false" id="trDividendFrequency" runat="server" class="spaceUnder">
                    <td>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblDividendOption" runat="server" Text="Dividend Option:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDividendFreq" CssClass="cmbField" Style="width: 300px;"
                            runat="server">
                            <%-- <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                            <asp:ListItem Text="Dividend Reinvestment" Value="DVR"></asp:ListItem>
                            <asp:ListItem Text="Dividend Payout" Value="DVP"></asp:ListItem>--%>
                        </asp:DropDownList>
                        <span id="Span10" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select Dividend Option"
                            ControlToValidate="ddlDividendFreq" InitialValue="0" ValidationGroup="btnSubmit"
                            CssClass="rfvPCG" Display="Dynamic">Please select Dividend Option</asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr id="trDividendOption" runat="server" class="spaceUnder" visible="false">
                    <td>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblDividendFreq" runat="server" Text="Dividend Frequency:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDividendOption" CssClass="cmbField" runat="server" AutoPostBack="True">
                            <asp:ListItem Selected="True" Value="--SELECT--">--SELECT--</asp:ListItem>
                            <asp:ListItem Text="Quarterly" Value="QTR"></asp:ListItem>
                            <asp:ListItem Text="Monthly" Value="MN"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="spaceUnder" id="trTermsCondition" runat="server">
                    <td style="width: 150px;">
                        &nbsp;
                    </td>
                    <td align="right">
                        <asp:CheckBox ID="chkTermsCondition" runat="server" Font-Bold="True" Font-Names="Shruti"
                            Enabled="false" Checked="false" ForeColor="#145765" Text="" ToolTip="Click 'Terms & Conditions' to proceed further"
                            CausesValidation="true" />
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="lnkTermsCondition" CausesValidation="false" Text="Terms & Conditions"
                            runat="server" CssClass="txtField" OnClick="lnkTermsCondition_Click" ToolTip="Click here to accept terms & conditions"></asp:LinkButton>
                        <span id="Span9" class="spnRequiredField">*</span>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please read terms & conditions"
                            ClientValidationFunction="ValidateTermsConditions" EnableClientScript="true"
                            OnServerValidate="TermsConditionCheckBox" Display="Dynamic" ValidationGroup="btnSubmit"
                            CssClass="rfvPCG">
                    Please read terms & conditions
                        </asp:CustomValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="spaceUnder">
                    <td style="width: 150px;">
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ValidationGroup="btnSubmit" ID="btnSubmit" runat="server" CssClass="PCGButton"
                            OnClick="btnSubmit_Click"></asp:Button>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr id="trNewOrder" runat="server" visible="false">
                    <td align="center" colspan="4">
                        <asp:LinkButton ID="lnkNewOrder" CausesValidation="false" Text="Make another SIP"
                            runat="server" OnClick="lnkNewOrder_Click" CssClass="LinkButtons"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        <div style="float: left; padding-top: 5px; display: none;">
            <table style="border-style: solid; border-width: 2px; border-color: Blue">
                <tr class="spaceUnder">
                    <td>
                        <asp:Label ID="lblUsefulLinks" runat="server" Text="Quick Links:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="spaceUnder">
                    <td>
                        <asp:LinkButton ID="lnkOfferDoc" CausesValidation="false" Text="Offer Doc" runat="server"
                            CssClass="txtField"></asp:LinkButton>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="spaceUnder">
                    <td>
                        <asp:LinkButton ID="lnkFactSheet" CausesValidation="false" Text="Fact Sheet" runat="server"
                            CssClass="txtField"></asp:LinkButton>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="spaceUnder">
                    <td>
                        <asp:LinkButton ID="lnkExitLoad" CausesValidation="false" runat="server" Text="Exit Load"
                            CssClass="txtField" OnClick="lnkExitLoad_Click"></asp:LinkButton>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblExitLoad"></asp:Label>
                    </td>
                </tr>
                <tr style="display: none;" class="spaceUnder">
                    <td>
                        <asp:LinkButton ID="lnkExitDetails" Text="Exit Details" runat="server" CssClass="txtField"></asp:LinkButton>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <telerik:RadWindow ID="rwTermsCondition" runat="server" VisibleOnPageLoad="false"
            Width="1000px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Behaviors="Move, Resize,Close"
            Title="Terms & Conditions" EnableShadow="true" Left="580" Top="-8">
            <ContentTemplate>
                <div style="padding: 0px; width: 100%">
                    <table width="100%" cellpadding="0" cellpadding="0">
                        <tr>
                            <td align="left">
                                <%--  <a href="../ReferenceFiles/MF-Terms-Condition.html">../ReferenceFiles/MF-Terms-Condition.html</a>--%>
                                <iframe src="../ReferenceFiles/MF-Terms-Condition.html" name="iframeTermsCondition"
                                    style="width: 100%"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnAccept" runat="server" Text="Accept" CssClass="PCGButton" OnClick="btnAccept_Click"
                                    CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
        <telerik:RadWindowManager runat="server" ID="RadWindowManager1">
            <Windows>
                <telerik:RadWindow ID="rw_customConfirm" Modal="true" Behaviors="Close, Move" VisibleStatusbar="false"
                    Width="700px" Height="160px" runat="server" Title="EUIN Confirm">
                    <ContentTemplate>
                        <div class="rwDialogPopup radconfirm">
                            <div class="rwDialogText">
                                <asp:Label ID="confirmMessage" Text="" runat="server" />
                            </div>
                            <div>
                                <asp:Button runat="server" ID="rbConfirm_OK" Text="OK" OnClick="rbConfirm_OK_Click"
                                    ValidationGroup="btnSubmit" OnClientClick="return PreventClicks();"></asp:Button>
                                <asp:Button runat="server" ID="rbConfirm_Cancel" Text="Cancel" OnClientClicked="closeCustomConfirm">
                                </asp:Button>
                            </div>
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <div style="float: inherit;">
            <asp:HiddenField ID="hdnAccountId" runat="server" />
        </div>
    </ContentTemplate>
    <Triggers>
    </Triggers>
</asp:UpdatePanel>

<table width="100%">
<tr>
<td align="center">
<div style="float:left;width:90%">
    <ul class="bxslider">
        <li>
            <img src="../Images/InvestorPageSlider/1.jpg" /></li>
        <li>
            <img src="../Images/InvestorPageSlider/2.jpg" /></li>
        <li>
            <img src="../Images/InvestorPageSlider/3.jpg" /></li>
        <li>
            <img src="../Images/InvestorPageSlider/4.jpg" /></li>
        <li>
            <img src="../Images/InvestorPageSlider/5.jpg" /></li>
    </ul>
</div>
</td>
</tr>
</table>


