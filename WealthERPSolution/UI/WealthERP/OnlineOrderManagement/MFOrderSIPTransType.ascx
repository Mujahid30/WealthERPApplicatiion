<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFOrderSIPTransType.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.MFOrderSIPTransType" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<style>
    tr.spaceUnder > td
    {
        padding-bottom: .5em;
    }
</style>

<script type="text/javascript">
     function DeleteConfirmation() {
        var bool = window.confirm('Are you sure you want to delete this Question?');

        if (bool) {
            document.getElementById("ctrl_RiskScore_hdnDeletemsgValue").value = 1;
            document.getElementById("ctrl_RiskScore_hiddenDeleteQuestion").click();

            return false;
        }
        else {
            return false;
        }
</script>

<table id="tblMessage" width="100%" runat="server" visible="false">
    <tr id="trSumbitSuccess">
        <td align="center">
            <div id="msgRecordStatus" class="success-msg" align="center" runat="server">
            </div>
        </td>
    </tr>
</table>

<table width="100%">
    <tr align="center">
        <td align="center">
            <div id="divValidationError" runat="server" class="failure-msg" align="center" visible="true">
                <asp:ValidationSummary ID="vsSummary" runat="server" Visible="true" ValidationGroup="btnSubmit" />
            </div>
        </td>
    </tr>
</table>
<div style="float: left;">
    <table>
        <tr class="spaceUnder">
            <td>
            </td>
            <td align="right" style="vertical-align: top;">
                <asp:Label ID="lblAmc" runat="server" Text="AMC:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlAmc" runat="server" CssClass="cmbField" AutoPostBack="true"
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
                <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField" AutoPostBack="true" Width="300px"
                    OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                    <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                </asp:DropDownList>
                 <span id="Span1" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ErrorMessage="Please select a scheme"
                    CssClass="rfvPCG" ControlToValidate="ddlScheme" Display="Dynamic" InitialValue="0"
                    ValidationGroup="btnSubmit">Please select a scheme</asp:RequiredFieldValidator>
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
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr id="trNominee" runat="server" class="spaceUnder">
            <td>
            </td>
            <td align="right">
                <asp:Label ID="lblHolder" runat="server" Text="Holder:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblHolderDisplay" runat="server" CssClass="FieldName"></asp:Label>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr id="trJointHolder" runat="server" class="spaceUnder">
            <td>
            </td>
            <td align="right">
                <asp:Label ID="lblNominee" runat="server" Text="Nominee:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNomineeDisplay" runat="server" CssClass="FieldName"></asp:Label>
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
                <asp:Label ID="lblNav" runat="server" Text="Last Recorded NAV(Rs):" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNavDisplay" runat="server" CssClass="FieldName"></asp:Label>
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
                <asp:Label ID="lblCutOffTime" runat="server" Text="Cut-off time:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCutOffTimeDisplay" runat="server" CssClass="FieldName"></asp:Label>
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
                <asp:Label ID="lblUnitHeld" runat="server" Text="Unit Held:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblUnitHeldDisplay" runat="server" CssClass="FieldName"></asp:Label>
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
                <asp:Label ID="lblEndDateDisplay" runat="server" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblMinAmountrequired" runat="server" Text="Minimum Initial Amount:"
                    CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblMinAmountrequiredDisplay" runat="server" CssClass="FieldName"></asp:Label>
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
                <asp:RangeValidator ID="rgvAmount" runat="server" ControlToValidate="txtAmount" ErrorMessage="You should enter the amount in Multiple of Subsequent Amount (e.g. 500.0,  100.15)"
                    Type="Double" ValidationGroup="btnSubmit" CssClass="rfvPCG" Display="Dynamic"> You should enter the amount in Multiple of Subsequent Amount</asp:RangeValidator>
            </td>
            <td style="vertical-align: top;" align="right">
                <asp:Label ID="lblMutiplesThereAfter" runat="server" CssClass="FieldName" Text="Subsequent Amount:"></asp:Label>
            </td>
            <td style="vertical-align: top;">
                <asp:TextBox Style="display: none;" ID="txtMinAmtDisplay" CssClass="txtField" Enabled="false"
                    runat="server"></asp:TextBox>
                <asp:Label ID="lblMutiplesThereAfterDisplay" runat="server" CssClass="FieldName"></asp:Label>
            </td>
        </tr>
        <tr style="display: none;" id="trDividendType" runat="server" class="spaceUnder">
            <td>
            </td>
            <td align="right">
                <asp:Label ID="lblDividendType" runat="server" Text="DividendType:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDividendTypeDisplay" runat="server" CssClass="FieldName"></asp:Label>
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
                <asp:DropDownList ID="ddlDividendFreq" CssClass="cmbField" runat="server" AutoPostBack="True">
                    <asp:ListItem Selected="True">--SELECT--</asp:ListItem>
                    <asp:ListItem Text="Dividend Reinvestment" Value="DVR"></asp:ListItem>
                    <asp:ListItem Text="Dividend Payout" Value="DVP"></asp:ListItem>
                </asp:DropDownList>
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
    </table>
</div>
<div>
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
</br>
<div style="float: inherit;">
    <asp:HiddenField ID="hdnAccountId" runat="server" />
</div>
