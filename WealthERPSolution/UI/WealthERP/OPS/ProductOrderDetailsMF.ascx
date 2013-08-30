<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductOrderDetailsMF.ascx.cs" 
Inherits="WealthERP.OPS.ProductOrderDetailsMF" EnableViewState="true"  %>


 <script type="text/javascript" language="javascript">
    function GetCustId(source, eventArgs) {

        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();

        alert(document.getElementById("<%= txtCustomerId.ClientID %>").value);
    }
    </script>

<table enableviewstate="true" runat="server" >
  
    <tr id="trTransactionType" >
        <td class="leftField" style="width: 20%">
            <asp:Label ID="ddlTransactionType" runat="server" Text="Transaction Type: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
           <asp:DropDownList ID="ddltransType" runat="server" CssClass="cmbField" 
                OnSelectedIndexChanged="ddltransType_SelectedIndexChanged" AutoPostBack="true"   >
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
          <%--  <asp:CompareValidator ID="CVTrxType" runat="server" ControlToValidate="ddltransType"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a transaction type"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>--%>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblReceivedDate" runat="server" Text="Application Received Date: "
                CssClass="FieldName"></asp:Label>
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
          <%--  <asp:CompareValidator ID="CVReceivedDate" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtReceivedDate" CssClass="cvPCG" Operator="DataTypeCheck"
                ValidationGroup="MFSubmit" ValueToCompare="" Display="Dynamic"></asp:CompareValidator>--%>
         <%--   <asp:CompareValidator ID="cvAppRcvDate" runat="server" ControlToValidate="txtReceivedDate"
                Display="Dynamic" CssClass="cvPCG" ValidationGroup="MFSubmit" ErrorMessage="<br />Application Received date must be less than or equal to Today"
                Operator="LessThanEqual" Type="Date"></asp:CompareValidator>--%>
        </td>
    </tr>
    <%--  <span id="Span7" class="spnRequiredField">*</span>--%>
    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtReceivedDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select an Application received Date"
                Display="Dynamic" runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>--%>
    <%-- </td>--%>
    <%--</tr>--%>
    <tr id="trARDate" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblApplicationNumber" runat="server" Text="Application Number:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtApplicationNumber" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblOrderDate" runat="server" Text="Order Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <telerik:RadDatePicker ID="txtOrderDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                OnSelectedDateChanged="txtOrderDate_DateChanged">
                <Calendar ID="Calendar1" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false" runat="server">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <span id="Span6" class="spnRequiredField">*</span>
           <%-- <asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtOrderDate" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>--%>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtOrderDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select order date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
           <%-- <asp:CompareValidator ID="cvOrderDate" runat="server" ControlToValidate="txtOrderDate"
                CssClass="cvPCG" ErrorMessage="<br />Order date cannot be greater than or equal to Today"
                ValueToCompare="" Operator="LessThanEqual" Type="Date"></asp:CompareValidator>--%>
        </td>
    </tr>
    <tr id="trAplNumber" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblAMC" runat="server" Text="AMC: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlAMCList" runat="server" CssClass="cmbLongField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlAMCList_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="spnAMC" runat="server" class="spnRequiredField">*</span>
           <%-- <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlAMCList"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an AMC"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>--%>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trOrderDate" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblSearchScheme" runat="server" Text="Scheme:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlAmcSchemeList" runat="server" CssClass="cmbLongField" width="400px" AutoPostBack="true"
                OnSelectedIndexChanged="ddlAmcSchemeList_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="spnScheme" runat="server" class="spnRequiredField">*</span>
           <%-- <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlAmcSchemeList"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a scheme"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>--%>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblFolioNumber" runat="server" Text="Folio Number:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlFolioNumber" runat="server" CssClass="cmbField" AutoPostBack="true">
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trOrderNo" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblOrderNumber" runat="server" Text="Order Number:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblGetOrderNo" runat="server" Text="" CssClass="txtField"></asp:Label>
        </td>
        <td align="right" id="tdLblNav" runat="server">
            <asp:Label ID="Label19" runat="server" Text="Purchase Price:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style1" id="tdtxtNAV" runat="server">
            <asp:TextBox ID="txtNAV" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
         <td class="leftField" style="width: 20%" visible="false">
            <asp:Label ID="lblPortfolio" runat="server" Text="Portfolio: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%" visible="false">
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trOrderType" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblOrderType" runat="server" Text="Order Type:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:RadioButton ID="rbtnImmediate" Class="cmbField" runat="server" AutoPostBack="true"
                GroupName="OrderType" Checked="True" Text="Immediate"   />
            <asp:RadioButton ID="rbtnFuture" Class="cmbField" runat="server" AutoPostBack="true"
                GroupName="OrderType" Text="Future"   />
        </td>
        <td align="right">
            <asp:Label ID="lblPurDate" runat="server" Text="As on Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style1">
            <asp:Label ID="lblNavAsOnDate" runat="server" CssClass="txtField" Enabled="false"></asp:Label>
        </td>
        <td colspan="2">
        </td>
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
            <asp:Label ID="lblFutureDate" runat="server" Text="Select Future Date:" CssClass="FieldName"></asp:Label>
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
            <span id="Span8" class="spnRequiredField">*</span>
           <%-- <asp:CompareValidator ID="CVFutureDate" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtFutureDate" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>--%>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtFutureDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select Future Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
           <%-- <asp:CompareValidator ID="cvFutureDate1" runat="server" ControlToValidate="txtFutureDate"
                CssClass="cvPCG" ErrorMessage="<br />Future date should  be greater than or equal to Today"
                Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>--%>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblFutureTrigger" runat="server" Text="Future Trigger:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtFutureTrigger" runat="server" CssClass="txtField" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
     <tr id="trBtnSubmit" runat="server">
        <td align="left" colspan="3">
            <asp:Button ID="BtnPayment" runat="server" Text="Payment Details" CssClass="PCGLongButton" 
                OnClick="btnSubmit_Click" />
                </td>
     </tr>
    </table>
       <asp:HiddenField ID="txtCustomerId" runat="server" />
       <asp:HiddenField ID="hdnAmcCode" runat="server" />       
        <asp:HiddenField ID="hdnSchemeCode" runat="server" />       
        
        <asp:HiddenField ID="hdnSchemeName" runat="server" />