<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFOrderRdemptionTransType.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.MFOrderRdemptionTransType" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<link href="../Base/CSS/bootstrap-theme.css" rel="stylesheet" type="text/css" />
<link href="../Base/CSS/bootstrap.css" rel="stylesheet" type="text/css" />
<link href="../Base/CSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<style>
    tr.spaceUnder > td
    {
        padding-bottom: .5em;
    }
    .fontsize
    {
        font-family: Times New Roman;
        font-weight: bold;
        font-size: smaller;
        color: #0396cc;
        vertical-align: middle;
        text-align: right;
    }
    .fieldFontSize
    {
        font-family: Times New Roman;
        font-weight: bold;
        font-size: smaller;
        color: #104259;
    }
</style>

<script type="text/javascript">
    function blink() {
        var blinks = document.getElementsByTagName('blink');
        for (var i = blinks.length - 1; i >= 0; i--) {
            var s = blinks[i];
            s.style.visibility = (s.style.visibility === 'visible') ? 'hidden' : 'visible';
        }
        window.setTimeout(blink, 1000);
    }
    if (document.addEventListener) document.addEventListener("DOMContentLoaded", blink, false);
    else if (window.addEventListener) window.addEventListener("load", blink, false);
    else if (window.attachEvent) window.attachEvent("onload", blink);
    else window.onload = blink;
</script>

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

<script type="text/jscript">


    //    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(PopupEndRequestHandler);
    
</script>

<body style="background-color: #E5F6FF; margin-left: 50px; margin-right: 50px;">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="height: 200px">
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
                <div style="margin-right: 50px; width: 100%" id="divControlContainer" runat="server">
                    <div class="col-md-12  col-xs-12 col-sm-12">
                        <div class="col-md-3">
                            <b class="fontsize">AMC:</b><asp:Label ID="lblAmc" runat="server" CssClass="fieldFontSize"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <b class="fontsize">Category:</b>
                            <asp:Label ID="lblCategory" runat="server" CssClass="fieldFontSize"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <b class="fontsize">Scheme:</b>
                            <asp:Label ID="lblScheme" runat="server" CssClass="fieldFontSize"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:ImageButton ID="imgInformation" runat="server" ImageUrl="../Images/help.png"
                                OnClick="imgInformation_OnClick" ToolTip="Help" Style="cursor: hand; float: right;" /></div>
                    </div>
                    <div class="col-md-12  col-xs-12 col-sm-12" style="border: 1px solid #ccc;">
                        <div class="col-md-3">
                            <b class="fontsize">NAV (Rs):</b>
                            <asp:Label ID="lblNavDisplay" runat="server" CssClass="fieldFontSize"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <b class="fontsize">Minimum Amount(Rs):</b>
                            <asp:Label ID="lblMinAmountValue" runat="server" Text="" CssClass="fieldFontSize"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <b class="fontsize">Minimum Units:</b>
                            <asp:Label ID="lblMinUnitValue" runat="server" Text="" CssClass="fieldFontSize"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <b class="fontsize">Cut-Off time:</b>
                            <asp:Label ID="lbltime" runat="server" Text="" CssClass="fieldFontSize"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-12  col-xs-12 col-sm-12" style="border: 1px solid #ccc;">
                        <div class="col-md-12">
                            <table>
                                <tr>
                                    <td rowspan="4">
                                        <a href="#" class="popper" data-popbox="divSchemeRatingDetails"><span class="fontsize">
                                            Scheme Rating</span>
                                            <asp:Image runat="server" ID="imgSchemeRating" />
                                            <asp:Label ID="lblSchemeRatingAsOn" runat="server" CssClass="fieldFontSize"></asp:Label>
                                        </a>
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
                                                        <asp:Label ID="lblRatingAsOnPopUp" runat="server" CssClass="readOnlyField"></asp:Label>
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
                        </div>
                    </div>
                    <div class="col-md-12  col-xs-12 col-sm-12" style="border: 1px solid #ccc;">
                        <div class="col-md-3">
                            <b class="fontsize">Joint Holder:</b>
                            <asp:Label ID="lblHolderDisplay" runat="server" CssClass="fieldFontSize"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <b class="fontsize">Nominee:</b>
                            <asp:Label ID="lblNomineeDisplay" runat="server" CssClass="fieldFontSize"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <b class="fontsize">Units Held:</b>
                            <asp:Label ID="lblUnitsheldDisplay" runat="server" CssClass="fieldFontSize"></asp:Label>
                            <%--<labe id="lblblink" runat="server" visible="false" CssClass="FieldName">Text to blink here</blink>--%>
                            <blink><asp:Label ID="lblMsg" runat="server" Visible="false"  
                                     Text="Units under lock in period"  CssClass="FieldName"></asp:Label></blink>
                        </div>
                        <div class="col-md-3">
                            <b class="fontsize">Current Value Of Holdings:</b>
                            <asp:Label ID="lblCurrentValueDisplay" runat="server" CssClass="fieldFontSize"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-12  col-xs-12 col-sm-12">
                        <div class="col-md-3">
                            <b class="fontsize">Folio Number:</b>
                            <asp:DropDownList ID="ddlFolio" CssClass="cmbField" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlFolio_onSelectedChanged">
                            </asp:DropDownList>
                            <span id="Span3" class="spnRequiredField">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Select a Folio"
                                CssClass="rfvPCG" ControlToValidate="ddlFolio" ValidationGroup="btnSubmit" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <b class="fontsize">Redeem:</b>
                            <asp:DropDownList ID="ddlRedeem" runat="server" AutoPostBack="true" CssClass="cmbField"
                                OnSelectedIndexChanged="ddlRedeem_OnSelectedIndexChanged">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Units" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Amount (Rs)" Value="2"></asp:ListItem>
                                <asp:ListItem Text="All Units" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="rfvPCG"
                                ErrorMessage="Please Select an Redeem Type" Display="Dynamic" ControlToValidate="ddlRedeem"
                                InitialValue="0" ValidationGroup="btnSubmit">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <table>
                                <tr class="spaceUnder" id="trRedeemType" runat="server">
                                    <td align="right" style="vertical-align: top;">
                                        <asp:Label ID="lblRedeemType" runat="server" CssClass="fontsize"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRedeemTypeValue" runat="server" CssClass="txtField" MaxLength="11"></asp:TextBox>
                                        <span id="Span4" class="spnRequiredField">*</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please enter required value"
                                            CssClass="rfvPCG" ControlToValidate="txtRedeemTypeValue" ValidationGroup="btnSubmit"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                        </br>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtRedeemTypeValue"
                                            ErrorMessage="Please Enter Only Numbers and 2 digits after Decimal. " CssClass="rfvPCG"
                                            ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$" ValidationGroup="btnSubmit" Display="Dynamic"></asp:RegularExpressionValidator>
                                        <asp:RangeValidator ID="RangeValidator1" Text="Please enter value greater than 0. "
                                            ControlToValidate="txtRedeemTypeValue" MinimumValue="1" MaximumValue="9999999999"
                                            ValidationGroup="btnSubmit" Type="Double" CssClass="rfvPCG" Display="Dynamic"
                                            runat="server" />
                                        <asp:CompareValidator runat="server" ID="cmpMinAmountUnits" ControlToValidate="txtRedeemTypeValue"
                                            Operator="GreaterThanEqual" Type="Double" CssClass="rfvPCG" ValidationGroup="btnSubmit" /><br />
                                    </td>
                                </tr>
                                <tr class="spaceUnder" id="trDividendOption" runat="server">
                                    <td align="right" style="vertical-align: top;">
                                        <asp:Label ID="lblOption" runat="server" Text="Option:" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDividendType" runat="server" CssClass="txtField"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-md-3">
                            <asp:CheckBox ID="chkTermsCondition" runat="server" Font-Bold="True" Font-Names="Shruti"
                                Enabled="false" Checked="false" ForeColor="#145765" Text="" ToolTip="Click 'Terms & Conditions' to proceed further"
                                CausesValidation="true" />
                            <asp:LinkButton ID="lnkTermsCondition" CausesValidation="false" Text="Terms & Conditions"
                                runat="server" CssClass="fontsize" OnClick="lnkTermsCondition_Click" ToolTip="Click here to accept terms & conditions"></asp:LinkButton>
                            <span id="Span9" class="spnRequiredField">*</span>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please read terms & conditions"
                                ClientValidationFunction="ValidateTermsConditions" EnableClientScript="true"
                                OnServerValidate="TermsConditionCheckBox" Display="Dynamic" ValidationGroup="btnSubmit"
                                CssClass="rfvPCG">
                    Please read terms & conditions
                            </asp:CustomValidator>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="OnClick_Submit"
                                CssClass="btn btn-sm btn-primary" ValidationGroup="btnSubmit"></asp:Button>
                        </div>
                    </div>
                    <div class="col-md-12  col-xs-12 col-sm-12" style="visibility: hidden">
                        <div class="col-md-2" style="visibility: hidden">
                            <b class="fontsize">Dividend Type:</b>
                            <asp:DropDownList ID="ddlDivType" runat="server" CssClass="cmbField" Width="200px">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Dividend Reinvestment" Value="DVR"></asp:ListItem>
                                <asp:ListItem Text="Dividend Payout" Value="DVP"></asp:ListItem>
                            </asp:DropDownList>
                            <span id="Span5" class="spnRequiredField">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="rfvPCG"
                                ErrorMessage="Please Select an Dividend Type" Display="Dynamic" ControlToValidate="ddlDivType"
                                InitialValue="0" >
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblDividendFrequency" runat="server" Visible="false" Text="Dividend Frequency:"
                                CssClass="FieldName"></asp:Label>
                        </div>
                        <div class="col-md-5">
                            <asp:Label ID="lbldftext" runat="server" Visible="false" CssClass="FieldName"></asp:Label>
                        </div>
                    </div>
                </div>
                <telerik:RadWindow ID="rwTermsCondition" runat="server" VisibleOnPageLoad="false"
                    Width="1000px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Behaviors="Move, Resize,Close"
                    Title="Terms & Conditions" Height="180px" EnableShadow="true" Left="15%" Top="5"
                    OnClientShow="setCustomPosition">
                    <contenttemplate>
                        <div style="padding: 0px; width: 100%">
                            <table width="100%" cellpadding="0" cellpadding="0">
                                <tr>
                                    <td align="left">
                                        <%--  <a href="../ReferenceFiles/MF-Terms-Condition.html">../ReferenceFiles/MF-Terms-Condition.html</a>--%>
                                        <iframe src="../ReferenceFiles/MF-Terms-Condition.html" name="iframeTermsCondition"
                                            style="width: 100%;height:130px" ></iframe>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnAccept" runat="server" Text="Accept" CssClass="PCGButton" OnClick="btnAccept_Click"
                                          />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </contenttemplate>
                </telerik:RadWindow>
                <div style="padding-top: 5px; display: none;">
                    <table style="border-style: solid; border-width: 2px; border-color: Blue; visibility: hidden">
                        <tr class="spaceUnder">
                            <td>
                                <asp:Label ID="lblUsefulLinks" runat="server" Text="Quick Links:" CssClass="FieldName"></asp:Label>
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
                <telerik:RadWindowManager runat="server" ID="RadWindowManager1">
                    <windows>
                        <telerik:RadWindow ID="rw_customConfirm" Modal="true" Behaviors="Close, Move" VisibleStatusbar="false"
                            Width="700px" Height="160px" runat="server" Title="EUIN Confirm" Left="15%" Top="5"
                            OnClientShow="setCustomPosition">
                            <ContentTemplate>
                                <div class="rwDialogPopup radconfirm">
                                    <div class="rwDialogText">
                                        <asp:Label ID="confirmMessage" Text="" runat="server" />
                                    </div>
                                    <div>
                                        <asp:Button runat="server" ID="rbConfirm_OK" Text="OK" OnClick="rbConfirm_OK_Click"
                                            ValidationGroup="btnSubmit" OnClientClick="return PreventClicks();" ></asp:Button>
                                        <asp:Button runat="server" ID="rbConfirm_Cancel" Text="Cancel" OnClientClicked="closeCustomConfirm">
                                        </asp:Button>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </telerik:RadWindow>
                    </windows>
                </telerik:RadWindowManager>
                <telerik:RadWindow ID="RadInformation" Modal="true" Behaviors="Close, Move" VisibleStatusbar="false"
                    Width="760px" Height="580px" runat="server" Left="15%" Top="5" OnClientShow="setCustomPosition">
                    <contenttemplate>
                        <div style="padding: 0px; width: 100%; height: 100%;">
                            <%--<table width="100%" cellpadding="0" cellpadding="0" Height="100%">
                        <tr>
                            <td align="left">--%>
                            <%--  <a href="../ReferenceFiles/MF-Terms-Condition.html">../ReferenceFiles/MF-Terms-Condition.html</a>--%>
                            <iframe src="../ReferenceFiles/HelpRedeem.htm" name="iframeTermsCondition" style="width: 100%;
                                height: 100%"></iframe>
                            <%-- </td>
                        </tr>
                    </table>--%>
                        </div>
                    </contenttemplate>
                </telerik:RadWindow>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</body>
<Banner:footer ID="MyHeader" assetCategory="MF" runat="server" />

<script type="text/javascript">
    function setCustomPosition(sender, args) {
        sender.moveTo(sender.get_left(), sender.get_top());
    }
</script>

<table id="tblSIP" runat="server" visible="false">
    <tr>
        <td>
            <asp:DropDownList ID="ddlAmc" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlAmc_OnSelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Select an AMC" Display="Dynamic" ControlToValidate="ddlAmc"
                InitialValue="0">
            </asp:RequiredFieldValidator>
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span7" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Select Category"
                CssClass="rfvPCG" ControlToValidate="ddlCategory" Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlScheme_onSelectedChanged">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select a scheme"
                InitialValue="0" CssClass="rfvPCG" ControlToValidate="ddlScheme" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
