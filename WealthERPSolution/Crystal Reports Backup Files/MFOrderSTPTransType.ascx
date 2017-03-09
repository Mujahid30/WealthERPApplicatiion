<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFOrderSTPTransType.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.MFOrderSTPTransType" %>
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

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

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

<style>
    tr.spaceUnder > td
    {
        padding-bottom: .5em;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table class="tblMessage" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div class="divOnlinePageHeading">
                        <div class="divClientAccountBalance" id="divClientAccountBalance" runat="server">
                            <asp:Label ID="Label1" runat="server" Text="Available Limits:" CssClass="BalanceLabel"> </asp:Label>
                            <asp:Label ID="lblAvailableLimits" runat="server" Text="" CssClass="BalanceAmount"></asp:Label>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <table class="tblMessage" cellpadding="10" cellspacing="0">
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
            <tr id="trStepTwoHeading" runat="server">
                <td class="tdSectionHeading" colspan="6">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        <div class="divSectionHeadingNumber fltlftStep">
                            1
                        </div>
                        <div class="fltlft">
                            &nbsp
                            <asp:Label ID="lblHeading" runat="server" Text="Existing Scheme Details ( Transfer From Scheme Details )"></asp:Label>
                        </div>
                    </div>
                </td>
            </tr>
            <tr class="spaceUnder">
                <td>
                </td>
                <td align="right" style="vertical-align: top;">
                    <asp:Label ID="lblAmc" runat="server" Text="AMC:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAmc" runat="server" CssClass="cmbExtraLongField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlAmc_OnSelectedIndexChanged">
                    </asp:DropDownList>
                    <span id="Span1" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Select an AMC" Display="Dynamic" ControlToValidate="ddlAmc"
                        InitialValue="0" ValidationGroup="btnSubmit">
                    </asp:RequiredFieldValidator>
                </td>
                <td colspan="2">
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
                    </asp:DropDownList>
                    <span id="Span7" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Select Category"
                        CssClass="rfvPCG" ControlToValidate="ddlCategory" ValidationGroup="btnSubmit"
                        Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
                </td>
                <td colspan="2">
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
                        OnSelectedIndexChanged="ddlScheme_onSelectedChanged">
                    </asp:DropDownList>
                    <span id="Span2" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select a scheme"
                        InitialValue="0" CssClass="rfvPCG" ControlToValidate="ddlScheme" ValidationGroup="btnSubmit"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr class="spaceUnder">
                <td>
                </td>
                <td align="right" align="right" style="vertical-align: top;">
                    <asp:Label ID="lblFolioNumber" runat="server" Text="Folio Number:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlFolio" CssClass="cmbField" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlFolio_OnSelectedIndexChanged">
                        <asp:ListItem Text="Select" Value="select"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr class="spaceUnder">
                <td colspan="2">
                </td>
                <td align="left" style="vertical-align: top;" colspan="3">
                    <table width="75%" class="SchemeInfoTable">
                        <tr class="SchemeInfoTable">
                            <td align="left" style="vertical-align: top;">
                                <asp:Label ID="lblFolio" runat="server" Text="Folio Number:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFolioNo" runat="server" CssClass="readOnlyField"></asp:Label>
                            </td>
                            <td align="left" style="vertical-align: top;">
                                <asp:Label ID="lblInvseted" runat="server" Text="Invested Amount:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblInvestedVale" runat="server" CssClass="readOnlyField"></asp:Label>
                            </td>
                        </tr>
                        <tr class="SchemeInfoTable">
                            <td align="left" style="vertical-align: top;">
                                <asp:Label ID="lblSwitchNav" runat="server" Text=" Last Recorded NAV (Rs):" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblNavDisplay" runat="server" CssClass="readOnlyField"></asp:Label>
                            </td>
                            <td align="left" style="vertical-align: top;">
                                <asp:Label ID="lblMin" runat="server" Text="Minimum Initial Amount:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblMintxt" runat="server" CssClass="readOnlyField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="vertical-align: top;">
                                <asp:Label ID="lblCutt" runat="server" Text="Cut-Off time:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbltime" runat="server" Text="" CssClass="readOnlyField"></asp:Label>
                            </td>
                            <td align="left" style="vertical-align: top;">
                                <asp:Label ID="lblMultiple" runat="server" Text="Subsequent Amount:</br>(In Multiples Of)"
                                    CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblMulti" runat="server" CssClass="readOnlyField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="vertical-align: top;">
                                <asp:Label ID="lblAmnt" runat="server" Text="Current Value of Holdings:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblAmtVale" runat="server" CssClass="readOnlyField"></asp:Label>
                                <asp:TextBox ID="txtAmtVale" runat="server" Visible="false" Text=""></asp:TextBox>
                            </td>
                            <td align="left" style="vertical-align: top;">
                                <asp:Label ID="lblunits" runat="server" Text="Unit Held:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblUnitsVale" runat="server" CssClass="readOnlyField"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="trJointHolder" runat="server" class="spaceUnder">
                <td>
                </td>
                <td align="right" style="vertical-align: top;">
                    <asp:Label ID="lblHolder" runat="server" Text="Joint Holder:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblHolderDisplay" runat="server" CssClass="readOnlyField"></asp:Label>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr id="trNominee" runat="server" class="spaceUnder">
                <td>
                </td>
                <td align="right" style="vertical-align: top;">
                    <asp:Label ID="lblNominee" runat="server" Text="Nominee:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblNomineeDisplay" runat="server" CssClass="readOnlyField"></asp:Label>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr class="spaceUnder" id="trDivtype" visible="false" runat="server">
                <td>
                </td>
                <td align="right" style="vertical-align: top;">
                    <asp:Label ID="lblDivType" runat="server" Text="Dividend Type:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDivType" runat="server" CssClass="cmbField" Style="width: 250px;"
                        OnSelectedIndexChanged="ddlDivType_OnSelectedIndexChanged">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Dividend Reinvestment" Value="DVR"></asp:ListItem>
                        <asp:ListItem Text="Dividend Payout" Value="DVP"></asp:ListItem>
                    </asp:DropDownList>
                    <span id="Span4" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Select an Dividend Type" Display="Dynamic" ControlToValidate="ddlDivType"
                        InitialValue="0" ValidationGroup="btnSubmit">
                    </asp:RequiredFieldValidator>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr class="spaceUnder" id="trDivFre" visible="false" runat="server">
                <td>
                </td>
                <td align="right" style="vertical-align: top;">
                    <asp:Label ID="lblDividendFrequency" runat="server" Text="Dividend Frequency:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbldftext" runat="server" CssClass="txtField"></asp:Label>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr id="Tr1" class="spaceUnder" runat="server" visible="false">
                <td>
                </td>
                <td align="right" style="vertical-align: top;">
                    <asp:Label ID="lblOption" runat="server" Text="Option:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDividendType" runat="server" CssClass="txtField"></asp:Label>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr id="tr2" runat="server">
                <td class="tdSectionHeading" colspan="6">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        <div class="divSectionHeadingNumber fltlftStep">
                            2
                        </div>
                        <div class="fltlft">
                            &nbsp
                            <asp:Label ID="lblTrnsfer" runat="server" Text="Scheme Details ( Transfer To Scheme Details)"></asp:Label>
                        </div>
                    </div>
                </td>
            </tr>
            <tr class="spaceUnder">
                <td>
                </td>
                <td align="right" style="vertical-align: top;">
                    <asp:Label ID="lblTypeScheme" runat="server" Text="Select Type Of Scheme:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSchemeType" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlSchemeType_OnSelectedIndexChanged">
                        <asp:ListItem Text="Select" Value="select"></asp:ListItem>
                        <asp:ListItem Text="New" Value="N"></asp:ListItem>
                        <asp:ListItem Text="Existing" Value="E"></asp:ListItem>
                    </asp:DropDownList>
                    <span id="Span5" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Select an Scheme" Display="Dynamic" ControlToValidate="ddlSchemeType"
                        InitialValue="select" ValidationGroup="btnSubmit">
                    </asp:RequiredFieldValidator>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr class="spaceUnder">
                <td>
                </td>
                <td align="right" style="vertical-align: top;">
                    <asp:Label ID="lblSwtchShmCat" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSwtchShmCat" runat="server" CssClass="cmbField" AutoPostBack="true">
                    </asp:DropDownList>
                    <span id="Span6" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Select Category"
                        CssClass="rfvPCG" ControlToValidate="ddlSwtchShmCat" ValidationGroup="btnSubmit"
                        Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr class="spaceUnder">
                <td>
                </td>
                <td align="right" style="vertical-align: top;">
                    <asp:Label ID="lblSwtchScheme" runat="server" Text="Scheme:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSchemeName" runat="server" CssClass="cmbExtraLongField"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlSchemeName_OnSelectedIndexChanged">
                    </asp:DropDownList>
                    <span id="Span8" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Select a scheme"
                        InitialValue="0" CssClass="rfvPCG" ControlToValidate="ddlSchemeName" ValidationGroup="btnSubmit"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr class="spaceUnder" id="tr5" visible="false" runat="server">
                <td>
                </td>
                <td align="right" style="vertical-align: top;">
                    <asp:Label ID="lblDvdntType" runat="server" Text="Dividend Type:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSwitchDvdnType" runat="server" CssClass="cmbField" Style="width: 250px;"
                        OnSelectedIndexChanged="ddlDivType_OnSelectedIndexChanged">
                    </asp:DropDownList>
                    <span id="Span11" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Select an Dividend Type" Display="Dynamic" ControlToValidate="ddlSwitchDvdnType"
                        InitialValue="0" ValidationGroup="btnSubmit">
                    </asp:RequiredFieldValidator>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr class="spaceUnder">
                <td colspan="2">
                </td>
                <td align="left" style="vertical-align: top;" colspan="3">
                    <table width="75%" class="SchemeInfoTable">
                        <tr class="SchemeInfoTable">
                            <td align="left" style="vertical-align: top;">
                                <asp:Label ID="lblNav" runat="server" Text=" Last Recorded NAV (Rs):" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblNavVale" runat="server" CssClass="readOnlyField"></asp:Label>
                            </td>
                            <td align="left" style="vertical-align: top;">
                                <asp:Label ID="lblMinAmnt" runat="server" Text="Minimum Initial Amount:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblMinAmntVale" runat="server" CssClass="readOnlyField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="vertical-align: top;">
                                <asp:Label ID="lblCuffTime" runat="server" Text="Cut-Off time:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCuffTimeVale" runat="server" Text="" CssClass="readOnlyField"></asp:Label>
                            </td>
                            <td align="left" style="vertical-align: top;">
                                <asp:Label ID="lblSqnAmnt" runat="server" Text="Subsequent Amount:</br>(In Multiples Of)"
                                    CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSqnAmtVale" runat="server" CssClass="readOnlyField"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="spaceUnder">
                <td>
                </td>
                <td align="right" align="right" style="vertical-align: top;">
                    <asp:Label ID="LblSwitchFolio" runat="server" Text="Folio Number:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSwitchFolio" CssClass="cmbField" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr id="tr3" runat="server" class="spaceUnder" visible="false">
                <td>
                </td>
                <td align="right" style="vertical-align: top;">
                    <asp:Label ID="lblJntHld" runat="server" Text="Joint Holder:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblJntHldName" runat="server" CssClass="readOnlyField"></asp:Label>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr id="tr4" runat="server" class="spaceUnder" visible="false">
                <td>
                </td>
                <td align="right" style="vertical-align: top;">
                    <asp:Label ID="lblSwtchNominee" runat="server" Text="Nominee:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblNomineeName" runat="server" CssClass="readOnlyField"></asp:Label>
                </td>
                <td colspan="2">
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
                    <asp:DropDownList ID="ddlStartDate" CssClass="cmbField" runat="server" AutoPostBack="false"
                         ValidationGroup="btnSubmit">
                        <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                    </asp:DropDownList>
                    <span id="Span3" class="spnRequiredField">*</span>
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
                    <asp:DropDownList ID="ddlTotalInstallments" CssClass="cmbField" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTotalInstallments_SelectedIndexChanged">
                        <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                    </asp:DropDownList>
                    <span id="Span12" class="spnRequiredField">*</span>
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
                    <asp:Label ID="lblSwitchAmnt" runat="server" Text="Amount:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSwitchAmnt" runat="server" CssClass="txtField" MaxLength="11"></asp:TextBox>
                    <span id="Span10" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Select a Amount"
                        CssClass="rfvPCG" ControlToValidate="txtSwitchAmnt" ValidationGroup="btnSubmit"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtSwitchAmnt"
                        ErrorMessage="Please Enter Only Numbers and 2 digits after decimal " CssClass="rfvPCG"
                        ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$" ValidationGroup="btnSubmit" Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="cvAmnt" ControlToValidate="txtSwitchAmnt" runat="server"
                        Operator="LessThanEqual" CssClass="rfvPCG" ValidationGroup="btnSubmit" ControlToCompare="txtAmtVale"
                        ErrorMessage="Please Enter Value Than" Display="Dynamic"></asp:CompareValidator>
                </td>
                <td colspan="2">
                </td>
            </tr>
             <tr style="display: none;" id="trDividendType" runat="server" class="spaceUnder">
                    <td>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label2" runat="server" Text="DividendType:" CssClass="FieldName"></asp:Label>
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
                        <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                        <asp:ListItem Text="Dividend Reinvestment" Value="DVR"></asp:ListItem>
                        <asp:ListItem Text="Dividend Payout" Value="DVP"></asp:ListItem>
                    </asp:DropDownList>
                    <span id="Span13" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please select Dividend Option"
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
            <%--  <tr class="spaceUnder" id="tr6" visible="false" runat="server">
                <td>
                </td>
                <td align="right" style="vertical-align: top;">
                    <asp:Label ID="lblSwtchDvdFrq" runat="server" Text="Dividend Frequency:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblSwtchDvdFrqValue" runat="server" CssClass="txtField"></asp:Label>
                </td>
                <td colspan="2">
                </td>
            </tr>--%>
            <tr id="Tr7" class="spaceUnder" runat="server" visible="false">
                <td>
                </td>
                <td align="right" style="vertical-align: top;">
                    <asp:Label ID="lblOptn" runat="server" Text="Option:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblOptnVale" runat="server" CssClass="txtField"></asp:Label>
                </td>
                <td colspan="2">
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
                <td style="width: 150px;" colspan="2">
                </td>
                <td align="left">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="OnClick_Submit"
                        CssClass="PCGButton" ValidationGroup="btnSubmit"></asp:Button>
                </td>
                <%--<td >
             <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="ResetControlDetails"
                    CssClass="FieldName"></asp:Button>
            </td>--%>
                <td>
                </td>
            </tr>
            <tr id="trNewOrder" runat="server" visible="false">
                <td align="center" colspan="4">
                    <asp:LinkButton ID="lnkNewOrder" CausesValidation="false" Text="Make another NewPurchase"
                        runat="server" OnClick="lnkNewOrder_Click" CssClass="LinkButtons"></asp:LinkButton>
                </td>
            </tr>
        </table>
        <div style="float: left; padding-top: 5px; display: none;">
            <table style="border-style: solid; border-width: 2px; border-color: Blue">
                <tr class="spaceUnder">
                    <td>
                        <asp:Label ID="lblUsefulLinks" CausesValidation="false" runat="server" Text="Quick Links:"
                            CssClass="FieldName"></asp:Label>
                    </td>
                </tr>
                <tr class="spaceUnder">
                    <td>
                        <asp:LinkButton ID="lnkOfferDoc" CausesValidation="false" Text="Offer Doc" runat="server"
                            CssClass="txtField"></asp:LinkButton>
                    </td>
                </tr>
                <tr class="spaceUnder">
                    <td>
                        <asp:LinkButton ID="lnkFactSheet" CausesValidation="false" Text="Fact Sheet" runat="server"
                            CssClass="txtField"></asp:LinkButton>
                    </td>
                </tr>
                <tr class="spaceUnder">
                    <td>
                        <asp:LinkButton ID="lnkExitLoad" CausesValidation="false" runat="server" Text="Exit Load"
                            CssClass="txtField"></asp:LinkButton>
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
                                    OnClientClick="return PreventClicks();" ValidationGroup="btnSubmit"></asp:Button>
                                <asp:Button runat="server" ID="rbConfirm_Cancel" Text="Cancel" OnClientClicked="closeCustomConfirm">
                                </asp:Button>
                            </div>
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
    </ContentTemplate>
    <Triggers>
    </Triggers>
</asp:UpdatePanel>
