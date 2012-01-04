<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerAssumptionsPreferencesSetup.ascx.cs" Inherits="WealthERP.Customer.CustomerAssumptionsPreferencesSetup" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>



<%--<script type="text/javascript">

    function Validate() {
//        var assumptionValue = document.getElementById('ctrl_CustomerAssumptionsPreferencesSetup_RadGrid1_ctl00__2').value;        
//        return false;
    }

</script>--%>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>
 <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager> 
    <br /> 
    
    
<telerik:RadTabStrip ID="RadTabStrip1" runat="server" EnableTheming="True" Skin="Telerik"
    EnableEmbeddedSkins="False" MultiPageID="CustomerAssumptionsPreferencesSetupId" 
    SelectedIndex="0">
    <Tabs>
        
        <telerik:RadTab runat="server" Text="Assumptions" onclick="HideStatusMsg()"
            Value="Assumtion" TabIndex="0" Selected="True">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Goal Funding" onclick="HideStatusMsg()"
            Value="Preferences" TabIndex="1">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Model Portfolio" onclick="HideStatusMsg()"
            Value="Calculation Basis" TabIndex="2">
        </telerik:RadTab>
         <telerik:RadTab runat="server" Text="Retirement" onclick="HideStatusMsg()"
            Value="Retirement" TabIndex="2">
        </telerik:RadTab>
        
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
         <table width="50%">
            <tr>
            <td><br /></td>
            </tr>
                <tr>
                <td></td>
   <td>
<asp:RadioButton ID="rbtnMapping" runat="server" CssClass="cmbField" Text="Mapping" GroupName="gpPlan" Checked="true"/>
        
 </td>

                    </tr>
                    <tr>
                    <td></td><td>
                        <asp:RadioButton ID="rbtnConsolidateEntry" runat="server" CssClass="cmbField" 
                            GroupName="gpPlan" Text="Consolidate Entry" />
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
        
        
        <telerik:RadPageView ID="RadPageView1" runat="server">
        <asp:Panel ID="Panel2" runat="server">
         <table width="50%">
   <tr>
       <td style="width:35px">
       </td>
       <td>
       <br />
          <%-- <asp:Label ID="lblRetirementGoalAnalysis0" runat="server" CssClass="FieldName" 
               Text="Retirement Goal Settings:"></asp:Label>--%>
     </td>
   </tr>
       </table>
       <table width="50%">
   <tr>
       <td style="width:95px"></td>
       
       <td align="left">
           <asp:RadioButton ID="rbtnYes" runat="server" Checked="true" 
               CssClass="cmbField" GroupName="gpRetirement" 
               Text="Yes" />
       </td><td></td>
       </tr>
   <tr>
   <td style="width:95px"></td>
   <td align="left">
       <asp:RadioButton ID="rbtnNo" runat="server" CssClass="cmbField" 
           GroupName="gpRetirement" Text="No" />
       </td><td>
           &nbsp;</td>
   </tr>
   <tr>
   <td style="width:35px"></td>
   <td></td><td></td>
   </tr>
   <tr>
   <td style="width:35px"></td>
   <td>
  <%--     <asp:Button ID="btnCalculationBasis" OnClientClick="clientSideFunctionsForSubmit()" runat="server" CssClass="PCGButton" 
           OnClick="btnCalculationBasis_OnClick " Text="Submit" />--%>
            <asp:Button ID="btnCalculationBasis" runat="server" CssClass="PCGButton" 
           OnClick="btnCalculationBasis_OnClick " Text="Submit" />
   </td>
   <td></td>
   </tr>
           </table>
        
         </asp:Panel>
        </telerik:RadPageView>
        
         <telerik:RadPageView ID="RadPageView4" runat="server">
        <asp:Panel ID="Retirementpnl" runat="server">
          <table width="50%">
   <tr>
       <td style="width:35px">
       </td>
       <td>
       <br />
          <%-- <asp:Label ID="lblRetirementGoalAnalysis0" runat="server" CssClass="FieldName" 
               Text="Retirement Goal Settings:"></asp:Label>--%>
     </td>
   </tr>
       </table>
       <table width="50%">
   <tr>
       <td style="width:95px"></td>
       
       <td align="left">
           <asp:RadioButton ID="rbtnNoCorpus" runat="server" Checked="true" 
               CssClass="cmbField" GroupName="grpRetirement" 
               Text="No corpus to be left behind" />
       </td><td></td>
       </tr>
   <tr>
   <td style="width:95px"></td>
   <td align="left">
       <asp:RadioButton ID="rbtnCorpus" runat="server" CssClass="cmbField" 
           GroupName="grpRetirement" Text="Corpus to be left behind" />
       </td><td>
           &nbsp;</td>
   </tr>
   <tr>
   <td style="width:35px"></td>
   <td></td><td></td>
   </tr>
   <tr>
   <td style="width:35px"></td>
   <td>
  <%--     <asp:Button ID="btnCalculationBasis" OnClientClick="clientSideFunctionsForSubmit()" runat="server" CssClass="PCGButton" 
           OnClick="btnCalculationBasis_OnClick " Text="Submit" />--%>
            <asp:Button ID="Button3" runat="server" CssClass="PCGButton" 
           OnClick="btnRetirementCalculationBasis_OnClick" Text="Submit" />
   </td>
   <td></td>
   </tr>
           </table>
        
         </asp:Panel>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
 
 <asp:HiddenField ID="hdnLEValue" Value="0" runat="server" />    
<asp:HiddenField ID="hdnRAValue" Value="0" runat="server" />    