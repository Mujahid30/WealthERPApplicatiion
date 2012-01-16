<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerAssumptionsPreferencesSetup.ascx.cs" Inherits="WealthERP.Customer.CustomerAssumptionsPreferencesSetup" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>



<%--<script type="text/javascript">

    function Validate() {
//        var assumptionValue = document.getElementById('ctrl_CustomerAssumptionsPreferencesSetup_RadGrid1_ctl00__2').value;        
//        return false;
    }

</script>--%>



 <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager> 
    <br /> 
   <script type="text/javascript">

       function HideStatusMsg() {
           document.getElementById("<%=msgRecordStatus.ClientID%>").style.display = 'none';
       }

</script>   
<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>


<script type="text/javascript">
    $(document).ready(function() {
        $('.ScreenTip1').bubbletip($('#div1'), { deltaDirection: 'right' });
        $('.ScreenTip2').bubbletip($('#div2'), { deltaDirection: 'right' });
        $('.ScreenTip3').bubbletip($('#div3'), { deltaDirection: 'right' });
    });
</script>    
<telerik:RadTabStrip ID="RadTabStrip1" runat="server" EnableTheming="True" Skin="Telerik"
    EnableEmbeddedSkins="False" MultiPageID="CustomerAssumptionsPreferencesSetupId" 
    SelectedIndex="0">
    <Tabs>
        
        <telerik:RadTab runat="server" Text="Assumptions" onclick="HideStatusMsg()"
            Value="Assumtion" TabIndex="0" Selected="True">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Preferences" onclick="HideStatusMsg()"
            Value="Preferences" TabIndex="1">
        </telerik:RadTab>
    <%--    <telerik:RadTab runat="server" Text="Model Portfolio" onclick="HideStatusMsg()"
            Value="Calculation Basis" TabIndex="2">
        </telerik:RadTab>
         <telerik:RadTab runat="server" Text="Retirement" onclick="HideStatusMsg()"
            Value="Retirement" TabIndex="2">
        </telerik:RadTab>--%>
        
    </Tabs>
</telerik:RadTabStrip>

<telerik:RadMultiPage ID="CustomerAssumptionsPreferencesSetupId" 
    EnableViewState="true" runat="server" SelectedIndex="0">
 <telerik:RadPageView ID="RadPageView2" runat="server">
        <asp:Panel ID="pnlPreferences" runat="server">
        <br />
        
      <table style="width:75%">
        <tr>
        <td>
        </td>
        <td>
        <telerik:RadGrid ID="RadGrid1" runat="server" CssClass="RadGrid" GridLines="None"
        AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="False" Skin="Telerik"
        ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="False"
        OnItemInserted="RadGrid1_ItemInserted" OnItemUpdated="RadGrid1_ItemUpdated" OnItemCommand="RadGrid1_ItemCommand"
        OnPreRender="RadGrid1_PreRender"  DataKeyNames="WA_AssumptionId"
        AllowAutomaticUpdates="False" HorizontalAlign="NotSet">
        <MasterTableView CommandItemDisplay="None" DataKeyNames="WA_AssumptionId"
         EditMode="PopUp">
            <Columns>
                <telerik:GridEditCommandColumn>
                </telerik:GridEditCommandColumn>
                <%--<telerik:GridBoundColumn UniqueName="RiskClass" HeaderText="Risk Class" DataField="RiskClass">
                    <HeaderStyle Width="60px"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="Description" HeaderText="Description" DataField="Description">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="LastName" HeaderText="LastName" DataField="LastName">
                </telerik:GridBoundColumn>--%>
                <telerik:GridBoundColumn UniqueName="WA_AssumptionName" HeaderText="Assumption" DataField="WA_AssumptionName" >
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CPA_Value" HeaderText="Value" DataField="CPA_Value" >
             </telerik:GridBoundColumn>
                <%-- <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="column">
                </telerik:GridButtonColumn>--%>
            </Columns>
            <EditFormSettings  InsertCaption="Add new item" FormTableStyle-HorizontalAlign="Center" 
            PopUpSettings-Modal="true" PopUpSettings-ZIndex="60" CaptionFormatString="Edit Risk ClassCode: {0}"
            CaptionDataField="CPA_Value" EditFormType="Template">
                <FormTemplate>
                    <table id="Table1" cellspacing="1" cellpadding="1" width="450" border="0">
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblPickAssumptions" runat="server" Text="Assumptions:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                               <asp:TextBox ID="txtAssumptions" runat="server" Text='<%# Bind( "WA_AssumptionName") %>' CssClass="txtField" Enabled="false"></asp:TextBox>
                                                                 
                               
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblAssumptionValue" runat="server" Text="Value:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAssumptionValue"  runat="server" CssClass="txtField" Text='<%# Bind( "CPA_Value") %>'>
                                </asp:TextBox>
                                
                            </td>
                        </tr>
                        <tr>
                         <td>
                        </td>
                        <td>
                        <asp:RangeValidator ID="rvAssumptionValue" runat="server" 
                              ControlToValidate="txtAssumptionValue" CssClass="cvPCG" Display="Dynamic" 
                              ErrorMessage="Please enter value less than 100" MaximumValue="99.9" 
                              MinimumValue="0.0" Type="Double" ValidationGroup="vgbtnSubmit"></asp:RangeValidator>
                        </td>
                       
                        </tr>                                           
                    </table>
                    <table style="width: 100%">
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                    runat="server" CssClass="PCGButton" ValidationGroup="vgbtnSubmit" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                </asp:Button>&nbsp;
                                <asp:Button ID="Button2" CssClass="PCGButton" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                </asp:Button>
                            </td>
                        </tr>
                    </table>
                </FormTemplate>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings>
            <%--<ClientEvents OnRowDblClick="RowDblClick" />--%>
        </ClientSettings>
      
        
    </telerik:RadGrid>  
    </td>
        </tr>
        </table>   
      </asp:Panel>
   
    </telerik:RadPageView>
    
 
 <telerik:RadPageView ID="RadPageView3" runat="server">
        <asp:Panel ID="Panel1" runat="server">
         <table width="100%">
    <tr>
        <td align="center">
            <div id="msgRecordStatus" runat="server" class="success-msg" align="center">
               Records Saved Successfully
            </div>
        </td>
    </tr>
</table>
         <table>
            <tr>
            <td><br /></td>
            </tr>
                <tr>
                <td></td>
   <td>
<asp:CheckBox ID="chkMapping" runat="server" CssClass="cmbField" Text="Goal funding from MF Investments" />
    <img src="../Images/help.png" class="ScreenTip1" style="height: 15px; width: 15px;" />
            <div id="div1" style="display: none;">
                <p class="tip">
                   Check if you are funding for Goal from MF Investments
                    
                </p>
            </div>
        
 </td>

                    </tr>
                    <tr>
                    <td></td><td>
                        <asp:CheckBox ID="chkRetirement" runat="server" CssClass="cmbField" 
                             Text="Retirement calculation basis corpus to be left behind" />
                              <img src="../Images/help.png" class="ScreenTip2" style="height: 15px; width: 15px;" />
            <div id="div2" style="display: none;">
                <p class="tip">
                    Check if you want retirement calculation based on corpus to be left behind
                </p>
            </div>
                        </td>
                    </tr>
                    
                     <tr>
                    <td></td><td>
                        <asp:CheckBox ID="chkModelPortFolio" runat="server" CssClass="cmbField" 
                          Text="View Model Portfolio" />
                              <img src="../Images/help.png" class="ScreenTip3" style="height: 15px; width: 15px;" />
            <div id="div3" style="display: none;">
                <p class="tip">
                   Check if you want to use model portfolio
                </p>
            </div>
                        </td>
                    </tr>
                    
                    <tr>
                    <td style="width:95px"></td>
                    <td>
                        <asp:Button ID="btnGoalFundingPreference" runat="server" Text="Submit" CssClass="PCGButton" OnClick="btnGoalFundingPreference_OnClick" />
                    </td>
                    
                    </tr>
                    </table>
        </asp:Panel>
        </telerik:RadPageView>
 </telerik:RadMultiPage>
 
 <asp:HiddenField ID="hdnLEValue" Value="0" runat="server" />    
<asp:HiddenField ID="hdnRAValue" Value="0" runat="server" />    