<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BondsOrderEntry.ascx.cs"
    Inherits="WealthERP.OPS.BondsOrderEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    };
</script>

<%--<script type="text/javascript">
    function popup() {
        window.open('OPS/AddAssetIssuer.aspx', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
    }
</script>

<script type="text/javascript">
    function popupScheme() {
        window.open('OPS/AddAssetScheme.aspx', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
    }
</script>--%>

<script>
    function openpopup() {
        window.open('PopUp.aspx?PageId=CustomerType', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
        return false;
    }

    function openpopupAddBank() {
        window.open('PopUp.aspx?PageId=AddBankDetails', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
        return false;
    }

    function openpopupAddCustomer() {
        window.open('PopUp.aspx?PageId=CustomerType', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
        return false;
    }

    function DownloadScript() {
        var btn = document.getElementById('<%= btnInsertBondParticular.ClientID %>');
        btn.click();
    }

    function InsertIssuerNameScript() {
        var btn = document.getElementById('<%= btnInsertBondIssuerName.ClientID %>');
        btn.click();
    }
</script>
<table width="100%">
    <tr>
        <td>
            <asp:Label Text="ORDER ENTRY-INFRASTRUCTURE BONDS" CssClass="HeaderTextBig" runat="server"
                ID="lblOrderEntry"></asp:Label>
        </td>
    </tr>
</table>
<table width="100%">
<%--    <tr>
        <td  colspan="2">
            <asp:Label Text="ORDER ENTRY-INFRASTRUCTURE BONDS" CssClass="HeaderTextBig" runat="server"
                ID="lblOrderEntry"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="5">
        </td>
    </tr>--%>
    <tr>
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Customer Details
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width:10%">
            <asp:Label ID="lblCustomer" runat="server" Text="Customer: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width:30%">
            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True">
            </asp:TextBox><span id="spnCustomer" class="spnRequiredField">*</span>
            <asp:Button ID="btnAddCustomer" runat="server" Text="Add a Customer" CssClass="PCGMediumButton"
                CausesValidation="true" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_OrderEntry_btnAddCustomer','M');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_OrderEntry_btnAddCustomer','M');"
                OnClientClick="return openpopupAddCustomer()" />
                
            <cc1:TextBoxWatermarkExtender ID="txtCustomer_water" TargetControlID="txtCustomerName" WatermarkText="Enter few chars of Customer"
            runat="server" EnableViewState="false"></cc1:TextBoxWatermarkExtender>
            
            <ajaxToolkit:AutoCompleteExtender ID="txtCustomerName_autoCompleteExtender" runat="server"
                TargetControlID="txtCustomerName" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                Enabled="True" />
            <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style='font-size: 9px; font-weight: normal'
                class='FieldName'>
                <br />
                Enter few characters of Individual customer name. </span>--%>
            <asp:RequiredFieldValidator ID="rfvCustomerName" ControlToValidate="txtCustomerName"
                ErrorMessage="<br />Please Enter Customer Name" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
        </td>
        <td class="leftField" style="width:10%">
            <asp:Label ID="lblRM" runat="server" Text="RM: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width:10%">
            <asp:Label ID="lblGetRM" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
        <td style="width:10%">
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width:25%">
            <asp:Label ID="lblBranch" runat="server" Text="Branch: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField"  style="width:30%">
            <asp:Label ID="lblGetBranch" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
        <td>            
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width:25%">
            <asp:Label ID="lblPan" runat="server" Text="PAN No: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField"  style="width:30%">
            <asp:Label ID="lblgetPan" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
        <td class="leftField" style="width:10%" >
            <asp:Label ID="lblEmail" runat="server" Text="Email: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblGetEmail" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <%--</table>
<table width="100%">--%>
    <tr>
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Account Details
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblAssetIssuer" CssClass="FieldName" Text="Asset Issuer:"  runat="server"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlBondsIssuer" CssClass="cmbField" runat="server" AutoPostBack="true"
                onselectedindexchanged="ddlBondsIssuer_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:Button runat="server" ID="btnAddNewAssetIssuer" CssClass="PCGButton" Text="AddNew" />
        </td>
        <td class="leftField">
            <asp:Label ID="lblAssetParticulars" CssClass="FieldName" Text="Asset Particulars/Scheme:" runat="server"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlAssetPerticular" runat="server" CssClass="cmbField">
            <%--<asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>--%>
            </asp:DropDownList>
            <asp:Button runat="server" CssClass="PCGButton" ID="btnAddNewAssetParticulars" Text="AddNew" />
        </td>
    </tr>
    <tr>
        <td class="leftField" colspan="3">
            <asp:Label ID="Label3" CssClass="FieldName" runat="server" Text="Mode of holding:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList CssClass="cmbField" runat="server" ID="ddlModeOfHolding">
            </asp:DropDownList>
        </td>
    </tr>
    
    <asp:UpdatePanel ID="UPNomineeJointHoldingGrid" runat="server">
        <ContentTemplate>
            <tr>
                <td align="right">
                    <asp:Label ID="lblIsHeldJointly" runat="server" Text="Joint Holding:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:Panel ID="Panel1" runat="server" Width="127px">
                        <asp:RadioButton ID="rbtnYes" runat="server" Text="Yes" GroupName="IsHeldJointly"
                            CssClass="txtField" AutoPostBack="True" OnCheckedChanged="RadioButton_CheckChanged" />
                        <asp:RadioButton ID="rbtnNo" runat="server" Text="No" GroupName="IsHeldJointly" CssClass="txtField"
                            AutoPostBack="True" OnCheckedChanged="RadioButton_CheckChanged" 
                            Checked="True" />
                    </asp:Panel>
                </td>
                <%--<td align="right">
                    <asp:Label ID="lblModeOfHolding" runat="server" Text="Mode of Holdings: " CssClass="FieldName"></asp:Label>
                </td>
                <td align="left" valign="top">
                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                </td>--%>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblPickJointHolder" runat="server" Text="Pick Joint Holder" CssClass="FieldName"></asp:Label>
                    <asp:GridView ID="gvPickJointHolder" runat="server" DataKeyNames="CA_AssociationId" Width="70%"
                        AutoGenerateColumns="False" CssClass="GridViewStyle">
                        <RowStyle CssClass="RowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="PJHCheckBox" runat="server" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="PJHCheckBox" runat="server" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Name" DataField="AssociateName" />
                            <asp:BoundField HeaderText="Relationship" DataField="XR_Relationship" />
                        </Columns>
                        <FooterStyle CssClass="FooterStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                    </asp:GridView>
                </td>
                <td colspan="2">
                    <asp:Label ID="lblPickNominee" runat="server" Text="Pick Nominee" CssClass="FieldName"></asp:Label>
                    <asp:GridView ID="gvPickNominee" runat="server" DataKeyNames="CA_AssociationId" AutoGenerateColumns="False"
                        CssClass="GridViewStyle">
                        <RowStyle CssClass="RowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="PNCheckBox" runat="server" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="PNCheckBox" runat="server" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Name" DataField="AssociateName" />
                            <asp:BoundField HeaderText="Relationship" DataField="XR_Relationship" />
                        </Columns>
                        <FooterStyle CssClass="FooterStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                    </asp:GridView>
                </td>
            </tr>
        </ContentTemplate>
    </asp:UpdatePanel>
    <tr id="trNoJointHolders" runat="server" visible="false">
        <td class="Message" colspan="5">
            <asp:Label ID="lblNoJointHolders" runat="server" Text="You have no Joint Holder"
                CssClass="FieldName"></asp:Label>
        </td>
    </tr>
   
    <tr>
        <td colspan="2">
            <asp:Button ID="btnAddAssociates" runat="server" CssClass="PCGLongButton" CausesValidation="false"
                autopostback="false" OnClientClick="return openpopup()" Text="Add customer/associate" />
        </td>
        <td colspan="2">
            <br />
            <br />
        </td>
    </tr>
    <%--</table>
<table>--%>
    <tr>
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Deposit Details
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label4" CssClass="FieldName" Text="Deposit Dates:" runat="server"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtDepositDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
        </td>
        <td class="leftField">
            <asp:Label CssClass="FieldName" ID="Label5" Text="Maturity Date:" runat="server"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtMaturityDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label6" CssClass="FieldName" Text="Face Value:" runat="server"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtFaceValue" CssClass="txtField" runat="server"></asp:TextBox>
        </td>
        <td colspan="2">
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label7" Text="Amount:" CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtAmount" CssClass="txtField" runat="server"></asp:TextBox>
        </td>
        <td class="leftField">
            <asp:Label ID="Label8" CssClass="FieldName" Text="Buy Back Facility:" runat="server"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlBuyBackFacility" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                <asp:ListItem Text="No" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label9" CssClass="FieldName" Text="Buy Back Date:" runat="server"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtBuyBackDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
        </td>
        <td class="leftField">
            <asp:Label ID="Label10" CssClass="FieldName" Text="Buy Back Amount" runat="server"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtBuyBackAmount" CssClass="txtField" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label11" CssClass="FieldName" Text="Premium Cycle:" runat="server"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlEPPremiumFrequencyCode" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td>
        </td>
        <td>
            <br />
            <br />
        </td>
    </tr>
    <%--</table>
<table>--%>
    <tr>
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Payment Details
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label12" CssClass="FieldName" Text="Mode Of Payment:" runat="server"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="cmbField">               
            </asp:DropDownList>
        </td>
        <td class="leftField">
            <asp:Label ID="Label13" CssClass="FieldName" Text="Instrument Date:" runat="server"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtInstrumentDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
        </td>
    </tr>
    <tr>
        <td colspan="2">
        </td>
        <td class="leftField">
            <asp:Label ID="Label14" CssClass="FieldName" Text="Instrument Number:" runat="server"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtPaymentInstrNo" CssClass="txtField" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label15" CssClass="FieldName" runat="server" Text="Bank Name:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlBankName" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged"></asp:DropDownList>
            <asp:Button ID="btnAddBank" runat="server" Text="Add Bank" CssClass="PCGButton" OnClientClick="return openpopupAddBank()" />
        </td>
        <td class="leftField">
            <asp:Label ID="lblBankBranchName" CssClass="FieldName" runat="server" Text="Branch Name:"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblBranchName" CssClass="FieldName" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <%--</table>
<table>--%>
    <tr>
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Other Details
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label17" runat="server" CssClass="FieldName" Text="Application Number:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtApplicationNo" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="leftField">
            <asp:Label ID="Label18" runat="server" CssClass="FieldName" Text="Received Date:"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtApplicationDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label19" runat="server" CssClass="FieldName" Text="Order Date:"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtOrderDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
        </td>
        <td class="leftField">
            <asp:Label ID="Label22" Text="Order Number:" CssClass="FieldName" runat="server"></asp:Label>
            
        </td>
        <td>
            <asp:TextBox CssClass="txtField" ID="txtOrderNumber" runat="server"></asp:TextBox>
        </td>
    </tr>
    
<asp:UpdatePanel ID="UpdatePanelRejectReason" runat="server">
<ContentTemplate>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label20" runat="server" CssClass="FieldName" Text="Order Status:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="cmbField" AutoPostBack="true"
            OnSelectedIndexChanged="ddlOrderStatus_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td class="leftField" style="width:10%">
            <asp:Label ID="lblReason" runat="server" Text="Reason: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField"  style="width:10%">
            <asp:DropDownList ID="ddlReason" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
</ContentTemplate>
</asp:UpdatePanel>
    
    
    
    <tr>
        <td></td>
    </tr>
<asp:UpdatePanel ID="UpdatePanelFormType" runat="server">
<ContentTemplate>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label21" Text="Form:" CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlForm" CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlForm_OnSelectedIndexChanged">
                <asp:ListItem Text="Select" Value="0">
                </asp:ListItem>
                <asp:ListItem Text="Dematerialised" Value="Dematerialised">                    
                </asp:ListItem>
                <asp:ListItem Text="Physical" Value="Physical">                
                </asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="leftField">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    
    <tr>
        <td class="leftField">
            <asp:Label CssClass="FieldName" ID="lblDematName" Text="Demat Name:" runat="server"></asp:Label>
        </td>
        <td>
            <asp:TextBox CssClass="txtField" ID="txtDematName" runat="server"></asp:TextBox>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label CssClass="FieldName" ID="lblDPID" Text="DPId:" runat="server"></asp:Label>
        </td>
        <td>
            <asp:TextBox CssClass="txtField" ID="txtDPID" runat="server"></asp:TextBox>
        </td>
        <td class="leftField">
            <asp:Label CssClass="FieldName" ID="lblBenificiaryAccountNumber" Text="Benificiary A\C No:" runat="server"></asp:Label>
        </td>
        <td>
            <asp:TextBox CssClass="txtField" ID="txtBenificiaryAccountNumber" runat="server"></asp:TextBox>
        </td>
    </tr>
</ContentTemplate>
</asp:UpdatePanel>
    <tr>
        <td>
            <asp:CheckBox ID="chkCustomerApproval" runat="server" Text="Customer approval required" CssClass="FieldName"/>
        </td>
        <td></td>
    </tr>
    <tr>
        <td>
            <asp:Button CssClass="PCGButton" runat="server" Text="Submit" ID="btnAllSubmit" 
                onclick="btnAllSubmit_Click" />
        </td>
        <td colspan="3">
        </td>
    </tr>
    <tr>
        <td>
            <%--<cc1:ModalPopupExtender ID="MPEAssetParticular" runat="server" PopupControlID="Panel2"
                TargetControlID="btnAddNewAssetParticulars" DynamicServicePath="" BackgroundCssClass="modalBackground"
                Enabled="True" OkControlID="btnOK" CancelControlID="btnCancel" Drag="true" PopupDragHandleControlID="Panel2"
                X="280" Y="235">
            </cc1:ModalPopupExtender>--%>
            
            <cc1:ModalPopupExtender ID="MPEAssetParticular" runat="server" TargetControlID="btnAddNewAssetParticulars"
                PopupControlID="Panel2" BackgroundCssClass="modalBackground" DropShadow="true" Drag="true"
                OkControlID="btnOk" OnOkScript="DownloadScript()" CancelControlID="btnCancel"
                PopupDragHandleControlID="Panel2" X="280" Y="235">
            </cc1:ModalPopupExtender>
        </td>
        <td>
            <asp:Panel ID="Panel3" runat="server" CssClass="ExortPanelpopup" Width="300px" Height="80px">
                <table width="100%" visible="false">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblAsset" runat="server" Text="Asset Issuer:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAsset" runat="server" CssClass="txtField"></asp:TextBox>
                            <span class="spnRequiredField">*</span>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnInsertIssuerName" runat="server" Text="OK" 
                                CssClass="PCGButton"/>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnCancelInsertIssuerName" runat="server" Text="Cancel" CssClass="PCGButton" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
        <td>
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel3"
                TargetControlID="btnAddNewAssetIssuer" DynamicServicePath="" BackgroundCssClass="modalBackground"
                Enabled="True" OkControlID="btnInsertIssuerName" OnOkScript="InsertIssuerNameScript()" CancelControlID="btnCancelInsertIssuerName" 
                Drag="true" PopupDragHandleControlID="Panel3" X="280" Y="235">
            </cc1:ModalPopupExtender>
        </td>
        <td>
            <asp:Panel ID="Panel2" runat="server" CssClass="ExortPanelpopup" Width="300px" Height="120px">
                <table width="100%" visible="false">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblAssetIssuerName" runat="server" Text="Asset Issuer:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblIssuerName" runat="server" Text="" CssClass="txtField"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblParticulars" runat="server" Text="Asset Particulars: " CssClass="FieldName"></asp:Label>
                        </td>
                        <td align="left">
                            <%----%>
                            <asp:TextBox ID="txtAssetParticular" runat="server" CssClass="txtField"></asp:TextBox>
                            <span class="spnRequiredField">*</span>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnOk" runat="server" Text="OK" CssClass="PCGButton"/>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="PCGButton" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
<asp:Button ID="btnInsertBondParticular" runat="server" Style="display: none"
    onclick="btnInsertBondParticular_Click" />
<asp:Button ID="btnInsertBondIssuerName" runat="server" Style="display: none"
    onclick="btnInsertBondIssuerName_Click" />

<asp:HiddenField ID="hdnCustomerId" runat="server" />
<asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="txtCustomerId_ValueChanged" />
<asp:HiddenField ID="hdnSchemeCode" runat="server" />
<asp:HiddenField ID="hdnType" runat="server" />
<asp:HiddenField ID="hdnDemateAccountId" runat="server" />
