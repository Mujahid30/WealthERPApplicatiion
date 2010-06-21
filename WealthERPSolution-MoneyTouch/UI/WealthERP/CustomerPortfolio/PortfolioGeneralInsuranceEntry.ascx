<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioGeneralInsuranceEntry.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioGeneralInsuranceEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
</style>
<%--Javascript Calendar Controls - Required Files--%>
<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>

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
            document.getElementById('<%= txtSumAssured1.ClientID %>').disabled = true;
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
                }
                // if this input type is checkbox, uncheck
                if (gridViewControls[i].type == "checkbox") {
                    gridViewControls[i].checked = false;
                }
            }
            document.getElementById('<%= txtSumAssured1.ClientID %>').disabled = false;
        }
        else {
            for (i = 0; i < gridViewControls.length; i++) {
                // if this input type is textbox, enable and clear
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
            document.getElementById('<%=txtCheckUpDate.ClientID %>').value = 'dd/mm/yyyy';
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
        var validityStartDate = document.getElementById('<%=txtPolicyValidityStartDate.ClientID %>').value;
        validityStartDate = changeDate(validityStartDate);
        var validityEndDate = document.getElementById('<%=txtPolicyValidityEndDate.ClientID %>').value;
        validityEndDate = changeDate(validityEndDate);
        if (document.getElementById('<%=txtProposalDate.ClientID %>').value != '' && document.getElementById('<%=txtProposalDate.ClientID %>').value != 'dd/mm/yyyy') {
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
        //            if (document.getElementById('<%=txtCheckUpDate.ClientID %>').value == '' || document.getElementById('<%=txtCheckUpDate.ClientID %>').value == 'dd/mm/yyyy') {
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

<table style="width: 100%;">
    <tr>
        <td colspan="4">
            <asp:Label ID="lblGeneralInsuranceEntryHeader" class="HeaderTextBig" runat="server"
                Text="General Insurance Add Screen"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="4" class="tdRequiredText">
            <label id="lbl" class="lblRequiredText">
                Note: Fields marked with ' * ' are compulsory</label>
        </td>
    </tr>
    <tr>
        <td class="style4">
            <%--<asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                OnClick="lnkBtnBack_Click"></asp:LinkButton>--%>
            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                OnClick="lnkBtnBack_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <%--<asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                OnClick="lnkBtnBack_Click"></asp:LinkButton>--%>
            <asp:LinkButton runat="server" ID="lnkBtnEdit" CssClass="LinkButtons" Text="Edit"
                OnClick="lnkBtnEdit_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblAssetCategory" runat="server" Text="Asset Category:" CssClass="FieldName" ></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtAssetCategory" runat="server" CssClass="txtField" Enabled="false">
            </asp:TextBox>
        </td>
        <td align="right">
            <asp:Label ID="lblAssetSubCategory" runat="server" Text="Asset Sub Category:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtAssetSubCategory" runat="server" CssClass="txtField" Enabled="false">
            </asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="lblAccountDetails" runat="server" Text="Account Details" CssClass="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblPolicyNumber" runat="server" Text="Policy Number:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style3">
            <asp:TextBox ID="txtPolicyNumber" runat="server" CssClass="txtField" Enabled="false">
            </asp:TextBox>
        </td>
        <td align="right">
            <asp:Label ID="lblPolicyIssuer" runat="server" Text="Policy Issuer:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlPolicyIssuer" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
            </asp:DropDownList>
            <span id="span1" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="cv_ddlPolicyIssuer" runat="server" ErrorMessage="<br />Please select an Insurance Issuer"
                ControlToValidate="ddlPolicyIssuer" Operator="NotEqual" ValueToCompare="Select"
                Display="Dynamic" CssClass="cvPCG" ValidationGroup="buttonSubmit"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblPolicyParticular" runat="server" Text="Policy Particular:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style3">
            <asp:TextBox ID="txtPolicyParticular" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td colspan="2">
            &nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="Label23" runat="server" CssClass="HeaderTextSmall" Text="Policy Details"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblPolicyCommencementDate" runat="server" CssClass="FieldName" Text="Policy Original Start Date:"></asp:Label>
        </td>
        <td class="style3">
            <asp:TextBox ID="txtPolicyCommencementDate" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="txtPolicyCommencementDate_CalendarExtender" runat="server"
                TargetControlID="txtPolicyCommencementDate" Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtPolicyCommencementDate_TextBoxWatermarkExtender"
                runat="server" TargetControlID="txtPolicyCommencementDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:CompareValidator ID="cv2_txtPolicyCommencementDate" runat="server" Operator="LessThan"
                ErrorMessage="<br />Please Select a valid date" Type="Date" ControlToValidate="txtPolicyCommencementDate"
                CssClass="cvPCG" ValidationGroup="buttonSubmit"></asp:CompareValidator>
        </td>
        <td colspan="2" align="center">
            <asp:CheckBox ID="chkIsPolicyByEmployer" Text="Policy provided by employer" runat="server"
                class="cmbField" onClick="EnableDisableValidators();" />
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblWhtFamilyPolicy" runat="server" CssClass="FieldName" Text="Whether Family/Group Policy:"></asp:Label>
        </td>
        <td class="style3">
            <asp:RadioButton ID="rdoGroupPolicyYes" Text="Yes" runat="server" GroupName="Policy"
                class="cmbField" onClick="ChangeGroupPolicy('Yes');" />
            <asp:RadioButton ID="rdoGroupPolicyNo" Text="No" runat="server" GroupName="Policy"
                Checked="true" class="cmbField" onClick="ChangeGroupPolicy('No');" />
        </td>
        <td align="right">
            <asp:Label ID="lblTypeOfPolicy" runat="server" CssClass="FieldName" Text="Type of Policy:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlTypeOfPolicy" runat="server" CssClass="Field" onChange="ChangePolicyType()"
                Enabled="false">
                <%--<asp:ListItem Text="Individual" Value="PTIND"></asp:ListItem>
                <asp:ListItem Text="Floater" Value="PTFLT"></asp:ListItem>--%>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <div id="divGridView">
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
        <td class="style3">
            &nbsp;&nbsp;
        </td>
        <td align="right">
            <asp:Label ID="lblSumAssured" runat="server" Text="Sum Assured:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
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
    </tr>
    <tr>
        <td class="style4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblTPA" runat="server" CssClass="FieldName" Text="Name of the Third Party Administartor[TPA]:"></asp:Label>
        </td>
        <td class="style3">
            <asp:TextBox ID="txtTPA" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td align="right">
            <asp:Label ID="lblTPAContactNumber" runat="server" CssClass="FieldName" Text="Contact No. of TPA:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtTPAContactNumber" runat="server" CssClass="txtField"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="<br />Enter a numeric value"
                CssClass="cvPCG" Type="Integer" ControlToValidate="txtTPAContactNumber" Operator="DataTypeCheck"
                Display="Dynamic" ValidationGroup="buttonSubmit"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblWthHealthCheckUp" runat="server" CssClass="FieldName" Text="Whether Eligible for Health Checkup:"></asp:Label>
        </td>
        <td class="style3">
            <asp:RadioButton ID="rdoHealthYes" Text="Yes" runat="server" GroupName="Health" class="cmbField"
                onClick="EnableDisableCheckUpDate('Yes');" />
            <asp:RadioButton ID="rdoHealthNo" Text="No" runat="server" GroupName="Health" class="cmbField"
                Checked="true" onClick="EnableDisableCheckUpDate('No');" />
        </td>
        <td align="right">
            <asp:Label ID="lblCheckUpDate" runat="server" CssClass="FieldName" Text="If Yes, Date:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtCheckUpDate" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCheckUpDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtCheckUpDate"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <%--<span id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvPolicyCommencementDate" ControlToValidate="txtPolicyCommencementDate"
                ErrorMessage="Please select a Policy Commencement Date" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator67" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyyyy"
                Type="Date" ControlToValidate="txtCheckUpDate" Operator="DataTypeCheck" CssClass="cvPCG"
                ValidationGroup="buttonSubmit" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblProposalNumber" runat="server" CssClass="FieldName" Text="Proposal Number:"></asp:Label>
        </td>
        <td class="style3">
            <asp:TextBox ID="txtProposalNumber" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td align="right">
            <asp:Label ID="lblProposalDate" runat="server" CssClass="FieldName" Text="Proposal Date:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtProposalDate" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtProposalDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtProposalDate"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <%--<span id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvPolicyCommencementDate" ControlToValidate="txtPolicyCommencementDate"
                ErrorMessage="Please select a Policy Commencement Date" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="The date format should be dd/mm/yyyyyy"
                Type="Date" ControlToValidate="txtPolicyCommencementDate" Operator="DataTypeCheck"
                CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>--%>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblPolicyValidityStartDate" runat="server" CssClass="FieldName" Text="Policy Validity Start Date:"></asp:Label>
        </td>
        <td class="style3">
            <asp:TextBox ID="txtPolicyValidityStartDate" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtPolicyValidityStartDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtPolicyValidityStartDate"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvPolicyValidityStartDate" ControlToValidate="txtPolicyValidityStartDate"
                ErrorMessage="<br />Please select a Policy Validity Start Date" Display="Dynamic"
                runat="server" CssClass="rfvPCG" ValidationGroup="buttonSubmit">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="<br />Please enter a valid date"
                Type="Date" ControlToValidate="txtPolicyValidityStartDate" Operator="DataTypeCheck"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="buttonSubmit"></asp:CompareValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblPolicyValidityEndDate" runat="server" CssClass="FieldName" Text="Policy Validity End Date:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtPolicyValidityEndDate" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtPolicyValidityEndDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtPolicyValidityEndDate"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvPolicyValidityEndDate" ControlToValidate="txtPolicyValidityEndDate"
                ErrorMessage="<br />Please select a Policy Validity End Date" Display="Dynamic"
                runat="server" CssClass="rfvPCG" ValidationGroup="buttonSubmit">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator77" runat="server" ErrorMessage="<br />Please enter a valid date"
                Type="Date" ControlToValidate="txtPolicyValidityEndDate" Operator="GreaterThan"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="buttonSubmit"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <%--Start Editing From Here--%>
    <tr>
        <td colspan="4">
            <asp:Label ID="Label24" runat="server" CssClass="HeaderTextSmall" Text="Premium Details"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblPremiumCycle" runat="server" CssClass="FieldName" Text="Premium Cycle:"></asp:Label>
        </td>
        <td class="style3">
            <asp:DropDownList ID="ddlPremiumCycle" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="span3" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />Please select a Premium Cycle"
                ControlToValidate="ddlPremiumCycle" Operator="NotEqual" ValueToCompare="Select"
                Display="Dynamic" CssClass="cvPCG" ValidationGroup="buttonSubmit"></asp:CompareValidator>
        </td>
        <td colspan="2">
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblPremiumAmount" runat="server" CssClass="FieldName" Text="Premium Amount:"></asp:Label>
        </td>
        <td class="style3">
            <asp:TextBox ID="txtPremiumAmount" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span4" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPremiumAmount"
                ErrorMessage="<br />Please enter Premium Amount" Display="Dynamic" CssClass="rfvPCG"
                runat="server" InitialValue="" ValidationGroup="buttonSubmit">
            </asp:RequiredFieldValidator>
            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtPremiumAmount"
                Display="Dynamic" CssClass="rfvPCG" runat="server" ErrorMessage="Not acceptable format"
                ValidationExpression="^\d*(\.(\d{0,5}))?$" ValidationGroup="buttonSubmit"></asp:RegularExpressionValidator>--%>
            <asp:CompareValidator ID="cv_txtPremiumAmount" ControlToValidate="txtPremiumAmount"
                Type="Double" Display="Dynamic" CssClass="cvPCG" runat="server" ErrorMessage="<br />Please enter a valid amount"
                ValidationGroup="buttonSubmit" ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>
        </td>
        <td colspan="2" class="style1">
        </td>
    </tr>
    <tr id="trAssetGroupHeader" runat="server" visible="false">
        <td colspan="4">
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextSmall" Text="Select Existing Asset Details"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr id="trAssetGroup" runat="server" visible="false">
        <td align="right">
            <asp:Label ID="lblAddAsset" runat="server" Text="Pick an Asset Group:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style3">
            <span id="spnchkProperty" runat="server" visible="false">
                <asp:CheckBox ID="chkProperty" runat="server" Text="Property" class="cmbField" />
            </span><span id="spnchkGold" runat="server" visible="false">&nbsp;&nbsp;&nbsp;<asp:CheckBox
                ID="chkGold" runat="server" Text="Gold & Jewellery" class="cmbField" />
            </span><span id="spnchkCollectibles" runat="server" visible="false">
                <br />
                <asp:CheckBox ID="chkCollectibles" runat="server" Text="Collectibles" class="cmbField" />
            </span><span id="spnchkPersonal" runat="server" visible="false">
                <asp:CheckBox ID="chkPersonal" runat="server" Text="Personal Items" class="cmbField" />
            </span>
        </td>
        <td colspan="2">
            <asp:Button ID="btnAssetShow" runat="server" Text="Show Assets" OnClick="btnAssetShow_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="4">
        </td>
    </tr>
    <asp:Panel ID="pnlPickAssetProperty" runat="server" Visible="false">
        <tr>
            <td align="left" colspan="4">
                <br />
                <asp:Label ID="Label6" runat="server" Text="Pick Properties:" CssClass="FieldName"></asp:Label>
                <br />
                <asp:PlaceHolder ID="phProperty" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
    </asp:Panel>
    <asp:Panel ID="pnlPickAssetCollectibles" runat="server" Visible="false">
        <tr>
            <td align="left" colspan="4">
                <br />
                <asp:Label ID="Label2" runat="server" Text="Pick Collectibles:" CssClass="FieldName"></asp:Label>
                <br />
                <asp:PlaceHolder ID="phCollectibles" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
    </asp:Panel>
    <asp:Panel ID="pnlPickAssetGold" runat="server" Visible="false">
        <tr>
            <td align="left" colspan="4">
                <br />
                <asp:Label ID="Label3" runat="server" Text="Pick Gold:" CssClass="FieldName"></asp:Label>
                <br />
                <asp:PlaceHolder ID="phGold" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
    </asp:Panel>
    <asp:Panel ID="pnlPickAssetPersonal" runat="server" Visible="false">
        <tr>
            <td align="left" colspan="4">
                <br />
                <asp:Label ID="Label4" runat="server" Text="Pick Personal Items:" CssClass="FieldName"></asp:Label>
                <br />
                <asp:PlaceHolder ID="phPersonal" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
    </asp:Panel>
    <tr>
        <td colspan="4">
            &nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblRemarks" runat="server" Text="Remarks :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style3">
            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" 
                CssClass="txtField" Height="75px" Width="208px"></asp:TextBox>
        </td>
        <td colspan="2">
            &nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr id="trSubmitButton" runat="server">
        <td colspan="4" class="SubmitCell">
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

<script language="javascript" type="text/javascript">
    StateMaintain();
    window.onload = EnableDisableValidators;
</script>

