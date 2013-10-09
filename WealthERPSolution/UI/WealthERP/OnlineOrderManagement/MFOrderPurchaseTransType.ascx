<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFOrderPurchaseTransType.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.MFOrderPurchaseTransactionType" %>
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
                            New Purchase
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table id="tbpurchase" width="100%">
    <tr>
        <td class="leftLabel" align="left">
            <asp:Label ID="lblAmc" runat="server" Text="AMC" CssClass="FieldName"></asp:Label>
        </td>
        <td class="leftLabel" align="left">
            <asp:Label ID="lblCategory" runat="server" Text="Category" CssClass="FieldName"></asp:Label>
        </td>
        <td class="leftLabel" align="left">
            <asp:Label ID="lblScheme" runat="server" Text="Scheme" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="rightData">
            <asp:DropDownList ID="ddlAmc" runat="server" CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlAmc_OnSelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span1" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Select an AMC" Display="Dynamic" ControlToValidate="ddlAmc"
                InitialValue="Select" ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span7" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Select Category"
                CssClass="rfvPCG" ControlToValidate="ddlCategory" ValidationGroup="btnSubmit"
                Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField" AutoPostBack="true">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select a scheme"
                CssClass="rfvPCG" ControlToValidate="ddlScheme" ValidationGroup="btnSubmit" Display="Dynamic"
                InitialValue="0"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
     <td class="leftLabel" align="left">
            <asp:Label ID="lblOption" runat="server" Text="Option" CssClass="FieldName"></asp:Label>
        </td>
        <td class="leftLabel" align="left">
            <asp:Label ID="lblDividendType" runat="server" Text="Dividend Type" CssClass="FieldName"></asp:Label>
        </td>
        <td class="leftLabel" align="left">
            <asp:Label ID="lblDividendFrequency" runat="server" Text="Dividend Frequency" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
    <td class="rightData">
         <asp:Label ID="lblOption1" runat="server" Text="Dividend" CssClass="FieldName"></asp:Label> 
        </td>
        <td class="rightData">
           <asp:Label ID="lblDividendType2" runat="server" Text="Dividend ReInvestment" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
           <asp:Label ID="lblDividendFrequency2" runat="server" Text="Monthly" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
            <asp:Label ID="lblOffer" runat="server" Text="Offer Doc" CssClass="FieldName"></asp:Label>
        </td>
        <td class="LeftLabel">
            <asp:Label ID="lblFact" runat="server" Text="Fact Sheet" CssClass="FieldName"></asp:Label>
        </td>
        <td class="LeftLabel">
            <asp:Label ID="lblNav" runat="server" Text="Latest Nav" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
            <asp:Label ID="lblMoh" runat="server" Text="Mode of Holding:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="leftlabel">
            <asp:Label ID="lblAmt" runat="server" Text="Amount" CssClass="FieldName"></asp:Label>
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblMin" runat="server" Text="Min Amount:" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="rightData">
            <asp:DropDownList ID="ddlMoh" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span3" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Select an AMC" Display="Dynamic" ControlToValidate="ddlMoh"
                InitialValue="Select" ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
        </td>
        <td class="rightData">
            <asp:TextBox ID="txtAmt" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="rightData">
            <asp:Label ID="lblMintxt" runat="server" Text="1000" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
            <asp:Label ID="lblHolder" runat="server" Text="Holder:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblNominee" runat="server" CssClass="FieldName"></asp:Label>
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblMultiple" runat="server" Text="Multiple There after:" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="rightData">
            <asp:TextBox ID="txtNominee" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="rightData">
            <asp:TextBox ID="txtHolder" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="rightData">
            <asp:Label ID="lblMulti" runat="server" Text="1000" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
    <td></td>
    <td class="leftLabel">
            <asp:Label ID="lblCutt" runat="server" Text="Cutt Off time" CssClass="FieldName"></asp:Label>
        </td class="rightData">
        <td>
            <asp:Label ID="lbltime" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
</table>
