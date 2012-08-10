﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Subscription.ascx.cs"
    Inherits="WealthERP.SuperAdmin.Subscription" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<script language="javascript" type="text/javascript" src="~/Scripts/JScript.js"></script>


<script type="text/javascript" language="javascript">    
   
    function BalanceCalculate() {
    
//       var storageUsed = 0;
        var storagePaid = 0;
        var storageUsed = 0;
        var storageDefault = 0;
        var storagePaid = document.getElementById('<%=txtPaidSize.ClientID %>').value;
        storageBalance = document.getElementById('<%=txtBalanceSize.ClientID %>').value;
        storageDefault = document.getElementById('<%=txtDefaultStorage.ClientID %>').value;
        storageUsed = document.getElementById('<%=hdnStorageUsed.ClientID %>').value;
       // alert(storageDefault);
        if (storageDefault == null || storageDefault == "") {
            storageDefault = 0;

        }
        if (storagePaid == null || storagePaid == "") {
            storagePaid = 0;

        }
        if (storageUsed == null || storageUsed == "") {
            storageUsed = 0;

        }
        var balance = 0;
        balance = parseFloat(storagePaid) + parseFloat(storageDefault) - parseFloat(storageUsed); 
            document.getElementById('<%=txtBalanceSize.ClientID %>').value =parseFloat(balance).toFixed(2);                     
//            parent.PageMethods.AjaxSetSession(storageBalance);
       
        
        
        //alert(document.getElementById('<%=hdnStorageBalance.ClientID %>').value);
    
    }
</script>
<script type="text/javascript" language="javascript">    
   
    function alertOnBadSelection() {

        var select = document.getElementById('<%=ddlFlavourCategory.ClientID %>');
        var status = false;
        var storageUsed = 0;
        var storagePaid = 0;
        var storageDefault = 0;

       
        
        storageUsed = document.getElementById('<%=hdnStorageUsed.ClientID %>').value;
        storagePaid = document.getElementById('<%=txtPaidSize.ClientID %>').value;
        storageDefault = document.getElementById('<%=txtDefaultStorage.ClientID %>').value;
        if (parseFloat(storagePaid) + parseFloat(storageDefault) < parseFloat(storageUsed)) {
          var msg = 'Your have allocated' + '\t' + storageUsed + '\t' +'MB of size';
          alert(msg);
            alert('Please free your space allocated first');
            return false;        
        }
        
            if (select.options[select.selectedIndex].value == "Select") {
                alert('Please Select Category');
                return false;
            }
            else {
                var chkListModules = document.getElementById('<%= chkModules.ClientID %>');
                var chkListinputs = chkListModules.getElementsByTagName("input");
                for (var i = 0; i < chkListinputs.length; i++) {
                    if (chkListinputs[i].checked) {
                        status=true;
                    }

                }
                if (status) {
                    return true;
                }
                else {                    
                    alert('Please select atleast one Module');
                    return false;
                }              
                
            }
             
    }
</script>

<style>
    .FieldName
    {
        text-align: left;
    }
</style>
<telerik:RadInputManager ID="RadInputManager1" runat="server" Skin="Telerik" EnableEmbeddedSkins="false">
    <telerik:NumericTextBoxSetting Type="Number" ErrorMessage="Please Enter only Numbers"
        GroupSeparator="" GroupSizes="3" MinValue="0" PositivePattern="n" 
        DecimalDigits="0">
        <TargetControls>
            <%--<telerik:TargetInput ControlID="txtNoOfBranches" />--%>
           <%-- <telerik:TargetInput ControlID="txtNoOfStaffLogins" />--%>
           <%-- <telerik:TargetInput ControlID="txtNoOfCustomerLogins" />--%>
            <%--<telerik:TargetInput ControlID="txtSMSBought" />--%>
            <telerik:TargetInput ControlID="txtSMSSentTillDate" />
            <telerik:TargetInput ControlID="txtSMSRemaining" />
        </TargetControls>
    </telerik:NumericTextBoxSetting>
    <telerik:DateInputSetting Culture="Hindi (India)" DateFormat="d/M/yyyy" DisplayDateFormat="d/M/yyyy"
        ErrorMessage="Please Enter Valid Date" MinDate="">
        <TargetControls>
            <telerik:TargetInput ControlID="dpTrialStartDate" />
            <telerik:TargetInput ControlID="dpTrialEndDate" />
            <telerik:TargetInput ControlID="dpStartDate" />
            <telerik:TargetInput ControlID="dpEndDate" />
        </TargetControls>
    </telerik:DateInputSetting>
</telerik:RadInputManager>


<table width="100%">
<tr>
     <td colspan="3">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                <tr>
                    <td align="left"> Subscription
                    </td>
                   <td align="right">
                      <asp:ImageButton ID="imgRefresh" runat="server" ToolTip="Click here to refresh Tree Node Cache" ImageUrl="../Images/refresh.png"
             Width="20px" Height="20px"   OnClick="imgRefresh_Click" />
                    </td>
                    
                </tr>
                </table>
            </div>
        </td>
    </tr>
    </table>
<table width="100%">
    <tr>
        <td align="center">
            <div class="success-msg" id="SettingsSavedMessage" runat="server" visible="false"
                align="center">
                Settings Saved Successfully
            </div>
        </td>
    </tr>
    </table>
<table width="100%">
    <tr>
        <td>
            <table width="100%">
                <tr>
                    <td align="right">
                        <asp:Label ID="lblTrialStartDate" runat="server" CssClass="FieldName" Text="Trial Start Date : "></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="dpTrialStartDate" runat="server" Skin="Telerik" EnableEmbeddedSkins="false">
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x" 
                                ></Calendar>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>

<DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy"></DateInput>
                        </telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblTrailEndDate" runat="server" CssClass="FieldName" Text="Trial End Date : "></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="dpTrialEndDate" runat="server" Skin="Telerik" EnableEmbeddedSkins="false">
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x" 
                                Skin="Telerik" EnableEmbeddedSkins="False"></Calendar>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>

<DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy" EnableEmbeddedSkins="False"></DateInput>
                        </telerik:RadDatePicker>
                        <asp:CompareValidator ID="cvTrailEndDate" runat="server" ControlToCompare="dpTrialStartDate"
                            ControlToValidate="dpTrialEndDate" Operator="GreaterThan" ErrorMessage="Trial End Date should be greater than Trial Start Date"
                            Display="Dynamic" CssClass="cvPCG" ValidationGroup="btnSubmit"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblStartDate" runat="server" CssClass="FieldName" Text="Start Date : "></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="dpStartDate" runat="server" Skin="Telerik" EnableEmbeddedSkins="false">
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x" 
                                EnableEmbeddedSkins="False" Skin="Telerik"></Calendar>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>

<DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy" EnableEmbeddedSkins="False"></DateInput>
                        </telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblEndDate" runat="server" CssClass="FieldName" Text="End Date : "></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="dpEndDate" runat="server" Skin="Telerik" EnableEmbeddedSkins="false">
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x" 
                                EnableEmbeddedSkins="False" Skin="Telerik"></Calendar>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>

<DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy" EnableEmbeddedSkins="False"></DateInput>
                        </telerik:RadDatePicker>
                        <asp:CompareValidator ID="cvEndStart" runat="server" ControlToCompare="dpStartDate"
                            ControlToValidate="dpEndDate" Operator="GreaterThan" ErrorMessage="End Date should be greater than Start Date"
                            Display="Dynamic" CssClass="cvPCG" ValidationGroup="btnSubmit" ></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblNoOfBranches" runat="server" CssClass="FieldName" Text="No Of Branches : "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNoOfBranches" runat="server" CssClass="txtField" MaxLength="3"></asp:TextBox>
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server"  CssClass="cvPCG" ControlToValidate="txtNoOfBranches"
                                     Display="Dynamic"  ErrorMessage="Please Enter Numbers" ValidationExpression="^0$|^[1-9][0-9]*$|^[1-9][0-9]{0,2}(,[0-9]{3})$" ValidationGroup="btnSubmit"></asp:RegularExpressionValidator>

                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblNoOfStaffLogins" runat="server" CssClass="FieldName" Text="No of StaffLogins : "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNoOfStaffLogins" runat="server" CssClass="txtField"  MaxLength="5"></asp:TextBox>
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" CssClass="cvPCG" ControlToValidate="txtNoOfStaffLogins"
                                     Display="Dynamic"  ErrorMessage="Please Enter Numbers" ValidationExpression="^0$|^[1-9][0-9]*$|^[1-9][0-9]{0,2}(,[0-9]{3})$" ValidationGroup="btnSubmit"></asp:RegularExpressionValidator>

                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblNoOfCustomerLogins" runat="server" CssClass="FieldName" Text="No.of CustomerLogins:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNoOfCustomerLogins" runat="server" CssClass="txtField" MaxLength="5"></asp:TextBox>
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" CssClass="cvPCG" ControlToValidate="txtNoOfCustomerLogins"
                                     Display="Dynamic"  ErrorMessage="Please Enter Numbers" ValidationExpression="^0$|^[1-9][0-9]*$|^[1-9][0-9]{0,2}(,[0-9]{3})$" ValidationGroup="btnSubmit"></asp:RegularExpressionValidator>

                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblSMSBought" runat="server" CssClass="FieldName" Text="SMS's Bought : "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSMSBought" runat="server" CssClass="txtField"></asp:TextBox>                       
                        
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" CssClass="cvPCG" ControlToValidate="txtSMSBought"
                                     Display="Dynamic"  ErrorMessage="Please Enter Numbers" ValidationExpression="^0$|^[1-9][0-9]*$|^[1-9][0-9]{0,2}(,[0-9]{3})$" ValidationGroup="btnSubmit"></asp:RegularExpressionValidator>
                    </td>
                </tr>   
                <tr>
                    <td align="right">
                        <asp:Label ID="lblDefaultStorage" runat="server" CssClass="FieldName" Text="Vault Default Size(mb):"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDefaultStorage" runat="server" CssClass="txtField" onchange="BalanceCalculate()"></asp:TextBox>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="cvPCG" ControlToValidate="txtDefaultStorage"
                                     Display="Dynamic"  ErrorMessage="Please Enter Numeric Value" ValidationExpression="\d+\.?\d*" ValidationGroup="btnSubmit"></asp:RegularExpressionValidator>
                    </td>
                </tr>  
                <tr>
                 <tr>
                    <td align="right">
                        <asp:Label ID="lblPaidSize" runat="server" CssClass="FieldName" Text="Vault Paid Size(mb):"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPaidSize" runat="server" CssClass="txtField" onchange="BalanceCalculate()"></asp:TextBox>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" CssClass="cvPCG" ControlToValidate="txtPaidSize"
                                     Display="Dynamic" ErrorMessage="Please Enter Numeric Value" ValidationExpression="\d+\.?\d*" ValidationGroup="btnSubmit"></asp:RegularExpressionValidator>

                    </td>
                </tr>  
                <tr>
                    <td align="right">
                        <asp:Label ID="lblBalanceSize" runat="server" CssClass="FieldName" Text="Vault Balance Storage:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBalanceSize" runat="server" CssClass="txtField"  ReadOnly="true"></asp:TextBox>
                    </td>
                </tr> 
                
                <tr>
                    <td align="right">
                        <asp:Label ID="lblUsedSpace" runat="server" CssClass="FieldName" Text="Vault Used Storage:" Enabled="false"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUsedSpace" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
                    </td>
                </tr>            
              <tr><td></td>  <td>
         <asp:CheckBox ID="chkMailSend" runat="server" Text="Send Login info?"  CssClass="cmbField"/>
        </td></tr>  
                <tr>
                    <td valign="top" align="right">
                        <asp:Label ID="lblComment" runat="server" CssClass="FieldName" Text="Comment : ">
                        </asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" CssClass="txtField"
                            MaxLength="500" Width="500px" Height="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td></td>
                <td><asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" CssClass="PCGMediumButton" OnClientClick="return alertOnBadSelection()" ValidationGroup="btnSubmit"/></td>
                </tr>
            </table>
        </td>
        <td valign="top">
        <asp:UpdatePanel runat="server" ID="uplPresentValue" UpdateMode="Conditional">
                                        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblFlavourCategory" runat="server" CssClass="FieldName" Text="Flavour Category: "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFlavourCategory" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlFlavourCategory_SelectedIndexChanged"
                            AutoPostBack="true">
                            <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                            <asp:ListItem Text="Advisory" Value="AD"></asp:ListItem>
                            <asp:ListItem Text="All" Value="AL"></asp:ListItem>
                            <asp:ListItem Text="Distribution" Value="DT"></asp:ListItem>
                            <%--<asp:ListItem Text="Value Adds" Value="VA"></asp:ListItem>--%>
                        </asp:DropDownList>
                          
                    </td>
                </tr>
                <tr>
                <td>
                    </td>
                    <td>
                        <asp:CheckBoxList ID="chkModules" runat="server" CssClass="FieldName">
                            <asp:ListItem Text="Mutual Funds" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Equity" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Financial Planning" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Insurance" Value="4"></asp:ListItem>                            
                            <asp:ListItem Text="Liabilities" Value="5"></asp:ListItem>
                            <asp:ListItem Text="Multi Asset" Value="6"></asp:ListItem>
                            <asp:ListItem Text="Common" Value="7" Enabled="false" Selected="True"></asp:ListItem>                            
                            <asp:ListItem Text="Goal" Value="8"></asp:ListItem>
                            <asp:ListItem Text="Risk Profile" Value="9"></asp:ListItem>                          
                        </asp:CheckBoxList>
                        <asp:CustomValidator runat="server" ID="cvmodulelist" 
                            ErrorMessage="Please Select Atleast one Module" CssClass="cvPCG"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                <td align="right">
                <asp:Label ID="lblValueAdds" runat="server" CssClass="FieldName" Text="Value Adds:"></asp:Label>
                </td>
                <td>
                        <asp:CheckBoxList ID="chkValueAdds" runat="server" CssClass="FieldName">
                            <asp:ListItem Text="Inbox" Value="10"></asp:ListItem>
                            <asp:ListItem Text="Repository" Value="11"></asp:ListItem>
                            <asp:ListItem Text="Vault" Value="12"></asp:ListItem>                                              
                        </asp:CheckBoxList>                  
                </td>
                </tr>
            </table>
            </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>      
</table>
<asp:HiddenField ID="hdnSMSBought" runat="server" />
<asp:HiddenField ID="hdnStorageUsed" runat="server" />
<asp:HiddenField ID="hdnStorageBalance" runat="server" />