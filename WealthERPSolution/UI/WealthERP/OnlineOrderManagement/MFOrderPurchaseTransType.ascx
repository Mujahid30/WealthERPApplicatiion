<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFOrderPurchaseTransType.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.MFOrderPurchaseTransactionType" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<script type="text/javascript">
    function ShowPopup() {
        var i = 0;
        var form = document.forms[0];
        var folioId = "";
        var count = 0;
        for (var i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == 'checkbox') {
                if (form.elements[i].checked == true) {
                    count++;
                }
            }
        }
        if (count == 0) {
            alert("Please select one record.")
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
<table id="tblMessage" width="100%" runat="server" visible="false">
    <tr id="trSumbitSuccess">
        <td align="center">
            <div id="msgRecordStatus" class="success-msg" align="center" runat="server">
                
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
                    <asp:ListItem Value="New" Text="New"></asp:ListItem>
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
                <asp:Label ID="lblCutt" runat="server" Text="Cut-Off time:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbltime" runat="server" Text="" CssClass="FieldName"></asp:Label>
            </td>
            <td align="right" style="vertical-align: top;">
                <asp:Label ID="lblMultiple" runat="server" Text="Subsequent Amount:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblMulti" runat="server" CssClass="FieldName"></asp:Label>
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
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAmt"
                    ErrorMessage="Please Enter Only Numbers" CssClass="rfvPCG" ValidationExpression="^\d+$"
                    ValidationGroup="btnSubmit"></asp:RegularExpressionValidator>
                </br>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select a Amount"
                    CssClass="rfvPCG" ControlToValidate="txtAmt" ValidationGroup="btnSubmit" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td align="right" style="vertical-align: top;">
                <asp:Label ID="lblMin" runat="server" Text="Minimum Initial Amount:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblMintxt" runat="server" CssClass="FieldName"></asp:Label>
            </td>
        </tr>
        <tr class="spaceUnder" id="trDivtype">
            <td>
            </td>
            <td align="right" style="vertical-align: top;">
                <asp:Label ID="lblDivType" runat="server" Text="Dividend Type:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDivType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlDivType_OnSelectedIndexChanged">
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
        <tr class="spaceUnder" id="trDivfeq">
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
        <tr class="spaceUnder">
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
        <tr class="spaceUnder">
            <td style="width: 150px;">
            </td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="OnClick_Submit"
                    CssClass="FieldName" ValidationGroup="btnSubmit"></asp:Button>
            </td>
            <%-- <td >
             <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="ResetControlDetails"
                    CssClass="FieldName"></asp:Button>
            </td>--%>
            <td>
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
                    CssClass="txtField" Visible="false"></asp:LinkButton>
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
                    CssClass="txtField" Visible="false"></asp:LinkButton>
            </td>
        </tr>
    </table>
</div>
</br> 