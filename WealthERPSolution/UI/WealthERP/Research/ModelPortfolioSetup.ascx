<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModelPortfolioSetup.ascx.cs" Inherits="WealthERP.Research.ModelPortfolioSetup" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager> 


<table id="tableGrid" runat="server" class="TableBackground" width="100%">
<tr>
    <td>
     <telerik:RadGrid ID="RadGrid1" runat="server" Skin="Telerik" CssClass="RadGrid" GridLines="None" AllowPaging="True" 
    PageSize="20" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true" AllowAutomaticDeletes="false" 
    AllowAutomaticInserts="false" OnItemDataBound="RadGrid1_ItemDataBound" OnDataBound="RadGrid1_DataBound"
    OnUpdateCommand="RadGrid1_UpdateCommand"  OnItemCommand="RadGrid1_ItemCommand" OnInsertCommand="RadGrid1_InsertCommand"
    OnPreRender="RadGrid1_PreRender" OnDeleteCommand="RadGrid1_DeleteCommand" OnItemCreated="RadGrid1_ItemCreated" 
    AllowAutomaticUpdates="false" HorizontalAlign="NotSet" DataKeyNames="XAMP_ModelPortfolioCode">
        <MasterTableView CommandItemDisplay="Top" EditMode="PopUp" DataKeyNames="XAMP_ModelPortfolioCode">
            <Columns>
                 <telerik:GridEditCommandColumn UpdateText="Update" UniqueName="EditCommandColumn"
                    CancelText="Cancel" ButtonType="ImageButton" CancelImageUrl="../Images/Telerik/Cancel.gif"
                    InsertImageUrl="../Images/Telerik/Update.gif" UpdateImageUrl="../Images/Telerik/Update.gif"
                    EditImageUrl="../Images/Telerik/Edit.gif">
                    <HeaderStyle Width="85px"></HeaderStyle>
                 </telerik:GridEditCommandColumn>
                                 
                <telerik:GridBoundColumn UniqueName="XAMP_ModelPortfolioName" HeaderText="Varient" DataField="XAMP_ModelPortfolioName">
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn UniqueName="XRC_RiskClass" HeaderText="Risk Class" DataField="XRC_RiskClass">
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn UniqueName="XAMP_MinAUM" HeaderText="Minimum AUM" DataField="XAMP_MinAUM">
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn UniqueName="XAMP_MaxAUM" HeaderText="Maximum AUM" DataField="XAMP_MaxAUM">
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn UniqueName="XAMP_MinAge" HeaderText="Minimum Age" DataField="XAMP_MinAge">
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn UniqueName="XAMP_MaxAge" HeaderText="Maximum Age" DataField="XAMP_MaxAge">
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn UniqueName="XAMP_MinTimeHorizon" HeaderText="Minimum Time Horizon (Months)" DataField="XAMP_MinTimeHorizon">
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn UniqueName="XAMP_MaxTimeHorizon" HeaderText="Maximum Time Horizon (Months)" DataField="XAMP_MaxTimeHorizon">
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn UniqueName="XAMP_Description" HeaderText="Description" DataField="XAMP_Description">
                </telerik:GridBoundColumn>
                
                <%--<telerik:GridBoundColumn UniqueName="AssetClass" HeaderText="Asset Class" DataField="AssetClass">
                </telerik:GridBoundColumn>--%>
                
                <telerik:GridBoundColumn UniqueName="Allocation" HeaderText="% Allocation" DataField="Allocation">
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn UniqueName="XAMP_ROR" HeaderText="ROR(%)" DataField="XAMP_ROR">
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn UniqueName="XAMP_RiskPercentage" HeaderText="Risk(%)" DataField="XAMP_RiskPercentage">
                </telerik:GridBoundColumn>
                
                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Remove" ConfirmText="Are you sure you want to Remove this Row?" 
                ShowInEditForm="true" ImageUrl="../Images/Telerik/Delete.gif" Text="Remove Row" UniqueName="Remove">
                </telerik:GridButtonColumn>
                
                <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="column">
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings InsertCaption="Add new item" FormTableStyle-HorizontalAlign="Center"
            PopUpSettings-Modal="true" PopUpSettings-ZIndex="20" EditFormType="Template">            
                <FormTemplate>
                    <table ID="Table1" cellspacing="1" cellpadding="1" width="100%" border="0">                        
                        <tr id="trAddNamePortfolio" runat="server">
                            <td class="leftField">
                                <asp:Label ID="lblNamePortfolio" runat="server" Text="Portfolio Name :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtNamePortfolio" CssClass="txtField" runat="server">
                                </asp:TextBox>                               
                            </td>
                            <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNamePortfolio"
                                ErrorMessage="Please Name the Portfolio" Display="Dynamic" runat="server"
                                CssClass="rfvPCG" ValidationGroup="Button1">
                            </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trEditNamePortfolio" runat="server">
                            <td class="leftField">
                                <asp:Label ID="lblPortfolioName" runat="server" Text="Portfolio Name :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtPortfolioName" CssClass="txtField" Text='<%# Bind( "XAMP_ModelPortfolioName") %>' runat="server">
                                </asp:TextBox>                               
                            </td>
                        </tr>  
                        <tr id="trRiskClassDdl" runat="server">
                            <td class="leftField">
                                <asp:Label ID="lblPickClass" runat="server" Text="Risk/Goal class :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                               <asp:DropDownList ID="ddlPickRiskClass" runat="server" CssClass="cmbField" >                                                         
                               </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" Text="Please Select Risk Class" InitialValue="Select Risk Class" 
                                ControlToValidate="ddlPickRiskClass" Display="Dynamic" runat="server"
                                CssClass="rfvPCG" ValidationGroup="Button1">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trRiskClassTxt" runat="server">
                            <td class="leftField">
                                <asp:Label ID="lblClass" runat="server" Text="Risk/Goal class :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                               <asp:TextBox ID="txtPickRiskClass" CssClass="txtField" Text='<%# Bind( "XRC_RiskClass") %>' Enabled="false" runat="server">
                                </asp:TextBox>     
                            </td>
                        </tr>
                        <tr class="leftField">
                            <td>
                            <br />
                                <asp:Label ID="lblAUM" runat="server" Text="AUM :" CssClass="FieldName"></asp:Label>
                            </td>
                        </tr>
                        <tr>                            
                            <td class="leftField">
                                <asp:Label ID="lblMinAum" runat="server" Text="From :" CssClass="FieldName"></asp:Label>                                
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtMinAUM" runat="server" CssClass="txtField">
                                </asp:TextBox>
                            </td>
                             <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtMinAUM"
                                ErrorMessage="Please Enter Minimum AUM" Display="Dynamic" runat="server"
                                CssClass="rfvPCG" ValidationGroup="Button1">
                            </asp:RequiredFieldValidator>
                             
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtMinAUM"
                                ValidationGroup="Button1" Display="Dynamic" runat="server" CssClass="rfvPCG" 
                                 ErrorMessage="Not acceptable format" ValidationExpression="^\d*$">
                            </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblMaxAUM" runat="server" Text="To :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtMaxAUM" runat="server" CssClass="txtField">
                                </asp:TextBox>
                            </td>
                             <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtMaxAUM"
                                ErrorMessage="Please Enter Maximum AUM" Display="Dynamic" runat="server"
                                CssClass="rfvPCG" ValidationGroup="Button1">
                            </asp:RequiredFieldValidator>
                             
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtMaxAUM"
                                ValidationGroup="Button1" Display="Dynamic" runat="server" CssClass="rfvPCG" 
                                 ErrorMessage="Not acceptable format" ValidationExpression="^\d*$">
                            </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr class="leftField">
                            <td><br />
                                <asp:Label ID="lblAge" runat="server" Text="Age (years) :" CssClass="FieldName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            
                            <td class="leftField">
                                <asp:Label ID="lblMinAge" runat="server" Text="From :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtMinAge" runat="server" CssClass="txtField">
                                </asp:TextBox>                               
                            </td>
                            <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtMinAge"
                                ErrorMessage="Please Enter Minimum AUM" Display="Dynamic" runat="server"
                                CssClass="rfvPCG" ValidationGroup="Button1">
                            </asp:RequiredFieldValidator>
                             
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtMinAge"
                                ValidationGroup="Button1" Display="Dynamic" runat="server" CssClass="rfvPCG" 
                                 ErrorMessage="Not acceptable format" ValidationExpression="^\d*$">
                            </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                             <td class="leftField">
                                <asp:Label ID="lblMaxAge" runat="server" Text="To :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtMaxAge" runat="server" CssClass="txtField">
                                </asp:TextBox>
                            </td>
                            <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtMaxAge"
                                ErrorMessage="Please Enter Maximum Age" Display="Dynamic" runat="server"
                                CssClass="rfvPCG" ValidationGroup="Button1">
                            </asp:RequiredFieldValidator>
                             
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtMaxAge"
                                ValidationGroup="Button1" Display="Dynamic" runat="server" CssClass="rfvPCG" 
                                 ErrorMessage="Not acceptable format" ValidationExpression="^\d*$">
                            </asp:RegularExpressionValidator>
                            </td>
                        </tr> 
                        <tr class="leftField">
                            <td><br />
                                <asp:Label ID="lblTimeHorizon" runat="server" Text="Time Horizon :" CssClass="FieldName"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td class="leftField">
                            <asp:Label ID="lblMinTimeHorizonYear" runat="server" Text="Minimum Year:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtMinTimeHorizonYear" runat="server" CssClass="txtField">
                                </asp:TextBox>
                            </td>
                            <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtMinTimeHorizonYear"
                                ErrorMessage="Please Enter Minimum Time Horizon in Year" Display="Dynamic" runat="server"
                                CssClass="rfvPCG" ValidationGroup="Button1">
                            </asp:RequiredFieldValidator>
                             
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtMinTimeHorizonYear"
                                ValidationGroup="Button1" Display="Dynamic" runat="server" CssClass="rfvPCG" 
                                 ErrorMessage="Not acceptable format" ValidationExpression="^\d*$">
                            </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                            <asp:Label ID="lblMinTimeHorizonMonth" runat="server" Text="Minimum month:" CssClass="FieldName"></asp:Label>                            
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtMinTimeHorizonMonth" runat="server" CssClass="txtField">
                                </asp:TextBox>
                            </td>
                            <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtMinTimeHorizonMonth"
                                ErrorMessage="Please Enter Minimum Time Horizon in Month" Display="Dynamic" runat="server"
                                CssClass="rfvPCG" ValidationGroup="Button1">
                            </asp:RequiredFieldValidator>
                             
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="txtMinTimeHorizonMonth"
                                ValidationGroup="Button1" Display="Dynamic" runat="server" CssClass="rfvPCG" 
                                 ErrorMessage="Not acceptable format" ValidationExpression="^\d*$">
                            </asp:RegularExpressionValidator>
                            </td>
                        </tr>                       
                        <tr>
                            <td class="leftField">
                            <asp:Label ID="lblMaxTimeHorizonYear" runat="server" Text="Maximum Year:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtMaxTimeHorizonYear" runat="server" CssClass="txtField">
                                </asp:TextBox>
                            </td>
                            <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtMaxTimeHorizonYear"
                                ErrorMessage="Please Enter Maximum Time Horizon in Year" Display="Dynamic" runat="server"
                                CssClass="rfvPCG" ValidationGroup="Button1">
                            </asp:RequiredFieldValidator>
                             
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ControlToValidate="txtMaxTimeHorizonYear"
                                ValidationGroup="Button1" Display="Dynamic" runat="server" CssClass="rfvPCG" 
                                 ErrorMessage="Not acceptable format" ValidationExpression="^\d*$">
                            </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                            <asp:Label ID="lblMaxTimeHorizonMonth" runat="server" Text="Maximum month :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtMaxTimeHorizonMonth" runat="server" CssClass="txtField">
                                </asp:TextBox>
                            </td>
                            <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtMaxTimeHorizonMonth"
                                ErrorMessage="Please Enter Maximum Time Horizon in Month" Display="Dynamic" runat="server"
                                CssClass="rfvPCG" ValidationGroup="Button1">
                            </asp:RequiredFieldValidator>
                             
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ControlToValidate="txtMaxTimeHorizonMonth"
                                ValidationGroup="Button1" Display="Dynamic" runat="server" CssClass="rfvPCG" 
                                 ErrorMessage="Not acceptable format" ValidationExpression="^\d*$">
                            </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblDescription" runat="server" Text="Description :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" colspan="3">
                                <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" CssClass="txtField">
                                </asp:TextBox>
                            </td>
                            <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtDescription"
                                ErrorMessage="Please Enter the Variant Description" Display="Dynamic" runat="server"
                                CssClass="rfvPCG" ValidationGroup="Button1">
                            </asp:RequiredFieldValidator>
                            </td>
                        </tr> 
                        <tr class="leftField">
                            <td><br />
                                <asp:Label ID="lblAllocation" runat="server" Text="Allocation :" CssClass="FieldName"></asp:Label>
                            </td>
                        </tr> 
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblDebt" runat="server" Text="Debt :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtDebt" runat="server" CssClass="txtField">
                                </asp:TextBox>
                            </td>
                            <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtDebt"
                                ErrorMessage="Please Enter the Debt allocation" Display="Dynamic" runat="server"
                                CssClass="rfvPCG" ValidationGroup="Button1">
                            </asp:RequiredFieldValidator>
                             
                            <asp:RangeValidator ID="rvAssumptionValue" runat="server" 
                              ControlToValidate="txtDebt" CssClass="cvPCG" Display="Dynamic" 
                              ErrorMessage="Please enter value less than 100" MaximumValue="99.9" 
                              MinimumValue="0.0" Type="Double" ValidationGroup="Button1"></asp:RangeValidator>
                            </td>
                        </tr>   
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblEquity" runat="server" Text="Equity :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtEquity" runat="server" CssClass="txtField">
                                </asp:TextBox>
                            </td>
                            <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="txtEquity"
                                ErrorMessage="Please Enter the Equity allocation" Display="Dynamic" runat="server"
                                CssClass="rfvPCG" ValidationGroup="Button1">
                            </asp:RequiredFieldValidator>
                             
                            <asp:RangeValidator ID="RangeValidator1" runat="server" 
                              ControlToValidate="txtEquity" CssClass="cvPCG" Display="Dynamic" 
                              ErrorMessage="Please enter value less than 100" MaximumValue="99.9" 
                              MinimumValue="0.0" Type="Double" ValidationGroup="Button1"></asp:RangeValidator>
                            </td>                            
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblCash" runat="server" Text="Cash :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtCash" runat="server" CssClass="txtField">
                                </asp:TextBox>
                            </td>
                            <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtCash"
                                ErrorMessage="Please Enter the Cash allocation" Display="Dynamic" runat="server"
                                CssClass="rfvPCG" ValidationGroup="Button1">
                            </asp:RequiredFieldValidator>
                             
                            <asp:RangeValidator ID="RangeValidator2" runat="server" 
                              ControlToValidate="txtCash" CssClass="cvPCG" Display="Dynamic" 
                              ErrorMessage="Please enter value less than 100" MaximumValue="99.9" 
                              MinimumValue="0.0" Type="Double" ValidationGroup="Button1"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblAlternate" runat="server" Text="Alternate :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtAlternate" runat="server" CssClass="txtField">
                                </asp:TextBox>
                            </td>
                            <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="txtAlternate"
                                ErrorMessage="Please Enter the Alternate allocation" Display="Dynamic" runat="server"
                                CssClass="rfvPCG" ValidationGroup="Button1">
                            </asp:RequiredFieldValidator>
                             
                            <asp:RangeValidator ID="RangeValidator3" runat="server" 
                              ControlToValidate="txtAlternate" CssClass="cvPCG" Display="Dynamic" 
                              ErrorMessage="Please enter value less than 100" MaximumValue="99.9" 
                              MinimumValue="0.0" Type="Double" ValidationGroup="Button1"></asp:RangeValidator>
                            </td>
                        </tr>                      
                        <tr>
                        <td></td>
                            <td align="right" colspan="2">
                                <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>' ValidationGroup="Button1"
                                  CssClass="PCGButton"   runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                </asp:Button>&nbsp;
                                <asp:Button ID="Button2" CssClass="PCGButton"  Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel">
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
    <td></td>
    </tr>
<tr>
    <td>
        <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" Text="Submit" onclick="btnSubmit_Click" 
           />
    </td>
    <td></td>
    </tr>
  
</table>
  