<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderMIS.ascx.cs" Inherits="WealthERP.OPS.OrderMIS" %>

<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

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
         $(".flip").click(function() { $(".panel").slideToggle(); });
    });
</script>
<script language="javascript" type="text/javascript">
    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var totalChkBoxes = parseInt('<%= gvMIS.Rows.Count %>');
        var gvControl = document.getElementById('<%= gvMIS.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "cbRecons";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkBxWerpAll");

        //get an array of input types in the gridview
        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            //if the input type is a checkbox and the id of it is what we set above
            //then check or uncheck according to the main checkbox in the header template
            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    }
    </script>

<%--<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    };

    function GetSchemeCode(source, eventArgs) {

        document.getElementById("<%= txtSchemeCode.ClientID %>").value = eventArgs.get_value();

        return false;
    };
</script>--%>

<script type="text/javascript" language="javascript">

    function setFormat(format) {
        document.getElementById('<%= hdnDownloadFormat.ClientID %>').value = format;
    }
    function DownloadScript() {
        if (document.getElementById('<%= gvMIS.ClientID %>') == null) {
            alert("No records to export");
            return false;
        }
        btn = document.getElementById('<%= btnExportExcel.ClientID %>');
        btn.click();

    }
    function setPageType(pageType) {
        document.getElementById('<%= hdnDownloadPageType.ClientID %>').value = pageType;

    }
    function AferExportAll(btnID) {
        var btn = document.getElementById(btnID);
        btn.click();
    }
    function Print_Click(div, btnID) {
        var ContentToPrint = document.getElementById(div);
        var myWindowToPrint = window.open('', '', 'width=200,height=100,toolbar=0,scrollbars=0,status=0,resizable=0,location=0,directories=0');
        myWindowToPrint.document.write(document.getElementById(div).innerHTML);
        myWindowToPrint.document.close();
        myWindowToPrint.focus();
        myWindowToPrint.print();
        myWindowToPrint.close();

        var btn = document.getElementById(btnID);
        btn.click();
    }
</script>

<table style="width: 100%;">
    <tr>
        <td class="HeaderTextBig" colspan="2">
        <img src="../Images/helpImage.png" height="25px" width="25px" style="float: right;"
                class="flip" />
            <asp:Label ID="lblOrderMIS" runat="server" CssClass="HeaderTextBig" Text="Order MIS"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <div class="panel">
                <p>
                    1.View various orders.<br />
                    2.Match orders to the receive transactions.
                </p>
            </div>
        </td>
    </tr>
    <tr>
        <td  class="style12" align="right">
            <asp:ImageButton ID="imgBtnExport" ImageUrl="~/Images/Export_Excel.png" runat="server"
                AlternateText="Excel" ToolTip="Export To Excel" OnClick="imgBtnExport_Click"
                OnClientClick="setFormat('excel')" CausesValidation="false" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
                TargetControlID="imgBtnExport" DynamicServicePath="" BackgroundCssClass="modalBackground"
                Enabled="True" OkControlID="btnOK" CancelControlID="btnCancel" Drag="true" OnOkScript="DownloadScript();"
                PopupDragHandleControlID="Panel1" X="280" Y="35">
            </cc1:ModalPopupExtender>
        </td>
    </tr>
    <tr id="Tr1" runat="server">
        <td>
        <asp:Panel ID="Panel1" runat="server" Width="208px" Height="112px" BackColor="Wheat"
                BorderColor="AliceBlue" Font-Bold="true" ForeColor="Black">
                <%--<br />
                &nbsp;&nbsp;
                <input id="rbtnSin" runat="server" name="Radio" onclick="setPageType('single')" type="radio" />
                <label for="rbtnSin" style="font-family: Times New Roman; font-size: medium; font-stretch: wider;
                    font-weight: 500">Current Page</label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />--%>
                &nbsp;&nbsp;
                <input id="Radio1" runat="server" name="Radio" onclick="setPageType('multiple')"
                    type="radio" />
                <label for="Radio1" style="font-family: Times New Roman; font-size: medium; font-stretch: wider;
                    font-weight: 500">All Pages</label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                <br />
                <div align="center">
                    <asp:Button ID="btnOk" runat="server" Text="OK" CssClass="PCGButton" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="PCGButton" />
                </div>
            </asp:Panel>

            <asp:Button class="ExportButton" ID="btnExportExcel" runat="server" Style="display: none"
                OnClick="btnExportExcel_Click" Height="31px" Width="35px" />
        </td>
    </tr>
</table>
<table width="80%">
<tr>
  <td align="right">
  <asp:Label ID="lblBranch" runat="server" Text="Select The Branch: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
  <asp:DropDownList ID="ddlBranch" runat="server" CssClass="cmbField" AutoPostBack="true"
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
  <asp:Label ID="lblTransactionType" runat="server" Text="Transaction Type: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
  <asp:DropDownList ID="ddlTrxType" runat="server" CssClass="cmbField">
   <asp:ListItem Text="All" Value="All" Selected="true"></asp:ListItem>
        <asp:ListItem Text="New Purchase" Value="BUY"></asp:ListItem>
        <asp:ListItem Text="Additional Purchase" Value="ABY"></asp:ListItem>
        <asp:ListItem Text="Redemption" Value="Sel"></asp:ListItem>
        <asp:ListItem Text="SIP" Value="SIP"></asp:ListItem>
        <asp:ListItem Text="SWP" Value="SWP"></asp:ListItem>
        <asp:ListItem Text="STP" Value="STP"></asp:ListItem>
        <asp:ListItem Text="Switch" Value="SWB"></asp:ListItem>
        <asp:ListItem Text="Change Of Address Form" Value="CAF"></asp:ListItem>
  </asp:DropDownList>

<%--        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="cmbField">
        <asp:ListItem Text="New Purchase" Value="Select" Selected="true"></asp:ListItem>
        <asp:ListItem Text="Additional Purchase" Value="Additional Purchase"></asp:ListItem>
        <asp:ListItem Text="Sell" Value="Sell"></asp:ListItem>
        <asp:ListItem Text="SIP" Value="SIP"></asp:ListItem>
        <asp:ListItem Text="SWP" Value="SWP"></asp:ListItem>
        <asp:ListItem Text="STP" Value="STP"></asp:ListItem>
        <asp:ListItem Text="Switch" Value="Switch"></asp:ListItem>
        <asp:ListItem Text="Change Of Bank Details" Value="Change Of Bank Details"></asp:ListItem>
        <asp:ListItem Text="Change Of Address Form" Value="Change Of Address Form"></asp:ListItem>
        <asp:ListItem Text="ConsoliDation Of Folio" Value="ConsoliDation Of Folio"></asp:ListItem>
        <asp:ListItem Text="Change Of Nominee" Value="Change Of Nominee"></asp:ListItem>
    </asp:DropDownList>--%>
    <%--<asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToValidate="ddlTrxType" CssClass="cvPCG" Display="Dynamic" 
                        ErrorMessage="<br />Please select a transaction type" Operator="NotEqual" 
                        ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>--%>  
  </td>
<%--<td align="right">
<asp:Label ID="lblAssetType" runat="server" Text="Asset Type:"  CssClass="FieldName"></asp:Label>
</td>
<td>
     <asp:DropDownList ID="ddlAssetType" runat="server" CssClass="cmbField">
    </asp:DropDownList>
</td>--%>
</tr>

<%--<tr>
  <td align="right">
  <asp:Label ID="lblCustomer" runat="server" Text="Select The Customer: "  CssClass="FieldName"></asp:Label>
  </td>
  <td align="left">
    <asp:HiddenField ID="txtCustomerId" runat="server"  Visible="true" 
            onvaluechanged="txtCustomerId_ValueChanged" />
   <asp:TextBox ID="txtCustomerName" runat="server" CssClass="txtField" AutoPostBack="true"
          ontextchanged="txtCustomerName_TextChanged"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="txtCustomerName_water" TargetControlID="txtCustomerName" WatermarkText="Enter few chars of Customer"
     runat="server" EnableViewState="false">
     </cc1:TextBoxWatermarkExtender>
     <ajaxToolkit:AutoCompleteExtender ID="txtCustomerName_autoCompleteExtender" runat="server"
     TargetControlID="txtCustomerName" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
     MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
      CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
     CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
     UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters="" Enabled="True"  />
   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCustomerName"
                                         CssClass="rfvPCG" ErrorMessage="<br />Please select a customer" Display="Dynamic"
                                            runat="server" InitialValue="" ValidationGroup="MFSubmit">
                                  </asp:RequiredFieldValidator>
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
           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtReceivedDate"
                                         CssClass="rfvPCG" ErrorMessage="<br />Please select a Application receive Date" Display="Dynamic"
                                            runat="server" InitialValue="" ValidationGroup="MFSubmit">
                                  </asp:RequiredFieldValidator>
 
  </td>
</tr>--%>

<tr>
   <td align="right">
  <asp:Label ID="lblOrderStatus" runat="server" Text="Order Status: "  CssClass="FieldName"></asp:Label>
  </td>
  
  <td>
        <asp:DropDownList ID="ddlMISOrderStatus" runat="server" CssClass="cmbField" 
            onselectedindexchanged="ddlMISOrderStatus_SelectedIndexChanged">
        <%--<asp:ListItem Text="Pending" Value="Pending" Selected="true"></asp:ListItem>
        <asp:ListItem Text="Executed" Value="Executed"></asp:ListItem>
        <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>
        <asp:ListItem Text="Rejected" Value="Reject"></asp:ListItem>--%>
        </asp:DropDownList>
        <%--<asp:CompareValidator ID="CompareValidator2" runat="server" 
                        ControlToValidate="ddlMISOrderStatus" CssClass="cvPCG" Display="Dynamic" 
                        ErrorMessage="<br />Please select order status" Operator="NotEqual" 
                        ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>--%>
  </td>
  
    <td align="right">
     <asp:Label ID="lblOrderType" runat="server" Text="Order Type: "  CssClass="FieldName"></asp:Label>
    </td>
      <td>
        <asp:DropDownList ID="ddlOrderType" runat="server" CssClass="cmbField">
        <asp:ListItem Text="Immediate" Value="1" Selected="true"></asp:ListItem>
        <asp:ListItem Text="Future" Value="0"></asp:ListItem>
        </asp:DropDownList>
  </td>
<%--    <td align="right">
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
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtOrderDate"
                                         CssClass="rfvPCG" ErrorMessage="<br />Please select a order Date" Display="Dynamic"
                                            runat="server" InitialValue="" ValidationGroup="MFSubmit">
                                  </asp:RequiredFieldValidator>
  </td>--%>
  <td align="right">
  <asp:Label ID="lblAMC" runat="server" Text="AMC: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
        <asp:DropDownList ID="ddlAMC" runat="server" CssClass="cmbField">     
        </asp:DropDownList>
  </td>
</tr>

<%--<tr>
 <td align="right">
  <asp:Label ID="Label2" runat="server" Text="Portfolio: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
 <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField">
   </asp:DropDownList>
  </td>
    
     <td align="right">
  <asp:Label ID="lblSearchScheme" runat="server" Text="Search Scheme: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
  <asp:HiddenField ID="txtSchemeCode" runat="server"  />
         <asp:TextBox ID="txtSchemeName" runat="server" CssClass="txtField" 
          AutoComplete="Off" AutoPostBack="true" 
          ontextchanged="txtSchemeName_TextChanged" >
         </asp:TextBox>
         <cc1:TextBoxWatermarkExtender ID="txtSchemeName_TextBoxWatermarkExtender" runat="server" TargetControlID="txtSchemeName" 
         WatermarkText="Type the Scheme Name"></cc1:TextBoxWatermarkExtender>
         <ajaxToolkit:AutoCompleteExtender ID="txtSchemeName_autoCompleteExtender" runat="server"
         TargetControlID="txtSchemeName" ServiceMethod="GetSchemeList" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
         MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="5" CompletionInterval="100"
         CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
         CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"  UseContextKey="true" OnClientItemSelected="GetSchemeCode" />
         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtSchemeName"
                                         CssClass="rfvPCG" ErrorMessage="<br />Please select Scheme" Display="Dynamic"
                                            runat="server" InitialValue="" ValidationGroup="MFSubmit">
                                  </asp:RequiredFieldValidator>
  </td>
  
 <td align="right">
  <asp:Label ID="lblFolioNumber" runat="server" Text="Folio Number: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
   <asp:DropDownList ID="ddlFolioNumber" runat="server" CssClass="cmbField" >
  </asp:DropDownList>
  <asp:CompareValidator ID="CompareValidator3" runat="server" 
                        ControlToValidate="ddlFolioNumber" CssClass="cvPCG" Display="Dynamic" 
                        ErrorMessage="<br />Please select a folio" Operator="NotEqual" 
                        ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
  </td>
</tr>--%>

<tr>
<td align="right" valign="top">
 <asp:Label ID="lblFrom" runat="server" Text=" Order FromDate: "  CssClass="FieldName"></asp:Label>
</td>
<td>
 <asp:TextBox ID="txtFrom" runat="server" CssClass="txtField" >
         </asp:TextBox>
         <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFrom"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                TargetControlID="txtFrom" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="rvFromdate" ControlToValidate="txtFrom" CssClass="rfvPCG" ErrorMessage="<br />Please select a  Date" Display="Dynamic"
               runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
</td>
<td align="right" valign="top">
 <asp:Label ID="lblTo" runat="server" Text="Order ToDate: "  CssClass="FieldName"></asp:Label>
</td>
<td>
 <asp:TextBox ID="txtTo" runat="server" CssClass="txtField" ></asp:TextBox>
         <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTo"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                TargetControlID="txtTo" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="rvtoDate" ControlToValidate="txtTo" CssClass="rfvPCG" ErrorMessage="<br />Please select a  Date" Display="Dynamic"
               runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
               <asp:CompareValidator ID="cvtodate" runat="server" ErrorMessage="<br />To Date should not less than From Date"
                            Type="Date" ControlToValidate="txtTo" ControlToCompare="txtFrom" Operator="GreaterThanEqual"
                            CssClass="cvPCG" Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>
</td>
<td colspan="2"></td>
</tr>

<tr>
<td colspan="2" align="left">
<asp:Button ID="btnGo" runat="server" Text="GO" CssClass="PCGButton" ValidationGroup="MFSubmit"
        onclick="btnGo_Click" />
</td>
<td colspan="2">&nbsp;&nbsp;</td>
<td colspan="2" align="left">

</td>
</tr>


</table>
<table width="100%">
 <tr id="trPager" runat="server">
        <td align="right">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
 </tr>
</table>
<asp:Panel ID="tbgvMIS" runat="server" class="Landscape" Width="100%" Height="80%"  ScrollBars="Both">
<table width="100%">
  <tr>
    <td>
  &nbsp;
  </td>
    <td>
        <asp:GridView ID="gvMIS" CssClass="GridViewStyle" 
        DataKeyNames="CMOT_MFOrderId,C_CustomerId,CP_portfolioId,PASP_SchemePlanCode,CMFA_AccountId,WMTT_TransactionClassificationCode,CMOT_Amount,CMOT_OrderDate"  
        runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" OnRowDataBound="gvMIS_RowDataBound">
                                        <RowStyle CssClass="RowStyle" />
                                        <AlternatingRowStyle CssClass="AltRowStyle" />                                        
                                        <Columns>
                                         <asp:TemplateField HeaderText="Select">
                                         <HeaderTemplate>
                                         <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                                         <input id="chkBxWerpAll" name="chkBxWerpAll" type="checkbox" onclick="checkAllBoxes()" />
                                         </HeaderTemplate>
                                            <ItemTemplate>
                                              <asp:CheckBox ID="cbRecons" runat="server" Checked="false" />
                                            </ItemTemplate>
                                            <%-- <HeaderTemplate>
                                                <input id="cbRecons" type="Select" />
                                            </HeaderTemplate>--%>
                                        </asp:TemplateField>                                 
                                            
                                      <asp:TemplateField HeaderText="OrderNumber" HeaderStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkOrderId" runat="server" CssClass="cmbField" Text='<%# Eval("CMOT_OrderNumber") %>' OnClick="lnkOrderId_Click">
                                            </asp:LinkButton>
                                        </ItemTemplate>                                      
                                     </asp:TemplateField>
                                     
                                     <asp:BoundField DataField="Customer_Name" HeaderText="Customer" ItemStyle-Wrap="false">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                            
                                      <asp:BoundField DataField="XS_Status" HeaderText="Order Status" ItemStyle-Wrap="false">
                                        <HeaderStyle HorizontalAlign="left" />
                                         <ItemStyle HorizontalAlign="left"></ItemStyle>
                                       </asp:BoundField>
                                       
                                            <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center">
                                           <HeaderTemplate>
                                               <asp:Label ID="lblOrderTypeHeader" runat="server" Text="Order Type" ></asp:Label>                  
                                           </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblOrderType" runat="server" Text='<%#Eval("CMOT_IsImmediate").ToString() %>'> </asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle Wrap="False" />
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
                                            
                                               <asp:BoundField DataField="CMFT_TransactionNumber" ItemStyle-Wrap="false" HeaderText="Transaction Number">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            
                                            <%--<asp:BoundField DataField="CMOT_IsImmediate" HeaderText="Order Type">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>--%>
                                       
                                             
                                            
                                            <asp:BoundField DataField="CMOT_ApplicationReceivedDate" HeaderText="Applicateion receive date" ItemStyle-Wrap="false"  DataFormatString="{0:d}" >
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CMOT_OrderDate" HeaderText="Order date" ItemStyle-Wrap="false"  DataFormatString="{0:d}" >
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="XSR_StatusReason" HeaderText="Pending/Reject reason" ItemStyle-Wrap="false">
                                                <HeaderStyle HorizontalAlign="left" />
                                                <ItemStyle HorizontalAlign="left"></ItemStyle>
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="ADUL_ProcessId" HeaderText="Process ID" ItemStyle-Wrap="false">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            
                                             <asp:BoundField DataField="CMOT_ApplicationNumber" HeaderText="Application Number" ItemStyle-Wrap="false">
                                                <HeaderStyle HorizontalAlign="left" />
                                                <ItemStyle HorizontalAlign="left"></ItemStyle>
                                            </asp:BoundField>  
                                         <%--<asp:BoundField DataField="CMOT_MFOrderId" HeaderText="Id" Visible="false">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>--%>
                                            <%--<asp:BoundField DataField="Assetclass" HeaderText="Asset class">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>--%>
                                            
                                             <asp:BoundField DataField="AB_BranchName" HeaderText="Branch" ItemStyle-Wrap="false">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                            
                                             <asp:BoundField DataField="RM_Name" HeaderText="RM" ItemStyle-Wrap="false">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Wrap="false"></ItemStyle>
                                            </asp:BoundField>
                                            
                                           
                                            <asp:BoundField DataField="CMFA_FolioNum" HeaderText="Folio No" ItemStyle-Wrap="false">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PASP_SchemePlanName" HeaderText="Scheme" ItemStyle-Wrap="false">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" Wrap="false"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="WMTT_TransactionClassificationCode" HeaderText="Type" ItemStyle-Wrap="false">
                                                <HeaderStyle HorizontalAlign="left" />
                                                <ItemStyle HorizontalAlign="left"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CMFT_TransactionDate" HeaderText="Transaction Date" ItemStyle-Wrap="false"  DataFormatString="{0:d}" >
                                                <HeaderStyle HorizontalAlign="center" />
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CMFT_Price" HeaderText="Price (Rs)" ItemStyle-Wrap="false" DataFormatString="{0:n}" >
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CMFT_Units" HeaderText="Units" ItemStyle-Wrap="false" DataFormatString="{0:n}" >
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CMOT_Amount" HeaderText="Amount (Rs)" ItemStyle-Wrap="false" DataFormatString="{0:n}" >
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

  </td>
  </tr>
                             
</table>

</asp:Panel>
<table style="width: 100%" id="tblPager" runat="server" visible="false">
    <tr>
        <td>
            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
        </td>
    </tr>
</table>
  <table width="50%">
  <tr> 
    <td>
    <asp:Button ID="btnSync" runat="server" Text="Auto Match" CssClass="PCGMediumButton" 
        onclick="btnSync_Click" />
    </td>
     <td> 
    <asp:Button ID="btnMannualMatch"  runat="server" Text="Manual Match" CssClass="PCGMediumButton"
             onclick="btnMannualMatch_Click" />
    </td>
<%--    <td> 
    <asp:Button ID="btnSubmit" CssClass="PCGButton" runat="server" Text="Submit" onclick="btnSubmit_Click" />
    </td>--%>
    
  </tr> 
</table>
<table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server" visible="false">
    <tr>
    <td align="center">
    <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
    </div>
    </td>
    </tr>
 </table>
<asp:HiddenField id="hdnBranchId" runat="server"/>
<asp:HiddenField id="hdnRMId" runat="server"/>
<%--<asp:HiddenField id="hdnAssetType" runat="server"/>
<asp:HiddenField id="hdnPortFolio" runat="server"/>--%>
<asp:HiddenField id="hdnTransactionType" runat="server"/>
<%--<asp:HiddenField id="hdnARDate" runat="server"/>--%>
<asp:HiddenField id="hdnOrdStatus" runat="server"/>
<asp:HiddenField id="hdnOrderType" runat="server"/>
<%--<asp:HiddenField id="hdnOrderDate" runat="server"/>--%>
<%--<asp:HiddenField id="hdnCustomerId" runat="server"/>
<asp:HiddenField id="hdnScheme" runat="server"/>
<asp:HiddenField id="hdnFolio" runat="server"/>--%>
<asp:HiddenField id="hdnamcCode" runat="server"/>
<asp:HiddenField id="hdnFromdate" runat="server"/>
<asp:HiddenField id="hdnTodate" runat="server"/>
<asp:HiddenField ID="hdnDownloadFormat" runat="server" Visible="true" />
<asp:HiddenField ID="hdnDownloadPageType" runat="server" Visible="true" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />





