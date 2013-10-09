<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFOrderSIPTransType.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.MFOrderSIPTransType" %>
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
                            SIP
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
        <td class="rightData">
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged">
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
            <asp:TextBox ID="txtOfferDoc" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="LeftLabel">
            <asp:TextBox ID="txtFactSheet" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="LeftLabel">
            <asp:TextBox ID="txtLatestNAV" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
            <asp:Label ID="lblOption" runat="server" Text="Option:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="leftlabel">
            <asp:Label ID="lblDividendType" runat="server" Text="DividendType" CssClass="FieldName"></asp:Label>
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblDividendFreq" runat="server" Text="Dividend Frequency:" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
            <asp:TextBox ID="txtOption" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="leftlabel">
            <asp:Label ID="txtDividendType" Text="Dividend reinvestment" runat="server" CssClass="txtField"></asp:Label>
        </td>
        <td class="leftLabel">
            <asp:Label ID="txtDividendFrequency" Text="Monthly" runat="server" CssClass="txtField"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
            <asp:Label ID="lblModeofHolding" runat="server" Text="Mode of Holding:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="leftlabel">
            <asp:Label ID="lblAmount" runat="server" Text="Amount" CssClass="FieldName"></asp:Label>
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblFrequency" runat="server" Text="Frequency" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
            <asp:DropDownList ID="ddlModeOfHolding" runat="server" CssClass="cmbField"></asp:DropDownList>
        </td>
        <td class="leftlabel">
            <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="leftLabel">
            <asp:TextBox ID="txtFrequency" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
   
    <tr>
        <td class="leftLabel">
            <asp:Label ID="lblHolder" runat="server" Text="Holder:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="leftlabel">
            
        </td>
        <td class="leftLabel">
            <asp:Label ID="Label3" runat="server" Text="Start Dt:" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
            <asp:TextBox ID="txtHolder" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="leftlabel">
           
        </td>
        <td class="leftLabel">
            <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
           
        </td>
        <td class="leftlabel">
            
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblAutoRenew" runat="server" Text="Auto Renew:" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
           
        </td>
        <td class="leftlabel">
            
        </td>
        <td class="leftLabel">
            <asp:CheckBox ID="chkAutoRenew" runat="server" CssClass="FieldName"></asp:CheckBox>
        </td>
    </tr>
    
    <tr>
        <td class="leftLabel">
            <asp:Label ID="lblNominee" runat="server" Text="Nominee:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="leftlabel">
            
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblMinAmountrequired" runat="server" Text="Min Amount required:" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
            <asp:TextBox ID="txtNominee" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="leftlabel">
            
        </td>
        <td class="leftLabel">
            <asp:TextBox ID="txtMinAmtReqd" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
          
        </td>
        <td class="leftlabel">
            
        </td>
        <td class="leftLabel">
            <asp:Label ID="Label2" runat="server" Text="Mutiples There After:" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
          
        </td>
        <td class="leftlabel">
            
        </td>
        <td class="leftLabel">
            <asp:TextBox ID="txtMultiplesThereAfter" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
          
        </td>
        <td class="leftlabel">
            
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblCutOffTime" runat="server" Text="cut off time:" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
          
        </td>
        <td class="leftlabel">
            
        </td>
        <td class="leftLabel">
            <asp:TextBox ID="txtCutOffTime" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
          
        </td>
        <td class="leftlabel">
            
        </td>
        <td class="leftLabel">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="FieldName"></asp:Button>
        </td>
    </tr>
</table>
