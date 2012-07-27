<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFOrderEntry.ascx.cs" Inherits="WealthERP.OPS.MFOrderEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>


<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    }
    
     function openpopupAddCustomer() {
        window.open('PopUp.aspx?PageId=CustomerType', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
        return false;
    }

    function openpopupAddBank() {
        window.open('PopUp.aspx?PageId=AddBankDetails', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
        return false;
    }
</script>

<script type="text/javascript">
    function CustomerValidate(type) {
        if (type == 'pdf') {
            window.document.forms[0].target = '_blank';
            window.document.forms[0].action = "/Reports/Display.aspx?mail=2";
        } else if (type == 'doc') {
            window.document.forms[0].target = '_blank';
            window.document.forms[0].action = "/Reports/Display.aspx?mail=4";
        }
        else if (type == 'View') {
            window.document.forms[0].target = '_blank';
            window.document.forms[0].action = "/Reports/Display.aspx?mail=0";
        }
        else {
            window.document.forms[0].target = '_blank';
            window.document.forms[0].action = "/Reports/Display.aspx?mail=3";
        }

        setTimeout(function() {
            window.document.forms[0].target = '';
            window.document.forms[0].action = "ControlHost.aspx?pageid=OrderEntry";
        }, 500);
        return true;

    }
</script>

<table width="100%">
<tr>
        <td colspan="5">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                <tr>
                    <td align="left">MF Order Entry</td>
                    <td align="right">
                        <asp:LinkButton ID="lnkBtnEdit" runat="server" CssClass="LinkButtons" 
                            Text="Edit" onclick="lnkBtnEdit_Click"></asp:LinkButton>
                        &nbsp; &nbsp; 
                        <asp:LinkButton runat="server" ID="lnlBack" CssClass="LinkButtons" Text="Back" 
                            onclick="lnlBack_Click" ></asp:LinkButton>&nbsp;  &nbsp;
                        <asp:LinkButton runat="server" ID="lnkDelete" CssClass="LinkButtons" 
                            Text="Delete" onclick="lnkDelete_Click" 
                            OnClientClick="javascript: return confirm('Are you sure you want to Delete the Order?')"></asp:LinkButton>&nbsp;  &nbsp;
                        <asp:Button ID="btnViewReport" runat="server" PostBackUrl="~/Reports/Display.aspx?mail=0"
                            CssClass="CrystalButton" ValidationGroup="MFSubmit" OnClientClick="return CustomerValidate('View')" />&nbsp;&nbsp;
                            <div id="div1" style="display: none;">
                                <p class="tip">
                                    Click here to view order details.
                                </p>
                            </div>
                        <asp:Button ID="btnViewInPDF" runat="server" ValidationGroup="MFSubmit" OnClientClick="return CustomerValidate('pdf')"
                               PostBackUrl="~/Reports/Display.aspx?mail=2" CssClass="PDFButton" />&nbsp;&nbsp;
                                <div id="div2" style="display: none;">
                                <p class="tip">
                                     Click here to view order details in pdf format.
                                </p>
                                </div>
                        <asp:Button ID="btnViewInDOC" runat="server" ValidationGroup="MFSubmit" CssClass="DOCButton"
                                OnClientClick="return CustomerValidate('doc')" PostBackUrl="~/Reports/Display.aspx?mail=4" />
                                <div id="div3" style="display: none;">
                                <p class="tip">
                                    Click here to view order details in word doc.</p>
                                </div>
                         </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
<tr>
      <td colspan="5">
        </td>
</tr>
<tr>
      <td colspan="5">
        </td>
</tr>
<%--    <tr>
        <td colspan="5">
            <asp:Label ID="lblOrderEntry" runat="server" CssClass="HeaderTextBig" Text="MF Order Entry"></asp:Label>
            <hr />
        </td>
    </tr>--%>
    
      <tr>
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Customer Details
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="5">
        </td>
    </tr>
    
      <tr>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblCustomer" runat="server" Text="Customer Name: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True">
            </asp:TextBox><span id="spnCustomer" class="spnRequiredField">*</span>
            <%--<asp:Button ID="btnAddCustomer" runat="server" Text="Add a Customer" CssClass="PCGMediumButton"
                CausesValidation="true" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_OrderEntry_btnAddCustomer','S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_OrderEntry_btnAddCustomer','S');"
                OnClientClick="return openpopupAddCustomer()" 
                onclick="btnAddCustomer_Click" />--%>
            <asp:ImageButton ID="btnImgAddCustomer" ImageUrl="~/App_Themes/Maroon/Images/user_add.png" AlternateText="Add"
                runat="server" ToolTip="Click here to Add Customer" OnClientClick="return openpopupAddCustomer()"
                Height="15px" Width="15px" ></asp:ImageButton>
            <cc1:TextBoxWatermarkExtender ID="txtCustomer_water" TargetControlID="txtCustomerName"
                WatermarkText="Enter few chars of Customer" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtCustomerName_autoCompleteExtender" runat="server"
                TargetControlID="txtCustomerName" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                Enabled="True" />
           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCustomerName"
                ErrorMessage="<br />Please Enter Customer Name" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="Submit"></asp:RequiredFieldValidator>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblRM" runat="server" Text="RM: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblGetRM" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
        <td style="width: 25%">
            &nbsp;
        </td>
    </tr>
    
      <tr>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblPan" runat="server" Text="PAN No: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblgetPan" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
         <td class="leftField" style="width: 20%">
            <asp:Label ID="lblBranch" runat="server" Text="Branch: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblGetBranch" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    
     <tr>
        <td colspan="5">
        </td>
    </tr>
    
      <tr>
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Order Details
            </div>
        </td>
    </tr>
    
     <tr>
        <td colspan="5">
        </td>
    </tr>
    
      <tr id="trTransactionType" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="ddlTransactionType" runat="server" Text="Transaction Type: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
             <asp:DropDownList ID="ddltransType" runat="server" CssClass="cmbField" 
                 AutoPostBack="true" onselectedindexchanged="ddltransType_SelectedIndexChanged">
                <asp:ListItem Text="Select" Value="Select" Selected="true"></asp:ListItem>
                <asp:ListItem Text="New Purchase" Value="BUY"></asp:ListItem>
                <asp:ListItem Text="Additional Purchase" Value="ABY"></asp:ListItem>
                <asp:ListItem Text="Redemption" Value="Sel"></asp:ListItem>
                <asp:ListItem Text="SIP" Value="SIP"></asp:ListItem>
                <asp:ListItem Text="SWP" Value="SWP"></asp:ListItem>
                <asp:ListItem Text="STP" Value="STB"></asp:ListItem>
                <asp:ListItem Text="Switch" Value="SWB"></asp:ListItem>
                <asp:ListItem Text="Change Of Address Form" Value="CAF"></asp:ListItem>
             </asp:DropDownList>
            <span id="spnTransType" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CVTrxType" runat="server" ControlToValidate="ddltransType"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a transaction type"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
         <td class="leftField" style="width: 20%">
            <asp:Label ID="lblAMC" runat="server" Text="AMC: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlAMCList" runat="server" CssClass="cmbField" 
                AutoPostBack="true" onselectedindexchanged="ddlAMCList_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="spnAMC" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlAMCList"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an AMC"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
    </tr>
    
      <tr id="trARDate" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblReceivedDate" runat="server" Text="Application Received Date: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <telerik:RadDatePicker ID="txtReceivedDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:CompareValidator ID="CVReceivedDate" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtReceivedDate" CssClass="cvPCG" Operator="DataTypeCheck"
                ValidationGroup="MFSubmit" ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtReceivedDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select an Application received Date"
                Display="Dynamic" runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
            <%--<asp:CompareValidator ID="cvAppRcvDate" runat="server" ControlToValidate="txtReceivedDate"
                CssClass="cvPCG" ValidationGroup="MFSubmit" ErrorMessage="<br />Application Received date must be less than or equal to Today"
                Operator="LessThanEqual" Type="Date"></asp:CompareValidator>--%>
        </td>
         <td class="leftField" style="width: 20%">
            <asp:Label ID="lblCategory" runat="server" Text="Category: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" 
                AutoPostBack="true" onselectedindexchanged="ddlCategory_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    
      <tr id="trAplNumber" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblApplicationNumber" runat="server" Text="Application Number: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtApplicationNumber" runat="server" CssClass="txtField"></asp:TextBox><span
                id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtApplicationNumber"
                CssClass="rfvPCG" ErrorMessage="<br />Please select an Application number" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
        </td>
         <td class="leftField" style="width: 20%">
            <asp:Label ID="lblSearchScheme" runat="server" Text="Scheme: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlAmcSchemeList" runat="server" CssClass="cmbField" 
                AutoPostBack="true" 
                onselectedindexchanged="ddlAmcSchemeList_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="spnScheme" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlAmcSchemeList"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a scheme"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
    </tr>
    
      <tr  id="trOrderDate" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblOrderDate" runat="server" Text="Order Date: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <telerik:RadDatePicker ID="txtOrderDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtOrderDate" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtOrderDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select order date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
            <%--<asp:CompareValidator ID="cvOrderDate" runat="server" ControlToValidate="txtOrderDate"
                CssClass="cvPCG" ErrorMessage="<br />Order date should  be greater than or equal to Today"
                Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>--%>
        </td>
         <td class="leftField" style="width: 20%">
            <asp:Label ID="lblFolioNumber" runat="server" Text="Folio Number: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlFolioNumber" runat="server" CssClass="cmbField" AutoPostBack="true">
            </asp:DropDownList>
        </td>
    </tr>
    
      <tr id="trOrderNo" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblOrderNumber" runat="server" Text="Order Number: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblGetOrderNo" runat="server" Text="" CssClass="txtField"></asp:Label>
        </td>
         <td class="leftField" style="width: 20%">
            <asp:Label ID="lblPortfolio" runat="server" Text="Portfolio: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    
      <tr id="trOrderType" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblOrderType" runat="server" Text="Order Type: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:RadioButton ID="rbtnImmediate" Class="cmbField" runat="server" AutoPostBack="true"
                GroupName="OrderType" Checked="True" Text="Immediate" 
                oncheckedchanged="rbtnImmediate_CheckedChanged"  />
            <asp:RadioButton ID="rbtnFuture" Class="cmbField" runat="server" AutoPostBack="true" 
                GroupName="OrderType" Text="Future"  oncheckedchanged="rbtnImmediate_CheckedChanged" />
        </td>
        <td colspan="2"></td>
<%--         <td class="leftField" style="width: 20%">
            <asp:Label ID="lblOrderStatus" runat="server" Text="Order Status: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="cmbField" AutoPostBack="true">
            </asp:DropDownList>
        </td>--%>
    </tr>
    
      <%--<tr id="trrejectReason" runat="server">
        <td class="leftField" colspan="2" style="width: 40%">
  
        </td>
         <td class="leftField" style="width: 20%">
            <asp:Label ID="lblOrderPendingReason" runat="server" Text="Order Pending Reason: " CssClass="FieldName" ></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
              <asp:DropDownList ID="ddlOrderPendingReason" runat="server" CssClass="cmbField">
              </asp:DropDownList>
        </td>
    </tr>--%>
    
      <tr id="trfutureDate" runat="Server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblFutureDate" runat="server" Text="Select Future Date: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <telerik:RadDatePicker ID="txtFutureDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:CompareValidator ID="CVFutureDate" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtFutureDate" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtFutureDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select Future Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
            <%--<asp:CompareValidator ID="cvFutureDate1" runat="server" ControlToValidate="txtFutureDate"
                CssClass="cvPCG" ErrorMessage="<br />Future date should  be greater than or equal to Today"
                Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>--%>
        </td>
         <td class="leftField" style="width: 20%">
            <asp:Label ID="lblFutureTrigger" runat="server" Text="Future Trigger: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtFutureTrigger" runat="server" CssClass="txtField" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    
     <tr>
        <td colspan="5">
        </td>
    </tr>
    
      <tr id="trSection1" runat="server">
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Order Section Details
            </div>
        </td>
    </tr>
    
     <tr>
        <td colspan="5">
        </td>
    </tr>
    
      <tr id="trAmount" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblAmount" runat="server" Text="Amount: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField"></asp:TextBox><span
                id="Span5" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtAmount"
                CssClass="rfvPCG" ErrorMessage="<br />Please select amount" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator6" ControlToValidate="txtAmount" runat="server"
                ValidationGroup="MFSubmit" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value"
                Type="Double" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
        </td>
         <td class="leftField" style="width: 20%">
            <asp:Label ID="lblMode" runat="server" Text="Mode Of Payment: " CssClass="FieldName"></asp:Label>
        </td>
        
        <td class="rightField" style="width: 20%">
             <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Cheque" Value="CQ" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Draft" Value="DF"></asp:ListItem>
                <asp:ListItem Text="ECS" Value="ES"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    
      <tr id="trPINo" runat="server">
        <td class="leftField" style="width: 20%">
             <asp:Label ID="lblPaymentNumber" runat="server" Text="Payment Instrument Number: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtPaymentNumber" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
         <td class="leftField" style="width: 20%">
            <asp:Label ID="lblPIDate" runat="server" Text="Payment Instrument Date: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <telerik:RadDatePicker ID="txtPaymentInstDate" CssClass="txtField" runat="server"
                Culture="English (United States)" Skin="Telerik" EnableEmbeddedSkins="false"
                ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:CompareValidator ID="CVPaymentDate" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtPaymentInstDate" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
            <asp:CompareValidator ID="cvdate" runat="server" ErrorMessage="<br />Payment Instrument Date should be less than or equal to Order Date"
                Type="Date" ControlToValidate="txtPaymentInstDate" ControlToCompare="txtOrderDate"
                Operator="LessThanEqual" CssClass="cvPCG" Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>
        </td>
    </tr>
    
      <tr id="trBankName" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblBankName" runat="server" Text="Bank Name: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlBankName" runat="server" CssClass="cmbField" AutoPostBack="true"
                onselectedindexchanged="ddlBankName_SelectedIndexChanged">
            </asp:DropDownList><span id="Span4" class="spnRequiredField">*</span>
            <asp:ImageButton ID="imgAddBank" ImageUrl="~/App_Themes/Maroon/Images/user_add.png" AlternateText="Add"
                runat="server" ToolTip="Click here to Add Bank" OnClientClick="return openpopupAddBank()"
                Height="15px" Width="15px" ></asp:ImageButton>
        <asp:CompareValidator ID="CompareValidator11" runat="server" ControlToValidate="ddlBankName"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a Bank"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>   
            
        </td>
         <td class="leftField" style="width: 20%">
            <asp:Label ID="lblBranchName" runat="server" Text="Bank BranchName: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
             <asp:TextBox ID="txtBranchName" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    
      <tr id="trFrequency" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblFrequencySIP" runat="server" Text="Frequency: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlFrequencySIP" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
         <td class="leftField" colspan="2" style="width: 40%">
          
        </td>
    </tr>
    
      <tr id="trSIPStartDate" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblStartDateSIP" runat="server" Text="Start Date: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <telerik:RadDatePicker ID="txtstartDateSIP" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtstartDateSIP" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
        </td>
         <td class="leftField" style="width: 20%">
            <asp:Label ID="lblEndDateSIP" runat="server" Text="End Date: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <telerik:RadDatePicker ID="txtendDateSIP" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtendDateSIP" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    
      <tr id="trSection2" runat="server">
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Order Section Details
            </div>
        </td>
    </tr>
    
      <tr id="trGetAmount" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblAvailableAmount" runat="server" Text="Available Amount: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblGetAvailableAmount" runat="server" Text="" CssClass="txtField"></asp:Label>
        </td>
         <td class="leftField" style="width: 20%">
            <asp:Label ID="lblAvailableUnits" runat="server" Text="Available Units: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblGetAvailableUnits" runat="server" Text="" CssClass="txtField"></asp:Label>
        </td>
    </tr>
    
      <tr id="trRedeemed" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblReedeemed" runat="server" Text="Redeem/Switch: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:RadioButton ID="rbtAmount" Class="cmbField" runat="server" GroupName="AmountUnit"
                Checked="True" Text="Amount" />
            <asp:RadioButton ID="rbtUnit" Class="cmbField" runat="server" GroupName="AmountUnit"
                Text="Units" />
        </td>
         <td class="leftField" style="width: 20%">
            <asp:Label ID="lblAmountUnits" runat="server" Text="Amount/Unit: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
             <asp:TextBox ID="txtNewAmount" runat="server" CssClass="txtField"></asp:TextBox><span
                id="Span2" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtNewAmount"
                CssClass="rfvPCG" ErrorMessage="<br />Please select Amount/Unit" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator9" ControlToValidate="txtNewAmount" runat="server"
                Display="Dynamic" ErrorMessage="<br />Please enter a numeric value" Type="Double"
                Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
        </td>
    </tr>
    
      <tr id="trScheme" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblSchemeSwitch" runat="server" Text="To Scheme: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlSchemeSwitch" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span3" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="ddlSchemeSwitch"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a scheme"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
         <td class="leftField" colspan="2" style="width: 40%">
         </td>
    </tr>
    
      <tr id="trFrequencySTP" runat="server">
        <td class="leftField" style="width: 20%">
             <asp:Label ID="lblFrequencySTP" runat="server" Text="Frequency: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlFrequencySTP" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
         <td class="leftField" colspan="2" style="width: 40%">
         </td>
    </tr>
    
      <tr id="trSTPStart" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblstartDateSTP" runat="server" Text="Start Date: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <telerik:RadDatePicker ID="txtstartDateSTP" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtstartDateSTP" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
        </td>
         <td class="leftField" style="width: 20%">
            <asp:Label ID="lblendDateSTP" runat="server" Text="End Date: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <telerik:RadDatePicker ID="txtendDateSTP" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtendDateSTP" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    
      <tr id="trSection3" runat="server">
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Order Section Details
            </div>
        </td>
    </tr>
    
      <tr id="trAddress1" runat="server">
        <td colspan="4">
            <asp:Label ID="Label23" CssClass="HeaderTextSmall" runat="server" Text="Current Address"></asp:Label>
        </td>
    </tr>
      <tr id="trOldLine1" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblLine1" CssClass="FieldName" runat="server" Text="Line1(House No./Building):"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblGetLine1" CssClass="txtField" runat="server" Text=""></asp:Label>
        </td>
         <td class="leftField" style="width: 20%">
            <asp:Label ID="lblLine2" CssClass="FieldName" runat="server" Text="Line2(Street):"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblGetLine2" CssClass="txtField" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    
      <tr id="trOldLine3" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblLine3" CssClass="FieldName" runat="server" Text="Line3(Area):"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblGetline3" CssClass="txtField" runat="server" Text=""></asp:Label>
        </td>
         <td class="leftField" style="width: 20%">
            <asp:Label ID="lblLivingSince" CssClass="FieldName" runat="server" Text="Living Since:"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblGetLivingSince" CssClass="txtField" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    
      <tr id="trOldCity" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblCity" CssClass="FieldName" runat="server" Text="City:"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblgetCity" CssClass="txtField" runat="server" Text=""></asp:Label>
        </td>
         <td class="leftField" style="width: 20%">
            <asp:Label ID="lblstate" CssClass="FieldName" runat="server" Text="State:"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblGetstate" CssClass="FieldName" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    
      <tr id="trOldPin" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblPin" CssClass="FieldName" runat="server" Text="Pin Code:"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblGetPin" CssClass="txtField" runat="server" Text=""></asp:Label>
        </td>
         <td class="leftField" style="width: 20%">
            <asp:Label ID="lblCountry" CssClass="FieldName" runat="server" Text="Country:"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblGetCountry" CssClass="txtField" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    
      <tr id="trAddress6" runat="server">
        <td colspan="4">
            <asp:Label ID="Label18" CssClass="HeaderTextSmall" runat="server" Text="New Address"></asp:Label>
        </td>
    </tr>
    
      <tr id="trNewLine1" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblAdrLine1" CssClass="FieldName" runat="server" Text="Line1(House No./Building):"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtCorrAdrLine1" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
         <td class="leftField" style="width: 20%">
            <asp:Label ID="lblAdrLine2" CssClass="FieldName" runat="server" Text="Line2(Street):"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtCorrAdrLine2" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    
      <tr id="trNewLine3" runat="server">
        <td class="leftField" style="width: 20%">
           <asp:Label ID="lblAdrLine3" CssClass="FieldName" runat="server" Text="Line3(Area):"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtCorrAdrLine3" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
         <td class="leftField" style="width: 20%">
            <asp:Label ID="lblResidenceLivingDate" CssClass="FieldName" runat="server" Text="Living Since:"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <telerik:RadDatePicker ID="txtLivingSince" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:CompareValidator ID="txtLivingSince_CompareValidator" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtLivingSince" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    
      <tr id="trNewCity" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblAdrCity" CssClass="FieldName" runat="server" Text="City:"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtCorrAdrCity" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
         <td class="leftField" style="width: 20%">
            <asp:Label ID="lblAdrState" CssClass="FieldName" runat="server" Text="State:"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlCorrAdrState" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    
      <tr id="trNewPin" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblAdrPinCode" CssClass="FieldName" runat="server" Text="Pin Code:"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtCorrAdrPinCode" runat="server" CssClass="txtField" MaxLength="6"></asp:TextBox>
            <asp:CompareValidator ID="txtCorrAdrPinCode_comparevalidator" ControlToValidate="txtCorrAdrPinCode"
                runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value"
                Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
        </td>
         <td class="leftField" style="width: 20%">
            <asp:Label ID="lblAdrCountry" CssClass="FieldName" runat="server" Text="Country:"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlCorrAdrCountry" runat="server" CssClass="cmbField">
                <asp:ListItem Text="India" Value="India" Selected="True"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    
      <tr id="trBtnSubmit" runat="server">
        <td align="left" colspan="3">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" 
                ValidationGroup="MFSubmit" onclick="btnSubmit_Click" />
            <asp:Button ID="btnAddMore" runat="server" Text="Save & AddMore" CssClass="PCGMediumButton"  
                ValidationGroup="MFSubmit" onclick="btnAddMore_Click" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="PCGButton"  
                ValidationGroup="MFSubmit" onclick="btnUpdate_Click"  />
        </td>
        
    </tr>
    
</table>
<asp:Panel ID="pnlOrderSteps" runat="server" Width="100%" Height="80%">
    <table width="100%">
        <tr>
            <td>
                <telerik:RadGrid ID="rgvOrderSteps" runat="server" Skin="Telerik" CssClass="RadGrid" width="80%"
                    GridLines="None" AllowPaging="True" PageSize="20" AllowSorting="false" AutoGenerateColumns="False" OnItemCreated="rgvOrderSteps_ItemCreated"
                    ShowStatusBar="true" AllowAutomaticUpdates="false" HorizontalAlign="NotSet" DataKeyNames="CO_OrderId,WOS_OrderStepCode"
                    OnItemDataBound="rgvOrderSteps_ItemDataBound" OnItemCommand="rgvOrderSteps_ItemCommand" OnNeedDataSource="rgvOrderSteps_NeedDataSource">
                    <mastertableview commanditemdisplay="none" editmode="PopUp" EnableViewState="false">                    
                        <Columns>
                        <%-- <telerik:GridBoundColumn  DataField="CO_OrderId"  HeaderText="OrderId" UniqueName="CO_OrderId" ReadOnly="True">
                            <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridBoundColumn  DataField="WOS_OrderStep"  HeaderText="Stages" UniqueName="WOS_OrderStep" ReadOnly="True">
                            <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>                        
                        
                       <%-- <telerik:GridDropDownColumn UniqueName="DropDownColumnStatus" HeaderText="Status" 
                        ListTextField="XS_Status" ListValueField="XS_StatusCode" DataField="XS_StatusCode"></telerik:GridDropDownColumn>
                        
                        <telerik:GridDropDownColumn UniqueName="DropDownColumnStatusReason" HeaderText="Pending Reason"
                        ListTextField="XSR_StatusReason" ListValueField="XSR_StatusReasonCode" DataField="XSR_StatusReasonCode"></telerik:GridDropDownColumn>--%>
                        
                        
                        <telerik:GridTemplateColumn DataField="XS_StatusCode" HeaderText="Status" 
                         UniqueName="DropDownColumnStatus">  
                            <EditItemTemplate> 
                                <telerik:RadComboBox ID="ddlCustomerOrderStatus" SelectedValue='<%#Bind("XS_StatusCode") %>'  runat="server">  
                                </telerik:RadComboBox> 
                            </EditItemTemplate> 
                            <ItemTemplate>
                               <%#DataBinder.Eval(Container.DataItem, "XS_Status")%>  
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        
                        <telerik:GridTemplateColumn DataField="XSR_StatusReasonCode" HeaderText="Pending Reason" 
                         UniqueName="DropDownColumnStatusReason">  
                            <EditItemTemplate> 
                                <telerik:RadComboBox ID="ddlCustomerOrderStatusReason" SelectedValue='<%#Bind("XSR_StatusReasonCode") %>' runat="server">  
                                </telerik:RadComboBox> 
                            </EditItemTemplate>
                            <ItemTemplate>
                               <%#DataBinder.Eval(Container.DataItem, "XSR_StatusReason")%>  
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>                         
                        
                        <telerik:GridDateTimeColumn DataField="CMFOS_Date" HeaderText="Date" DataFormatString="{0:d}" HtmlEncode="false" DataType="System.DateTime"
                        UniqueName="CMFOS_Date" ReadOnly="true"/>
                        
                        <telerik:GridEditCommandColumn UpdateText="Update" EditText="Edit" UniqueName="EditCommandColumn" CancelText="Cancel">                
                            <HeaderStyle Width="85px"></HeaderStyle>
                        </telerik:GridEditCommandColumn>                        
                        <telerik:GridBoundColumn DataField="COS_IsEditable" DataType="System.Boolean" UniqueName="COS_IsEditable" Display="false" ReadOnly="True">
                        </telerik:GridBoundColumn>

                    </Columns>
        </mastertableview>
    </telerik:RadGrid>
</td>
</tr>
</table>
</asp:Panel>
<asp:HiddenField ID="txtCustomerId" runat="server" onvaluechanged="txtCustomerId_ValueChanged1"/>
<asp:HiddenField ID="hdnCustomerId" runat="server" />
<asp:HiddenField ID="hdnType" runat="server" />
<asp:HiddenField ID="hdnSchemeCode" runat="server" />
