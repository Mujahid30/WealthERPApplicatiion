<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LifeInsuranceOrderEntry.ascx.cs"
    Inherits="WealthERP.OPS.LifeInsuranceOrderEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
    }    
</script>

<script language="javascript" type="text/javascript">

    function showmessage() {

        var bool = window.confirm('Are you sure you want to delete this Order?');
        if (bool) {
            document.getElementById("ctrl_LifeInsuranceOrderEntry_hdnMsgValue").value = 1;
            document.getElementById("ctrl_LifeInsuranceOrderEntry_hiddenassociation").click();
            return false;
        }
        else {
            document.getElementById("ctrl_LifeInsuranceOrderEntry_hdnMsgValue").value = 0;
            document.getElementById("ctrl_LifeInsuranceOrderEntry_hiddenassociation").click();
            return true;
        }
    }
</script>

<script language="javascript" type="text/javascript">

    function openpopupAddBank() {
        window.open('PopUp.aspx?PageId=AddBankAccount', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
        return false;
    }
    function openpopupAddCustomer() {
        window.open('PopUp.aspx?PageId=CustomerType', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
        return false;
    }
</script>

<telerik:RadWindow ID="radwindowPopup" runat="server" VisibleOnPageLoad="false" Height="30%"
    Width="500px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Behaviors="None"
    Title="Add New Scheme">
    <ContentTemplate>
        <div style="padding: 20px">
            <table width="100%">
                <tr>
                    <td class="leftField" style="width: 10%">
                        <asp:Label ID="lblIssuer" runat="server" Text="Insurance Issuer: " CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField" style="width: 25%">
                        <asp:Label ID="lblIssuerCode" runat="server" Text="" CssClass="FieldName"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="leftField" style="width: 10%">
                        <asp:Label ID="lblAsset" runat="server" Text="Asset Particulars: " CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField" style="width: 25%">
                        <asp:TextBox ID="txtAsset" runat="server" CssClass="txtField"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtAsset" ErrorMessage="Enter the Scheme Name"
                            ValidationGroup="vgOK" Display="Dynamic" runat="server" CssClass="rfvPCG">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField" style="width: 10%">
                        <asp:Button ID="btnOk" runat="server" Text="OK" CssClass="PCGButton" OnClick="btnOk_Click"
                            ValidationGroup="vgOK" />
                    </td>
                    <td class="rightField" style="width: 25%">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="PCGButton" CausesValidation="false"
                            OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
</telerik:RadWindow>
<table width="100%">
    <tr>
        <td colspan="5">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Order Entry Insurance
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="lnkBack" runat="server" CssClass="LinkButtons" Text="Back" OnClick="lnkBack_Click"></asp:LinkButton>
                            &nbsp; &nbsp;
                            <asp:LinkButton runat="server" ID="lnkBtnEdit" CssClass="LinkButtons" Text="Edit"
                                OnClick="lnkBtnEdit_Click"></asp:LinkButton>&nbsp; &nbsp;
                            <asp:LinkButton runat="server" ID="lnkbtnDelete" CssClass="LinkButtons" Text="Delete"
                                OnClick="lnkBtnDelete_Click"></asp:LinkButton>
                            &nbsp; &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
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
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblCustomer" runat="server" Text="Customer Name: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True">
            </asp:TextBox><span id="spnCustomer" class="spnRequiredField">*</span>
            <%--<asp:Button ID="btnAddCustomer" runat="server" Text="Add Customer" CssClass="PCGMediumButton"
                CausesValidation="true" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_OrderEntry_btnAddCustomer','M');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_OrderEntry_btnAddCustomer','M');"
                OnClientClick="return openpopupAddCustomer()" />--%>
            <asp:ImageButton ID="btnImgAddCustomer" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                runat="server" ToolTip="Click here to Add Customer" OnClientClick="return openpopupAddCustomer()"
                Height="6%" Width="6%"></asp:ImageButton>
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
        <td class="leftField" style="width: 15%">
            <%--<asp:Label ID="lblRM" runat="server" Text="RM: " CssClass="FieldName"></asp:Label>--%>
        </td>
        <td class="rightField" style="width: 25%">
            <%--<asp:Label ID="lblGetRM" runat="server" Text="" CssClass="FieldName"></asp:Label>--%>
        </td>
        <td style="width: 20%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 15%">
            <asp:Label ID="lblBranch" runat="server" Text="Branch: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblGetBranch" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
        <td class="leftField" style="width: 15%">
            <asp:Label ID="lblRM" runat="server" Text="RM: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <asp:Label ID="lblGetRM" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 15%">
            <asp:Label ID="lblPan" runat="server" Text="PAN No: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblgetPan" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblEmail" runat="server" Text="Email: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
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
        <td class="leftField" style="width: 15%">
            <asp:Label ID="lblProductType" runat="server" CssClass="FieldName" Text="Insurance Type:"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlProductType" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlProductType_OnSelectedIndexChanged">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                <asp:ListItem Text="Life Insurance" Value="LI"></asp:ListItem>
                <asp:ListItem Text="General Insurance" Value="GI"></asp:ListItem>
            </asp:DropDownList>
            <span id="Span14" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlInstrumentCategory"
                ErrorMessage="Select category" Operator="NotEqual" ValueToCompare="Select" Display="Dynamic"
                CssClass="cvPCG" ValidationGroup="Submit">
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
        <td class="leftField" style="width: 15%">
            <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" Text="Instrument Category:"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlInstrumentCategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlInstrumentCategory_OnSelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="cvInstrumentCategory" runat="server" ControlToValidate="ddlInstrumentCategory"
                ErrorMessage="Select category" Operator="NotEqual" ValueToCompare="Select" Display="Dynamic"
                CssClass="cvPCG" ValidationGroup="Submit">
            </asp:CompareValidator>
        </td>
        <td align="right" style="width: 15%" runat="server" visible="false" id="tdlblAssetSubCategory">
            <asp:Label ID="lblAssetSubCategory" runat="server" Text="Asset Sub Category:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" style="width: 50%" runat="server" visible="false" id="tdddlAssetSubCategory">
            <asp:DropDownList ID="ddlAssetSubCategory" Style="width: 35%" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="span15" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="cv_ddlAssetSubCategory" runat="server" ErrorMessage="<br />Please select Asset Sub-Category"
                ControlToValidate="ddlAssetSubCategory" Operator="NotEqual" ValueToCompare="Select Asset Sub-Category"
                Display="Dynamic" CssClass="cvPCG"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 15%">
            <asp:Label ID="lblPolicyIssuer" runat="server" Text="Policy Issuer: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlPolicyIssuer" runat="server" CssClass="cmbField" AutoPostBack="True"
                OnSelectedIndexChanged="ddlPolicyIssuer_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="cvPolicyIssuer" runat="server" ControlToValidate="ddlPolicyIssuer"
                ErrorMessage="Select Issuer" Operator="NotEqual" ValueToCompare="Select" Display="Dynamic"
                CssClass="cvPCG" ValidationGroup="Submit">
            </asp:CompareValidator>
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblAssetPerticular" runat="server" Text="Scheme Name: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <asp:DropDownList ID="ddlAssetPerticular" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span3" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlAssetPerticular"
                ErrorMessage="Select Asset" Operator="NotEqual" ValueToCompare="Select" Display="Dynamic"
                CssClass="cvPCG" ValidationGroup="Submit">      
            </asp:CompareValidator>
            <asp:ImageButton ID="imgBtnOpenPopup" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                runat="server" ToolTip="Click here to Add Scheme" OnClick="btnOpenPopup_Click"
                CausesValidation="false" Height="5%" Width="5%"></asp:ImageButton>
        </td>
    </tr>
    <tr>
        <td align="right" valign="top">
            <asp:Label ID="lblPickNominee" runat="server" Text="Pick Nominee:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="leftField" colspan="3" style="margin-left: 5px">
            <asp:GridView ID="gvPickNominee" runat="server" DataKeyNames="CA_AssociationId" AutoGenerateColumns="False"
                Width="85%" CssClass="GridViewStyle">
                <RowStyle CssClass="RowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:CheckBox ID="PNCheckBox" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Name" DataField="AssociateName" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="Relationship" DataField="XR_Relationship" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                </Columns>
                <FooterStyle CssClass="FooterStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
            </asp:GridView>
        </td>
    </tr>
    <%--<asp:UpdatePanel ID="UPNomineeJointHoldingGrid" runat="server">
        <ContentTemplate>
            <tr>
                <td class="leftField" style="width: 15%">
                    <asp:Label ID="lblIsHeldJointly" runat="server" Text="Joint Holding" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:Panel ID="pnlHolding" runat="server" Width="127%">
                        <asp:RadioButton ID="rbtnYes" runat="server" Text="Yes" GroupName="IsHeldJointly"
                            CssClass="txtField" AutoPostBack="True" OnCheckedChanged="RadioButton_CheckChanged" />
                        <asp:RadioButton ID="rbtnNo" runat="server" Text="No" GroupName="IsHeldJointly" CssClass="txtField"
                            AutoPostBack="True" OnCheckedChanged="RadioButton_CheckChanged"
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
                
                <td colspan="2" style="margin-left:5px">
                    <asp:Label ID="lblPickJointHolder" runat="server" Text="Pick Joint Holder" CssClass="FieldName"></asp:Label>
                    <asp:GridView ID="gvPickJointHolder" runat="server" DataKeyNames="CA_AssociationId"
                        AutoGenerateColumns="False" CssClass="GridViewStyle" Width="80%">
                        <RowStyle CssClass="RowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="PJHCheckBox" runat="server" />
                                </ItemTemplate>
                                
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
    </asp:UpdatePanel>--%>
    <tr id="trSectionTwo8" runat="server">
        <td class="leftField" style="width: 15%">
            &nbsp;
        </td>
        <td style="width: 20%">
        </td>
        <td class="leftField" style="width: 10%">
        </td>
        <td style="width: 25%">
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
            <span id="Span4" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rfvPCG"
                ControlToValidate="txtApplicationNo" Display="Dynamic" ErrorMessage="Please enter Application Number"
                ValidationGroup="Submit"></asp:RequiredFieldValidator>
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
            <span id="Span5" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator runat="server" ID="rfvApplicationDate" ControlToValidate="txtApplicationDate"
                CssClass="rfvPCG" ValidationGroup="Submit" ErrorMessage="Enter a date!"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 15%">
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
            <span id="Span6" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator runat="server" ID="rfvMaturityDate" ControlToValidate="txtMaturityDate"
                ValidationGroup="Submit" CssClass="rfvPCG" ErrorMessage="Enter a date!"></asp:RequiredFieldValidator>
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
            <span id="Span7" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator runat="server" ID="rfvOrderDate" ControlToValidate="txtOrderDate"
                ValidationGroup="Submit" CssClass="rfvPCG" ErrorMessage="Enter a date!"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 15%">
            <asp:Label ID="lblSumAssured" runat="server" Text="Sum Assured: " CssClass="FieldName"></asp:Label>
        </td>
        <td valign="middle">
            <asp:TextBox ID="txtSumAssured" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span8" class="spnRequiredField">*</span><br />
            <asp:RegularExpressionValidator ID="revPaymentInstrNo" runat="server" CssClass="rfvPCG"
                ValidationExpression="^\d+$" ControlToValidate="txtSumAssured" Display="Dynamic"
                ErrorMessage="Please enter number only" ValidationGroup="Submit">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfgvSumAssured" runat="server" CssClass="rfvPCG"
                ControlToValidate="txtSumAssured" Display="Dynamic" ErrorMessage="Please enter sum assured"
                ValidationGroup="Submit"></asp:RequiredFieldValidator>
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblEPPremiumCycle" runat="server" CssClass="FieldName" Text="Premium Cycle:"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <asp:DropDownList ID="ddlEPPremiumFrequencyCode" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span9" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlEPPremiumFrequencyCode"
                ErrorMessage="Select Frequency" Operator="NotEqual" ValueToCompare="Select" Display="Dynamic"
                CssClass="cvPCG" ValidationGroup="Submit">      
            </asp:CompareValidator>
        </td>
    </tr>
    <%--    <tr>
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
    </tr>--%>
    <%--<tr>
        <td class="leftField" style="width: 10%">
            <asp:CheckBox ID="chkCA" runat="server" CssClass="cmbField" AutoPostBack="true" OnCheckedChanged="chkCA_CheckedChanged" />
        </td>
        <td>
            <asp:Label ID="lblCustomerApproval" runat="server" CssClass="FieldName" Text=": Customer Approval"></asp:Label>
        </td>
    </tr>--%>
    <tr>
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Payment Details
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 15%">
            <asp:Label ID="Label2" CssClass="FieldName" Text="Mode Of Payment:" runat="server"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span10" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlPaymentMode"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a Payment Mode"
                Operator="NotEqual" ValidationGroup="Submit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblPaymentInstruDate" CssClass="FieldName" Text="Instrument Date:"
                runat="server"></asp:Label>
        </td>
        <td style="width: 25%">
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
            <span id="Span11" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator runat="server" ID="rfvPaymentInstruDate" ControlToValidate="txtPaymentInstruDate"
                ValidationGroup="Submit" CssClass="rfvPCG" ErrorMessage="Enter a date!"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 15%">
            <asp:Label ID="Label5" CssClass="FieldName" runat="server" Text="Bank Name:"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlBankName" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span12" class="spnRequiredField">*</span>
            <%--<asp:Button ID="btnAddBank" runat="server" Text="Add Bank" CssClass="PCGButton" OnClientClick="return openpopupAddBank()" />--%>
            <asp:CompareValidator ID="CompareValidator11" runat="server" ControlToValidate="ddlBankName"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a Bank"
                Operator="NotEqual" ValidationGroup="Submit" ValueToCompare="Select"></asp:CompareValidator>
            <asp:ImageButton ID="imgBtnAddBank" Visible="false" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                runat="server" ToolTip="Click here to Add Bank" OnClientClick="return openpopupAddBank()"
                Height="6%" Width="6%"></asp:ImageButton>
            <asp:ImageButton ID="imgBtnRefereshBank" Visible="false" ImageUrl="~/Images/refresh.png"
                AlternateText="Refresh" runat="server" ToolTip="Click here to refresh Bank List"
                OnClick="imgBtnRefereshBank_OnClick" Height="15px" Width="25px"></asp:ImageButton>
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblPaymentInstrNo" CssClass="FieldName" Text="Instrument No:" runat="server"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <asp:TextBox ID="txtPaymentInstrNo" CssClass="txtField" runat="server"></asp:TextBox>
            <span id="Span13" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ValidationGroup="Submit"
                ControlToValidate="txtPaymentInstrNo" CssClass="rfvPCG" ErrorMessage="Enter Instrument No!"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 15%">
            <asp:Label ID="Label6" CssClass="FieldName" runat="server" Text="Branch Name:"></asp:Label>
        </td>
        <td style="width: 20%">
            <%--<asp:TextBox ID="TextBox3" CssClass="txtField" runat="server"></asp:TextBox>--%>
            <asp:Label ID="lblBranchName" CssClass="FieldName" runat="server" Text=""></asp:Label>
        </td>
        <td class="leftField" style="width: 10%">
        </td>
        <td style="width: 25%">
        </td>
    </tr>
    <tr>
        <td class="rightField" style="width: 15%">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" OnClick="btnSubmit_Click"
                ValidationGroup="Submit" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="PCGButton" ValidationGroup="Submit"
                OnClick="btnUpdate_Click" />
        </td>
    </tr>
</table>
<asp:Panel ID="pnlOrderSteps" runat="server" Width="100%" Height="80%">
    <table width="100%">
        <tr>
            <td>
                <div class="divSectionHeading" style="vertical-align: text-bottom">
                    Order Steps
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="rgvOrderSteps" runat="server" Skin="Telerik" CssClass="RadGrid"
                    Width="80%" GridLines="None" AllowPaging="True" PageSize="20" AllowSorting="false"
                    AutoGenerateColumns="False" OnItemCreated="rgvOrderSteps_ItemCreated" ShowStatusBar="true"
                    AllowAutomaticUpdates="false" HorizontalAlign="NotSet" DataKeyNames="CO_OrderId,WOS_OrderStepCode"
                    OnItemDataBound="rgvOrderSteps_ItemDataBound" OnItemCommand="rgvOrderSteps_ItemCommand"
                    OnNeedDataSource="rgvOrderSteps_NeedDataSource">
                    <MasterTableView CommandItemDisplay="none" EditMode="PopUp" EnableViewState="false">
                        <Columns>
                            <telerik:GridBoundColumn DataField="CO_OrderId" HeaderText="Serial No." UniqueName="CO_OrderId"
                                ReadOnly="True">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WOS_OrderStep" HeaderText="Stages" UniqueName="WOS_OrderStep"
                                ReadOnly="True">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%-- <telerik:GridDropDownColumn UniqueName="DropDownColumnStatus" HeaderText="Status" 
                        ListTextField="XS_Status" ListValueField="XS_StatusCode" DataField="XS_StatusCode"></telerik:GridDropDownColumn>
                        
                        <telerik:GridDropDownColumn UniqueName="DropDownColumnStatusReason" HeaderText="Pending Reason"
                        ListTextField="XSR_StatusReason" ListValueField="XSR_StatusReasonCode" DataField="XSR_StatusReasonCode"></telerik:GridDropDownColumn>--%>
                            <telerik:GridTemplateColumn DataField="XS_StatusCode" HeaderText="Status" UniqueName="DropDownColumnStatus">
                                <EditItemTemplate>
                                    <telerik:RadComboBox ID="ddlCustomerOrderStatus" OnSelectedIndexChanged="rcStatus_SelectedIndexChanged"
                                        AutoPostBack="true" SelectedValue='<%#Bind("XS_StatusCode") %>' runat="server">
                                    </telerik:RadComboBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "XS_Status")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="XSR_StatusReasonCode" HeaderText="Pending Reason"
                                UniqueName="DropDownColumnStatusReason">
                                <EditItemTemplate>
                                    <telerik:RadComboBox ID="ddlCustomerOrderStatusReason" runat="server">
                                    </telerik:RadComboBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "XSR_StatusReason")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridDateTimeColumn DataField="CMFOS_Date" HeaderText="Date" DataFormatString="{0:d}"
                                HtmlEncode="false" DataType="System.DateTime" UniqueName="CMFOS_Date" ReadOnly="true" />
                            <telerik:GridEditCommandColumn UpdateText="Update" EditText="Edit" UniqueName="EditCommandColumn"
                                CancelText="Cancel">
                                <HeaderStyle Width="85px"></HeaderStyle>
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn DataField="COS_IsEditable" DataType="System.Boolean" UniqueName="COS_IsEditable"
                                Display="false" ReadOnly="True">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
    <asp:LinkButton ID="lbOrderBook" runat="server" Text="View Order Book" Visible="false" OnClick="lbOrderBook_OnClick" CssClass="LinkButtons"></asp:LinkButton>
</asp:Panel>
<asp:HiddenField ID="hdnCustomerId" Value="0" runat="server" />
<asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="txtCustomerId_ValueChanged" />
<asp:HiddenField ID="hdnSchemeCode" runat="server" />
<asp:HiddenField ID="hdnType" runat="server" />
<asp:HiddenField ID="hdnMsgValue" runat="server" />
<asp:Button ID="hiddenassociation" runat="server" OnClick="hiddenassociation_Click"
    BorderStyle="None" BackColor="Transparent" />
