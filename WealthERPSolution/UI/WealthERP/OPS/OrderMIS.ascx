<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderMIS.ascx.cs" Inherits="WealthERP.OPS.OrderMIS" %>

<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

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
<asp:Label ID="lblOrderMIS" runat="server" CssClass="HeaderTextBig" Text="Order MIS"></asp:Label>
<br />
<hr />
<br />
<table width="80%">
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
<td align="right">
<asp:Label ID="lblAssetType" runat="server" Text="Asset Type:"  CssClass="FieldName"></asp:Label>
</td>
<td>
     <asp:DropDownList ID="ddlAssetType" runat="server" CssClass="cmbField">
        <asp:ListItem Text="Mutual Fund" Value="Mutual Fund" Selected="true"></asp:ListItem>
        <asp:ListItem Text="Equity" Value="Equity"></asp:ListItem>
        <asp:ListItem Text="Insurance" Value="Insurance"></asp:ListItem>
        <asp:ListItem Text="Loan Application" Value="Loan Application"></asp:ListItem>
        <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
    </asp:DropDownList>
</td>
</tr>

<tr>
 <td align="right">
  <asp:Label ID="Label2" runat="server" Text="Portfolio: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
 <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField">
  <asp:ListItem Text="Managed" Value="1">Managed</asp:ListItem>
   <asp:ListItem Text="UnManaged" Value="0">UnManaged</asp:ListItem>
   </asp:DropDownList>
  </td>
  
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
  <asp:Label ID="lblOrderStatus" runat="server" Text="Order Status: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="cmbField">
        <asp:ListItem Text="Pending" Value="Pending" Selected="true"></asp:ListItem>
        <asp:ListItem Text="Executed" Value="Executed"></asp:ListItem>
        <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>
        <asp:ListItem Text="Reject" Value="Reject"></asp:ListItem>
        </asp:DropDownList>
  </td>
  
    <td align="right">
     <asp:Label ID="lblOrderType" runat="server" Text="Order Type: "  CssClass="FieldName"></asp:Label>
    </td>
      <td>
        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="cmbField">
        <asp:ListItem Text="Immediate" Value="Immediate" Selected="true"></asp:ListItem>
        <asp:ListItem Text="Future" Value="Future"></asp:ListItem>
        </asp:DropDownList>
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
    
     <td align="right">
  <asp:Label ID="lblSearchScheme" runat="server" Text="Search Scheme: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
  <asp:HiddenField ID="txtSchemeCode" runat="server"  />
         <asp:TextBox ID="txtSchemeName" runat="server" CssClass="txtField" AutoComplete="Off" AutoPostBack="true" >
         </asp:TextBox>
         <cc1:TextBoxWatermarkExtender ID="txtSchemeName_TextBoxWatermarkExtender" runat="server" TargetControlID="txtSchemeName" 
         WatermarkText="Type the Scheme Name"></cc1:TextBoxWatermarkExtender>
         <ajaxToolkit:AutoCompleteExtender ID="txtSchemeName_autoCompleteExtender" runat="server"
         TargetControlID="txtSchemeName" ServiceMethod="GetSchemeList" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
         MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="5" CompletionInterval="100"
         CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
         CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"  UseContextKey="true" OnClientItemSelected="GetSchemeCode" />
  </td>
  
 <td align="right">
  <asp:Label ID="lblFolioNumber" runat="server" Text="Folio Number: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
   <asp:DropDownList ID="ddlFolioNumber" runat="server" CssClass="cmbField" >
  </asp:DropDownList>
  </td>
</tr>

<tr>
<td colspan="6">&nbsp;</td>
</tr>

<tr>
<td colspan="2" align="left">
<asp:Button ID="btnGo" runat="server" Text="GO" CssClass="PCGButton" 
        onclick="btnGo_Click" />
</td>
<td colspan="2">&nbsp;</td>
<td colspan="2" align="left">
<asp:Button ID="btnSync" runat="server" Text="Sync" CssClass="PCGButton" />
</td>
</tr>


</table>

<asp:Panel ID="tbgvMIS" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
<table width="100%">
 
 
  <tr>
    <td>
  &nbsp;
  </td>
    <td>
        <asp:GridView ID="gvMIS" CssClass="GridViewStyle" DataKeyNames="Id" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True">
                                        <RowStyle CssClass="RowStyle" />
                                        <AlternatingRowStyle CssClass="AltRowStyle" />
                                        <Columns>
                                         <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                              <asp:CheckBox ID="cbRecons" runat="server" />
                                            </ItemTemplate>
                                            <%-- <HeaderTemplate>
                                                <input id="cbRecons" type="Select" />
                                            </HeaderTemplate>--%>
                                        </asp:TemplateField>                                 
                                            
                                      <asp:TemplateField HeaderText="OrderNumber" HeaderStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkOrderId" runat="server" CssClass="cmbField" Text='<%# Eval("OrderNumber") %>' OnClick="lnkOrderId_Click">
                                            </asp:LinkButton>
                                        </ItemTemplate>                                      
                                     </asp:TemplateField>
                                        
                                        <%--<asp:TemplateField HeaderText="OrderNumber">
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lnkOrderNumber" runat="server" Text='<%# Eval("OrderNumber").ToString() %>'>
                                        </asp:LinkButton>
                                        
                                        </ItemTemplate>
                                        </asp:TemplateField> --%>
                                        <%-- <asp:BoundField DataField="OrderNumber" HeaderText="Order Number">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>--%>
                                            
                                               <asp:BoundField DataField="TransactionNumber" HeaderText="Transaction Number">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="OrderType" HeaderText="Order Type">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                             <asp:BoundField DataField="OrderStatus" HeaderText="Order Status">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="Applicateionreceivedate" HeaderText="Applicateion receive date">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Orderdate" HeaderText="Order date">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Pending/Reject" HeaderText="Pending/Reject reason">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="ProcessID" HeaderText="Process ID">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            
                                             <asp:BoundField DataField="ApplicationNumber" HeaderText="Application Number">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>  
                                         <asp:BoundField DataField="Id" HeaderText="Id" Visible="false">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Assetclass" HeaderText="Asset class">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                            
                                             <asp:BoundField DataField="Branch" HeaderText="Branch">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                            
                                             <asp:BoundField DataField="RM" HeaderText="RM">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                            
                                             <asp:BoundField DataField="Customer" HeaderText="Customer">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="FolioNo" HeaderText="Folio No">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Scheme" HeaderText="Scheme">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" Wrap="true"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Type" HeaderText="Type">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TransactionDate" HeaderText="Transaction Date">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Price" HeaderText="Price (Rs)">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Units" HeaderText="Units">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Amount" HeaderText="Amount (Rs)">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            
                                              
                                 
                                        </Columns>
                                        <EditRowStyle CssClass="EditRowStyle" />
                                        <FooterStyle CssClass="FooterStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    </asp:GridView>
    </td>
    <td>
    &nbsp;&nbsp;
    </td>
  </tr>
  
  <tr>
  <td colspan="3">
  &nbsp;&nbsp;
  </td>
  </tr>
  
  <tr>
    <td colspan="3"> 
    <asp:Button ID="btnSubmit" CssClass="PCGButton" runat="server" Text="Submit" onclick="btnSubmit_Click" />
    </td>
  </tr>                                

</table>
</asp:Panel>





