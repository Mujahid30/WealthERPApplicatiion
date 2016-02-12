<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFOrderPurchaseTransType.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.MFOrderPurchaseTransactionType" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.bxslider.js" type="text/javascript"></script>

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

<script type="text/javascript">
    function Information() {

        resizey = $find("<%=RadInformation.ClientID %>").show();
        resizey.returnValue = false;
        return true;

    }
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
<body style="background-color: #E5F6FF;">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
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
                <div id="divControlContainer" runat="server" style="margin-left: 50px; margin-right: 50px;
                    width: 100%">
                    <table width="90%" style="margin-right: 50px;">
                        <tr>
                          <td style="margin-left: 1px" colspan="2">
                                <b class="fontsize">Scheme:</b>
                                <asp:Label ID="lblScheme" runat="server" CssClass="fieldFontSize"></asp:Label>
                            </td>
                            <td style="margin-left: 1px">
                                <b class="fontsize">Category:</b>
                                <asp:Label ID="lblCategory" runat="server" CssClass="fieldFontSize"></asp:Label>
                            </td>
                             <td style="margin-left: 5px">
                                <b class="fontsize">Scheme Rating:</b>
                                <asp:Image runat="server" ID="imgSchemeRating" />
                                <asp:Label ID="lblSchemeRatingAsOn" runat="server" CssClass="fieldFontSize"></asp:Label>
                            </td>
                            <td style="margin-left: 5px" id="tdFolio" runat="server" visible="false">
                                <b class="fontsize">Folio Number:</b>
                                <asp:DropDownList ID="ddlFolio" CssClass="cmbField" OnSelectedIndexChanged="ddlFolio_OnSelectedIndexChanged"
                                    runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblDemate" runat="server" CssClass="fieldFontSize" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgInformation" runat="server" ImageUrl="../Images/help.png"
                                    OnClick="imgInformation_OnClick" ToolTip="Help" Style="cursor: hand; float: right" />
                            </td>
                        </tr>
                        <tr style="border: 1px solid #ccc;">
                            <td>
                                <b class="fontsize">NAV (Rs):</b>
                                <asp:Label ID="lblNavDisplay" runat="server" CssClass="fieldFontSize"></asp:Label>
                            </td>
                            <td style="margin-left: 5px">
                                <b class="fontsize">Minimum Initial Amount:</b>
                                <asp:Label ID="lblMintxt" runat="server" CssClass="fieldFontSize"></asp:Label>
                            </td>
                            <td style="margin-left: 5px">
                                <b class="fontsize">Subsequent Amount(In Multiples Of):</b>
                                <asp:Label ID="lblMulti" runat="server" CssClass="fieldFontSize"></asp:Label>
                            </td>
                            <td>
                                <b class="fontsize">Cut-Off time:</b>
                                <asp:Label ID="lbltime" runat="server" Text="" CssClass="fieldFontSize"></asp:Label>
                            </td>
                        </tr>
                        <tr style="border: 1px solid #ccc;">
                            <td>
                                <b class="fontsize">Joint Holder:</b>
                                <asp:Label ID="lblHolderDisplay" runat="server" CssClass="fieldFontSize"></asp:Label>
                            </td>
                            <td style="margin-left: 1px">
                                <b class="fontsize">Nominee:</b>
                                <asp:Label ID="lblNomineeDisplay" runat="server" CssClass="fieldFontSize"></asp:Label>
                            </td>
                            <td style="margin-left: 5px">
                                <b class="fontsize">Units Held:</b>
                                <asp:Label ID="lblUnitsheldDisplay" runat="server"></asp:Label>
                            </td>
                           
                        </tr>
                        <tr>
                            <td>
                                <b class="fontsize">Amount:</b>
                                <asp:TextBox ID="txtAmt" runat="server" CssClass="txtField" MaxLength="11"></asp:TextBox>
                                <span id="Span3" class="spnRequiredField">*</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select a Amount"
                                    CssClass="rfvPCG" ControlToValidate="txtAmt" ValidationGroup="btnSubmit" Display="Dynamic"></asp:RequiredFieldValidator>
                                <br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAmt"
                                    ErrorMessage="Please Enter Only Numbers and 2 digits after decimal " CssClass="rfvPCG"
                                    ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$" ValidationGroup="btnSubmit" Display="Dynamic"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <div id="divDVR" runat="server" visible="false">
                                    <b class="fontsize">Dividend Type:</b>
                                    <asp:DropDownList ID="ddlDivType" runat="server" CssClass="cmbField" Style="width: 200px;">
                                    </asp:DropDownList>
                                    <%--  <span id="Span4" class="spnRequiredField">*</span>--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="rfvPCG"
                                        ErrorMessage="Please Select an Dividend Type" Display="Dynamic" ControlToValidate="ddlDivType"
                                        InitialValue="0" ValidationGroup="btnSubmit">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:CheckBox ID="chkTermsCondition" runat="server" Font-Bold="True" Font-Names="Shruti"
                                    Enabled="false" Checked="false" ForeColor="#145765" Text="" ToolTip="Click 'Terms & Conditions' to proceed further"
                                    CausesValidation="true" />
                                <asp:LinkButton ID="lnkTermsCondition" CausesValidation="false" Text="Terms & Conditions"
                                    runat="server" CssClass="fontsize" OnClick="lnkTermsCondition_Click" ToolTip="Click here to accept terms & conditions"></asp:LinkButton>
                                <span id="Span9" class="spnRequiredField">*</span>
                                <br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please read terms & conditions"
                                    ClientValidationFunction="ValidateTermsConditions" EnableClientScript="true"
                                    OnServerValidate="TermsConditionCheckBox" Display="Dynamic" ValidationGroup="btnSubmit"
                                    CssClass="rfvPCG">
                     Please read terms & conditions
                                </asp:CustomValidator>
                            </td>
                            <td>
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="OnClick_Submit"
                                    class="btn btn-sm btn-primary" ValidationGroup="btnSubmit"></asp:Button>
                            </td>
                        </tr>
                    </table>
                    <%-- <td >
             <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="ResetControlDetails"
                    CssClass="FieldName"></asp:Button>
            </td>--%>
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
                    Width="1000px" Height="200px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false"
                    Behaviors="Move, Resize,Close" Title="Terms & Conditions" EnableShadow="true"
                    Left="15%" Top="1" OnClientShow="setCustomPosition">
                    <contenttemplate>
                       <div style="padding: 0px; width: 100%">
                          <table width="100%" cellpadding="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <%--  <a href="../ReferenceFiles/MF-Terms-Condition.html">../ReferenceFiles/MF-Terms-Condition.html</a>--%>
                                        <iframe src="../ReferenceFiles/MF-Terms-Condition.html" name="iframeTermsCondition"
                                            style="width: 100%; height: 115px;"></iframe>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnAccept" runat="server" Text="Accept" class="btn btn-sm btn-primary"
                                            OnClick="btnAccept_Click" CausesValidation="false" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </contenttemplate>
                </telerik:RadWindow>
                <telerik:RadWindowManager runat="server" ID="RadWindowManager1">
                    <windows>
                        <telerik:RadWindow ID="rw_customConfirm" Modal="true" Behaviors="Close, Move" VisibleStatusbar="false"
                            Width="700px" Height="160px" runat="server" Title="EUIN Confirm" Left="300" Top="5"
                            OnClientShow="setCustomPosition">
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
                    </windows>
                </telerik:RadWindowManager>
                <telerik:RadWindow ID="RadInformation" Modal="true" Behaviors="Close, Move" VisibleStatusbar="false"
                    Width="760px" Height="180px" runat="server" Left="300" Top="5" OnClientShow="setCustomPosition">
                    <contenttemplate>
                        <div style="padding: 0px; width: 100%; height: 100%;">
                            <%--<table width="100%" cellpadding="0" cellpadding="0" Height="100%">
                        <tr>
                            <td align="left">--%>
                            <%--  <a href="../ReferenceFiles/MF-Terms-Condition.html">../ReferenceFiles/MF-Terms-Condition.html</a>--%>
                            <iframe src="../ReferenceFiles/HelpNewPurchaseInformation.htm" name="iframeTermsCondition"
                                style="width: 100%; height: 100%"></iframe>
                            <%-- </td>
                        </tr>
                    </table>--%>
                        </div>
                    </contenttemplate>
                </telerik:RadWindow>
            </div>
        </ContentTemplate>
        <Triggers>
       
        </Triggers>
    </asp:UpdatePanel>
</body>
<Banner:footer ID="MyHeader" assetCategory="MF" runat="server" />

<script type="text/javascript">
    function setCustomPosition(sender, args) {
        sender.moveTo(sender.get_left(), sender.get_top());
    }
</script>

<table>
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
    <tr id="trNewOrder" runat="server" visible="false">
        <td align="center" colspan="4">
            <asp:LinkButton ID="lnkNewOrder" CausesValidation="false" Text="Make another NewPurchase"
                runat="server" OnClick="lnkNewOrder_Click" CssClass="LinkButtons"></asp:LinkButton>
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
        <td>
            <b class="fontsize">AMC:</b><asp:Label ID="lblAmc" runat="server" CssClass="fieldFontSize"></asp:Label>
        </td>
    </tr>
</table>
