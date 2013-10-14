<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFOrderRdemptionTransType.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.MFOrderRdemptionTransType" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<style>
    tr.spaceUnder > td
    {
        padding-bottom: .5em;
    }
</style>
<table width="100%">
    <tr class="spaceUnder">
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr class="spaceUnder">
                        <td align="left">
                            Redeem
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<div style="float: left;">
    <table id="tbpurchase">
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
                <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlScheme_onSelectedChanged">
                </asp:DropDownList>
                <span id="Span2" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select a scheme"
                    CssClass="rfvPCG" ControlToValidate="ddlScheme" ValidationGroup="btnSubmit" Display="Dynamic"></asp:RequiredFieldValidator>
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
                <asp:DropDownList ID="ddlFolio" CssClass="cmbField" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td colspan="2">
            </td>
        </tr>
        <tr id="trJointHolder" runat="server" class="spaceUnder">
            <td>
            </td>
            <td align="right" style="vertical-align: top;">
                <asp:Label ID="lblHolder" runat="server" Text="Joint Holder:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblHolderDisplay" runat="server" CssClass="FieldName"></asp:Label>
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
                <asp:Label ID="lblNomineeDisplay" runat="server" CssClass="FieldName"></asp:Label>
            </td>
            <td colspan="2">
            </td>
        </tr>
        <tr class="spaceUnder">
            <td>
            </td>
            <td align="right" style="vertical-align: top;">
                <asp:Label ID="lblNav" runat="server" Text=" Last Recorded NAV (Rs):" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNavDisplay" runat="server" CssClass="FieldName"></asp:Label>
            </td>
            <td colspan="2">
            </td>
        </tr>
        <tr class="spaceUnder">
            <td>
            </td>
            <td align="right" style="vertical-align: top;">
                <asp:Label ID="lblCutt" runat="server" Text="Cut-Off time" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbltime" runat="server" Text="" CssClass="FieldName"></asp:Label>
            </td>
            <td colspan="2">
            </td>
        </tr>
        <tr class="spaceUnder">
            <td>
            </td>
            <td align="right" style="vertical-align: top;">
                <asp:Label ID="lblUnitsheld" runat="server" Text="Units Held:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblUnitsheldDisplay" runat="server" CssClass="FieldName"></asp:Label>
            </td>
            <td colspan="2">
            </td>
        </tr>
        <tr class="spaceUnder">
            <td>
            </td>
            <td align="right" style="vertical-align: top;">
                <asp:Label ID="lblCurrentValue" runat="server" Text="Current Value Of Holdings:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCurrentValueDisplay" runat="server" CssClass="FieldName"></asp:Label>
            </td>
            <td colspan="2">
            </td>
        </tr>
        <%-- <tr class="spaceUnder">
            <td>
            </td>
            <td align="right" style="vertical-align: top;">
                <asp:Label ID="lblAmt" runat="server" Text="Amount:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAmt" runat="server" CssClass="txtField"></asp:TextBox>
                <span id="Span3" class="spnRequiredField">*</span>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAmt"
                    ErrorMessage="Please Enter Only Numbers" CssClass="rfvPCG" ValidationExpression="^\d+$"
                    ValidationGroup="btnSubmit"></asp:RegularExpressionValidator>
                </br>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select a Amount"
                    CssClass="rfvPCG" ControlToValidate="txtAmt" ValidationGroup="btnSubmit" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            
        </tr>--%>
        <tr class="spaceUnder">
            <td>
            </td>
            <td align="right" style="vertical-align: top;">
            </td>
            <td>
            </td>
            <td colspan="2">
            </td>
        </tr>
        <tr class="spaceUnder" id="trDivtype">
            <td>
            </td>
            <td align="right" style="vertical-align: top;">
                <asp:Label ID="lblDivType" runat="server" Text="Dividend Type:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDivType" runat="server" CssClass="cmbField">
                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Dividend Reinvestement" Value="DVR"></asp:ListItem>
                    <asp:ListItem Text="Dividend Payout" Value="DVP"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="rfvPCG"
                    ErrorMessage="Please Select an Dividend Type" Display="Dynamic" ControlToValidate="ddlDivType"
                    InitialValue="0" ValidationGroup="btnSubmit">
                </asp:RequiredFieldValidator>
            </td>
            <td colspan="2">
            </td>
        </tr>
        <tr class="spaceUnder" id="trDivfeq">
            <td>
            </td>
            <td align="right" style="vertical-align: top;">
                <asp:Label ID="lblDividendFrequency" runat="server" Text="Dividend Frequency:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbldftext" runat="server" CssClass="FieldName"></asp:Label>
            </td>
            <td colspan="2">
            </td>
        </tr>
        <tr class="spaceUnder">
            <td>
            </td>
            <td align="right" style="vertical-align: top;">
                <asp:Label ID="lblRedeem" runat="server" Text="Redeem:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlRedeem" runat="server" AutoPostBack="true" CssClass="cmbField"
                    OnSelectedIndexChanged="ddlRedeem_OnSelectedIndexChanged">
                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Units" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Amounts" Value="2"></asp:ListItem>
                    <asp:ListItem Text="All" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="spaceUnder">
            <td>
            </td>
            <td align="right" style="vertical-align: top;">
                <asp:Label ID="lblRedeemType" runat="server" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtRedeemTypeValue" runat="server" CssClass="FieldName"></asp:TextBox>
            </td>
            <td colspan="2">
            </td>
        </tr>
        <tr class="spaceUnder">
            <td>
            </td>
            <td align="right" style="vertical-align: top;">
                <asp:Label ID="lblOption" runat="server" Text="Option" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDividendType" runat="server" CssClass="txtField"></asp:Label>
            </td>
            <td colspan="2">
            </td>
        </tr>
        <tr class="spaceUnder">
            <td style="width: 150px;">
            </td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="OnClick_Submit"
                    CssClass="FieldName" ValidationGroup="btnSubmit"></asp:Button>
            </td>
            <td colspan="2">
            </td>
        </tr>
    </table>
</div>
<div>
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
</br> 