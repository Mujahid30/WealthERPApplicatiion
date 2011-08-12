<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderEntry.ascx.cs" Inherits="WealthERP.OPS.OrderEntry" %>
<%@ Register Src="../General/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    };

    function GetSchemeCode(source, eventArgs) {

        document.getElementById("<%= txtSchemeCode.ClientID %>").value = eventArgs.get_value();

        return false;
    };
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
<asp:Label ID="lblOrderEntry" runat="server" CssClass="HeaderTextBig" Text="Order Entry"></asp:Label>
<br />
<hr />
<telerik:RadToolBar ID="aplToolBar" runat="server" Skin="Telerik" EnableEmbeddedSkins="false" EnableShadows="true" EnableRoundedCorners="true"
    Width="100%"  OnButtonClick="aplToolBar_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ID="btnEdit" runat="server" Text="Edit" Value="Edit" ImageUrl="~/Images/Telerik/EditButton.gif"
            ImagePosition="Left" ToolTip="Edit">            
        </telerik:RadToolBarButton>
        
    </Items>
</telerik:RadToolBar>
<br />
<br />
<table width="70%">
<tr>
  <td align="right">
  <asp:Label ID="lblOrderNumber" runat="server" Text="Order Number: "  CssClass="FieldName"></asp:Label>
  </td>
  <td align="left">
  <asp:TextBox ID="txtOrederNumber" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
  </td>
  <td align="right">
  <asp:Label ID="lblOrderDate" runat="server" Text="Order Date: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
  <asp:TextBox ID="txtOrderDate" runat="server" CssClass="txtField"></asp:TextBox>
           <cc1:CalendarExtender ID="txtOrderDate_CalendarExtender" runat="server" TargetControlID="txtOrderDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtOrderDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtOrderDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
  </td>
</tr>

<tr>
  <td align="right">
  <asp:Label ID="lblBranch" runat="server" Text="Select The Branch: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
  <asp:DropDownList ID="ddlBranch" runat="server" CssClass="cmbField" 
          onselectedindexchanged="ddlBranch_SelectedIndexChanged" >
  </asp:DropDownList>
  </td>
  <td align="right">
  <asp:Label ID="lblRM" runat="server" Text="Select the RM: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
   <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField" >
  </asp:DropDownList>
  </td>
</tr>

<tr>
  <td align="right">
  <asp:Label ID="lblCustomer" runat="server" Text="Select The Customer: "  CssClass="FieldName"></asp:Label>
  </td>
  <td align="left">
    <asp:HiddenField ID="txtCustomerId" runat="server"  Visible="true" 
            onvaluechanged="txtCustomerId_ValueChanged" />
   <asp:TextBox ID="txtCustomerName" runat="server" CssClass="txtField"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="txtCustomerName_water" TargetControlID="txtCustomerName" WatermarkText="Enter few chars of Customer"
     runat="server" EnableViewState="false">
     </cc1:TextBoxWatermarkExtender>
     <ajaxToolkit:AutoCompleteExtender ID="txtCustomerName_autoCompleteExtender" runat="server"
     TargetControlID="txtCustomerName" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
     MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
      CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
     CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
     UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters="" Enabled="True"  />
    </td>
  <td>
  &nbsp;
  </td>
  <td>
     <asp:Button ID="btnAddCustomer" runat="server" Text="Add a New Customer" CssClass="PCGButton" CausesValidation="false" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_OrderEntry_btnAddCustomer','L');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_OrderEntry_btnAddCustomer','L');" />
  </td>
</tr>

<tr>
  <td align="right">
  <asp:Label ID="ddlTransactionType" runat="server" Text="Transaction Type: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="cmbField">
        <asp:ListItem Text="New Purchase" Value="Select" Selected="true"></asp:ListItem>
        <asp:ListItem Text="Additional Purchase" Value="Change Of Bank Details"></asp:ListItem>
        <asp:ListItem Text="Sell" Value="Change Of Address Form"></asp:ListItem>
        <asp:ListItem Text="SIP" Value="ConsoliDation Of Folio"></asp:ListItem>
        <asp:ListItem Text="SWP" Value="Change Of Nominee"></asp:ListItem>
        <asp:ListItem Text="STP" Value="Change Of Nominee"></asp:ListItem>
        <asp:ListItem Text="Switch" Value="Change Of Nominee"></asp:ListItem>
        <asp:ListItem Text="Change Of Bank Details" Value="Change Of Bank Details"></asp:ListItem>
        <asp:ListItem Text="Change Of Address Form" Value="Change Of Address Form"></asp:ListItem>
        <asp:ListItem Text="ConsoliDation Of Folio" Value="ConsoliDation Of Folio"></asp:ListItem>
        <asp:ListItem Text="Change Of Nominee" Value="Change Of Nominee"></asp:ListItem>
    </asp:DropDownList>
  </td>
  <td colspan="2">
  &nbsp;
  </td>
</tr>

<tr>
  <td align="right">
  <asp:Label ID="Label2" runat="server" Text="Portfolio: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
   <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" >
  </asp:DropDownList>
  </td>
  <td colspan="2"></td>
</tr>

<tr>
  <td align="right">
  <asp:Label ID="lblFolioNumber" runat="server" Text="Folio Number: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
   <asp:DropDownList ID="ddlFolioNumber" runat="server" CssClass="cmbField" >
  </asp:DropDownList>
  </td>
  <td>
  &nbsp;
  </td>
  <td>
     <asp:Button ID="btnAddFolio" runat="server" Text="Add a New Folio" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_OrderEntry_btnAddFolio','L');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_OrderEntry_btnAddFolio','L');" />
  </td>
</tr>

<tr>
  <td align="right">
  <asp:Label ID="lblSearchScheme" runat="server" Text="Search Scheme: "  CssClass="FieldName"></asp:Label>
  </td>
  <td colspan="2">
  <asp:HiddenField ID="txtSchemeCode" runat="server"  />
         <asp:TextBox ID="txtSchemeName" runat="server" CssClass="txtField" AutoComplete="Off" AutoPostBack="true" style="width:275px;">
         </asp:TextBox>
         <cc1:TextBoxWatermarkExtender ID="txtSchemeName_TextBoxWatermarkExtender" runat="server" TargetControlID="txtSchemeName" 
         WatermarkText="Type the Scheme Name"></cc1:TextBoxWatermarkExtender>
         <ajaxToolkit:AutoCompleteExtender ID="txtSchemeName_autoCompleteExtender" runat="server"
         TargetControlID="txtSchemeName" ServiceMethod="GetSchemeList" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
         MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="5" CompletionInterval="100"
         CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
         CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"  UseContextKey="true" OnClientItemSelected="GetSchemeCode" />
  </td>
  <td>
 
  </td>
</tr>

<tr>
  <td align="right">
  <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
  <asp:TextBox ID="txtTransactionDate" runat="server" CssClass="txtField"></asp:TextBox>
           <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTransactionDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
  </td>
  <td align="right">
  <asp:Label ID="lblReceivedDate" runat="server" Text="Application Received Date: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
  <asp:TextBox ID="txtReceivedDate" runat="server" CssClass="txtField"></asp:TextBox>
           <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtReceivedDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                TargetControlID="txtReceivedDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
  </td>
</tr>

<tr>
  <td align="right">
     <asp:Label ID="lblOrderType" runat="server" Text="Order Type: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
<asp:RadioButton ID="rbtnImmediate" Class="cmbField"  runat="server" AutoPostBack="true"
          GroupName="OrderDate" Checked="True"  Text="Immediate" OnCheckedChanged="rbtnImmediate_CheckedChanged"
           />
<asp:RadioButton ID="rbtnFuture" Class="cmbField" runat="server" AutoPostBack="true"
          GroupName="OrderDate" Text="Future" OnCheckedChanged="rbtnFuture_CheckedChanged"
           />
</td>
  <td align="right">
  <asp:Label ID="lblApplicationNumber" runat="server" Text="Application Number: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
 <asp:TextBox ID="txtApplicationNumber" runat="server" CssClass="txtField"></asp:TextBox>
  </td>
</tr>

<tr>
<td colspan="4"></td>
</tr>

<tr id="trfutureDate" runat="server">
<td  align="right">
 <asp:Label ID="lblFutureDate" runat="server" Text="Select Future Date: "  CssClass="FieldName"></asp:Label>
 </td>
 <td>
   <asp:TextBox ID="txtFutureDate" runat="server" CssClass="txtField"></asp:TextBox>
           <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFutureDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                TargetControlID="txtFutureDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
</td>
<td colspan="2"></td>
</tr>



<tr id="trFutureTrigger" runat="server">
<td align="right">
<asp:Label ID="lblFutureTrigger" runat="server" Text="Future Trigger: "  CssClass="FieldName"></asp:Label>
</td>
<td>
 <asp:TextBox ID="txtFutureTrigger" runat="server" CssClass="txtField" TextMode="MultiLine"></asp:TextBox>
</td>
<td colspan="2"></td>
</tr>

<tr>

  <td align="right">
  <asp:Label ID="lblAmount" runat="server" Text="Amount: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
 <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField"></asp:TextBox>
  </td>
  <td align="right">
   <asp:Label ID="lblUnits" runat="server" Text="Units: "  CssClass="FieldName"></asp:Label>
  </td>
    <td>
 <asp:TextBox ID="txtUnits" runat="server" CssClass="txtField"></asp:TextBox>
  </td>
</tr>

<tr>

  <td align="right" valign="top">
  <asp:Label ID="lblMode" runat="server" Text="Mode Of Payment: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
 <asp:CheckBox ID="chkCheque" runat="server" CssClass="cmbField" Text="Cheque" Checked="true"  />
<asp:CheckBox ID="chkECS" runat="server" CssClass="cmbField" Text="ECS" Checked="false" />
<asp:CheckBox ID="chkDraft" runat="server" CssClass="cmbField" Text="Draft" Checked="false"/>
</td>
  <td align="right" valign="top">
 <asp:Label ID="lblPaymentNumber" runat="server" Text="Payment Instrument Number: "  CssClass="FieldName"></asp:Label>
  </td>
  <td valign="top">
  <asp:TextBox ID="txtPaymentNumber" runat="server" CssClass="txtField"></asp:TextBox>
  </td>
</tr>


<tr>
  <td align="right">
  <asp:Label ID="lblPaymentDetails" runat="server" Text="Payment Details: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
 <asp:TextBox ID="txtPaymentDetails" runat="server" CssClass="txtField"></asp:TextBox>
  </td>
  <td colspan="2">&nbsp;</td>
</tr>

<tr>
  <td align="right">
  <asp:Label ID="lblBankDetails" runat="server" Text="Bank Details: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
 <asp:TextBox ID="txtBankDetails" runat="server" CssClass="txtField"></asp:TextBox>
  </td>
  <td colspan="2">&nbsp;</td>
</tr>

<tr>

<td align="right" valign="top">
<asp:Label ID="Label1" runat="server" Text="Mode Of Payment: "  CssClass="FieldName"></asp:Label>
</td>
<td>
<asp:RadioButton ID="rbtnPending" Class="cmbField"  runat="server" GroupName="ModeOfPayment" Checked="True"  Text="Pending" />
<br />
<asp:RadioButton ID="rbtnExecuted" Class="cmbField"  runat="server" GroupName="ModeOfPayment" Checked="False"  Text="Executed" />
<br />
<asp:RadioButton ID="rbtnCancelled" Class="cmbField"  runat="server" GroupName="ModeOfPayment" Checked="False"  Text="Cancelled" />
<br />
<asp:RadioButton ID="rbtnReject" Class="cmbField"  runat="server" GroupName="ModeOfPayment" Checked="False"  Text="Reject" />
<td align="right" valign="top">
<asp:Label ID="lblOrderPendingReason" runat="server" Text="Order Pending Reason: "  CssClass="FieldName"></asp:Label>
</td>
<td valign="top">
        <asp:DropDownList ID="ddlOrderPendingReason" runat="server" CssClass="cmbField">
        <asp:ListItem Text="KYC not completed" Value="KYC not completed" Selected="true"></asp:ListItem>
        <asp:ListItem Text="Product information not adequate/client wants more details" Value="Product information not adequate/client wants more details"></asp:ListItem>
        <asp:ListItem Text="Waiting for market correction/ levels" Value="Waiting for market correction/ levels"></asp:ListItem>
        <asp:ListItem Text="Incomplete information" Value="Incomplete information"></asp:ListItem>
        <asp:ListItem Text="Support documentation not provided" Value="Support documentation not provided"></asp:ListItem>
        <asp:ListItem Text="For want of funds,/post Dated cheque" Value="For want of funds,/post Dated cheque"></asp:ListItem>
        <asp:ListItem Text="Attestation pending (signature verfication)" Value="Attestation pending (signature verfication)"></asp:ListItem>
        <asp:ListItem Text="Signature not done/not tallied" Value="Signature not done/not tallied"></asp:ListItem>
    </asp:DropDownList>
</td>
</tr>

<tr>
<td colspan="2"></td>
<td colspan="2" align="left">
<asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" OnClick="btnSubmit_OnClick" />
</td>
</tr>
</table>
