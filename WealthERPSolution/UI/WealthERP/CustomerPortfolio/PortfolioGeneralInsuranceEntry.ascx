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

<script type="text/javascript">
    function DownloadScript() {
        var btn = document.getElementById('<%= btnInsertNewScheme.ClientID %>');
        btn.click();
    }
</script>

<script runat="server">
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

    }
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
        <td colspan="4" class="style5">
            <%--<asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                OnClick="lnkBtnBack_Click"></asp:LinkButton>--%>
            <asp:LinkButton runat="server" ID="lnkBtnEdit" CssClass="LinkButtons" Text="Edit"
                OnClick="lnkBtnEdit_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblAssetCategory" runat="server" Text="Asset Category:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtAssetCategory" runat="server" Width="176px" CssClass="txtField"
                Enabled="false">
            </asp:TextBox>
        </td>
        <td align="right">
            <asp:Label ID="lblAssetSubCategory" runat="server" Text="Asset Sub Category:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtAssetSubCategory" runat="server" Width="176px" CssClass="txtField"
                Enabled="false">
            </asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblPolicyNumber" runat="server" Text="Policy Number:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style3">
            <asp:TextBox ID="txtPolicyNumber" runat="server" CssClass="txtField" Enabled="false"
                Width="176px"></asp:TextBox>
        </td>
        <%-- <td >
            &nbsp;
        </td>--%>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="lblAccountDetails" runat="server" Text="Account Details" CssClass="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblPolicyIssuer" runat="server" Text="Policy Issuer:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlPolicyIssuer" runat="server" Width="176px" CssClass="cmbField"
                OnSelectedIndexChanged="ddlPolicyIssuer_OnSelectedIndexChanged" AutoPostBack="true">
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
            <asp:Label ID="lblPolicyParticular" runat="server" Text="Scheme Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style3">
            <asp:DropDownList ID="txtPolicyParticular" runat="server" Width="176px" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td visible="false">
            <asp:Button ID="btnAddScheme" runat="server" CssClass="PCGLongButton" Text="Add Scheme" />
        </td>
    </tr>
    <tr>
        <td>
            <cc1:ModalPopupExtender ID="MPEAssetParticular" runat="server" TargetControlID="btnAddScheme"
                PopupControlID="Panel2" BackgroundCssClass="modalBackground" OnOkScript="DownloadScript()"
                Y="150" DropShadow="true" OkControlID="btnOk" CancelControlID="btnCancel" PopupDragHandleControlID="Panel2">
            </cc1:ModalPopupExtender>
        </td>
        <td>
            <asp:Panel ID="Panel2" runat="server" CssClass="ExortPanelpopup" Width="100%" Height="100%">
                <%-- <asp:UpdatePanel ID="udpSchemeName" runat="Server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
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
                            <asp:TextBox ID="txtAsset" runat="server" CssClass="txtField" ValidationGroup="vgOK"></asp:TextBox>
                            <span id="Span5" class="spnRequiredField">*</span>
                            <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtAsset" ErrorMessage="Please enter the Scheme Name"
                                ValidationGroup="vgOK" Display="Dynamic" runat="server" CssClass="rfvPCG">
                            </asp:RequiredFieldValidator>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="leftField" style="width: 10%">
                            <asp:Button ID="btnOk" runat="server" Text="OK" CssClass="PCGButton" CausesValidation="false"
                                ValidationGroup="vgOK" />
                        </td>
                        <td class="rightField" style="width: 25%">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="PCGButton" />
                        </td>
                    </tr>
                </table>
                <asp:Button ID="btnInsertNewScheme" runat="server" Text="Scheme" Style="display: none"
                    OnClick="btnInsertNewScheme_Click" CausesValidation="false" />
                <%--</ContentTemplate>--%>
                <triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnOk" EventName="Click" />
                    </triggers>
                <%--
                </asp:UpdatePanel>--%>
            </asp:Panel>
        </td>
        <td colspan="2">
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
            <asp:Label ID="lblPolicyCommencementDate" runat="server" CssClass="FieldName" Text="Policy Start Date:"></asp:Label>
        </td>
        <td class="style3">
            <telerik:RadDatePicker ID="txtPolicyCommencementDate" CssClass="txtField" runat="server"
                Culture="English (United States)" Skin="Telerik" EnableEmbeddedSkins="false"
                ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <calendar userowheadersasselectors="False" usecolumnheadersasselectors="False" viewselectortext="x"
                    skin="Telerik" enableembeddedskins="false">
                </calendar>
                <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                <dateinput displaydateformat="d/M/yyyy" dateformat="d/M/yyyy">
                </dateinput>
            </telerik:RadDatePicker>
            <%-- <asp:TextBox ID="txtPolicyCommencementDate" runat="server" Width="176px" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="txtPolicyCommencementDate_CalendarExtender" runat="server"
                TargetControlID="txtPolicyCommencementDate" Format="MM/dd/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtPolicyCommencementDate_TextBoxWatermarkExtender"
                runat="server" TargetControlID="txtPolicyCommencementDate" WatermarkText="mm/dd/yyyy">
            </cc1:TextBoxWatermarkExtender>--%>
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
            <asp:DropDownList ID="ddlTypeOfPolicy" runat="server" Width="176px" CssClass="Field"
                onChange="ChangePolicyType()" Enabled="false">
                <%--<asp:ListItem Text="Individual" Value="PTIND"></asp:ListItem>
                <asp:ListItem Text="Floater" Value="PTFLT"></asp:ListItem>--%>
            </asp:DropDownList>
        </td>
    </tr>
    <%--<tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>--%>
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
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblPolicyTerm" runat="server" CssClass="FieldName" Text="Policy Term:"></asp:Label>
        </td>
        <td class="style3">
            <%-- <asp:RadioButton ID="rdbPolicyTermDays" Text="Days" runat="server" onClick="return EnableDisableDaysMonths()"
                GroupName="PolicyTerm" class="cmbField" />
            <asp:RadioButton ID="rdbPolicyTermMonth" Text="Month" runat="server" onClick="return EnableDisableDaysMonths()"
                GroupName="PolicyTerm" Checked="true" class="cmbField" />--%>
            <asp:TextBox ID="txtPolicyTerm" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span6" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPolicyTerm"
                ErrorMessage="<br />Please enter the Policy Term" Display="Dynamic" CssClass="rfvPCG"
                runat="server" InitialValue="" ValidationGroup="buttonSubmit">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPolicyTerm"
                ErrorMessage="Please Enter Valid Term" ValidationExpression="^\d+$"  CssClass="rfvPCG" ValidationGroup="buttonSubmit"></asp:RegularExpressionValidator>
        </td>
        <td>
            <asp:DropDownList AutoPostBack="true" ID="ddlPeriodSelection" runat="server" CssClass="Field"
                OnSelectedIndexChanged="ddlPeriodSelection_SelectedIndexChanged">
                <asp:ListItem Text="Select" Value="0" Selected="True">               
                </asp:ListItem>
                <asp:ListItem Text="Days" Value="DA">               
                </asp:ListItem>
                <asp:ListItem Text="Months" Value="MN">               
                </asp:ListItem>
                <asp:ListItem Text="Years" Value="YR">               
                </asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlPeriodSelection"
                ErrorMessage="<br />Please select tenure" Display="Dynamic" CssClass="rfvPCG"
                runat="server" InitialValue="0" ValidationGroup="buttonSubmit">
            </asp:RequiredFieldValidator>
            <%--  <asp:Label ID="lblDays" runat="server" CssClass="FieldName" Text="Days:"></asp:Label>--%>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblMaturityDate" runat="server" Text="Maturity Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <telerik:RadDatePicker Enabled="false" ID="txtMaturityDate" CssClass="txtField" runat="server"
                Culture="English (United States)" Skin="Telerik" EnableEmbeddedSkins="false"
                ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <calendar userowheadersasselectors="False" usecolumnheadersasselectors="False" viewselectortext="x"
                    skin="Telerik" enableembeddedskins="false">
                </calendar>
                <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                <dateinput displaydateformat="d/M/yyyy" dateformat="d/M/yyyy">
                </dateinput>
            </telerik:RadDatePicker>
            <%-- <asp:TextBox ID="txtMaturityDate" runat="server" Width="176px" CssClass="txtField"
                Enabled="false"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtMaturityDate"
                Format="MM/dd/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtMaturityDate"
                WatermarkText="MM/dd/yyyy">
            </cc1:TextBoxWatermarkExtender>--%>
        </td>
        <td align="right">
            <asp:Label ID="lblSumAssured" runat="server" Text="Sum Assured:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtSumAssured1" runat="server" Width="176px" CssClass="txtField"
                ReadOnly="false"></asp:TextBox>
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
        <td align="right">
            <asp:Label ID="lblTPA" runat="server" CssClass="FieldName" Text="Name of the Third Party Administartor[TPA]:"></asp:Label>
        </td>
        <td class="style3">
            <asp:TextBox ID="txtTPA" runat="server" Width="176px" CssClass="txtField"></asp:TextBox>
        </td>
        <td align="right">
            <asp:Label ID="lblTPAContactNumber" runat="server" CssClass="FieldName" Text="Contact No. of TPA:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtTPAContactNumber" runat="server" Width="176px" CssClass="txtField"></asp:TextBox>
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
            <telerik:RadDatePicker ID="txtCheckUpDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <calendar userowheadersasselectors="False" usecolumnheadersasselectors="False" viewselectortext="x"
                    skin="Telerik" enableembeddedskins="false">
                </calendar>
                <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                <dateinput displaydateformat="d/M/yyyy" dateformat="d/M/yyyy">
                </dateinput>
            </telerik:RadDatePicker>
            <%--<asp:TextBox ID="txtCheckUpDate" runat="server" Width="176px" CssClass="txtField"
                Enabled="false"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCheckUpDate"
                Format="MM/dd/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtCheckUpDate"
                WatermarkText="MM/dd/yyyy">
            </cc1:TextBoxWatermarkExtender>--%>
            <%--<span id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvPolicyCommencementDate" ControlToValidate="txtPolicyCommencementDate"
                ErrorMessage="Please select a Policy Commencement Date" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator67" runat="server" ErrorMessage="<br />The date format should be MM/dd/yyyy"
                Type="Date" ControlToValidate="txtCheckUpDate" Operator="DataTypeCheck" CssClass="cvPCG"
                ValidationGroup="buttonSubmit" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblProposalNumber" runat="server" CssClass="FieldName" Text="Proposal Number:"></asp:Label>
        </td>
        <td class="style3">
            <asp:TextBox ID="txtProposalNumber" Width="176px" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td align="right">
            <asp:Label ID="lblProposalDate" runat="server" CssClass="FieldName" Text="Proposal Date:"></asp:Label>
        </td>
        <td class="rightField">
            <telerik:RadDatePicker ID="txtProposalDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <calendar userowheadersasselectors="False" usecolumnheadersasselectors="False" viewselectortext="x"
                    skin="Telerik" enableembeddedskins="false">
                </calendar>
                <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                <dateinput displaydateformat="d/M/yyyy" dateformat="d/M/yyyy">
                </dateinput>
            </telerik:RadDatePicker>
            <%-- <asp:TextBox ID="txtProposalDate" runat="server" Width="176px" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtProposalDate"
                Format="MM/dd/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtProposalDate"
                WatermarkText="MM/dd/yyyy">
            </cc1:TextBoxWatermarkExtender>--%>
            <%--<span id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvPolicyCommencementDate" ControlToValidate="txtPolicyCommencementDate"
                ErrorMessage="Please select a Policy Commencement Date" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="The date format should be MM/dd/yyyyyy"
                Type="Date" ControlToValidate="txtPolicyCommencementDate" Operator="DataTypeCheck"
                CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>--%>
        </td>
    </tr>
    <%-- <tr>
        <td align="right">
            <asp:Label ID="lblPolicyValidityStartDate" runat="server" CssClass="FieldName" Text="Policy Validity Start Date:"></asp:Label>
        </td>
        <td class="style3">
            <telerik:RadDatePicker ID="txtPolicyValidityStartDate" CssClass="txtField" runat="server"
                Culture="English (United States)" Skin="Telerik" EnableEmbeddedSkins="false"
                ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:TextBox ID="txtPolicyValidityStartDate" Width="176px" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtPolicyValidityStartDate"
                Format="MM/dd/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtPolicyValidityStartDate"
                WatermarkText="MM/dd/yyyy">
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
            <telerik:RadDatePicker ID="txtPolicyValidityEndDate" CssClass="txtField" runat="server"
                Culture="English (United States)" Skin="Telerik" EnableEmbeddedSkins="false"
                ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:TextBox ID="txtPolicyValidityEndDate" runat="server" Width="176px" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtPolicyValidityEndDate"
                Format="MM/dd/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtPolicyValidityEndDate"
                WatermarkText="MM/dd/yyyy">
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
    </tr>--%>
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
            <asp:DropDownList ID="ddlPremiumCycle" Width="176px" runat="server" CssClass="cmbField">
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
            <asp:TextBox ID="txtPremiumAmount" Width="176px" runat="server" CssClass="txtField"></asp:TextBox>
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
            <asp:Button ID="btnAssetShow" runat="server" Text="Show Assets" CssClass="PCGMediumButton"
                OnClick="btnAssetShow_Click" />
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
            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="txtField"
                Height="75px" Width="208px"></asp:TextBox>
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

