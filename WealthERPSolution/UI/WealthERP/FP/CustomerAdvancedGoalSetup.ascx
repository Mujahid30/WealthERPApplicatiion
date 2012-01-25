<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerAdvancedGoalSetup.ascx.cs" Inherits="WealthERP.FP.CustomerAdvancedGoalSetup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>




<script type="text/javascript">

    function MFBasedGoalSelection(value) {
        
           if(value=='rdoMFBasedGoalYes')
               {
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

 <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
 
  <telerik:RadTabStrip ID="RadTabStripFPGoalDetails" runat="server" EnableTheming="True" Skin="Telerik"
    EnableEmbeddedSkins="False" MultiPageID="CustomerFPGoalDetail">
    <Tabs>
        <telerik:RadTab runat="server" Text="Goal Add/View" Value="GoalAdd" TabIndex="0">
        </telerik:RadTab>  
        <telerik:RadTab runat="server" Text="Goal Funding/Progress" Value="Funding" TabIndex="1">
        </telerik:RadTab>  
         <telerik:RadTab runat="server" Text="Model Portfolio" Value="Model" TabIndex="2">
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
                         <telerik:radcombobox id="ddlGoalType" OnSelectedIndexChanged="ddlGoalType_OnSelectedIndexChange" CssClass="cmbField" runat="server" Width="150px" EnableEmbeddedSkins="false" skin="Telerik" allowcustomtext="true" AutoPostBack="true" CausesValidation="false" ValidationGroup="btnSave" onchange="Page_BlockSubmit = false;">    
                                <Items>   
                                    <telerik:RadComboBoxItem ImageUrl="~/Images/Select.png" Text="Select" Value="0" Selected="true">        
                                    </telerik:RadComboBoxItem>   
                                    <telerik:RadComboBoxItem ImageUrl="~/Images/HomeGoal.png" Text="Buy Home" Value="BH" runat="server">        
                                    </telerik:RadComboBoxItem>    
                                    <telerik:RadComboBoxItem ImageUrl="~/Images/EducationGoal.png" Text="Child Education" Value="ED" runat="server">        
                                    </telerik:RadComboBoxItem>        
                                    <telerik:RadComboBoxItem ImageUrl="~/Images/ChildMarraiageGoal.png" Text="Child Marriage" Value="MR" runat="server">        
                                    </telerik:RadComboBoxItem>        
                                    <telerik:RadComboBoxItem ImageUrl="~/Images/OtherGoal.png" Text="Other" Value="OT" runat="server">        
                                    </telerik:RadComboBoxItem>   
                                    <telerik:RadComboBoxItem ImageUrl="~/Images/RetirementGoal.png" Text="Retirement" Value="RT" runat="server">        
                                    </telerik:RadComboBoxItem>       
                                          
                                       
                                </Items>
                        </telerik:radcombobox>
                        <span id="spanGoalTypeGoalAdd" class="spnRequiredField" runat="server">*</span>  
                        <asp:RequiredFieldValidator ID="reqValQuestionType" runat="server" CssClass="rfvPCG" ErrorMessage="Select Goal Type" Text="Select Goal Type" Display="Dynamic" ValidationGroup="btnSave" ControlToValidate="ddlGoalType" InitialValue="Select">
                        </asp:RequiredFieldValidator>                    
                    </td>
                    
                    <td class="leftField" id="tdCustomerAge1" runat="server">
                        <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Customer Age :"></asp:Label>
                    </td>
                    <td class="rightField" id="tdCustomerAge2" runat="server">
                        <asp:TextBox ID="txtCustomerAge" runat="server" AutoCompleteType="Disabled" CssClass="txtField"></asp:TextBox>
                        <span id="Span8" class="spnRequiredField" runat="server" visible="false">*</span>                           
                        <asp:RangeValidator ID="RangeValidator12"  Display="Dynamic" 
                            SetFocusOnError="True" Type="Double" ErrorMessage="Value should be between 18 to 150"
                            MinimumValue="18" MaximumValue="150" ControlToValidate="txtCustomerAge" 
                            runat="server"></asp:RangeValidator>
                        <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtCustomerAge"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please enter some value"></asp:RequiredFieldValidator>
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
                        <asp:RangeValidator ID="RangeValidator11"  Display="Dynamic" 
                            SetFocusOnError="True" Type="Double" ErrorMessage="Value should be between 18 to 150"
                            MinimumValue="0" MaximumValue="150" ControlToValidate="txtSpouseAge" 
                            runat="server"></asp:RangeValidator>
                        <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtSpouseAge"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please enter some value"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                
                <tr id="trGoalDesc" runat="server">
                    <td id="Td2333" class="leftField" runat="server">
                        <asp:Label ID="lblGoalDescription" runat="server" CssClass="FieldName" Text="Goal Description :"></asp:Label>
                    </td>
                    <td id="Td333444" class="rightField" runat="server">
                        <asp:TextBox ID="txtGoalDescription" runat="server" MaxLength="15" AutoCompleteType="Disabled" CssClass="txtField"></asp:TextBox>
                    </td>
                    
                    <td class="leftField" id="tdRetirementAge1" runat="server">
                        <asp:Label ID="Label5" runat="server" CssClass="FieldName" Text="Retirement Age :"></asp:Label>
                    </td>
                    <td class="rightField" id="tdRetirementAge2" runat="server">
                        <asp:TextBox ID="txtRetirementAge" runat="server" AutoCompleteType="Disabled" CssClass="txtField" ></asp:TextBox>
                        <span id="Span6" class="spnRequiredField" runat="server" visible="false">*</span>                           
                        <asp:RangeValidator ID="RangeValidator10"  Display="Dynamic" 
                            SetFocusOnError="True" Type="Double" ErrorMessage="Value should be between 30 to 65"
                            MinimumValue="30" MaximumValue="65" ControlToValidate="txtRetirementAge" 
                            runat="server"></asp:RangeValidator>
                        <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtRetirementAge"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please enter some value"></asp:RequiredFieldValidator>
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
                        <asp:Label ID="lblGoalCostToday" runat="server" CssClass="FieldName" Text="Goal Cost Today :"></asp:Label>
                    </td>
                    <td id="Td1" class="rightField" runat="server">
                        <asp:TextBox ID="txtGoalCostToday" runat="server" CssClass="txtField"></asp:TextBox>
                        <span id="SpanGoalCostTodayReq" class="spnRequiredField" runat="server">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtGoalCostToday" ValidationGroup="btnSave" CssClass="rfvPCG" ErrorMessage="Goal cost Today Required" Display="Dynamic"></asp:RequiredFieldValidator>
                             <ajaxToolkit:FilteredTextBoxExtender ID="txtGoalCostToday_E" runat="server" Enabled="True" TargetControlID="txtGoalCostToday"
                                            FilterType="Custom, Numbers" ValidChars=".">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                        
                         
                        <asp:RangeValidator ID="RVtxtGoalCostToday" Display="Dynamic" CssClass="rfvPCG"  
                            SetFocusOnError="True" Type="Double" ErrorMessage="Value  should not be more than 15 digit & can't be zero"
                             ValidationGroup="btnSave" MinimumValue="0.00000000001" MaximumValue="999999999999999" 
                            ControlToValidate="txtGoalCostToday" runat="server"></asp:RangeValidator>
                         
                    </td>
                    
                    <td class="leftField" id="tdCustomerEOL1" runat="server">
                        <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Customer EOL :"></asp:Label>
                    </td>
                    <td class="rightField" id="tdCustomerEOL2" runat="server">
                        <asp:TextBox ID="txtCustomerEOL" runat="server" AutoCompleteType="Disabled" CssClass="txtField" ></asp:TextBox>
                        <span id="Span5" class="spnRequiredField" runat="server" visible="false">*</span>                           
                        <asp:RangeValidator ID="RangeValidator9"  Display="Dynamic" 
                            SetFocusOnError="True" Type="Double" ErrorMessage="Value should be between 30 to 150"
                            MinimumValue="30" MaximumValue="150" ControlToValidate="txtCustomerEOL" 
                            runat="server"></asp:RangeValidator>
                        <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtCustomerEOL"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please enter some value"></asp:RequiredFieldValidator>
                    </td>
                   
                </tr>
                
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblGoalYear" runat="server" CssClass="FieldName" Text="Goal Year :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlGoalYear" runat="server" CssClass="cmbField" CausesValidation="True">                                                       
                            <asp:ListItem Selected="True">2013</asp:ListItem>
                            <asp:ListItem>2014</asp:ListItem>
                            <asp:ListItem>2015</asp:ListItem>
                            <asp:ListItem>2016</asp:ListItem>
                            <asp:ListItem>2017</asp:ListItem>
                            <asp:ListItem>2018</asp:ListItem>
                            <asp:ListItem>2019</asp:ListItem>
                            <asp:ListItem>2020</asp:ListItem>
                            <asp:ListItem>2021</asp:ListItem>
                            <asp:ListItem>2022</asp:ListItem>
                            <asp:ListItem>2023</asp:ListItem>
                            <asp:ListItem>2024</asp:ListItem>
                            <asp:ListItem>2025</asp:ListItem>
                            <asp:ListItem>2026</asp:ListItem>
                            <asp:ListItem>2027</asp:ListItem>
                            <asp:ListItem>2028</asp:ListItem>
                            <asp:ListItem>2029</asp:ListItem>
                            <asp:ListItem>2030</asp:ListItem>
                            <asp:ListItem>2031</asp:ListItem>
                            <asp:ListItem>2032</asp:ListItem>
                            <asp:ListItem>2033</asp:ListItem>
                            <asp:ListItem>2034</asp:ListItem>
                            <asp:ListItem>2035</asp:ListItem>
                            <asp:ListItem>2036</asp:ListItem>
                            <asp:ListItem>2037</asp:ListItem>
                            <asp:ListItem>2038</asp:ListItem>
                            <asp:ListItem>2039</asp:ListItem>
                            <asp:ListItem>2040</asp:ListItem>
                            <asp:ListItem>2041</asp:ListItem>
                            <asp:ListItem>2042</asp:ListItem>
                            <asp:ListItem>2043</asp:ListItem>
                            <asp:ListItem>2044</asp:ListItem>
                            <asp:ListItem>2045</asp:ListItem>
                            <asp:ListItem>2046</asp:ListItem>
                            <asp:ListItem>2047</asp:ListItem>
                            <asp:ListItem>2048</asp:ListItem>
                            <asp:ListItem>2049</asp:ListItem>
                            <asp:ListItem>2050</asp:ListItem>
                            <asp:ListItem>2051</asp:ListItem>
                            <asp:ListItem>2052</asp:ListItem>
                            <asp:ListItem>2053</asp:ListItem>
                            <asp:ListItem>2054</asp:ListItem>
                            <asp:ListItem>2055</asp:ListItem>
                            <asp:ListItem>2056</asp:ListItem>
                            <asp:ListItem>2057</asp:ListItem>
                            <asp:ListItem>2058</asp:ListItem>
                            <asp:ListItem>2059</asp:ListItem>
                            <asp:ListItem>2060</asp:ListItem>
                            <asp:ListItem>2061</asp:ListItem>
                            <asp:ListItem>2062</asp:ListItem>
                            <asp:ListItem>2063</asp:ListItem>
                            <asp:ListItem>2064</asp:ListItem>
                            <asp:ListItem>2065</asp:ListItem>
                            <asp:ListItem>2066</asp:ListItem>
                            <asp:ListItem>2067</asp:ListItem>
                            <asp:ListItem>2068</asp:ListItem>
                            <asp:ListItem>2069</asp:ListItem>
                            <asp:ListItem>2070</asp:ListItem>
                            <asp:ListItem>2071</asp:ListItem>
                            <asp:ListItem>2072</asp:ListItem>
                            <asp:ListItem>2073</asp:ListItem>
                            <asp:ListItem>2074</asp:ListItem>
                            <asp:ListItem>2075</asp:ListItem>
                        </asp:DropDownList>
                        <span id="SpanGoalYearReq" class="spnRequiredField" runat="server">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlGoalYear"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please Select Goal Year"></asp:RequiredFieldValidator>
                    </td>
                    
                    <td class="leftField" id="tdSpouseEOL1" runat="server">
                        <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="Spouse EOL :"></asp:Label>
                    </td>
                    <td class="rightField"  id="tdSpouseEOL2" runat="server">
                        <asp:TextBox ID="txtSpouseEOL" runat="server" AutoCompleteType="Disabled" CssClass="txtField" ></asp:TextBox>
                        <span id="Span4" class="spnRequiredField" runat="server" visible="false">*</span>                           
                        <asp:RangeValidator ID="RangeValidator8"  Display="Dynamic" 
                            SetFocusOnError="True" Type="Double" ErrorMessage="Value should be between 30 to 150"
                            MinimumValue="0" MaximumValue="150" ControlToValidate="txtSpouseEOL" 
                            runat="server"></asp:RangeValidator>
                        <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtSpouseEOL"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please enter some value"></asp:RequiredFieldValidator>
                    </td>
                  
                </tr>
                
                <tr>                
                  <td class="leftField">
                <asp:Label ID="lblUseMFBasedGoal" runat="server" CssClass="FieldName" Text="Use MF Based Goal Planning:"></asp:Label>
                </td>                
                  <td class="rightField">
                    <asp:RadioButton ID="rdoMFBasedGoalYes" Text="Yes" runat="server" GroupName="YesNo" 
                            Class="cmbField" 
                            oncheckedchanged="rdoMFBasedGoalYes_CheckedChanged"  AutoPostBack="True"/>              
                    <asp:RadioButton ID="rdoMFBasedGoalNo" Text="No" runat="server" GroupName="YesNo"  
                            Class="cmbField" Checked="True"
                            oncheckedchanged="rdoMFBasedGoalNo_CheckedChanged"  AutoPostBack="True"/>  
                
                </td>
                
                  <td class="leftField" id="tdPostRetirementReturns1" runat="server">
                        <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="Post Retirement Returns(%) :"></asp:Label>
                    </td>
                  <td class="rightField"  id="tdPostRetirementReturns2" runat="server">
                        <asp:TextBox ID="txtPostRetirementReturns" runat="server" AutoCompleteType="Disabled" CssClass="txtField" ></asp:TextBox>
                        <span id="Span1" class="spnRequiredField" runat="server" visible="false">*</span>                           
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" TargetControlID="txtPostRetirementReturns"
                                            FilterType="Custom, Numbers" ValidChars=".">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RangeValidator ID="RangeValidator5"  Display="Dynamic" CssClass="rfvPCG"
                            SetFocusOnError="True" Type="Double" ErrorMessage="Value  should be in between 0 and 100"
                            MinimumValue="0.000000001" MaximumValue="100" ControlToValidate="txtPostRetirementReturns" 
                            runat="server"></asp:RangeValidator>
                            
                        <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtPostRetirementReturns"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please enter some % value"></asp:RequiredFieldValidator>
                    </td>
                 
                </tr>
                
                <tr id="trExistingInvestmentAllocated" runat="server">
                    <td class="leftField">
                        <asp:Label ID="lblCurrentInvestPurpose" runat="server" CssClass="FieldName" Text="Existing Investment Allocated :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtCurrentInvestPurpose" runat="server" AutoCompleteType="Disabled"
                            CssClass="txtField" MaxLength="15" OnBlur="SetROI();"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="txtCurrentInvestPurpose_E" runat="server" Enabled="True" TargetControlID="txtCurrentInvestPurpose"
                                            FilterType="Custom, Numbers" ValidChars=".">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                        <span id="SpanCurrInvestmentAllocated" class="spnRequiredField" runat="server">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCurrentInvestPurpose"
                            CssClass="txtField" ValidationGroup="btnSave" ErrorMessage="Please enter some amount"></asp:RequiredFieldValidator>
                    </td>
                    
                    <td id="tdExistingInvestBlank" runat="server" colspan="2">
                     &nbsp;&nbsp;&nbsp
                    </td>
                </tr>                              
                
                <tr id="trReturnOnExistingInvestmentAll" runat="server">
                    <td class="leftField">
                        <asp:Label ID="lblRateOfInterstAbove" runat="server" CssClass="FieldName" Text="Expected return on the allocated investment(%) :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtAboveRateOfInterst" runat="server" AutoCompleteType="Disabled"
                            CssClass="txtField" MaxLength="15"></asp:TextBox>
                              <ajaxToolkit:FilteredTextBoxExtender ID="txtAboveRateOfInterst_E" runat="server" Enabled="True" TargetControlID="txtAboveRateOfInterst"              FilterType="Custom, Numbers" ValidChars=".">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                        <span id="SpanReturnOnExistingInvestment" class="spnRequiredField" runat="server">*</span>
                         <asp:RangeValidator ID="RangeValidator1"   Display="Dynamic" 
                            SetFocusOnError="True" Type="Double" ErrorMessage="Value  should not be more than 100"
                            MinimumValue="0" MaximumValue="100" 
                            ControlToValidate="txtAboveRateOfInterst" runat="server"></asp:RangeValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4"    Display="Dynamic" runat="server" ControlToValidate="txtAboveRateOfInterst"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please enter some % value"></asp:RequiredFieldValidator>
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
                        <ajaxToolkit:FilteredTextBoxExtender ID="txtExpRateOfReturn_E" runat="server" Enabled="True" TargetControlID="txtExpRateOfReturn"
                                            FilterType="Custom, Numbers" ValidChars=".">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                         <asp:RangeValidator Display="Dynamic" ID="RangeValidator3" 
                            SetFocusOnError="True" Type="Double" ErrorMessage="Value  should be between 4 to 100"
                            MinimumValue="4" MaximumValue="100" ControlToValidate="txtExpRateOfReturn" ValidationGroup="btnSave"
                            runat="server"></asp:RangeValidator>
                        <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtExpRateOfReturn"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please enter some % value"></asp:RequiredFieldValidator>
                    </td>
                    
                    <td id="tdReturnOnFutureInvestBlank" runat="server" colspan="2">
                     &nbsp;&nbsp;&nbsp
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
                        <asp:TextBox ID="txtInflation" runat="server" AutoCompleteType="Disabled" 
                            CssClass="txtField" ></asp:TextBox>
                            <span id="spnInflation" class="spnRequiredField" runat="server">*</span>                           
                            <asp:RangeValidator ID="RangeValidator4"  Display="Dynamic" 
                            SetFocusOnError="True" Type="Double" ErrorMessage="Inflation value should not less than 4"
                            MinimumValue="4" MaximumValue="100" ControlToValidate="txtInflation" 
                            runat="server"></asp:RangeValidator>
                        <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtInflation"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please enter some % value"></asp:RequiredFieldValidator>
                    </td>
                    
                    <td id="tdInflationBlank" runat="server" colspan="2">
                     &nbsp;&nbsp;&nbsp
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
                        <asp:TextBox ID="txtCorpusToBeLeftBehind" runat="server" AutoCompleteType="Disabled" CssClass="txtField"></asp:TextBox>
                        <span id="spnCorpsToBeLeftBehind" class="spnRequiredField" runat="server">*</span>                           
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True" TargetControlID="txtCorpusToBeLeftBehind"
                                            FilterType="Custom, Numbers" ValidChars=".">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RangeValidator ID="RangeValidator7"  Display="Dynamic" CssClass="rfvPCG"
                            SetFocusOnError="True" Type="Double" ErrorMessage="Value  should be in between 0 and 9999999999"
                            MinimumValue="0" MaximumValue="9999999999" ControlToValidate="txtCorpusToBeLeftBehind" 
                            runat="server"></asp:RangeValidator>
                        <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtCorpusToBeLeftBehind"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please enter some amount"></asp:RequiredFieldValidator>
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
                            TextMode="MultiLine" ></asp:TextBox>
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
                    <td id="Td8" runat="server">
                        <asp:CheckBox ID="chkApprove" runat="server" CssClass="FieldName" Text=" Approved by Customer" />
                        
                        
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
                            OnClick="btnCancel_Click"/>
                        <asp:Button ID="btnSaveAdd" runat="server" CssClass="PCGButton" OnClick="btnSaveAdd_Click"
                            Text="Save" ValidationGroup="btnSave"  />
                        <%--<asp:Button ID="btnNext" runat="server" CssClass="PCGMediumButton" Text="Save & Next" OnClick="btnNext_Click"
                            ValidationGroup="btnSave"  OnClientClick="return validate()"/>--%>
                        <asp:Button ID="btnBackToAddMode" runat="server" CssClass="PCGButton" Text="AddNew"
                             ValidationGroup="btnSave" OnClick="btnBackToAddMode_Click"  OnClientClick="return validate()"/>
                         <asp:Button ID="btnBackToView" runat="server" CssClass="PCGMediumButton" Text="Back To View" OnClick="btnBackToView_Click"/>
                         <asp:Button ID="btnEdit" runat="server" CssClass="PCGButton" Text="Edit" OnClick="btnEdit_Click"/>
                        
                         <asp:Button ID="btnUpdate" runat="server" ValidationGroup="btnSave" CssClass="PCGButton" Text="Update" OnClick="btnUpdate_Click"/>
                        
                        
                    </td>
                </tr>
                <tr id="tdNote" runat="server">
                    <td>
                        <asp:Label ID="lblNoteHeading" runat="server" CssClass="cmbField" style="font-size: small;" Text="Note :"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td id="Td2" class="tdRequiredText" runat="server">
                       <asp:Label ID="trRequiedNote" CssClass="cmbField" style="font-size: small;" runat="server" Text="1)Fields marked with ' * ' are mandatory."></asp:Label>
                       
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNote" runat="server" CssClass="cmbField" style="font-size: small;" Text="2)Expected rate of return is defaulted as per your risk assessment.If risk profile is not complete default risk profile will be 'MODERATE'."></asp:Label>
                    </td>
                </tr>
            </table>
   </asp:Panel>
 
    </telerik:RadPageView>


    <telerik:RadPageView ID="RadPageView2" runat="server">
    <table width="100%">
    <tr>
               <td>
               <asp:Label ID="lblFundingProgressHeading" runat="server" CssClass="HeaderTextBig" Text="Goal Fund/Progress"></asp:Label>
               <hr />
               </td>
    </tr>
    </table>
     <asp:Panel runat="server" ID="pnlNoRecordFoundGoalFundingProgress" width="100%" Visible="false">
      <table width="100%">
       <tr>
                <td align="center">
                    <div id="Div3" class="failure-msg" align="center">
                        No Record Found
                    </div>
                </td>
            </tr>
      </table>
      </asp:Panel>
   
    
    <asp:Panel ID="pnlFundingProgress" runat="server">
    
    <table>
    <%--****************************************************************************--%>
    <tr>
    <td class="leftField">
     <asp:Label id="lblGoalName" Text="Goal:"  CssClass="FieldName" runat="server"></asp:Label>
    </td>
    <td class="rightField">
     <asp:TextBox ID="txtGoalName" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>     
    </td>
    <td class="leftField" valign="middle">
    <asp:Image ID="imgGoalImage" ImageAlign="Left" runat="server" />
    <span id="span9" class="spnRequiredField" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span> 
    
    <asp:Label id="lblGoalStatus" Text="Goal Achievable ?:" CssClass="FieldName" runat="server"></asp:Label>
    </td>
    <td class="rightField">
     <asp:Image ID="imgGoalFundIndicator" ImageAlign="Left" runat="server" />
    </td>
    
   <td class="leftField">
     <asp:Label id="lblReturnsXIRR" Text="Returns (XIRR)(%):"  CssClass="FieldName" runat="server"></asp:Label>
    </td>
    <td class="rightField">
     <asp:TextBox ID="txtReturnsXIRR" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
    </td>
    </tr>
   <%-- ****************************************************************************--%>
    <tr>
    <td class="leftField">
     <asp:Label id="lblStartDate" Text="Start Date:"  CssClass="FieldName" runat="server"></asp:Label>
     </td>
    <td class="rightField">
     <asp:TextBox ID="txtStartDate" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
     </td>
     
    <td class="leftField">
     <asp:Label id="lblTargetDate" Text="Target Date:"  CssClass="FieldName" runat="server"></asp:Label>
    </td>
    <td class="rightField">
     <asp:TextBox ID="txtTargetDate" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
    </td>
     <td class="leftField">
     <asp:Label id="lblProjectedCompleteYear" Text="Likely Tareget Date:"  CssClass="FieldName" runat="server"></asp:Label>
    </td>
    <td class="rightField">
     <asp:TextBox ID="txtProjectedCompleteYear" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
    </td>   
     
    </tr>
     <%-- ****************************************************************************--%>
      <tr>
    <td class="leftField">
     <asp:Label id="lblEstmdTimeToReachGoal" Text="Time Gap from Target Date:"  CssClass="FieldName" runat="server"></asp:Label>
     </td>
    <td class="rightField">
     <asp:TextBox ID="txtEstmdTimeToReachGoal" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
     </td>
       <td class="leftField">
     <asp:Label id="lblTenureCompleted" Text="Tenure Completed (Years):"  CssClass="FieldName" runat="server"></asp:Label>
     </td>
    <td class="rightField">
     <asp:TextBox ID="txtTenureCompleted" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
     </td>
     
    <td class="leftField">
     <asp:Label id="lblBalanceTenor" Text="Balance Tenure(Years):"  CssClass="FieldName" runat="server"></asp:Label>
    </td>
    <td class="rightField">
     <asp:TextBox ID="txtBalanceTenor" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
    </td>
      </tr>
     
 
     <%-- ****************************************************************************--%>
      
    <tr>
      <td class="leftField">
     <asp:Label id="lblProjectedValueOnGoalDate" Text="Proj. Value On Goal Date:"  CssClass="FieldName" runat="server"></asp:Label>
    </td>
    <td class="rightField">
     <asp:TextBox ID="txtProjectedValueOnGoalDate" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
    </td>
    
    <td class="leftField">
     <asp:Label id="lblProjectedGap" Text="Projected Gap:"  CssClass="FieldName" runat="server"></asp:Label>
    </td>
    <td class="rightField">
     <asp:TextBox ID="txtProjectedGap" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
    </td> 
    <td class="leftField">
     <asp:Label id="lblMonthlyContribution" Text="Monthly Contribution:"  CssClass="FieldName" runat="server"></asp:Label>
    </td>
    <td class="rightField">
     <asp:TextBox ID="txtMonthlyContribution" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
    </td>    
    </tr>
    <%-- ****************************************************************************--%>
    <tr>
    <td class="leftField">
     <asp:Label id="lblCostAtBeginning" Text="Cost At Beginning:" CssClass="FieldName" runat="server"></asp:Label>
     </td>
    <td class="rightField">
     <asp:TextBox ID="txtCostAtBeginning" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
     </td>
     
    <td class="leftField">
     <asp:Label id="lblGoalAmount" Text="Goal Amount:"  CssClass="FieldName" runat="server"></asp:Label>
    </td>
    <td class="rightField">
     <asp:TextBox ID="txtGoalAmount" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
    </td> 
    <td class="leftField">
     <asp:Label id="lblAmountInvestedTillDate" Text="Amount Invested Till Date:"  CssClass="FieldName" runat="server"></asp:Label>
     </td>
    <td class="rightField">
     <asp:TextBox ID="txtAmountInvested" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
     </td>
     
    </tr>
    <%-- ****************************************************************************--%>
      <tr>
      <td class="leftField">
     <asp:Label id="lblValueOfCurrentGoal" Text="Value of Current Goal:"  CssClass="FieldName" runat="server"></asp:Label>
    </td>
    <td class="rightField">
     <asp:TextBox ID="txtValueOfCurrentGoal" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
    </td>
      <td class="leftField">
     <asp:Label id="lblAdditionalInvestmentsRequired" Text="Additional Invest. Req/Month:"  CssClass="FieldName" runat="server"></asp:Label>
     </td>
    <td class="rightField">
     <asp:TextBox ID="txtAdditionalInvestmentsRequired" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
     </td>
     
    <td class="leftField">
     <asp:Label id="lblAdditionalInvestments" Text="Additional Invest. Req/Year:"  CssClass="FieldName" runat="server"></asp:Label>
    </td>
    <td class="rightField">
     <asp:TextBox ID="txtAdditionalInvestments" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
    </td>  
       
    </tr>
    <%-- ****************************************************************************--%>
    <tr>
 
    
     
        
    </tr>
    <tr>
    <td colspan="6">
   
    </td>
    </tr>
   </table>
   </asp:Panel>
     <br />  
   
     <asp:Panel runat="server" ID="pnlDocuments">
       <table ID="tblDocuments" runat="server"  Width="100%">
       <tr>
                            <td colspan="2" class="HeaderTextSmall">
                                <b>Fund from Existing MF Investments</b>
                            </td>
                        </tr>
      </table>
       <telerik:RadGrid ID="RadGrid1" runat="server" CssClass="RadGrid" GridLines="None" Width="100%"
        AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false"
        ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
        AllowAutomaticUpdates="false" Skin="Telerik" OnItemDataBound="RadGrid1_ItemDataBound" OnDeleteCommand="RadGrid1_DeleteCommand"  OnInsertCommand="RadGrid1_ItemInserted"
        OnItemUpdated="RadGrid1_ItemUpdated" OnItemCommand="RadGrid1_ItemCommand"
        OnPreRender="RadGrid1_PreRender">
        <MasterTableView CommandItemDisplay="Top"  CommandItemSettings-ShowRefreshButton="false" DataKeyNames="SchemeCode,OtherGoalAllocation">
            <Columns>
                <telerik:GridEditCommandColumn>
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn UniqueName="SchemeName" HeaderText="Scheme" DataField="SchemeName">
                    <%--<HeaderStyle ForeColor="Silver"></HeaderStyle>--%>
                   <%-- <ItemStyle ForeColor="Gray" />--%>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="InvestedAmount" HeaderText="Invested Amount" DataField="InvestedAmount" DataFormatString="{0:C2}">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="Units" HeaderText="Units" DataField="Units" DataFormatString="{0:C2}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CurrentValue" HeaderText="Current Value" DataField="CurrentValue" DataFormatString="{0:C2}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="ReturnsXIRR" HeaderText="Returns (XIRR)(%)" DataField="ReturnsXIRR">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="ProjectedAmount" HeaderText="Projected amount in goal year" DataField="ProjectedAmount" DataFormatString="{0:n2}">
                </telerik:GridBoundColumn>
                 <telerik:GridButtonColumn CommandName="Delete" Text="Delete" ConfirmText="Are you sure you want to Remove this Record?"  UniqueName="column">
                </telerik:GridButtonColumn>
                <%--<telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="column">
                </telerik:GridButtonColumn>--%>
            </Columns>
          <EditFormSettings EditFormType="Template">
                <FormTemplate>
                    <table id="Table2" cellspacing="2" cellpadding="1" width="100%" border="0" rules="none"
                        style="border-collapse: collapse; background: white;">
                        <tr class="EditFormHeader">
                            <td colspan="2" style="font-size: small">
                                <b>MF Investment Funding</b>
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
                                    <tr runat="server" id="trSchemeDDL">
                                     <td align="right">
                                            <asp:Label id="Label133" Text="Scheme:" CssClass="FieldName" runat="server" >
                                        </asp:Label> 
                                        </td>
                                    <td>
                               <asp:DropDownList ID="ddlPickScheme" runat="server" CssClass="cmbField">                                    
                                </asp:DropDownList>
                            </td>
                            </tr>
                                    <tr runat="server" id="trSchemeTextBox" >
                                        <td align="right">
                                            <asp:Label id="Label13" Text="Scheme:" CssClass="FieldName" runat="server">
                                        </asp:Label> 
                                        </td>
                                        <td>
                                        <asp:Label id="lblGoalName" Text='<%# Bind("SchemeName") %>'  CssClass="FieldName" runat="server">
                                        </asp:Label>
                                           
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="right">
                                        <asp:Label id="Label14" Text="Units:" CssClass="FieldName" runat="server">
                                        </asp:Label>
                                            
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUnits" runat="server" CssClass="txtField" Text='<%# Bind("Units") %>'  Enabled="false" TabIndex="2" >
                                            </asp:TextBox>
                                        </td>
                                        
                                         <td align="right">
                                        <asp:Label id="Label11" Text="Amount Available:" CssClass="FieldName" runat="server">
                                        </asp:Label>
                                            
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAmtAvailable" runat="server" CssClass="txtField" Text='<%# Bind("AvailableAmount") %>'  Enabled="false" TabIndex="2" >
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                          <asp:Label id="Label15" Text="Current Value:" CssClass="FieldName" runat="server">
                                        </asp:Label> 
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCurrentValue" CssClass="txtField" runat="server" Text='<%# Bind("CurrentValue") %>'  Enabled="false" TabIndex="3">
                                            </asp:TextBox>
                                        </td>
                                         <td align="right">
                                          <asp:Label id="Label2" Text="Amount Marked for the Goal:" CssClass="FieldName" runat="server">
                                        </asp:Label> 
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtInvestedAmt" CssClass="txtField" runat="server" Text='<%# Bind("InvestedAmount") %>'  Enabled="false" TabIndex="3">
                                            </asp:TextBox>
                                        </td>
                                    </tr>      
                                     <tr>
                                        <td align="right">
                                           <asp:Label id="Label16" Text="Total Goal Allocation(%):" CssClass="FieldName" runat="server">
                                        </asp:Label> 
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="txtField" Enabled="false" Text='<%# Bind("AllocationEntry") %>' TabIndex="3">
                                            </asp:TextBox>
                                        </td>
                                         <td align="right">
                                        <asp:Label id="Label17" Text="Current Goal Allocation(%):" CssClass="FieldName" runat="server" Enabled="false">
                                        </asp:Label>
                                          
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox4" runat="server" CssClass="txtField" Text='<%# Bind("CurrentGoalAllocation") %>' TabIndex="3" > 
                                            </asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="TextBox4_TextBoxWatermarkExtender" runat="server"
                                               Enabled="True" TargetControlID="TextBox4" WatermarkText="Please enter here..">
                                               </cc1:TextBoxWatermarkExtender>
                                        </td>
                                    </tr>  
                                   
                                    <tr>
                                        <td align="right">
                                         <asp:Label id="Label18" Text="Other Goal Allocation(%):" CssClass="FieldName" runat="server">
                                        </asp:Label> 
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSchemeAllocationPer" CssClass="txtField" runat="server" Text='<%# Bind("OtherGoalAllocation") %>'  Enabled="false" TabIndex="1">
                                            </asp:TextBox>
                                        </td>
                                        <td align="right">
                                         <asp:Label id="Label19" Text="Available Allocation(%):" CssClass="FieldName" runat="server">
                                        </asp:Label>  
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox5" runat="server" CssClass="txtField"  Enabled="false" Text='<%# Bind("AvailableAllocation") %>' TabIndex="1">
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
                                <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                    runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                </asp:Button>&nbsp;
    <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton" CommandName="Cancel"></asp:Button>
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
    
    
  
    <asp:Panel runat="server" ID="pnlMFFunding">
    <hr />
   <table ID="Table1" runat="server" Width="100%">
        <tr>
                            <td colspan="2" class="HeaderTextSmall">
                                <b>Fund from Future MF Savings</b>
                            </td>
                             
                        </tr>
                        </table>
      <telerik:RadGrid ID="RadGrid2" runat="server" CssClass="RadGrid" GridLines="None"
        AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false"
        ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
        AllowAutomaticUpdates="false" Skin="Telerik" OnItemDataBound="RadGrid2_ItemDataBound" OnInsertCommand="RadGrid2_ItemInserted" OnDeleteCommand="RadGrid2_DeleteCommand"
        OnItemCommand="RadGrid2_ItemCommand" >
        <MasterTableView CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="false" DataKeyNames="SIPId,TotalSIPamount">
            <Columns>
                <telerik:GridEditCommandColumn>
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn UniqueName="SchemeName" HeaderText="Scheme" DataField="SchemeName">
                    <%--<HeaderStyle ForeColor="Silver"></HeaderStyle>--%>
                   <%-- <ItemStyle ForeColor="Gray" />--%>
                </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn UniqueName="AvailableAllocation" HeaderText="SIP Amount Available" DataField="AvailableAllocation">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="SIPInvestedAmount" HeaderText="SIP Amount Invested" DataField="SIPInvestedAmount">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="SIPProjectedAmount" HeaderText="SIP Projected Amount" DataField="SIPProjectedAmount">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridButtonColumn CommandName="Delete" Text="Delete" ConfirmText="Are you sure you want to Remove this Record?"  UniqueName="column">
                </telerik:GridButtonColumn>
               
                <%--<telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="column">
                </telerik:GridButtonColumn>--%>
            </Columns>
            <EditFormSettings EditFormType="Template">
                <FormTemplate>
                    <table id="Table2" cellspacing="2" cellpadding="1" width="100%" border="0" rules="none"
                        style="border-collapse: collapse; background: white;">
                        <tr class="EditFormHeader">
                            <td colspan="2" style="font-size: small">
                                <b>Monthly SIP MF Funding</b>
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
                                            <asp:Label id="Label20" Text="Scheme-Amount-SIP Date:" CssClass="FieldName" runat="server">
                                        </asp:Label> 
                                        </td>
                                    <td>
                               <asp:DropDownList ID="ddlPickSIPScheme" runat="server" CssClass="cmbField">                                    
                                </asp:DropDownList>
                            </td>
                            </tr>
                                    <tr runat="server" id="trSchemeNameText">
                                        <td align="right">
                                            <asp:Label id="Label21" Text="Scheme:" CssClass="FieldName" runat="server">
                                        </asp:Label> 
                                        </td>
                                        <td>
                                        <asp:Label id="lblSchemeName" Text='<%# Bind("SchemeName") %>'  CssClass="FieldName" runat="server">
                                        </asp:Label>
                                           
                                        </td>
                                         <td align="right">
                                        <asp:Label id="Label26" Text="SIP Frequecny:" CssClass="FieldName" runat="server" Enabled="false">
                                        </asp:Label>
                                          
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSIPFrequency" runat="server" Enabled="false" CssClass="txtField" Text='<%# Bind("SIPFrequecny") %>' TabIndex="3">
                                            </asp:TextBox>
                                        </td>
                                    </tr>                                  
                                      
                                    <tr>
                                        <td align="right">
                                        <asp:Label id="Label22" Text="Current Goal Invested Amount:" CssClass="FieldName" runat="server" Enabled="false">
                                        </asp:Label>
                                          
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox3" runat="server" CssClass="txtField" Text='<%# Bind("SIPInvestedAmount") %>' TabIndex="3">
                                            </asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="TextBox3_TextBoxWatermarkExtender" runat="server"
                                               Enabled="True" TargetControlID="TextBox3" WatermarkText="Please enter SIP Amt.">
                                               </cc1:TextBoxWatermarkExtender>
                                        </td>
                                        <td align="right">
                                         <asp:Label id="Label23" Text="Other Goal Invested Amount:" CssClass="FieldName" runat="server">
                                        </asp:Label> 
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOtherSchemeAllocationPer" CssClass="txtField" runat="server" Text='<%# Bind("OtherGoalAllocation") %>'  Enabled="false" TabIndex="1">
                                            </asp:TextBox>
                                        </td>
                                    </tr> 
                                    <tr>
                                        <td align="right">
                                         <asp:Label id="Label24" Text="Available Amount:" CssClass="FieldName" runat="server">
                                        </asp:Label>  
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="txtField"  Enabled="false" Text='<%# Bind("AvailableAllocation") %>' TabIndex="1">
                                            </asp:TextBox>
                                        </td>
                                         <td align="right">
                                         <asp:Label id="Label4" Text="Total SIP Amount:" CssClass="FieldName" runat="server">
                                        </asp:Label>  
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTotalSIPAmount" runat="server" CssClass="txtField"  Enabled="false" Text='<%# Bind("TotalSIPamount") %>' TabIndex="1">
                                            </asp:TextBox>
                                        </td>
                                    </tr> 
                                     <tr>
                                        <td align="right">
                                         <asp:Label id="Label12" Text="SIP Start Date:" CssClass="FieldName" runat="server">
                                        </asp:Label>  
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSIPStartDate" runat="server" CssClass="txtField"  Enabled="false" Text='<%# Bind("SIPStartDate") %>' TabIndex="1">
                                            </asp:TextBox>
                                        </td>
                                         <td align="right">
                                         <asp:Label id="Label25" Text="SIP End Date:" CssClass="FieldName" runat="server">
                                        </asp:Label>  
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSIPEndDate" runat="server" CssClass="txtField"  Enabled="false" Text='<%# Bind("SIPEndDate") %>' TabIndex="1">
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
                                <asp:Button ID="Button3" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                    runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                </asp:Button>&nbsp;
      <asp:Button ID="Button4" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton" CommandName="Cancel">
      </asp:Button>
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
 
  
    <telerik:RadPageView ID="RadPageView4" runat="server">
 
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
                                <asp:DropDownList ID="ddlModelPortFolio" runat="server" CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlModelPortFolio_OnSelectedIndexChanged"></asp:DropDownList>
                                </td>
                                </tr>
                                </table>                             
        <br />                                
        <table id="tableGrid" runat="server" class="TableBackground" width="100%">

    <tr>
        <td>
    <telerik:RadGrid ID="RadGrid3" runat="server" Skin="Telerik" CssClass="RadGrid" GridLines="None" AllowPaging="True" 
    PageSize="20" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true" AllowAutomaticDeletes="false" 
    AllowAutomaticInserts="false" 
    AllowAutomaticUpdates="false" HorizontalAlign="NotSet">
        <MasterTableView>
            <Columns>
                <%--<telerik:GridClientSelectColumn UniqueName="SelectColumn"/>--%>               
                <telerik:GridBoundColumn  DataField="PASP_SchemePlanName"  HeaderText="Scheme Name" UniqueName="PASP_SchemePlanName" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="AMFMPD_AllocationPercentage"  HeaderText="Weightage" UniqueName="AMFMPD_AllocationPercentage">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                      
                <telerik:GridBoundColumn  DataField="AMFMPD_AddedOn"  HeaderText="Started Date" UniqueName="AMFMPD_AddedOn">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="AMFMPD_SchemeDescription"  HeaderText="Description" UniqueName="AMFMPD_SchemeDescription">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
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
    
     <asp:Panel runat="server" ID="pnlModelPortfolioNoRecoredFound" width="100%" Visible="false">
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
