<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFOrderNFOTransType.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.MFOrderNFOTransType" %>
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
                <td align="center">
                    <div id="divMessage" align="center">
                    </div>
                    <div style="clear: both">
                    </div>
                </td>
            </tr>
        </table>
        <div style="float: left; width: 100%" id="divControlContainer" runat="server">
            <div class="col-md-12  col-xs-12 col-sm-12">
                <div style="float: right">
                    <asp:ImageButton ID="imgInformation" runat="server" ImageUrl="../Images/help.png"
                        OnClick="imgInformation_OnClick" ToolTip="Help" Style="cursor: hand;" />
                </div>
                <div class="col-md-3">
                    NFO Scheme:
                    <asp:Label ID="lblScheme" runat="server" CssClass="readOnlyField"></asp:Label>
                </div>
                <div class="col-md-3">
                    AMC:
                    <asp:Label ID="lblNFOSchemeAMC" runat="server"  CssClass="readOnlyField"></asp:Label>
                </div>
                <div class="col-md-3">
                    Category:
                    <asp:Label ID="lblSchemeCategory" runat="server" CssClass="readOnlyField"></asp:Label>
                </div>
            </div>
            <div class="col-md-12  col-xs-12 col-sm-12">
                <div class="col-md-3">
                    NFO Start Date:
                    <asp:Label ID="NFOStartDate" runat="server" CssClass="readOnlyField"></asp:Label>
                </div>
                <div class="col-md-3">
                    NFO End Date:
                    <asp:Label ID="NFOEndDate" runat="server" CssClass="readOnlyField"></asp:Label>
                </div>
                <div class="col-md-3">
                    Minimum Initial Amount
                    <asp:Label ID="lblMintxt" runat="server" CssClass="readOnlyField"></asp:Label>
                </div>
                <div class="col-md-3">
                    Subsequent Amount(In Multiples Of)
                    <asp:Label ID="lblMulti" runat="server" CssClass="readOnlyField"></asp:Label>
                </div>
                <div class="col-md-3">
                    Cut-Off time:
                    <asp:Label ID="lbltime" runat="server" Text="" CssClass="readOnlyField"></asp:Label>
                </div>
            </div>
            <div class="col-md-12  col-xs-12 col-sm-12">
                <div class="col-md-3">
                    Joint Holder:
                    <asp:Label ID="lblHolderDisplay" runat="server" CssClass="readOnlyField"></asp:Label>
                </div>
                <div class="col-md-3">
                    Nominee:
                    <asp:Label ID="lblNomineeDisplay" runat="server" CssClass="readOnlyField"></asp:Label>
                </div>
            </div>
            <div class="col-md-12  col-xs-12 col-sm-12">
                <div class="col-md-3">
                    Folio Number:
                    <asp:DropDownList ID="ddlFolio" CssClass="form-control input-sm" runat="server" AutoPostBack="false">
                    </asp:DropDownList>
                    <span id="Span1" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select a Folio"
                        InitialValue="0" CssClass="rfvPCG" ControlToValidate="ddlFolio" ValidationGroup="btnSubmit"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    Amount:
                    <asp:TextBox ID="txtAmt" runat="server" CssClass="txtField" MaxLength="11"></asp:TextBox>
                    <span id="Span3" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select a Amount"
                        CssClass="rfvPCG" ControlToValidate="txtAmt" ValidationGroup="btnSubmit" Display="Dynamic"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAmt"
                        ErrorMessage="Please Enter Only Numbers and 2 digits after decimal " CssClass="rfvPCG"
                        ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$" ValidationGroup="btnSubmit" Display="Dynamic"></asp:RegularExpressionValidator>
                </div>
                <div class="col-md-3" id="divDividentType" runat="server" visible="false">
                    Dividend Type
                    <asp:DropDownList ID="ddlDivType" runat="server" CssClass="form-control input-sm"
                        Style="width: 250px;">
                    </asp:DropDownList>
                    <span id="Span4" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Select an Dividend Type" Display="Dynamic" ControlToValidate="ddlDivType"
                        InitialValue="0" ValidationGroup="btnSubmit">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:CheckBox ID="chkTermsCondition" runat="server" Font-Bold="True" Font-Names="Shruti"
                        Enabled="false" Checked="false" ForeColor="#145765" Text="" ToolTip="Click 'Terms & Conditions' to proceed further"
                        CausesValidation="true" />
                    <asp:LinkButton ID="lnkTermsCondition" CausesValidation="false" Text="Terms & Conditions"
                        runat="server" CssClass="txtField" OnClick="lnkTermsCondition_Click" ToolTip="Click here to accept terms & conditions"></asp:LinkButton>
                    <span id="Span9" class="spnRequiredField">*</span>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please read terms & conditions"
                        ClientValidationFunction="ValidateTermsConditions" EnableClientScript="true"
                        OnServerValidate="TermsConditionCheckBox" Display="Dynamic" ValidationGroup="btnSubmit"
                        CssClass="rfvPCG">
                    Please read terms & conditions
                    </asp:CustomValidator>
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="OnClick_Submit"
                        CssClass="btn btn-sm btn-primary" ValidationGroup="btnSubmit"></asp:Button>
                </div>
            </div>
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
            Width="1000px" Height="140px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Behaviors="Move,
        Resize,Close" Title="Terms & Conditions" EnableShadow="true" Left="15%" Top="5"
            OnClientShow="setCustomPosition">
            <ContentTemplate>
                <div style="padding: 0px; width: 100%">
                    <table width="100%" cellpadding="0" cellpadding="0">
                        <tr>
                            <td align="left">
                                <iframe src="../ReferenceFiles/MF-Terms-Condition.html" name="iframeTermsCondition"
                                    style="width: 100%;height:130px"></iframe>
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
        <telerik:RadWindow ID="RadInformation" Modal="true" Behaviors="Close, Move" VisibleStatusbar="false"
            Width="760px" Height="135px" runat="server" Left="300" Top="5" OnClientShow="setCustomPosition">
            <ContentTemplate>
                <div style="padding: 0px; width: 100%; height: 100%;">
                    <iframe src="../ReferenceFiles/HelpNFO.htm" name="iframeTermsCondition" style="width: 100%;
                        height: 100%"></iframe>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
        <telerik:RadWindowManager runat="server" ID="RadWindowManager1">
            <Windows>
                <telerik:RadWindow ID="rw_customConfirm" Modal="true" Behaviors="Close, Move" VisibleStatusbar="false"
                    Width="700px" Height="140px" runat="server" Title="EUIN Confirm" Left="15%" Top="5"
                    OnClientShow="setCustomPosition">
                    <ContentTemplate>
                        <div class="rwDialogPopup radconfirm">
                            <div class="rwDialogText">
                                <asp:Label ID="confirmMessage" Text="" runat="server" />
                            </div>
                            <div>
                                <asp:Button runat="server" ID="rbConfirm_OK" Text="OK" OnClick="rbConfirm_OK_Click"
                                    OnClientClick="return  PreventClicks();" ValidationGroup="btnSubmit"></asp:Button>
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

<table>
    <tr class="spaceUnder" id="trDivFre" visible="false" runat="server">
        <td>
            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbExtraLongField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlScheme_onSelectedChanged" Visible="false">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select a scheme"
                InitialValue="0" CssClass="rfvPCG" ControlToValidate="ddlScheme" ValidationGroup="btnSubmit"
                Display="Dynamic"></asp:RequiredFieldValidator>
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
    <tr id="trNewOrder" runat="server" visible="false">
        <td align="center" colspan="4">
            <asp:LinkButton ID="lnkNewOrder" CausesValidation="false" Text="Make another NFO Purchase"
                runat="server" OnClick="lnkNewOrder_Click" CssClass="LinkButtons"></asp:LinkButton>
        </td>
    </tr>
</table>
