<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductOrderMaster.ascx.cs" Inherits="WealthERP.OPS.ProductOrderMaster" Debug="true"  %>
   <%@ Register Src="~/OPS/FixedIncomeOrderEntry.ascx" TagPrefix="FixedIncomeOrder" TagName="FixedIncomeOrder" %> 
  <%@ Register Src="~/OPS/ProductOrderDetailsMF.ascx" TagPrefix="TestControl" TagName="TestControl" %> 
   
<%-- <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FixedIncomeOrderEntry.ascx.cs" Inherits="WealthERP.OPS.FixedIncomeOrderEntry" %> 
--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {

        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();

        return false;
    }
 
 
    </script>

<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>


 
     
     
   <%-- <script type="text/javascript" language="javascript">
        function GetCustomerId(source, eventArgs) {

            document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();

            return false;
        }
--%>
<script type="text/javascript" language="javascript">
        function openpopupAddCustomer() {
            window.open('PopUp.aspx?PageId=CustomerType', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
            return false;
        }
</script>

<script type="text/javascript" language="javascript">
    function openpopupAddBank() {
        window.open('PopUp.aspx?PageId=AddBankAccount', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
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
    function ShowIsa() {

        var hdn = document.getElementById("<%=hdnIsSubscripted.ClientID%>").value;

        if (hdn == "True") {

            document.getElementById("<%= trIsa.ClientID %>").style.visibility = 'visible';
            document.getElementById("<%= trJointHoldersList.ClientID %>").style.visibility = 'visible';

        }
        else {
            document.getElementById("<%= trIsa.ClientID %>").style.visibility = 'collapse';
            document.getElementById("<%= trJointHoldersList.ClientID %>").style.visibility = 'collapse';

        }

    }
    function ShowInitialIsa() {

        document.getElementById("<%= trIsa.ClientID %>").style.visibility = 'collapse';
        document.getElementById("<%= trJointHoldersList.ClientID %>").style.visibility = 'collapse';

    }
    function CheckSubscription() {

        document.getElementById("<%= trIsa.ClientID %>").style.visibility = 'visible';
        document.getElementById("<%= trJointHoldersList.ClientID %>").style.visibility = 'collapse';
    }
</script>
  <table width="100%">
   
    <tr>
    <td align="left" >
      <div class="divPageHeading" style="vertical-align: text-bottom">
      Order Entry
      </div>
      </td>
      </tr>
      </table>
<table width="100%">
   
    <tr>
    <td align="center" >
      <div class="divSectionHeading" style="vertical-align: text-bottom">
       <asp:Label ID="Label1" runat="server" Text="Select Product:" CssClass="FieldName">
    </asp:Label> 
      <asp:DropDownList ID="DdlLoad" runat="server" CssClass="cmbField" OnSelectedIndexChanged="DdlLoad_Selectedindexchanged"
                AutoPostBack="true">
                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                <asp:ListItem Text="MutualFund" Value="1" Enabled="false" ></asp:ListItem>
                <asp:ListItem Text="FixedIncome" Value="2"></asp:ListItem>
            </asp:DropDownList>
            
    </div>
   
     </td>

       </tr>
       <tr id="tr1" runat="server">
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
               <asp:Label ID="Label6" runat="server" ></asp:Label>
            </div>
        </td>
    </tr>
    </table>
    
<table>
    <tr id="trCustSect" runat="server">
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
    <tr id="trCustSearch" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblsearch" runat="server" CssClass="FieldName" Text="Search for"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlsearch" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlsearch_Selectedindexchanged"
                AutoPostBack="true">
                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                <asp:ListItem Text="Customer" Value="1"></asp:ListItem>
                <asp:ListItem Text="PAN" Value="2"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblARNNo" runat="server" CssClass="FieldName" Text="ARN No:" ></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlARNNo" runat="server" CssClass="cmbField" AutoPostBack="false"  >
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator12" runat="server" ControlToValidate="ddlARNNo"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an ARN"
                Operator="NotEqual"  ValueToCompare="Select"></asp:CompareValidator>
               <%-- ValidationGroup="MFSubmit"--%>
        </td>
        <td >
        </td>
    </tr>
    
    <tr id="trpan" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblPansearch" runat="server" Text="Pan Number: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtPansearch" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" onclientClick="ShowIsa()" >
            </asp:TextBox><span id="Span1" class="spnRequiredField">*</span>
            <%--<asp:Button ID="btnAddCustomer" runat="server" Text="Add a Customer" CssClass="PCGMediumButton"
                CausesValidation="true" onmouseover="javascritxtBranchNamept:ChangeButtonCss('hover', 'ctrl_OrderEntry_btnAddCustomer','S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_OrderEntry_btnAddCustomer','S');"
                OnClientClick="return openpopupAddCustomer()" 
                onclick="btnAddCustomer_Click" />--%>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" TargetControlID="txtPansearch"
                WatermarkText="Enter few chars of Pan" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtPansearch"
                ServiceMethod="GetAdviserCustomerPan" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                Enabled="True" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPansearch"
                ErrorMessage="<br />Please Enter Pan number" Display="Dynamic" runat="server"
                CssClass="rfvPCG"  ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="label2" runat="server" Text="Customer Name: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblgetcust" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
        <td>
        </td>
    </tr>
    <tr id="trCust" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblCustomer" runat="server" Text="Customer Name: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="txtField" AutoComplete="Off"
                onclientClick="ShowIsa()" AutoPostBack="True">
            </asp:TextBox><span id="spnCustomer" class="spnRequiredField">*</span>
            <%--<asp:Button ID="btnAddCustomer" runat="server" Text="Add a Customer" CssClass="PCGMediumButton"
                CausesValidation="true" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_OrderEntry_btnAddCustomer','S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_OrderEntry_btnAddCustomer','S');"
                OnClientClick="return openpopupAddCustomer()" 
                onclick="btnAddCustomer_Click" />--%>
            <asp:ImageButton ID="btnImgAddCustomer" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                AlternateText="Add" runat="server" ToolTip="Click here to Add Customer" OnClientClick="return openpopupAddCustomer()"
                Height="15px" Width="15px"></asp:ImageButton>
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
            <asp:Label ID="lblPan" runat="server" Text="PAN" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblgetPan" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblRM" runat="server" Text="RM: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblGetRM" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblBranch" runat="server" Text="Branch: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblGetBranch" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="3">
        </td>
    </tr>
    <tr id="trAssociateSearch" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblAssociateSearch" runat="server" CssClass="FieldName" Text="Agent Code:"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtAssociateSearch" runat="server" CssClass="txtField" AutoComplete="Off"
                OnTextChanged="OnAssociateTextchanged" AutoPostBack="True">
            </asp:TextBox><span id="Span7" class="spnRequiredField">*</span>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" TargetControlID="txtAssociateSearch"
                WatermarkText="Enter few chars of Agent code" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtAssociateSearch"
                ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" OnClientItemSelected="" DelimiterCharacters=""
                Enabled="True" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtAssociateSearch"
                ErrorMessage="<br />Please Enter a agent code" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblAssociate" runat="server" CssClass="FieldName" Text="Associate:"></asp:Label>
        </td>
         <td class="rightField" style="width: 20%">
         <asp:Label ID="lblAssociatetext" runat="server" Text="" CssClass="FieldName"></asp:Label>
        
         </td>
         <td></td>
        <%--<td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlAssociate" runat="server" CssClass="cmbLongField" AutoPostBack="false">
            </asp:DropDownList>
            <asp:CompareValidator ID="ddlAssociate_CompareValidator2" runat="server" ControlToValidate="ddlAssociate"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a associate"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select(SubBroker Code/Name/Type)"></asp:CompareValidator>
        </td>--%>
    </tr>
    <tr id="trTaxStatus" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label5" runat="server" Text="Customer Tax status: " CssClass="FieldName" ></asp:Label>
        </td>
        <td>
        <asp:TextBox ID="txtTax" runat="server" CssClass="txtField" AutoComplete="Off" ReadOnly="true"   />                
         </td>    
         <td colspan="3">
        </td>
    </tr>
    <tr id="trIsa" runat="server"  >
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblIsa" runat="server" CssClass="FieldName" Text="ISA No:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlCustomerISAAccount" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlCustomerISAAccount_SelectedIndexChanged">
            </asp:DropDownList>
            &nbsp
            <asp:ImageButton ID="btnIsa" ImageUrl="~/App_Themes/Maroon/Images/user_add.png" AlternateText="Add"
                runat="server" ToolTip="Click here to Request ISA" OnClick="ISA_Onclick" Height="15px"
                Width="15px"></asp:ImageButton>
        </td>
        <td class="rightField" style="width: 20%">
        </td>
        <td class="rightField" style="width: 20%">
        </td>
        <td></td>
    </tr>
    <%-- <tr id="trRegretMsg" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblRegretMsg" runat="server" CssClass="FieldName" Text="ISA not Created for this Adviser"></asp:Label>
        </td>
        <td style="width: 20%">
            <%--<asp:Button ID="btnIsa" runat="server" CssClass="PCGLongButton" OnClick="ISA_Onclick"
                Text="Request ISA Account" />
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 20%">
        
        </td> 
    </tr>--%>
    <tr id="trJointHoldersList" runat="server">
        <td class="leftField" style="width: 20%">
        </td>
        <td colspan="4">
            <%-- <asp:Panel ID="pnlJointholders" runat="server" ScrollBars="Horizontal">--%>
            <telerik:RadGrid ID="gvJointHoldersList" Height="70px" runat="server" GridLines="None"
                AutoGenerateColumns="False" Width="45%" PageSize="4" AllowSorting="false" AllowPaging="True"
                ShowStatusBar="True" ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false"
                AllowFilteringByColumn="false" AllowAutomaticInserts="false" ExportSettings-FileName="Count">
                <MasterTableView Width="100%" AllowMultiColumnSorting="false" AutoGenerateColumns="false"
                    CommandItemDisplay="None">
                    <Columns>
                        <telerik:GridBoundColumn DataField="AccountNumber" HeaderText="Account Number" UniqueName="AccountNumber"
                            SortExpression="Account Number">
                            <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Customer" HeaderText="Customer" AllowFiltering="false"
                            HeaderStyle-HorizontalAlign="Left" UniqueName="Customer">
                            <ItemStyle HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ModeOfHolding" HeaderText="Mode Of Holding" AllowFiltering="false"
                            HeaderStyle-HorizontalAlign="Left" UniqueName="ModeOfHolding">
                            <ItemStyle HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </ClientSettings>
            </telerik:RadGrid>
            <%-- </asp:Panel>  --%>
        </td>
        <%--<td></td>
   <td></td>
   <td></td>
   <td></td>--%>
    </tr>
    <tr>
        <td colspan="5">
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
  <table width="100%">
      
    <tr id="trOrderSection" runat="server">
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
    </table> 
   <%-- <tr id="trSection1" runat="server">
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
            <asp:Label ID="lblAmount" runat="server" Text="Amount:" CssClass="FieldName"></asp:Label>
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
            <asp:Label ID="lblMode" runat="server" Text="Mode Of Payment:" CssClass="FieldName"></asp:Label>
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
            <asp:Label ID="lblPaymentNumber" runat="server" Text="Payment Instrument Number: "
                CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtPaymentNumber" runat="server" MaxLength=6 CssClass="txtField"></asp:TextBox>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblPIDate" runat="server" Text="Payment Instrument Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <telerik:RadDatePicker ID="txtPaymentInstDate" CssClass="txtField" runat="server"
                Culture="English (United States)" Skin="Telerik" EnableEmbeddedSkins="false"
                ShowAnimation-Type="Fade"  MinDate="1900-01-01">
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
                <%--<asp:CompareValidator ID="CVPaymentdate2" runat="server" ErrorMessage="<br/>Payment date cannot be greater than order date"
                 ControlToValidate="txtPaymentInstDate" ValidationGroup="MFSubmit" CssClass="cvPCG" Operator="LessThanEqual" Display="Dynamic"
               Type="Date"></asp:CompareValidator>--%>
            <%--<asp:CompareValidator ID="cvdate" runat="server" ErrorMessage="<br />Payment Instrument Date should be less than or equal to Order Date"
                Type="Date" ControlToValidate="txtPaymentInstDate" ControlToCompare="txtOrderDate"
                Operator="LessThanEqual" CssClass="cvPCG" Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>--%>
      <%--  </td>
    </tr>--%>
   <%-- <tr id="trBankName" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblBankName" runat="server" Text="Bank Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlBankName" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span4" class="spnRequiredField">*</span>
            <asp:ImageButton ID="imgAddBank" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                AlternateText="Add" runat="server" ToolTip="Click here to Add Bank" OnClientClick="return openpopupAddBank()"
                Height="15px" Width="15px"></asp:ImageButton>
            <asp:ImageButton ID="imgBtnRefereshBank" ImageUrl="~/Images/refresh.png" AlternateText="Refresh"
                runat="server" ToolTip="Click here to refresh Bank List" OnClick="imgBtnRefereshBank_OnClick"
                Height="15px" Width="25px"></asp:ImageButton>
            <asp:CompareValidator ID="CompareValidator11" runat="server" ControlToValidate="ddlBankName"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a Bank"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblBranchName" runat="server" Text="Bank BranchName:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtBranchName" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr id="trFrequency" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblFrequencySIP" runat="server" Text="Frequency:" CssClass="FieldName"></asp:Label>
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
            <asp:Label ID="lblStartDateSIP" runat="server" Text="Start Date:" CssClass="FieldName"></asp:Label>
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
            <asp:Label ID="lblEndDateSIP" runat="server" Text="End Date:" CssClass="FieldName"></asp:Label>
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
   --%>
</table>
<table>

</table>

 

<table width="100%" runat="server">
<tr  colspan="5">
<td  >
<%--<div id="test" runat="server" enableviewstate="true" ></div>--%>
<asp:PlaceHolder ID="FixedIncomePlaceHolder" runat="server" EnableViewState="true" >
<FixedIncomeOrder:FixedIncomeOrder ID="FixedIncomeOrder" runat="server">
                                    </FixedIncomeOrder:FixedIncomeOrder> 
                                      <asp:CustomValidator ID="MyChildUserControlCustomValidator" runat="server" ValidationGroup="MFSubmit" ErrorMessage="errormessage to show when the sh*t hit the fan" Text="*"></asp:CustomValidator>
</asp:PlaceHolder>
<asp:PlaceHolder ID="PlaceHolder1" runat="server" EnableViewState="true" >
<TestControl:TestControl ID="TestControl" runat="server">
                                    </TestControl:TestControl> 
</asp:PlaceHolder>

</td>
<td colspan="1"></td>
</tr>




 </table>
 <table width="100%" runat="server">
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
            <asp:Label ID="lblMode" runat="server" Text="Mode Of Payment:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Cheque" Value="CQ" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Draft" Value="DF"></asp:ListItem>
                <asp:ListItem Text="ECS" Value="ES"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblAmount" runat="server" Text="Amount:" CssClass="FieldName"></asp:Label>
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
       
    </tr>
    <tr id="trPINo" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblPaymentNumber" runat="server" Text="Payment instrument No.: "
                CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtPaymentNumber" runat="server" MaxLength=6 CssClass="txtField"></asp:TextBox>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblPIDate" runat="server" Text="Payment Instrument Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <telerik:RadDatePicker ID="txtPaymentInstDate" CssClass="txtField" runat="server"
                Culture="English (United States)" Skin="Telerik" EnableEmbeddedSkins="false"
                ShowAnimation-Type="Fade"  MinDate="1900-01-01">
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
                <%--<asp:CompareValidator ID="CVPaymentdate2" runat="server" ErrorMessage="<br/>Payment date cannot be greater than order date"
                 ControlToValidate="txtPaymentInstDate" ValidationGroup="MFSubmit" CssClass="cvPCG" Operator="LessThanEqual" Display="Dynamic"
               Type="Date"></asp:CompareValidator>--%>
            <%--<asp:CompareValidator ID="cvdate" runat="server" ErrorMessage="<br />Payment Instrument Date should be less than or equal to Order Date"
                Type="Date" ControlToValidate="txtPaymentInstDate" ControlToCompare="txtOrderDate"
                Operator="LessThanEqual" CssClass="cvPCG" Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>--%>
        </td>
    </tr>
    <tr id="trBankName" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblBankName" runat="server" Text="Bank Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlBankName" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span4" class="spnRequiredField">*</span>
            <asp:ImageButton ID="imgAddBank" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                AlternateText="Add" runat="server" ToolTip="Click here to Add Bank" OnClientClick="return openpopupAddBank()"
                Height="15px" Width="15px"></asp:ImageButton>
            <asp:ImageButton ID="imgBtnRefereshBank" ImageUrl="~/Images/refresh.png" AlternateText="Refresh"
                runat="server" ToolTip="Click here to refresh Bank List" OnClick="imgBtnRefereshBank_OnClick"
                Height="15px" Width="25px"></asp:ImageButton>
            <asp:CompareValidator ID="CompareValidator11" runat="server" ControlToValidate="ddlBankName"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a Bank"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblBranchName" runat="server" Text="Bank Branch Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtBranchName" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    
      <tr id="trDepositedBank" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label3" runat="server" Text="Bank name for payment of interest/redemption:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlDepoBank" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlDepoBank_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
           <%-- <asp:ImageButton ID="ImageButton1" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                AlternateText="Add" runat="server" ToolTip="Click here to Add Bank" OnClientClick="return openpopupAddBank()"
                Height="15px" Width="15px"></asp:ImageButton>--%>
           <%-- <asp:ImageButton ID="ImageButton2" ImageUrl="~/Images/refresh.png" AlternateText="Refresh"
                runat="server" ToolTip="Click here to refresh Bank List" OnClick="imgBtnRefereshBank_OnClick"
                Height="15px" Width="25px"></asp:ImageButton>--%>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlBankName"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a Bank"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label4" runat="server" Text="Deposited at Branch:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtDepositedBranch" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr id="trFrequency" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblFrequencySIP" runat="server" Text="Frequency:" CssClass="FieldName"></asp:Label>
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
            <asp:Label ID="lblStartDateSIP" runat="server" Text="Start Date:" CssClass="FieldName"></asp:Label>
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
            <asp:Label ID="lblEndDateSIP" runat="server" Text="End Date:" CssClass="FieldName"></asp:Label>
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
            <asp:Label ID="lblAvailableAmount" runat="server" Text="Available Amount:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblGetAvailableAmount" runat="server" Text="" CssClass="txtField"></asp:Label>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblAvailableUnits" runat="server" Text="Available Units:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblGetAvailableUnits" runat="server" Text="" CssClass="txtField"></asp:Label>
        </td>
    </tr>
    <tr id="trRedeemed" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblReedeemed" runat="server" Text="Redeem/Switch:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:RadioButton ID="rbtAmount" Class="cmbField" runat="server" GroupName="AmountUnit"
                Checked="True" Text="Amount" />
            <asp:RadioButton ID="rbtUnit" Class="cmbField" runat="server" GroupName="AmountUnit"
                Text="Units" />
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblAmountUnits" runat="server" Text="Amount/Unit:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtNewAmount" runat="server" CssClass="txtField"></asp:TextBox><span
                id="Span4" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtNewAmount"
                CssClass="rfvPCG" ErrorMessage="<br />Please select Amount/Unit" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtNewAmount" runat="server"
                Display="Dynamic" ErrorMessage="<br />Please enter a numeric value" Type="Double"
                Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trScheme" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblSchemeSwitch" runat="server" Text="To Scheme:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlSchemeSwitch" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span5" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="ddlSchemeSwitch"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a scheme"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2" style="width: 40%">
        </td>
    </tr>
    <tr id="trFrequencySTP" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblFrequencySTP" runat="server" Text="Frequency:" CssClass="FieldName"></asp:Label>
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
            <asp:Label ID="lblstartDateSTP" runat="server" Text="Start Date:" CssClass="FieldName"></asp:Label>
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
            <asp:Label ID="lblendDateSTP" runat="server" Text="End Date:" CssClass="FieldName"></asp:Label>
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
    <tr id="trDocumentSec" runat="server">
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Documents Submitted
            </div>
        </td>
    </tr>
     <tr id="trProofType" runat="server">
                    <td align="right" >
                        <label class="FieldName">
                            Proof type:
                        </label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlProofType" AutoPostBack="true" runat="server" CssClass="cmbField" 
                        OnSelectedIndexChanged="ddlProofType_SelectedIndexChanged">
                        </asp:DropDownList>
                    <%--    <asp:CompareValidator ID="cmpProof" runat="server" ValidationGroup="VaultValidations"
                            ControlToValidate="ddlProof" ErrorMessage="Please select a Proof or Form" Operator="NotEqual"
                            ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic">
                        </asp:CompareValidator>--%>
                    </td>
                </tr>
                
                 <tr id="trProof" runat="server">
                    <td align="right">
                        <label class="FieldName">
                            Proof :
                        </label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlProof" runat="server" CssClass="cmbField" >
                        </asp:DropDownList>
                     <%--   <asp:CompareValidator ID="cmpProofCopyType" runat="server" ValidationGroup="VaultValidations"
                            ControlToValidate="ddlProofCopyType" ErrorMessage="Please select a copy type"
                            Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic">
                        </asp:CompareValidator>--%>
                    </td>
                </tr>
                
                
    <tr id="trUpload" runat="server" >
        <td align="right" style="vertical-align: middle">
            <label class="FieldName">
                Upload:
            </label>
        </td>
        <td align="left" style="vertical-align: middle">
            <span style="font-size: xx-small">(Allowed extensions are: .jpg,.jpeg,.bmp,.png,.pdf)</span>
            <telerik:RadUpload ID="radUploadProof" runat="server" ControlObjectsVisibility="None" AutoPostBack="false" 
                AllowedFileExtensions=".jpg,.jpeg,.bmp,.png,.pdf" Skin="Telerik" EnableEmbeddedSkins="true">
            </telerik:RadUpload>
           <%-- <asp:CustomValidator ID="Customvalidator1" ValidationGroup="VaultValidations" Font-Bold="true"
                Font-Size="X-Small" ErrorMessage="Empty / Invalid File..!!!" ForeColor="Red"
                runat="server" Display="Dynamic" ClientValidationFunction="validateRadUpload1"></asp:CustomValidator>--%>
            <asp:Label ID="lblFileUploaded" runat="server" CssClass="cmbField" Text=""></asp:Label>
          <%--   <asp:Button ID="btnUploadImg" runat="server" Text="Submit" CssClass="PCGButton"  
                OnClick="btnUploadImg_Click" />--%>
        </td>
    </tr>
    <tr id="trBtnSubmit" runat="server">
        <td align="left" colspan="3">
            <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="PCGButton" ValidationGroup="MFSubmit"
                OnClick="btnSubmit_Click" />
            <asp:Button ID="Button2" runat="server" Text="Save & AddMore" CssClass="PCGMediumButton"
                ValidationGroup="MFSubmit" OnClick="btnAddMore_Click" />
            <asp:Button ID="Button3" runat="server" Text="Update" CssClass="PCGButton" ValidationGroup="MFSubmit" Visible="false" 
                OnClick="btnUpdate_Click" />
        </td>
    </tr>
    </table>
    <table>
    
 </table>
<%-- ValidationGroup="MFSubmit"--%>
 
 
 
 
 
 
 
 
 
 
 
 <asp:Panel ID="pnlOrderSteps" runat="server" Width="100%" Height="80%">
    <table width="100%">
        <tr>
            <td>
                <telerik:RadGrid ID="rgvOrderSteps" runat="server" Skin="Telerik" CssClass="RadGrid"
                    Width="80%" GridLines="None" AllowPaging="True" PageSize="20" AllowSorting="false"
                    AutoGenerateColumns="True" OnItemCreated="rgvOrderSteps_ItemCreated" ShowStatusBar="true"
                    AllowAutomaticUpdates="false" HorizontalAlign="NotSet" DataKeyNames="CO_OrderId,WOS_OrderStepCode"
                    OnItemDataBound="rgvOrderSteps_ItemDataBound" OnItemCommand="rgvOrderSteps_ItemCommand"
                    OnNeedDataSource="rgvOrderSteps_NeedDataSource">
                    <MasterTableView CommandItemDisplay="none" EditMode="PopUp" EnableViewState="false">
                        <Columns>
                            <%-- <telerik:GridBoundColumn  DataField="CO_OrderId"  HeaderText="OrderId" UniqueName="CO_OrderId" ReadOnly="True">
                            <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn DataField="WOS_OrderStep" HeaderText="Stages" UniqueName="WOS_OrderStep"
                                ReadOnly="True">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Stages" UniqueName="WOS_OrderStepCode" DataField="WOS_OrderStepCode"
                                Visible="false" ReadOnly="True">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrderStepCode" runat="server" Text='<%#Eval("WOS_OrderStepCode")%>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <%-- <telerik:GridDropDownColumn UniqueName="DropDownColumnStatus" HeaderText="Status" 
                        ListTextField="XS_Status" ListValueField="XS_StatusCode" DataField="XS_StatusCode"></telerik:GridDropDownColumn>
                        
                        <telerik:GridDropDownColumn UniqueName="DropDownColumnStatusReason" HeaderText="Pending Reason"
                        ListTextField="XSR_StatusReason" ListValueField="XSR_StatusReasonCode" DataField="XSR_StatusReasonCode"></telerik:GridDropDownColumn>--%>
                            <telerik:GridTemplateColumn DataField="XS_StatusCode" HeaderText="Status" UniqueName="DropDownColumnStatus">
                                <EditItemTemplate>
                                    <telerik:RadComboBox ID="ddlCustomerOrderStatus" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomerOrderStatus_OnSelectedIndexChanged"
                                        SelectedValue='<%#Bind("XS_StatusCode") %>' runat="server">
                                    </telerik:RadComboBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblOrderStatus" runat="server" Text='<%#Eval("XS_Status")%>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="XSR_StatusReasonCode" HeaderText="Pending Reason"
                                UniqueName="DropDownColumnStatusReason">
                                <EditItemTemplate>
                                    <telerik:RadComboBox ID="ddlCustomerOrderStatusReason" runat="server">
                                    </telerik:RadComboBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblOrderStatusReason" runat="server" Text='<%#Eval("XSR_StatusReason")%>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <%--     <telerik:GridDateTimeColumn DataField="CMFOS_Date" HeaderText="Date" DataFormatString="{0:d}" HtmlEncode="false" DataType="System.DateTime"
                        UniqueName="CMFOS_Date" ReadOnly="true"/>--%>
                            <telerik:GridTemplateColumn UniqueName="lblCMFOS_Date" DataField="CMFOS_Date" HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblCMFOS_Date" runat="server" DataFormatString="{0:d}" Text='<%# DataBinder.Eval(Container.DataItem, "CMFOS_Date", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridEditCommandColumn UpdateText="Update" UniqueName="EditCommandColumn"
                                CancelText="Cancel">
                                <HeaderStyle></HeaderStyle>
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn DataField="COS_IsEditable" DataType="System.Boolean" UniqueName="COS_IsEditable"
                                Display="false" ReadOnly="True">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn Visible="false" UniqueName="lblStatus" DataField="XS_StatusCode">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatusCode" runat="server" Text='<%#Eval("XS_StatusCode")%>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
 
 
 
    
  
    
    <asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="txtCustomerId_ValueChanged1" />
<asp:HiddenField ID="hdnTransType" runat="server" OnValueChanged="hdnTransType_ValueChanged1" />
<asp:HiddenField ID="hdnType" runat="server" />
<asp:HiddenField ID="hdnSchemeCode" runat="server" />
<asp:HiddenField ID="hdnPortfolioId" runat="server" />
<asp:HiddenField ID="hdnAccountId" runat="server" />
<asp:HiddenField ID="hdnAmcCode" runat="server" />
<asp:HiddenField ID="hdnSchemeName" runat="server" />
<asp:HiddenField ID="hdnSchemeSwitch" runat="server" />
<asp:HiddenField ID="hdnBankName" runat="server" />
<asp:HiddenField ID="hdnIsSubscripted" runat="server" />

<asp:HiddenField ID="hdnCustomerId" runat="server" />



<%--Vieing--%>

<%--
 <asp:Button ID="btnView" runat="server" Text="Submit" CssClass="PCGButton" 
                OnClick="btnView_Click" />--%>
<asp:HiddenField ID="hdnTodate" runat="server" />
<asp:HiddenField ID="hdnFromdate" runat="server" />
 
    <%--<asp:Panel ID="pnlOrderList" runat="server" class="Landscape" Width="100%" Height="80%"
    ScrollBars="Horizontal" Visible="false">
    <table width="100%">
        <tr id="trExportFilteredDupData" runat="server">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="gvOrderList" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AllowAutomaticInserts="false"
                    OnNeedDataSource="gvOrderList_OnNeedDataSource" OnItemDataBound="gvOrderList_ItemDataBound">
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="OrderMIS">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="CO_OrderId,C_CustomerId,PAG_AssetGroupCode" Width="102%"
                        AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                        <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                            ShowExportToCsvButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                        <Columns>
                            <telerik:GridTemplateColumn ItemStyle-Width="120px" AllowFiltering="false">
                                <ItemTemplate>
                                    <telerik:RadComboBox ID="ddlMenu" 
                                        CssClass="cmbField" runat="server" EnableEmbeddedSkins="false" Skin="Telerik"
                                        AllowCustomText="true" Width="80px" AutoPostBack="true">
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="~/Images/Select.png" Text="Select" Value="0" Selected="true">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem Text="View" Value="View" ImageUrl="~/Images/DetailedView.png"
                                                runat="server"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="~/Images/RecordEdit.png" Text="Edit" Value="Edit"
                                                runat="server"></telerik:RadComboBoxItem>
                                            <%--<telerik:RadComboBoxItem ImageUrl="~/Images/DeleteRecord.png" Text="Delete" Value="Delete"
                                            runat="server"></telerik:RadComboBoxItem>--%>
                                            <%--OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged"--%>
                                     <%--   </Items>
                                    </telerik:RadComboBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>--%>
                            <%--<telerik:GridButtonColumn ButtonType="LinkButton" CommandName="Redirect" UniqueName="CO_OrderId"
                                HeaderText="Order No." DataTextField="CO_OrderId" FooterStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridButtonColumn>--%>
                         <%--   <telerik:GridBoundColumn DataField="CFIOD_OrderNO" AllowFiltering="true" HeaderText="Order No."
                                UniqueName="CFIOD_OrderNO" SortExpression="CFIOD_OrderNO" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="85px" FilterControlWidth="50px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_OrderDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="true" HeaderText="Order Date" UniqueName="CO_OrderDate" SortExpression="CO_OrderDate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="80px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ZonalManagerName" AllowFiltering="true" HeaderText="Zonal Manager"
                                UniqueName="ZonalManagerName" SortExpression="ZonalManagerName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AreaManager" AllowFiltering="true" HeaderText="Area Manager"
                                UniqueName="AreaManager" SortExpression="AreaManager" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="CircleManager" AllowFiltering="true"
                                HeaderText="Channel Manager" UniqueName="CircleManager" SortExpression="CircleManager"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ChannelName" AllowFiltering="true" HeaderText="Channel"
                                UniqueName="ChannelName" SortExpression="ChannelName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                           <%-- <telerik:GridBoundColumn DataField="Name" AllowFiltering="true" HeaderText="Customer"
                                UniqueName="Name" SortExpression="Name" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="C_PANNum" AllowFiltering="true" HeaderText="PAN"
                                UniqueName="C_PANNum" SortExpression="C_PANNum" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                          <%--  <telerik:GridBoundColumn DataField="PAG_AssetGroupName" AllowFiltering="true" HeaderText="Asset Name"
                                UniqueName="PAG_AssetGroupName" SortExpression="PAG_AssetGroupName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SubBrokerCode" AllowFiltering="true" HeaderText="SubBroker Code"
                                UniqueName="SubBrokerCode" SortExpression="SubBrokerCode" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AssociatesName" AllowFiltering="true" HeaderText="SubBroker Name"
                                Visible="true" UniqueName="AssociatesName" SortExpression="AssociatesName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="120px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                           <%-- <telerik:GridBoundColumn DataField="PASP_SchemePlanName" HeaderText="SchemePlanName"
                                AllowFiltering="true" SortExpression="PASP_SchemePlanName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="PASP_SchemePlanName"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="250px">
                            </telerik:GridBoundColumn>--%>
                         <%--   <telerik:GridBoundColumn DataField="PAIC_AssetInstrumentCategoryName" HeaderText="Category"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="PAIC_AssetInstrumentCategoryName"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="PAIC_AssetInstrumentCategoryName" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                           <%-- <telerik:GridBoundColumn DataField="PAISC_AssetInstrumentSubCategoryName" HeaderText="Sub Category"
                                AllowFiltering="false" HeaderStyle-Wrap="false" SortExpression="PAISC_AssetInstrumentSubCategoryName"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="100px" UniqueName="PAISC_AssetInstrumentSubCategoryName" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                        <%--    <telerik:GridBoundColumn DataField="CFIOD_AmountPayable" AllowFiltering="true" HeaderText="Amount"
                                DataFormatString="{0:N0}" UniqueName="CFIOD_AmountPayable" SortExpression="CFIOD_AmountPayable"
                                ShowFilterIcon="false" HeaderStyle-Width="80px" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true">                                
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="CFIOD_MaturityAmount" HeaderText="Cheque No."
                                 AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CFIOD_MaturityAmount"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="100px" UniqueName="CFIOD_MaturityAmount" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                              <%--<telerik:GridBoundColumn DataField="CO_PaymentDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="true" HeaderText="Payment Date" UniqueName="CO_PaymentDate" SortExpression="CO_PaymentDate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="80px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="CMFOD_BankName" HeaderText="Bank"
                                 AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CMFOD_BankName"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="100px" UniqueName="CMFOD_BankName" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="CMFOD_BranchName" HeaderText="Branch"
                                 AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CMFOD_BranchName"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="100px" UniqueName="CMFOD_BranchName" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="CMFOD_ARNNo" HeaderText="ARN No."
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CMFOD_ARNNo"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="100px" UniqueName="CMFOD_ARNNo" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="Address" HeaderText="Address"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="Address"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="100px" UniqueName="Address" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="CMFOD_City" HeaderText="City"
                                 AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CMFOD_City"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="100px" UniqueName="CMFOD_City" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="CMFOD_PinCode" HeaderText="Pin Code"
                                 AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CMFOD_PinCode"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="100px" UniqueName="CMFOD_PinCode" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>                            
                             <telerik:GridBoundColumn DataField="XF_FrequencyCode" HeaderText="FrequencyCode"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="XF_FrequencyCode"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="100px" UniqueName="XF_FrequencyCode" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                              <telerik:GridBoundColumn DataField="CMFOD_ApprovalDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="true" HeaderText="Approval Date" UniqueName="CMFOD_ApprovalDate" SortExpression="CMFOD_ApprovalDate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="80px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WMTT_TransactionType" AllowFiltering="true" HeaderText="Transaction Type"
                                UniqueName="WMTT_TransactionType" SortExpression="WMTT_TransactionType" ShowFilterIcon="false"
                                HeaderStyle-Width="80px" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="XS_Status" AllowFiltering="true" HeaderText="Order Status"
                                HeaderStyle-Width="80px" UniqueName="XS_Status" SortExpression="XS_Status" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_ApplicationNumber" AllowFiltering="true" HeaderText="Application No."
                                UniqueName="CO_ApplicationNumber" SortExpression="CO_ApplicationNumber" ShowFilterIcon="false"
                                HeaderStyle-Width="80px" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />--%>
                           <%-- </telerik:GridBoundColumn>--%>
                            <%--<telerik:GridBoundColumn DataField="IS_SchemeName" AllowFiltering="false" HeaderText="Scheme Name"
                                UniqueName="IS_SchemeName">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="XII_InsuranceIssuerName" AllowFiltering="false"
                                HeaderText="Issuer Name" UniqueName="XII_InsuranceIssuerName">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                            <%--<telerik:GridBoundColumn DataField="CO_ApplicationReceivedDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="false" HeaderText="Application Received Date" UniqueName="CO_ApplicationReceivedDate">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CIOD_PolicyMaturityDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="false" HeaderText="Maturity Date" UniqueName="CIOD_PolicyMaturityDate">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_PaymentDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="false" HeaderText="Payment Date" UniqueName="CO_PaymentDate">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WOSR_SourceName" AllowFiltering="false" HeaderText="Source Type"
                                UniqueName="WOSR_SourceName">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                            <%--<telerik:GridBoundColumn DataField="XPM_PaymentMode" AllowFiltering="false" HeaderText="Payment Mode"
                                UniqueName="XPM_PaymentMode">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_ChequeNumber" AllowFiltering="false" HeaderText="Cheque Number"
                                UniqueName="CO_ChequeNumber">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CB_BankName" AllowFiltering="false" HeaderText="Bank Name"
                                UniqueName="CB_BankName">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CIOD_SumAssured" AllowFiltering="false" HeaderText="Sum Assured"
                                UniqueName="CIOD_SumAssured">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="XF_Frequency" AllowFiltering="false" HeaderText="Frequency"
                                UniqueName="XF_Frequency">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                      <%--  </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Resizing AllowColumnResize="true" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>--%> 