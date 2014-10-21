<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReceivableSetup.ascx.cs"
    Inherits="WealthERP.Receivable.ReceivableSetup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1" />

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.2.6.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script language="JavaScript" type="text/jscript">
    function DeleteAllStructureRule() {
        alert(hi);
        var conf = confirm("Are you sure you want to delete this image?");

        if (conf == true) {
            return false;
        }
        else
            return false;

    }
</script>

<script language="JavaScript" type="text/jscript">

    function InvestmentAmountValidation(source, args) {
        args.IsValid = false;
        var minValue = document.getElementById('ctrl_ReceivableSetup_RadGridStructureRule_ctl00_ctl02_ctl03_txtMinInvestmentAmount').value;
        var maxValue = document.getElementById('ctrl_ReceivableSetup_RadGridStructureRule_ctl00_ctl02_ctl03_txtMaxInvestmentAmount').value;
        if (parseInt(maxValue) > parseInt(minValue))
            args.IsValid = true;

        if ((minValue == "" && maxValue == "") || (minValue != "" && maxValue == ""))
            args.IsValid = true;

    }

    //    function alertTest() {
    //        var maxValue = document.getElementById('ctrl_ReceivableSetup_RadGridStructureRule_ctl00_ctl02_ctl03_txtMaxTenure').value;
    //        if (maxValue=="")
    //            alert("blank");
    //            else
    //                alert("empty");
    //    }

    function TenureValidation(source, args) {
        args.IsValid = false;
        var minValue = document.getElementById('ctrl_ReceivableSetup_RadGridStructureRule_ctl00_ctl02_ctl03_txtMinTenure').value;
        var maxValue = document.getElementById('ctrl_ReceivableSetup_RadGridStructureRule_ctl00_ctl02_ctl03_txtMaxTenure').value;
        if (parseInt(maxValue) > parseInt(minValue))
            args.IsValid = true;

        if ((minValue == "" && maxValue == "") || (minValue != "" && maxValue == ""))
            args.IsValid = true;
    }

    function InvestmentAgeValidation(source, args) {
        args.IsValid = false;
        var minValue = document.getElementById('ctrl_ReceivableSetup_RadGridStructureRule_ctl00_ctl02_ctl03_txtMinInvestAge').value;
        var maxValue = document.getElementById('ctrl_ReceivableSetup_RadGridStructureRule_ctl00_ctl02_ctl03_txtMaxInvestAge').value;
        if (parseInt(minValue) < parseInt(maxValue)) {
            args.IsValid = true;

        }

        if ((minValue == "" && maxValue == "") || (minValue != "" && maxValue == "")) {
            args.IsValid = true;
        }
    }


</script>

<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<style type="text/css">
    .table
    {
        border: 1px solid orange;
    }
    .leftLabel
    {
        width: 15%;
        text-align: right;
    }
    .rightData
    {
        width: 18%;
        text-align: left;
    }
    .rightDataTwoColumn
    {
        width: 25%;
        text-align: left;
    }
    .rightDataFourColumn
    {
        width: 50%;
        text-align: left;
    }
    .rightDataThreeColumn
    {
        width: 41%;
        text-align: left;
    }
    .tdSectionHeading
    {
        padding-bottom: 6px;
        padding-top: 6px;
        width: 100%;
    }
    .divSectionHeading table td span
    {
        padding-bottom: 5px !important;
    }
    .fltlft
    {
        float: left;
        padding-left: 3px;
        width: 20%;
    }
    .divCollapseImage
    {
        float: left;
        padding-left: 5px;
        width: 2%;
        float: right;
        text-align: right;
        cursor: pointer;
        cursor: hand;
    }
    .imgCollapse
    {
        background: Url(../Images/Section-Expand.png);
        cursor: pointer;
        cursor: hand;
    }
    .imgExpand
    {
        background: Url(../Images/Section-Collapse.png) no-repeat left top;
        cursor: pointer;
        cursor: hand;
    }
    .fltlftStep
    {
        float: left;
    }
    .StepOneContentTable, .StepTwoContentTable, .StageRequestTable, .StepThreeContentTable, .StepFourContentTable
    {
        width: 100%;
    }
    .SectionBody
    {
        width: 100%;
    }
    .collapse
    {
        text-align: right;
    }
    .divStepStatus
    {
        float: left;
        padding-left: 2px;
        padding-right: 5px;
    }
    .divViewEdit
    {
        padding-right: 15px;
        width: 2%;
        float: right;
        text-align: right;
        cursor: hand;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table width="100%">
            <tr>
                <td>
                    <div class="divPageHeading">
                        <table cellspacing="0" cellpadding="3" width="100%">
                            <tr>
                                <td align="left">
                                    Commission Receivable Structure Setup
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <table width="100%">
            <%--***********************************************Commission receivable Structure setup********************************--%>
            <tr id="trStepOneHeading" runat="server">
                <td class="tdSectionHeading" colspan="5">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        <div class="divSectionHeadingNumber fltlftStep">
                            1
                        </div>
                        <div class="fltlft">
                            &nbsp;
                            <asp:Label ID="Label2" runat="server" Text="Commission Structure"></asp:Label>
                        </div>
                        <div class="divViewEdit">
                            <asp:LinkButton ID="lnkAddNewStructure" Text="Add" runat="server" CssClass="LinkButtons"
                                ToolTip="Add new commission structure" OnClick="lnkAddNewStructure_Click">
                            </asp:LinkButton>
                        </div>
                        <div class="divViewEdit">
                            <asp:LinkButton ID="lnkEditStructure" Text="Edit" runat="server" CssClass="LinkButtons"
                                OnClick="lnkEditStructure_Click">
                            </asp:LinkButton>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="Label3" runat="server" Text="Pick CommissionType:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlCommissionype" runat="server" CssClass="cmbField" AutoPostBack="true">
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Select CommissionType" Display="Dynamic" ControlToValidate="ddlCommissionype"
                        InitialValue="Select" ValidationGroup="vgBtnSubmitStage2">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lblStatusStage2" runat="server" Text="Pick Product:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlProductType" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlProductType_OnSelectedIndexChanged">
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Select Product Type" Display="Dynamic" ControlToValidate="ddlProductType"
                        InitialValue="Select" ValidationGroup="vgBtnSubmitStage2">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged">
                    </asp:DropDownList>
                    <span id="SpanCategory" class="spnRequiredField" runat="server">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Select Category"
                        CssClass="rfvPCG" ControlToValidate="ddlCategory" ValidationGroup="btnStrAddUpdate"
                        Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                </td>
                <td align="left">
                    <asp:Label ID="lblSubCategory" runat="server" Text="Sub Category:" CssClass="FieldName"></asp:Label>
                    <span id="SpanSubCategory" class="spnRequiredField" runat="server">*</span>
                </td>
            </tr>
            <tr id="trIssuer" runat="server">
                <td class="leftLabel">
                    <asp:Label ID="lblIssuer" runat="server" Text="Issuer :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlIssuer" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                    <span id="Span6" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Issuer"
                        CssClass="rfvPCG" ControlToValidate="ddlIssuer" ValidationGroup="btnStrAddUpdate"
                        Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                </td>
                <td class="leftLabel">
                </td>
                <td class="rightData">
                </td>
                <td rowspan="5" class="rightDataTwoColumn">
                    <telerik:RadListBox ID="rlbAssetSubCategory" runat="server" CheckBoxes="true" CssClass="txtField"
                        Width="220px" Height="200px">
                    </telerik:RadListBox>
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lblValidityFrom" runat="server" Text="Validity From :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtValidityFrom" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span1" class="spnRequiredField">*</span>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtValidityFrom"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtValidityFrom"
                        WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br/>Please enter a valid date."
                        Type="Date" ControlToValidate="txtValidityFrom" CssClass="cvPCG" Operator="DataTypeCheck"
                        ValidationGroup="vgBtnSubmitTemp" ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtValidityFrom"
                        ErrorMessage="<br />Please enter a validity from Date" Display="Dynamic" CssClass="rfvPCG"
                        runat="server" InitialValue="" ValidationGroup="btnStrAddUpdate">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lblValidityTo" runat="server" Text="To:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtValidityTo" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span3" class="spnRequiredField">*</span>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtValidityTo"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtValidityTo"
                        WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <asp:CompareValidator ID="CVReceivedDate" runat="server" ErrorMessage="<br/>Please enter a valid date."
                        Type="Date" ControlToValidate="txtValidityTo" CssClass="cvPCG" Operator="DataTypeCheck"
                        ValidationGroup="vgBtnSubmitTemp" ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtValidityTo"
                        ErrorMessage="<br />Please enter a validity to Date" Display="Dynamic" CssClass="rfvPCG"
                        runat="server" InitialValue="" ValidationGroup="btnStrAddUpdate">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ControlToCompare="txtValidityFrom" ControlToValidate="txtValidityTo"
                        Display="Dynamic" CssClass="rfvPCG" ValidationGroup="btnStrAddUpdate" ErrorMessage="The Validity To must be greater than or equal to Validity From"
                        ID="CompareValidator2" Operator="GreaterThanEqual" Type="Date" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lblStructureName" runat="server" Text="Name:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightDataThreeColumn" colspan="3">
                    <asp:TextBox ID="txtStructureName" runat="server" CssClass="txtField" Style="width: 70% !Important"></asp:TextBox>
                    <span id="Span2" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtStructureName"
                        ErrorMessage="<br />Structure name required" Display="Dynamic" CssClass="rfvPCG"
                        runat="server" InitialValue="" ValidationGroup="btnStrAddUpdate">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lblOptions" runat="server" Text="Options:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightDataThreeColumn" colspan="3">
                    <asp:CheckBox ID="chkHasClawBackOption" Text="" runat="server" Visible="false" />
                    <asp:Label ID="lblHasClawBackOption" runat="server" Text="Has claw back option" CssClass="txtField"
                        Visible="false"></asp:Label>
                    <asp:CheckBox ID="chkMoneytaryReward" Text="" runat="server" />
                    <asp:Label ID="Label1" runat="server" Text="Is non moneytary reward" CssClass="txtField"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lblNote" runat="server" Text="Note:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightDataThreeColumn" colspan="3">
                    <asp:TextBox ID="txtNote" runat="server" CssClass="txtField" TextMode="MultiLine"
                        Width="50%"></asp:TextBox>
                    <asp:Button ID="btnStructureSubmit" CssClass="PCGButton" Text="Submit" runat="server"
                        ValidationGroup="btnStrAddUpdate" OnClick="btnStructureSubmit_Click" />
                    <asp:Button ID="btnStructureUpdate" CssClass="PCGButton" Text="Update" runat="server"
                        OnClick="btnStructureUpdate_Click" ValidationGroup="btnStrAddUpdate" />
                    <asp:Button ID="btnMapToscheme" CssClass="PCGMediumButton" Text="Map Scheme" runat="server"
                        OnClick="btnMapToscheme_Click" ValidationGroup="btnStrAddUpdate" />
                         <asp:Button ID="ButtonAgentCodeMapping" CssClass="PCGMediumButton" Text="Map AgentCode" runat="server"
                        OnClick="ButtonAgentCodeMapping_Click" ValidationGroup="btnStrAddUpdate" />
                </td>
            </tr>
        </table>
        <table id="tblCommissionStructureRule" runat="server" width="100%">
            <tr id="trStepTwoHeading" runat="server">
                <td class="tdSectionHeading">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        <div class="divSectionHeadingNumber fltlftStep">
                            2
                        </div>
                        <div class="fltlft" style="width: 200px;">
                            &nbsp;
                            <asp:Label ID="lblStage" runat="server" Text="Structure Rule Details"></asp:Label>
                        </div>
                        <div class="divViewEdit" style="padding-right: 10px;">
                            <asp:ImageButton ID="imgexportButton" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                Visible="true" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                OnClick="btnExportData_OnClick" OnClientClick="setFormat('excel')" Height="22px"
                                Width="25px"></asp:ImageButton>
                        </div>
                        <div class="divViewEdit" style="padding-right: 30px;">
                            <asp:LinkButton ID="lnkDeleteAllRule" Text="Delete" ToolTip="Delete commission structure all rule"
                                runat="server" CssClass="LinkButtons" OnClientClick="return confirm('Do you want to delete structure all rules? Click OK to proceed');"
                                OnClick="lnkDeleteAllRule_Click">
                            </asp:LinkButton>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <table id="tblCommissionStructureRule1" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Panel ID="Panel2" runat="server" class="Landscape" Width="90%" ScrollBars="Horizontal">
                        <telerik:RadGrid ID="RadGridStructureRule" runat="server" CssClass="RadGrid" GridLines="Both"
                            AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false"
                            ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
                            AllowAutomaticUpdates="false" Skin="Telerik" OnItemDataBound="RadGridStructureRule_ItemDataBound"
                            OnNeedDataSource="RadGridStructureRule_NeedDataSource" OnInsertCommand="RadGridStructureRule_InsertCommand"
                            OnItemCommand="RadGridStructureRule_ItemCommand" OnDeleteCommand="RadGridStructureRule_DeleteCommand"
                            OnUpdateCommand="RadGridStructureRule_UpdateCommand" Width="100%">
                            <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="CommissionStructureRule">
                            </ExportSettings>
                            <MasterTableView CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="false"
                                EditMode="EditForms" CommandItemSettings-AddNewRecordText="Create New Commission Structure Rule"
                                DataKeyNames="ACSR_CommissionStructureRuleId,ACSR_MinTenure,WCT_CommissionTypeCode,XCT_CustomerTypeCode,ACSR_TenureUnit,
                                ACSR_TransactionType,WCU_UnitCode,WCCO_CalculatedOnCode,ACSM_AUMFrequency,ACSR_MaxTenure,ACSR_SIPFrequency,ACG_CityGroupID,
                                ACSR_ReceivableRuleFrequency,WCAL_ApplicableLevelCode,ACSR_IsServiceTaxReduced,ACSR_IsTDSReduced,ACSM_IsOtherTaxReduced">
                                <Columns>
                                    <telerik:GridEditCommandColumn>
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridButtonColumn CommandName="Delete" Text="Delete" ConfirmText="Do you want to delete this rule? Click OK to proceed"
                                        UniqueName="column">
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn UniqueName="WCT_CommissionType" HeaderText="Commission Type "
                                        DataField="WCT_CommissionType">
                                        <HeaderStyle></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="SchemeName" HeaderText="Customer Category" DataField="XCT_CustomerTypeName">
                                        <%--<HeaderStyle ForeColor="Silver"></HeaderStyle>--%>
                                        <%-- <ItemStyle ForeColor="Gray" />--%>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ACSR_MinInvestmentAmount" HeaderText="Min Invest Amount"
                                        DataField="ACSR_MinInvestmentAmount" DataFormatString="{0:N2}">
                                        <HeaderStyle></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ACSR_MaxInvestmentAmount" HeaderText="Max Invest Amount"
                                        DataField="ACSR_MaxInvestmentAmount" DataFormatString="{0:N2}">
                                        <HeaderStyle></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ACSR_MinTenure" HeaderText="Min Tenure (SIP)"
                                        DataField="ACSR_MinTenure">
                                        <HeaderStyle></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ACSR_MaxTenure" HeaderText="Max Tenure (SIP)"
                                        DataField="ACSR_MaxTenure">
                                        <HeaderStyle></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ACSR_TenureUnit" HeaderText="Tenure Unit" DataField="ACSR_TenureUnit">
                                        <HeaderStyle></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ACSR_MinInvestmentAge" HeaderText="Min Invest Age"
                                        DataField="ACSR_MinInvestmentAge">
                                        <HeaderStyle></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ACSR_MaxInvestmentAge" HeaderText="Max Invest Age"
                                        DataField="ACSR_MaxInvestmentAge">
                                        <HeaderStyle></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ACSR_InvestmentAgeUnit" HeaderText="Invest Age Unit"
                                        DataField="ACSR_InvestmentAgeUnit">
                                        <HeaderStyle></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ACSR_TransactionType" HeaderText="Transaction Types"
                                        DataField="ACSR_TransactionType">
                                        <HeaderStyle></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ACSR_MinNumberOfApplications" HeaderText="Min No Applications"
                                        DataField="ACSR_MinNumberOfApplications">
                                        <HeaderStyle></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ACSR_BrokerageValue" HeaderText="Brokerage Value"
                                        DataField="ACSR_BrokerageValue" DataFormatString="{0:N2}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="WCU_Unit" HeaderText="Unit" DataField="WCU_Unit">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="WCCO_CalculatedOn" HeaderText="Calculated On"
                                        DataField="WCCO_CalculatedOn">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ACSM_AUMFrequency" HeaderText="AUM Frequency"
                                        DataField="ACSM_AUMFrequency">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ACSR_AUMMonth" HeaderText="AUM Month" DataField="ACSR_AUMMonth">
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn UniqueName="ACSR_Comment" HeaderText="City Group Name" DataField="ACSR_Comment">
                                    </telerik:GridBoundColumn>--%>
                                </Columns>
                                <EditFormSettings EditFormType="Template">
                                    <FormTemplate>
                                        <table cellspacing="2" cellpadding="2" width="100%">
                                            <tr>
                                                <td colspan="5" class="tdSectionHeading">
                                                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                                                        <div class="divSectionHeadingNumber fltlftStep">
                                                            3
                                                        </div>
                                                        <div class="fltlft" style="width: 200px;">
                                                            &nbsp;
                                                            <asp:Label ID="lblStage" runat="server" Text="Structure Rule Add"></asp:Label>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="leftLabel">
                                                    <asp:Label ID="lblCommissionType" runat="server" Text="Commission Type:" CssClass="FieldName"></asp:Label>
                                                </td>
                                                <td class="rightData">
                                                    <asp:DropDownList ID="ddlCommissionType" runat="server" CssClass="cmbField" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlCommissionType_Selectedindexchanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="leftLabel">
                                                    <asp:Label ID="lblInvestorType" runat="server" Text="Investor type:" CssClass="FieldName"></asp:Label>
                                                </td>
                                                <td class="rightData">
                                                    <asp:DropDownList ID="ddlInvestorType" runat="server" CssClass="cmbField">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="leftLabel">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="leftLabel">
                                                    <asp:Label ID="lblAppCityGroup" runat="server" Text="App for city group:" CssClass="FieldName"></asp:Label>
                                                </td>
                                                <td class="rightData">
                                                    <asp:DropDownList ID="ddlAppCityGroup" runat="server" CssClass="cmbField">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="leftLabel">
                                                    <asp:Label ID="lblReceivableFrequency" runat="server" Text="Receivable Fre:" CssClass="FieldName"></asp:Label>
                                                </td>
                                                <td class="rightData">
                                                    <asp:DropDownList ID="ddlReceivableFrequency" runat="server" CssClass="cmbField">
                                                    </asp:DropDownList>
                                                    <span id="Span5" class="spnRequiredField">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="leftLabel">
                                                    <asp:Label ID="lblCommissionApplicableLevel" runat="server" Text="Level:" CssClass="FieldName"></asp:Label>
                                                </td>
                                                <td class="rightData">
                                                    <asp:DropDownList ID="ddlCommissionApplicableLevel" runat="server" CssClass="cmbField">
                                                    </asp:DropDownList>
                                                    <span id="Span9" class="spnRequiredField">*</span>
                                                </td>
                                                <td class="leftLabel">
                                                    <asp:Label ID="lblApplyTaxes" runat="server" Text="Apply Taxes:" CssClass="FieldName"></asp:Label>
                                                </td>
                                                <td class="rightData">
                                                    <asp:CheckBoxList ID="chkListApplyTax" runat="server" CssClass="txtField" RepeatLayout="Flow"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="Service Tax" Value="ServiceTax"></asp:ListItem>
                                                        <asp:ListItem Text="TDS" Value="TDS"></asp:ListItem>
                                                        <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="leftLabel">
                                                    <asp:Label ID="lblMinInvestmentAmount" runat="server" Text="Min Investment Amount:"
                                                        CssClass="FieldName"></asp:Label>
                                                </td>
                                                <td class="rightData">
                                                    <asp:TextBox ID="txtMinInvestmentAmount" Text='<%# Bind( "ACSR_MinInvestmentAmount") %>'
                                                        runat="server" CssClass="txtField"></asp:TextBox>
                                                </td>
                                                <td class="leftLabel">
                                                    <asp:Label ID="lblMaxInvestmentAmount" runat="server" Text="Max Investment Amount:"
                                                        CssClass="FieldName"></asp:Label>
                                                </td>
                                                <td class="rightData">
                                                    <asp:TextBox ID="txtMaxInvestmentAmount" Text='<%# Bind( "ACSR_MaxInvestmentAmount") %>'
                                                        runat="server" CssClass="txtField"></asp:TextBox>
                                                </td>
                                                <td class="leftLabel">
                                                </td>
                                            </tr>
                                            <tr id="trMinMaxTenure" runat="server">
                                                <td class="leftLabel">
                                                    <asp:Label ID="lblMinTenure" runat="server" Text="Min Tenure:" CssClass="FieldName"></asp:Label>
                                                </td>
                                                <td class="rightData">
                                                    <asp:TextBox ID="txtMinTenure" Text='<%# Bind( "ACSR_MinTenure") %>' runat="server"
                                                        CssClass="txtField"></asp:TextBox>
                                                </td>
                                                <td class="leftLabel">
                                                    <asp:Label ID="lblMaxTenure" runat="server" Text="Max Tenure:" CssClass="FieldName"></asp:Label>
                                                </td>
                                                <td class="rightData" colspan="2">
                                                    <asp:TextBox ID="txtMaxTenure" Text='<%# Bind( "ACSR_MaxTenure") %>' runat="server"
                                                        CssClass="txtField"></asp:TextBox>
                                                    <asp:DropDownList ID="ddlTenureFrequency" runat="server" CssClass="cmbField" Style="width: 100px !Important">
                                                        <asp:ListItem Text="Day" Value="Day"></asp:ListItem>
                                                        <asp:ListItem Text="Month" Value="Month"></asp:ListItem>
                                                        <asp:ListItem Text="Installment" Value="Installment"></asp:ListItem>
                                                        <asp:ListItem Text="Year" Value="Year"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr id="trMinMaxAge" runat="server">
                                                <td class="leftLabel">
                                                    <asp:Label ID="lblMinInvestAge" runat="server" Text="Min Investment age :" CssClass="FieldName"></asp:Label>
                                                </td>
                                                <td class="rightData">
                                                    <asp:TextBox ID="txtMinInvestAge" Text='<%# Bind( "ACSR_MinInvestmentAge") %>' runat="server"
                                                        CssClass="txtField"></asp:TextBox>
                                                </td>
                                                <td class="leftLabel">
                                                    <asp:Label ID="lblMaxInvestAge" runat="server" Text="Max Investment age :" CssClass="FieldName"></asp:Label>
                                                </td>
                                                <td class="rightData" colspan="2">
                                                    <asp:TextBox ID="txtMaxInvestAge" Text='<%# Bind( "ACSR_MaxInvestmentAge") %>' runat="server"
                                                        CssClass="txtField"></asp:TextBox>
                                                    <asp:DropDownList ID="ddlInvestAgeTenure" runat="server" CssClass="cmbField" Style="width: 100px !Important">
                                                        <asp:ListItem Text="Days" Value="Days"></asp:ListItem>
                                                        <asp:ListItem Text="Years" Value="Years"></asp:ListItem>
                                                        <asp:ListItem Text="Months" Value="Months"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="trTransactionTypeSipFreq">
                                                <td class="leftLabel">
                                                    <asp:Label ID="lblTransactionType" runat="server" Text="Transaction type:" CssClass="FieldName"></asp:Label>
                                                </td>
                                                <td class="rightData">
                                                    <asp:CheckBoxList ID="chkListTtansactionType" runat="server" CssClass="txtField"
                                                        RepeatLayout="Flow" RepeatDirection="Horizontal">
                                                        <%--<asp:ListItem Text="Buy" Value="BUY"></asp:ListItem>
                                                        <asp:ListItem Text="SIP" Value="SIP"></asp:ListItem>
                                                        <asp:ListItem Text="STP Buy" Value="STPBUY"></asp:ListItem>
                                                        <asp:ListItem Text="Switch Buy" Value="SWITCHBUY"></asp:ListItem>
                                                        <asp:ListItem Text="Additional Purchase" Value="ADDPUR"></asp:ListItem>--%>
                                                    </asp:CheckBoxList>
                                                </td>
                                                <td class="leftLabel">
                                                    <asp:Label ID="lblSIPFrequency" runat="server" Text="SIP Frequency:" CssClass="FieldName"></asp:Label>
                                                </td>
                                                <td class="rightData">
                                                    <asp:DropDownList ID="ddlSIPFrequency" runat="server" CssClass="cmbField">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="leftLabel">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="leftLabel">
                                                    <asp:Label ID="lblBrokerageValue" runat="server" Text="Brokerage Value:" CssClass="FieldName"></asp:Label>
                                                </td>
                                                <td class="rightData">
                                                    <asp:TextBox ID="txtBrokerageValue" Text='<%# Bind( "ACSR_BrokerageValue") %>' runat="server"
                                                        CssClass="txtField"></asp:TextBox>
                                                    <span id="Span8" class="spnRequiredField" runat="server" visible="true">*</span>
                                                </td>
                                                <td class="leftLabel">
                                                    <asp:Label ID="lblUnit" runat="server" Text="Brokerage Unit:" CssClass="FieldName"></asp:Label>
                                                </td>
                                                <td class="rightData">
                                                    <asp:DropDownList ID="ddlBrokerageUnit" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlBrokerageUnit_OnSelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="leftLabel">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="leftLabel" runat="server" id="tdMinNumberOfApplication">
                                                    <asp:Label ID="lblMinNumberOfApplication" runat="server" Text="Min number of applications:"
                                                        CssClass="FieldName"></asp:Label>
                                                    <br />
                                                    <span id="Span4" class="spnRequiredField">&nbsp;</span>
                                                </td>
                                                <td class="rightData">
                                                    <asp:TextBox ID="txtMinNumberOfApplication" Text='<%# Bind( "ACSR_MinNumberOfApplications") %>'
                                                        runat="server" CssClass="txtField"></asp:TextBox>
                                                </td>
                                                <td class="leftLabel">
                                                    <asp:Label ID="lblCalculatedOn" runat="server" Text="Calculated On:" CssClass="FieldName"></asp:Label>
                                                </td>
                                                <td class="rightData" colspan="2">
                                                    <asp:DropDownList ID="ddlCommisionCalOn" runat="server" CssClass="cmbField">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="leftLabel">
                                                    <asp:Label ID="lblRuleNote" runat="server" Text="Comment:" CssClass="FieldName"></asp:Label>
                                                </td>
                                                <td class="rightData">
                                                    <asp:TextBox ID="txtStruRuleComment" runat="server" CssClass="txtField" Text='<%# Bind( "ACSR_Comment") %>'
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td class="rightData" colspan="2">
                                                    <asp:Button ID="btnSubmitRule" ValidationGroup="btnSubmitRule" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                        CssClass="PCGButton" runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                        CausesValidation="true"></asp:Button>&nbsp;
                                                    <asp:Button ID="Button2" CssClass="PCGButton" Text="Cancel" runat="server" CausesValidation="false"
                                                        CommandName="Cancel"></asp:Button>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <%--<asp:CustomValidator ID="CustomValidator4" runat="server" Text="At least one rule is required"
                                                        ControlToValidate="txtBrokerageValue" ClientValidationFunction="InvestmentAmountValidation"
                                                        ValidateEmptyText="true" ValidationGroup="btnSubmitRule" Display="Dynamic" SetFocusOnError="true">
                                                    </asp:CustomValidator>--%>
                                                    <asp:CustomValidator ID="CustomValidator1" runat="server" Text="Min Invest Amount should be less than Max Invest"
                                                        ControlToValidate="txtMaxInvestmentAmount" ClientValidationFunction="InvestmentAmountValidation"
                                                        ValidateEmptyText="true" ValidationGroup="btnSubmitRule" Display="Dynamic" SetFocusOnError="true">
                                                    </asp:CustomValidator>
                                                    <asp:CustomValidator ID="CustomValidator2" runat="server" Text="Min Tenure should be less than Max Tenure"
                                                        ControlToValidate="txtMaxTenure" ClientValidationFunction="TenureValidation"
                                                        ValidateEmptyText="true" ValidationGroup="btnSubmitRule" Display="Dynamic" SetFocusOnError="true">
                                                    </asp:CustomValidator>
                                                    <asp:CustomValidator ID="CustomValidator3" runat="server" Text="Min Investment Age should be less than Max Investment Age"
                                                        ControlToValidate="txtMaxInvestAge" ClientValidationFunction="InvestmentAgeValidation"
                                                        ValidateEmptyText="true" ValidationGroup="btnSubmitRule" Display="Dynamic" SetFocusOnError="true">
                                                    </asp:CustomValidator>
                                                    <asp:RequiredFieldValidator runat="server" ID="reqName" ValidationGroup="btnSubmitRule"
                                                        Display="Dynamic" ControlToValidate="txtBrokerageValue" ErrorMessage="Brokerage value is mandatory"
                                                        Text="" />
                                                    <%--  <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="You must enter valid value in the following fields:"
                                                        ShowMessageBox="false" DisplayMode="BulletList" ShowSummary="true" Display="Dynamic"
                                                        ValidationGroup="btnSubmitRule" />--%>
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Select Frequency"
                                                        CssClass="rfvPCG" ControlToValidate="ddlReceivableFrequency" ValidationGroup="btnSubmitRule"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Select ApplicableLevel"
                                                        CssClass="rfvPCG" ControlToValidate="ddlCommissionApplicableLevel" ValidationGroup="btnSubmitRule"
                                                        InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </FormTemplate>
                                </EditFormSettings>
                            </MasterTableView>
                            <ClientSettings>
                            </ClientSettings>
                        </telerik:RadGrid>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <div>
            <asp:HiddenField ID="hidCommissionStructureName" runat="server" />
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="imgexportButton" />
    </Triggers>
</asp:UpdatePanel>
