<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FixedIncome54ECOrderEntry.ascx.cs"
    Inherits="WealthERP.OffLineOrderManagement.FixedIncome54ECOrderEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script type="text/javascript">
    function Confirm() {
        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (confirm("Do you want to save data?")) {
            confirm_value.value = "Yes";
        } else {
            confirm_value.value = "No";
        }
        document.forms[0].appendChild(confirm_value);
    }
</script>

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
    function TriggeredKey(e) {
        var keycode;
        if (window.event) keycode = window.event.keyCode;
        if (window.event.keyCode = 13) return false;
    }
    
</script>

<script type="text/javascript" language="javascript">


    function GetCustomerId(source, eventArgs) {
        isItemSelected = true;
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();

        return false;
    }
    function GeAgentId(source, eventArgs) {
        isItemSelected = true;
        document.getElementById("<%= txtAgentId.ClientID %>").value = eventArgs.get_value();

        return false;
    }



    function openpopupAddCustomer() {

        window.open('PopUp.aspx?AddMFCustLinkId=mf&pageID=CustomerType&', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')


    }
    function openpopupAddDematAccount() {

        var customerId = document.getElementById("<%=txtCustomerId.ClientID %>").value;
        var customerPortfolioId = document.getElementById("<%=hdnPortfolioId.ClientID %>").value;
        if (customerId != 0) {
            window.open('PopUp.aspx?PageId=AddDematAccountDetails&CustomerId=' + customerId + '&CustomerPortfolioId=' + customerPortfolioId, 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')

        }
        else {
            alert("Please Select the Customer From Search")
        }
    }

    function ValidateAssociateName() {
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
                alert("Please select PAN from the PAN list only");
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





    }
    function ShowInitialIsa() {

    }
    function CheckSubscription() {


    }
</script>

<script type="text/javascript">

    var isValidFolio = false;


    function ValidateFolioSelection(txtFolioNuber) {

        var returnValue = true;
        if (!isValidFolio) {

            if (txtFolioNuber.value != "") {
                txtFolioNuber.focus();
                alert("Please select valid folio");
                txtFolioNuber.value = "";
                returnValue = false;
            }
        } else {
            if (txtFolioNuber.value != "")
                alert("Valid folio found");
        }
        return returnValue;


    }

</script>

<script language="javascript" type="text/javascript">
    var crnt = 0;
    function PreventClicks(btnsubmit) {

        if (typeof (Page_ClientValidate('btnSubmit')) == 'function') {
            Page_ClientValidate();

        }

        if (Page_IsValid) {
            if (++crnt > 1) {
                // alert(crnt);
                return false;
            }
            return true;
        }
        else {
            return false;
        }
    }
</script>

<script type="text/javascript">
    function setCustomPosition(sender, args) {
        sender.moveTo(sender.get_left(), sender.get_top());
    }
</script>

<table width="100%">
    <tr>
        <td align="left">
            <%--<div class="divPageHeading" style="vertical-align: text-bottom">
      Order Entry
      </div>--%>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr id="Tr1" runat="server">
                        <td align="left">
                            FD/54EC & NPS Order Entry
                        </td>
                        <td align="right">
                            <%--   <asp:LinkButton ID="LinkButton1" runat="server"  CausesValidation="false"  Text="test"   OnClick ="lnlFIBack_Click"/>
                        
                         <asp:Button ID="Button2" runat="server"  CausesValidation="false" CssClass="DOCButton"
                                Visible="true" OnClick ="lnlFIBack_Click" />--%>
                            <%--<asp:LinkButton ID="lnkTest" runat="server"    CausesValidation="false"  Text="test"   OnClick ="lnlFIBack_Click"/>--%>
                            <%--     <asp:Button ID="Button1" runat="server"  CssClass="DOCButton"
                                Visible="true" OnClick ="lnlFIBack_Click" />--%>
                            <%--<asp:LinkButton ID="lnkBtnnews" runat="server"    OnClick="lnkBtnEdit_Click"  Text="news" CssClass="LinkButtons" />--%>
                            <asp:LinkButton ID="lnkBtnFIEdit" runat="server" CssClass="LinkButtons" Text="Edit"
                                CausesValidation="false" Visible="false" OnClick="lnkBtnFIEdit_Click"></asp:LinkButton>
                            &nbsp; &nbsp;
                            <asp:LinkButton runat="server" ID="lnlFIBack" Text="Back" Visible="false" CausesValidation="false"
                                CssClass="LinkButtons"></asp:LinkButton>&nbsp; &nbsp;
                            <asp:LinkButton runat="server" ID="lnkDelete" CssClass="LinkButtons" Text="Delete"
                                CausesValidation="false" Visible="false" OnClientClick="javascript: return confirm('Are you sure you want to Delete the Order?')"></asp:LinkButton>&nbsp;
                            &nbsp;
                            <asp:Button ID="btnViewReport" runat="server" PostBackUrl="~/Reports/Display.aspx?mail=0"
                                Visible="false" CssClass="CrystalButton" ValidationGroup="MFSubmit" OnClientClick="return CustomerValidate('View')" />&nbsp;&nbsp;
                            <div id="div1" style="display: none;">
                                <p class="tip">
                                    Click here to view order details.
                                </p>
                            </div>
                            <asp:Button ID="btnViewInPDF" runat="server" ValidationGroup="MFSubmit" OnClientClick="return CustomerValidate('pdf')"
                                PostBackUrl="~/Reports/Display.aspx?mail=2" CssClass="PDFButton" Visible="false" />&nbsp;&nbsp;
                            <asp:Button ID="btnreport" runat="server" CssClass="CrystalButton" Visible="false" />
                            <asp:Button ID="btnpdfReport" runat="server" CssClass="PDFButton" Visible="false" />
                            <div id="div2" style="display: none;">
                                <p class="tip">
                                    Click here to view order details.
                                </p>
                            </div>
                            <asp:Button ID="btnViewInDOC" runat="server" ValidationGroup="MFSubmit" CssClass="DOCButton"
                                OnClientClick="return CustomerValidate('doc')" PostBackUrl="~/Reports/Display.aspx?mail=4"
                                Visible="false" />
                            <div id="div3" style="display: none;">
                                <p class="tip">
                                    Click here to view order details in word doc.</p>
                            </div>
                            <asp:Button ID="btnViewInPDFNew" runat="server" ValidationGroup="MFSubmit" CssClass="PDFButton"
                                Visible="false" OnClientClick="return CustomerValidate('pdf')" PostBackUrl="~/Reports/Display.aspx?mail=2" />
                            <asp:Button ID="btnViewInDOCNew" runat="server" ValidationGroup="MFSubmit" CssClass="DOCButton"
                                Visible="false" OnClientClick="return CustomerValidate('doc')" PostBackUrl="~/Reports/Display.aspx?mail=4" />
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
    </tr>
</table>
<telerik:RadWindow ID="rwDematDetails" runat="server" VisibleOnPageLoad="false" Height="30%"
    Width="400px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Behaviors="None"
    Title="Select Demat" Top="120" Left="70" RestrictionZoneID="radWindowZone" OnClientShow="setCustomPosition">
    <ContentTemplate>
        <table>
            <tr>
                <td>
                    <telerik:RadGrid ID="gvDematDetailsTeleR" runat="server" AllowAutomaticInserts="false"
                        AllowPaging="true" AllowSorting="true" AutoGenerateColumns="False" Height="150px"
                        EnableEmbeddedSkins="false" EnableHeaderContextMenu="true" fAllowAutomaticDeletes="false"
                        GridLines="none" ShowFooter="false" ShowStatusBar="false" Skin="Telerik">
                        <ExportSettings HideStructureColumns="true">
                        </ExportSettings>
                        <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false" DataKeyNames="CEDA_DematAccountId,CEDA_DPClientId"
                            Width="99%">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="false" DataField="Action" HeaderStyle-Width="30px"
                                    UniqueName="Action">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDematId" runat="server" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" DataField="CEDA_DPName" HeaderStyle-Width="67px"
                                    HeaderText="DP Name" ShowFilterIcon="false" SortExpression="CEDA_DPName" UniqueName="CEDA_DPName">
                                    <HeaderStyle Width="67px" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="100px" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataField="CEDA_DepositoryName" HeaderStyle-Width="140px" HeaderText="Depository Name"
                                    ShowFilterIcon="false" SortExpression="CEDA_DepositoryName" UniqueName="CEDA_DepositoryName">
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="67px" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataField="CEDA_DPClientId" HeaderStyle-Width="67px" HeaderText="Beneficiary Acct No"
                                    ShowFilterIcon="false" SortExpression="CEDA_DPClientId" UniqueName="CEDA_DPClientId">
                                    <HeaderStyle Width="120px" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="100px" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataField="CEDA_DPId" HeaderStyle-Width="67px" HeaderText="DP Id" ShowFilterIcon="false"
                                    SortExpression="CEDA_DPId" UniqueName="CEDA_DPId">
                                    <HeaderStyle Width="140px" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="140px" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" DataField="XMOH_ModeOfHolding"
                                    HeaderStyle-Width="145px" HeaderText="Mode of holding" ShowFilterIcon="false"
                                    SortExpression="XMOH_ModeOfHolding" UniqueName="XMOH_ModeOfHolding">
                                    <HeaderStyle Width="145px" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="145px" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" DataField="CEDA_AccountOpeningDate"
                                    DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="145px" HeaderText="Account Opening Date"
                                    ShowFilterIcon="false" SortExpression="CEDA_AccountOpeningDate" UniqueName="CEDA_AccountOpeningDate"
                                    Visible="false">
                                    <HeaderStyle Width="145px" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="145px" Wrap="false" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <EditFormSettings>
                                <EditColumn CancelImageUrl="Cancel.gif" EditImageUrl="Edit.gif" InsertImageUrl="Update.gif"
                                    UpdateImageUrl="Update.gif">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <ClientSettings>
                            <Scrolling AllowScroll="true" ScrollHeight="70px" UseStaticHeaders="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnAddDemat" runat="server" Text="Accept" CssClass="PCGButton" OnClick="btnAddDemat_Click"
                        CausesValidation="false" OnClientClick="javascript:return  TestCheckBox();" />
                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="PCGButton" OnClick="btnClose_Click"
                        CausesValidation="false" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</telerik:RadWindow>
<table width="100%">
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
        <td align="right">
            <asp:Label ID="lblsearch" runat="server" CssClass="FieldName" Text="Search for:"></asp:Label>
        </td>
        <td style="width: 23.5%">
            <asp:DropDownList ID="ddlsearch" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlsearch_Selectedindexchanged"
                AutoPostBack="true" TabIndex="0">
                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                <asp:ListItem Text="Customer" Value="1"></asp:ListItem>
                <asp:ListItem Text="PAN" Value="2"></asp:ListItem>
            </asp:DropDownList>
            <span id="Span14" class="spnRequiredField">*</span>
        </td>
        <td align="right" runat="server" visible="false">
            <asp:Label ID="lblARNNo" runat="server" CssClass="FieldName" Text="ARN No:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlARNNo" runat="server" CssClass="cmbField" AutoPostBack="false"
                TabIndex="1" Visible="false">
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator12" runat="server" ControlToValidate="ddlARNNo"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an ARN"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare=""></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trpan" runat="server" visible="false">
        <td align="right">
            <asp:Label ID="lblPansearch" runat="server" Text="PAN:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtPansearch" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" onclientClick="ShowIsa()" onblur="return checkItemSelected(this)"
                TabIndex="2">
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
                ErrorMessage="<br />Please Enter PAN" Display="Dynamic" runat="server" CssClass="rfvPCG"
                ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
        </td>
        <td align="right" style="width: 15.4%;">
            <asp:Label ID="label2" runat="server" Text="Customer Name: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblgetcust" runat="server" Text="" CssClass="FieldName" onclientClick="CheckPanno()"></asp:Label>
        </td>
    </tr>
    <tr id="trCust" runat="server" visible="false">
        <td align="right">
            <asp:Label ID="lblCustomer" runat="server" Text="Customer Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
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
                CssClass="rfvPCG" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
        </td>
        <td align="right" style="width: 15.4%;">
            <asp:Label ID="lblPan" runat="server" Text="PAN:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblgetPan" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
        <td align="right" runat="server" visible="false">
            <asp:Label ID="lblRM" runat="server" Text="RM: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblGetRM" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblBranch" runat="server" Text="Branch: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblGetBranch" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
        <td align="right" runat="server" visible="false">
            <asp:Label ID="Label1" runat="server" Text="EUIN: " CssClass="FieldName"></asp:Label>
        </td>
        <td runat="server" visible="false">
            <asp:Label ID="lb1EUIN" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblAssociateSearch" runat="server" CssClass="FieldName" Text="Sub Broker Code:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtAssociateSearch" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" TabIndex="4">
            </asp:TextBox><span id="Span7" class="spnRequiredField">*</span>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" TargetControlID="txtAssociateSearch"
                WatermarkText="Enter few chars of Sub Broker Code" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtAssociateSearch"
                ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                OnClientItemSelected="GeAgentId" MinimumPrefixLength="1" EnableCaching="False"
                CompletionSetCount="5" CompletionInterval="100" CompletionListCssClass="AutoCompleteExtender_CompletionList"
                CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem" CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" DelimiterCharacters="" Enabled="True" ShowOnlyCurrentWordInCompletionListItem="true" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtAssociateSearch"
                ErrorMessage="<br />Please Enter a Sub Broker Code" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblAssociate" runat="server" CssClass="FieldName" Text="Associate:"
                Visible="false"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblAssociatetext" runat="server" CssClass="FieldName" Enabled="false"></asp:Label>
        </td>
        <td>
        </td>
    </tr>
    <tr id="trTaxStatus" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label5" runat="server" Text="Customer Tax Status: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlTax" runat="server" CssClass="cmbField" AutoPostBack="true">
            </asp:DropDownList>
            <%--<asp:TextBox ID="txtTax" runat="server" CssClass="txtField" AutoComplete="Off" ReadOnly="true"   />  --%>
        </td>
        <td colspan="3">
        </td>
    </tr>
</table>
<table id="Table1" runat="server" width="100%">
    <tr>
        <td colspan="6">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Order Section Details
            </div>
        </td>
    </tr>
    <tr id="trCatIss" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label7" runat="server" Text="Category: " CssClass="FieldName"></asp:Label>
        </td>
        <td style="width: 20%">
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" Enabled="true" Width="200px">
            </asp:DropDownList>
            <span id="SpanddlCategory" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidatorddlCategory" runat="server" ControlToValidate="ddlCategory"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select Category"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td style="width: 5%">
        </td>
        <td id="Td2" align="right" style="width: 15%" runat="server" visible="false">
            <asp:Label ID="Label8" runat="server" Text="Issuer:" CssClass="FieldName"></asp:Label>
        </td>
        <td id="Td3" style="width: 50%" align="left" runat="server" visible="false">
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
            <asp:Label ID="Label9" runat="server" Text="Select Issue: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="4">
            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" Width="600px">
            </asp:DropDownList>
            <span id="SpanddlScheme" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="ddlScheme"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Scheme"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td align="right" style="width: 10%">
            <asp:Label ID="Label10" runat="server" Text="Series:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 35%">
            <asp:DropDownList ID="ddlSeries" runat="server" CssClass="cmbField" AutoPostBack="true"
                Width="150px" OnSelectedIndexChanged="ddlSeries_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="SpanddlSeries" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidatorSeries" runat="server" ControlToValidate="ddlSeries"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Series"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td style="width: 5%">
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="Label11" runat="server" Text="Transaction Type: " CssClass="FieldName"></asp:Label>
        </td>
        <td style="width: 35%">
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
    </tr>
    <tr id="TRTransSer" runat="server">
        <td align="right" style="width: 10%">
            <asp:Label ID="Label15" runat="server" Text="Series Details: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="2">
            <table class="SchemeInfoTable" width="100%">
                <tr style="width: 100%;">
                    <td align="right">
                        <asp:Label ID="lblTenure" runat="server" CssClass="FieldName" Text="Tenure:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblTenureRate" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblCoupen" runat="server" CssClass="FieldName" Text="Coupon Rate:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblCoupenRate" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblFace" runat="server" CssClass="FieldName" Text="Face Value:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblFaceValue" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblMinQty" runat="server" CssClass="FieldName" Text="Min. Qty:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblMinQuentity" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblMaxQty" runat="server" CssClass="FieldName" Text="Max. Qty:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblMaxQuentity" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:TextBox ID="txtSeries" runat="server" CssClass="txtField" ReadOnly="true" Visible="false"></asp:TextBox>
            <asp:Label ID="Label12" runat="server" CssClass="txtField"></asp:Label>
        </td>
    </tr>
    <tr id="trSchemeOpFreq" runat="server" visible="false">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label16" runat="server" Text="Scheme Option: " CssClass="FieldName"></asp:Label>
        </td>
        <td style="width: 20%">
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
        <td style="width: 5%">
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="Label17Msg" runat="server" Text="Frequency: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 35%">
            <asp:DropDownList ID="ddlFrequency" runat="server" CssClass="cmbLongField" AutoPostBack="true"
                Width="150px">
                <asp:ListItem Text="Select" Value="Select" Selected="true"></asp:ListItem>
                <asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem>
                <asp:ListItem Text="Quarterly" Value="Quarterly"></asp:ListItem>
                <asp:ListItem Text="yearly" Value="Yearly"></asp:ListItem>
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
    <tr id="trDepPaypriv" runat="server" visible="false">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label21" runat="server" Text="Deposit Payable to:" CssClass="FieldName"></asp:Label>
        </td>
        <td style="width: 20%">
            <asp:CheckBox ID="ChkFirstholder" runat="server" CssClass="txtField" Text="First holder">
            </asp:CheckBox>
            <asp:CheckBox ID="ChkEORS" runat="server" CssClass="txtField" Text="Either or survivor">
            </asp:CheckBox>
        </td>
        <td style="width: 5%">
        </td>
        <td align="right" style="width: 10%">
            <asp:Label ID="Label22" runat="server" Text="Privilege:" CssClass="FieldName"></asp:Label>
        </td>
        <td style="width: 35%">
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
            <asp:Label ID="lblOrderNumber" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
        <td style="width: 20%">
            <asp:Label ID="lblGetOrderNo" runat="server" Text="" CssClass="txtField"></asp:Label>
        </td>
        <td style="width: 5%">
        </td>
        <td id="Td4" class="leftField" style="width: 10%" visible="false" runat="server">
            <asp:Label ID="lblOrderDate" runat="server" Text="Order Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td id="Td5" style="width: 35%" visible="false" runat="server">
            <telerik:RadDatePicker ID="txtOrderDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                AutoPostBack="true">
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
            <asp:RequiredFieldValidator ID="RequiredFieldValidator41" ControlToValidate="txtOrderDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select order date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trARDate" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblApplicationNumber" runat="server" Text="Application No.:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtApplicationNumber" runat="server" CssClass="txtField" AutoPostBack="true"
                MaxLength="8"></asp:TextBox>
            <%--<asp:RegularExpressionValidator ID="regFrom" ControlToValidate="txtApplicationNumber"
                runat="server" Display="Dynamic" ErrorMessage="<br/>Enter Numeric Value Between 8  Digit"
                CssClass="cvPCG" ValidationExpression="[A-Z]{1,8}$" ValidationGroup="MFSubmit" />--%>
        </td>
        <td style="width: 5%">
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="Label13" runat="server" Text="Application Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 35%">
            <telerik:RadDatePicker ID="txtApplicationDate" CssClass="txtField" runat="server"
                Culture="English (United States)" Skin="Telerik" EnableEmbeddedSkins="false"
                ShowAnimation-Type="Fade" MinDate="1900-01-01" AutoPostBack="true">
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
        </td>
    </tr>
    <tr id="Tr3" runat="server" visible="false">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label14" runat="server" Text="Maturity Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td style="width: 20%">
            <telerik:RadDatePicker ID="txtMaturDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
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
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtMaturDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select Maturity Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
        </td>
        <td style="width: 5%">
        </td>
        <td colspan="2" style="width: 35%" align="center">
            <asp:Label ID="LabelMsg" runat="server" Style="color: red;"></asp:Label>
        </td>
    </tr>
    <tr id="trDepRen" runat="server" visible="false">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label16Msg" runat="server" Text="Existing Deposit Receipt No.:" CssClass="FieldName"></asp:Label>
        </td>
        <td style="width: 20%">
            <asp:TextBox ID="txtExistDepositreceiptno" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" />
        </td>
        <td style="width: 5%">
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="Label17" runat="server" Text="Renewal Amount:" CssClass="FieldName"
                OnTextChanged="OnPayAmtTextchanged"></asp:Label>
        </td>
        <td style="width: 35%">
            <asp:TextBox ID="txtRenAmt" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" />
        </td>
    </tr>
    <tr id="trQtyAndAmt" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lb1Qty" runat="server" Text="Qty:" CssClass="FieldName">           
            </asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtQty" runat="server" CssClass="txtField" AutoPostBack="True" OnTextChanged="OnQtychanged" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtQty"
                runat="server" Display="Dynamic" ErrorMessage="Please Enter Integer Value" CssClass="cvPCG"
                ValidationExpression="[1-9]\d*$" ValidationGroup="MFSubmit">     
            </asp:RegularExpressionValidator>
            <span id="Span8" runat="server" class="spnRequiredField">*</span>
            <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPayAmt"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please Enter FD Amount"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>--%>
            <%-- <asp:RequiredFieldValidator ID="ReqQty" ControlToValidate="txtQty" CssClass="rfvPCG"
                ErrorMessage="<br />Please Enter Qty" Display="Dynamic" runat="server" InitialValue=""
                ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>--%>
        </td>
        <td style="width: 5%">
        </td>
        <td align="right" style="width: 15%">
            <asp:Label ID="lb1PurchaseAmt" runat="server" Text="Purchase Amount:" CssClass="FieldName"></asp:Label>
        </td>
        <td style="width: 30%">
            <asp:TextBox ID="TxtPurAmt" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" OnTextChanged="OnAmtchanged" />
            <span id="Span3" runat="server" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="ReqQty" ControlToValidate="txtQty" CssClass="rfvPCG"
                ErrorMessage="<br />Please Enter Qty" Display="Dynamic" runat="server" InitialValue=""
                ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="TxtPurAmt"
                runat="server" Display="Dynamic" ErrorMessage="Please Enter Integer Value" CssClass="cvPCG"
                ValidationExpression="[1-9]\d*$" ValidationGroup="MFSubmit">     
            </asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr id="trMatAmtDate" runat="server" visible="false">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label19" runat="server" Text="FD Amount:" CssClass="FieldName">           
            </asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtPayAmt" runat="server" CssClass="txtField" OnTextChanged="OnPayAmtTextchanged"
                AutoPostBack="True" />
            <span id="SpanPayAmt" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidatorPayamt" runat="server" ControlToValidate="txtPayAmt"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please Enter FD Amount"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtPayAmt"
                CssClass="rfvPCG" ErrorMessage="<br />Please Enter FD Amount" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
        </td>
        <td style="width: 5%">
        </td>
        <td align="right" style="width: 15%">
            <asp:Label ID="Label20Msg" runat="server" Text="Maturity Amount:" CssClass="FieldName"></asp:Label>
        </td>
        <td style="width: 30%">
            <asp:TextBox ID="txtMatAmt" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" />
        </td>
    </tr>
</table>
<table id="Table3" width="100%" runat="server">
    <tr>
        <td>
            <asp:Panel ID="Panel1" runat="server" class="Landscape" Width="100%" Height="100%"
                ScrollBars="None" Visible="false">
                <table width="100%" cellspacing="10">
                    <tr>
                        <td colspan="3">
                            <div class="divSectionHeading" style="vertical-align: text-bottom">
                                Demat Details
                            </div>
                        </td>
                    </tr>
                    <tr id="tdlnkbtn" runat="server">
                        <td class="leftField" style="width: 20%">
                            <asp:LinkButton ID="lnkBtnDemat" runat="server" OnClick="lnkBtnDemat_onClick" CssClass="LinkButtons"
                                Text="Click to select Demat Details" CausesValidation="false"></asp:LinkButton>
                        </td>
                        <td id="Td1" class="rightField" style="width: 20%" colspan="2">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                                AlternateText="Add Demat Account" runat="server" ToolTip="Click here to Add Demat Account"
                                OnClientClick="return openpopupAddDematAccount()" Height="15px" Width="15px"
                                TabIndex="3"></asp:ImageButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftField" style="width: 25%">
                            <asp:Label ID="lblDpClientId" runat="server" Text="Beneficiary Acct No:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField" style="width: 20%">
                            <asp:TextBox ID="txtDematid" Enabled="false" onkeydown="return (event.keyCode!=13);"
                                runat="server" CssClass="txtField"></asp:TextBox>
                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtDematid"
                                ErrorMessage="<br />Please Select Demat from the List" Display="Dynamic" runat="server"
                                CssClass="rfvPCG" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>--%>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlJointHolderNominee" runat="server" class="Landscape" Width="100%"
                Height="80%" ScrollBars="None" Visible="false">
                <table width="100%" cellspacing="10">
                    <tr>
                        <td>
                            <div class="divSectionHeading" style="vertical-align: text-bottom">
                                Joint Holder/Nominee Details
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="gvAssociate" runat="server" CssClass="RadGrid" GridLines="Both"
                                Visible="false" Width="90%" AllowPaging="True" PageSize="20" AllowSorting="True"
                                AutoGenerateColumns="false" ShowStatusBar="true" Skin="Telerik">
                                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                    FileName="Family Associates" Excel-Format="ExcelML">
                                </ExportSettings>
                                <MasterTableView DataKeyNames="CDAA_Id,CEDA_DematAccountId,CDAA_Name,CDAA_PanNum,Sex,CDAA_DOB,RelationshipName,AssociateType,CDAA_AssociateTypeNo,CDAA_IsKYC,SexShortName,CDAA_AssociateType,XR_RelationshipCode"
                                    Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemSettings-ShowRefreshButton="false">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="CDAA_Name" HeaderText="Member name" UniqueName="AssociateName"
                                            SortExpression="AssociateName">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AssociateType" HeaderText="Associate Type" UniqueName="AssociateType"
                                            SortExpression="AssociateType">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CDAA_PanNum" HeaderText="PAN Number" UniqueName="CDAA_PanNum"
                                            SortExpression="CDAA_PanNum">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CDAA_IsKYC" HeaderText="IsKYC" UniqueName="CDAA_IsKYC"
                                            SortExpression="CDAA_IsKYC">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CDAA_DOB" HeaderText="Date Of Birth" UniqueName="CDAA_DOB"
                                            SortExpression="CDAA_DOB" DataFormatString="{0:d}">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="RelationshipName" HeaderText="Relationship" AllowFiltering="false"
                                            UniqueName="RelationshipName" SortExpression="RelationshipName">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
<table id="Table2" width="100%" runat="server">
    <tr id="trSection1" runat="server">
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Payment Details
            </div>
        </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
    <tr id="trAmount" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblMode" runat="server" Text="Mode Of Payment:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlPaymentMode" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged">
                <asp:ListItem Text="Cheque" Value="CQ" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Draft" Value="DF"></asp:ListItem>
                <asp:ListItem Text="ECS" Value="ES"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="leftField" style="width: 20%" visible="false" runat="server">
            <asp:Label ID="lblAmount" runat="server" Text="Amount:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%" visible="false" runat="server">
            <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr id="trPINo" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblPaymentNumber" runat="server" Text="Payment Instrument No.:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtPaymentNumber" runat="server" MaxLength="6" CssClass="txtField"></asp:TextBox>
            <span id="Span5" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtPaymentNumber"
                ErrorMessage="<br />Please Enter Instrument Number" Display="Dynamic" runat="server"
                CssClass="rfvPCG" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
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
            <span id="Span6" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtPaymentInstDate"
                ErrorMessage="<br />Please Enter Instrument Date" Display="Dynamic" runat="server"
                CssClass="rfvPCG" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CVPaymentDate" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtPaymentInstDate" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trBankName" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblBankName" runat="server" Text="Bank Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlBankName" runat="server" CssClass="cmbField" AutoPostBack="true"
                CausesValidation="true">
            </asp:DropDownList>
            <span id="Span4" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="ddlBankName"
                ErrorMessage="<br />Please Select Bank from the List" Display="Dynamic" runat="server"
                CssClass="rfvPCG" InitialValue="Select" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
            <asp:ImageButton ID="imgAddBank" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                AlternateText="Add" runat="server" ToolTip="Click here to Add Bank" OnClientClick="return openpopupAddBank()"
                Height="15px" Width="15px" Visible="false"></asp:ImageButton>
            <asp:ImageButton ID="imgBtnRefereshBank" ImageUrl="~/Images/refresh.png" AlternateText="Refresh"
                runat="server" ToolTip="Click here to refresh Bank List" Height="15px" Width="25px"
                Visible="false"></asp:ImageButton>
            <%--<asp:CompareValidator ID="CompareValidator11" runat="server" ControlToValidate="ddlBankName"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a Bank"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>--%>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblBranchName" runat="server" Text="Bank Branch Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtBranchName" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr id="trDepositedBank" runat="server" visible="false">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label3" runat="server" Text="Application Submitted at Bank name:"
                CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%" visible="false">
            <asp:DropDownList ID="ddlDepoBank" runat="server" CssClass="cmbField" AutoPostBack="true">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlDepoBank"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a Bank"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td class="leftField" style="width: 20%" visible="false">
            <asp:Label ID="Label4" runat="server" Text="Application Submitted at Branch:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%" visible="false">
            <asp:TextBox ID="txtDepositedBranch" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr id="trBtnSubmit" runat="server">
        <td align="left" colspan="3">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" OnClick="btnSubmit_Click"
                ValidationGroup="MFSubmit" />
            <asp:Button ID="btnAddMore" runat="server" Text="Save & AddMore" CssClass="PCGMediumButton"
                Visible="false" ValidationGroup="MFSubmit" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="PCGButton" ValidationGroup="MFSubmit"
                Visible="false" OnClick="btnUpdate_Click" />
        </td>
    </tr>
    <tr id="trDocumentSec" runat="server">
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Documents Submitted
            </div>
        </td>
    </tr>
    <tr>
        <%--  <td>
            <asp:Button ID="BtnFileupload" runat="server" Text="Upload Documents" CssClass="PCGButton"
                ValidationGroup="MFSubmit" OnClick="BtnFileupload_Click" Visible="false" />
        </td>--%>
    </tr>
</table>
<table width="68%" id="tbUploadDocument" runat="server" visible="false">
    <tr>
        <td>
            <telerik:RadGrid ID="gvUploadDocument" runat="server" AllowSorting="True" enableloadondemand="True"
                PageSize="5" AutoGenerateColumns="False" EnableEmbeddedSkins="False" GridLines="None"
                ShowFooter="True" PagerStyle-AlwaysVisible="true" AllowPaging="true" ShowStatusBar="True"
                Skin="Telerik" AllowFilteringByColumn="true" OnItemCommand="gvUploadDocument_OnItemCommand"
                OnNeedDataSource="gvUploadDocument_OnNeedDataSource" OnItemDataBound="gvUploadDocument_OnItemDataBound">
                <MasterTableView DataKeyNames="COD_DocumentId,COD_image,XPRT_ProofTypeCode,XPRT_ProofType"
                    AllowFilteringByColumn="true" Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                    CommandItemDisplay="Top" EditMode="PopUp">
                    <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                        AddNewRecordText="Add Document" ShowExportToCsvButton="false" ShowAddNewRecordButton="true"
                        ShowRefreshButton="false" />
                    <Columns>
                        <%-- <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" Visible="false">
                        </telerik:GridEditCommandColumn>--%>
                        <telerik:GridEditCommandColumn EditText="Edit" UniqueName="editColumn" CancelText="Cancel"
                            UpdateText="Update" HeaderStyle-Width="80px" Visible="false">
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="COD_DocumentId" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="DocumentId" UniqueName="COD_DocumentId"
                            SortExpression="COD_DocumentId" AllowFiltering="true" Visible="false">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="XPRT_ProofType" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Proof Type" UniqueName="XPRT_ProofType"
                            SortExpression="XPRT_ProofType" AllowFiltering="true">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn DataField="COD_image" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="File" UniqueName="COD_image"
                            SortExpression="COD_image" AllowFiltering="true">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDownload" runat="server" CommandName="download_file" Text='<%#Eval("COD_image") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete?"
                            ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                            Text="Delete">
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" Width="70px" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings EditFormType="Template" PopUpSettings-Height="180px" PopUpSettings-Width="500px"
                        CaptionFormatString="Upload Document">
                        <FormTemplate>
                            <table width="100%">
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblProoftype" runat="server" Text="Proof Type:" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlProofType" AutoPostBack="true" runat="server" CssClass="cmbField"
                                            OnSelectedIndexChanged="ddlProofType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <span id="Span3" class="spnRequiredField">*</span>
                                        <br />
                                        <asp:RequiredFieldValidator ID="ReqddlLevel" runat="server" CssClass="rfvPCG" ErrorMessage="Please Select Proof Type"
                                            Display="Dynamic" ControlToValidate="ddlProofType" ValidationGroup="btnOK" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblUpload" runat="server" Text="Upload:" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td>
                                        <span style="font-size: xx-small">(Allowed extensions are: .jpg,.jpeg,.bmp,.png,.pdf)</span>
                                        <telerik:RadUpload ID="radUploadProof" runat="server" ControlObjectsVisibility="None"
                                            AutoPostBack="true" AllowedFileExtensions=".jpg,.jpeg,.bmp,.png,.pdf" Skin="Telerik"
                                            EnableEmbeddedSkins="true">
                                        </telerik:RadUpload>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Button ID="btnOK" Text="Submit" runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                            CausesValidation="True" ValidationGroup="btnOK" />
                                    </td>
                                    <td class="rightData">
                                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                            CssClass="PCGButton" CommandName="Cancel"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </FormTemplate>
                    </EditFormSettings>
                </MasterTableView>
            </telerik:RadGrid>
        </td>
    </tr>
</table>
<asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="txtCustomerId_ValueChanged1" />
<asp:HiddenField ID="txtAgentId" runat="server" OnValueChanged="txtAgentId_ValueChanged1" />
<asp:HiddenField ID="hdnDefaulteInteresRate" runat="server" />
<asp:HiddenField ID="hdnSeriesDetails" runat="server" />
<asp:HiddenField ID="hdnMinQty" runat="server" />
<asp:HiddenField ID="hdnButtonAction" runat="server" />
<asp:HiddenField ID="hdnMaxQty" runat="server" />
<asp:HiddenField ID="hdnPortfolioId" runat="server" />
