<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IPOIssueTransactOffline.ascx.cs" Inherits="WealthERP.OffLineOrderManagement.IPOIssueTransactOffline" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<script type="text/javascript">
    var TargetBaseControl = null;
    var TragetBaseControl2 = null;

    window.onload = function() {
        try {
            //get target base control.
            TargetBaseControl =
           document.getElementById('<%=this.gvDematDetailsTeleR.ClientID %>');

        }
        catch (err) {
            TargetBaseControl = null;
        }
    }

    function TestCheckBox() {
        if (TargetBaseControl == null) return false;

        //get target child control.
        var TargetChildControl = "chkDematId";
        var Count = 0;
        //get all the control of the type INPUT in the base control.
        var Inputs = TargetBaseControl.getElementsByTagName("input");

        for (var n = 0; n < Inputs.length; ++n)
            if (Inputs[n].type == 'checkbox' &&
            Inputs[n].id.indexOf(TargetChildControl, 0) >= 0 &&
            Inputs[n].checked)
            Count++;
        if (Count > 1) {
            alert('Please Select One Demat!');
            return false;
        }
        else if (Count == 0) {
            alert('Please Select Aleast One Demat!');
            return false;
        }

        return true;


    }
    
</script>

<script type="text/javascript" language="javascript">

    function GetCustomerId(source, eventArgs) {
        isItemSelected = true;
        //         document.getElementById("lblgetPan").innerHTML = "";
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();

        return false;
    }

    function ValidateAssociateName() {
        //        var x = document.forms["form1"]["TextBoxName"].value;
        document.getElementById("<%=  lblAssociatetext.ClientID %>").value = eventArgs.get_value();
        document.getElementById("lblAssociatetext").innerHTML = "AgentCode Required";
        return true;
    }

    function openpopupAddCustomer() {
        window.open('PopUp.aspx?AddMFCustLinkId=mf&pageID=CustomerType&', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
        return false;
    }

    
   
</script>

<script type="text/javascript">

    var isItemSelected = false;

    //Handler for textbox blur event
    function checkItemSelected(txtPanNumber) {
        var returnValue = true;
        if (!isItemSelected) {

            if (txtPanNumber.value != "") {
                txtPanNumber.focus();
                alert("Please select Pan Number from the Pan list only");
                txtPanNumber.value = "";
                returnValue = false;
            }
        }
        return returnValue;



    }
    
</script>

<script type="text/javascript" language="javascript">

    function openpopupAddBank() {
        var custId = document.getElementById("<%= txtCustomerId.ClientID %>").value
        window.open('PopUp.aspx?PageId=AddBankAccount&bankId=0&action=OfflineMF&custId=' + custId, 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
        return false;

        //        document.getElementById("<%= HiddenField1.ClientID %>").value = 1;

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

    }
    
</script>

<script type="text/javascript">
    function isNumberKey(evt) { // Numbers only
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            alert('Only Numeric');
            return false;
        }
        return true;  
</script>
<%--<script type="text/javascript">
    function ValidateTermsConditions(sender, args) {

        if (document.getElementById("<%=chkTermsCondition.ClientID %>").checked == true) {
            args.IsValid = true;
        } else {
            args.IsValid = false;
        }
    }
</script>--%>

<script type="text/javascript">
    var crnt = 0;
    function PreventClicks() {

        if (typeof (Page_ClientValidate('btnSubmit')) == 'function') {
            Page_ClientValidate();
        }

        if (Page_IsValid) {
            if (++crnt > 1) {
                return false;
            }
            return true;
        }
        else {
            return false;
        }
    }
</script>




<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
       
        <table id="tblMessage" width="100%" runat="server" visible="false" style="padding-top: 20px;">
            <tr id="trSumbitSuccess">
                <td align="center">
                    <div id="msgRecordStatus" class="success-msg" align="center" runat="server">
                    </div>
                </td>
            </tr>
            <tr id="trinsufficentmessage" runat="server" visible="false">
                <td align="center">
                    <asp:Label ID="lblinsufficent" runat="server" ForeColor="Red" Text="Order cannot be processed due to insufficient balance"></asp:Label>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>


<table width="100%">
    <tr>
        <td colspan="5">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            IPO Order Entry
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    </table>
     <asp:Panel ID="pnl_OrderSection" runat="server" class="Landscape" Width="130%" Height="80%"
    ScrollBars="None"  >
    <table width="100%">
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
            <asp:Label ID="lblsearch" runat="server" CssClass="FieldName" Text="Search for"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlsearch" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlsearch_Selectedindexchanged"
                AutoPostBack="true" TabIndex="0">
                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                <asp:ListItem Text="Customer" Value="1"></asp:ListItem>
                <asp:ListItem Text="Pan" Value="2"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblARNNo" runat="server" CssClass="FieldName" Text="ARN No:"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlARNNo" runat="server" CssClass="cmbField" AutoPostBack="false"
                TabIndex="1">
            </asp:DropDownList>
            <span id="Span14" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator12" runat="server" ControlToValidate="ddlARNNo"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an ARN"
                Operator="NotEqual" ValidationGroup="btnConfirmOrder" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td colspan="1"></td>
        
    </tr>
    
    <tr id="trpan" runat="server" visible="false">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblPansearch" runat="server" Text="Pan Number: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtPansearch" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" onclientClick="ShowIsa()" onblur="return checkItemSelected(this)"
                OnTextChanged="OnAssociateTextchanged1" TabIndex="2">
            </asp:TextBox><span id="Span1" class="spnRequiredField">*</span>
            
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" TargetControlID="txtPansearch"
                WatermarkText="Enter few chars of Pan" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtPansearch"
                ServiceMethod="GetAdviserCustomerPan" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="1" CompletionInterval="0"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                Enabled="True" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPansearch"
                ErrorMessage="<br />Please Enter Pan number" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="label2" runat="server" Text="Customer Name: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblgetcust" runat="server" Text="" CssClass="FieldName" onclientClick="CheckPanno()"></asp:Label>
        </td>
       
    </tr>
    
    <tr id="trCust" runat="server" visible="false">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblCustomer" runat="server" Text="Customer Name: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="txtField" AutoComplete="Off"
                onclientClick="ShowIsa()" AutoPostBack="True" TabIndex="2">
            
                 
            </asp:TextBox><span id="spnCustomer" class="spnRequiredField">*</span>
            
            <asp:ImageButton ID="btnImgAddCustomer" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                AlternateText="Add" runat="server" ToolTip="Click here to Add Customer" OnClientClick="return openpopupAddCustomer()"
                Height="15px" Width="15px" TabIndex="3"></asp:ImageButton>
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
                CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblPan" runat="server" Text="PAN No: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblgetPan" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
       <%-- <td class="leftField" style="width: 20%">
            <asp:Label ID="lblRM" runat="server" Text="RM: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblGetRM" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>--%>
    </tr>
    
    <tr>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblBranch" runat="server" Text="Branch: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblGetBranch" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="2">
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblAssociateSearch" runat="server" CssClass="FieldName" Text="Agent Code:"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtAssociateSearch" runat="server" CssClass="txtField" AutoComplete="Off"
                OnTextChanged="OnAssociateTextchanged" AutoPostBack="True" TabIndex="4">
            </asp:TextBox><span id="Span7" class="spnRequiredField">*</span>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" TargetControlID="txtAssociateSearch"
                WatermarkText="Enter few chars of Agent code" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtAssociateSearch"
                ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" DelimiterCharacters="" Enabled="True" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtAssociateSearch"
                ErrorMessage="<br />Please Enter a agent code" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblAssociate" runat="server" CssClass="FieldName" Text="Associate:"></asp:Label>
        </td>
        <td class="rightField" style="width: 15%">
            <asp:Label ID="lblAssociatetext" runat="server" CssClass="FieldName" Enabled="false"></asp:Label>
        </td>
       
    </tr>
    <tr id="trIsa" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblIsa" runat="server" CssClass="FieldName" Text="ISA No:"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlCustomerISAAccount" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlCustomerISAAccount_SelectedIndexChanged">
            </asp:DropDownList>
            &nbsp
            <asp:ImageButton ID="btnIsa" ImageUrl="~/App_Themes/Maroon/Images/user_add.png" AlternateText="Add"
                runat="server" ToolTip="Click here to Request ISA" OnClick="ISA_Onclick" Height="15px"
                Width="15px"></asp:ImageButton>
        </td>
       
    </tr>
 
    <tr>
        <td colspan="4">
        </td>
    </tr>
    </table>
    </asp:Panel>
     
    <asp:Panel ID="PnlNCDApplicationDetails" runat="server" class="Landscape" Width="100%" Height="80%"
    ScrollBars="None" >
    <table width="100%">
   <tr>
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Order Detail Section
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="5">
        </td>
    </tr>
    <tr>
    <td class="leftField" style="width: 20%">
            <asp:Label ID="lblApplicationNo" runat="server" Text="Application No: "
                CssClass="FieldName"></asp:Label>
        </td>
       <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtApplicationNo" runat="server" CssClass="txtField"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtApplicationNo"
                ErrorMessage="<br />Please Enter Application No" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
        </td>
        <td id="Td1" class="leftField" style="width: 20%" runat="server" visible="false">
        <asp:Label ID="lblDepository" runat="server" Text="Depository Type: "
                CssClass="FieldName"></asp:Label>
        </td>
        <td id="Td2" class="rightField" style="width: 20%" runat="server" visible="false">
        <asp:DropDownList ID="ddlDepositoryName" runat="server" CssClass="cmbField" AutoPostBack="true"></asp:DropDownList>
       
                 <asp:ImageButton ID="ImageddlSyndicate" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                AlternateText="Add" runat="server" ToolTip="Click here to Add Depository Type" 
                Height="15px" Width="15px"></asp:ImageButton>
            <br />
       <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="ddlDepositoryName"
                ErrorMessage="<br />Please Enter Depository Name" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
        </td>
        
    </tr>
    <%--<tr>
    <td class="leftField" style="width: 20%">
    <asp:Label ID="lblDPId" runat="server" Text="DP ID: "
                CssClass="FieldName"></asp:Label>
    </td>
     <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtDPId" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        
        <td class="leftField" style="width: 20%">
        <asp:Label ID="Label4" runat="server" Text="DP ClientId: "
                CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
        <asp:TextBox ID="txtDPIDClientId" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>--%>
    
    
    <tr>
    <td class="leftField" style="width: 20%">
    <asp:Label ID="Label16" runat="server" Text="Mode of Payment:" CssClass="FieldName"></asp:Label>
    </td>
    <td class="rightField" style="width: 20%">
    <asp:DropDownList ID="ddlPaymentMode" runat="server" AutoPostBack="true" CssClass="cmbField" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged">
     <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
    <asp:ListItem Text="Cheque/Demand Draft" Value="CQ"></asp:ListItem>
    <asp:ListItem Text="ASBA" Value="ES" ></asp:ListItem>
    </asp:DropDownList>
     <span id="Span10" class="spnRequiredField">*</span>
            
            <asp:CompareValidator ID="CompareValidator13" runat="server" ControlToValidate="ddlPaymentMode"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select  Mode Of Payment"
                Operator="NotEqual" ValidationGroup="btnConfirmOrder" ValueToCompare="Select"></asp:CompareValidator>
    </td>
    <td class="leftField">
    <asp:Label ID="lblAmount" Text="Amount" runat="server" CssClass="FieldName"></asp:Label>
    </td>
    <td class="rightField">
    <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField" ReadOnly="true" ></asp:TextBox>
    
    </td>
    <td>
        </td>
    </tr>
    <tr id="trPINo" runat="server" visible="false">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblPaymentNumber" runat="server" Text="Cheque/Demand Draft NO: "
                CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtPaymentNumber" runat="server" MaxLength="6" CssClass="txtField"
                TabIndex="16"></asp:TextBox>
            <span id="Span12" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtPaymentNumber"
                ErrorMessage="<br />Please Enter Cheque/Demand Draft NO." Display="Dynamic"
                runat="server" CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblPIDate" runat="server" Text="Cheque Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <telerik:RadDatePicker ID="txtPaymentInstDate" CssClass="txtField" runat="server"
                Culture="English (United States)" Skin="Telerik" EnableEmbeddedSkins="false"
                ShowAnimation-Type="Fade" MinDate="1900-01-01" TabIndex="17">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <span id="Span11" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator14" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtPaymentInstDate" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic" ValidationGroup="btnConfirmOrder" Enabled="true"></asp:CompareValidator>
           
        </td>
       </tr>
     <tr id="trASBA"  runat="server" visible="false">
    <td class="leftField">
    <asp:Label ID="lblASBANo" Text="ASBA Bank A/c NO:" runat="server" CssClass="FieldName" OnKeypress="javascript:return isNumberKey(event);"></asp:Label>
    </td>
     <td class="rightField">
     <asp:TextBox ID="txtASBANO" runat="server" CssClass="txtField" ></asp:TextBox>
      <span id="Span6" class="spnRequiredField">*</span>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtASBANO"
                ErrorMessage="<br />Please Enter Account No." Display="Dynamic"
                runat="server" CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
     </td>
     <td colspan="3"></td>
     </tr>
        <tr id="trBankName" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblBankName" runat="server" Text="Bank Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlBankName" runat="server" CssClass="cmbField" AutoPostBack="true"
                AppendDataBoundItems="true" OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged"
                TabIndex="18">
            </asp:DropDownList>
            <span id="Span8" class="spnRequiredField">*</span>
            <asp:ImageButton ID="imgAddBank" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                AlternateText="Add" runat="server" ToolTip="Click here to Add Bank" OnClientClick="return openpopupAddBank()"
                Height="15px" Width="15px" visible="false"></asp:ImageButton>
            <%-- --%>
            <asp:ImageButton ID="imgBtnRefereshBank" ImageUrl="~/Images/refresh.png" AlternateText="Refresh"
                runat="server" ToolTip="Click here to refresh Bank List" OnClick="imgBtnRefereshBank_OnClick"
                OnClientClick="return closepopupAddBank()" Height="15px" Width="25px" TabIndex="19" visible="false">
            </asp:ImageButton>
            <asp:CompareValidator ID="CompareValidator18" runat="server" ControlToValidate="ddlBankName"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a Bank"
                Operator="NotEqual" ValidationGroup="btnConfirmOrder" ValueToCompare="Select"></asp:CompareValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ControlToValidate="ddlBankName"
                CssClass="rfvPCG" ErrorMessage="<br />Please select an Bank" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
       
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblBranchName" runat="server" Text="Bank BranchName:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtBranchName" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span10" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtBranchName"
                CssClass="rfvPCG" ErrorMessage="<br />Please Enter Bank Branch" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
        </td>
    </tr>
    
    </table>
    </asp:Panel>
     <asp:Panel ID="pnlDematAccount" runat="server" class="Landscape" Width="100%" Height="80%">
   <table width="100%" cellspacing="10" >
      <tr>
        <td>
            <div class="divSectionHeading" style="vertical-align: text-bottom">
               Demat Details
            </div>
        </td>
    </tr>
    <tr>
    <td align="right">
     <asp:ImageButton ID="imgDemat" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                AlternateText="Add Demat Account" runat="server" ToolTip="Click here to Add Demat Account" OnClientClick="return openpopupAddDematAccount()"
            Height="15px" Width="15px" TabIndex="3"></asp:ImageButton>
            </td>
    </tr>
    <tr>
    <td>
    <asp:Panel ID="pnlDematGv" runat="server" class="Landscape" Width="100%" Height="100px"
   ScrollBars="Vertical" Visible="false">
        <telerik:RadGrid ID="gvDematDetailsTeleR" runat="server" 
            AllowAutomaticInserts="false" AllowFilteringByColumn="true" AllowPaging="true" 
            AllowSorting="true" AutoGenerateColumns="False" EnableEmbeddedSkins="false" 
            EnableHeaderContextMenu="true" fAllowAutomaticDeletes="false" GridLines="none" 
            ShowFooter="false" ShowStatusBar="false" Skin="Telerik">
            <%--<HeaderContextMenu EnableEmbeddedSkins="False">
                                </HeaderContextMenu>--%>
            <ExportSettings HideStructureColumns="true">
            </ExportSettings>
            <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false" 
                DataKeyNames="CEDA_DematAccountId" Width="99%">
                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                <Columns>
                    <telerik:GridTemplateColumn AllowFiltering="false" DataField="Action" 
                        HeaderStyle-Width="30px" UniqueName="Action">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkDematId" runat="server" />
                        </ItemTemplate>
                       
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                        CurrentFilterFunction="Contains" DataField="CEDA_DPName" 
                        FilterControlWidth="50px" HeaderStyle-Width="67px" HeaderText="DP Name" 
                        ShowFilterIcon="false" SortExpression="CEDA_DPName" UniqueName="CEDA_DPName">
                        <HeaderStyle Width="67px" />
                        <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="100px" 
                            Wrap="false" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                        CurrentFilterFunction="Contains" DataField="CEDA_DepositoryName" 
                        FilterControlWidth="120px" HeaderStyle-Width="140px" 
                        HeaderText="Depository Name" ShowFilterIcon="false" 
                        SortExpression="CEDA_DepositoryName" UniqueName="CEDA_DepositoryName">
                        <HeaderStyle Width="140px" />
                        <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="67px" 
                            Wrap="false" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                        CurrentFilterFunction="Contains" DataField="CEDA_DPClientId" 
                        FilterControlWidth="50px" HeaderStyle-Width="67px" 
                        HeaderText="Beneficiary Acct No" ShowFilterIcon="false" 
                        SortExpression="CEDA_DPClientId" UniqueName="CEDA_DPClientId">
                        <HeaderStyle Width="80px" />
                        <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="100px" 
                            Wrap="false" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                        CurrentFilterFunction="Contains" DataField="CEDA_DPId" 
                        FilterControlWidth="50px" HeaderStyle-Width="67px" HeaderText="DP Id" 
                        ShowFilterIcon="false" SortExpression="CEDA_DPId" UniqueName="CEDA_DPId">
                        <HeaderStyle Width="140px" />
                        <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="140px" 
                            Wrap="false" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                        DataField="XMOH_ModeOfHolding" HeaderStyle-Width="145px" 
                        HeaderText="Mode of holding" ShowFilterIcon="false" 
                        SortExpression="XMOH_ModeOfHolding" UniqueName="XMOH_ModeOfHolding">
                        <HeaderStyle Width="145px" />
                        <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="145px" 
                            Wrap="false" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                        DataField="CEDA_AccountOpeningDate" DataFormatString="{0:dd/MM/yyyy}" 
                        HeaderStyle-Width="145px" HeaderText="Account Opening Date" 
                        ShowFilterIcon="false" SortExpression="CEDA_AccountOpeningDate" 
                        UniqueName="CEDA_AccountOpeningDate" Visible="false">
                        <HeaderStyle Width="145px" />
                        <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="145px" 
                            Wrap="false" />
                    </telerik:GridBoundColumn>
                </Columns>
                <EditFormSettings>
                    <EditColumn CancelImageUrl="Cancel.gif" EditImageUrl="Edit.gif" 
                        InsertImageUrl="Update.gif" UpdateImageUrl="Update.gif">
                    </EditColumn>
                </EditFormSettings>
            </MasterTableView>
            <ClientSettings>
                <Scrolling AllowScroll="true" ScrollHeight="50px" UseStaticHeaders="True" />
            </ClientSettings>
        </telerik:RadGrid>
</asp:Panel>
       </td>
    
    </tr>
    
    
    </table>
    </asp:Panel>
    
    
   <asp:Panel ID="pnlJointHolderNominee" runat="server" class="Landscape" Width="100%" Height="80%"
    ScrollBars="None"  >
   <table width="100%"  cellspacing="10">
      <tr>
        <td colspan="3">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
               Joint Holder/Nominee Details
            </div>
        </td>
    </tr>
    
  <tr  >
  <td class="style1">
  <asp:Label ID="lblJointDetails" runat="server" text="Joint Holder Details" CssClass="HeaderTextSmall"></asp:Label>
  </td>
  <td>
  <asp:Label ID="lblNominneeDetails" runat="server" Text="Nominee Details" CssClass="HeaderTextSmall"></asp:Label>
  </td>
  
  </tr>
  <tr>
  <td style="width:50%">
  <asp:Panel ID="pnlJointHolder" runat="server" class="Landscape" Width="100%" Height="100px"
   ScrollBars="Vertical" Visible="false">
     <telerik:RadGrid ID="gvJointHolder" runat="server" AllowSorting="True" enableloadondemand="True"
                                PageSize="10" AllowPaging="false" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                                GridLines="None" ShowFooter="false" PagerStyle-AlwaysVisible="true" ShowStatusBar="True"
                                Skin="Telerik" AllowFilteringByColumn="false">
                                <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames="AssociateId"
                                    AutoGenerateColumns="false" Width="100%">
                                    
                                    <Columns>
                                    <telerik:GridTemplateColumn  AllowFiltering="false" DataField="Action" 
                                     HeaderStyle-Width="30px" UniqueName="Action">
                                        <ItemTemplate>
                                       <asp:CheckBox ID="chkjnthld" runat="server" OnCheckedChanged="chkjnthld_OnCheckedChanged" AutoPostBack="true"/> 
                                        </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="AssociateName" HeaderStyle-Width="200px" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Joint Holder Name" UniqueName="AssociateName"
                                            SortExpression="AssociateName">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="20px" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AssociationType" HeaderStyle-Width="60px"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            HeaderText="Associate Type" UniqueName="AssociationType" SortExpression="AssociationType">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="20px" Wrap="true" />
                                        </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn DataField="AssociateId" HeaderStyle-Width="200px" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Nominee Name" UniqueName="AssociateId"
                                            SortExpression="AssociateId"  Visible="false">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="20px" Wrap="false" />
                                            </telerik:GridBoundColumn>
                                                                           </Columns>
                                                                           
                                </MasterTableView>
                              <%-- <ClientSettings>
                                <Scrolling  AllowScroll="true" UseStaticHeaders="true" ScrollHeight="100px" />
                            </ClientSettings>--%>
                            </telerik:RadGrid>
                             
    </asp:Panel>
  </td>
  <td style="width:50%">
   <asp:Panel ID="pnlNominee" runat="server" class="Landscape" Width="100%" Height="100px"  ScrollBars="Vertical" Visible="false">
    <telerik:RadGrid ID="gvNominee" runat="server" AllowSorting="True" enableloadondemand="True"
                                PageSize="10" AllowPaging="false" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                                GridLines="None" ShowFooter="false" PagerStyle-AlwaysVisible="true" ShowStatusBar="True"
                                Skin="Telerik" AllowFilteringByColumn="false">
                                <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames="AssociateId"
                                    AutoGenerateColumns="false" Width="100%">
                                    <Columns>
                                        <telerik:GridTemplateColumn AllowFiltering="false" DataField="Action" 
                        HeaderStyle-Width="30px" UniqueName="Action" >
                                        <ItemTemplate >
                                       <asp:CheckBox ID="ChkNominee" runat="server" /> 
                                        </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="AssociateName" HeaderStyle-Width="200px" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Nominee Name" UniqueName="AssociateName"
                                            SortExpression="AssociateName">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="20px" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AssociateId" HeaderStyle-Width="200px" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Nominee Name" UniqueName="AssociateId"
                                            SortExpression="AssociateId" Visible="false">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="20px" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AssociationType" HeaderStyle-Width="60px"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            HeaderText="Associate Type" UniqueName="AssociationType" SortExpression="AssociationType">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="20px" Wrap="true" />
                                        </telerik:GridBoundColumn>
                                        </Columns>
                                </MasterTableView>
                                <%--<ClientSettings>
                                <Scrolling  AllowScroll="true" UseStaticHeaders="true" ScrollHeight="100px" />
                            </ClientSettings>--%>
                            </telerik:RadGrid>
    </asp:Panel>
  </td>
  
  </tr>
  <tr id="trModeOfHolding" runat="server">
                <td style="width: 20%" align="right">
                    <asp:Label ID="lblModeOfHolding" runat="server" CssClass="FieldName" Text="Mode Of Holding:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:DropDownList ID="ddlModeofHOldingFI" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                    <span id="Span6" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />Please select a Mode of Holding"
                        ControlToValidate="ddlModeofHOldingFI" Operator="NotEqual" ValueToCompare="Select"
                        Display="Dynamic" CssClass="cvPCG" SetFocusOnError="true"  Enabled="false" ValidationGroup="btnConfirmOrder"></asp:CompareValidator>
                         
                </td>
              <td></td>
            </tr>
           
             
         
    
</table>
</asp:Panel>
   <asp:Panel ID="pnlIPOOrder"  runat="server" class="Landscape" Width="100%" Height="80%"
    ScrollBars="None" >
    <table width="100%">
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
    <tr>
    <td class="leftField">
    <asp:Label ID="lblIssueName" runat="server" Text="Select Issue Name:" CssClass="FieldName"></asp:Label>
    </td>
    <td class="rightField">
    <asp:DropDownList ID="ddlIssueList" runat="server" AutoPostBack="true" CssClass="cmbExtraLongField" OnSelectedIndexChanged="ddlIssueList_OnSelectedIndexChanged"></asp:DropDownList>
    <asp:RequiredFieldValidator ID="rfvIssueList" runat="server" ControlToValidate="ddlIssueList" ErrorMessage="Please select the Issue Name" Display="Dynamic"  ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
    </td>
    <td  colspan="4">
    </td>
    </tr>
    </table>
    </asp:Panel>

<asp:UpdatePanel ID="UpdatePanel2" runat="server" >
    <ContentTemplate>
            <asp:Panel ID="pnlIPOControlContainer" runat="server" ScrollBars="Horizontal" Width="100%" Visible="false">
            <div id="divControlContainer" class="divControlContiner" runat="server">
                <table width="100%">
                    <tr>
                        <td colspan="4">
                            <telerik:RadGrid ID="RadGridIPOIssueList" runat="server" AllowSorting="True" enableloadondemand="True"
                                PageSize="10" AllowPaging="false" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                                GridLines="None" ShowFooter="false" PagerStyle-AlwaysVisible="false" ShowStatusBar="True"
                                Skin="Telerik" AllowFilteringByColumn="false">
                                <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames="AIM_IssueId,AIIC_PriceDiscountType,AIM_IsMultipleApplicationsallowed,AIIC_PriceDiscountValue,AIM_CutOffTime,AIM_TradingInMultipleOf,AIM_MInQty,AIM_MaxQty,AIIC_MInBidAmount,AIIC_MaxBidAmount,AIM_CloseDate"
                                    AutoGenerateColumns="false" Width="100%" PagerStyle-AlwaysVisible="false">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="AIM_IssueName" HeaderStyle-Width="200px" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="true" AutoPostBackOnFilter="true" HeaderText="Issue Name" UniqueName="AIM_IssueName"
                                            SortExpression="AIM_IssueName">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_IssueSizeQty" HeaderStyle-Width="200px" HeaderText="Issue Size"
                                            ShowFilterIcon="false" UniqueName="AIM_IssueSizeQty" Visible="true">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_IssueSizeAmt" HeaderStyle-Width="200px" HeaderText="Issue Size Amount"
                                            ShowFilterIcon="false" UniqueName="AIM_IssueSizeAmt" Visible="true">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_Rating" HeaderStyle-Width="200px" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true" HeaderText="Grading" UniqueName="AIM_Rating" SortExpression="AIM_Rating">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_IsBookBuilding" HeaderStyle-Width="200px"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Basis" UniqueName="AIM_IsBookBuilding"
                                            SortExpression="AIM_IsBookBuilding">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_FaceValue" HeaderStyle-Width="200px" HeaderText="Face Value"
                                            ShowFilterIcon="false" UniqueName="AIM_FaceValue" Visible="true">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_MInQty" HeaderStyle-Width="200px" HeaderText="Minimum Qty"
                                            ShowFilterIcon="false" UniqueName="AIM_MInQty" Visible="true">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_TradingInMultipleOf" HeaderStyle-Width="200px"
                                            HeaderText="Multiples of Qty" ShowFilterIcon="false" UniqueName="AIM_TradingInMultipleOf"
                                            Visible="true">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_MaxQty" HeaderStyle-Width="200px" HeaderText="Maximum Qty"
                                            ShowFilterIcon="false" UniqueName="AIM_MaxQty" Visible="true">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_FloorPrice" HeaderStyle-Width="200px" HeaderText="Min Bid Price"
                                            ShowFilterIcon="false" UniqueName="AIM_FloorPrice" Visible="true" DataType="System.Decimal"
                                            DataFormatString="{0:0.00}">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_CapPrice" HeaderStyle-Width="200px" HeaderText="Max Bid Price"
                                            ShowFilterIcon="false" UniqueName="AIM_CapPrice" Visible="true" DataType="System.Decimal"
                                            DataFormatString="{0:0.00}">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_FixedPrice" HeaderStyle-Width="200px" HeaderText="Max Bid Price"
                                            Visible="false" ShowFilterIcon="false" UniqueName="AIM_FixedPrice">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIIC_MInBidAmount" HeaderStyle-Width="200px"
                                            HeaderText="Min Bid Amount" ShowFilterIcon="false" UniqueName="AIIC_MInBidAmount"
                                            Visible="true" DataType="System.Decimal" DataFormatString="{0:0.00}">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIIC_MaxBidAmount" HeaderStyle-Width="200px"
                                            HeaderText="Max Bid Amount" Visible="true" ShowFilterIcon="false" UniqueName="AIIC_MaxBidAmount">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_OpenDate" HeaderStyle-Width="200px" HeaderText="Open Date"
                                            ShowFilterIcon="false" UniqueName="AIM_OpenDate" Visible="true">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_CloseDate" HeaderStyle-Width="200px" HeaderText="Close Date"
                                            ShowFilterIcon="false" UniqueName="AIM_CloseDate" Visible="true">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DiscountType" HeaderStyle-Width="200px" HeaderText="Discount Type"
                                            ShowFilterIcon="false" UniqueName="DiscountType" Visible="true">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DiscountValue" HeaderStyle-Width="200px" HeaderText="Discount Value/Bid Qty"
                                            ShowFilterIcon="false" UniqueName="DiscountValue" Visible="true" DataFormatString="{0:0.00}">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:ValidationSummary ID="vsSummary" runat="server" CssClass="rfvPCG" Visible="true"
                                ValidationGroup="btnConfirmOrder" ShowSummary="true" DisplayMode="BulletList" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <telerik:RadGrid ID="RadGridIPOBid" runat="server" AllowSorting="True" enableloadondemand="True"
                                PageSize="10" AllowPaging="false" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                                GridLines="None" ShowFooter="true" PagerStyle-AlwaysVisible="false" ShowStatusBar="True"
                                Skin="Telerik" AllowFilteringByColumn="false" OnItemDataBound="RadGridIPOBid_ItemDataBound">
                                <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" AutoGenerateColumns="false"
                                    DataKeyNames="IssueBidNo" Width="100%" PagerStyle-AlwaysVisible="false">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="BidOptions" HeaderStyle-Width="120px" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="true" AutoPostBackOnFilter="true" HeaderText="Bidding Options"
                                            UniqueName="BidOptions" SortExpression="BidOptions">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="80px"
                                            Visible="true" UniqueName="CheckCutOff" HeaderText="Cut-Off" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbCutOffCheck" runat="server" Visible='<%# (Convert.ToInt32(Eval("IssueBidNo")) == 1)? true: false %>'
                                                    AutoPostBack="true" OnCheckedChanged="CutOffCheckBox_Changed" />
                                                <%-- <a href="#" class="popper" data-popbox="divCutOffCheck">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>
                                                <div id="divCutOffCheck" class="popbox">
                                                    <h2>
                                                        CUT-OFF!</h2>
                                                    <p>
                                                        1)If this box is checked then price filed will auto fill with Max Bid Price.</p>
                                                </div>--%>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px"
                                            UniqueName="BidQuantity" HeaderText="Quantity" FooterAggregateFormatString="{0:N2}"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtBidQuantity" runat="server" Text='<%# Bind("BidQty")%>' CssClass="txtField"
                                                    OnTextChanged="BidQuantity_TextChanged" AutoPostBack="true" onkeypress="return isNumberKey(event)"> </asp:TextBox>
                                                <a href="#" class="popper" data-popbox="divBidQuantity" style="display: none">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>
                                                <div id="divBidQuantity" class="popbox">
                                                    <h2>
                                                        BID-QUANTITY!</h2>
                                                    <p>
                                                        1)Please enter value between MinQuantity and MaxQuantity.</p>
                                                </div>
                                                <asp:RangeValidator ID="rvQuantity" runat="server" ControlToValidate="txtBidQuantity"
                                                    ValidationGroup="btnConfirmOrder" Type="Integer" CssClass="rfvPCG" Text="*" ErrorMessage="BidQuantity should be between MinQuantity and MaxQuantity"
                                                    Display="Dynamic" />
                                                <asp:RegularExpressionValidator ID="revtxtBidQuantity" ControlToValidate="txtBidQuantity"
                                                    runat="server" ErrorMessage="Please enter a valid bid quantity" Text="*" Display="Dynamic"
                                                    ValidationExpression="[0-9]*" CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RegularExpressionValidator>
                                                <asp:CustomValidator ID="CVBidQtyMultiple" runat="server" 
                                                    OnServerValidate="CVBidQtyMultiple_ServerValidate" Text="*" ErrorMessage="Please enter Quantity in multiples permissibile for this issue"
                                                    ControlToValidate="txtBidQuantity" Display="Dynamic" ValidationGroup="btnConfirmOrder"
                                                    CssClass="rfvPCG">                                                
                                                </asp:CustomValidator>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px"
                                            FooterText="" UniqueName="BidPrice" HeaderText="Price" FooterAggregateFormatString="{0:N2}"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtBidPrice" runat="server" CssClass="txtField" Text='<%# Bind("BidPrice")%>'
                                                    AutoPostBack="true" OnTextChanged="BidPrice_TextChanged" onkeypress="return isNumberKey(event)"> </asp:TextBox>
                                                <a href="#" class="popper" data-popbox="divBidPrice" style="display: none">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>
                                                <div id="divBidPrice" class="popbox">
                                                    <h2>
                                                        BID-PRICE!</h2>
                                                    <p>
                                                        1)Please enter value between Min Bid Price and Min Max Price.
                                                        <br />
                                                        2)In case of cutoff cheked Max Bid price will be use for same field</p>
                                                </div>
                                                <asp:RangeValidator ID="rvBidPrice" runat="server" ControlToValidate="txtBidPrice"
                                                    ValidationGroup="btnConfirmOrder" Type="Double" CssClass="rfvPCG" Text="*" ErrorMessage="BidPrice should be between Min Bid Price and Min Max Price"
                                                    Display="Dynamic" />
                                                <asp:RegularExpressionValidator ID="revtxtBidPrice" ControlToValidate="txtBidPrice"
                                                    runat="server" ErrorMessage="Please enter a valid bid price" Text="*" Display="Dynamic"
                                                    ValidationExpression="^\d+(\.\d{1,2})?$" CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblBidHighestValue" Text="Highest Bid Value"></asp:Label>
                                            </FooterTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px"
                                            FooterText="" UniqueName="BidAmountPayable" HeaderText="Amount Payable" FooterAggregateFormatString="{0:N2}"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtBidAmountPayable" runat="server" ReadOnly="true" CssClass="txtDisableField"
                                                    Text='<%# Bind("BidAmountPayable")%>'></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblFinalBidAmountPayable" Text="0"></asp:Label>
                                                <asp:TextBox ID="txtFinalBidValue" runat="server" CssClass="txtField" Text="0" Visible="false">
                                                </asp:TextBox>
                                            </FooterTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px"
                                            FooterText="" UniqueName="BidAmount" HeaderText="Amount Bid" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtBidAmount" runat="server" ReadOnly="true" CssClass="txtDisableField"
                                                    Text='<%# Bind("BidAmount")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                    <tr class="spaceUnder" id="trTermsCondition" runat="server">
                        <td align="left" style="width: 100%" colspan="4">
                            <asp:CheckBox ID="chkTermsCondition" runat="server" Font-Bold="True" Font-Names="Shruti"
                                Enabled="false" Checked="false" ForeColor="#145765" Text="" ToolTip="Click 'Terms & Conditions' to proceed further"
                                CausesValidation="true" />
                            <asp:LinkButton ID="lnkTermsCondition" CausesValidation="false" Text="Terms & Conditions"
                                runat="server" CssClass="txtField" OnClick="lnkTermsCondition_Click" ToolTip="Click here to accept terms & conditions"></asp:LinkButton>
                            <span id="Span9" class="spnRequiredField">*</span>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" Text="Please read terms & conditions"
                                ClientValidationFunction="ValidateTermsConditions" EnableClientScript="true"
                                OnServerValidate="TermsConditionCheckBox" Display="Dynamic" ValidationGroup="btnTC"
                                CssClass="rfvPCG">
                               Please read terms & conditions
                            </asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%">
                            <asp:Button ID="btnConfirmOrder" runat="server" Text="Confirm Order" OnClick="btnConfirmOrder_Click"
                                CssClass="PCGMediumButton" ValidationGroup="btnConfirmOrder, btnTC" OnClientClick="return TestCheckBox(); PreventClicks(); Validate(); " />
                        </td>
                        <td>
                            <asp:LinkButton runat="server" ID="lnlBack" CssClass="LinkButtons" Text="Click here to view the issue list"
                                Visible="false" OnClick="lnlktoviewIPOissue_Click"></asp:LinkButton>
                        </td>
                        <%--<td colspan="2" style="width: 90%">
                        </td>--%>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <telerik:RadWindow ID="rwTermsCondition" runat="server" VisibleOnPageLoad="false"
            Width="1000px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Behaviors="Move, Resize,Close"
            Title="Terms & Conditions" EnableShadow="true" Left="580" Top="-8">
            <ContentTemplate>
                <div style="padding: 0px; width: 100%">
                    <table width="100%" cellpadding="0" cellpadding="0">
                        <tr>
                            <td align="left">
                                <%--  <a href="../ReferenceFiles/MF-Terms-Condition.html">../ReferenceFiles/MF-Terms-Condition.html</a>--%>
                                <iframe src="../ReferenceFiles/IPO-Terms-Condition.htm" name="iframeTermsCondition"
                                    style="width: 100%"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnAccept" runat="server" Text="Accept" CssClass="PCGButton" OnClick="btnAccept_Click"
                                    CausesValidation="false" ValidationGroup="btnConfirmOrder" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
        <telerik:RadWindowManager runat="server" ID="RadWindowManager1">
            <Windows>
                <telerik:RadWindow ID="rw_customConfirm" Modal="true" Behaviors="Close, Move" VisibleStatusbar="false"
                    Width="700px" Height="160px" runat="server" Title="EUIN Confirm">
                    <ContentTemplate>
                        <div class="rwDialogPopup radconfirm">
                            <div class="rwDialogText">
                                <asp:Label ID="confirmMessage" Text="" runat="server" />
                            </div>
                            <div>
                                <asp:Button runat="server" ID="rbConfirm_OK" Text="OK" OnClick="rbConfirm_OK_Click"
                                    ValidationGroup="btnConfirmOrder" OnClientClick="return PreventClicks();"></asp:Button>
                                <asp:Button runat="server" ID="rbConfirm_Cancel" Text="Cancel" OnClientClicked="closeCustomConfirm">
                                </asp:Button>
                            </div>
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
    </ContentTemplate>
    <Triggers>
    </Triggers>
</asp:UpdatePanel>

    <asp:HiddenField ID="txtTotAmt" runat="server"  />
    <asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="txtCustomerId_ValueChanged1" />
<asp:HiddenField ID="hdnCustomerId" runat="server" />
<asp:HiddenField ID="hdnType" runat="server" />
<asp:HiddenField ID="hdnSchemeCode" runat="server"  />
<asp:HiddenField ID="hdnPortfolioId" runat="server" />
<asp:HiddenField ID="hdnAccountId" runat="server" />
<asp:HiddenField ID="hdnAmcCode" runat="server" />
<asp:HiddenField ID="hdnSchemeName" runat="server" />
<asp:HiddenField ID="hdnSchemeSwitch" runat="server" />
<asp:HiddenField ID="hdnBankName" runat="server" />
<asp:HiddenField ID="hdnIsSubscripted" runat="server" />
<asp:HiddenField ID="txtSwitchSchemeCode" runat="server" />
<asp:HiddenField ID="txtAgentId" runat="server" OnValueChanged="txtAgentId_ValueChanged1" />
<asp:HiddenField ID="hdnAplicationNo" runat="server" OnValueChanged="txtAgentId_ValueChanged1" />
<asp:HiddenField ID="hidValidCheck" runat="server" EnableViewState="true" />
<asp:HiddenField ID="HiddenField1" runat="server" OnValueChanged="HiddenField1_ValueChanged1" />
<asp:HiddenField ID="hidAmt" runat="server"   />
