<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LifeInsuranceOrderEntry.ascx.cs"
    Inherits="WealthERP.OPS.LifeInsuranceOrderEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
    }

    function DownloadScript() {
        var btn = document.getElementById('<%= btnInsertAsset.ClientID %>');
        btn.click();
    }
</script>

<script>
    function openpopupAddBank() {
        window.open('PopUp.aspx?PageId=AddBankDetails', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
        return false;
    }

    function openpopupAddCustomer() {
        window.open('PopUp.aspx?PageId=CustomerType', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
        return false;
    }
</script>

<table width="100%">
    <tr>
        <td style="width: 30%">
            <asp:Label ID="lblOrderEntry" runat="server" CssClass="HeaderTextBig" Text="Order Entry-Life Insurance"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:LinkButton runat="server" ID="lnkBtnEdit" CssClass="LinkButtons" Text="Edit"
                OnClick="lnkBtnEdit_Click"></asp:LinkButton>
            <asp:LinkButton ID="lnlBack" runat="server" CssClass="LinkButtons" Text="Back" OnClick="lnlBack_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Customer Details
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblCustomer" runat="server" Text="Customer Name: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 30%">
            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True">
            </asp:TextBox><span id="spnCustomer" class="spnRequiredField">*</span>
            <asp:Button ID="btnAddCustomer" runat="server" Text="Add a Customer" CssClass="PCGMediumButton"
                CausesValidation="true" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_OrderEntry_btnAddCustomer','M');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_OrderEntry_btnAddCustomer','M');"
                OnClientClick="return openpopupAddCustomer()"/>
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
            <%--<span style='font-size: 9%; font-weight: normal' class='FieldName'>
                <br />
                Enter few characters of customer name</span>--%>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCustomerName"
                ErrorMessage="<br />Please Enter Customer Name" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="Submit"></asp:RequiredFieldValidator>
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblRM" runat="server" Text="RM: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <asp:Label ID="lblGetRM" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
        <td style="width: 25%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblBranch" runat="server" Text="Branch: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 30%">
            <asp:Label ID="lblGetBranch" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblPan" runat="server" Text="PAN No: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 30%">
            <asp:Label ID="lblgetPan" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblEmail" runat="server" Text="Email: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblGetEmail" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Account Details
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" Text="Instrument Category:"></asp:Label>
        </td>
        <td class="rightField" style="width: 30%">
            <asp:DropDownList ID="ddlInstrumentCategory" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="cvInstrumentCategory" runat="server"
                ControlToValidate="ddlInstrumentCategory" ErrorMessage="Please select category"
                Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG" ValidationGroup="Submit">
            </asp:CompareValidator>
        </td>
        <td class="leftField" style="width: 10%">
            &nbsp;
        </td>
        <td class="rightField" style="width: 25%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblPolicyIssuer" runat="server" Text="Policy Issuer: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 30%">
            <asp:DropDownList ID="ddlPolicyIssuer" runat="server" CssClass="cmbField" AutoPostBack="True"
                OnSelectedIndexChanged="ddlPolicyIssuer_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="cvPolicyIssuer" runat="server"
                ControlToValidate="ddlPolicyIssuer" ErrorMessage="Please select a Policy Issuer"
                Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG" ValidationGroup="Submit">
            </asp:CompareValidator>
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblAssetPerticular" runat="server" Text="Asset Particulars: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <asp:DropDownList ID="ddlAssetPerticular" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <%--<asp:TextBox ID="txtAssetPerticular" runat="server" CssClass="txtField"></asp:TextBox>--%>
            <asp:Button ID="btnAddNew" runat="server" Text="Add New" CssClass="PCGButton" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <cc1:ModalPopupExtender ID="MPEAssetParticular" runat="server" TargetControlID="btnAddNew"
                PopupControlID="Panel2" BackgroundCssClass="modalBackground" DropShadow="true"
                OkControlID="btnOk" OnOkScript="DownloadScript()" CancelControlID="btnCancel"
                PopupDragHandleControlID="Panel2">
            </cc1:ModalPopupExtender>
        </td>
    </tr>
    <asp:UpdatePanel ID="UPNomineeJointHoldingGrid" runat="server">
        <ContentTemplate>
            <tr>
                <td class="leftField" style="width: 10%">
                    <asp:Label ID="lblIsHeldJointly" runat="server" Text="Joint Holding" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" style="width: 30%">
                    <asp:Panel ID="pnlHolding" runat="server" Width="127%">
                        <asp:RadioButton ID="rbtnYes" runat="server" Text="Yes" GroupName="IsHeldJointly"
                            CssClass="txtField" AutoPostBack="True" OnCheckedChanged="RadioButton_CheckChanged" />
                        <asp:RadioButton ID="rbtnNo" runat="server" Text="No" GroupName="IsHeldJointly" CssClass="txtField"
                            AutoPostBack="True" OnCheckedChanged="RadioButton_CheckChanged" OnLoad="rbtnNo_Load"
                            Checked="True" />
                    </asp:Panel>
                </td>
                <td class="leftField" style="width: 10%">
                    <asp:Label ID="lblModeOfHolding" runat="server" Text="Mode of Holdings: " CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" style="width: 25%">
                    <asp:DropDownList ID="ddlModeOfHolding" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblPickNominee" runat="server" Text="Pick Nominee" CssClass="FieldName"></asp:Label>
                    <asp:GridView ID="gvPickNominee" runat="server" DataKeyNames="CA_AssociationId" AutoGenerateColumns="False"
                        Width="70%" CssClass="GridViewStyle">
                        <RowStyle CssClass="RowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="PNCheckBox" runat="server" />
                                </ItemTemplate>
                                <%--<EditItemTemplate>
                                    <asp:CheckBox ID="PNCheckBox" runat="server" />
                                </EditItemTemplate>--%>
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
                    <asp:Label ID="lblPickJointHolder" runat="server" Text="Pick Joint Holder" CssClass="FieldName"></asp:Label>
                    <asp:GridView ID="gvPickJointHolder" runat="server" DataKeyNames="CA_AssociationId"
                        AutoGenerateColumns="False" CssClass="GridViewStyle" Width="70%">
                        <RowStyle CssClass="RowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="PJHCheckBox" runat="server" />
                                </ItemTemplate>
                                <%--<EditItemTemplate>
                                    <asp:CheckBox ID="PJHCheckBox" runat="server" />
                                </EditItemTemplate>--%>
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
    <tr id="trSectionTwo8" runat="server">
        <td class="leftField" style="width: 10%">
            &nbsp;
        </td>
        <td>
        </td>
        <td class="leftField" style="width: 10%">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Payment Details
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="Label2" CssClass="FieldName" Text="Mode Of Payment:" runat="server"></asp:Label>
        </td>
        <td class="rightField" style="width: 30%">
            <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblPaymentInstruDate" CssClass="FieldName" Text="Instrument Date:"
                runat="server"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtPaymentInstruDate" CssClass="txtField" runat="server"
                Culture="English (United States)" Skin="Telerik" EnableEmbeddedSkins="false"
                ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:RequiredFieldValidator runat="server" ID="rfvPaymentInstruDate" ControlToValidate="txtPaymentInstruDate" CssClass="rfvPCG"
            ErrorMessage="Enter a date!"></asp:RequiredFieldValidator> 
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="Label5" CssClass="FieldName" runat="server" Text="Bank Name:"></asp:Label>
        </td>
        <td class="rightField" style="width: 30%">
            <asp:DropDownList ID="ddlBankName" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:Button ID="btnAddBank" runat="server" Text="Add Bank" CssClass="PCGButton" OnClientClick="return openpopupAddBank()" />
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblPaymentInstrNo" CssClass="FieldName" Text="Instrument No:" runat="server"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <asp:TextBox ID="txtPaymentInstrNo" CssClass="txtField" runat="server"></asp:TextBox>            
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="Label6" CssClass="FieldName" runat="server" Text="Branch Name:"></asp:Label>
        </td>
        <td>
            <%--<asp:TextBox ID="TextBox3" CssClass="txtField" runat="server"></asp:TextBox>--%>
            <asp:Label ID="lblBranchName" CssClass="FieldName" runat="server" Text=""></asp:Label>
        </td>
        <td class="leftField" style="width: 10%">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Policy Details
            </div>
        </td>
    </tr>
    <%--
<tr>
<td colspan="5">
<asp:Label ID="lblPolicyDetails" runat="server" Text="Policy Details: "  CssClass="FieldName"></asp:Label>
</td>
</tr>--%>
    <tr>
        <td align="right">
            <asp:Label ID="lblApplicationNo" runat="server" Text="Application Number: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtApplicationNo" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblApplicationDate" runat="server" Text="Application Date: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtApplicationDate" CssClass="txtField" runat="server"
                Culture="English (United States)" Skin="Telerik" EnableEmbeddedSkins="false"
                ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:RequiredFieldValidator runat="server" ID="rfvApplicationDate" ControlToValidate="txtApplicationDate" CssClass="rfvPCG"
            ErrorMessage="Enter a date!"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblMaturityDate" runat="server" Text="Policy Maturity Date: " CssClass="FieldName"></asp:Label>
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
            <asp:RequiredFieldValidator runat="server" ID="rfvMaturityDate" ControlToValidate="txtMaturityDate" CssClass="rfvPCG"
            ErrorMessage="Enter a date!"></asp:RequiredFieldValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblOrederDate" runat="server" Text="Order Date: " CssClass="FieldName"></asp:Label>
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
            <asp:RequiredFieldValidator runat="server" ID="rfvOrderDate" ControlToValidate="txtOrderDate" CssClass="rfvPCG"
            ErrorMessage="Enter a date!"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblSumAssured" runat="server" Text="Sum Assured: " CssClass="FieldName"></asp:Label>
        </td>
        <td valign="middle">
            <asp:TextBox ID="txtSumAssured" runat="server" CssClass="txtField"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revPaymentInstrNo" runat="server" CssClass="rfvPCG" ValidationExpression="^\d+$"
            ControlToValidate="txtSumAssured" Display="Dynamic" ErrorMessage="Please enter number only" ValidationGroup="Submit">
            </asp:RegularExpressionValidator>
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblEPPremiumCycle" runat="server" CssClass="FieldName" Text="Premium Cycle:"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <asp:DropDownList ID="ddlEPPremiumFrequencyCode" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblOrderStatus" runat="server" Text="Order Status: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlOrderStatus_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblReason" runat="server" Text="Reason: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <asp:DropDownList ID="ddlReason" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlReason_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 10%">
            <asp:CheckBox ID="chkCA" runat="server" CssClass="cmbField" AutoPostBack="true" OnCheckedChanged="chkCA_CheckedChanged" />
        </td>
        <td>
            <asp:Label ID="lblCustomerApproval" runat="server" CssClass="FieldName" Text=": Customer Approval"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="rightField" style="width: 25%">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" OnClick="btnSubmit_Click" ValidationGroup="Submit"/>
            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="PCGButton"  ValidationGroup="Submit"
                onclick="btnUpdate_Click"/>
        </td>
    </tr>
</table>
<%--<asp:Panel ID="Panel2" runat="server" CssClass="ExortPanelpopup" Width="100%" Height="80%">
    
</asp:Panel>--%>
<asp:Panel ID="Panel2" runat="server" CssClass="ExortPanelpopup" Width="100%" Height="85%">
    <asp:UpdatePanel ID="udpInnerUpdatePanel" runat="Server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="leftField" style="width: 10%">
                        <asp:Label ID="lblIssuar" runat="server" Text="Insurance Issuar: " CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField" style="width: 25%">
                        <asp:Label ID="lblIssuarCode" runat="server" Text="" CssClass="FieldName"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="leftField" style="width: 10%">
                        <asp:Label ID="lblAsset" runat="server" Text="Asset Particulars: " CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField" style="width: 25%">
                        <asp:TextBox ID="txtAsset" runat="server" CssClass="txtField"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td class="leftField" style="width: 10%">
                        <asp:Button ID="btnOk" runat="server" Text="OK" CssClass="PCGButton" OnClick="btnOk_Click" />
                    </td>
                    <td class="rightField" style="width: 25%">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="PCGButton" />
                        <asp:Button ID="btnInsertAsset" runat="server" Style="display: none" OnClick="btnInsertAsset_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnOk" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Panel>

<asp:Panel ID="pnlOrderSteps" runat="server" CssClass="ExortPanelpopup" Width="100%" Height="80%">
    <table width="100%">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Order Steps" CssClass="FieldName"></asp:Label>
                <asp:GridView ID="gvOrderSteps" runat="server" AutoGenerateColumns="False"
                    Width="70%" CssClass="GridViewStyle">
                    <RowStyle CssClass="RowStyle" />
                    <Columns>
                        <asp:BoundField HeaderText="Summary" DataField="CO_OrderId" />
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" CssClass="GridViewCmbField">
                                </asp:DropDownList>                                
                                    <%--OnSelectedIndexChanged="ddlStatus_OnSelectedIndexChange"--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pending Reason">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlPendingReason" runat="server" CssClass="GridViewCmbField">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>                       
                        <asp:BoundField HeaderText="Date" DataField="XS_StatusCode" />
                        <asp:BoundField HeaderText="User Name" DataField="XSR_StatusReasonCode" />
                    </Columns>
                    <FooterStyle CssClass="FooterStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Panel>

<asp:HiddenField ID="hdnCustomerId" runat="server" />
<asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="txtCustomerId_ValueChanged" />
<asp:HiddenField ID="hdnSchemeCode" runat="server" />
<asp:HiddenField ID="hdnType" runat="server" />
