<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FixedIncomeOrderEntry.ascx.cs"
    Inherits="WealthERP.OPS.FixedIncomeOrderEntry" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<%--<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>--%>
 

 <table runat="server" width="100%">
    <%--
  runat="server"--%>
    <tr id="trCatIss" runat="server">
        <td class="leftField" style="width:20%">
            <asp:Label ID="Label3" runat="server" Text="Category: " CssClass="FieldName"></asp:Label>
        </td>
        <td style="width: 20%">
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                 OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="SpanddlCategory" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidatorddlCategory" runat="server" ControlToValidate="ddlCategory"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select Category"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
         <td style="width:5%"></td>  
        <td align="right" style="width: 15%">
            <asp:Label ID="Label4" runat="server" Text="Issuer: " CssClass="FieldName"></asp:Label>
        </td>
        <td  style="width: 50%" align="left">
            <asp:DropDownList ID="ddlIssuer" runat="server" CssClass="cmbField" AutoPostBack="true"
                  OnSelectedIndexChanged="ddlIssuer_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="SpanddlIssuer" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidatorIssuer" runat="server" ControlToValidate="ddlIssuer"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Issuer"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="SchemeSeries" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label5" runat="server" Text="Scheme/Bond: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="SpanddlScheme" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlScheme"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Scheme"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td style="width:5%"></td>        
        <td align="right" style="width: 10%">
            <asp:Label ID="Label6" runat="server" Text="Series: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 35%">
            <asp:DropDownList ID="ddlSeries" runat="server" CssClass="Field" AutoPostBack="true"
                Width="150px" OnSelectedIndexChanged="ddlSeries_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="SpanddlSeries" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidatorSeries" runat="server" ControlToValidate="ddlSeries"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Series"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
       
    </tr>
    
     <tr id="TRTransSer" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label1" runat="server" Text="Transaction Type: " CssClass="FieldName"></asp:Label>
        </td>
        <td   style="width: 20%">
            <asp:DropDownList ID="ddlTranstype" runat="server" CssClass="cmbField" AutoPostBack="true"
                  OnSelectedIndexChanged="ddlTranstype_SelectedIndexChanged">
                <asp:ListItem Text="Select" Value="Select" Selected="true"></asp:ListItem>
                <asp:ListItem Text="New" Value="New"></asp:ListItem>
                <asp:ListItem Text="Renewal" Value="Renewal"></asp:ListItem>
            </asp:DropDownList>
            <span id="SpanddlTranstype" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidatorTranstype" runat="server" ControlToValidate="ddlTranstype"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Transaction Type"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
         <td style="width:5%"></td>  
        <td class="leftField" style="width: 10%">
            <asp:Label ID="Label15" runat="server" Text="Series Details: " CssClass="FieldName"></asp:Label>
        </td>
        <td   style="width: 35%">
            <asp:TextBox ID="txtSeries" runat="server" CssClass="txtField" ReadOnly="true"></asp:TextBox>
            <%-- <span id="Span1" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlAMCList"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an AMC"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>--%>
        </td>
         
    </tr>
    <tr id="trSchemeOpFreq" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label16" runat="server" Text="Scheme Option: " CssClass="FieldName"></asp:Label>
        </td>
        <td   style="width: 20%">
            <asp:DropDownList ID="ddlSchemeOption" runat="server" CssClass="cmbField" AutoPostBack="true"
                 OnSelectedIndexChanged="ddlSchemeOption_SelectedIndexChanged">
                <asp:ListItem Text="Select" Value="Select" Selected="true"></asp:ListItem>
                <asp:ListItem Text="Cummulative" Value="Cummulative"></asp:ListItem>
                <asp:ListItem Text="Non Cummulative" Value="NonCummulative"></asp:ListItem>
                <asp:ListItem Text="Annual income plan" Value="AIncPlan"></asp:ListItem>
            </asp:DropDownList>
            <span id="SpanddlSchemeOption" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidatorSchemeOption" runat="server" ControlToValidate="ddlSchemeOption"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an  SchemeOption"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
         <td style="width:5%"></td>  
        <td class="leftField" style="width: 10%">
            <asp:Label ID="Label17" runat="server" Text="Frequency: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 35%">
            <asp:DropDownList ID="ddlFrequency" runat="server" CssClass="cmbLongField" AutoPostBack="true"
                Width="150px" OnSelectedIndexChanged="ddlFrequency_SelectedIndexChanged">
                <asp:ListItem Text="Select" Value="Select" Selected="true"></asp:ListItem>
                <asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem>
                <asp:ListItem Text="Quarterly" Value="Quarterly"></asp:ListItem>
                <asp:ListItem Text="yearly" Value="yearly"></asp:ListItem>
                <asp:ListItem Text="Half yearly" Value="Hfyearly"></asp:ListItem>
            </asp:DropDownList>
            <span id="SpanddlFrequency" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidatorFrequency" runat="server" ControlToValidate="ddlFrequency"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Frequency"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td colspan="2">
        </td>
    </tr>
    
    
    <tr id="trDepPaypriv" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label21" runat="server" Text="Deposit Payable to:" CssClass="FieldName"></asp:Label>
        </td>
        <td   style="width: 20%">
            <asp:CheckBox ID="ChkFirstholder" runat="server" CssClass="txtField" Text="First holder">
            </asp:CheckBox>
            <asp:CheckBox ID="ChkEORS" runat="server" CssClass="txtField" Text="Either or survivor">
            </asp:CheckBox>
        </td>
        <td style="width:5%"></td>  
        <td  align="right"  style="width: 10%">
            <asp:Label ID="Label22" runat="server" Text="Privilege:" CssClass="FieldName"></asp:Label>
        </td>
       <%--  <td style="width:5%"></td>  --%>
        <td   style="width: 35%">
            <asp:CheckBox ID="ChkSeniorcitizens" runat="server" CssClass="txtField" Text="Senior Citizen">
            </asp:CheckBox>
            <asp:CheckBox ID="ChkWidow" runat="server" CssClass="txtField" Text="Widow"></asp:CheckBox>
            <asp:CheckBox ID="ChkArmedForcePersonnel" runat="server" CssClass="txtField" Text="Armed Force Personnel">
            </asp:CheckBox>
            <asp:CheckBox ID="CHKExistingrelationship" runat="server" CssClass="txtField" Text="Existing Relationship">
            </asp:CheckBox>
        </td>
         
    </tr>
    <tr id="trOrder" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblOrderNumber" runat="server" Text="Order No.:" CssClass="FieldName"></asp:Label>
        </td>
        <td   style="width: 20%">
            <asp:Label ID="lblGetOrderNo" runat="server" Text="" CssClass="txtField"></asp:Label>
        </td>
        <td style="width:5%"></td> 
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblOrderDate" runat="server" Text="Order Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td   style="width: 35%">
            <telerik:RadDatePicker ID="txtOrderDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                AutoPostBack="true" OnSelectedDateChanged="txtOrderDate_DateChanged">
                <Calendar ID="Calendar2" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false" runat="server">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <span id="SpantxtOrderDate" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidatorOrderDate" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtOrderDate" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtOrderDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select order date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
            <%-- <asp:CompareValidator ID="cvOrderDate" runat="server" ControlToValidate="txtOrderDate"
                CssClass="cvPCG" ErrorMessage="<br />Order date cannot be greater than or equal to Today"
                ValueToCompare="" Operator="LessThanEqual" Type="Date"></asp:CompareValidator>--%>
        </td>
        
    </tr>
    
    
    <tr id="trARDate" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblApplicationNumber" runat="server" Text="Application No.:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtApplicationNumber" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td style="width:5%"></td> 
        <td class="leftField" style="width: 10%">
            <asp:Label ID="Label7" runat="server" Text="Application Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 35%">
            <telerik:RadDatePicker ID="txtApplicationDate" CssClass="txtField" runat="server"
                Culture="English (United States)" Skin="Telerik" EnableEmbeddedSkins="false"
                ShowAnimation-Type="Fade" MinDate="1900-01-01" AutoPostBack="true" OnSelectedDateChanged="txtApplicationDt_DateChanged">
                <Calendar ID="Calendar3" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false" runat="server">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <span id="SpantxtApplicationDate" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidatorApplicationDate" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtOrderDate" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatororderdate" ControlToValidate="txtApplicationDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select Application date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
            <%-- <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtOrderDate"
                CssClass="cvPCG" ErrorMessage="<br />Order date cannot be greater than or equal to Today"
                ValueToCompare="" Operator="LessThanEqual" Type="Date"></asp:CompareValidator> --%>
        </td>
         
    </tr>
  <%--  id="trPayAmt"--%>
    <tr  runat="server">
     <td class="leftField" style="width: 20%">
            <asp:Label ID="Label18" runat="server" Text="Maturity Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td  style="width: 20%">
            <telerik:RadDatePicker ID="txtMaturDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                OnSelectedDateChanged="txtMaturDate_DateChanged"
                AutoPostBack="true">
                <Calendar ID="Calendar1" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false" runat="server">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <span id="SpantxtMaturDate" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidatorMaturDate" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtMaturDate" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtMaturDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select Maturity Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
            <%-- <asp:CompareValidator ID="cvOrderDate" runat="server" ControlToValidate="txtOrderDate"
                CssClass="cvPCG" ErrorMessage="<br />Order date cannot be greater than or equal to Today"
                ValueToCompare="" Operator="LessThanEqual" Type="Date"></asp:CompareValidator>--%>
        </td>
          
        <td colspan="3" style="width: 60%">
        </td>
    </tr>
    
    <tr id="trDepRen" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblgetPan" runat="server" Text="Existing Deposit Receipt No.:" CssClass="FieldName"></asp:Label>
        </td>
        <td  style="width: 20%">
            <asp:TextBox ID="txtExistDepositreceiptno" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" />
        </td>
         <td style="width:5%"></td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblBranch" runat="server" Text="Renewal Amount: " CssClass="FieldName" OnTextChanged="OnPayAmtTextchanged"></asp:Label>
        </td>
        <td   style="width: 35%">
            <asp:TextBox ID="txtRenAmt" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" />
        </td>
        
    </tr>
    <tr id="trMatAmtDate" runat="server">
         <td class="leftField" style="width: 20%">
            <asp:Label ID="Label8" runat="server" Text="FD Amount:" CssClass="FieldName">           
            </asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtPayAmt" runat="server" CssClass="txtField" OnTextChanged="OnPayAmtTextchanged"
                AutoPostBack="True" />
                 <span id="SpanPayAmt" runat="server" class="spnRequiredField">*</span>
                 <asp:CompareValidator ID="CompareValidatorPayamt" runat="server" ControlToValidate="txtPayAmt"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please Enter FD Amount"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtMaturDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select Maturity Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
        </td>
        <td style="width:5%"></td>
        <td align="right" style="width: 15%">
            <asp:Label ID="Label20" runat="server" Text="Maturity Amount:" CssClass="FieldName"></asp:Label>
        </td>
        <td   style="width: 30%">
            <asp:TextBox ID="txtMatAmt" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" />
        </td>
       
    </tr>
    </table> 
    <table>
    
   
    
    
    
    
    
    <%-- <tr id="trFolio" runat="server" >
    <td class="leftField" style="width: 20%">
            <asp:Label ID="lblFolioNumber" runat="server" Text="Folio Number:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
           <asp:TextBox ID="TextBox1" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>--%>
    <%-- <tr id="trbondsIP" runat="server" >
    <td class="leftField" style="width: 20%">
            <asp:Label ID="Label8" runat="server" Text="No. of Bonds:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
           <asp:TextBox ID="TextBox2" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
         <td class="leftField" style="width: 20%">
            <asp:Label ID="Label9" runat="server" Text="Issue Price:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
           <asp:TextBox ID="TextBox3" runat="server" CssClass="txtField" Enabled="false" ></asp:TextBox>
        </td>
    </tr>
     --%>
    <%-- <tr id="trAmtPay" runat="server">
        
        <td align="right" id="tdAmtPAy" runat="server">
            <asp:Label ID="Label19" runat="server" Text="Amount Payable:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style1" id="tdAmtPAy1" runat="server">
            <asp:TextBox ID="txtAmtPay" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
        </td>
      
    </tr>--%>
    <%-- <tr runat="server" id="Mop">
          <td class="leftField" style="width: 20%">
            <asp:Label ID="lblMode" runat="server" Text="Mode Of Payment:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Cheque" Value="CQ" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Draft" Value="DF"></asp:ListItem>
                <asp:ListItem Text="ECS" Value="ES"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>--%>
    <%--    <tr id="trPINo" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblPaymentNumber" runat="server" Text="Payment Instrument Number: "
                CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtPaymentNumber" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblPIDate" runat="server" Text="Payment Instrument Date:" CssClass="FieldName"></asp:Label>
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
            <%--<asp:CompareValidator ID="cvdate" runat="server" ErrorMessage="<br />Payment Instrument Date should be less than or equal to Order Date"
                Type="Date" ControlToValidate="txtPaymentInstDate" ControlToCompare="txtOrderDate"
                Operator="LessThanEqual" CssClass="cvPCG" Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>--%>
    <%-- </td>
    </tr> --%>
    <%--<tr id="trBankName" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblBankName" runat="server" Text="Bank Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
        <asp:TextBox ID="txtBankName" runat="server" CssClass="txtField"></asp:TextBox>
           <%-- <asp:DropDownList ID="ddlBankName" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged">
            </asp:DropDownList>--%>
    <%--<span id="Span4" class="spnRequiredField">*</span>--%>
    <%--<asp:ImageButton ID="imgAddBank" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                AlternateText="Add" runat="server" ToolTip="Click here to Add Bank" OnClientClick="return openpopupAddBank()"
                Height="15px" Width="15px"></asp:ImageButton>
            <asp:ImageButton ID="imgBtnRefereshBank" ImageUrl="~/Images/refresh.png" AlternateText="Refresh"
                runat="server" ToolTip="Click here to refresh Bank List" OnClick="imgBtnRefereshBank_OnClick"
                Height="15px" Width="25px"></asp:ImageButton>--%>
    <%--  <asp:CompareValidator ID="CompareValidator11" runat="server" ControlToValidate="ddlBankName"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a Bank"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>--%>
    <%--  </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblBranchAddress" runat="server" Text="BranchAddress:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtBranchAddress" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>--%>
    <%--<tr id="trMOh" runat="server">
     <td class="leftField" style="width: 20%">
            <asp:Label ID="Label10" runat="server" Text="Mode Of Holding:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:CheckBox  ID="chk1" runat="server" CssClass="txtField" Text="Physical"></asp:CheckBox>
              <asp:CheckBox  ID="CheckBox1" runat="server" CssClass="txtField" Text="Demat"></asp:CheckBox>
        </td>
        
        
        
        <td style="width: 20%" class="leftField" >
         <asp:Label ID="Label11" runat="server" Text="DP Id:" CssClass="FieldName"></asp:Label>
   
        </td> 
          <td style="width: 20%" class="rightField" >
             <asp:TextBox ID="txtDpId" runat="server" CssClass="txtField"></asp:TextBox>
          </td>
     </tr> --%>
    <%--<tr >
     <td colspan="2"   ></td>
     <td class="leftField">
      <asp:Label ID="Label12" runat="server" Text="Client id:" CssClass="FieldName"></asp:Label>
     </td>
     <td class="rightField" >
     <asp:TextBox ID="txtClientID" runat="server" CssClass="txtField"></asp:TextBox>
     </td>
     </tr>--%>
    <%-- <tr>
     <td>
      <asp:Label ID="Label13" runat="server" Text="Bank name for payment of interest/redemption:" CssClass="FieldName"></asp:Label>
     </td>
     <td>
      <asp:TextBox ID="txtBankPay" runat="server" CssClass="txtField"></asp:TextBox>
      </td>
     </tr>--%>
    <%--<tr>     
    
      <td class="leftField" style="width: 20%">
            <asp:Label ID="lblMode" runat="server" Text="Mode Of Payment:" CssClass="FieldName"></asp:Label>
        </td>
   
     </tr>--%>
    <%--  <tr id="trCust" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblCustName" runat="server" Text="Customer Name: " CssClass="FieldName"></asp:Label>
        </td>
           <td class="rightField" style="width: 20%">
         <asp:TextBox ID="txtCustomerName" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" onclientClick="ShowIsa()" >
            </asp:TextBox><span id="Span1" class="spnRequiredField">*</span>
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
        </tr> 
        
    <tr id="PanAndBranch">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblPan" runat="server" Text="Pan Number: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtPan" runat="server" CssClass="txtField" 
                AutoPostBack="True" > </asp:TextBox> 
         </td> 
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblBranch" runat="server" Text="Branch: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblGetBranch" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
        
    </tr>--%>
    <tr>
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
               Joint Holder/Nominee Details
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label2" runat="server" Text="Pick:" CssClass="FieldName" Visible="false" ></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:CheckBox ID="CheckBox2" runat="server" CssClass="txtField" Text="Joint Holder" Visible="false" >
            </asp:CheckBox>
            <asp:CheckBox ID="CheckBox3" runat="server" CssClass="txtField" Text="Nominee" Visible="false" ></asp:CheckBox>
        </td>
    </tr>
    <tr>
      <%--  <td class="leftField" style="width: 20%">
            <asp:Label ID="Label14" runat="server" Text="Mode of holding: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlModeofHOlding" runat="server" CssClass="cmbLongField" AutoPostBack="true"
                Width="150px">
            </asp:DropDownList>--%>
            <%-- <span id="Span1" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlAMCList"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an AMC"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>--%>
       <%-- </td>--%>
    </tr>
    <%--   </tr>--%>
     <tr id="trJoingHolding" runat="server">
                <td class="leftField">
                    <asp:Label ID="Label9" runat="server" CssClass="FieldName" Text="Joint Holding:"></asp:Label>
                </td>
                <td>
                    <asp:RadioButton ID="rbtnYes" runat="server" CssClass="cmbField" GroupName="rbtnJointHolding"
                        Text="Joint Holder" OnCheckedChanged="rbtnYes_CheckedChanged" AutoPostBack="true" />
                    <asp:RadioButton ID="rbtnNo" runat="server" CssClass="cmbField" GroupName="rbtnJointHolding"
                        Text="Nominee" AutoPostBack="true" OnCheckedChanged="rbtnYes_CheckedChanged" Checked="true" />
                </td>
            </tr>
            <tr id="trModeOfHolding" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblModeOfHolding" runat="server" CssClass="FieldName" Text="Mode Of Holding:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlModeofHOldingFI" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                    <span id="Span6" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />Please select a Mode of Holding"
                        ControlToValidate="ddlModeofHOldingFI" Operator="NotEqual" ValueToCompare="Select"
                        Display="Dynamic" CssClass="cvPCG" SetFocusOnError="true"></asp:CompareValidator>
                </td>
            </tr>
             
           
            <tr id="trJoinHolders" runat="server">
                <td colspan="2">
                    <asp:Label ID="lblJointHolders" runat="server" CssClass="HeaderTextSmall" Text=""></asp:Label>
                 <%--   <hr />--%>
                </td>
            </tr>
            <tr id="trJointHolderGrid" runat="server">
                <td colspan="2">
                    <asp:GridView ID="gvJointHoldersList" runat="server" AutoGenerateColumns="False"
                        Width="60%" CellPadding="4" DataKeyNames="MemberCustomerId, AssociationId" AllowSorting="True"
                        ShowFooter="true" CssClass="GridViewStyle">
                        <FooterStyle CssClass="FooterStyle" />
                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkId" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="Relationship" HeaderText="Relationship" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr id="trNoJointHolders" runat="server" visible="false">
                <td class="Message" colspan="2">
                    <asp:Label ID="lblNoJointHolders" runat="server" Text="You have no Joint Holder"
                        CssClass="FieldName"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr id="trError" runat="server" visible="false">
        <td colspan="2" class="SubmitCell">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </td>
    </tr>
            <tr id="trNomineeCaption" runat="server" visible="true">
                <td colspan="6" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        Nominees
                    </div>
                </td>
            </tr>
             
           <tr id="trNominees" runat="server">
                <td colspan="2">
                    <asp:GridView ID="gvNominees" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Width="60%" ShowFooter="true" DataKeyNames="MemberCustomerId, AssociationId"
                        AllowSorting="True" CssClass="GridViewStyle">
                        <FooterStyle CssClass="FooterStyle" />
                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkId0" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="Relationship" HeaderText="Relationship" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr> 
      <tr id="trNoNominee" runat="server" visible="false">
                <td class="Message" colspan="2">
                    <asp:Label ID="lblNoNominee" runat="server" Text="You have no Associations" CssClass="FieldName"></asp:Label>
                </td>
            </tr> 
   
    <tr>
    </tr>
    
</table>
<asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="txtCustomerId_ValueChanged1" />

 <%--<table width="100%">
   
            </table> --%>
<%--<table width="100%">
    <tr>
        <td>
            <asp:GridView ID="gvAssociation" runat="server" CellPadding="4" CssClass="GridViewStyle"
                AllowSorting="True" ShowFooter="true" AutoGenerateColumns="false" DataKeyNames="CA_AssociationId">
                <Columns>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblPANNumber" runat="server" Text="AssociateName"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblRMPANHeader" runat="server" Text='<%# Eval("AssociateName").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblPANNumber" runat="server" Text="Relationshi"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblRMPANHeader" runat="server" Text='<%# Eval("XR_Relationship").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <%-- OnSorting="gvAssociation_Sort"
                DataKeyNames="CA_AssociationId">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" HorizontalAlign="Left" VerticalAlign="Middle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle " />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                     <HeaderTemplate>
                            <asp:Label ID="LblSelect" runat="server" Text=""></asp:Label>
                            <br />
                            <%--<asp:Button ID="lnkSelectAll" Text="All" runat="server"  OnClientClick="return CheckAll();" />
    <%--  <input id="chkBoxAll" class="CheckField" name="CheckAllCustomer" value="Customer" type="checkbox" onclick="checkAllBoxes()"  />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkId" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblCustomerName" runat="server" Text="Name"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtCustNameSearch" runat="server" CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_CuCustomerAssociationSetup_btnCustomerSearch');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                           <asp:Label ID="lblCustNameHeader" runat="server" Text='<%# Eval("Cust_Comp_Name").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblBranchName" runat="server" Text="Branch"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtBranchSearch" runat="server" Text='<%# hdnBranchFilter.Value %>'
                                CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_CuCustomerAssociationSetup_btnBranchSearch');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblRMBranchNameHeader" runat="server" Text='<%# Eval("BranchName").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblRMName" runat="server" Text="RM"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtRMNameSearch" runat="server" Text='<%# hdnRMFilter.Value %>'
                                CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_CuCustomerAssociationSetup_btnRMSearch');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblRMNameHeader" runat="server" Text='<%# Eval("RMName").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                         <HeaderTemplate>
                                <asp:Label ID="lblArea" runat="server" Text="Area"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtAreaSearch" runat="server" CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_CuCustomerAssociationSetup_btnAreaSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAreaHeader" runat="server" Text='<%# Eval("Area").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblCityName" runat="server" Text="City & Area"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtCitySearch" runat="server" Text='<%# hdnCityFilter.Value %>'
                                CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_CuCustomerAssociationSetup_btnCitySearch');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCityHeader" runat="server" Text='<%# Eval("City").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblCustomerType" runat="server" Text="Type"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblRMCustomerType" runat="server" Text='<%# Eval("CustomerType").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblPANNumber" runat="server" Text="PAN"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblRMPANHeader" runat="server" Text='<%# Eval("PAN Number").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblAddressName" runat="server" Text="Address"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false"></HeaderStyle>
                        <ItemStyle Wrap="false"></ItemStyle>
                    </asp:TemplateField>
                    
                </Columns>
            </asp:GridView> --%>
<%--</table> --%> 
<%--
     <tr>
      <td>
    <asp:Panel ID="pnCustGrid" runat="server" Width="100%" Height="80%">
    <table width="100%">
        
          
                <telerik:RadGrid ID="rgvCustGrid" runat="server" Skin="Telerik" CssClass="RadGrid"
                    Width="80%" GridLines="None" AllowPaging="True" PageSize="20" AllowSorting="false"
                    AutoGenerateColumns="true"   ShowStatusBar="true" 
                    AllowAutomaticUpdates="false" HorizontalAlign="NotSet"  
                    > </telerik:RadGrid> 
                     
                    <%-- <MasterTableView CommandItemDisplay="none" EditMode="PopUp" EnableViewState="false">
                        <Columns>
                            <telerik:GridBoundColumn  DataField="CO_OrderId"  HeaderText="OrderId" UniqueName="CO_OrderId" ReadOnly="True">
                            <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn> 
                          <telerik:GridBoundColumn DataField="WOS_OrderStep" HeaderText="Stages" UniqueName="WOS_OrderStep"
                                ReadOnly="True">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn> --%>
<%-- </table> 
                             </asp:Panel> 
                            </td>
                            </tr> --%>
<asp:HiddenField ID="hdnDefaulteInteresRate" runat="server" />
<asp:HiddenField ID="hdnOrderId" runat="server" />
<asp:HiddenField ID="hdnNewFileName" runat="server" />
<asp:HiddenField ID="hdnFrequency" runat="server" />
 <asp:HiddenField ID="hdnMintenure" runat="server" />
 <asp:HiddenField ID="hdnMaxtenure" runat="server" />