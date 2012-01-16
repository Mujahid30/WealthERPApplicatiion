<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderEntry.ascx.cs" Inherits="WealthERP.OPS.OrderEntry" %>
<%@ Register Src="../General/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function() {
    $('#ctrl_OrderEntry_btnViewReport').bubbletip($('#div1'), { deltaDirection: 'left' });
    $('#ctrl_OrderEntry_btnViewInPDF').bubbletip($('#div2'), { deltaDirection: 'left' });
    $('#ctrl_OrderEntry_btnViewInDOC').bubbletip($('#div3'), { deltaDirection: 'left' });
    });
</script>

<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    };

    
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
        else         
        {
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

 <%--<script type="text/javascript">

     function ShowHideFuture() {


       
         if (document.getElementById("<%= rbtnFuture.ClientID %>").checked == true) {
          

             document.getElementById("<%= trFutureTrigger.ClientID %>").style.display = 'block';
             document.getElementById("<%= trfutureDate.ClientID %>").style.display = 'block';
         }
         else if (document.getElementById("<%= rbtnImmediate.ClientID %>").checked == true) {

         document.getElementById("<%= trFutureTrigger.ClientID %>").style.display = 'none';
         document.getElementById("<%= trfutureDate.ClientID %>").style.display = 'none';
       
         }
          
     }
  

</script>--%>
<table width="100%">
<tr>
<td width="30%">
<asp:Label ID="lblOrderEntry" runat="server" CssClass="HeaderTextBig" Text="Order Entry"></asp:Label>
</td>
<td id="trReportButtons" runat="server" width="70%"  align="right">
<asp:Button ID="btnViewReport" runat="server"  PostBackUrl="~/Reports/Display.aspx?mail=0" CssClass="CrystalButton" 
ValidationGroup="MFSubmit" OnClientClick="return CustomerValidate('View')" />&nbsp;&nbsp;
            <div id="div1" style="display: none;">
                <p class="tip">
                    Enter and view order details.
                </p>
            </div>
<asp:Button ID="btnViewInPDF" runat="server"  ValidationGroup="MFSubmit" OnClientClick="return CustomerValidate('pdf')"
     PostBackUrl="~/Reports/Display.aspx?mail=2" CssClass="PDFButton"  />&nbsp;&nbsp;
     <div id="div2" style="display: none;">
                <p class="tip">
                    Enter and view order details.
                </p>
      </div>
<asp:Button ID="btnViewInDOC" runat="server" ValidationGroup="MFSubmit"  CssClass="DOCButton" OnClientClick="return CustomerValidate('doc')"
     PostBackUrl="~/Reports/Display.aspx?mail=4" />
     <div id="div3" style="display: none;">
                <p class="tip">
                    Enter and view order details..</p>
     </div>
</td>
</tr>
<tr>
<td colspan="2"><hr /></td>
</tr>
</table>

<%--<telerik:RadToolBar ID="aplToolBar" runat="server" Skin="Telerik" EnableEmbeddedSkins="false" EnableShadows="true" EnableRoundedCorners="true"
    Width="100%"  OnButtonClick="aplToolBar_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ID="btnEdit" runat="server" Text="Edit" Value="Edit" ImageUrl="~/Images/Telerik/EditButton.gif"
            ImagePosition="Left" ToolTip="Edit">            
        </telerik:RadToolBarButton>        
        <telerik:RadToolBarButton ID="btnPrintTransSlip" runat="server" Text="Print Transaction Slip" Value="PrintTransactionSlip" ToolTip="Print Transaction Slip">            
        </telerik:RadToolBarButton>
        
        <telerik:RadToolBarButton ID="btnSubmitOnline" runat="server" Text="Submit Online" Value="SubmitOnline" ToolTip="Submit Online">            
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>--%>

        <%--<table width="100%">
        <tr>
        <td align="center">
            <div id="msgRecordStatus" runat="server" class="success-msg" align="center" visible="false">
                Record added Successfully
            </div>
        </td>
       </tr>
       </table>
        </table>--%>
        <table width="100%">
        <tr>
        <td align="center">
            <div id="msgRecordStatus" runat="server" class="success-msg" align="center" visible="false">
                Order entered Successfully
            </div>
        </td>
       </tr>
       </table>
<table width="100%">
<tr>
<td colspan="4">
<asp:LinkButton runat="server" ID="lnkBtnEdit" CssClass="LinkButtons" Text="Edit" 
        onclick="lnkBtnEdit_Click" ></asp:LinkButton>
</td>
</tr>
<tr>
  <td align="right" valign="top">
<asp:Label ID="lblAssetType" runat="server" Text="Asset Type: "  CssClass="FieldName"></asp:Label><br />
<asp:Label ID="lblBranch" runat="server" Text="Branch: "  CssClass="FieldName"></asp:Label>
</td>
  <td valign="top">
  <asp:Label ID="lblShowAssetType" runat="server" Text="Mutual Fund"  CssClass="FieldName"></asp:Label><br />
<%--    <asp:DropDownList ID="ddlAssetType" runat="server" CssClass="cmbField" 
          AutoPostBack="true" onselectedindexchanged="ddlAssetType_SelectedIndexChanged">
       <asp:ListItem Text="Select" Value="Select" Selected="true"></asp:ListItem>
        <asp:ListItem Text="Mutual Fund" Value="Mutual Fund"></asp:ListItem>
       <asp:ListItem Text="Equity" Value="Equity"></asp:ListItem>
        <asp:ListItem Text="Insurance" Value="Insurance"></asp:ListItem>
        <asp:ListItem Text="Loan Application" Value="Loan Application"></asp:ListItem>
        <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
     </asp:DropDownList>--%>
     <asp:Label ID="lblGetBranch" runat="server" Text=""  CssClass="FieldName"></asp:Label>
  </td>
  <td align="right" valign="top">
  <asp:Label ID="lblCustomer" runat="server" Text="Customer: "  CssClass="FieldName"></asp:Label>
  </td>
  <td align="left">
    <%--
   <asp:TextBox ID="txtCustomerName" runat="server" CssClass="txtField" 
          AutoPostBack="true"  AutoComplete="Off"></asp:TextBox>
           <asp:RequiredFieldValidator ID="rvcustomerName" ControlToValidate="txtCustomerName"
                                         CssClass="rfvPCG" ErrorMessage="<br />Please select a customer" Display="Dynamic"
                                            runat="server" InitialValue="" ValidationGroup="MFSubmit">
                                  </asp:RequiredFieldValidator>
     <cc1:TextBoxWatermarkExtender ID="txtCustomerName_water" TargetControlID="txtCustomerName" WatermarkText="Enter few chars of Customer"
     runat="server" EnableViewState="false">
     </cc1:TextBoxWatermarkExtender>
     <ajaxToolkit:AutoCompleteExtender ID="txtCustomerName_autoCompleteExtender" runat="server"
     TargetControlID="txtCustomerName" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
     MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
      CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
     CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
     UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters="" Enabled="True"  />
     <span style='font-size: 10px; font-weight: normal' class='FieldName'><br />
     Enter few characters of Customer. </span>--%>
     <asp:TextBox ID="txtCustomerName" runat="server" CssClass="txtField" AutoComplete="Off" AutoPostBack="True">
     </asp:TextBox><span id="spnCustomer" class="spnRequiredField">*</span>
     <ajaxToolkit:AutoCompleteExtender ID="txtCustomerName_autoCompleteExtender" runat="server"
     TargetControlID="txtCustomerName" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
     MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
     CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
     CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
     UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters="" Enabled="True"  />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <span style='font-size: 9px; font-weight: normal' class='FieldName'><br />
       Enter few characters of Individual customer name. </span>                                                        
                                                            
       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCustomerName"
       ErrorMessage="<br />Please Enter Customer Name" Display="Dynamic" runat="server"
       CssClass="rfvPCG" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
      
    </td>
    <td>
    <asp:Button ID="btnAddCustomer" runat="server" Text="Add a Customer" 
          CssClass="PCGMediumButton" CausesValidation="true" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_OrderEntry_btnAddCustomer','M');"
                
          onmouseout="javascript:ChangeButtonCss('out', 'ctrl_OrderEntry_btnAddCustomer','M');" 
          onclick="btnAddCustomer_Click" />   
    </td>
</tr>
<tr>
 
  <td align="right">
  <asp:Label ID="lblPan" runat="server" Text="PAN No: "  CssClass="FieldName"></asp:Label>
  <%-- <asp:DropDownList ID="ddlBranch" runat="server" CssClass="cmbField" 
          onselectedindexchanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>--%>
  </td>
  <td>
  <asp:Label ID="lblgetPan" runat="server" Text=""  CssClass="FieldName"></asp:Label>
  </td>
  <td align="right">
  <asp:Label ID="lblRM" runat="server" Text="RM: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
  <asp:Label ID="lblGetRM" runat="server" Text=""  CssClass="FieldName"></asp:Label>
  <%--<asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField"></asp:DropDownList>--%>
  </td>
</tr>
<tr>
<td align="right">
  
  </td>
   <td>
  
  </td>
  <td colspan="2"></td>
</tr>


<tr id="trSectionTwo1" runat="server">
<td colspan="5">
<hr />
</td>
</tr>
<tr id="trSectionTwo3" runat="server">
  <td align="right" valign="top">
  <asp:Label ID="ddlTransactionType" runat="server" Text="Transaction Type: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
        <asp:DropDownList ID="ddltransType" runat="server" CssClass="cmbField"  AutoPostBack="true" onselectedindexchanged="ddltransType_SelectedIndexChanged">
         <asp:ListItem Text="Select" Value="Select" Selected="true"></asp:ListItem>
        <asp:ListItem Text="New Purchase" Value="BUY"></asp:ListItem>
        <asp:ListItem Text="Additional Purchase" Value="ABY"></asp:ListItem>
        <asp:ListItem Text="Redemption" Value="Sel"></asp:ListItem>
        <asp:ListItem Text="SIP" Value="SIP"></asp:ListItem>
        <asp:ListItem Text="SWP" Value="SWP"></asp:ListItem>
        <asp:ListItem Text="STP" Value="STB"></asp:ListItem>
        <asp:ListItem Text="Switch" Value="SWB"></asp:ListItem>
        <asp:ListItem Text="Change Of Address Form" Value="CAF"></asp:ListItem>
        <%--<asp:ListItem Text="Change Of Bank Details" Value="Change_Of_Bank_Details"></asp:ListItem>--%>
       <%-- <asp:ListItem Text="ConsoliDation Of Folio" Value="ConsoliDation_Of_Folio"></asp:ListItem>
        <asp:ListItem Text="Change Of Nominee" Value="Change_Of_Nominee"></asp:ListItem>--%>
    </asp:DropDownList><span id="spnTransType" class="spnRequiredField">*</span>
    <asp:CompareValidator ID="CVTrxType" runat="server" 
                        ControlToValidate="ddltransType" CssClass="cvPCG" Display="Dynamic" 
                        ErrorMessage="<br />Please select a transaction type" Operator="NotEqual" 
                        ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
  </td>
  <td align="right" valign="top">
<asp:Label ID="lblAMC" runat="server" Text="AMC: "  CssClass="FieldName"></asp:Label>
</td>
<td align="left">
  <asp:DropDownList ID="ddlAMCList" runat="server" CssClass="cmbField" AutoPostBack="true"
        onselectedindexchanged="ddlAMCList_SelectedIndexChanged">
  </asp:DropDownList><span id="Span2" class="spnRequiredField">*</span>
  <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlAMCList" CssClass="cvPCG" Display="Dynamic" 
   ErrorMessage="<br />Please select an AMC" Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
</td>
</tr>
<tr id="trSectionTwo2" runat="server">
<td align="right" valign="top">
  <asp:Label ID="lblReceivedDate" runat="server" Text="Application Received Date: "  CssClass="FieldName"></asp:Label>
  </td>
  <td align="left">
  <asp:TextBox ID="txtReceivedDate" runat="server" CssClass="txtField"></asp:TextBox><span id="spnReceiveDate" class="spnRequiredField">*</span>
           <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtReceivedDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                TargetControlID="txtReceivedDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:CompareValidator ID="CVReceivedDate" runat="server" ErrorMessage="<br/>Please enter a valid date."
              Type="Date" ControlToValidate="txtReceivedDate" CssClass="cvPCG" Operator="DataTypeCheck"
              ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtReceivedDate"
               CssClass="rfvPCG" ErrorMessage="<br />Please select an Application received Date" Display="Dynamic"
               runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
               <asp:CompareValidator ID="cvAppRcvDate" runat="server" ControlToValidate="txtReceivedDate" CssClass="cvPCG"
                ErrorMessage="<br />Application Received date must be less than or equal to Today" Operator="LessThanEqual" Type="Date"></asp:CompareValidator>
  </td>
  <td align="right">
<asp:Label ID="Label7" runat="server" Text="Category: "  CssClass="FieldName"></asp:Label>
</td>
<td align="left">
  <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" AutoPostBack="true"
        onselectedindexchanged="ddlCategory_SelectedIndexChanged">  
  </asp:DropDownList>
</td>
  
</tr>
<tr id="trSectionTwo4" runat="server">
<td align="right" valign="top">
  <asp:Label ID="lblApplicationNumber" runat="server" Text="Application Number: "  CssClass="FieldName"></asp:Label>
  </td>
   <td valign="top">
 <asp:TextBox ID="txtApplicationNumber" runat="server" CssClass="txtField"></asp:TextBox><span id="Span1" class="spnRequiredField">*</span>
 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtApplicationNumber"
               CssClass="rfvPCG" ErrorMessage="<br />Please select an Application number" Display="Dynamic"
               runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
  </td>
<td align="right" valign="top"> 
  <asp:Label ID="lblSearchScheme" runat="server" Text="Scheme: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>  
<asp:DropDownList ID="ddlAmcSchemeList" runat="server" CssClass="cmbField" style="width:300px;" AutoPostBack="true"
          onselectedindexchanged="ddlAmcSchemeList_SelectedIndexChanged">
</asp:DropDownList><span id="Span3" class="spnRequiredField">*</span>
<asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlAmcSchemeList" CssClass="cvPCG" Display="Dynamic" 
   ErrorMessage="<br />Please select a scheme" Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
  
   
  </td>

</tr>

<tr id="trSectionTwo5" runat="server">
  <td colspan="2"></td>
  <td align="right" valign="top">
  <asp:Label ID="lblFolioNumber" runat="server" Text="Folio Number: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
   <asp:DropDownList ID="ddlFolioNumber" runat="server" CssClass="cmbField" AutoPostBack="true"
          onselectedindexchanged="ddlFolioNumber_SelectedIndexChanged" >
   </asp:DropDownList>
   <%--<asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlFolioNumber" CssClass="cvPCG" Display="Dynamic" 
   ErrorMessage="<br />Please select a folio" Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>--%>
    </td>
 
</tr>

<tr id="trSectionTwo6" runat="server">
  <td align="right">
  <asp:Label ID="lblOrderDate" runat="server" Text="Order Date: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
  <%--<asp:Label ID="lblGetOrderDate" runat="server" Text=""  CssClass="cmbField"></asp:Label>--%>   
   <asp:TextBox ID="txtOrderDate" runat="server" CssClass="txtField"></asp:TextBox><span id="Span4" class="spnRequiredField">*</span>
           <cc1:CalendarExtender ID="CalendarExtender9" runat="server" TargetControlID="txtOrderDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender9" runat="server"
                TargetControlID="txtOrderDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="<br/>Please enter a valid date."
              Type="Date" ControlToValidate="txtOrderDate" CssClass="cvPCG" Operator="DataTypeCheck"
              ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtOrderDate"
               CssClass="rfvPCG" ErrorMessage="<br />Please select order date" Display="Dynamic"
               runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator> 
             <asp:CompareValidator ID="cvOrderDate" runat="server" ControlToValidate="txtOrderDate" CssClass="cvPCG"
               ErrorMessage="<br />Order date should  be greater than or equal to Today" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>     
  </td>
  <td align="right">
  <asp:Label ID="Label2" runat="server" Text="Portfolio: "  CssClass="FieldName"></asp:Label>
  </td>
  <td align="left">
   <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField"></asp:DropDownList>
  </td>
  
</tr>

<tr id="trSectionTwo7" runat="server">
<td align="right">
  <asp:Label ID="lblOrderNumber" runat="server" Text="Order Number: "  CssClass="FieldName"></asp:Label>
  </td>
   <td align="left">
   <asp:Label ID="lblGetOrderNo" runat="server" Text=""  CssClass="txtField"></asp:Label>
  </td>
  <td align="right">
 <asp:Label ID="Label9" runat="server" Text="Order Status: "  CssClass="FieldName"></asp:Label>
 </td>
 <td align="left">
  <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="cmbField" 
         AutoPostBack="true" 
         onselectedindexchanged="ddlOrderStatus_SelectedIndexChanged">
  </asp:DropDownList>
<%--   <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="ddlOrderStatus" CssClass="cvPCG" Display="Dynamic" 
   ErrorMessage="<br />Please select an order status" Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>--%>
</td>
</tr>


<tr id="trSectionTwo8" runat="server">
  <td align="right">
     <asp:Label ID="lblOrderType" runat="server" Text="Order Type: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
<asp:RadioButton ID="rbtnImmediate" Class="cmbField"  runat="server" AutoPostBack="true" GroupName="OrderDate" Checked="True"  Text="Immediate" OnCheckedChanged="rbtnImmediate_CheckedChanged" />
<asp:RadioButton ID="rbtnFuture" Class="cmbField" runat="server" AutoPostBack="true" GroupName="OrderDate" Text="Future" OnCheckedChanged="rbtnFuture_CheckedChanged" />
</td>
 <td align="right" valign="top">
<asp:Label ID="lblOrderPendingReason" runat="server" Text="Order Pending Reason: "  CssClass="FieldName"></asp:Label>
</td>
<td valign="top">
        <asp:DropDownList ID="ddlOrderPendingReason" runat="server" CssClass="cmbField">
 <%--       <asp:ListItem Text="KYC not completed" Value="KYC not completed" Selected="true"></asp:ListItem>
        <asp:ListItem Text="Product information not adequate/client wants more details" Value="Product information not adequate/client wants more details"></asp:ListItem>
        <asp:ListItem Text="Waiting for market correction/ levels" Value="Waiting for market correction/ levels"></asp:ListItem>
        <asp:ListItem Text="Incomplete information" Value="Incomplete information"></asp:ListItem>
        <asp:ListItem Text="Support documentation not provided" Value="Support documentation not provided"></asp:ListItem>
        <asp:ListItem Text="For want of funds,/post Dated cheque" Value="For want of funds,/post Dated cheque"></asp:ListItem>
        <asp:ListItem Text="Attestation pending (signature verfication)" Value="Attestation pending (signature verfication)"></asp:ListItem>
        <asp:ListItem Text="Signature not done/not tallied" Value="Signature not done/not tallied"></asp:ListItem>--%>
    </asp:DropDownList>
   <%-- <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlOrderPendingReason" CssClass="cvPCG" Display="Dynamic" 
   ErrorMessage="<br />Please select a pending reason" Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>--%>
</td>
</tr>
<tr id="trSectionTwo10" runat="server">
 
 <td  align="right" >
 <asp:Label ID="lblFutureDate" runat="server" Text="Select Future Date: "  CssClass="FieldName"></asp:Label>
 </td>
 <td>
   <asp:TextBox ID="txtFutureDate" runat="server" CssClass="txtField"></asp:TextBox><span id="Span6" class="spnRequiredField">*</span>
           <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFutureDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                TargetControlID="txtFutureDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:CompareValidator ID="CVFutureDate" runat="server" ErrorMessage="<br/>Please enter a valid date."
              Type="Date" ControlToValidate="txtFutureDate" CssClass="cvPCG" Operator="DataTypeCheck"
              ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtFutureDate"
               CssClass="rfvPCG" ErrorMessage="<br />Please select Future Date" Display="Dynamic"
               runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator> 
               <asp:CompareValidator ID="cvFutureDate1" runat="server" ControlToValidate="txtFutureDate" CssClass="cvPCG"
               ErrorMessage="<br />Future date should  be greater than or equal to Today" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator> 
</td>

<td align="right">
<asp:Label ID="lblFutureTrigger" runat="server" Text="Future Trigger: "  CssClass="FieldName"></asp:Label>
</td>
 <td>
 <asp:TextBox ID="txtFutureTrigger" runat="server" CssClass="txtField" TextMode="MultiLine"></asp:TextBox>
</td>

</tr>







<tr id="trPurchase" runat="server">
<td colspan="5">
<hr />
</td>
</tr>


<tr id="trPurchase1" runat="server">

  <td align="right" valign="top">
  <asp:Label ID="lblAmount" runat="server" Text="Amount: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
 <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField"></asp:TextBox><span id="Span5" class="spnRequiredField">*</span>
 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtAmount"
               CssClass="rfvPCG" ErrorMessage="<br />Please select amount" Display="Dynamic"
               runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>  
  <asp:CompareValidator ID="CompareValidator6" ControlToValidate="txtAmount" runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value"
   Type="Double" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
  </td>
  
  <td align="right" valign="top">
  <asp:Label ID="lblMode" runat="server" Text="Mode Of Payment: "  CssClass="FieldName"></asp:Label>
  </td>
  <td valign="top">
  <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="cmbField">
  <asp:ListItem Text="Cheque" Value="CQ" Selected="True"></asp:ListItem>
  <asp:ListItem Text="Draft" Value="DF"></asp:ListItem>
  <asp:ListItem Text="ECS" Value="ES"></asp:ListItem>
  </asp:DropDownList>
<%-- <asp:CheckBox ID="chkCheque" runat="server" CssClass="cmbField" Text="Cheque" Checked="true"  />
<asp:CheckBox ID="chkECS" runat="server" CssClass="cmbField" Text="ECS" Checked="false" />
<asp:CheckBox ID="chkDraft" runat="server" CssClass="cmbField" Text="Draft" Checked="false"/>--%>
</td>
  
</tr>


<tr  id="trPurchase2" runat="server">
  <td align="right" valign="top">
 <asp:Label ID="lblPaymentNumber" runat="server" Text="Payment Instrument Number: "  CssClass="FieldName"></asp:Label>
  </td>
  <td valign="top">
  <asp:TextBox ID="txtPaymentNumber" runat="server" CssClass="txtField"></asp:TextBox>
  </td>
   
  <td  align="right" valign="top">
 <asp:Label ID="Label1" runat="server" Text="Payment Instrument Date: "  CssClass="FieldName"></asp:Label>
 </td>
  <td>
   <asp:TextBox ID="txtPaymentInstDate" runat="server" CssClass="txtField"></asp:TextBox>
           <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtPaymentInstDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server"
                TargetControlID="txtPaymentInstDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:CompareValidator ID="CVPaymentDate" runat="server" ErrorMessage="<br/>Please enter a valid date."
                                    Type="Date" ControlToValidate="txtPaymentInstDate" CssClass="cvPCG" Operator="DataTypeCheck"
                                    ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                                    <asp:CompareValidator ID="cvdate" runat="server" ErrorMessage="<br />Payment Instrument Date should be less than or equal to Order Date"
                            Type="Date" ControlToValidate="txtPaymentInstDate" ControlToCompare="txtOrderDate" Operator="LessThanEqual"
                            CssClass="cvPCG" Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>
</td>


</tr>

<tr id="trPurchase3" runat="server">
  <td align="right">
  <asp:Label ID="lblBankName" runat="server" Text="Bank Name: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
 <%--<asp:TextBox ID="txtBankName" runat="server" CssClass="txtField"></asp:TextBox>--%>
  <asp:DropDownList ID="ddlBankName" runat="server" CssClass="cmbField" >
   </asp:DropDownList>
  </td>
   <td align="right">
  <asp:Label ID="lblBranchName" runat="server" Text="Branch Name: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
 <asp:TextBox ID="txtBranchName" runat="server" CssClass="txtField"></asp:TextBox>
  </td>
</tr>

<tr id="trSipFrequency" runat="server">
<td align="right">
<asp:Label ID="lblFrequencySIP" runat="server" Text="Frequency: "  CssClass="FieldName"></asp:Label>
</td>
<td>
 <asp:DropDownList ID="ddlFrequencySIP" runat="server" CssClass="cmbField" >
   </asp:DropDownList>
</td>
<td colspan="2"></td>
</tr>

<tr id="trSIPDate" runat="server">
<td align="right">
<asp:Label ID="lblStartDateSIP" runat="server" Text="Start Date: "  CssClass="FieldName"></asp:Label>
</td>
<td>
<asp:TextBox ID="txtstartDateSIP" runat="server" CssClass="txtField"></asp:TextBox>
<cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtstartDateSIP" Format="dd/MM/yyyy">
 </cc1:CalendarExtender>
 <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtstartDateSIP" 
 WatermarkText="dd/mm/yyyy"></cc1:TextBoxWatermarkExtender>
<asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="<br/>Please enter a valid date."
 Type="Date" ControlToValidate="txtstartDateSIP" CssClass="cvPCG" Operator="DataTypeCheck"
 ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
</td>
<td align="right">
<asp:Label ID="lblEndDateSIP" runat="server" Text="End Date: "  CssClass="FieldName"></asp:Label>
</td>
<td>
<asp:TextBox ID="txtendDateSIP" runat="server" CssClass="txtField"></asp:TextBox>
<cc1:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtendDateSIP" Format="dd/MM/yyyy">
 </cc1:CalendarExtender>
 <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtendDateSIP" 
 WatermarkText="dd/mm/yyyy"></cc1:TextBoxWatermarkExtender>
<asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="<br/>Please enter a valid date."
 Type="Date" ControlToValidate="txtendDateSIP" CssClass="cvPCG" Operator="DataTypeCheck"
 ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
</td>
</tr>

<tr id="trPurchase4" runat="server">
<td colspan="5">
<hr />
</td>
</tr>

<tr id="trSell1" runat="server">
   <td align="right">
  <asp:Label ID="lblAvailableAmount" runat="server" Text="Available Amount: "  CssClass="FieldName"></asp:Label>
  </td>
   <td align="left">
  <asp:Label ID="lblGetAvailableAmount" runat="server" Text=""  CssClass="txtField"></asp:Label>
  </td>
   
   <td id="tdamount" runat="server" align="right">
  <asp:Label ID="lblAvailableUnits" runat="server" Text="Available Units: "  CssClass="FieldName"></asp:Label>
  </td>
   <td align="left">
  <asp:Label ID="lblGetAvailableUnits" runat="server" Text=""  CssClass="txtField"></asp:Label>
  </td>
 
</tr>


<tr id="trSell2" runat="server">
  <td align="right">
  <asp:Label ID="Label16" runat="server" Text="Redeem/Switch: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
<asp:RadioButton ID="rbtAmount" Class="cmbField"  runat="server" GroupName="AmountUnit" Checked="True"  Text="Amount"/>
<asp:RadioButton ID="rbtUnit" Class="cmbField" runat="server"  GroupName="AmountUnit" Text="Units"/>
</td>

 <td align="right">
   <asp:Label ID="lblAmountUnits" runat="server" Text="Amount/Unit: "  CssClass="FieldName"></asp:Label>
  </td>
 <td>
 <asp:TextBox ID="txtNewAmount" runat="server" CssClass="txtField"></asp:TextBox>
 </td>

</tr>

<tr id="trSell3" runat="server">
  <td align="right">
  <asp:Label ID="lblSchemeSwitch" runat="server" Text="To Scheme: "  CssClass="FieldName"></asp:Label>
  </td>
   <td align="left">
   <asp:DropDownList ID="ddlSchemeSwitch" runat="server" CssClass="cmbField" >
   </asp:DropDownList>
   </td>
<td colspan="2">
</td>
</tr>

<tr id="trfrequencySTP" runat="server">
<td align="right">
<asp:Label ID="lblFrequencySTP" runat="server" Text="Frequency: "  CssClass="FieldName"></asp:Label>
</td>
<td>
 <asp:DropDownList ID="ddlFrequencySTP" runat="server" CssClass="cmbField" >
   </asp:DropDownList>
</td>
<td colspan="2"></td>
</tr>

<tr id="trDateSTP" runat="server">
<td align="right">
<asp:Label ID="lblstartDateSTP" runat="server" Text="Start Date: "  CssClass="FieldName"></asp:Label>
</td>
<td>
<asp:TextBox ID="txtstartDateSTP" runat="server" CssClass="txtField"></asp:TextBox>
<cc1:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtstartDateSTP" Format="dd/MM/yyyy">
 </cc1:CalendarExtender>
 <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" TargetControlID="txtstartDateSTP" 
 WatermarkText="dd/mm/yyyy"></cc1:TextBoxWatermarkExtender>
<asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="<br/>Please enter a valid date."
 Type="Date" ControlToValidate="txtstartDateSTP" CssClass="cvPCG" Operator="DataTypeCheck"
 ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
</td>
<td align="right">
<asp:Label ID="lblendDateSTP" runat="server" Text="End Date: "  CssClass="FieldName"></asp:Label>
</td>
<td>
<asp:TextBox ID="txtendDateSTP" runat="server" CssClass="txtField"></asp:TextBox>
<cc1:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtendDateSTP" Format="dd/MM/yyyy">
 </cc1:CalendarExtender>
 <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" runat="server" TargetControlID="txtendDateSTP" 
 WatermarkText="dd/mm/yyyy"></cc1:TextBoxWatermarkExtender>
<asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="<br/>Please enter a valid date."
 Type="Date" ControlToValidate="txtendDateSTP" CssClass="cvPCG" Operator="DataTypeCheck"
 ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
</td>
</tr>

<tr id="trSell4" runat="server">
<td colspan="5">
<hr />
</td>
</tr>


<%--Current Address--%>
 <tr id="trAddress1" runat="server">
<td colspan="4">
 <asp:Label ID="Label23" CssClass="HeaderTextSmall" runat="server" Text="Current Address"></asp:Label>
</td>
</tr>
   
 <tr id="trAddress2" runat="server">

                            <td class="leftField">
                                <asp:Label ID="lblLine1" CssClass="FieldName" runat="server" Text="Line1(House No./Building):"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:Label ID="lblGetLine1" CssClass="txtField" runat="server" Text=""></asp:Label>                          
                            </td>
                       
                            <td class="leftField">
                                <asp:Label ID="lblLine2" CssClass="FieldName" runat="server" Text="Line2(Street):"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:Label ID="lblGetLine2" CssClass="txtField" runat="server" Text=""></asp:Label>
                            </td>
                        
</tr>
  
 <tr id="trAddress3" runat="server">
                            <td class="leftField">
                                <asp:Label ID="lblLine3" CssClass="FieldName" runat="server" Text="Line3(Area):"></asp:Label>
                            </td>
                            <td class="rightField">
                                 <asp:Label ID="lblGetline3" CssClass="txtField" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="lblLivingSince" CssClass="FieldName" runat="server" Text="Living Since:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:Label ID="lblGetLivingSince" CssClass="txtField" runat="server" Text=""></asp:Label>
                            </td>
   </tr>
   
 <tr id="trAddress4" runat="server">
                            <td class="leftField">
                                <asp:Label ID="lblCity" CssClass="FieldName" runat="server" Text="City:"></asp:Label>
                            </td>
                            <td class="rightField">
                             <asp:Label ID="lblgetCity" CssClass="txtField" runat="server" Text=""></asp:Label>                               
                            </td>
                            <td class="leftField">
                                <asp:Label ID="lblstate" CssClass="FieldName" runat="server" Text="State:"></asp:Label>
                            </td>
                            
                            <td class="rightField">
                             <asp:Label ID="lblGetstate" CssClass="FieldName" runat="server" Text=""></asp:Label>                             
                            </td>
   </tr>
   
 <tr id="trAddress5" runat="server">
                            <td class="leftField">
                                <asp:Label ID="lblPin" CssClass="FieldName" runat="server" Text="Pin Code:"></asp:Label>
                            </td>
                            <td class="rightField">
                               <asp:Label ID="lblGetPin" CssClass="txtField" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="lblCountry" CssClass="FieldName" runat="server" Text="Country:"></asp:Label>
                            </td>
                            <td class="rightField" >
                              <asp:Label ID="lblGetCountry" CssClass="txtField" runat="server" Text=""></asp:Label>
                            </td>
  </tr>

<%--New Address--%>

 <tr id="trAddress6" runat="server">
<td colspan="4">
 <asp:Label ID="Label18" CssClass="HeaderTextSmall" runat="server" Text="New Address"></asp:Label>
</td>
</tr>

 <tr id="trAddress7" runat="server">

                            <td class="leftField">
                                <asp:Label ID="lblAdrLine1" CssClass="FieldName" runat="server" Text="Line1(House No./Building):"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtCorrAdrLine1" runat="server" CssClass="txtField"></asp:TextBox>                              
                            </td>
                       
                            <td class="leftField">
                                <asp:Label ID="lblAdrLine2" CssClass="FieldName" runat="server" Text="Line2(Street):"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtCorrAdrLine2" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                        
</tr>

 <tr id="trAddress8" runat="server">
                            <td class="leftField">
                                <asp:Label ID="lblAdrLine3" CssClass="FieldName" runat="server" Text="Line3(Area):"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtCorrAdrLine3" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="lblResidenceLivingDate" CssClass="FieldName" runat="server" Text="Living Since:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtLivingSince" runat="server" CssClass="txtField"></asp:TextBox>
                               <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtLivingSince"
                                    Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server"
                                    TargetControlID="txtLivingSince" WatermarkText="dd/mm/yyyy">
                                    </cc1:TextBoxWatermarkExtender>
                                <asp:CompareValidator ID="txtLivingSince_CompareValidator" runat="server" ErrorMessage="<br/>Please enter a valid date."
                                    Type="Date" ControlToValidate="txtLivingSince" CssClass="cvPCG" Operator="DataTypeCheck"
                                    ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                            </td>
   </tr>
   
 <tr id="trAddress9" runat="server">
                            <td class="leftField">
                                <asp:Label ID="lblAdrCity" CssClass="FieldName" runat="server" Text="City:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtCorrAdrCity" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="lblAdrState" CssClass="FieldName" runat="server" Text="State:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:DropDownList ID="ddlCorrAdrState" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                            </td>
   </tr>
   
 <tr id="trAddress10" runat="server">
                            <td class="leftField">
                                <asp:Label ID="lblAdrPinCode" CssClass="FieldName" runat="server" Text="Pin Code:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtCorrAdrPinCode" runat="server" CssClass="txtField" MaxLength="6"></asp:TextBox>
                                <asp:CompareValidator ID="txtCorrAdrPinCode_comparevalidator" ControlToValidate="txtCorrAdrPinCode"
                                    runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value"
                                    Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="lblAdrCountry" CssClass="FieldName" runat="server" Text="Country:"></asp:Label>
                            </td>
                            <td class="rightField" >
                                <asp:DropDownList ID="ddlCorrAdrCountry" runat="server" CssClass="cmbField">
                                    <asp:ListItem Text="India" Value="India" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
  </tr>
   

<tr id="trBtnSubmit" runat="server">
<td  align="left">
<asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" OnClick="btnSubmit_OnClick" ValidationGroup="MFSubmit"/>
<asp:Button ID="btnAddMore" runat="server" Text="AddMore" ValidationGroup="MFSubmit"
        CssClass="PCGMediumButton" onclick="btnAddMore_Click" />

</td>

<td colspan="2">
<asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="PCGButton"  ValidationGroup="MFSubmit" onclick="btnUpdate_Click"/>
</td>
</tr>
</table>
<asp:HiddenField ID="hdnCustomerId" runat="server" />
<asp:HiddenField ID="txtCustomerId" runat="server"  onvaluechanged="txtCustomerId_ValueChanged" />
<asp:HiddenField ID="hdnSchemeCode" runat="server" />
<asp:HiddenField ID="hdnType" runat="server" />