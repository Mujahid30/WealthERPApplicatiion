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
<asp:Panel ID="pnl1" runat="server" Height="1000px">
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
                        <asp:DropDownList ID="ddlIssuer" runat="server" CssClass="cmbLongField">
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
                            Visible="false" OnClick="btnMapToscheme_Click" ValidationGroup="btnStrAddUpdate" />
                        <asp:Button ID="ButtonAgentCodeMapping" CssClass="PCGMediumButton" Text="Map Agents"
                            Visible="false" runat="server" OnClick="ButtonAgentCodeMapping_Click" ValidationGroup="btnStrAddUpdate" />
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
                                         <telerik:GridBoundColumn UniqueName="ACSR_MaxNumberOfApplications" HeaderText="Max No Applications"
                                            DataField="ACSR_MaxNumberOfApplications">
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
                                                        <asp:DropDownList ID="ddlCommissionApplicableLevel" runat="server" CssClass="cmbField"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlCommissionApplicableLevel_Selectedindexchanged">
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
                                                        <asp:TextBox ID="txtTaxValue" Text='<%# Bind( "ACSR_ReducedValue") %>' runat="server"
                                                            CssClass="txtField"></asp:TextBox>
                                                        <cc1:TextBoxWatermarkExtender ID="twtxtTaxValue" TargetControlID="txtTaxValue"
                                                            WatermarkText="Enter the Value" runat="server" EnableViewState="false">
                                                        </cc1:TextBoxWatermarkExtender>
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
                                                        <span id="Span10" class="spnRequiredField" runat="server" visible="true">*</span>
                                                        <asp:RequiredFieldValidator runat="server" ID="rfvtxtMinInvestmentAmount" ValidationGroup="btnSubmitRule"
                                                            Display="Dynamic" ControlToValidate="txtMinInvestmentAmount" ErrorMessage="Min Investment Amount is mandatory"
                                                            Text="" />
                                                    </td>
                                                    <td class="leftLabel">
                                                        <asp:Label ID="lblMaxInvestmentAmount" runat="server" Text="Max Investment Amount:"
                                                            CssClass="FieldName"></asp:Label>
                                                    </td>
                                                    <td class="rightData">
                                                        <asp:TextBox ID="txtMaxInvestmentAmount" Text='<%# Bind( "ACSR_MaxInvestmentAmount") %>'
                                                            runat="server" CssClass="txtField"></asp:TextBox>
                                                        <span id="Span7" class="spnRequiredField" runat="server" visible="true">*</span>
                                                        <asp:RequiredFieldValidator runat="server" ID="rfvtxtMaxInvestmentAmount" ValidationGroup="btnSubmitRule"
                                                            Display="Dynamic" ControlToValidate="txtMaxInvestmentAmount" ErrorMessage="Max Investment Amount is mandatory"
                                                            Text="" />
                                                    </td>
                                                    <td class="leftLabel">
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
                                                    <td class="leftLabel" runat="server" id="tdlb1TransactionType">
                                                        <asp:Label ID="lblTransactionType" runat="server" Text="Transaction type:" CssClass="FieldName"></asp:Label>
                                                    </td>
                                                    <td class="rightData" runat="server" id="tdtxtTransactionType">
                                                        <asp:DropDownList Visible="true" ID="ddlTransaction" runat="server" CssClass="cmbField"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlTransaction_Selectedindexchanged">
                                                            <asp:ListItem Text="Select" Value="0">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="Systematic Transaction" Value="SIP">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="Non Systematic Transaction" Value="NonSIP">
                                                            </asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlTransaction" runat="server" ErrorMessage="Please Select Transaction type"
                                                            Enabled="false" CssClass="rfvPCG" ControlToValidate="ddlTransaction" ValidationGroup="btnStrAddUpdate"
                                                            Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator><br />
                                                    </td>
                                                    <td class="leftLabel" runat="server" id="tdlb1SipFreq">
                                                        <asp:Label ID="lblSIPFrequency" runat="server" Text="SIP Frequency:" CssClass="FieldName"></asp:Label>
                                                    </td>
                                                    <td class="rightData" runat="server" id="tdddlSipFreq">
                                                        <asp:DropDownList ID="ddlSIPFrequency" runat="server" CssClass="cmbField">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="leftLabel">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBoxList ID="chkListTtansactionType" runat="server" CssClass="txtField"
                                                            Visible="false" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                                            <%--<asp:ListItem Text="Buy" Value="BUY"></asp:ListItem>
                                                        <asp:ListItem Text="SIP" Value="SIP"></asp:ListItem>
                                                        <asp:ListItem Text="STP Buy" Value="STPBUY"></asp:ListItem>
                                                        <asp:ListItem Text="Switch Buy" Value="SWITCHBUY"></asp:ListItem>
                                                        <asp:ListItem Text="Additional Purchase" Value="ADDPUR"></asp:ListItem>--%>
                                                        </asp:CheckBoxList>
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
                                                <tr runat="server" id="trMinAndMaxNumberOfApplication">
                                                    <td class="leftLabel" runat="server" id="tdlb1MinNumberOfApplication">
                                                        <asp:Label ID="lblMinNumberOfApplication" runat="server" Text="Min number of applications:"
                                                            CssClass="FieldName"></asp:Label>
                                                        <br />
                                                        <span id="Span4" class="spnRequiredField">&nbsp;</span>
                                                    </td>
                                                    <td class="rightData" runat="server" id="tdtxtMinNumberOfApplication">
                                                        <asp:TextBox ID="txtMinNumberOfApplication" Text='<%# Bind( "ACSR_MinNumberOfApplications") %>'
                                                            runat="server" CssClass="txtField"></asp:TextBox>
                                                    </td>
                                                    <td class="leftLabel" runat="server" id="tdlb1MaxNumberOfApplication">
                                                        <asp:Label ID="lblMaxNumberOfApplication" runat="server" Text="Max number of applications:"
                                                            CssClass="FieldName"></asp:Label>
                                                        <span id="Span13" class="spnRequiredField">&nbsp;</span>
                                                    </td>
                                                    <td class="rightData" runat="server" id="tdtxtMaxNumberOfApplication">
                                                        <asp:TextBox ID="txtMaxNumberOfApplication" Text='<%# Bind( "ACSR_MaxNumberOfApplications") %>'
                                                            runat="server" CssClass="txtField"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leftLabel">
                                                        <asp:Label ID="lblCalculatedOn" runat="server" Text="Calculated On:" CssClass="FieldName"></asp:Label>
                                                    </td>
                                                    <td class="rightData">
                                                        <asp:DropDownList ID="ddlCommisionCalOn" runat="server" CssClass="cmbField">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="leftLabel">
                                                        <asp:Label ID="lblRuleNote" runat="server" Text="Comment:" CssClass="FieldName"></asp:Label>
                                                    </td>
                                                    <td class="rightData">
                                                        <asp:TextBox ID="txtStruRuleComment" runat="server" CssClass="txtField" Text='<%# Bind( "ACSR_Comment") %>'
                                                            TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td class="leftLabel">
                                                    </td>
                                                </tr>
                                                <tr>
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
            <table id="Table2" runat="server" width="100%" visible="false">
                <tr id="tr1" runat="server">
                    <td class="tdSectionHeading">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            <div class="divSectionHeadingNumber fltlftStep">
                                3
                            </div>
                            <div class="fltlft" style="width: 300px;">
                                &nbsp;
                                <asp:Label ID="Label4" runat="server" Text="Schemes Mapped To The Structures "></asp:Label>
                            </div>
                            <div class="divViewEdit" style="padding-right: 10px;">
                                <asp:ImageButton ID="ibtExportSummary" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                    Visible="false" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                    OnClick="ibtExportSummary_OnClick" OnClientClick="setFormat('excel')" Height="22px"
                                    Width="25px"></asp:ImageButton>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <table id="Table1" runat="server" width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="pnlGrid" runat="server" CssClass="Landscape" Width="100%" ScrollBars="None"
                            Visible="false">
                            <table width="75%">
                                <tr>
                                    <td>
                                        <telerik:RadGrid ID="gvMappedSchemes" AllowSorting="false" runat="server" AllowAutomaticInserts="false"
                                            AllowPaging="True" AutoGenerateColumns="False" AllowFilteringByColumn="true"
                                            enableloadondemand="true" EnableEmbeddedSkins="false" GridLines="none" ShowFooter="true"
                                            PagerStyle-AlwaysVisible="true" EnableViewState="true" ShowStatusBar="true" Skin="Telerik"
                                            OnPageSizeChanged="gvMappedSchemes_PageSizeChanged" OnNeedDataSource="gvMappedSchemes_NeedDataSource"
                                            OnItemCreated="gvMappedSchemes_OnItemCreated" OnPageIndexChanged="gvMappedSchemes_PageIndexChanged"
                                            OnUpdateCommand="gvMappedSchemes_UpdateCommand" OnDeleteCommand="gvMappedSchemes_DeleteCommand">
                                            <HeaderContextMenu EnableEmbeddedSkins="False">
                                            </HeaderContextMenu>
                                            <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="MappedSchemes"
                                                IgnorePaging="true">
                                            </ExportSettings>
                                            <PagerStyle AlwaysVisible="True" />
                                            <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" AutoGenerateColumns="false"
                                                Width="100%" DataKeyNames="ACSTSM_SetupId">
                                                <CommandItemSettings ExportToExcelText="Export to excel" />
                                                <Columns>
                                                    <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" HeaderStyle-Width="50px">
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px" Wrap="false" />
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridBoundColumn DataField="Name" HeaderStyle-Width="400px" CurrentFilterFunction="Contains"
                                                        ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Scheme Name" UniqueName="structSchemeName"
                                                        ReadOnly="true">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="400px" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridDateTimeColumn DataField="ValidFrom" ReadOnly="true" DataFormatString="{0:dd/MM/yyyy}"
                                                        HeaderStyle-Width="100px" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                                        AutoPostBackOnFilter="true" HeaderText="Valid From" SortExpression="ValidFrom"
                                                        UniqueName="schemeValidFrom">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" Wrap="false" />
                                                    </telerik:GridDateTimeColumn>
                                                    <telerik:GridDateTimeColumn DataField="ValidTill" DataFormatString="{0:dd/MM/yyyy}"
                                                        HeaderStyle-Width="100px" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                                        AutoPostBackOnFilter="true" HeaderText="Valid Till" UniqueName="schemeValidTill">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" Wrap="false" />
                                                    </telerik:GridDateTimeColumn>
                                                    <telerik:GridButtonColumn ButtonType="LinkButton" Text="Delete" ConfirmText="Do you want to delete the mapping?"
                                                        CommandName="Delete" UniqueName="DeleteCommandColumn" HeaderStyle-Width="50px">
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px" Wrap="false" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                                <PagerStyle AlwaysVisible="True" />
                                            </MasterTableView>
                                            <ClientSettings>
                                                <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                                <Resizing AllowColumnResize="true" />
                                            </ClientSettings>
                                            <%--<FilterMenu EnableEmbeddedSkins="False"></FilterMenu>--%>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlAddSchemesButton" runat="server" Visible="false">
                <table width="33%">
                    <tr>
                        <td class="leftLabel">
                            <asp:Label ID="lblAddNewSchemes" runat="server" CssClass="FieldName" Text="Add Schemes"></asp:Label>
                        </td>
                        <td class="rightData">
                            <asp:Button ID="btnAddNewSchemes" runat="server" Text="New Schemes" OnClick="btnAddNewSchemes_Click"
                                CssClass="PCGButton wide-button" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlAddSchemes" runat="server" Visible="false">
                <table width="100%">
                    <tr>
                        <td class="leftLabel">
                            <asp:Label ID="lblPeriodStart" runat="server" CssClass="FieldName" Text="Available Between: "></asp:Label>
                        </td>
                        <td class="rightData">
                            <telerik:RadDatePicker ID="rdpPeriodStart" runat="server">
                            </telerik:RadDatePicker>
                        </td>
                        <td class="leftLabel">
                            <telerik:RadDatePicker ID="rdpPeriodEnd" runat="server">
                            </telerik:RadDatePicker>
                        </td>
                        <td class="rightData">
                            <asp:Button ID="btn_GetAvailableSchemes" runat="server" Text="Schemes" CssClass="PCGButton"
                                OnClick="btn_GetAvailableSchemes_Click" />
                        </td>
                        <td colspan="2">
                            <asp:RequiredFieldValidator ID="rfvPeriodStart" runat="server" CssClass="rfvPCG"
                                ErrorMessage="Please enter valid date(s)" ControlToValidate="rdpPeriodStart"
                                ValidationGroup="availSchemesPeriod" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvPeriodEnd" runat="server" CssClass="rfvPCG" ErrorMessage="Please enter valid date(s)"
                                ControlToValidate="rdpPeriodEnd" ValidationGroup="availSchemesPeriod" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvPeriodEnd" runat="server" CssClass="rfvPCG" ControlToCompare="rdpPeriodStart"
                                ErrorMessage="Please enter valid date(s)" ControlToValidate="rdpPeriodEnd" Display="Dynamic"
                                Operator="GreaterThan" SetFocusOnError="True" Type="Date" ValidationGroup="availSchemesPeriod"></asp:CompareValidator>
                            <asp:Label ID="lblMapError" runat="server" CssClass="rfvPCG" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td class="rightData">
                            <asp:Label ID="lblAvailableSchemes" runat="server" Text="Available Schemes" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightData">
                            <asp:Label ID="lblMappedSchemes" runat="server" Text="Mapped Schemes" CssClass="FieldName"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td rowspan="2" class="rightData">
                            <telerik:RadListBox SelectionMode="Multiple" EnableDragAndDrop="true" AllowTransferOnDoubleClick="true"
                                AllowTransferDuplicates="false" EnableViewState="true" EnableMarkMatches="true"
                                runat="server" ID="rlbAvailSchemes" Height="200px" Width="250px" AllowTransfer="true"
                                TransferToID="rlbMappedSchemes" CssClass="cmbField">
                                <ButtonSettings TransferButtons="All" />
                            </telerik:RadListBox>
                        </td>
                        <td rowspan="2" class="leftLabel">
                            <telerik:RadListBox runat="server" AutoPostBackOnTransfer="true" SelectionMode="Multiple"
                                ID="rlbMappedSchemes" Height="200px" Width="220px" CssClass="cmbField">
                            </telerik:RadListBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="leftLabel">
                            <asp:Label ID="lblMappedFrom" runat="server" CssClass="FieldName" Text="Mapping Period: "></asp:Label>
                        </td>
                        <td class="rightData">
                            <telerik:RadDatePicker ID="rdpMappedFrom" runat="server">
                            </telerik:RadDatePicker>
                            <td class="leftLabel">
                                <telerik:RadDatePicker ID="rdpMappedTill" runat="server">
                                </telerik:RadDatePicker>
                                <td class="rightData">
                                    <asp:Button ID="btnMapSchemes" CssClass="PCGButton" runat="server" Text="Map" OnClick="btnMapSchemes_Click" />
                                </td>
                        <td colspan="2">
                            <asp:RequiredFieldValidator ID="rfvMappingTo" runat="server" ErrorMessage="Please enter valid date(s)"
                                Display="Dynamic" CssClass="rfvPCG" ValidationGroup="mappingPeriod" ControlToValidate="rdpMappedTill"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvMappingFrom" runat="server" ErrorMessage="Please enter valid date(s)"
                                Display="Dynamic" CssClass="rfvPCG" ValidationGroup="mappingPeriod" ControlToValidate="rdpMappedFrom"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cmvMappingPeriod" runat="server" ErrorMessage="Please enter valid date(s)"
                                CssClass="rfvPCG" Display="Dynamic" ControlToCompare="rdpMappedFrom" ControlToValidate="rdpMappedTill"
                                Operator="GreaterThan"></asp:CompareValidator>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table id="Table3" runat="server" width="100%" visible="false">
                <tr id="tr2" runat="server">
                    <td class="tdSectionHeading">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            <div class="divSectionHeadingNumber fltlftStep">
                                3
                            </div>
                            <div class="fltlft" style="width: 400px;">
                                &nbsp;
                                <asp:Label ID="Label8" runat="server" Text="Payable Structure To Agent Category Mapping "></asp:Label>
                            </div>
                            <div class="divViewEdit" style="padding-right: 10px;">
                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                    Visible="false" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                    OnClick="ibtExportSummary_OnClick" OnClientClick="setFormat('excel')" Height="22px"
                                    Width="25px"></asp:ImageButton>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <table id="tblMapping" runat="server" width="100%" visible="false">
                <tr>
                    <td class="leftLabel">
                        <asp:Label ID="Label5" runat="server" Text="Mapping For:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightData">
                        <asp:DropDownList ID="ddlMapping" runat="server" CssClass="cmbField" AutoPostBack="true">
                            <asp:ListItem Text="Staff" Value="Staff"></asp:ListItem>
                            <asp:ListItem Text="Associate" Value="Associate"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="leftLabel">
                        <asp:Label ID="Label6" runat="server" Text="Type: " CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightData">
                        <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlType_Selectedindexchanged"
                            AutoPostBack="true">
                            <asp:ListItem Text="Custom" Value="Custom"></asp:ListItem>
                            <asp:ListItem Text="UserCategory" Value="UserCategory"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <%--  <td class="leftLabel">
            <asp:Label ID="lblAssetCategory" CssClass="FieldName" runat="server" Text="Asset Category:"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlAdviserCategory" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>--%>
                </tr>
                <tr>
                    <td class="leftLabel">
                        <asp:Label ID="lblAssetCategory" CssClass="FieldName" runat="server" Text="Associate Category:"></asp:Label>
                    </td>
                    <td class="rightData">
                        <asp:DropDownList ID="ddlAdviserCategory" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table>
                <tr runat="server" id="trListControls" visible="false">
                    <td>
                        <div class="clearfix" style="margin-bottom: 1em;">
                            <asp:Panel ID="PLCustomer" runat="server" Style="float: left; padding-left: 150px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblSelectBranch" runat="server" CssClass="FieldName" Text="Existing AgentCodes">
                                </asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="Mapped AgentCodes">
                                </asp:Label>
                                <br />
                                <telerik:RadListBox SelectionMode="Multiple" EnableDragAndDrop="true" AccessKey="y"
                                    AllowTransferOnDoubleClick="true" AllowTransferDuplicates="false" EnableViewState="true"
                                    EnableMarkMatches="true" runat="server" ID="LBAgentCodes" Height="200px" Width="250px"
                                    AllowTransfer="true" TransferToID="RadListBoxSelectedAgentCodes" CssClass="cmbFielde"
                                    Visible="true">
                                </telerik:RadListBox>
                                <telerik:RadListBox runat="server" AutoPostBackOnTransfer="true" SelectionMode="Multiple"
                                    ID="RadListBoxSelectedAgentCodes" Height="200px" Width="220px" CssClass="cmbField">
                                </telerik:RadListBox>
                            </asp:Panel>
                        </div>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td class="rightData" colspan="2">
                        <asp:Button ID="btnPaybleMapping" ValidationGroup="btnSubmitRule" CssClass="PCGButton"
                            OnClick="btnPaybleMapping_Click" Visible="false" Text="Submit" runat="server"
                            CausesValidation="true"></asp:Button>&nbsp;
                        <%--  <asp:Button ID="Button2" CssClass="PCGButton" Text="Cancel" runat="server" CausesValidation="false"
                                                        CommandName="Cancel"></asp:Button>--%>
                    </td>
                </tr>
            </table>
            <table id="Table4" runat="server" width="100%" visible="false">
                <tr id="tr3" runat="server">
                    <td class="tdSectionHeading">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            <div class="divSectionHeadingNumber fltlftStep">
                                3
                            </div>
                            <div class="fltlft" style="width: 400px;">
                                &nbsp;
                                <asp:Label ID="Label11" runat="server" Text="Commision Management Structure To Issue Mapping "></asp:Label>
                            </div>
                            <div class="divViewEdit" style="padding-right: 10px;">
                                <asp:ImageButton ID="ImageButton2" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                    Visible="false" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                    OnClick="ibtExportSummary_OnClick" OnClientClick="setFormat('excel')" Height="22px"
                                    Width="25px"></asp:ImageButton>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <table runat="server" id="tbNcdIssueList" visible="false">
                <tr>
                    <td align="right">
                        <asp:Label ID="Label9" runat="server" Text="Issue type:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlIssueType" runat="server" CssClass="cmbField" AutoPostBack="true"
                            Width="205px" OnSelectedIndexChanged="ddlIssueType_Selectedindexchanged">
                            <asp:ListItem Value="OpenIssue">Open Issue</asp:ListItem>
                            <asp:ListItem Value="ClosedIssue">Closed Issue</asp:ListItem>
                            <asp:ListItem Value="FutureIssue">Future Issue</asp:ListItem>
                        </asp:DropDownList>
                        <span id="Span11" class="spnRequiredField">*</span>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Select Issue Type"
                            CssClass="rfvPCG" ControlToValidate="ddlIssueType" ValidationGroup="btnGo" Display="Dynamic"
                            InitialValue="Select"></asp:RequiredFieldValidator>
                    </td>
                    <td align="right">
                        &nbsp;&nbsp;
                        <asp:Label ID="Label10" runat="server" Text="Unmmaped Issues:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlUnMappedIssues" runat="server" CssClass="cmbField" AutoPostBack="true"
                            Width="205px">
                        </asp:DropDownList>
                        <span id="Span12" class="spnRequiredField">*</span>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Select Issue Type"
                            CssClass="rfvPCG" ControlToValidate="ddlUnMappedIssues" ValidationGroup="btnGo"
                            Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr id="trBtnSubmit" runat="server">
                    <td>
                        <asp:Button ID="btnMAP" runat="server" Text="Map" CssClass="PCGButton" ValidationGroup="btnGo"
                            OnClick="btnMAP_Click" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlIssueList" Visible="false" runat="server" class="Landscape" Width="80%"
                Height="80%" ScrollBars="Both">
                <table width="100%">
                    <tr>
                        <td>
                            <div id="dvIssueList" runat="server" style="width: auto;">
                                <telerik:RadGrid ID="gvMappedIssueList" runat="server" GridLines="None" AutoGenerateColumns="False"
                                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                    Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true"
                                    AllowAutomaticInserts="false" ExportSettings-FileName="Issue List" OnNeedDataSource="gvMappedIssueList_OnNeedDataSource"
                                    OnItemCommand="gvMappedIssueList_ItemCommand">
                                    <%-- <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                            FileName="MF Order Recon" Excel-Format="ExcelML">
                        </ExportSettings>--%>
                                    <MasterTableView Width="100%" AllowMultiColumnSorting="True" DataKeyNames="ACSTSM_SetupId"
                                        AutoGenerateColumns="false" CommandItemDisplay="None">
                                        <Columns>
                                            <telerik:GridButtonColumn CommandName="Delete" Text="Delete" ConfirmText="Do you want to delete this rule? Click OK to proceed"
                                                UniqueName="column">
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn DataField="AIM_IssueName" HeaderText="Issue Name" SortExpression="AIM_IssueName"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                UniqueName="AIM_IssueName" FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ValidityFrom" HeaderText="Validity From" SortExpression="ValidityFrom"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                DataFormatString="{0:d}" UniqueName="ValidityFrom" FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ValidityTo" HeaderText="Validity To" SortExpression="ValidityTo"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                DataFormatString="{0:d}" UniqueName="ValidityTo" FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div>
                <asp:HiddenField ID="hidCommissionStructureName" runat="server" />
                <asp:HiddenField ID="hdnProductId" runat="server" />
                <asp:HiddenField ID="hdnStructValidFrom" runat="server" />
                <asp:HiddenField ID="hdnStructValidTill" runat="server" />
                <asp:HiddenField ID="hdnIssuerId" runat="server" />
                <asp:HiddenField ID="hdnCategoryId" runat="server" />
                <asp:HiddenField ID="hdnSubcategoryIds" runat="server" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgexportButton" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Panel>
