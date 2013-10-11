<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFOrderAdditionalPurchase.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.MFOrderAdditionalPurchase" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Additional Purchase
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table id="tbpurchase" width="100%">
    <tr>
        <td class="leftLabel">
            <asp:Label ID="lblAmc" runat="server" Text="AMC:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlAmc" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlAmc_OnSelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span1" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Select an AMC" Display="Dynamic" ControlToValidate="ddlAmc"
                InitialValue="0" ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblCategory" runat="server" Text="Category" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span7" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Select Category"
                CssClass="rfvPCG" ControlToValidate="ddlCategory" ValidationGroup="btnSubmit"
                Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblScheme" runat="server" Text="Scheme" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlScheme_onSelectedChanged">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select a scheme"
                CssClass="rfvPCG" ControlToValidate="ddlScheme" ValidationGroup="btnSubmit" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
            <asp:Label ID="lblOption" runat="server" Text="Option" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:Label ID="lblDividendType" runat="server" CssClass="txtField"></asp:Label>
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblDividendFrequency" runat="server" Text="Dividend Frequency" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:Label ID="lbldftext" runat="server" CssClass="txtField"></asp:Label>
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblDivType" runat="server" Text="Dividend Type" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlDivType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlDivType_OnSelectedIndexChanged">
                <asp:ListItem>Dividend Reinvestement</asp:ListItem>
                <asp:ListItem>Dividend Payout</asp:ListItem>
            </asp:DropDownList>
        </td>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="rfvPCG"
            ErrorMessage="Enter " Display="Dynamic" ControlToValidate="ddlDivType" InitialValue="Select"
            ValidationGroup="btnSubmit">
        </asp:RequiredFieldValidator>
    </tr>
    <tr>
        <td class="leftLabel">
            <asp:Label ID="lblMoh" runat="server" Text="Mode of Holding:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlMoh" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span3" class="spnRequiredField">*</span>
            <br />
        </td>
        <td class="leftlabel">
            <asp:Label ID="lblAmt" runat="server" Text="Amount" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:TextBox ID="txtAmt" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <span id="Span4" class="spnRequiredField">*</span>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="rfvPCG"
            ErrorMessage="Enter Amount" Display="Dynamic" ControlToValidate="txtAmt" InitialValue="Select"
            ValidationGroup="btnSubmit">
        </asp:RequiredFieldValidator>
        <td class="leftLabel">
            <asp:Label ID="lblMin" runat="server" Text="Min Amount:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:Label ID="lblMintxt" runat="server" CssClass="txtField"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblHolder" CssClass="FieldName" Text="Add Holder :" runat="server"></asp:Label>
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblCutt" runat="server" Text="Cutt Off time" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:Label ID="lbltime" runat="server" Text="" CssClass="txtField"></asp:Label>
        </td>
        <td class="LeftLabel">
            <asp:Label ID="lblNav" runat="server" Text="Latest Nav" CssClass="FieldName"></asp:Label>
        </td>
        <%--<td class="leftLabel">
            <asp:Label ID="lblOffer" runat="server" Text="Offer Doc" CssClass="FieldName"></asp:Label>
        </td>--%>
        <td class="LeftLabel">
            <asp:Label ID="lblFact" runat="server" Text="Fact Sheet" CssClass="FieldName"></asp:Label>
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblMultiple" runat="server" Text="Multiple There after:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:Label ID="lblMulti" runat="server" CssClass="txtField"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="5">
        </td>
        <td>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="OnClick_Submit"
                CssClass="FieldName" ValidationGroup="btnSubmit"></asp:Button>
        </td>
    </tr>
</table>
