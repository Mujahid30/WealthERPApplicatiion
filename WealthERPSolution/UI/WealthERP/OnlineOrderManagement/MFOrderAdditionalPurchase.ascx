<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFOrderAdditionalPurchase.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.MFOrderAdditionalPurchase" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>


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

<script type="text/jscript">
  

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(PopupEndRequestHandler);
    
</script>

<style>
    tr.spaceUnder > td
    {
        padding-bottom: .5em;
    }
</style>
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
        <div style="float: left; padding-top: 5px; width: 100%" id="divControlContainer"
            runat="server">
            <table id="tbpurchase" width="100%">
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
                            ValidationGroup="btnSubmit">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td colspan="2">
                    <asp:ImageButton ID="imgInformation" runat="server" ImageUrl="../Images/help.png"
                            OnClick="imgInformation_OnClick" ToolTip="Help" Style="cursor: hand;" />
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
                    <td colspan="2">
                    </td>
                    <td align="left" style="vertical-align: top;" colspan="3">
                        <table width="75%" class="SchemeInfoTable">
                            <tr class="SchemeInfoTable">
                                <td align="left" style="vertical-align: top;">
                                    <asp:Label ID="lblNav" runat="server" Text=" Last Recorded NAV (Rs):" CssClass="FieldName"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblNavDisplay" runat="server" CssClass="readOnlyField"></asp:Label>
                                </td>
                                <td align="left" style="vertical-align: top;">
                                    <asp:Label ID="lblMin" runat="server" Text="Minimum Initial Amount: " CssClass="FieldName"></asp:Label>
                                </td>
                                <td style="vertical-align: top;">
                                    <asp:Label ID="lblMintxt" runat="server" CssClass="readOnlyField"></asp:Label>
                                </td>
                                <td rowspan="4">
                                    <a href="#" class="popper" data-popbox="divSchemeRatingDetails"><span class="FieldName">
                                        Scheme Rating</span>
                                        <asp:Label ID="lblSchemeRatingAsOn" runat="server" CssClass="FieldName"></asp:Label>
                                        <br />
                                        <asp:Image runat="server" ID="imgSchemeRating" />
                                    </a>
                                </td>
                            </tr>
                            <tr class="SchemeInfoTable">
                                <td align="left" style="vertical-align: top;">
                                    <asp:Label ID="lblCutt" runat="server" Text="Cut-Off time:" CssClass="FieldName"></asp:Label>
                                </td>
                                <td style="vertical-align: top;">
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
                    </td>
                </tr>
                <tr class="spaceUnder">
                    <td>
                    </td>
                    <td align="right" align="right" style="vertical-align: top;">
                        <asp:Label ID="lblFolioNumber" runat="server" Text="Folio Number:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFolio" CssClass="cmbField" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                        <span id="Span5" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Select a Folio"
                            CssClass="rfvPCG" ControlToValidate="ddlFolio" ValidationGroup="btnSubmit" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
                <tr class="spaceUnder" id="trNomineeJoint" runat="server">
                    <td colspan="2">
                    </td>
                    <td align="left" style="vertical-align: top;" colspan="3">
                        <table width="75%" class="SchemeInfoTable" id="tblNomineeJoint" runat="server">
                            <tr id="trJointHolder" runat="server" class="spaceUnder">
                                <td align="left" style="vertical-align: top;">
                                    <asp:Label ID="lblHolder" runat="server" Text="Joint Holder:" CssClass="FieldName"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblHolderDisplay" runat="server" CssClass="readOnlyField"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trNominee" runat="server" class="spaceUnder">
                                <td align="left" style="vertical-align: top;">
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
                    <td align="right" style="vertical-align: top;">
                        <asp:Label ID="lblUnitsheld" runat="server" Text="Units Held:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblUnitsheldDisplay" runat="server" CssClass="readOnlyField"></asp:Label>
                    </td>
                </tr>
                <tr class="spaceUnder">
                    <td>
                    </td>
                    <td align="right" style="vertical-align: top;">
                        <asp:Label ID="lblAmt" runat="server" Text="Amount:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAmt" runat="server" CssClass="txtField" MaxLength="8"></asp:TextBox>
                        <span id="Span3" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select a Amount"
                            CssClass="rfvPCG" ControlToValidate="txtAmt" ValidationGroup="btnSubmit" Display="Dynamic"></asp:RequiredFieldValidator>
                        </br>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAmt"
                            ErrorMessage="Please Enter Only Numbers and 2 digits after decimal" CssClass="rfvPCG"
                            ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$" ValidationGroup="btnSubmit" Display="Dynamic"></asp:RegularExpressionValidator>
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
                <tr class="spaceUnder" id="trDivtype" runat="server" visible="false">
                    <td>
                    </td>
                    <td align="right" style="vertical-align: top;">
                        <asp:Label ID="lblDivType" runat="server" Text="Dividend Type:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDivType" runat="server" Width="300px" CssClass="cmbField">
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
                <tr class="spaceUnder" id="trDivfeq" runat="server" visible="false">
                    <td>
                    </td>
                    <td align="right" style="vertical-align: top;">
                        <asp:Label ID="lblDividendFrequency" Visible="false" runat="server" Text="Dividend Frequency:"
                            CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbldftext" Visible="false" runat="server" CssClass="readOnlyField"></asp:Label>
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
                <tr class="spaceUnder" id="trDividendType" runat="server" visible="false">
                    <td>
                    </td>
                    <td align="right" style="vertical-align: top;">
                        <asp:Label ID="lblOption" runat="server" Text="Option:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDividendType" runat="server" CssClass="readOnlyField"></asp:Label>
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
                    <td colspan="2">
                    </td>
                    <td colspan="2" align="left">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="OnClick_Submit"
                            CssClass="PCGButton" ValidationGroup="btnSubmit"></asp:Button>
                    </td>
                    <%-- <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="ResetControlDetails()"
                CssClass="FieldName"></asp:Button>
            <td>
            </td>--%>
                </tr>
                <tr id="trNewOrder" runat="server" visible="false">
                    <td align="center" colspan="4">
                        <asp:LinkButton ID="lnkNewOrder" CausesValidation="false" Text="Make another Additional Purchase"
                            runat="server" OnClick="lnkNewOrder_Click" CssClass="LinkButtons"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
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
            Title="Terms & Conditions" EnableShadow="true" Left="15%" Top="100" OnClientShow="setCustomPosition">
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
        </telerik:RadWindowManager>
        <telerik:RadWindow ID="RadInformation" Modal="true" Behaviors="Close, Move" VisibleStatusbar="false"
            Width="760px" Height="580px" runat="server" Left="300" Top="50" OnClientShow="setCustomPosition" >
            <ContentTemplate>
                <div style="padding: 0px; width: 100%; height:100%;">
                    <%--<table width="100%" cellpadding="0" cellpadding="0" Height="100%">
                        <tr>
                            <td align="left">--%>
                                <%--  <a href="../ReferenceFiles/MF-Terms-Condition.html">../ReferenceFiles/MF-Terms-Condition.html</a>--%>
                                <iframe src="../ReferenceFiles/HelpAdditional Purchase.htm" name="iframeTermsCondition"
                                style="width: 100%; height:100%"></iframe>
                                            
                           <%-- </td>
                        </tr>
                    </table>--%>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
        
        <telerik:RadWindowManager runat="server" ID="RadWindowManager1">
            <Windows>
                <telerik:RadWindow ID="rw_customConfirm" Modal="true" Behaviors="Close, Move" VisibleStatusbar="false"
                    Width="700px" Height="160px" runat="server" Title="EUIN Confirm" Left="15%" Top="100" OnClientShow="setCustomPosition">
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
    </ContentTemplate>
    <Triggers>
    </Triggers>
</asp:UpdatePanel>
 <Banner:footer ID="MyHeader" assetCategory="MF" runat="server" />
 
 <script type="text/javascript">
    function setCustomPosition(sender, args) {
        sender.moveTo(sender.get_left(), sender.get_top());
    }
</script>
