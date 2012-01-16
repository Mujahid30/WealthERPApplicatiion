<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerAdvancedGoalSetup.ascx.cs" Inherits="WealthERP.FP.CustomerAdvancedGoalSetup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>




<script type="text/javascript">

    function MFBasedGoalSelection(value) {
        
           if(value=='rdoMFBasedGoalYes')
               {
                    document.getElementById("<%= trExistingInvestmentAllocated.ClientID %>").style.display = 'none';
                    document.getElementById("<%= trReturnOnExistingInvestmentAll.ClientID %>").style.display = 'none';
                   
                }
        
       
    }






    //
    function SetROI() {
        if (document.getElementById('<%=txtCurrentInvestPurpose.ClientID %>').value > 0)
            document.getElementById('<%=txtAboveRateOfInterst.ClientID %>').value = "8.5";

    };


    function validate() {        
        var goalCost = document.getElementById("<%=txtGoalCostToday.ClientID %>").value;
        var RetirementCorps = "-";
        if (document.getElementById("<%=txtROIFutureInvest.ClientID %>") != null)
            RetirementCorps = document.getElementById("<%=txtROIFutureInvest.ClientID %>").value;



//        if (parseInt(CurrentInvestPurpose) == 0 && parseInt(aboveRateOfInterest) > 0) {
//            alert("Fill correct investment amount")
//            return false;
        //        }
        if (document.getElementById('<%=ddlGoalType.ClientID %>').value == "0") {
            alert("Please Select a Goal")
            document.getElementById('<%=ddlGoalType.ClientID %>').focus();
            return false;
        }

//        if (document.getElementById("<%=txtROIFutureInvest.ClientID %>") == "") {
//            alert("Retirement Corpus(%) cannot be empty.")
//            return false;
//        }

        if (document.getElementById('<%=txtInflation.ClientID %>').value == "") {
            alert("Inflation(%) cannot be empty.")
            document.getElementById('<%=txtInflation.ClientID %>').focus();
            return false;
        }

        if ((document.getElementById('<%=txtCurrentInvestPurpose.ClientID %>').value == "0") && (document.getElementById('<%=txtAboveRateOfInterst.ClientID %>').value > "0")) {
            alert("You can not expect return when your existing allocated amount is not sufficient");
            document.getElementById('<%=txtCurrentInvestPurpose.ClientID %>').focus();
            return false;
        }

        if (document.getElementById('<%=txtExpRateOfReturn.ClientID %>').value == "") {
            alert("Expected Rate of Return on new investment(%) cannot be empty.")
            document.getElementById('<%=txtExpRateOfReturn.ClientID %>').focus();
            return false;
        }

        if (document.getElementById('<%=txtGoalCostToday.ClientID %>').value == "") {
            alert("Cost today cannot be empty.")
            document.getElementById('<%=txtGoalCostToday.ClientID %>').focus();
            return false;
        }
        else if (document.getElementById('<%=txtGoalCostToday.ClientID %>').value == "0") {
            alert("Cost today cannot be 0.")
            document.getElementById('<%=txtGoalCostToday.ClientID %>').focus();
            return false;
        }
        if (RetirementCorps == "") {

            alert("Retirement Corpus(%) cannot be empty.")
            return false;
        }
        else if (parseInt(RetirementCorps) == 0) {
            alert("Return on retirement corpus(%) should be greater than 0 ")
            return false;
        }




        if (goalCost == "") {
            document.getElementById("<%=txtGoalCostToday.ClientID %>").focus();
            alert("Please enter Goal Cost Today");
            return false;
        }
        else
            return true;

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



    function onCALShown() {
        var cal = $find("calendar1");

        //Setting the default mode to month

        cal._switchMode("years", true);


        //Iterate every month Item and attach click event to it

    };

    //Numeric Test
    function IsNumeric() //  check for valid numeric strings
    {
        alert("here");
        var Stringvalue = document.getElementById("<%= txtGoalCostToday.ClientID %>").value;
        if (!/\D/.test(Stringvalue)) return true; //IF NUMBER
        else if (/^\d+\.\d+$/.test(Stringvalue)) return true; //IF A DECIMAL NUMBER HAVING AN INTEGER ON EITHER SIDE OF THE DOT(.)
        alert("Warning! - Must be a Number or Numeric value");
    };
   
   
    
    
    
</script>

 <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
 
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
                         <telerik:radcombobox id="ddlGoalType" OnSelectedIndexChanged="ddlGoalType_OnSelectedIndexChange" CssClass="cmbField" runat="server" Width="150px"  EnableEmbeddedSkins=false skin="Telerik" allowcustomtext="true" AutoPostBack="true">    
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
                        <span id="spanGoalType" class="spnRequiredField" runat="server">*</span>                      
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
                </tr>
                
                <tr id="trGoalDesc" runat="server">
                    <td id="Td2333" class="leftField" runat="server">
                        <asp:Label ID="lblGoalDescription" runat="server" CssClass="FieldName" Text="Goal Description :"></asp:Label>
                    </td>
                    <td id="Td333444" class="rightField" runat="server">
                        <asp:TextBox ID="txtGoalDescription" runat="server" AutoCompleteType="Disabled" CssClass="txtField"></asp:TextBox>
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
                            SetFocusOnError="True" Type="Double" ErrorMessage="Value  should not be more than 100"
                            MinimumValue="0" MaximumValue="100" ControlToValidate="txtExpRateOfReturn" 
                            runat="server"></asp:RangeValidator>
                        <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtExpRateOfReturn"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please enter some % value"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                
                <tr id="trROIFutureInvestment" runat="server">
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
                </tr>               
                
                <tr id="trCustomerAge" runat="server">
                    <td class="leftField">
                        <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Customer Age :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtCustomerAge" runat="server" AutoCompleteType="Disabled" CssClass="txtField"></asp:TextBox>
                        <span id="Span8" class="spnRequiredField" runat="server">*</span>                           
                        <asp:RangeValidator ID="RangeValidator12"  Display="Dynamic" 
                            SetFocusOnError="True" Type="Double" ErrorMessage="Value should be between 18 to 150"
                            MinimumValue="18" MaximumValue="150" ControlToValidate="txtCustomerAge" 
                            runat="server"></asp:RangeValidator>
                        <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtCustomerAge"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please enter some value"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                
                <tr id="trSpouseAge" runat="server">
                    <td class="leftField">
                        <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Spouse Age :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtSpouseAge" runat="server" AutoCompleteType="Disabled" CssClass="txtField"></asp:TextBox>
                        <span id="Span7" class="spnRequiredField" runat="server">*</span>                           
                        <asp:RangeValidator ID="RangeValidator11"  Display="Dynamic" 
                            SetFocusOnError="True" Type="Double" ErrorMessage="Value should be between 18 to 150"
                            MinimumValue="0" MaximumValue="150" ControlToValidate="txtSpouseAge" 
                            runat="server"></asp:RangeValidator>
                        <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtSpouseAge"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please enter some value"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                
                <tr id="trRetirementAge" runat="server">
                    <td class="leftField">
                        <asp:Label ID="Label5" runat="server" CssClass="FieldName" Text="Retirement Age :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtRetirementAge" runat="server" AutoCompleteType="Disabled" CssClass="txtField"></asp:TextBox>
                        <span id="Span6" class="spnRequiredField" runat="server">*</span>                           
                        <asp:RangeValidator ID="RangeValidator10"  Display="Dynamic" 
                            SetFocusOnError="True" Type="Double" ErrorMessage="Value should be between 30 to 65"
                            MinimumValue="30" MaximumValue="65" ControlToValidate="txtRetirementAge" 
                            runat="server"></asp:RangeValidator>
                        <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtRetirementAge"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please enter some value"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                
                <tr id="trCustomerEOL" runat="server">
                    <td class="leftField">
                        <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Customer EOL :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtCustomerEOL" runat="server" AutoCompleteType="Disabled" CssClass="txtField"></asp:TextBox>
                        <span id="Span5" class="spnRequiredField" runat="server">*</span>                           
                        <asp:RangeValidator ID="RangeValidator9"  Display="Dynamic" 
                            SetFocusOnError="True" Type="Double" ErrorMessage="Value should be between 30 to 150"
                            MinimumValue="30" MaximumValue="150" ControlToValidate="txtCustomerEOL" 
                            runat="server"></asp:RangeValidator>
                        <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtCustomerEOL"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please enter some value"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                
                <tr id="trSpouseEOL" runat="server">
                    <td class="leftField">
                        <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="Spouse EOL :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtSpouseEOL" runat="server" AutoCompleteType="Disabled" CssClass="txtField"></asp:TextBox>
                        <span id="Span4" class="spnRequiredField" runat="server">*</span>                           
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
                        <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Inflation(%) :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtInflation" runat="server" AutoCompleteType="Disabled" 
                            CssClass="txtField" ></asp:TextBox>
                            <span id="spnInflation" class="spnRequiredField" runat="server">*</span>                           
                            <asp:RangeValidator ID="RangeValidator4"  Display="Dynamic" 
                            SetFocusOnError="True" Type="Double" ErrorMessage="Value should not be more than 100"
                            MinimumValue="0" MaximumValue="100" ControlToValidate="txtInflation" 
                            runat="server"></asp:RangeValidator>
                        <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtInflation"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please enter some % value"></asp:RequiredFieldValidator>
                    </td>
                </tr>                                
               
                <tr id="trPostRetirementReturns" runat="server">
                    <td class="leftField">
                        <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="Post Retirement Returns(%) :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtPostRetirementReturns" runat="server" AutoCompleteType="Disabled" CssClass="txtField"></asp:TextBox>
                        <span id="Span1" class="spnRequiredField" runat="server">*</span>                           
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
                
                <tr id="trReturnOnNewInvestments" runat="server">
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
                </tr> 
                
                <tr id="trCorpusToBeLeftBehind" runat="server">
                    <td class="leftField">
                        <asp:Label ID="Label10" runat="server" CssClass="FieldName" Text="Corpus To Be Left Behind :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtCorpusToBeLeftBehind" runat="server" AutoCompleteType="Disabled" CssClass="txtField"></asp:TextBox>
                        <span id="Span3" class="spnRequiredField" runat="server">*</span>                           
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True" TargetControlID="txtCorpusToBeLeftBehind"
                                            FilterType="Custom, Numbers" ValidChars=".">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RangeValidator ID="RangeValidator7"  Display="Dynamic" CssClass="rfvPCG"
                            SetFocusOnError="True" Type="Double" ErrorMessage="Value  should be in between 0 and 9999999999"
                            MinimumValue="0" MaximumValue="9999999999" ControlToValidate="txtReturnOnNewInvestments" 
                            runat="server"></asp:RangeValidator>
                        <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtCorpusToBeLeftBehind"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please enter some % value"></asp:RequiredFieldValidator>
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
                </tr>
                
            </table>
         
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
                            OnClick="btnCancel_Click" OnClientClick="return validate()"/>
                        <asp:Button ID="btnSaveAdd" runat="server" CssClass="PCGButton" OnClick="btnSaveAdd_Click"
                            Text="Save" ValidationGroup="btnSave" OnClientClick="return validate()" />
                        <%--<asp:Button ID="btnNext" runat="server" CssClass="PCGMediumButton" Text="Save & Next" OnClick="btnNext_Click"
                            ValidationGroup="btnSave"  OnClientClick="return validate()"/>--%>
                        <asp:Button ID="btnBackToAddMode" runat="server" CssClass="PCGButton" Text="AddNew"
                             ValidationGroup="btnSave" OnClick="btnBackToAddMode_Click"  OnClientClick="return validate()"/>
                         <asp:Button ID="btnBackToView" runat="server" CssClass="PCGMediumButton" Text="Back To View" OnClick="btnBackToView_Click"/>
                         <asp:Button ID="btnEdit" runat="server" CssClass="PCGButton" Text="Edit" OnClick="btnEdit_Click"/>
                        
                         <asp:Button ID="btnUpdate" runat="server" CssClass="PCGButton" Text="Update" OnClick="btnUpdate_Click" OnClientClick="return validate()"/>
                        
                        
                    </td>
                </tr>
                <tr id="tdNote" runat="server">
                    <td>
                        <asp:Label ID="Label2" runat="server" CssClass="cmbField" style="font-size: small;" Text="Note :"></asp:Label>
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