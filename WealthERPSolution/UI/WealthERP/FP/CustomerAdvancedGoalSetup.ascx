<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerAdvancedGoalSetup.ascx.cs"
    Inherits="WealthERP.FP.CustomerAdvancedGoalSetup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript">

    function MFBasedGoalSelection(value) {

        if (value == 'rdoMFBasedGoalYes') {
            document.getElementById("<%= trExistingInvestmentAllocated.ClientID %>").style.display = 'none';
            document.getElementById("<%= trReturnOnExistingInvestmentAll.ClientID %>").style.display = 'none';

        }

    }


    function checkDate(sender, args) {

        var selectedDate = new Date();
        selectedDate = sender._selectedDate;

        var todayDate = new Date();
        var msg = "";

        if (selectedDate > todayDate) {
            sender._selectedDate = todayDate;
            sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            alert("Warning! - Date Cannot be in the future");
        }
    };

  
</script>

<asp:HiddenField ID="previousTabHidden" runat="Server" />

<script type="text/javascript">
    function OnSelecting(sender, args) {
        $get("<%= previousTabHidden.ClientID%>").value = sender.get_selectedTab().get_text();
    }
</script>

<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
<telerik:RadTabStrip ID="RadTabStripFPGoalDetails" runat="server" EnableTheming="True"
    Skin="Telerik" EnableEmbeddedSkins="False" MultiPageID="CustomerFPGoalDetail"
    OnClientTabSelecting="OnSelecting">
    <Tabs>
        <telerik:RadTab runat="server" Text="Goal Add/View" Value="GoalAdd" TabIndex="0">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Goal Progress" Value="Progress" TabIndex="1">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Goal Funding" Value="Funding" TabIndex="2">
            <Tabs>
                <telerik:RadTab runat="server" Text="Mutual Fund" Value="MF" TabIndex="3">
                </telerik:RadTab>
                <telerik:RadTab runat="server" Text="Equity" Value="EQ" TabIndex="4">
                </telerik:RadTab>
                <telerik:RadTab runat="server" Text="Fixed Income" Value="FI" TabIndex="5" Visible="false">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Model Portfolio" Value="Model" TabIndex="6">
        </telerik:RadTab>
    </Tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="CustomerFPGoalDetail" EnableViewState="true" runat="server">
    <telerik:RadPageView ID="RadPageView1" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="headertitle" runat="server" CssClass="HeaderTextBig" Text="Goal Setup"></asp:Label>
                            <hr />
                        </td>
                    </tr>
                    <tr id="trSumbitSuccess" runat="server" visible="false">
                        <td align="center">
                            <div id="msgRecordStatus" class="success-msg" align="center">
                                Record saved Successfully
                            </div>
                        </td>
                    </tr>
                    <tr id="trUpdateSuccess" runat="server" visible="false">
                        <td align="center">
                            <div id="Div1" class="success-msg" align="center">
                                Record Updated Successfully
                            </div>
                        </td>
                    </tr>
                </table>
                <table class="TableBackground">
                    <tr>
                        <td class="leftField">
                            <asp:Label ID="lblGoalbjective" runat="server" CssClass="FieldName" Text="Pick Goal Objective :"></asp:Label>
                        </td>
                        <td class="rightField">
                            <%-- <asp:DropDownList ID="ddlGoalType" runat="server" AutoPostBack="True" CssClass="cmbField"
                              OnSelectedIndexChanged="ddlGoalType_SelectedIndexChanged">
                        </asp:DropDownList>--%>
                            <telerik:RadComboBox ID="ddlGoalType" OnSelectedIndexChanged="ddlGoalType_OnSelectedIndexChange"
                                CssClass="cmbField" runat="server" Width="150px" EnableEmbeddedSkins="false"
                                Skin="Telerik" AllowCustomText="true" AutoPostBack="true" CausesValidation="false"
                                ValidationGroup="btnSave" onchange="Page_BlockSubmit = false;">
                                <Items>
                                    <telerik:RadComboBoxItem ImageUrl="~/Images/Select.png" Text="Select" Value="0" Selected="true">
                                    </telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem ImageUrl="~/Images/HomeGoal.png" Text="Buy Home" Value="BH"
                                        runat="server"></telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem ImageUrl="~/Images/EducationGoal.png" Text="Children Education"
                                        Value="ED" runat="server"></telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem ImageUrl="~/Images/ChildMarraiageGoal.png" Text="Children Marriage"
                                        Value="MR" runat="server"></telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem ImageUrl="~/Images/OtherGoal.png" Text="Other" Value="OT"
                                        runat="server"></telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem ImageUrl="~/Images/RetirementGoal.png" Text="Retirement"
                                        Value="RT" runat="server"></telerik:RadComboBoxItem>
                                </Items>
                            </telerik:RadComboBox>
                            <span id="spanGoalTypeGoalAdd" class="spnRequiredField" runat="server">*</span>
                            <asp:RequiredFieldValidator ID="reqValQuestionType" runat="server" CssClass="rfvPCG"
                                ErrorMessage="Select Goal Type" Text="Select Goal Type" Display="Dynamic" ValidationGroup="btnSave"
                                ControlToValidate="ddlGoalType" InitialValue="Select">
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" CssClass="rfvPCG"
                                ErrorMessage="Select Goal Type" Text="Select Goal Type" Display="Dynamic" ValidationGroup="NoofYears"
                                ControlToValidate="ddlGoalType" InitialValue="Select">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="leftField" id="tdCustomerAge1" runat="server">
                            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Customer Age :"></asp:Label>
                        </td>
                        <td class="rightField" id="tdCustomerAge2" runat="server">
                            <asp:TextBox ID="txtCustomerAge" runat="server" AutoCompleteType="Disabled" CssClass="txtField"></asp:TextBox>
                            <span id="Span8" class="spnRequiredField" runat="server" visible="false">*</span>
                            <asp:RangeValidator ID="RangeValidator12" Display="Dynamic" SetFocusOnError="True"
                                CssClass="rfvPCG" Type="Double" ErrorMessage="Value should be between 18 to 150"
                                MinimumValue="18" MaximumValue="150" ControlToValidate="txtCustomerAge" runat="server"></asp:RangeValidator>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator16" runat="server"
                                ControlToValidate="txtCustomerAge" CssClass="rfvPCG" ValidationGroup="btnSave"
                                ErrorMessage="Please enter some value"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftField">
                            <asp:Label ID="lblGoalDate" runat="server" CssClass="FieldName" Text="Goal Entry Date :"></asp:Label>
                        </td>
                        <td class="rightField">
                            <asp:TextBox ID="txtGoalDate" runat="server" AutoCompleteType="Disabled" CssClass="txtField"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtGoalDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                OnClientDateSelectionChanged="checkDate" TargetControlID="txtGoalDate" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                            <span id="SpanGoalDateReq" class="spnRequiredField" runat="server">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtGoalDate"
                                CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please select a Date"></asp:RequiredFieldValidator>
                        </td>
                        <td class="leftField" id="tdSpouseAge1" runat="server">
                            <asp:Label ID="lblSpouseAge" runat="server" CssClass="FieldName" Text="Spouse Age :"></asp:Label>
                        </td>
                        <td class="rightField" id="tdSpouseAge2" runat="server">
                            <asp:TextBox ID="txtSpouseAge" runat="server" AutoCompleteType="Disabled" CssClass="txtField"></asp:TextBox>
                            <span id="Span7" class="spnRequiredField" runat="server" visible="false">*</span>
                            <asp:RangeValidator ID="RangeValidator11" Display="Dynamic" SetFocusOnError="True"
                                Type="Double" ErrorMessage="Value should be between 18 to 150" MinimumValue="0"
                                CssClass="rfvPCG" MaximumValue="150" ControlToValidate="txtSpouseAge" runat="server"></asp:RangeValidator>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator15" runat="server"
                                ControlToValidate="txtSpouseAge" CssClass="rfvPCG" ValidationGroup="btnSave"
                                ErrorMessage="Please enter some value"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="trGoalDesc" runat="server">
                        <td id="Td2333" class="leftField" runat="server">
                            <asp:Label ID="lblGoalDescription" runat="server" CssClass="FieldName" Text="Goal Description :"></asp:Label>
                        </td>
                        <td id="Td333444" class="rightField" runat="server">
                            <asp:TextBox ID="txtGoalDescription" runat="server" MaxLength="100" AutoCompleteType="Disabled"
                                CssClass="txtField"></asp:TextBox>
                        </td>
                        <td class="leftField" id="tdRetirementAge1" runat="server">
                            <asp:Label ID="Label5" runat="server" CssClass="FieldName" Text="Retirement Age :"></asp:Label>
                        </td>
                        <td class="rightField" id="tdRetirementAge2" runat="server">
                            <asp:TextBox ID="txtRetirementAge" runat="server" AutoCompleteType="Disabled" CssClass="txtField"></asp:TextBox>
                            <span id="Span6" class="spnRequiredField" runat="server" visible="false">*</span>
                            <asp:RangeValidator ID="RangeValidator10" Display="Dynamic" SetFocusOnError="True"
                                Type="Double" ErrorMessage="Value should be between 30 to 65" MinimumValue="30"
                                CssClass="rfvPCG" MaximumValue="65" ControlToValidate="txtRetirementAge" runat="server"></asp:RangeValidator>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator14" runat="server"
                                ControlToValidate="txtRetirementAge" CssClass="rfvPCG" ValidationGroup="btnSave"
                                ErrorMessage="Please enter some value"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="trPickChild" runat="server">
                        <td id="Td4" class="leftField" runat="server">
                            <asp:Label ID="lblPickChild" runat="server" CssClass="FieldName" Text="Select a child for Goal planning :"></asp:Label>
                        </td>
                        <td id="Td5" class="rightField" runat="server">
                            <asp:DropDownList ID="ddlPickChild" runat="server" CssClass="cmbField">
                            </asp:DropDownList>
                        </td>
                        <td id="tdPickChildBlank" runat="server" colspan="2">
                            &nbsp;&nbsp;&nbsp
                        </td>
                    </tr>
                    <tr>
                        <td class="leftField">
                            <asp:Label ID="lblGoalType" runat="server" Text="Goal Type:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField">
                            <asp:DropDownList ID="ddlGoalTypes" runat="server" AutoPostBack="true" CssClass="cmbField"
                                OnSelectedIndexChanged="ddlGoalTypes_OnSelectedIndexChange">
                                <asp:ListItem Text="Normal" Value="NG"></asp:ListItem>
                                <asp:ListItem Text="Recurring" Value="RG"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftField">
                            <asp:Label ID="lblGoalCostToday" runat="server" CssClass="FieldName" Text="Goal Cost Today :"></asp:Label>
                        </td>
                        <td id="Td1" class="rightField" runat="server">
                            <asp:TextBox ID="txtGoalCostToday" runat="server" CssClass="txtField"></asp:TextBox>
                            <span id="SpanGoalCostTodayReq" class="spnRequiredField" runat="server">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtGoalCostToday"
                                ValidationGroup="btnSave" CssClass="rfvPCG" ErrorMessage="Goal cost Today Required"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="txtGoalCostToday_E" runat="server" Enabled="True"
                                TargetControlID="txtGoalCostToday" FilterType="Custom, Numbers" ValidChars=".">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RangeValidator ID="RVtxtGoalCostToday" Display="Dynamic" CssClass="rfvPCG" SetFocusOnError="True"
                                Type="Double" ErrorMessage="Value  should not be more than 15 digit & can't be zero"
                                ValidationGroup="btnSave" MinimumValue="0.00000000001" MaximumValue="999999999999999"
                                ControlToValidate="txtGoalCostToday" runat="server"></asp:RangeValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtGoalCostToday"
                                ValidationGroup="NoofYears" CssClass="rfvPCG" ErrorMessage="Goal cost Today Required"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td class="leftField" id="tdCustomerEOL1" runat="server">
                            <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Customer EOL :"></asp:Label>
                        </td>
                        <td class="rightField" id="tdCustomerEOL2" runat="server">
                            <asp:TextBox ID="txtCustomerEOL" runat="server" AutoCompleteType="Disabled" CssClass="txtField"></asp:TextBox>
                            <span id="Span5" class="spnRequiredField" runat="server" visible="false">*</span>
                            <asp:RangeValidator ID="RangeValidator9" Display="Dynamic" SetFocusOnError="True"
                                Type="Double" ErrorMessage="Value should be between 30 to 150" MinimumValue="30"
                                CssClass="rfvPCG" MaximumValue="150" ControlToValidate="txtCustomerEOL" runat="server"></asp:RangeValidator>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator13" runat="server"
                                ControlToValidate="txtCustomerEOL" CssClass="rfvPCG" ValidationGroup="btnSave"
                                ErrorMessage="Please enter some value"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftField">
                            <asp:Label ID="lblGoalYear" runat="server" CssClass="FieldName" Text="Goal Year :"></asp:Label>
                        </td>
                        <td class="rightField">
                            <asp:DropDownList ID="ddlGoalYear" runat="server" CssClass="cmbField" CausesValidation="True">
                            </asp:DropDownList>
                            <span id="SpanGoalYearReq" class="spnRequiredField" runat="server">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlGoalYear"
                                CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please Select Goal Year"
                                InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                        <td class="leftField" id="tdSpouseEOL1" runat="server">
                            <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="Spouse EOL :"></asp:Label>
                        </td>
                        <td class="rightField" id="tdSpouseEOL2" runat="server">
                            <asp:TextBox ID="txtSpouseEOL" runat="server" Text="80" CssClass="txtField"></asp:TextBox>
                            <span id="Span4" class="spnRequiredField" runat="server" visible="false">*</span>
                            <asp:RangeValidator ID="RangeValidator8" Display="Dynamic" SetFocusOnError="True"
                                Type="Double" ErrorMessage="Value should be between 30 to 150" MinimumValue="0"
                                CssClass="rfvPCG" MaximumValue="150" ControlToValidate="txtSpouseEOL" runat="server"></asp:RangeValidator>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator12" runat="server"
                                ControlToValidate="txtSpouseEOL" CssClass="rfvPCG" ValidationGroup="btnSave"
                                ErrorMessage="Please enter some value"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="trNoofYears" runat="server" visible="false">
                        <td class="leftField">
                            <asp:Label ID="lblNoofYear" runat="server" CssClass="FieldName" Text="No. of Periods:"></asp:Label>
                        </td>
                        <td id="tdtxtNoofYears" runat="server">
                            <asp:TextBox ID="txtNoofYears" runat="server"> </asp:TextBox>
                            <asp:Button ID="btnRecuring" runat="server" OnClick="btnRecuring_OnClick" Text="Go"
                                ValidationGroup="NoofYears" />
                            <span id="Span3" class="spnRequiredField" runat="server" visible="false">*</span>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator19" runat="server"
                                ControlToValidate="txtNoofYears" CssClass="rfvPCG" ValidationGroup="NoofYears"
                                ErrorMessage="Please enter year"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="trRadRecurring" runat="server" visible="false">
                        <td>
                        </td>
                        <td class="rightField" colspan="3">
                            <telerik:RadGrid ID="RadRecurring" runat="server" GridLines="None" AllowPaging="True"
                                PageSize="5" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                                AllowAutomaticDeletes="false" AllowAutomaticInserts="false" AllowAutomaticUpdates="false"
                                HorizontalAlign="NotSet" OnNeedDataSource="RadRecurring_OnNeedDataSource" ShowFooter="true">
                                <MasterTableView DataKeyNames="" Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                    CommandItemDisplay="None">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Year" UniqueName="Year" HeaderText="Year" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px" SortExpression="Year"
                                            FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Costperannum" UniqueName="Costperannum" HeaderText="Cost per annum"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px"
                                            SortExpression="Costperannum" FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Inflation" UniqueName="Inflation" HeaderText="Inflation"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px"
                                            SortExpression="Inflation" FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Yrs" UniqueName="Yrs" HeaderText="Yrs" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px" SortExpression="Yrs"
                                            FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FutureValue" UniqueName="FutureValue" HeaderText="Future Value"
                                            DataFormatString="{0:F2}" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            AllowFiltering="true" HeaderStyle-Width="90px" SortExpression="FutureValue" FilterControlWidth="70px"
                                            CurrentFilterFunction="Contains" Aggregate="Sum" FooterText="Total:" DataType="System.Decimal">
                                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Returnoninvestment" UniqueName="Returnoninvestment"
                                            HeaderText="Return on investment" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            AllowFiltering="true" HeaderStyle-Width="90px" SortExpression="FutureValue" FilterControlWidth="70px"
                                            CurrentFilterFunction="Contains">
                                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PresentValue" UniqueName="PresentValue" HeaderText="FV as on start of Goal Date"
                                            DataFormatString="{0:F2}" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            AllowFiltering="true" HeaderStyle-Width="90px" SortExpression="PresentValue" FilterControlWidth="70px"
                                            CurrentFilterFunction="Contains" Aggregate="Sum" FooterText="Total:" DataType="System.Decimal">
                                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftField">
                            <asp:Label ID="lblUseMFBasedGoal" runat="server" CssClass="FieldName" Text="Use Assets to Fund the Goal:"></asp:Label>
                        </td>
                        <td class="rightField">
                            <asp:RadioButton ID="rdoMFBasedGoalYes" Text="Yes" runat="server" GroupName="YesNo"
                                Class="cmbFielde" OnCheckedChanged="rdoMFBasedGoalYes_CheckedChanged" AutoPostBack="True" />
                            <asp:RadioButton ID="rdoMFBasedGoalNo" Text="No" runat="server" GroupName="YesNo"
                                Class="cmbFielde" Checked="True" OnCheckedChanged="rdoMFBasedGoalNo_CheckedChanged"
                                AutoPostBack="True" />
                        </td>
                        <td class="leftField" id="tdPostRetirementReturns1" runat="server">
                            <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="Post Retirement Returns(%) :"></asp:Label>
                        </td>
                        <td class="rightField" id="tdPostRetirementReturns2" runat="server">
                            <asp:TextBox ID="txtPostRetirementReturns" runat="server" AutoCompleteType="Disabled"
                                CssClass="txtField"></asp:TextBox>
                            <span id="Span1" class="spnRequiredField" runat="server" visible="false">*</span>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                Enabled="True" TargetControlID="txtPostRetirementReturns" FilterType="Custom, Numbers"
                                ValidChars=".">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RangeValidator ID="RangeValidator5" Display="Dynamic" CssClass="rfvPCG" SetFocusOnError="True"
                                Type="Double" ErrorMessage="Value  should be in between 0 and 100" MinimumValue="0.000000001"
                                MaximumValue="100" ControlToValidate="txtPostRetirementReturns" runat="server"></asp:RangeValidator>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator9" runat="server"
                                ControlToValidate="txtPostRetirementReturns" CssClass="rfvPCG" ValidationGroup="btnSave"
                                ErrorMessage="Please enter some % value"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="trExistingInvestmentAllocated" runat="server">
                        <td class="leftField">
                            <asp:Label ID="lblCurrentInvestPurpose" runat="server" CssClass="FieldName" Text="Existing Investment Allocated :"></asp:Label>
                        </td>
                        <td class="rightField">
                            <asp:TextBox ID="txtCurrentInvestPurpose" runat="server" AutoCompleteType="Disabled"
                                CssClass="txtField" MaxLength="15" OnBlur="SetROI();"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="txtCurrentInvestPurpose_E" runat="server"
                                Enabled="True" TargetControlID="txtCurrentInvestPurpose" FilterType="Custom, Numbers"
                                ValidChars=".">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <span id="SpanCurrInvestmentAllocated" class="spnRequiredField" runat="server">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCurrentInvestPurpose"
                                CssClass="txtField" ValidationGroup="btnSave" ErrorMessage="Please enter some amount"></asp:RequiredFieldValidator>
                        </td>
                        <%-- <td id="tdExistingInvestBlank" runat="server" colspan="2">
                            &nbsp;&nbsp;&nbsp
                        </td>--%>
                        <td class="leftField" id="tdPostRetirementInflation1" runat="server">
                            <asp:Label ID="lblPostRetirementInflation" runat="server" CssClass="FieldName" Text="Post Retirement Inflation(%) :"></asp:Label>
                        </td>
                        <td class="rightField" id="tdPostRetirementInflation2" runat="server">
                            <asp:TextBox ID="txtPostRetirementInflation" runat="server" AutoCompleteType="Disabled"
                                CssClass="txtField"></asp:TextBox>
                            <span id="Span2" class="spnRequiredField" runat="server" visible="false">*</span>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                Enabled="True" TargetControlID="txtPostRetirementInflation" FilterType="Custom, Numbers"
                                ValidChars=".">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RangeValidator ID="RangeValidator2" Display="Dynamic" CssClass="rfvPCG" SetFocusOnError="True"
                                Type="Double" ErrorMessage="Value  should be in between 0 and 100" MinimumValue="0.000000001"
                                MaximumValue="100" ControlToValidate="txtPostRetirementInflation" runat="server"></asp:RangeValidator>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator6" runat="server"
                                ControlToValidate="txtPostRetirementInflation" CssClass="rfvPCG" ValidationGroup="btnSave"
                                ErrorMessage="Please enter some % value"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="trReturnOnExistingInvestmentAll" runat="server">
                        <td class="leftField">
                            <asp:Label ID="lblRateOfInterstAbove" runat="server" CssClass="FieldName" Text="Expected return on the allocated investment(%) :"></asp:Label>
                        </td>
                        <td class="rightField">
                            <asp:TextBox ID="txtAboveRateOfInterst" runat="server" AutoCompleteType="Disabled"
                                CssClass="txtField" MaxLength="15"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="txtAboveRateOfInterst_E" runat="server"
                                Enabled="True" TargetControlID="txtAboveRateOfInterst" FilterType="Custom, Numbers"
                                ValidChars=".">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <span id="SpanReturnOnExistingInvestment" class="spnRequiredField" runat="server">*</span>
                            <asp:RangeValidator ID="RangeValidator1" Display="Dynamic" SetFocusOnError="True"
                                Type="Double" ErrorMessage="Value  should not be more than 100" MinimumValue="0"
                                MaximumValue="100" ControlToValidate="txtAboveRateOfInterst" runat="server"></asp:RangeValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server"
                                ControlToValidate="txtAboveRateOfInterst" CssClass="rfvPCG" ValidationGroup="btnSave"
                                ErrorMessage="Please enter some % value"></asp:RequiredFieldValidator>
                        </td>
                        <td id="tdReturnOnExistingInvestBlank" runat="server" colspan="2">
                            &nbsp;&nbsp;&nbsp
                        </td>
                    </tr>
                    <tr id="trReturnOnFutureInvest" runat="server">
                        <td class="leftField">
                            <nobr>  <asp:Label ID="ExpRateOfReturn" runat="server" CssClass="FieldName" Text="Expected return on the future investment(%) :"></asp:Label></nobr>
                        </td>
                        <td class="rightField">
                            <asp:TextBox ID="txtExpRateOfReturn" runat="server" AutoCompleteType="Disabled" CssClass="txtField"
                                MaxLength="15"></asp:TextBox>
                            <span id="SpanExpROI" class="spnRequiredField" runat="server">*</span>
                            <ajaxToolkit:FilteredTextBoxExtender ID="txtExpRateOfReturn_E" runat="server" Enabled="True"
                                TargetControlID="txtExpRateOfReturn" FilterType="Custom, Numbers" ValidChars=".">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RangeValidator Display="Dynamic" ID="RangeValidator3" SetFocusOnError="True"
                                Type="Double" ErrorMessage="Value  should be between 4 to 100" MinimumValue="4"
                                CssClass="rfvPCG" MaximumValue="100" ControlToValidate="txtExpRateOfReturn" ValidationGroup="btnSave"
                                runat="server"></asp:RangeValidator>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator7" runat="server"
                                ControlToValidate="txtExpRateOfReturn" CssClass="rfvPCG" ValidationGroup="btnSave"
                                ErrorMessage="Please enter some % value"></asp:RequiredFieldValidator>
                        </td>
                        <td align="right" id="tdlblInvestmntLumpsum" runat="server">
                            <asp:Label ID="lblInvestmntLumpsum" runat="server" CssClass="FieldName" Text="Investmnt Required-Lumpsum :"></asp:Label>
                        </td>
                        <td align="left" id="tdlblInvestmntLumpsumTxt" runat="server">
                            <asp:Label ID="lblInvestmntLumpsumTxt" runat="server" CssClass="txtField"></asp:Label>
                        </td>
                    </tr>
                    <%--<tr id="trROIFutureInvestment" runat="server">
                    <td id="Td6" class="leftField" runat="server">
                        <asp:Label ID="lblROIFutureInvest" runat="server" CssClass="FieldName" Text="Return on retirement corpus(%) :"></asp:Label>
                    </td>
                    <td id="Td7" class="rightField" runat="server">
                        <asp:TextBox ID="txtROIFutureInvest" runat="server" AutoCompleteType="Disabled" CssClass="txtField"
                            MaxLength="15"></asp:TextBox>
                              <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtROIFutureInvest"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please enter some % value"></asp:RequiredFieldValidator>
                            <span id="SpanROIFutureInvest" class="spnRequiredField" runat="server">*</span>
                            
                         <ajaxToolkit:FilteredTextBoxExtender ID="txtROIFutureInvest_E" runat="server" Enabled="True" TargetControlID="txtROIFutureInvest"
                                            FilterType="Custom, Numbers" ValidChars=".">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                         <asp:RangeValidator ID="RangeValidator2"  Display="Dynamic" CssClass="rfvPCG"
                            SetFocusOnError="True" Type="Double" ErrorMessage="Value  should be in between 0 and 100"
                            MinimumValue="0.000000001" MaximumValue="100" ControlToValidate="txtROIFutureInvest" 
                            runat="server"></asp:RangeValidator>
                      
                    </td>
                    
                    <td id="tdROIFutureInvestBlank" runat="server" colspan="2">
                     &nbsp;&nbsp;&nbsp
                    </td>
                </tr> --%>
                    <tr>
                        <td class="leftField">
                            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Inflation(%) :"></asp:Label>
                        </td>
                        <td class="rightField">
                            <asp:TextBox ID="txtInflation" runat="server" AutoCompleteType="Disabled" CssClass="txtField"></asp:TextBox>
                            <span id="spnInflation" class="spnRequiredField" runat="server">*</span>
                            <asp:RangeValidator ID="RangeValidator4" Display="Dynamic" class="spnRequiredField"
                                SetFocusOnError="True" Type="Double" ErrorMessage="Inflation value should not less than 4"
                                MinimumValue="4" MaximumValue="100" ControlToValidate="txtInflation" runat="server"></asp:RangeValidator>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator8" runat="server"
                                ControlToValidate="txtInflation" CssClass="rfvPCG" ValidationGroup="btnSave"
                                ErrorMessage="Please enter some % value"></asp:RequiredFieldValidator>
                        </td>
                        <td align="right" id="tdSavingsRequiredMonthly" runat="server">
                            <asp:Label ID="lblSavingsRequiredMonthly" runat="server" CssClass="FieldName" Text="Savings Required-Monthly:"></asp:Label>
                        </td>
                        <td align="left" id="tdSavingsRequiredMonthlyTxt" runat="server">
                            <asp:Label ID="lblSavingsRequiredMonthlyTxt" runat="server" CssClass="txtField"></asp:Label>
                        </td>
                    </tr>
                    <%--<tr id="trReturnOnNewInvestments" runat="server">
                    <td class="leftField">
                        <asp:Label ID="Label9" runat="server" CssClass="FieldName" Text="Return On New Investments(%) :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtReturnOnNewInvestments" runat="server" AutoCompleteType="Disabled" CssClass="txtField"></asp:TextBox>
                        <span id="Span2" class="spnRequiredField" runat="server">*</span>                           
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True" TargetControlID="txtReturnOnNewInvestments"
                                            FilterType="Custom, Numbers" ValidChars=".">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RangeValidator ID="RangeValidator6"  Display="Dynamic" CssClass="rfvPCG"
                            SetFocusOnError="True" Type="Double" ErrorMessage="Value  should be in between 0 and 100"
                            MinimumValue="0.000000001" MaximumValue="100" ControlToValidate="txtReturnOnNewInvestments" 
                            runat="server"></asp:RangeValidator>
                            
                        <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtReturnOnNewInvestments"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please enter some % value"></asp:RequiredFieldValidator>
                    </td>
                    
                    <td id="tdReturnOnNewInvestBlank" runat="server" colspan="2">
                     &nbsp;&nbsp;&nbsp
                    </td>
                </tr> --%>
                    <tr id="trCorpusToBeLeftBehind" runat="server">
                        <td class="leftField">
                            <asp:Label ID="Label10" runat="server" CssClass="FieldName" Text="Corpus To Be Left Behind :"></asp:Label>
                        </td>
                        <td class="rightField">
                            <asp:TextBox ID="txtCorpusToBeLeftBehind" runat="server" AutoCompleteType="Disabled"
                                CssClass="txtField"></asp:TextBox>
                            <span id="spnCorpsToBeLeftBehind" class="spnRequiredField" runat="server">*</span>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                Enabled="True" TargetControlID="txtCorpusToBeLeftBehind" FilterType="Custom, Numbers"
                                ValidChars=".">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RangeValidator ID="RangeValidator7" Display="Dynamic" SetFocusOnError="True"
                                Type="Double" ErrorMessage="Value  should be in between 0 and 9999999999" MinimumValue="0"
                                CssClass="rfvPCG" MaximumValue="9999999999" ControlToValidate="txtCorpusToBeLeftBehind"
                                runat="server"></asp:RangeValidator>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator11" runat="server"
                                ControlToValidate="txtCorpusToBeLeftBehind" CssClass="rfvPCG" ValidationGroup="btnSave"
                                ErrorMessage="Please enter some amount"></asp:RequiredFieldValidator>
                        </td>
                        <td id="tdCorpusToBeLeftBehindBlank" runat="server" colspan="2">
                            &nbsp;&nbsp;&nbsp
                        </td>
                    </tr>
                    <tr>
                        <td class="leftField">
                            <asp:Label ID="lblComment" runat="server" CssClass="FieldName" Text="Comments :"></asp:Label>
                        </td>
                        <td class="rightField">
                            <asp:TextBox ID="txtComment" runat="server" AutoCompleteType="Disabled" CssClass="txtField"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td id="tdCommentBlank" runat="server" colspan="2">
                            &nbsp;&nbsp;&nbsp
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Panel ID="pnlButtonControls" runat="server">
            <table class="TableBackground">
                <tr id="trchkApprove" runat="server">
                    <td id="Td8" runat="server" style="width: 350px">
                        <asp:CheckBox ID="chkApprove" runat="server" CssClass="FieldName" Text=" Approved by Customer" />
                    </td>
                    <td style="float: left">
                        <asp:Button ID="btnCalculateSavLum" runat="server" CssClass="PCGButton" Text="Calculate"
                            ValidationGroup="btnSave" OnClick="OnClick_btnCalculateSavLum" />
                    </td>
                </tr>
                <tr id="trlblApproveOn" runat="server">
                    <td id="Td9" runat="server">
                        <asp:Label ID="lblApproveOn" runat="server" CssClass="FieldName" Text="Customer Approved On "></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnCancel" runat="server" CssClass="PCGButton" Text="Cancel" CausesValidation="False"
                            OnClick="btnCancel_Click" />
                        <asp:Button ID="btnSaveAdd" runat="server" CssClass="PCGButton" OnClick="btnSaveAdd_Click"
                            Text="Save" ValidationGroup="btnSave" />
                        <%--<asp:Button ID="btnNext" runat="server" CssClass="PCGMediumButton" Text="Save & Next" OnClick="btnNext_Click"
                            ValidationGroup="btnSave"  OnClientClick="return validate()"/>--%>
                        <asp:Button ID="btnBackToAddMode" runat="server" CssClass="PCGButton" Text="AddNew"
                            ValidationGroup="btnSave" OnClick="btnBackToAddMode_Click" OnClientClick="return validate()" />
                        <asp:Button ID="btnBackToView" runat="server" CssClass="PCGMediumButton" Text="Back To View"
                            OnClick="btnBackToView_Click" />
                        <asp:Button ID="btnEdit" runat="server" CssClass="PCGButton" Text="Edit" OnClick="btnEdit_Click" />
                        <asp:Button ID="btnUpdate" runat="server" ValidationGroup="btnSave" CssClass="PCGButton"
                            Text="Update" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnFundAdd" runat="server" CssClass="PCGButton" OnClick="btnFundAdd_Click"
                            Text="Fund" ValidationGroup="btnSave" />
                    </td>
                </tr>
                <tr id="tdNote" runat="server">
                    <td>
                        <asp:Label ID="lblNoteHeading" runat="server" CssClass="cmbFielde" Style="font-size: small;"
                            Text="Note :"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td id="Td2" class="tdRequiredText" runat="server">
                        <asp:Label ID="trRequiedNote" CssClass="cmbFielde" Style="font-size: small;" runat="server"
                            Text="1)Fields marked with ' * ' are mandatory."></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNote" runat="server" CssClass="cmbFielde" Style="font-size: small;"
                            Text="2)Expected rate of return as per your risk assessment.If risk profile is not complete, please complete risk profile for return calculation."></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageView7" runat="server">
        <table width="100%">
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" CssClass="HeaderTextBig" Text="Goal Progress"></asp:Label>
                    <hr />
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlFundingProgress" runat="server">
            <table>
                <%--****************************************************************************--%>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblGoalName" Text="Goal:" CssClass="FieldName" runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtGoalName" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="leftField" valign="middle">
                        <asp:Image ID="imgGoalImage" ImageAlign="Left" runat="server" />
                        <span id="span9" class="spnRequiredField" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                        <asp:Label ID="lblGoalStatus" Text="Goal Achievable ?:" CssClass="FieldName" runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:Image ID="imgGoalFundIndicator" ImageAlign="Left" runat="server" />
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblReturnsXIRR" Text="Returns (XIRR)(%):" CssClass="FieldName" runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtReturnsXIRR" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <%-- ****************************************************************************--%>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblStartDate" Text="Start Date:" CssClass="FieldName" runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtStartDate" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblTargetDate" Text="Target Date:" CssClass="FieldName" runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtTargetDate" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblProjectedCompleteYear" Text="Likely Target Date:" CssClass="FieldName"
                            runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtProjectedCompleteYear" runat="server" Text="" CssClass="txtField"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <%-- ****************************************************************************--%>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblEstmdTimeToReachGoal" Text="Time Gap from Target Date:" CssClass="FieldName"
                            runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtEstmdTimeToReachGoal" runat="server" Text="" CssClass="txtField"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblTenureCompleted" Text="Tenure Completed (Years):" CssClass="FieldName"
                            runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtTenureCompleted" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblBalanceTenor" Text="Balance Tenure(Years):" CssClass="FieldName"
                            runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtBalanceTenor" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <%-- ****************************************************************************--%>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblProjectedValueOnGoalDate" Text="Proj. Value On Goal Date:" CssClass="FieldName"
                            runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtProjectedValueOnGoalDate" runat="server" Text="" CssClass="txtField"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblProjectedGap" Text="Projected Gap:" CssClass="FieldName" runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtProjectedGap" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblMonthlyContribution" Text="Monthly Contribution:" CssClass="FieldName"
                            runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtMonthlyContribution" runat="server" Text="" CssClass="txtField"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <%-- ****************************************************************************--%>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblCostAtBeginning" Text="Cost At Beginning:" CssClass="FieldName"
                            runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtCostAtBeginning" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblGoalAmount" Text="Goal Amount:" CssClass="FieldName" runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtGoalAmount" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblAmountInvestedTillDate" Text="Amount Invested Till Date:" CssClass="FieldName"
                            runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtAmountInvested" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <%-- ****************************************************************************--%>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblValueOfCurrentGoal" Text="Value of Current Goal:" CssClass="FieldName"
                            runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtValueOfCurrentGoal" runat="server" Text="" CssClass="txtField"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblAdditionalInvestmentsRequired" Text="Additional Invest. Req/Month:"
                            CssClass="FieldName" runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtAdditionalInvestmentsRequired" runat="server" Text="" CssClass="txtField"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblAdditionalInvestments" Text="Additional Invest. Req/Year:" CssClass="FieldName"
                            runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtAdditionalInvestments" runat="server" Text="" CssClass="txtField"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <%-- ****************************************************************************--%>
                <tr>
                    <td colspan="6">
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageView6" runat="server">
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageView4" runat="server">
        <asp:Panel runat="server" ID="pnlDocuments" Width="90%">
            <br />
            <table id="tblDocuments" runat="server" width="100%">
                <tr>
                    <td style="width: 98%">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            Fund from Existing MF Investments
                        </div>
                    </td>
                    <td style="width: 2%">
                        <asp:ImageButton ID="ImageButton1" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                            runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnexistmfinvest_OnClick"
                            OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="RadGrid1" runat="server" CssClass="RadGrid" GridLines="Both"
                Width="100%" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false"
                ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
                AllowAutomaticUpdates="false" Skin="Telerik" OnItemDataBound="RadGrid1_ItemDataBound"
                OnDeleteCommand="RadGrid1_DeleteCommand" OnInsertCommand="RadGrid1_ItemInserted"
                OnItemUpdated="RadGrid1_ItemUpdated" OnItemCommand="RadGrid1_ItemCommand">
                <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="ExistMFInvestlist">
                </ExportSettings>
                <MasterTableView CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="false"
                    CommandItemSettings-AddNewRecordText="Select MF Investment" DataKeyNames="SchemeCode,OtherGoalAllocation">
                    <Columns>
                        <telerik:GridEditCommandColumn>
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn UniqueName="MemberName" HeaderText="Member Name" DataField="MemberName">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="SchemeName" HeaderText="Scheme" DataField="SchemeName">
                            <%--<HeaderStyle ForeColor="Silver"></HeaderStyle>--%>
                            <%-- <ItemStyle ForeColor="Gray" />--%>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="InvestedAmount" HeaderText="Invstd Amt" DataField="InvestedAmount"
                            DataFormatString="{0:C2}">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Units" HeaderText="Units" DataField="Units"
                            DataFormatString="{0:C2}">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="CurrentValue" HeaderText="Current Value" DataField="CurrentValue"
                            DataFormatString="{0:C2}">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="ReturnsXIRR" HeaderText="XIRR(%)" DataField="ReturnsXIRR">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="ProjectedAmount" HeaderText="Projected amount in goal year"
                            DataField="ProjectedAmount" DataFormatString="{0:n2}">
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn CommandName="Delete" Text="Delete" ConfirmText="Are you sure you want to Remove this Record?"
                            UniqueName="column">
                        </telerik:GridButtonColumn>
                        <%--<telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="column">
                </telerik:GridButtonColumn>--%>
                    </Columns>
                    <EditFormSettings EditFormType="Template">
                        <FormTemplate>
                            <table id="Table2" cellspacing="2" cellpadding="1" width="100%" border="0" rules="none"
                                style="border-collapse: collapse;" class="EditFormSettingsTableColor">
                                <tr class="EditFormHeader">
                                    <td colspan="2" style="font-size: small">
                                        <asp:Label ID="EditFormHeader" runat="server" CssClass="HeaderTextSmall" Text="MF Investment Funding"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="Table3" cellspacing="1" cellpadding="1" border="0" class="module">
                                            <tr>
                                                <td colspan="5">
                                                </td>
                                            </tr>
                                            <tr runat="server" id="trSchemeDDL">
                                                <td align="right">
                                                    <asp:Label ID="lblMemberAddMode" Text="Member Name:" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlMemberName" runat="server" CssClass="cmbField" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlMemberName_OnSelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" Text="Please Select Member"
                                                        InitialValue="Select" ControlToValidate="ddlMemberName" Display="Dynamic" runat="server"
                                                        CssClass="rfvPCG" ValidationGroup="btnMFSubmit">
                                                    </asp:RequiredFieldValidator>
                                                    <%-- <asp:Label ID="lblMemberNameAddMode"  CssClass="FieldName"
                                                        runat="server">
                                                    </asp:Label>      --%>
                                                </td>
                                                <td>
                                                </td>
                                                <td id="tdlblSchemeName" runat="server" align="right">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="Label133" Text="Scheme:" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td id="tdddlPickScheme" runat="server">
                                                    <asp:DropDownList ID="ddlPickScheme" runat="server" CssClass="cmbLongField" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlPickScheme_OnSelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" Text="Please Select Scheme"
                                                        InitialValue="Select" ControlToValidate="ddlPickScheme" Display="Dynamic" runat="server"
                                                        CssClass="rfvPCG" ValidationGroup="btnMFSubmit">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="trSchemeTextBox">
                                                <td align="right">
                                                    <asp:Label ID="lblMemberName" Text="Member Name:" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtMemberName" Text='<%# Bind("MemberName") %>' CssClass="txtField"
                                                        runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label13" Text="Scheme:" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblGoalName" Text='<%# Bind("SchemeName") %>' CssClass="txtField"
                                                        runat="server">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trUnits" runat="server">
                                                <td align="right">
                                                    <asp:Label ID="Label14" Text="Units:" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtUnits" runat="server" CssClass="txtField" Text='<%# Bind("Units") %>'
                                                        Enabled="false" TabIndex="2">
                                                    </asp:Label>
                                                    <asp:Label ID="txtUnitsAddMode" runat="server" CssClass="txtField" Enabled="false"
                                                        TabIndex="2">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label11" Text="Amount Available:" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtAmtAvailableEditMode" runat="server" CssClass="txtField" Text='<%# Bind("AvailableAmount") %>'
                                                        Enabled="false" TabIndex="2">
                                                    </asp:Label>
                                                    <asp:Label ID="txtAmtAvailableAddMode" runat="server" CssClass="txtField" Enabled="false"
                                                        TabIndex="2">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trCurrentValue" runat="server">
                                                <td align="right">
                                                    <asp:Label ID="Label15" Text="Current Value:" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtCurrentValueEditMode" CssClass="txtField" runat="server" Text='<%# Bind("CurrentValue") %>'
                                                        Enabled="false" TabIndex="3">
                                                    </asp:Label>
                                                    <asp:Label ID="txtCurrentValueAddMode" CssClass="txtField" runat="server" Enabled="false"
                                                        TabIndex="3">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label2" Text="Amount Marked for the Goal:" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtInvestedAmt" CssClass="txtField" runat="server" Text='<%# Bind("InvestedAmount") %>'
                                                        Enabled="false" TabIndex="3">
                                                    </asp:Label>
                                                    <asp:Label ID="txtInvestedAmtAdd" CssClass="txtField" runat="server" Enabled="false"
                                                        TabIndex="3">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trTotalGoalAllocation" runat="server">
                                                <td align="right">
                                                    <asp:Label ID="Label16" Text="Total Goal Allocation(%):" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="TextBox1" runat="server" CssClass="txtField" Enabled="false" Text='<%# Bind("AllocationEntry") %>'
                                                        TabIndex="3">
                                                    </asp:Label>
                                                    <asp:Label ID="txtAllocationEntryAddMode" runat="server" CssClass="txtField" Enabled="false"
                                                        TabIndex="3">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label17" Text="Current Goal Allocation(%):" CssClass="FieldName" runat="server"
                                                        Enabled="false">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox4" runat="server" CssClass="txtField" Text='<%# Bind("CurrentGoalAllocation") %>'
                                                        TabIndex="3"> 
                                                    </asp:TextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBox4_TextBoxWatermarkExtender" runat="server"
                                                        Enabled="True" TargetControlID="TextBox4" WatermarkText="Please enter here..">
                                                    </cc1:TextBoxWatermarkExtender>
                                                    <asp:RequiredFieldValidator ID="rfvAllocationEntry" ControlToValidate="TextBox4"
                                                        ErrorMessage="Please fill the allocation" Display="Dynamic" runat="server" CssClass="rfvPCG"
                                                        ValidationGroup="btnMFSubmit"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator8"
                                                        runat="server" CssClass="rfvPCG" ControlToValidate="TextBox4" ValidationGroup="btnMFSubmit"
                                                        ErrorMessage="Please Enter Numeric Value" ValidationExpression="\d+\.?\d*"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr id="trOtherGoalAllocation" runat="server">
                                                <td align="right">
                                                    <asp:Label ID="Label18" Text="Other Goal Allocation(%):" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtSchemeAllocationPerEditMode" CssClass="txtField" runat="server"
                                                        Text='<%# Bind("OtherGoalAllocation") %>' Enabled="false" TabIndex="1">
                                                    </asp:Label>
                                                    <asp:Label ID="txtSchemeAllocationPerAddMode" CssClass="txtField" runat="server"
                                                        Enabled="false" TabIndex="1">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label19" Text="Available Allocation(%):" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtAvailableAllocationEditMode" runat="server" CssClass="txtField"
                                                        Enabled="false" Text='<%# Bind("AvailableAllocation") %>' TabIndex="1">
                                                    </asp:Label>
                                                    <asp:Label ID="txtAvailableAllocationAddMode" runat="server" CssClass="txtField"
                                                        Enabled="false" TabIndex="1">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                            runat="server" CssClass="PCGButton" ValidationGroup="btnMFSubmit" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                        </asp:Button>&nbsp;
                                        <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton"
                                            CommandName="Cancel"></asp:Button>
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
        <asp:Panel runat="server" ID="pnlMFFunding" Width="90%">
            <br />
            <table id="Table1" runat="server" width="100%">
                <tr>
                    <td style="width: 98%">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            Fund from Future MF Savings
                        </div>
                    </td>
                    <td style="width: 2%">
                        <asp:ImageButton ID="btnFutureMFinvest" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                            runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="FutureMFinvest_OnClick"
                            OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="RadGrid2" runat="server" CssClass="RadGrid" GridLines="Both"
                AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false"
                ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
                AllowAutomaticUpdates="false" Skin="Telerik" OnItemDataBound="RadGrid2_ItemDataBound"
                OnInsertCommand="RadGrid2_ItemInserted" OnDeleteCommand="RadGrid2_DeleteCommand"
                OnItemCommand="RadGrid2_ItemCommand">
                <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="FutureMFInvestlist">
                </ExportSettings>
                <MasterTableView CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="false"
                    CommandItemSettings-AddNewRecordText="Select MF Savings" DataKeyNames="SIPId,TotalSIPamount">
                    <Columns>
                        <telerik:GridEditCommandColumn>
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn UniqueName="SIPMemberName" HeaderText="Member Name" DataField="MemberName">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="SchemeName" HeaderText="Scheme" DataField="SchemeName">
                            <%--<HeaderStyle ForeColor="Silver"></HeaderStyle>--%>
                            <%-- <ItemStyle ForeColor="Gray" />--%>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="AvailableAllocation" HeaderText="SIP Amount Available"
                            DataField="AvailableAllocation">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="SIPInvestedAmount" HeaderText="SIP Invstd Amt"
                            DataField="SIPInvestedAmount">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="SIPProjectedAmount" HeaderText="SIP Projected Amount"
                            DataField="SIPProjectedAmount">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn CommandName="Delete" Text="Delete" ConfirmText="Are you sure you want to Remove this Record?"
                            UniqueName="column">
                        </telerik:GridButtonColumn>
                        <%--<telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="column">
                </telerik:GridButtonColumn>--%>
                    </Columns>
                    <EditFormSettings EditFormType="Template">
                        <FormTemplate>
                            <table id="Table2" cellspacing="2" cellpadding="1" width="100%" border="0" rules="none"
                                style="border-collapse: collapse;" class="EditFormSettingsTableColor">
                                <tr class="EditFormHeader">
                                    <td colspan="2" style="font-size: small">
                                        <asp:Label ID="EditFormHeaderSIP" runat="server" CssClass="HeaderTextSmall" Text="Monthly SIP MF Funding"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="Table3" cellspacing="1" cellpadding="1" border="0" class="module">
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="trSchemeNameDDL">
                                                <td align="right">
                                                    <asp:Label ID="lblSIPMemberAddMode" Text="Member Name:" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSIPMemberName" runat="server" CssClass="cmbField" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlSIPMemberName_OnSelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" Text="Please Select Member"
                                                        InitialValue="Select" ControlToValidate="ddlSIPMemberName" Display="Dynamic"
                                                        runat="server" CssClass="rfvPCG" ValidationGroup="btnSIPSubmit">
                                                    </asp:RequiredFieldValidator>
                                                    <%-- <asp:Label ID="lblMemberNameAddMode" CssClass="FieldName"
                                                        runat="server">
                                                    </asp:Label>      --%>
                                                </td>
                                                <td align="right" runat="server" id="tdSipScheme">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="Label20" Text="Scheme-Amount-SIP Date:" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td id="tdddlPickSIPScheme" runat="server">
                                                    <asp:DropDownList ID="ddlPickSIPScheme" runat="server" CssClass="cmbLongField" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlPickSIPScheme_OnSelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" Text="Please Select SIP"
                                                        InitialValue="Select" ControlToValidate="ddlPickSIPScheme" Display="Dynamic"
                                                        runat="server" CssClass="rfvPCG" ValidationGroup="btnSIPSubmit">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="trSchemeNameText">
                                                <td align="right">
                                                    <asp:Label ID="Label21" Text="Scheme:" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSchemeName" Text='<%# Bind("SchemeName") %>' CssClass="txtField"
                                                        runat="server">
                                                    </asp:Label>
                                                    <asp:Label ID="lblSchemeAdd" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label26" Text="SIP Frequecny:" CssClass="FieldName" runat="server"
                                                        Enabled="false">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtSIPFrequency" runat="server" Enabled="false" CssClass="txtField"
                                                        Text='<%# Bind("SIPFrequecny") %>' TabIndex="3">
                                                    </asp:Label>
                                                    <asp:Label ID="txtSIPFrequencyAdd" runat="server" Enabled="false" CssClass="txtField"
                                                        TabIndex="3">
                                                    </asp:Label>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="lblMemberName" Text="Member Name:" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtMemberName" Text='<%# Bind("MemberName") %>' CssClass="txtField"
                                                        runat="server">
                                                    </asp:Label>
                                                    <asp:Label ID="txtMemberNameAdd" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trAllocationEntry" runat="server">
                                                <td align="right">
                                                    <asp:Label ID="Label24" Text="Available Amount:" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="TextBox2" runat="server" CssClass="txtField" Enabled="false" Text='<%# Bind("AvailableAllocation") %>'
                                                        TabIndex="1">
                                                    </asp:Label>
                                                    <asp:Label ID="TextBox2Add" runat="server" CssClass="txtField" Enabled="false" TabIndex="1">
                                                    </asp:Label>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label4" Text="Total SIP Amount:" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtTotalSIPAmount" runat="server" CssClass="txtField" Enabled="false"
                                                        Text='<%# Bind("TotalSIPamount") %>' TabIndex="1">
                                                    </asp:Label>
                                                    <asp:Label ID="txtTotalSIPAmountAdd" runat="server" CssClass="txtField" Enabled="false"
                                                        TabIndex="1">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trAvailableAmount" runat="server">
                                                <td align="right">
                                                    <asp:Label ID="Label23" Text="Other Goal Invested Amount:" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtOtherSchemeAllocationPer" CssClass="txtField" runat="server" Text='<%# Bind("OtherGoalAllocation") %>'
                                                        Enabled="false" TabIndex="1">
                                                    </asp:Label>
                                                    <asp:Label ID="txtOtherSchemeAllocationPerAdd" CssClass="txtField" runat="server"
                                                        Enabled="false" TabIndex="1">
                                                    </asp:Label>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label22" Text="Current Goal Invested Amount:" CssClass="FieldName"
                                                        runat="server" Enabled="false">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox3" runat="server" CssClass="txtField" Text='<%# Bind("SIPInvestedAmount") %>'
                                                        TabIndex="3">
                                                    </asp:TextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBox3_TextBoxWatermarkExtender" runat="server"
                                                        Enabled="True" TargetControlID="TextBox3" WatermarkText="Please enter SIP Amt.">
                                                    </cc1:TextBoxWatermarkExtender>
                                                    <asp:RequiredFieldValidator ID="rfvAllocationEntry" ControlToValidate="TextBox3"
                                                        ErrorMessage="Please fill the allocation" Display="Dynamic" runat="server" CssClass="rfvPCG"
                                                        ValidationGroup="btnSIPSubmit"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator8"
                                                        runat="server" CssClass="rfvPCG" ControlToValidate="TextBox3" ValidationGroup="btnSIPSubmit"
                                                        ErrorMessage="Please Enter Numeric Value" ValidationExpression="\d+\.?\d*"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr id="trSIPStartDate" runat="server">
                                                <td align="right">
                                                    <asp:Label ID="Label12" Text="SIP Start Date:" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtSIPStartDate" runat="server" CssClass="txtField" Enabled="false"
                                                        Text='<%# Bind("SIPStartDate") %>' TabIndex="1">
                                                    </asp:Label>
                                                    <asp:Label ID="txtSIPStartDateAdd" runat="server" CssClass="txtField" Enabled="false"
                                                        TabIndex="1">
                                                    </asp:Label>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label25" Text="SIP End Date:" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtSIPEndDate" runat="server" CssClass="txtField" Enabled="false"
                                                        Text='<%# Bind("SIPEndDate") %>' TabIndex="1">
                                                    </asp:Label>
                                                    <asp:Label ID="txtSIPEndDateAdd" runat="server" CssClass="txtField" Enabled="false"
                                                        TabIndex="1">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="Button3" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                            runat="server" CssClass="PCGButton" ValidationGroup="btnSIPSubmit" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                        </asp:Button>&nbsp;
                                        <asp:Button ID="Button4" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton"
                                            CommandName="Cancel"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </FormTemplate>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings>
                </ClientSettings>
            </telerik:RadGrid>
            <table width="100%">
                <tr>
                    <td align="right">
                        <asp:Button ID="btnSIPAdd" runat="server" CssClass="PCGButton" Text="Add SIP" OnClick="btnSIPAdd_OnClick" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageView5" runat="server">
        <asp:Panel runat="server" ID="Panel3" Width="90%">
            <br />
            <table id="Table4" runat="server" width="100%">
                <tr>
                    <td style="width: 98%">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            Fund from Existing EQ Investments
                        </div>
                    </td>
                    <td style="width: 2%">
                        <asp:ImageButton ID="ImageButton2" Visible="false" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                            runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnexistEQinvest_OnClick"
                            OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="RadGrid4" runat="server" CssClass="RadGrid" GridLines="Both"
                Width="100%" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false"
                ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
                OnItemDataBound="RadGrid4_ItemDataBound" OnItemCommand="RadGrid4_ItemCommand"
                OnDeleteCommand="RadGrid4_DeleteCommand" OnInsertCommand="RadGrid4_ItemInserted"
                AllowAutomaticUpdates="false" Skin="Telerik">
                <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="ExistEQinvestList">
                </ExportSettings>
                <MasterTableView CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="false"
                    CommandItemSettings-AddNewRecordText="Select EQ Investment" DataKeyNames="PEM_ScripCode,CENPS_Id,OtherEquityGoalAllocation">
                    <Columns>
                        <telerik:GridEditCommandColumn>
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn UniqueName="MemberNameEquity" HeaderText="Member Name" DataField="MemberName">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="PEM_CompanyName" HeaderText="Scrips" DataField="PEM_CompanyName">
                            <%--<HeaderStyle ForeColor="Silver"></HeaderStyle>--%>
                            <%-- <ItemStyle ForeColor="Gray" />--%>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="AvailableShares" HeaderText="Shares Available"
                            DataField="AvailableShares">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="AllocatedShares" HeaderText="Shares Allocated"
                            DataField="AllocatedShares">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="EquityProjectedAmount" HeaderText="Projected Amount"
                            DataField="EquityProjectedAmount">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn CommandName="Delete" Text="Delete" ConfirmText="Are you sure you want to Remove this Record?"
                            UniqueName="column">
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings EditFormType="Template">
                        <FormTemplate>
                            <table id="tblEquityFundingHeader" cellspacing="2" cellpadding="1" width="100%" border="0"
                                class="EditFormSettingsTableColor">
                                <tr class="EditFormHeader">
                                    <td colspan="2">
                                        <%-- <b>EQ Investment Funding</b>--%>
                                        <asp:Label runat="server" CssClass="HeaderTextSmall" Text="EQ Investment Funding"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="tblEquityFunding" cellspacing="1" cellpadding="1" border="0" class="module">
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="trScripsDDL">
                                                <td align="right">
                                                    <asp:Label ID="lblMemberEQAddMode" Text="Member Name:" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlMemberNameEq" runat="server" CssClass="cmbField" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlMemberNameEquity_OnSelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <%-- <asp:Label ID="lblMemberNameAddMode"  CssClass="FieldName"
                                                        runat="server">
                                                    </asp:Label>      --%>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" Text="Please Select Member"
                                                        InitialValue="Select" ControlToValidate="ddlMemberNameEq" Display="Dynamic" runat="server"
                                                        CssClass="rfvPCG" ValidationGroup="btnSubmit">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td align="right" id="tdlblPickScrips" runat="server">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="lblPickScrips" Text="Scrips:" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td id="tdddlPickScrips" runat="server">
                                                    <asp:DropDownList ID="ddlPickScrips" runat="server" CssClass="cmbLongField" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlPickScrips_OnSelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Text="Please Select Scrips"
                                                        InitialValue="Select" ControlToValidate="ddlPickScrips" Display="Dynamic" runat="server"
                                                        CssClass="rfvPCG" ValidationGroup="btnSubmit">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="trScripsTextBox">
                                                <td align="right">
                                                    <asp:Label ID="Label13" Text="Scrips:" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblScripName" CssClass="txtField" Text='<%# Bind("PEM_CompanyName") %>'
                                                        runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="lblMemberName" Text="Member Name:" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtMemberName" CssClass="txtField" Text='<%# Bind("MemberName") %>'
                                                        runat="server">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trTotalShares" runat="server">
                                                <td align="right">
                                                    <asp:Label ID="lblShares" Text="Total Shares:" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtShares" runat="server" CssClass="txtField" Text='<%# Bind("TotalHoldingEquityShares") %>'
                                                        Enabled="false" TabIndex="2">
                                                    </asp:Label>
                                                    <asp:Label ID="txtSharesAdd" runat="server" CssClass="txtField" Enabled="false" TabIndex="2">
                                                    </asp:Label>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label11" Text="Available Shares After Allocation:" CssClass="FieldName"
                                                        runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtShareAvailableEditMode" runat="server" CssClass="txtField" Text='<%# Bind("AvailableShares") %>'
                                                        Enabled="false" TabIndex="2">
                                                    </asp:Label>
                                                    <asp:Label ID="txtShareAvailableAddMode" runat="server" CssClass="txtField" Enabled="false"
                                                        TabIndex="2">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trCurrentValue" runat="server">
                                                <td align="right">
                                                    <asp:Label ID="Label15" Text="Current Value:" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtCurrentValueEditMode" CssClass="txtField" runat="server" Text='<%# Bind("CurrentValue") %>'
                                                        Enabled="false" TabIndex="3">
                                                    </asp:Label>
                                                    <asp:Label ID="txtCurrentValueAddMode" CssClass="txtField" runat="server" Enabled="false"
                                                        TabIndex="3">
                                                    </asp:Label>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label2" Text="Share Marked for the Goal:" CssClass="FieldName" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtInvestedAmtEq" CssClass="txtField" runat="server" Text='<%# Bind("AllocatedShares") %>'
                                                        Enabled="false" TabIndex="3">
                                                    </asp:Label>
                                                    <asp:Label ID="txtInvestedAmtAddEq" CssClass="txtField" runat="server" Enabled="false"
                                                        TabIndex="3">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trTotalGoalAllocation" runat="server">
                                                <td align="right">
                                                    <asp:Label ID="Label16" Text="Total Goal Allocation(Shares):" CssClass="FieldName"
                                                        runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtAllocationTotalEditModeEQ" runat="server" CssClass="txtField" Enabled="false"
                                                        Text='<%# Bind("TotalSharesAllocation") %>' TabIndex="3">
                                                    </asp:Label>
                                                    <asp:Label ID="txtAllocationTotalAddMode" runat="server" CssClass="txtField" Enabled="false"
                                                        TabIndex="3">
                                                    </asp:Label>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label17" Text="Current Goal Allocation(Shares):" CssClass="FieldName"
                                                        runat="server" Enabled="false">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAllocationEntryEquity" runat="server" CssClass="txtField" Text='<%# Bind("AllocatedShares") %>'
                                                        TabIndex="3"> 
                                                    </asp:TextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="txtAllocationEntryEquity_TextBoxWatermarkExtender"
                                                        runat="server" Enabled="True" TargetControlID="txtAllocationEntryEquity" WatermarkText="Please enter here..">
                                                    </cc1:TextBoxWatermarkExtender>
                                                    <asp:RequiredFieldValidator ID="rfvAllocationEntry" ControlToValidate="txtAllocationEntryEquity"
                                                        ErrorMessage="Please fill the allocation" Display="Dynamic" runat="server" CssClass="rfvPCG"
                                                        ValidationGroup="btnSubmit"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator8"
                                                        runat="server" ValidationGroup="btnSubmit" CssClass="rfvPCG" ControlToValidate="txtAllocationEntryEquity"
                                                        ErrorMessage="Please Enter Numeric Value" ValidationExpression="\d+\.?\d*"></asp:RegularExpressionValidator>
                                                    <asp:CompareValidator ID="cvAvlShares" runat="server" CssClass="rfvPCG" Operator="LessThanEqual"
                                                        Display="Dynamic" ControlToCompare="lblAvailableSharesforCurrentGoalEdit" Type="Double"
                                                        ControlToValidate="txtAllocationEntryEquity" ErrorMessage="Check Your Available Allocation"
                                                        ValidationGroup="btnSubmit"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="rfvPCG" Operator="LessThanEqual"
                                                        Type="Double" Display="Dynamic" ControlToCompare="lblAvailableSharesforCurrentGoalAdd"
                                                        ControlToValidate="txtAllocationEntryEquity" ErrorMessage="Check Your Available Allocation"
                                                        ValidationGroup="btnSubmit"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr id="trOtherGoalAllocation" runat="server">
                                                <td align="right">
                                                    <asp:Label ID="Label18" Text="Other Goal Allocation(Shares):" CssClass="FieldName"
                                                        runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtShareOtherAllocationEditMode" CssClass="txtField" runat="server"
                                                        Text='<%# Bind("OtherEquityGoalAllocation") %>' Enabled="false" TabIndex="1">
                                                    </asp:Label>
                                                    <asp:Label ID="txtShareOtherAllocationAddMode" CssClass="txtField" runat="server"
                                                        Enabled="false" TabIndex="1">
                                                    </asp:Label>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label27" Text="Available Shares For Allocation:" CssClass="FieldName"
                                                        runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="lblAvailableSharesforCurrentGoalEdit" runat="server" CssClass="txtField"
                                                        Text='<%# Bind("AvailableSharesforCurrentGoal") %>' Enabled="false" TabIndex="2">
                                                    </asp:TextBox>
                                                    <asp:TextBox ID="lblAvailableSharesforCurrentGoalAdd" runat="server" CssClass="txtField"
                                                        Enabled="false" TabIndex="2">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                            runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                        </asp:Button>&nbsp;
                                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                            CssClass="PCGButton" CommandName="Cancel"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </FormTemplate>
                    </EditFormSettings>
                </MasterTableView>
                <ValidationSettings CommandsToValidate="PerformInsert,Update" />
                <ClientSettings>
                </ClientSettings>
            </telerik:RadGrid>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageView3" runat="server">
        <asp:Panel ID="Panel2" runat="server">
            <table width="100%">
                <tr>
                    <td align="center">
                        <div id="Div5" class="failure-msg" align="center">
                            Fixed Income No Record Found
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageView2" runat="server">
        <table width="100%">
            <tr>
                <td>
                    <asp:Label ID="lblModelPortolioHeading" runat="server" CssClass="HeaderTextBig" Text="Model Portfolio"></asp:Label>
                    <hr />
                </td>
            </tr>
        </table>
        <asp:Panel runat="server" ID="pnlModelPortfolio">
            <%--<table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server" visible="false">
         <tr>
            <td align="center">
             <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
             </div>
            </td>
        </tr>
       </table>--%>
            <table width="100%" runat="server" id="tblModelPortFolioDropDown">
                <tr style="float: left">
                    <td>
                        <asp:Label ID="lblModelPortfolio" runat="server" CssClass="FieldName" Text="Select Model Portfolio :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlModelPortFolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlModelPortFolio_OnSelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <table id="tableGrid" runat="server" class="TableBackground" width="100%">
                <tr>
                    <td>
                        <telerik:RadGrid ID="RadGrid3" runat="server" Skin="Telerik" CssClass="RadGrid" GridLines="Both"
                            AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"
                            ShowStatusBar="true" AllowAutomaticDeletes="false" AllowAutomaticInserts="false"
                            AllowAutomaticUpdates="false" HorizontalAlign="NotSet">
                            <MasterTableView>
                                <Columns>
                                    <%--<telerik:GridClientSelectColumn UniqueName="SelectColumn"/>--%>
                                    <telerik:GridBoundColumn DataField="PASP_SchemePlanName" HeaderText="Scheme Name"
                                        UniqueName="PASP_SchemePlanName">
                                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AMFMPD_AllocationPercentage" HeaderText="Weightage"
                                        UniqueName="AMFMPD_AllocationPercentage">
                                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AMFMPD_AddedOn" HeaderText="Started Date" UniqueName="AMFMPD_AddedOn">
                                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AMFMPD_SchemeDescription" HeaderText="Description"
                                        UniqueName="AMFMPD_SchemeDescription">
                                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <%-- <table id="tblArchive" runat="server">
            <tr>
            <td class="leftField">
                        <asp:Label ID="lblArchive" runat="server" CssClass="FieldName" Text="Reason for Archiving:"></asp:Label>
                    </td>
                    <td class="rightField">                         
                        <asp:DropDownList ID="ddlArchive" runat="server" CssClass="cmbField">               
                        </asp:DropDownList>
                    </td>
            </tr>
            </table>--%>
                            </MasterTableView>
                            <ClientSettings>
                                <%--<ClientEvents OnRowDblClick="RowDblClick" />--%>
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlModelPortfolioNoRecoredFound" Width="100%" Visible="false">
            <table width="100%">
                <tr>
                    <td align="center">
                        <div id="Div2" class="failure-msg" align="center">
                            No Record Found
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
</telerik:RadMultiPage>
<asp:HiddenField ID="hdfAvailableAmount" runat="server" />
