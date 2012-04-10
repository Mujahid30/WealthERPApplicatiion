<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Subscription.ascx.cs"
    Inherits="WealthERP.SuperAdmin.Subscription" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<script language="javascript" type="text/javascript" src="~/Scripts/JScript.js"></script>

<script type="text/javascript" language="javascript">    
   
    function alertOnBadSelection() {

        var select = document.getElementById('<%=ddlFlavourCategory.ClientID %>');
        var status = false;
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
            <telerik:TargetInput ControlID="txtNoOfBranches" />
            <telerik:TargetInput ControlID="txtNoOfStaffLogins" />
            <telerik:TargetInput ControlID="txtNoOfCustomerLogins" />
            <telerik:TargetInput ControlID="txtSMSBought" />
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
<div>
    <asp:Label ID="lblIFFAdd" runat="server" Text="Subscription" CssClass="HeaderTextBig"></asp:Label>
    <hr />
</div>
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

<DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy"></DateInput>
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

<DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" EnableEmbeddedSkins="False"></DateInput>
                        </telerik:RadDatePicker>
                        <asp:CompareValidator ID="cvTrailEndDate" runat="server" ControlToCompare="dpTrialStartDate"
                            ControlToValidate="dpTrialEndDate" Operator="GreaterThan" ErrorMessage="Trial End Date should be greater than Trial Start Date"
                            Display="Dynamic" CssClass="cvPCG"></asp:CompareValidator>
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

<DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" EnableEmbeddedSkins="False"></DateInput>
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

<DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" EnableEmbeddedSkins="False"></DateInput>
                        </telerik:RadDatePicker>
                        <asp:CompareValidator ID="cvEndStart" runat="server" ControlToCompare="dpStartDate"
                            ControlToValidate="dpEndDate" Operator="GreaterThan" ErrorMessage="End Date should be greater than Start Date"
                            Display="Dynamic" CssClass="cvPCG"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblNoOfBranches" runat="server" CssClass="FieldName" Text="No Of Branches : "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNoOfBranches" runat="server" CssClass="txtField" MaxLength="3"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblNoOfStaffLogins" runat="server" CssClass="FieldName" Text="No of StaffLogins : "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNoOfStaffLogins" runat="server" CssClass="txtField"  MaxLength="5"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblNoOfCustomerLogins" runat="server" CssClass="FieldName" Text="No of CustomerLogins : "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNoOfCustomerLogins" runat="server" CssClass="txtField" MaxLength="5"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblSMSBought" runat="server" CssClass="FieldName" Text="SMS's Bought : "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSMSBought" runat="server" CssClass="txtField"></asp:TextBox>
                       
                    </td>
                </tr>               
               
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
                <td><asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" CssClass="PCGMediumButton" OnClientClick="return alertOnBadSelection()"/></td>
                </tr>
            </table>
        </td>
        <td valign="top">
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
                            <asp:ListItem Text="Distribtion" Value="DT"></asp:ListItem>
                            <asp:ListItem Text="Value Adds" Value="VA"></asp:ListItem>
                        </asp:DropDownList>
                          
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBoxList ID="chkModules" runat="server" CssClass="FieldName">
                            <asp:ListItem Text="Mutual Funds" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Equity" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Financial Planning" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Insurance" Value="4"></asp:ListItem>                            
                            <asp:ListItem Text="Liabilities" Value="5"></asp:ListItem>
                            <asp:ListItem Text="Multi Asset" Value="6"></asp:ListItem>
                            <asp:ListItem Text="Common" Value="7"></asp:ListItem>                            
                            <asp:ListItem Text="Goal" Value="8"></asp:ListItem>
                            <asp:ListItem Text="Risk Profile" Value="9"></asp:ListItem>                          
                        </asp:CheckBoxList>
                        <asp:CustomValidator runat="server" ID="cvmodulelist" 
                            ErrorMessage="Please Select Atleast one Module" CssClass="cvPCG"></asp:CustomValidator>
                    </td>
                </tr>
            </table>
        </td>
    </tr>   
    
</table>
<asp:HiddenField ID="hdnSMSBought" runat="server" />