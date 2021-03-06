﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerFPGoalSetup.ascx.cs" Inherits="WealthERP.FP.CustomerFPGoalSetup" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<script type="text/javascript">


//    function ShowHideControls() {
//        var ddlGoalType = document.getElementById("<%=ddlGoalType.ClientID %>").value;
//        if ((ddlGoalType == 'ED') || (ddlGoalType == 'MR'))
//            document.getElementById("<%= trPickChild.ClientID %>").style.display = 'block';
//    }
//    
//    function ShowTrFrequency() {
//        var ddlOccurrence = document.getElementById("<%=ddlOccurrence.ClientID %>").value;
//        if (ddlOccurrence == 'Recurring') 
//              document.getElementById("<%= trFrequency.ClientID %>").style.display = 'block';
//    }
</script>
<script language="javascript" type="text/javascript">
    function showmessageNRT() {

        var bool = window.confirm('Are you sure you want to update Goal Setup,Funding will get delete..!!');

        if (bool) {
            document.getElementById("ctrl_CustomerFPGoalSetup_hdnMsgValue").value = 1;
            document.getElementById("ctrl_CustomerFPGoalSetup_hiddenNRTUpdate").click();
            return false;
        }
        else {
            document.getElementById("ctrl_CustomerFPGoalSetup_hdnMsgValue").value = 0;
            document.getElementById("ctrl_CustomerFPGoalSetup_hiddenNRTUpdate").click();
            return true;

        }
    }
</script>
<script language="javascript" type="text/javascript">
    function showmessageRT() {

        var bool = window.confirm('Are you sure you want to update Goal Setup,Funding will get delete..!!');

        if (bool) {
            document.getElementById("ctrl_CustomerFPGoalSetup_hdnMsgValueRT").value = 1;
            document.getElementById("ctrl_CustomerFPGoalSetup_hiddenRTUpdate").click();
            return false;
        }
        else {
            document.getElementById("ctrl_CustomerFPGoalSetup_hdnMsgValueRT").value = 0;
            document.getElementById("ctrl_CustomerFPGoalSetup_hiddenRTUpdate").click();
            return true;

        }
    }
</script>


<asp:Label ID="headertitle" runat="server" CssClass="HeaderTextBig" Text="Goal Setup"></asp:Label>
<hr />


<table width="100%" cellspacing="0" cellpadding="0">
<tr>
<td>
<telerik:RadToolBar ID="aplToolBar" runat="server" Skin="Telerik" 
        EnableEmbeddedSkins="false" EnableShadows="true" EnableRoundedCorners="true"
    Width="100%" onbuttonclick="aplToolBar_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ID="btnEdit" runat="server" Text="Edit" Value="Edit" ImageUrl="~/Images/Telerik/EditButton.gif"
            ImagePosition="Left" ToolTip="Edit">            
        </telerik:RadToolBarButton>
        
    </Items>
</telerik:RadToolBar>
</td>
</tr>
</table>
<hr />
<table width="100%" cellspacing="0" cellpadding="0">
    <tr>
        <td align="center" colspan="2">
            <div id="msgUpdateStatus" runat="server" class="success-msg" align="center" visible="false">
                Record Updated Successfully
            </div>
        </td>
    </tr>
    
 </table>

<table width="100%" cellspacing="0" cellpadding="0">
    <tr>
        <td align="center" colspan="2">
            <div id="msgRecordStatus" runat="server" class="success-msg" align="center" visible="false">
                Record saved Successfully
            </div>
        </td>
    </tr>
    
 </table> 
<%--<table width="50%" cellspacing="0" cellpadding="0">
 <tr>
    <td style="width:20%;">
    </td>
    <td style="width:20%">
     <asp:RadioButton ID="rtbNonRT" Text="Non Retirement" runat="server" Class="FieldName" Checked="true" GroupName="GaolType" onClick="return ShowHideGaolType()"/>
     </td>
    <td style="width:10%">
     <asp:RadioButton ID="rtbRT" runat="server" Text="Retirement" Class="FieldName" GroupName="GaolType" onClick="return ShowHideGaolType()"/>
    </td>
 </tr>
 <tr>
 <td colspan="2">
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 </td>
 </tr>
</table>
--%>

<asp:Panel ID="PnlGoalTypes" runat="server">
<table width="50%">
 <tr>
                    <td style="width:10%;">
                    </td>
                    <td class="leftField" valign="top" style="width:20%;">
                        <asp:Label ID="lblGoalbjective" runat="server" CssClass="FieldName" Text="Goal Objective :"></asp:Label>
                    </td>
                    <td class="rightField" style="width:20%;">
                                      
                         <asp:DropDownList ID="ddlGoalType" runat="server" CssClass="cmbField" 
                             onselectedindexchanged="ddlGoalType_SelectedIndexChanged" AutoPostBack="true">                              
                         </asp:DropDownList>
                        
                   
                        <span id="spanGoalType" class="spnRequiredField" runat="server">*</span> 
                        <%--<asp:RequiredFieldValidator id="RequiredFieldValidator3" ValidationGroup="btnSave" ControlToValidate="ddlGoalType" ErrorMessage="Expiration Date." Display="Dynamic" InitialValue="Select" Width="100%" runat="server">
                        </asp:Requiredfieldvalidator>--%>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlGoalType" ValueToCompare="Select"
	                      ErrorMessage="<br />Goal Objective Required" Operator="NotEqual" CssClass="rfvPCG" ValidationGroup="btnSave" display="Dynamic"> </asp:CompareValidator> 
                        <%--<asp:Requiredfieldvalidator id="RequiredFieldValidator1" runat="server" 
                             errormessage="RequiredFieldValidator" controltovalidate="ddlGoalType" 
                             display="Dynamic" initialvalue="0" setfocusonerror="True" EnableClientScript="True">
                        </asp:Requiredfieldvalidator>--%>
                    </td>
                </tr>
</table>
</asp:Panel>

<asp:Panel ID="PnlNonRetirement" runat="server">

<table width="50%">          
            
             
              <tr id="trPickChild" runat="server" >
                    <td style="width:10%;"></td>
                    <td id="Td4" class="leftField" runat="server" valign="top" style="width:20%;">
                        <asp:Label ID="lblPickChild" runat="server" CssClass="FieldName" Text="Select a child for Goal planning :"></asp:Label>
                    </td>
                    <td id="Td5" class="rightField" runat="server" style="width:20%;">
                        <asp:DropDownList ID="ddlPickChild" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                    </td>
                </tr>
               
            
             
             
                 <tr id="trGoalDesc" runat="server">
                     <td style="width:10%;"></td>
                    <td id="Td2" class="leftField" runat="server" valign="top" style="width:20%;">
                        <asp:Label ID="lblGoalDescription" runat="server" CssClass="FieldName" Text="Goal Description :"></asp:Label>
                    </td>
                    <td id="Td3" class="rightField" TextMode="MultiLine" runat="server" style="width:20%;">
                        <asp:TextBox ID="txtGoalDescription" runat="server" AutoCompleteType="Disabled" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
                 <%--<tr>
                    <td class="leftField">
                        <asp:Label ID="lblGoalDate" runat="server" CssClass="FieldName" Text="Goal Entry Date :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtGoalDate" runat="server" AutoCompleteType="Disabled" CssClass="txtField"></asp:TextBox>                        
                        <cc1:CalendarExtender ID="txtGoalDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                        TargetControlID="txtGoalDate">
                        </cc1:CalendarExtender>
                        <cc1:TextBoxWatermarkExtender ID="txtGoalDate_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtGoalDate" WatermarkText="dd/mm/yyyy">
                        </cc1:TextBoxWatermarkExtender>
                        
                      
                        <span id="SpanGoalDateReq" class="spnRequiredField" runat="server">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtGoalDate"
                            CssClass="rfvPCG" ValidationGroup="btnSave" ErrorMessage="Please select a Date"></asp:RequiredFieldValidator>
                    </td>
                </tr>--%>
              <tr>
                     <td style="width:10%;"></td>  
                    <td class="leftField" valign="top" style="width:20%;">
                        <asp:Label ID="lblPriority" runat="server" CssClass="FieldName" Text="Priority :"></asp:Label>
                    </td>
                    <td class="rightField" style="width:20%;">
                        <asp:DropDownList ID="ddlPriority" runat="server" CssClass="cmbField">
                        <asp:ListItem Text="Select" Value="Select">                                              
                        </asp:ListItem>
                        <asp:ListItem Text="Low" Value="Low">                                              
                        </asp:ListItem>
                        <asp:ListItem Text="Medium" Value="Medium">                                              
                        </asp:ListItem>
                        <asp:ListItem Text="High" Value="High">                                              
                        </asp:ListItem>
                        </asp:DropDownList>
                        <span id="span1" class="spnRequiredField" runat="server">*</span>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlPriority" ValueToCompare="Select"
	                      ErrorMessage="<br />Goal Objective Required" Display="Dynamic" Operator="NotEqual" CssClass="rfvPCG" ValidationGroup="btnSave" ></asp:CompareValidator>                     
                    </td>
                </tr>
                
                <tr>
                    <td style="width:10%;"></td>
                    <td class="leftField" valign="top" style="width:20%;">
                        <asp:Label ID="lblOccurrence" runat="server" CssClass="FieldName" Text="Occurrence :"></asp:Label>
                    </td>
                    <td class="rightField" style="width:20%;">
                        <asp:DropDownList ID="ddlOccurrence" runat="server" CssClass="cmbField" 
                            
                            OnSelectedIndexChanged="ddlOccurrence_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Text="Select" Value="Select">                                              
                        </asp:ListItem>
                        <asp:ListItem Text="One Time" Value="Once">                                              
                        </asp:ListItem>
                        <asp:ListItem Text="Recurring" Value="Recurring">                                              
                        </asp:ListItem>                        
                        </asp:DropDownList>
                        <span id="span2" class="spnRequiredField" runat="server">*</span>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlOccurrence" ValueToCompare="Select"
	                      ErrorMessage="<br />Goal Occurrence Required" Display="Dynamic" Operator="NotEqual" ValidationGroup="btnSave" CssClass="rfvPCG" ></asp:CompareValidator>                    
                    </td>
                </tr>
                
                <%--<tr>
                    <td class="leftField">
                        <asp:Label ID="lblOccurrence" runat="server" CssClass="FieldName" Text="Occurrence :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlOccurrence" runat="server" AutoPostBack="True" CssClass="cmbField">
                        <asp:ListItem Text="Select" Value="0">                                              
                        </asp:ListItem>
                        <asp:ListItem Text="One Time" Value="Once">                                              
                        </asp:ListItem>
                        <asp:ListItem Text="Recurring" Value="Recurring">                                              
                        </asp:ListItem>                        
                        </asp:DropDownList>
                        <span id="span3" class="spnRequiredField" runat="server">*</span>                      
                    </td>
                </tr>  --%>          
                
                
                <tr id="trFrequency" runat="server">
                     <td style="width:10%;"></td>
                    <td class="leftField" valign="top" style="width:20%;">
                        <asp:Label ID="lblFrequency" runat="server" CssClass="FieldName" Text="Frequency :" ></asp:Label>
                    </td>
                    <td class="rightField" style="width:20%;">
                        <asp:DropDownList ID="ddlFrequency" runat="server" CssClass="cmbField">
                        <asp:ListItem Text="Select" Value="0">                                              
                        </asp:ListItem>
                                                
                        </asp:DropDownList>
                        <span id="span5" class="spnRequiredField" runat="server">*</span>                      
                    </td>
                </tr>
                
                <tr>
                    <td style="width:10%;"></td>
                    <td class="leftField" valign="top" style="width:20%;">
                        <asp:Label ID="lblGoalYear" runat="server" CssClass="FieldName" Text="Goal Year :"></asp:Label>
                    </td>
                        <td class="rightField" style="width:20%;">
                        <asp:DropDownList ID="ddlGoalYear" runat="server" CssClass="cmbField" CausesValidation="True">                                                    
                            
                        </asp:DropDownList>
                        <span id="SpanGoalYearReq" class="spnRequiredField" runat="server">*</span>
                        
                         <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlGoalYear" ValueToCompare="Select"
	                      ErrorMessage="<br />Goal Year Required" Display="Dynamic" Operator="NotEqual" ValidationGroup="btnSave" CssClass="rfvPCG" ></asp:CompareValidator> 
	                                         
                       
                    </td>
                </tr>                
               
                <tr>
                     <td style="width:10%;"></td>
                    <td class="leftField" valign="top" style="width:20%;">
                        <asp:Label ID="lblGoalCostToday" runat="server" CssClass="FieldName" Text="Goal Cost Today :"></asp:Label>
                    </td>
                    <td class="rightField" style="width:20%;">
                        <asp:TextBox ID="txtGoalCostToday" runat="server" CssClass="txtField"></asp:TextBox>
                        <span id="SpanGoalCostTodayReq" class="spnRequiredField" runat="server">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtGoalCostToday" 
                        ValidationGroup="btnSave" CssClass="rfvPCG" ErrorMessage="<br />Goal cost Today Required" Display="Dynamic"></asp:RequiredFieldValidator>
                        
                       <ajaxToolkit:FilteredTextBoxExtender ID="txtGoalCostToday_E" runat="server" Enabled="True" TargetControlID="txtGoalCostToday"
                                            FilterType="Custom, Numbers" ValidChars=".">
                       </ajaxToolkit:FilteredTextBoxExtender>
                        
                        <asp:RangeValidator ID="RVtxtGoalCostToday" Display="Dynamic" CssClass="rfvPCG"  
                            SetFocusOnError="True" Type="Double" ErrorMessage="<br />Value  should not be more than 15 digit"
                             ValidationGroup="btnSave" MinimumValue="0" MaximumValue="999999999999999" diplay="dynamic"
                            ControlToValidate="txtGoalCostToday" runat="server"></asp:RangeValidator>
                         
                    </td>
                </tr>
                <tr>
                    <td style="width:10%;"></td>
                    <td class="leftField" style="width:20%;">
                        <asp:Label ID="lblComment" runat="server" CssClass="FieldName" Text="Comments :"></asp:Label>
                    </td>
                    <td class="rightField" style="width:20%;">
                        <asp:TextBox ID="txtComment" runat="server" AutoCompleteType="Disabled" CssClass="txtField"
                            TextMode="MultiLine" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td>
                &nbsp;&nbsp;
                </td>
                <td>
                &nbsp;&nbsp;
                </td>
              
                <td>
                 <asp:Button ID="btnNonRTSave" runat="server" CssClass="PCGMediumButton" Text="Save" ValidationGroup="btnSave" onclick="btnNonRTSave_Click"  />
                  <asp:Button ID="btnNonRTUpdate" runat="server" CssClass="PCGMediumButton" 
                        Text="Update" ValidationGroup="btnSave" onclick="btnNonRTUpdate_Click" />
                </td>
                </tr>
                
</table>

</asp:Panel>

<asp:Panel ID="PnlRetirement" runat="server">

<table width="50%">
    
            <tr>             
                    <td></td>   
                    <td class="leftField">
                        <asp:Label ID="lbl" runat="server" CssClass="FieldName" Text="Goal Cost today (monthly) :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtRTGoalCostToday" runat="server" AutoCompleteType="Disabled" CssClass="txtField">
                        </asp:TextBox>
                        <span id="Span3" class="spnRequiredField" runat="server">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRTGoalCostToday" 
                        ValidationGroup="btnRTSave" CssClass="rfvPCG" ErrorMessage="<br />Goal cost Today(monthly) Required" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </td>
</tr>

            <tr id="trRTGoalCorpsToBeLeftBehind" runat="server">
                     <td></td> 
                     <td class="leftField">
                        <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Corpus to be left behind :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtRTCorpusToBeLeftBehind" runat="server" AutoCompleteType="Disabled" CssClass="txtField">
                        </asp:TextBox>
                    </td>
 </tr>
 
            <tr>
    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
    <td colspan="2" align="justify">
     <asp:Label ID="lblRTGoalBasis" runat="server" CssClass="FieldName" Text="You have chosen to do retirement planning with "></asp:Label>
    </td>
    <td></td>
    </tr>
 
 
            <tr>
                <td></td>  
                <td>
                &nbsp;&nbsp;
                </td>
                <td>
                 <asp:Button ID="btnRTSave" runat="server" CssClass="PCGButton" Text="Save" 
                         ValidationGroup="btnRTSave" onclick="btnRTSave_Click"  />
                 <asp:Button ID="btnRTUpdate" runat="server" CssClass="PCGButton" onclick="btnRTUpdate_Click"  Text="Update" ValidationGroup="btnRTSave"  />
                </td>
                </tr>
</table>

</asp:Panel>




<asp:HiddenField ID="hidRTGoalCorpsLeftBehind" runat="server" />
<asp:HiddenField ID="hidFPCalculationBasis" runat="server" />
<asp:HiddenField ID="hdnMsgValue" runat="server" />

<asp:Button ID="hiddenNRTUpdate" runat="server" OnClick="hiddenNRTUpdate_Click"
    BorderStyle="None" BackColor="Transparent" />
    
    <asp:HiddenField ID="hdnMsgValueRT" runat="server" />

<asp:Button ID="hiddenRTUpdate" runat="server" OnClick="hiddenRTUpdate_Click"
    BorderStyle="None" BackColor="Transparent" />

<script type="text/javascript">
    document.getElementById("<%= PnlNonRetirement.ClientID %>").style.display = 'block';
    document.getElementById("<%= PnlRetirement.ClientID %>").style.display = 'none';

</script>