<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioGeneralInsuranceEntry.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioGeneralInsuranceEntry" EnableViewState="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<style type="text/css">
    .style1
    {
        height: 30px;
    }
    .style3
    {
        width: 211px;
    }
    .style4
    {
        width: 290px;
    }
    .style5
    {
        height: 23px;
    }
</style>
<%--Javascript Calendar Controls - Required Files--%>
<asp:ScriptManager ID="scptMgr" runat="server" EnablePartialRendering="true" EnablePageMethods="true">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script language="javascript" type="text/javascript">
    StateMaintain();
    window.onload = EnableDisableValidators;
</script>

<script language="javascript" type="text/javascript">
    //Business Rules to happen on policy type change
    function ChangeGroupPolicy(value) {
        debugger;
        if (value == 'Yes') {
            document.getElementById('<%= ddlTypeOfPolicy.ClientID %>').value = 'PTFLT';
            document.getElementById('<%= hdnPolicyType.ClientID %>').value = 'PTFLT';
            document.getElementById('<%= hdnGroupPolicy.ClientID %>').value = '1';
            document.getElementById('<%= ddlTypeOfPolicy.ClientID %>').disabled = false;
            document.getElementById('divGridView').style.display = 'block';
            document.getElementById('<%= txtSumAssured1.ClientID %>').disabled = false;
            ChangePolicyType();
        }
        else {
            document.getElementById('<%= ddlTypeOfPolicy.ClientID %>').value = 'PTIND';
            document.getElementById('<%= hdnPolicyType.ClientID %>').value = 'PTIND';
            document.getElementById('<%= hdnGroupPolicy.ClientID %>').value = '0';
            document.getElementById('<%= ddlTypeOfPolicy.ClientID %>').disabled = true;
            document.getElementById('divGridView').style.display = 'none';
            document.getElementById('<%= txtSumAssured1.ClientID %>').disabled = false;
        }
    }

    function ChangePolicyType() {
        debugger;
        var gridViewID = "<%=gvNominees.ClientID %>";
        var gridView = document.getElementById(gridViewID);
        var gridViewControls = gridView.getElementsByTagName("input");

        if (document.getElementById('<%= ddlTypeOfPolicy.ClientID %>').value == 'PTFLT') {
            for (i = 0; i < gridViewControls.length; i++) {
                // if this input type is textbox, disable and clear
                if (gridViewControls[i].type == "text") {
                    gridViewControls[i].disabled = true;
                    gridViewControls[i].value = '';
                } Error
                // if this input type is checkbox, uncheck
                if (gridViewControls[i].type == "checkbox") {
                    gridViewControls[i].checked = false;
                }
            }
            document.getElementById('<%= txtSumAssured1.ClientID %>').disabled = false;
        }
        else {
            for (i = 0; i < gridViewControls.length; i++) {
                // if this input type is textbox, enable and clear abcd
                if (gridViewControls[i].type == "text") {
                    gridViewControls[i].disabled = false;
                    gridViewControls[i].value = '';
                }
                // if this input type is checkbox, uncheck
                if (gridViewControls[i].type == "checkbox") {
                    gridViewControls[i].checked = false;
                }
            }
            document.getElementById('<%= txtSumAssured1.ClientID %>').disabled = true;
        }
        document.getElementById('<%= txtSumAssured1.ClientID %>').value = '';
        document.getElementById('<%= hdnPolicyType.ClientID %>').value = document.getElementById('<%= ddlTypeOfPolicy.ClientID %>').value;
    }

    function StateMaintain() {
        debugger;
        var gridViewControls;
        var gridViewID = "<%=gvNominees.ClientID %>";
        var gridView = document.getElementById(gridViewID);
        if (gridView != null)
            gridViewControls = gridView.getElementsByTagName("input");

        if (document.getElementById('<%= hdnGroupPolicy.ClientID %>').value == '1') {
            document.getElementById('<%= rdoGroupPolicyYes.ClientID %>').checked = true;
        }
        else {
            document.getElementById('<%= rdoGroupPolicyNo.ClientID %>').checked = true;
            document.getElementById('<%= ddlTypeOfPolicy.ClientID %>').value = 'PTIND';
        }
        document.getElementById('<%= ddlTypeOfPolicy.ClientID %>').value = document.getElementById('<%= hdnPolicyType.ClientID %>').value;
        if (document.getElementById('<%= rdoGroupPolicyYes.ClientID %>').checked) {
            if (document.getElementById('<%= ddlTypeOfPolicy.ClientID %>').value == 'PTIND') {
                for (i = 0; i < gridViewControls.length; i++) {
                    // if this input type is textbox, disable
                    if (document.getElementById('<%= btnSubmit.ClientID %>') != null) {
                        if (gridViewControls[i].type == "text") {
                            gridViewControls[i].disabled = false;
                        }
                    }
                }
                document.getElementById('<%= txtSumAssured1.ClientID %>').disabled = true;
                CalculateSum();
            }
            else
                document.getElementById('<%= txtSumAssured1.ClientID %>').disabled = false;
            if (document.getElementById('<%= btnSubmit.ClientID %>') != null)
                document.getElementById('<%= ddlTypeOfPolicy.ClientID %>').disabled = false;
        }
        else {
            document.getElementById('divGridView').style.display = 'none';
            //code to be checked
            document.getElementById('<%= ddlTypeOfPolicy.ClientID %>').value = 'PTIND';
        }
        if (document.getElementById('<%= hdnFreeHealth.ClientID %>').value == '1') {
            document.getElementById('<%= rdoHealthYes.ClientID %>').checked = true;
            if (document.getElementById('<%= btnSubmit.ClientID %>') != null)
                document.getElementById('<%= txtCheckUpDate.ClientID %>').disabled = false;
        }
        else {
            document.getElementById('<%= rdoHealthNo.ClientID %>').checked = true;
            document.getElementById('<%= txtCheckUpDate.ClientID %>').disabled = true;
        }
        //EnableDisableValidators();
        //CalculateSum();
        //document.getElementById('<%= txtSumAssured1.ClientID %>').value = document.getElementById('<%= hdnSumAssured.ClientID %>').value;
    }
    function CalculateSum() {
        debugger;
        var gridViewID = "<%=gvNominees.ClientID %>";
        var gridView = document.getElementById(gridViewID);
        var gridViewControls = gridView.getElementsByTagName("input");
        var sum = 0;
        for (i = 0; i < gridViewControls.length; i++) {
            if (gridViewControls[i].type == "text") {
                if (!isNaN(gridViewControls[i].value) && gridViewControls[i].value != '') {
                    gridViewControls[i].value = parseFloat(gridViewControls[i].value).toFixed(2);
                    sum = sum + parseFloat(gridViewControls[i].value);
                }
            }
        }
        document.getElementById('<%= txtSumAssured1.ClientID %>').value = sum;
        document.getElementById('<%= hdnSumAssured.ClientID %>').value = sum;
    }


    function EnableDisableCheckUpDate(value) {
        debugger;
        if (value == 'Yes')
            document.getElementById('<%=txtCheckUpDate.ClientID %>').disabled = false;
        else {
            document.getElementById('<%=txtCheckUpDate.ClientID %>').disabled = true;
            document.getElementById('<%=txtCheckUpDate.ClientID %>').value = 'MM/dd/yyyy';
        }
    }

    function EnableDisableValidators() {
        debugger;
        if (document.getElementById('<%=chkIsPolicyByEmployer.ClientID %>').checked) {
            document.getElementById('span1').style.visibility = 'hidden';
            ValidatorEnable(document.getElementById('<%=cv_ddlPolicyIssuer.ClientID %>'), false);
            document.getElementById('span3').style.visibility = 'hidden';
            ValidatorEnable(document.getElementById('<%=CompareValidator1.ClientID %>'), false);
            document.getElementById('Span4').style.visibility = 'hidden';
            ValidatorEnable(document.getElementById('<%=RequiredFieldValidator1.ClientID %>'), false);
        }
        else {
            ValidatorEnable(document.getElementById('<%=cv_ddlPolicyIssuer.ClientID %>'), true);
            document.getElementById('span1').style.visibility = 'visible';
            ValidatorEnable(document.getElementById('<%=CompareValidator1.ClientID %>'), true);
            document.getElementById('span3').style.visibility = 'visible';
            ValidatorEnable(document.getElementById('<%=RequiredFieldValidator1.ClientID %>'), true);
            document.getElementById('Span4').style.visibility = 'visible';
        }
        document.getElementById('ctrl_PortfolioGeneralInsuranceEntry_cv_ddlPolicyIssuer').style.display = 'none';
        document.getElementById('ctrl_PortfolioGeneralInsuranceEntry_CompareValidator1').style.display = 'none';
        document.getElementById('ctrl_PortfolioGeneralInsuranceEntry_RequiredFieldValidator1').style.display = 'none';
    }
    function ValidateGroupMembersAndDates() {
        debugger;
        if (document.getElementById('<%= rdoGroupPolicyYes.ClientID %>').checked) {
            var count = 0;
            var gridViewID = "<%=gvNominees.ClientID %>";
            var gridView = document.getElementById(gridViewID);
            var gridViewControls = gridView.getElementsByTagName("input");
            for (i = 0; i < gridViewControls.length; i++) {
                // if this input type is checkbox
                if (gridViewControls[i].type == "checkbox") {
                    if (gridViewControls[i].checked == true) {
                        count = count + 1;
                    }
                }
            }
            if (count < 1) {
                alert('Please select the Family/Group members !');
                return false;
            }
        }
        validityStartDate = changeDate(validityStartDate);

        validityEndDate = changeDate(validityEndDate);
        if (document.getElementById('<%=txtProposalDate.ClientID %>').value != '' && document.getElementById('<%=txtProposalDate.ClientID %>').value != 'MM/dd/yyyy') {
            var proposalDate = document.getElementById('<%=txtProposalDate.ClientID %>').value;
            proposalDate = changeDate(proposalDate);
            if (Date.parse(validityStartDate) < Date.parse(proposalDate)) {
                alert('Sorry, Validity Start Date cannot be less than Proposal Date');
                return false;
            }
        }
        if (Date.parse(validityEndDate) < Date.parse(validityStartDate)) {
            alert('Sorry, Validity End Date cannot be less than Validity Start Date');
            return false;
        }
        //        if (document.getElementById('<%= rdoHealthYes.ClientID %>').checked) {
        //            if (document.getElementById('<%=txtCheckUpDate.ClientID %>').value == '' || document.getElementById('<%=txtCheckUpDate.ClientID %>').value == 'MM/dd/yyyy') {
        //                alert('Please enter the Check Up Date !');
        //                return false;
        //            }
        //        }
        document.getElementById('<%= hdnSumAssured.ClientID %>').value = document.getElementById('<%= txtSumAssured1.ClientID %>').value;
        return true;
    }
    function changeDate(date) {
        var newDate = date.split('/');
        date = newDate[1] + "/" + newDate[0] + "/" + newDate[2];
        return date;
    }

</script>

<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            General Insurance Add Screen
                        </td>
                        <td align="right">
                            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                                OnClick="lnkBtnBack_Click"></asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="lnkBtnEdit" CssClass="LinkButtons" Text="Edit"
                                OnClick="lnkBtnEdit_Click"></asp:LinkButton>
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table style="width: 100%;">
    <tr>
        <td align="right" style="width: 22%">
            <asp:Label ID="lblAssetCategory" runat="server" Text="Asset Category:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 22%">
            <asp:TextBox ID="txtAssetCategory" runat="server" CssClass="txtField" Enabled="false">
            </asp:TextBox>
        </td>
        <td align="right" style="width: 15%">
            <asp:Label ID="lblAssetSubCategory" runat="server" Text="Asset Sub Category:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 22%">
            <asp:TextBox ID="txtAssetSubCategory" runat="server" CssClass="txtField" Enabled="false">
            </asp:TextBox>
        </td>
        <td style="width: 19%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="right" style="width: 22%">
            <asp:Label ID="lblPolicyNumber" runat="server" Text="Policy Number:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style3" style="width: 22%">
            <asp:TextBox ID="txtPolicyNumber" runat="server" CssClass="txtField" Enabled="false">
            </asp:TextBox>
        </td>
        <td colspan="4" style="width: 56%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="6" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Account Details
            </div>
        </td>
    </tr>
    <tr>
        <td align="right" style="width: 22%">
            <asp:Label ID="lblPolicyIssuer" runat="server" Text="Insurer:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 22%" colspan="2">
            <asp:DropDownList ID="ddlPolicyIssuer" CausesValidation="true" runat="server" CssClass="cmbField"
                Width="80%" OnSelectedIndexChanged="ddlPolicyIssuer_OnSelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
            </asp:DropDownList>
            <span id="span1" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="cv_ddlPolicyIssuer" runat="server" ErrorMessage="<br />Please select an Insurance Issuer"
                ControlToValidate="ddlPolicyIssuer" Operator="NotEqual" ValueToCompare="Select"
                Display="Dynamic" CssClass="cvPCG" ValidationGroup="buttonSubmit"></asp:CompareValidator>
            <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="<br />Please select a Scheme Name:"
                ControlToValidate="ddlPolicyIssuer" Operator="NotEqual" ValueToCompare="Select"
                Display="Dynamic" CssClass="cvPCG" ValidationGroup="btnAddScheme"></asp:CompareValidator>
        </td>
        <td colspan="3" style="width: 56%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="right" style="width: 22%" valign="top">
            <asp:Label ID="lblPolicyParticular" runat="server" Text="Policy Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" align="right" style="width: 22%" colspan="2">
            <asp:DropDownList ID="txtPolicyParticular" runat="server" CssClass="cmbField" Width="80%">
            </asp:DropDownList>
            <span id="span5" class="spnRequiredField">*</span>
            <%--  <asp:Button ID="btnAddScheme" runat="server" Text="++" OnClick="btnAddScheme_OnClick"
    ValidationGroup="btnAddScheme"  />--%>
            <asp:ImageButton ID="btnAddScheme" ImageUrl="~/Images/user_add.png" runat="server"
                ToolTip="Add policy Name" OnClick="btnAddScheme_OnClick" ValidationGroup="btnAddScheme"
                Height="15px" Width="15px" Visible="true" />
            <%-- 3.2% ~/App_Themes/Maroon/Images/user_add.png--%>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="<br />Please select a Scheme Name:"
                ControlToValidate="txtPolicyParticular" Operator="NotEqual" ValueToCompare="Select"
                Display="Dynamic" CssClass="cvPCG" ValidationGroup="buttonSubmit"></asp:CompareValidator>
        </td>
        <td colspan="2" style="width: 56%">
            <telerik:RadWindow ID="radwindowPopup" runat="server" Height="30%" Width="500px"
                Modal="true" BackColor="#DADADA" VisibleOnPageLoad="false" Top="10px" Left="20px"
                VisibleStatusbar="false" Behaviors="Move,resize,close" Title="Inset New Policy">
                <ContentTemplate>
                    <div style="padding: 20px">
                        <table width="100%">
                            <tr>
                                <td class="leftField" style="width: 10%">
                                    <asp:Label ID="lblIssuar" runat="server" Text="Insurer: " CssClass="FieldName"></asp:Label>
                                </td>
                                <td class="rightField" style="width: 25%">
                                    <asp:Label ID="lblIssuarCode" runat="server" Text="" CssClass="FieldName"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField" style="width: 10%">
                                    <asp:Label ID="lblAsset" runat="server" Text="Policy Name: " CssClass="FieldName"></asp:Label>
                                </td>
                                <td class="rightField" style="width: 25%">
                                    <asp:TextBox ID="txtAsset" runat="server" CssClass="txtField"></asp:TextBox><br />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField" style="width: 10%">
                                    <asp:Button ID="btnOk" runat="server" Text="OK" CssClass="PCGButton" CausesValidation="false"
                                        OnClick="btnInsertNewScheme_Click" />
                                    <%--<asp:Button ID="Button3" runat="server" Text="Ok" CssClass="PCGButton" CausesValidation="false" OnClick="btnInsertAsset_Click" />--%>
                                </td>
                                <td class="rightField" style="width: 25%">
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="PCGButton" CausesValidation="false"
                                        OnClick="btnCancel_Click" />
                                    <%--<asp:Button ID="Button4" runat="server" Text="Cancel" CssClass="PCGButton" CausesValidation="false" OnClick="btnCancel4_Click" />--%>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>
        </td>
    </tr>
    <tr>
        <td colspan="6" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Policy Details
            </div>
        </td>
    </tr>
    <tr>
        <td align="right" style="width: 22%">
            <asp:Label ID="lblIsPolicyByEmployer" runat="server" CssClass="FieldName" Text="Is policy provided by employer:"></asp:Label>
        </td>
        <td style="width: 22%">
            <asp:CheckBox ID="chkIsPolicyByEmployer" Text="" runat="server" class="cmbField"
                onClick="EnableDisableValidators();" />
        </td>
        <td style="width: 56%" colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="right" style="width: 22%">
            <asp:Label ID="lblPolicyCommencementDate" runat="server" CssClass="FieldName" Text="Policy Start Date:"></asp:Label>
        </td>
        <td class="style3" style="width: 22%">
            <telerik:RadDatePicker ID="txtPolicyCommencementDate" CssClass="txtField" runat="server"
                Culture="English (United States)" Skin="Telerik" EnableEmbeddedSkins="false"
                ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
        </td>
        <td colspan="3" style="width: 56%">
            &nbsp;
        </td>
    </tr>
    <tr id="trPolicyTerm" runat="server">
        <td align="right" style="width: 22%">
            <asp:Label ID="lblPolicyTerm" runat="server" CssClass="FieldName" Text="Policy Term:"></asp:Label>
        </td>
        <td colspan="2" style="width: 22%">
            <%-- <asp:RadioButton ID="rdbPolicyTermDays" Text="Days" runat="server" onClick="return EnableDisableDaysMonths()"
                GroupName="PolicyTerm" class="cmbField" />
            <asp:RadioButton ID="rdbPolicyTermMonth" Text="Month" runat="server" onClick="return EnableDisableDaysMonths()"
                GroupName="PolicyTerm" Checked="true" class="cmbField" />--%>
            <asp:TextBox Width="30px" ID="txtPolicyTerm" runat="server" CssClass="txtField" AutoPostBack="true"
                OnTextChanged="txtPolicyTerm_TextChanged"></asp:TextBox>
            <span id="Span6" class="spnRequiredField">*</span>
            <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator1"
                runat="server" ControlToValidate="txtPolicyTerm" ErrorMessage="Please Enter Valid Term"
                ValidationExpression="^\d+$" CssClass="rfvPCG" ValidationGroup="buttonSubmit"></asp:RegularExpressionValidator>
            <asp:DropDownList CausesValidation="true" AutoPostBack="true" ID="ddlPeriodSelection"
                runat="server" CssClass="Field" OnSelectedIndexChanged="ddlPeriodSelection_SelectedIndexChanged">
                <asp:ListItem Text="Select" Value="0" Selected="True">               
                </asp:ListItem>
                <asp:ListItem Text="Days" Value="DA">               
                </asp:ListItem>
                <asp:ListItem Text="Months" Value="MN">               
                </asp:ListItem>
                <asp:ListItem Text="Years" Value="YR">               
                </asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="lblterm" runat="server" Text="(units)" CssClass="FieldName"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlPeriodSelection"
                ErrorMessage="Please select tenure" Display="Dynamic" CssClass="rfvPCG" runat="server"
                InitialValue="0" ValidationGroup="buttonSubmit">
            </asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPolicyTerm"
                ErrorMessage="Please enter the Policy Term" Display="Dynamic" CssClass="rfvPCG"
                runat="server" InitialValue="" ValidationGroup="buttonSubmit">
            </asp:RequiredFieldValidator>
            <%--  <asp:Label ID="lblDays" runat="server" CssClass="FieldName" Text="Days:"></asp:Label>--%>
        </td>
        <td colspan="2" style="width: 56%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="right" style="width: 22%">
            <asp:Label ID="lblMaturityDate" runat="server" Text="Maturity Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 22%">
            <telerik:RadDatePicker Enabled="false" ID="txtMaturityDate" CssClass="txtField" runat="server"
                Culture="English (United States)" Skin="Telerik" EnableEmbeddedSkins="false"
                MaxDate="2200-01-01" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
        </td>
        <td align="right" style="width: 15%">
            <asp:Label ID="lblSumAssured" runat="server" Text="Sum Assured:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 22%">
            <asp:TextBox ID="txtSumAssured1" runat="server" CssClass="txtField" ReadOnly="false"></asp:TextBox>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtSumAssured1"
                ErrorMessage="<br />Please enter the Sum Assured" Display="Dynamic" CssClass="rfvPCG"
                runat="server" InitialValue="" ValidationGroup="buttonSubmit">
            </asp:RequiredFieldValidator>
            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtSumAssured1"
                Display="Dynamic" CssClass="rfvPCG" runat="server" ErrorMessage="Not acceptable format"
                ValidationExpression="^\d*(\.(\d{0,5}))?$" ValidationGroup="buttonSubmit"></asp:RegularExpressionValidator>--%>
            <asp:CompareValidator ID="cv_txtSumAssured1" ControlToValidate="txtSumAssured1" Type="Double"
                Display="Dynamic" CssClass="cvPCG" runat="server" ErrorMessage="<br />Please enter a valid amount"
                ValidationGroup="buttonSubmit" ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>
        </td>
        <td style="width: 19%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="right" style="width: 22%">
            <asp:Label ID="lblWhtFamilyPolicy" runat="server" CssClass="FieldName" Text="Whether Family/Group Policy:"></asp:Label>
        </td>
        <td class="style3" style="width: 22%">
            <asp:RadioButton ID="rdoGroupPolicyYes" Text="Yes" runat="server" GroupName="Policy" 
                class="cmbField" onClick="ChangeGroupPolicy('Yes');" />
            <asp:RadioButton ID="rdoGroupPolicyNo" Text="No" runat="server" GroupName="Policy" Checked="true" 
                 class="cmbField" onClick="ChangeGroupPolicy('No');" />
        </td>
        <td align="right" style="width: 15%">
            <asp:Label ID="lblTypeOfPolicy" runat="server" CssClass="FieldName" Text="Type of Policy:"></asp:Label>
        </td>
        <td class="rightField" style="width: 22%">
            <asp:DropDownList ID="ddlTypeOfPolicy" runat="server" CssClass="cmbField" onChange="ChangePolicyType()"
                Enabled="false">
                <%--<asp:ListItem Text="Individual" Value="PTIND"></asp:ListItem>
                <asp:ListItem Text="Floater" Value="PTFLT"></asp:ListItem>--%>
            </asp:DropDownList>
        </td>
        <td style="width: 19%">
            &nbsp;
        </td>
    </tr>
    <tr id="Tr1" runat="server">
        <td style="width: 22%">
            &nbsp;
        </td>
        <td colspan="2" style="width: 44%">
            <div id="divGridView" style="width: 100%">
                <asp:GridView ID="gvNominees" runat="server" AutoGenerateColumns="False" DataKeyNames="AssociationId"
                    AllowSorting="False" CssClass="GridViewStyle">
                    <FooterStyle CssClass="FooterStyle" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <RowStyle CssClass="RowStyle" />
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkId" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:TemplateField HeaderText="Sum Assured">
                            <ItemTemplate>
                                <asp:TextBox ID="txtSumAssured" runat="server" CssClass="GridViewTxtField" Enabled="false"
                                    onblur="CalculateSum()" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </td>
        <td colspan="2" style="width: 34%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="right" style="width: 22%">
            <asp:Label ID="lblTPA" runat="server" CssClass="FieldName" Text="Name of the 3rd Party Admin.[TPA]:"></asp:Label>
        </td>
        <td class="style3" style="width: 22%">
            <asp:TextBox ID="txtTPA" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td align="right" style="width: 15%">
            <asp:Label ID="lblTPAContactNumber" runat="server" CssClass="FieldName" Text="Contact No. of TPA:"></asp:Label>
        </td>
        <td class="rightField" style="width: 22%">
            <asp:TextBox ID="txtTPAContactNumber" runat="server" CssClass="txtField"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="<br />Enter a numeric value"
                CssClass="cvPCG" Type="Integer" ControlToValidate="txtTPAContactNumber" Operator="DataTypeCheck"
                Display="Dynamic" ValidationGroup="buttonSubmit"></asp:CompareValidator>
        </td>
        <td style="width: 19%">
            &nbsp;
        </td>
    </tr>
    <tr id="trHealthSection1" runat="server">
        <td align="right" style="width: 22%">
            <asp:Label ID="lblWthHealthCheckUp" runat="server" CssClass="FieldName" Text="Whether Eligible for Health Checkup:"></asp:Label>
        </td>
        <td class="style3" style="width: 22%">
            <asp:RadioButton ID="rdoHealthYes" Text="Yes" runat="server" GroupName="Health" class="cmbField"
                onClick="EnableDisableCheckUpDate('Yes');" />
            <asp:RadioButton ID="rdoHealthNo" Text="No" runat="server" GroupName="Health" class="cmbField"
                Checked="true" onClick="EnableDisableCheckUpDate('No');" />
        </td>
        <td align="right" style="width: 15%">
            <asp:Label ID="lblCheckUpDate" runat="server" CssClass="FieldName" Text="If Yes, Date:"></asp:Label>
        </td>
        <td class="rightField" style="width: 22%">
            <telerik:RadDatePicker Width="178px" ID="txtCheckUpDate" CssClass="txtField" runat="server"
                Culture="English (United States)" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade"
                MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:CompareValidator ID="CompareValidator67" runat="server" ErrorMessage="<br />The date format should be MM/dd/yyyy"
                Type="Date" ControlToValidate="txtCheckUpDate" Operator="DataTypeCheck" CssClass="cvPCG"
                ValidationGroup="buttonSubmit" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td style="width: 19%">
            &nbsp;
        </td>
    </tr>
    <tr id="trHealthSection2" runat="server">
        <td align="right" style="width: 22%">
            <asp:Label ID="lblProposalNumber" runat="server" CssClass="FieldName" Text="Proposal Number:"></asp:Label>
        </td>
        <td class="style3" style="width: 22%">
            <asp:TextBox ID="txtProposalNumber" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td align="right" style="width: 15%">
            <asp:Label ID="lblProposalDate" runat="server" CssClass="FieldName" Text="Proposal Date:"></asp:Label>
        </td>
        <td class="rightField" style="width: 22%">
            <telerik:RadDatePicker Width="178px" ID="txtProposalDate" CssClass="txtField" runat="server"
                Culture="English (United States)" Skin="Telerik" EnableEmbeddedSkins="false"
                ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
        </td>
        <td style="width: 19%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="right" style="width: 22%">
            <asp:Label ID="lblPremiumCycle" runat="server" CssClass="FieldName" Text="Premium Cycle:"></asp:Label>
        </td>
        <td class="style3" style="width: 22%">
            <asp:DropDownList ID="ddlPremiumCycle" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="span3" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />Please select a Premium Cycle"
                ControlToValidate="ddlPremiumCycle" Operator="NotEqual" ValueToCompare="Select"
                Display="Dynamic" CssClass="cvPCG" ValidationGroup="buttonSubmit"></asp:CompareValidator>
        </td>
        <td colspan="3" style="width: 56%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="right" style="width: 22%">
            <asp:Label ID="lblPremiumAmount" runat="server" CssClass="FieldName" Text="Premium Amount:"></asp:Label>
        </td>
        <td class="style3" style="width: 22%">
            <asp:TextBox ID="txtPremiumAmount" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span4" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPremiumAmount"
                ErrorMessage="<br />Please enter Premium Amount" Display="Dynamic" CssClass="rfvPCG"
                runat="server" InitialValue="" ValidationGroup="buttonSubmit">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cv_txtPremiumAmount" ControlToValidate="txtPremiumAmount"
                Type="Double" Display="Dynamic" CssClass="cvPCG" runat="server" ErrorMessage="<br />Please enter a valid amount"
                ValidationGroup="buttonSubmit" ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>
        </td>
        <td colspan="3" style="width: 56%">
            &nbsp;
        </td>
    </tr>
    <tr id="trAssetGroupHeader" runat="server" visible="false">
        <td align="left" colspan="6" style="vertical-align: text-bottom; padding-top: 6px;
            padding-bottom: 6px">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Select Existing Asset Details
            </div>
        </td>
    </tr>
    <tr id="trAssetGroup" runat="server" visible="false">
        <td align="right" style="width: 22%">
            <asp:Label ID="lblAddAsset" runat="server" Text="Pick an Asset Group:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style3" style="width: 22%">
            <span id="spnchkProperty" runat="server" visible="false">
                <asp:CheckBox ID="chkProperty" runat="server" Text="Property" class="cmbField" />
            </span><span id="spnchkGold" runat="server" visible="false">
                <asp:CheckBox ID="chkGold" runat="server" Text="Gold & Jewellery" class="cmbField" />
            </span><span id="spnchkCollectibles" runat="server" visible="false">
                <br />
                <asp:CheckBox ID="chkCollectibles" runat="server" Text="Collectibles" class="cmbField" />
            </span><span id="spnchkPersonal" runat="server" visible="false">
                <asp:CheckBox ID="chkPersonal" runat="server" Text="Personal Items" class="cmbField" />
            </span>
        </td>
        <td style="width: 15%" align="left">
            <asp:Button ID="btnAssetShow" runat="server" Text="Show Assets" CssClass="PCGMediumButton"
                OnClick="btnAssetShow_Click" />
        </td>
        <td colspan="2" style="width: 41%">
            &nbsp;
        </td>
    </tr>
    <tr id="trProperty" runat="server">
        <td align="right" style="width: 22%">
            <asp:Label ID="Label6" runat="server" Text="Pick Properties:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" style="width: 22%">
            <asp:Panel ID="pnlPickAssetProperty" runat="server">
                <asp:PlaceHolder ID="phProperty" runat="server"></asp:PlaceHolder>
            </asp:Panel>
        </td>
        <td colspan="3" style="width: 56%">
            &nbsp;
        </td>
    </tr>
    <tr id="trCollectibles" runat="server">
        <td align="right" style="width: 22%">
            <asp:Label ID="Label2" runat="server" Text="Pick Collectibles:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" style="width: 22%">
            <asp:Panel ID="pnlPickAssetCollectibles" runat="server">
                <asp:PlaceHolder ID="phCollectibles" runat="server"></asp:PlaceHolder>
            </asp:Panel>
        </td>
        <td colspan="3" style="width: 56%">
            &nbsp;
        </td>
    </tr>
    <tr id="trGold" runat="server">
        <td align="right" style="width: 22%">
            <asp:Label ID="Label3" runat="server" Text="Pick Gold:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" style="width: 22%">
            <asp:Panel ID="pnlPickAssetGold" runat="server">
                <asp:PlaceHolder ID="phGold" runat="server"></asp:PlaceHolder>
            </asp:Panel>
        </td>
        <td colspan="3" style="width: 56%">
            &nbsp;
        </td>
    </tr>
    <tr id="trPersonal" runat="server">
        <td align="right" style="width: 22%">
            <asp:Label ID="Label4" runat="server" Text="Pick Personal Items:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" style="width: 22%">
            <asp:Panel ID="pnlPickAssetPersonal" runat="server">
                <asp:PlaceHolder ID="phPersonal" runat="server"></asp:PlaceHolder>
            </asp:Panel>
        </td>
        <td colspan="3" style="width: 56%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="right" style="width: 22%">
            <asp:Label ID="lblRemarks" runat="server" Text="Remarks :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style3" style="width: 22%">
            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="txtField"
                Height="75px" Width="208px"></asp:TextBox>
        </td>
        <td colspan="3" style="width: 56%">
            &nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr id="trSubmitButton" runat="server">
        <td colspan="6" class="SubmitCell">
            <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" Text="Submit" OnClick="btnSubmit_Click"
                CausesValidation="true" ValidationGroup="buttonSubmit" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_PortfolioGeneralInsuranceEntry_btnSubmit', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_PortfolioGeneralInsuranceEntry_btnSubmit', 'S');"
                OnClientClick="if(Page_ClientValidate()){return ValidateGroupMembersAndDates()};" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnSumAssured" runat="server" />
<asp:HiddenField ID="hdnGroupPolicy" runat="server" />
<asp:HiddenField ID="hdnPolicyType" runat="server" />
<asp:HiddenField ID="hdnFreeHealth" runat="server" />
