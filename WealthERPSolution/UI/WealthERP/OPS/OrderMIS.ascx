<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderMIS.ascx.cs" Inherits="WealthERP.OPS.OrderMIS" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= hdnCustomerId.ClientID %>").value = eventArgs.get_value();
        //alert(document.getElementById("<%= hdnCustomerId.ClientID %>").value = eventArgs.get_value());
        return false;
    };

</script>
<script type="text/javascript">
    $(document).ready(function() {
        $(".flip").click(function() { $(".panel").slideToggle(); });
    });

    function keyPress(sender, args) {
        if (args.keyCode == 13) {
            return false;
        }
    }
</script>

<style type="text/css">
   .ajax__calendar_container { z-index : 1000 ; }
</style>

<script language="javascript" type="text/javascript">
    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var totalChkBoxes = parseInt('<%= gvCustomerOrderMIS.Items.Count %>');
        var gvControl = document.getElementById('<%= gvCustomerOrderMIS.ClientID %>');

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


<table style="width: 100%;">
<%-- <tr>
        <td colspan="5">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                <tr>
                    <td align="left">Order MIS</td>
                    
                </tr>
                </table>
            </div>
        </td>
    </tr>
<tr>--%>
<tr>
     <td colspan="5">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                <tr>
                    <td align="left">MF Order Recon</td>
                    <td align="right" style="padding-bottom:2px;">
                       <img src="../Images/helpImage.png" height="20px" width="25px" style="float: right;"
                class="flip" />
                <asp:ImageButton ID="btnMForderRecon" ImageUrl="~/Images/Export_Excel.png"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" Visible="false"
                OnClientClick="setFormat('excel')" Height="25px" Width="25px" 
            onclick="btnMForderRecon_Click"></asp:ImageButton>
                    </td>
                </tr>
                </table>
            </div>
        </td>
    </tr>
<%--    <tr>
        <td class="HeaderTextBig" colspan="2">
            <img src="../Images/helpImage.png" height="25px" width="25px" style="float: right;"
                class="flip" />
            <asp:Label ID="lblOrderMIS" runat="server" CssClass="HeaderTextBig" Text="Order MIS"></asp:Label>
            <hr />
        </td>
    </tr>--%>
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
<%--    <tr>
        <td class="style12" align="left">
            <asp:ImageButton ID="imgBtnExport" ImageUrl="~/Images/Export_Excel.png" runat="server"
                AlternateText="Excel" ToolTip="Export To Excel" OnClick="imgBtnExport_Click"
                OnClientClick="setFormat('excel')" CausesValidation="false" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
                TargetControlID="imgBtnExport" DynamicServicePath="" BackgroundCssClass="modalBackground"
                Enabled="True" OkControlID="btnOK" CancelControlID="btnCancel" Drag="true" OnOkScript="DownloadScript();"
                PopupDragHandleControlID="Panel1" X="280" Y="35">
            </cc1:ModalPopupExtender>
        </td>
    </tr>--%>
    <%--<tr id="Tr1" runat="server">
        <td>
            <asp:Panel ID="Panel1" runat="server" Width="208px" Height="112px" BackColor="Wheat"
                BorderColor="AliceBlue" Font-Bold="true" ForeColor="Black">

                &nbsp;&nbsp;
                <input id="Radio1" runat="server" name="Radio" onclick="setPageType('multiple')"
                    type="radio" />
                <label for="Radio1" style="font-family: Times New Roman; font-size: medium; font-stretch: wider;
                    font-weight: 500">
                    All Pages</label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                <br />
                <div align="center">
                    <asp:Button ID="btnOk" runat="server" Text="OK" CssClass="PCGButton" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="PCGButton" />
                </div>
            </asp:Panel>
            <asp:Button class="ExportButton" ID="btnExportExcel" runat="server" Style="display: none"
                Height="31px" Width="35px" />
        </td>
    </tr>--%>
</table>

<table width="80%" onkeypress="return keyPress(this, event)">
    <tr>
        <td align="right" valign="top">
            <asp:Label ID="lblFrom" runat="server" Text=" Order From Date: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtFrom" runat="server" CssClass="txtField">
            </asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFrom"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtFrom"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="rvFromdate" ControlToValidate="txtFrom" CssClass="rfvPCG"
                ErrorMessage="<br />Please select a  Date" Display="Dynamic" runat="server" InitialValue=""
                ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                
            <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="rfvPCG"
                ControlToValidate="txtFrom" Display="Dynamic" ErrorMessage="Invalid Date" ValidationGroup="MFSubmit"
                Operator="DataTypeCheck" Type="Date">
            </asp:CompareValidator>
        </td>
        <td align="right" valign="top">
            <asp:Label ID="lblTo" runat="server" Text="Order To Date: " CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtTo" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTo"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtTo"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="rvtoDate" ControlToValidate="txtTo" CssClass="rfvPCG"
                ErrorMessage="<br />Please select a Date" Display="Dynamic" runat="server" InitialValue=""
                ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
              
            <%--<asp:RegularExpressionValidator ID="rev1" runat="server" CssClass="rfvPCG" ValidationExpression="[0-9][0-9]/[0-9][0-9]/[0-9][0-9][0-9][0-9]"
            ControlToValidate="txtTo" Display="Dynamic" ErrorMessage="Invalid Date" ValidationGroup="MFSubmit">
            </asp:RegularExpressionValidator>--%>
            
            <asp:CompareValidator ID="CompareValidator2" runat="server" CssClass="rfvPCG"
            ControlToValidate="txtTo" Display="Dynamic" ErrorMessage="Invalid Date" ValidationGroup="MFSubmit"
            Operator="DataTypeCheck" Type="Date">
            </asp:CompareValidator>  
            
            <asp:CompareValidator ID="cvtodate" runat="server" ErrorMessage="<br/>To Date should not less than From Date"
                Type="Date" ControlToValidate="txtTo" ControlToCompare="txtFrom" Operator="GreaterThanEqual" 
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>
        </td>
       <%-- <td colspan="2">
        </td>--%>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblTransactionType" runat="server" Text="Transaction Type: " CssClass="FieldName"></asp:Label>
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
        <td align="right">
            <asp:Label ID="lblAMC" runat="server" Text="AMC: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlAMC" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td align="right">
            <asp:Label ID="lblBranch" runat="server" Text="Select The Branch: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
            </asp:DropDownList>
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
            <asp:Label ID="lblOrderStatus" runat="server" Text="Order Status: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
        <%--<asp:DropDownList ID="ddlMISOrderStatus" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlMISOrderStatus_SelectedIndexChanged">
                <asp:ListItem Text="Open" Value="0" Selected="true"></asp:ListItem>
                <asp:ListItem Text="Closed" Value="1"></asp:ListItem>
            </asp:DropDownList>--%>
            <asp:DropDownList ID="ddlMISOrderStatus" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlMISOrderStatus_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="right">
            <asp:Label ID="lblOrderType" runat="server" Text="Order Type: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlOrderType" runat="server" CssClass="cmbField" >
                <asp:ListItem Text="Immediate" Value="1" Selected="true"></asp:ListItem>
                <asp:ListItem Text="Future" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right">
            <asp:Label ID="lblRM" runat="server" Text="Select the RM: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField">
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
 <td class="leftField">
            <asp:Label ID="lblSelectTypeOfCustomer" runat="server" CssClass="FieldName" Text="Customer Type: "></asp:Label>
        </td>
        <td class="rightField">
            <%--<asp:RadioButton runat="server" ID="rdoPickCustomer" Text="Pick Customer" AutoPostBack="true"
              Class="cmbField" GroupName="SelectCustomer" oncheckedchanged="rdoPickCustomer_CheckedChanged"/>--%>
            <asp:DropDownList ID="ddlCustomerType" Style="vertical-align: middle" runat="server"
                CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomerType_SelectedIndexChanged">
                <asp:ListItem Value="All" Text="All" Selected="True"></asp:ListItem>
                <asp:ListItem Value="0" Text="Group Head"></asp:ListItem>
                <asp:ListItem Value="1" Text="Individual"></asp:ListItem>
            </asp:DropDownList>
        </td>
     
        <td class="leftField" >
            <asp:Label ID="lblselectCustomer" runat="server" CssClass="FieldName" Text="Search Customer: "></asp:Label>
        </td>
       <td align="left" onkeypress="return keyPress(this, event)">
            <asp:TextBox ID="txtIndividualCustomer" runat="server" CssClass="txtField" AutoComplete="Off" Enabled="false"
                AutoPostBack="True">  </asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtIndividualCustomer_water" TargetControlID="txtIndividualCustomer"
                WatermarkText="Enter few chars of Customer" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtIndividualCustomer_autoCompleteExtender"
                runat="server" TargetControlID="txtIndividualCustomer" ServiceMethod="GetCustomerName"
                ServicePath="~/CustomerPortfolio/AutoComplete.asmx" MinimumPrefixLength="1" EnableCaching="False"
                CompletionSetCount="5" CompletionInterval="100" CompletionListCssClass="AutoCompleteExtender_CompletionList"
                CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem" CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" DelimiterCharacters="" OnClientItemSelected="GetCustomerId"
                Enabled="True" />
            <asp:RequiredFieldValidator ID="rquiredFieldValidatorIndivudialCustomer" Display="Dynamic"
                ControlToValidate="txtIndividualCustomer" CssClass="rfvPCG" ErrorMessage="<br />Please select the customer"
                runat="server" ValidationGroup="CustomerValidation">
            </asp:RequiredFieldValidator>
        </td>
</tr>
    <tr>
        <td colspan="2" align="left">
            <asp:Button ID="btnGo" runat="server" Text="GO" CssClass="PCGButton" ValidationGroup="MFSubmit"
                OnClick="btnGo_Click" />
        </td>
        <td colspan="2">
            &nbsp;&nbsp;&nbsp;&nbsp;
        </td>
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
<table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server"
    visible="false">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
            </div>
        </td>
    </tr>
</table>
<asp:Panel ID="tbgvMIS" runat="server" class="Landscape" Width="100%" Height="80%"
    ScrollBars="Both">
    <table width="100%">
        <tr>
            <td>
                <%--<asp:GridView ID="gvMIS" CssClass="GridViewStyle" DataKeyNames="CO_OrderId,CMFOD_OrderDetailsId,C_CustomerId,CP_portfolioId,PASP_SchemePlanCode,CMFA_AccountId,WMTT_TransactionClassificationCode,CMFOD_Amount,CO_OrderDate,PASP_SchemePlanSwitch"
                    runat="server" AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="gvMIS_RowDataBound"
                    OnRowCommand="gvMIS_RowCommand">
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
                        </asp:TemplateField>

                        <asp:ButtonField DataTextField="CMFOD_OrderNumber" CommandName="ViewOrder" ButtonType="Link"
                            HeaderText="Number" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="CO_OrderDate" HeaderText="Order date" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false" DataFormatString="{0:d}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                       
                        <asp:BoundField DataField="Customer_Name" HeaderText="Customer" ItemStyle-Wrap="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="WMTT_TransactionClassificationName" HeaderText="Trans Type"
                            HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="XS_Status" HeaderText="Order Status"
                            HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Left">
                            <HeaderTemplate>
                                <asp:Label ID="lblOrderTypeHeader" runat="server" Text="Order Type"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblOrderType" runat="server" Text='<%#Eval("CMFOD_IsImmediate").ToString() %>'> </asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="CMFA_FolioNum" HeaderText="Folio" ItemStyle-Wrap="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PASP_SchemePlanName" HeaderText="Scheme" ItemStyle-Wrap="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false"></ItemStyle>
                        </asp:BoundField>
              
                        <asp:BoundField DataField="CO_ApplicationReceivedDate" HeaderText="App rcv Date"
                            HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataFormatString="{0:d}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CO_ApplicationNumber" HeaderText="App Nbr" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CMFOD_Amount" HeaderText="Amount" ItemStyle-Wrap="false"
                            DataFormatString="{0:n}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CMFOD_Units" HeaderText="Unit" ItemStyle-Wrap="false" DataFormatString="{0:n}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                       
                        <asp:BoundField DataField="AB_BranchName" HeaderText="Branch" ItemStyle-Wrap="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="RM_Name" HeaderText="RM" ItemStyle-Wrap="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CMFT_TransactionNumber" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                            HeaderText="Trans Nbr">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ADUL_ProcessId" HeaderText="Upload ProcessID" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="CMFT_TransactionDate" HeaderText="Trans Date" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false" DataFormatString="{0:d}">
                            <HeaderStyle HorizontalAlign="center" />
                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CMFT_Price" HeaderText="Price (Rs)" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false" DataFormatString="{0:n}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CMFT_Amount" HeaderText="Trans Amount" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false" DataFormatString="{0:n}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CMFOD_Units" HeaderText="Trans Units" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false" DataFormatString="{0:n}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        
                        <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Left">
                            <HeaderTemplate>
                                <asp:Label ID="lblApprovedHeader" runat="server" Text="Is Approved"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIsApproved" runat="server" Text='<%#Eval("CO_IsClose").ToString() %>'> </asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        
                    </Columns>
                    <EditRowStyle CssClass="EditRowStyle" />
                    <FooterStyle CssClass="FooterStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                </asp:GridView>--%>
                <div id="dvOrderMIS" runat="server" style="width: 640px;">
                <telerik:RadGrid ID="gvCustomerOrderMIS" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True"  
                    OnItemDataBound="gvCustomerOrderMIS_ItemDataBound" OnNeedDataSource="gvCustomerOrderMIS_OnNeedDataSource"
                    ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="false"
                    AllowAutomaticInserts="false" ExportSettings-FileName="MF Order Recon" > 
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true" FileName="MF Order Recon" Excel-Format="ExcelML">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="CO_OrderId,CMFOD_OrderDetailsId,C_CustomerId,CP_portfolioId,PASP_SchemePlanCode,CMFA_AccountId,WMTT_TransactionClassificationCode,CMFOD_Amount,CO_OrderDate,PASP_SchemePlanSwitch" 
                        Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                        
                        <telerik:GridTemplateColumn HeaderText="Select">
                        <HeaderTemplate>
                        <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                         <input id="chkBxWerpAll" name="chkBxWerpAll" type="checkbox" onclick="checkAllBoxes()" />
                        </HeaderTemplate>
                        <ItemTemplate>
                        <asp:CheckBox ID="cbRecons" runat="server" Checked="false" />
                        </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        
                     <%--    <telerik:GridBoundColumn DataField="CMFOD_OrderNumber" HeaderText="OrderNumber"
                                SortExpression="CMFOD_OrderNumber" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="CMFOD_OrderNumber" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>--%>
                        
                         <%--<telerik:GridButtonColumn HeaderStyle-Width="50px" ButtonType="LinkButton" Text='<%#Eval("CMFOD_OrderNumber").ToString() %>' CommandName="ViewOrder">
                          </telerik:GridButtonColumn>--%>
                          <telerik:GridTemplateColumn HeaderText="Order Nbr" AllowFiltering="false">
                            <ItemStyle />
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkOrderNo" runat="server" CssClass="cmbField" Text='<%# Eval("CMFOD_OrderNumber") %>'
                                    OnClick="lnkOrderNo_Click">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        
                        <telerik:GridBoundColumn DataField="CO_OrderDate" HeaderText="OrderDate"
                                SortExpression="CO_OrderDate" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="CO_OrderDate" FooterStyle-HorizontalAlign="Left" DataFormatString="{0:d}">
                                <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                      
                        <telerik:GridBoundColumn DataField="Customer_Name" HeaderText="Customer"
                                SortExpression="Customer_Name" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="Customer_Name" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="WMTT_TransactionClassificationName" HeaderText="Trans Type"
                                SortExpression="WMTT_TransactionClassificationName" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="WMTT_TransactionClassificationName" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="XS_Status" HeaderText="Status"
                                SortExpression="XS_Status" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="XS_Status" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                       
                        <telerik:GridTemplateColumn >
                        <HeaderTemplate>
                                <asp:Label ID="lblOrderTypeHeader" runat="server" Text="Order Type"></asp:Label>
                            </HeaderTemplate>
                        <ItemTemplate>
                                <asp:Label ID="lblOrderType" runat="server" Text='<%#Eval("CMFOD_IsImmediate").ToString() %>'> </asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        
                        <telerik:GridBoundColumn DataField="CMFA_FolioNum" HeaderText="FolioNum"
                                SortExpression="CMFA_FolioNum" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="CMFA_FolioNum" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="PASP_SchemePlanName" HeaderText="SchemePlanName"
                                SortExpression="PASP_SchemePlanName" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="PASP_SchemePlanName" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="CO_ApplicationReceivedDate" HeaderText="App rcv Date"
                                SortExpression="CO_ApplicationReceivedDate" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="CO_ApplicationReceivedDate" DataFormatString="{0:d}"
                                FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="CO_ApplicationNumber" HeaderText="App Nbr"
                                SortExpression="CO_ApplicationNumber" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="CO_ApplicationNumber" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="CMFOD_Amount" HeaderText="Amount"
                                SortExpression="CMFOD_Amount" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="CMFOD_Amount" FooterStyle-HorizontalAlign="Left"
                                DataFormatString="{0:n}">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="CMFOD_Units" HeaderText="Units"
                                SortExpression="CMFOD_Units" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="CMFOD_Units" FooterStyle-HorizontalAlign="Left"
                                DataFormatString="{0:n}">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="AB_BranchName" HeaderText="Branch"
                                SortExpression="AB_BranchName" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="AB_BranchName" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="RM_Name" HeaderText="RM"
                                SortExpression="RM_Name" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="RM_Name" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="CMFT_TransactionNumber" HeaderText="Trans Nbr"
                                SortExpression="CMFT_TransactionNumber" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="CMFT_TransactionNumber" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ADUL_ProcessId" HeaderText="Upload ProcessID"
                                SortExpression="ADUL_ProcessId" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="ADUL_ProcessId" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CMFT_TransactionDate" HeaderText="Trans Date"
                                SortExpression="CMFT_TransactionDate" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="CMFT_TransactionDate" FooterStyle-HorizontalAlign="Left"
                                DataFormatString="{0:d}">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CMFT_Price" HeaderText="Price (Rs)"
                                SortExpression="CMFT_Price" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="CMFT_Price" FooterStyle-HorizontalAlign="Left"
                                DataFormatString="{0:n}">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CMFT_Amount" HeaderText="Trans Amount"
                                SortExpression="CMFT_Amount" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="CMFT_Amount" FooterStyle-HorizontalAlign="Left"
                                DataFormatString="{0:n}">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CMFT_Units" HeaderText="Trans Units"
                                SortExpression="CMFT_Units" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="CMFT_Units" FooterStyle-HorizontalAlign="Left"
                                DataFormatString="{0:n}">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                                                
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid></div>
            </td>
            <td>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSync" runat="server" Text="Auto Match" CssClass="PCGMediumButton"
                    OnClick="btnSync_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnMannualMatch" runat="server" Text="Manual Match" CssClass="PCGMediumButton"
                    OnClick="btnMannualMatch_Click" />
            </td>
            <td>
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
        <%--    <td> 
    <asp:Button ID="btnSubmit" CssClass="PCGButton" runat="server" Text="Submit" onclick="btnSubmit_Click" />
    </td>--%>
    </tr>
</table>
<asp:HiddenField ID="hdnBranchId" runat="server" />
<asp:HiddenField ID="hdnRMId" runat="server" />
<%--<asp:HiddenField id="hdnAssetType" runat="server"/>
<asp:HiddenField id="hdnPortFolio" runat="server"/>--%>
<asp:HiddenField ID="hdnTransactionType" runat="server" />
<%--<asp:HiddenField id="hdnARDate" runat="server"/>--%>
<asp:HiddenField ID="hdnOrdStatus" runat="server" />
<asp:HiddenField ID="hdnOrderType" runat="server" />
<%--<asp:HiddenField id="hdnOrderDate" runat="server"/>--%>
<%--<asp:HiddenField id="hdnCustomerId" runat="server"/>
<asp:HiddenField id="hdnScheme" runat="server"/>
<asp:HiddenField id="hdnFolio" runat="server"/>--%>
<asp:HiddenField ID="hdnamcCode" runat="server" />
<asp:HiddenField ID="hdnFromdate" runat="server" />
<asp:HiddenField ID="hdnTodate" runat="server" />
<asp:HiddenField ID="hdnDownloadFormat" runat="server" Visible="true" />
<asp:HiddenField ID="hdnDownloadPageType" runat="server" Visible="true" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnIndividualOrGroup" runat="server" />
 <asp:HiddenField ID="hdnCustomerId" runat="server" 
    onvaluechanged="hdnCustomerId_ValueChanged" />
