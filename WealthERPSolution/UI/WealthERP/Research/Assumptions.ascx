<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Assumptions.ascx.cs" Inherits="WealthERP.Research.Assumptions" %>
 <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager> 
<telerik:RadMultiPage ID="AdviserAssumptionsId" 
    EnableViewState="true" runat="server" SelectedIndex="0">
<telerik:RadPageView ID="RadPageView2" runat="server">
        <asp:Panel ID="pnlPreferences" runat="server">
      <table width="100%" class="TableBackground">
        <tr>
                <td class="HeaderCell">
                    <asp:Label ID="lblView" runat="server" CssClass="HeaderTextBig" Text="Assumptions"></asp:Label>
                    <hr />
                </td>
            </tr>
        </table>  
        
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
        <MasterTableView CommandItemDisplay="None" DataKeyNames="WA_AssumptionId" EditMode="PopUp">
            <Columns>
                <telerik:GridEditCommandColumn>
                </telerik:GridEditCommandColumn>                
                <telerik:GridBoundColumn UniqueName="WA_AssumptionName" HeaderText="Assumption" DataField="WA_AssumptionName" >
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="AA_Value" HeaderText="Value" DataField="AA_Value" >
             </telerik:GridBoundColumn>                
            </Columns>
            <EditFormSettings  InsertCaption="Add new item" FormTableStyle-HorizontalAlign="Center" 
            PopUpSettings-Modal="true" PopUpSettings-ZIndex="60" CaptionFormatString="Edit"
            CaptionDataField="AA_Value" EditFormType="Template">
                <FormTemplate>
                    <table id="Table1" cellspacing="1" cellpadding="1" width="100%" border="0">
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
                                <asp:TextBox ID="txtAssumptionValue"  runat="server" CssClass="txtField" Text='<%# Bind( "AA_Value") %>'>
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                         <td>
                        </td>
                        <td>
                        <asp:RangeValidator ID="rvAssumptionValue" runat="server" 
                              ControlToValidate="txtAssumptionValue" CssClass="cvPCG" Display="Dynamic" 
                              ErrorMessage="Please enter correct value" MaximumValue="100.00" 
                              MinimumValue="-100.00" Type="Double" ValidationGroup="vgbtnSubmit"></asp:RangeValidator>
                        </td>
                       
                        </tr>                                           
                    
                        <tr>
                         <td></td>
                            <td align="right" colspan="2">
                                <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                    runat="server" CssClass="PCGButton" ValidationGroup="vgbtnSubmit" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                </asp:Button>&nbsp;
                                <asp:Button ID="Button2" CssClass="PCGButton" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                </asp:Button>
                            </td>
                           
                        </tr>
                        <tr>
                        <td></td>
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
 </telerik:RadMultiPage>