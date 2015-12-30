<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFOrderSIPTransType.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.MFOrderSIPTransType" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

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
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<style type="text/css">
    tr.spaceUnder > td
    {
        padding-bottom: .5em;
    }
    #tbpurchase
    {
        width: 103%;
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


    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(PopupEndRequestHandler);
    
</script>

<body style="background-color: #E5F6FF; margin-left: 50px; margin-right: 50px;">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%--<table class="tblMessage" cellpadding="0" cellspacing="0">
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
        </table>--%>
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
            <div style="margin-right: 50px; width: 100%" id="divControlContainer" runat="server">
                <div class="col-md-12  col-xs-12 col-sm-12">
                    <div class="col-md-3">
                        <b class="fontsize">Scheme:</b>
                        <asp:Label ID="lblScheme" runat="server" CssClass="fieldFontSize"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <b class="fontsize">Category:</b>
                        <asp:Label ID="lblCategory" runat="server" CssClass="fieldFontSize"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <b class="fontsize">Scheme Rating</b>
                        <asp:Image runat="server" ID="imgSchemeRating" />
                        <asp:Label ID="lblSchemeRatingAsOn" runat="server" CssClass="fieldFontSize"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <b class="fontsize">Folio Number:</b>
                        <asp:DropDownList OnSelectedIndexChanged="ddlFolio_SelectedIndexChanged" ID="ddlFolio"
                            CssClass="cmbField" runat="server" AutoPostBack="True">
                            <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                        </asp:DropDownList>
                        <span id="Span2" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="rfvFolio" runat="server" ErrorMessage="Please select folio number"
                            CssClass="rfvPCG" ControlToValidate="ddlFolio" Display="Dynamic" InitialValue="0"
                            ValidationGroup="btnSubmit">Please select folio number</asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-1">
                        <asp:ImageButton ID="imgInformation" runat="server" ImageUrl="../Images/help.png"
                            OnClick="imgInformation_OnClick" ToolTip="Help" Style="cursor: hand;" />
                    </div>
                </div>
                <div class="col-md-12  col-xs-12 col-sm-12" style="border: 1px solid #ccc;">
                    <div class="col-md-3">
                        <b class="fontsize">NAV(Rs):</b>
                        <asp:Label ID="lblNavDisplay" runat="server" CssClass="fieldFontSize"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <b class="fontsize">Minimum Initial Amount:</b>
                        <asp:Label ID="lblMinAmountrequiredDisplay" runat="server" CssClass="fieldFontSize"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <b class="fontsize">Subsequent Amount(In Multiples Of):</b>
                        <asp:Label ID="lblMutiplesThereAfterDisplay" runat="server" CssClass="fieldFontSize"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <b class="fontsize">Cut-Off time:</b>
                        <asp:Label ID="lblCutOffTimeDisplay" runat="server" CssClass="fieldFontSize"></asp:Label>
                    </div>
                </div>
                <div class="col-md-12  col-xs-12 col-sm-12" style="border: 1px solid #ccc;">
                    <div class="col-md-3">
                        <b class="fontsize">Joint Holder:</b>
                        <asp:Label ID="lblHolderDisplay" runat="server" CssClass="fieldFontSize"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <b class="fontsize">Nominee:</b>
                        <asp:Label ID="lblNomineeDisplay" runat="server" CssClass="fieldFontSize"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <b class="fontsize">Unit Held:</b>
                        <asp:Label ID="lblUnitHeldDisplay" runat="server" CssClass="fieldFontSize"></asp:Label>
                    </div>
                </div>
                <div class="col-md-12  col-xs-12 col-sm-12">
                    <div class="col-md-3">
                        <b class="fontsize">SIP Frequency:</b>
                        <asp:DropDownList ID="ddlFrequency" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlFrequency_OnSelectedIndexChanged"
                            AutoPostBack="True">
                            <asp:ListItem Text="--SELECT--" Value="0" Selected="True"></asp:ListItem>
                            <%--  <asp:ListItem Text="Quarterly" Value="QT"></asp:ListItem>--%>
                        </asp:DropDownList>
                        <span id="Span3" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="rfvFrequency" runat="server" ErrorMessage="Please select frequency"
                            CssClass="rfvPCG" ControlToValidate="ddlFrequency" Display="Dynamic" InitialValue="0"
                            ValidationGroup="btnSubmit">Please select frequency</asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-3">
                        <b class="fontsize">Start Date:</b>
                        <asp:DropDownList ID="ddlStartDate" CssClass="cmbField" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlStartDate_SelectedIndexChanged" ValidationGroup="btnSubmit">
                            <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                        </asp:DropDownList>
                        <span id="Span4" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ErrorMessage="Please select a start date"
                            InitialValue="0" ControlToValidate="ddlStartDate" ValidationGroup="btnSubmit"
                            CssClass="rfvPCG" Display="Dynamic">Please select a start date</asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-3">
                        <b class="fontsize">Total Installments:</b>
                        <asp:DropDownList ID="ddlTotalInstallments" CssClass="cmbField" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlTotalInstallments_SelectedIndexChanged" Width="90px">
                            <asp:ListItem Value="0">SELECT</asp:ListItem>
                        </asp:DropDownList>
                        <span id="Span5" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="rfvInstallments" runat="server" ErrorMessage="Please select a value"
                            ControlToValidate="ddlTotalInstallments" InitialValue="0" ValidationGroup="btnSubmit"
                            CssClass="rfvPCG" Display="Dynamic">Please select a value</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-12  col-xs-12 col-sm-12" style="margin-top: .5%">
                    <div class="col-md-3" runat="server" id="DivText" visible="false">
                        <b class="fontsize">Dividend Option:</b>
                        <%--</div>
                    <div class="col-md-2" runat="server" id="DivDropdown" visible="false">--%>
                        <asp:DropDownList ID="ddlDividendFreq" CssClass="cmbField" runat="server">
                        </asp:DropDownList>
                        <%-- <span id="Span10" class="spnRequiredField">*</span>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select Dividend Option"
                            ControlToValidate="ddlDividendFreq" InitialValue="0" ValidationGroup="btnSubmit"
                            CssClass="rfvPCG" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-3">
                        <b class="fontsize">End Date:</b>
                        <asp:Label ID="lblEndDateDisplay" runat="server" CssClass="fieldFontSize"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <b class="fontsize">Amount:</b>
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField"></asp:TextBox>
                        <span id="Span6" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ErrorMessage="Please enter a valid amount (e.g. 500.0,  100.15)"
                            ValidationGroup="btnSubmit" ControlToValidate="txtAmount" Display="Dynamic" CssClass="rfvPCG">Please enter a valid amount</asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="rgvAmount" runat="server" ControlToValidate="txtAmount" ErrorMessage="You have entered the amount less than the Minimum Initial Amount"
                            Type="Double" ValidationGroup="btnSubmit" CssClass="rfvPCG" Display="Dynamic"> You should enter the amount in multiple of subsequent amount</asp:RangeValidator>
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
                        <asp:Button ValidationGroup="btnSubmit" ID="btnSubmit" runat="server" CssClass="btn btn-sm btn-primary"
                            OnClick="btnSubmit_Click"></asp:Button>
                    </div>
                </div>
            </div>
            <div style="float: left; padding-top: 5px; display: none;">
                <div class="col-md-1">
                    <asp:TextBox Style="display: none;" ID="txtMinAmtDisplay" CssClass="txtField" Enabled="false"
                        runat="server"></asp:TextBox>
                </div>
                <table style="border-style: solid; border-width: 2px; border-color: Blue">
                    <tr class="spaceUnder">
                        <td>
                            <asp:Label ID="lblUsefulLinks" runat="server" Text="Quick Links:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblDividendType" runat="server"></asp:Label>
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
            </div>
            <telerik:RadWindow ID="rwTermsCondition" runat="server" VisibleOnPageLoad="false"
                Width="1000px" Height="150px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false"
                Behaviors="Move, Resize,Close" Title="Terms & Conditions" EnableShadow="true"
                Left="15%" Top="1" OnClientShow="setCustomPosition">
                <contenttemplate>
                    <div style="padding: 0px; width: 100%">
                        <table width="100%" cellpadding="0" cellpadding="0">
                            <tr>
                                <td align="left">
                                    <%--  <a href="../ReferenceFiles/MF-Terms-Condition.html">../ReferenceFiles/MF-Terms-Condition.html</a>--%>
                                    <iframe src="../ReferenceFiles/MF-Terms-Condition.html" name="iframeTermsCondition"
                                        style="width: 100%; height: 90px"></iframe>
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
                </contenttemplate>
            </telerik:RadWindow>
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
                                        ValidationGroup="btnSubmit" OnClientClick="return PreventClicks();"></asp:Button>
                                    <asp:Button runat="server" ID="rbConfirm_Cancel" Text="Cancel" OnClientClicked="closeCustomConfirm">
                                    </asp:Button>
                                </div>
                            </div>
                        </ContentTemplate>
                    </telerik:RadWindow>
                </windows>
            </telerik:RadWindowManager>
            <telrik:radwindowmanager>
       
        <telerik:RadWindow ID="RadInformation" Modal="true" Behaviors="Close, Move"  VisibleOnPageLoad="false"
            Width="760px" Height="1600px" runat="server" Left="300px" Top="50px" OnClientShow="setCustomPosition" >
            <ContentTemplate>
                <div style="padding: 0px; width: 130%; height:100%;">
                    <%--<table width="100%" cellpadding="0" cellpadding="0" Height="100%">
                        <tr>
                            <td align="left">--%>
                                <%--  <a href="../ReferenceFiles/MF-Terms-Condition.html">../ReferenceFiles/MF-Terms-Condition.html</a>--%>
                                
                                <iframe src="../ReferenceFiles/HelpSIP.htm" name="iframeTermsCondition"
                                    style="width: 100%; height:100%">
                                    </iframe>
                           <%-- </td>
                        </tr>
                    </table>--%>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
        </Windows>
        </telrik:radwindowmanager>
            <div style="float: inherit;">
                <asp:HiddenField ID="hdnAccountId" runat="server" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlFrequency" />
            <asp:PostBackTrigger ControlID="ddlFolio" />
            <asp:PostBackTrigger ControlID="ddlTotalInstallments" />
            <asp:PostBackTrigger ControlID="ddlStartDate" />
            <asp:PostBackTrigger ControlID="lnkTermsCondition" />
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

<table border="1" cellpadding="1" cellspacing="2" style="border-collapse: collapse;">
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
    <tr id="trNewOrder" runat="server" visible="false">
        <td align="center" colspan="4">
            <asp:LinkButton ID="lnkNewOrder" CausesValidation="false" Text="Make another SIP"
                runat="server" OnClick="lnkNewOrder_Click" CssClass="LinkButtons"></asp:LinkButton>
            <asp:DropDownList ID="ddlAmc" runat="server" CssClass="cmbExtraLongField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlAmc_OnSelectedIndexChanged" Visible="false">
                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
            </asp:DropDownList>
            <span id="Span7" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvAmc" runat="server" CssClass="rfvPCG" ErrorMessage="Please Select an AMC"
                Display="Dynamic" ControlToValidate="ddlAmc" InitialValue="0" ValidationGroup="btnSubmit">Please Select an AMC</asp:RequiredFieldValidator>
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged" Visible="false">
                <asp:ListItem Text="ALL" Value="ALL"></asp:ListItem>
                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
            </asp:DropDownList>
            <span id="Span8" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ErrorMessage="Please select a category"
                CssClass="rfvPCG" ControlToValidate="ddlCategory" ValidationGroup="btnSubmit"
                Display="Dynamic" InitialValue="0">Please select a category</asp:RequiredFieldValidator>
            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbExtraLongField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" Visible="false">
                <asp:ListItem Value="0">--SELECT--</asp:ListItem>
            </asp:DropDownList>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ErrorMessage="Please select a scheme"
                CssClass="rfvPCG" ControlToValidate="ddlScheme" Display="Dynamic" InitialValue="0"
                ValidationGroup="btnSubmit">Please select a scheme</asp:RequiredFieldValidator>
            <asp:Label ID="lblAmc" runat="server" CssClass="fieldFontSize" Visible="false"></asp:Label>
        </td>
    </tr>
</table>
