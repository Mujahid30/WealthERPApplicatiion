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
<table width="100%">
    <tr class="spaceUnder">
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr class="spaceUnder">
                        <td align="left">
                            SIP
                        </td>
                    </tr>
                </table>
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
                </asp:DropDownList>
                <span id="Span1" class="spnRequiredField">*</span>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rfvPCG"
                    ErrorMessage="Please Select an AMC" Display="Dynamic" ControlToValidate="ddlAmc"
                    InitialValue="Select" ValidationGroup="btnSubmit">
                </asp:RequiredFieldValidator>
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
                </asp:DropDownList>
                <span id="Span7" class="spnRequiredField">*</span> </br>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Select Category"
                    CssClass="rfvPCG" ControlToValidate="ddlCategory" ValidationGroup="btnSubmit"
                    Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
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
                <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                </asp:DropDownList>
                <span id="Span2" class="spnRequiredField">*</span> </br>
                <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ErrorMessage="Please Select a scheme"
                    CssClass="rfvPCG" ControlToValidate="ddlScheme" ValidationGroup="btnSubmit" Display="Dynamic"
                    InitialValue="0"></asp:RequiredFieldValidator>
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
                </asp:DropDownList>
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
                <asp:Label ID="lblNav" runat="server" Text="Last Recorderd NAV(Rs):" CssClass="FieldName"></asp:Label>
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
                    <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                    <%--  <asp:ListItem Text="Quarterly" Value="QT"></asp:ListItem>--%>
                    <asp:ListItem Text="Monthly" Value="MN"></asp:ListItem>
                    <asp:ListItem Text="Quarterly" Value="QT"></asp:ListItem>
                </asp:DropDownList>
                <span id="Span4" class="spnRequiredField">*</span> </br>
                <asp:RequiredFieldValidator class="rfvPCG" ID="rfvFrequency" runat="server" ErrorMessage="Please select a frequency"
                    ControlToValidate="ddlFrequency">Please select a frequency</asp:RequiredFieldValidator>
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
                <asp:DropDownList ID="ddlStartDate" CssClass="cmbField" runat="server" AutoPostBack="True">
                </asp:DropDownList>
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
                <asp:Label ID="lblTotalInstallments" runat="server" Text="TotalInstallments:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlTotalInstallments" CssClass="cmbField" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlTotalInstallments_SelectedIndexChanged">
                </asp:DropDownList>
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
            <td><asp:Label ID="lblMutiplesThereAfter" runat="server" CssClass="FieldName" Text="Subsequent Amount:"></asp:Label>
            </td>
            <td><asp:Label ID="lblMutiplesThereAfterDisplay" runat="server" CssClass="FieldName"></asp:Label>
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
                <span id="Span3" class="spnRequiredField">*</span> </br>
                <asp:RequiredFieldValidator class="rfvPCG" ID="rfvAmount" runat="server" ControlToValidate="txtAmount"
                    ErrorMessage="Please enter a valid amount">Please enter a valid amount</asp:RequiredFieldValidator></br>
                   
            </td>
            <td style="vertical-align:top;">
                <asp:Label ID="lblMinAmountrequired" runat="server" Text="Minimum Initial Amount:" CssClass="FieldName"></asp:Label>
            </td>
            <td style="vertical-align:top;"><asp:TextBox ID="txtMinAmtDisplay" CssClass="txtField" Enabled="false" runat="server"></asp:TextBox>
                <asp:Label style="display:none;" ID="lblMinAmountrequiredDisplay"  runat="server" CssClass="FieldName"></asp:Label>
            </td>
        </tr>
        <tr style="display:none;" id="trDividendType" runat="server" class="spaceUnder">
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
        <tr id="trDividendFrequency" runat="server" class="spaceUnder">
            <td>
            </td>
            <td align="right">
                <asp:Label ID="lblDividendFreq" runat="server" Text="Dividend Frequency:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDividendFreq" CssClass="cmbField" runat="server" AutoPostBack="True">
                    <asp:ListItem Text="Dividend Reinvestment" Value="DVR"></asp:ListItem>
                    <asp:ListItem Text="Dividend Payout" Value="DVP"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr id="trDividendOption" runat="server" class="spaceUnder">
            <td>
            </td>
            <td align="right">
                <asp:Label ID="lblDividendOption" runat="server" Text="Dividend Option:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDividendOptionDisplay" runat="server" CssClass="FieldName"></asp:Label>
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
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" OnClick="btnSubmit_Click">
                </asp:Button>
            </td>
            <td>
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
        </tr>
        <tr class="spaceUnder">
            <td>
                <asp:LinkButton ID="lnkOfferDoc" CausesValidation="false"  Text="Offer Doc" runat="server" CssClass="txtField"></asp:LinkButton>
            </td>
        </tr>
        <tr class="spaceUnder">
            <td>
                <asp:LinkButton ID="lnkFactSheet" CausesValidation="false" Text="Fact Sheet" runat="server" CssClass="txtField"></asp:LinkButton>
            </td>
        </tr>
        <tr class="spaceUnder">
            <td>
                <asp:LinkButton ID="lnkExitLoad" CausesValidation="false" runat="server" Text="Exit Load" CssClass="txtField"></asp:LinkButton>
            </td>
        </tr>
        <tr style="display:none;" class="spaceUnder">
            <td>
                <asp:LinkButton ID="lnkExitDetails"  Text="Exit Details" runat="server" CssClass="txtField"></asp:LinkButton>
            </td>
        </tr>
    </table>
</div>
</br>
<div style="float: inherit;">
    <asp:HiddenField ID="hdnAccountId" runat="server" />
</div>
