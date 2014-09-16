<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IPOIssueTransactOffline.ascx.cs" Inherits="WealthERP.OffLineOrderManagement.IPOIssueTransactOffline" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

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
<script type="text/javascript">
    function ValidateTermsConditions(sender, args) {

        if (document.getElementById("<%=chkTermsCondition.ClientID %>").checked == true) {
            args.IsValid = true;
        } else {
            args.IsValid = false;
        }
    }
</script>

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
                            NCD Order Entry
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
    <table>
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
        <td colspan="2">
        </td>
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
        <td colspan="2">
        </td>
    </tr>
    
    <tr id="trCust" runat="server">
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
        <td colspan="4">
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
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblAssociatetext" runat="server" CssClass="FieldName" Enabled="false"></asp:Label>
        </td>
       
    </tr>
    <tr id="trIsa" runat="server">
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
    </tr>
 
    <tr>
        <td colspan="5">
        </td>
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
