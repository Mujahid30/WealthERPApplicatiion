<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SchemeMappingToModelPortfolio.ascx.cs" Inherits="WealthERP.Research.SchemeMappingToModelPortfolio" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager> 

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function() {
        $(".flip").click(function() { $(".panel").slideToggle(); });
    });
</script>

<telerik:RadTabStrip ID="RadTabStrip1" runat="server" EnableTheming="True" Skin="Telerik"
    EnableEmbeddedSkins="False" MultiPageID="SchemeDetailsId" 
    SelectedIndex="0">
    <Tabs>
        <telerik:RadTab runat="server" Text="Attach Schemes" 
            Value="ActiveScheme" TabIndex="0" Selected="True">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="History"
            Value="HistoryScheme" TabIndex="1" Selected="True">
        </telerik:RadTab>        
    </Tabs>
</telerik:RadTabStrip>

<telerik:RadMultiPage ID="SchemeDetailsId" EnableViewState="true" runat="server" SelectedIndex="0">
<telerik:RadPageView ID="RadPageView1" runat="server">
<asp:Panel ID="pnlSchemeAttachment" runat="server">
<table class="TableBackground" style="width: 100%;">
    <td class="HeaderTextBig" colspan="2">
        <img src="../Images/helpImage.png" height="25px" width="25px" style="float: right;"
                class="flip" />            
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <div class="panel">
                <p>
                    Add, Edit & Delete MF schemes attached to the Model Portfolio.
                    <%--<br />
                    2.Match orders to the receive transactions.--%>
                </p>
            </div>
        </td>
    </tr>   
</table>
<table id="ErrorMessage" width="100%" cellspacing="0" cellpadding="0" runat="server"
    visible="false">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage1" runat="server" visible="true" align="center">
                Please Create variant Asset allocation models.....
            </div>
        </td>
    </tr>
</table>
<table id="tblSelectddl" runat="server" class="TableBackground" width="50%">
    <tr>
        <td style="width:135Px" class="leftField">
            <asp:Label ID="lblSelectModelPortfolio" runat="server" CssClass="FieldName" Text="Select Model Portfolio:"></asp:Label>
        </td> 
        <td class="rightField">
            <asp:DropDownList ID="ddlSelectedMP" runat="server" CssClass="cmbField" 
                AutoPostBack="true" onselectedindexchanged="ddlSelectedMP_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
</table>
<table id="tblAllocation" runat="server" class="TableBackground" width="100%" visible="false">
    <tr> 
        <td style="width:135Px" class="leftField">
            <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text=""></asp:Label>
        </td>  
        <td align="left">
            <asp:Label ID="lblEquity" runat="server" Text="Equity Alloc(%)" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="lblDebt" runat="server" Text="Debt Alloc(%)" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="lblCash" runat="server" Text="Cash Alloc(%)" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="lblAlternate" runat="server" Text="Alternate Alloc(%)" CssClass="FieldName"></asp:Label>
        </td>        
    </tr>
    <tr>
        <td style="width:20%" class="leftField">
            <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Defined:"></asp:Label>
        </td>
        <td align="left" style="width:135Px">
            <asp:TextBox ID="txtEquity" runat="server" Enabled="false" CssClass="txtField">
            </asp:TextBox>
        </td>
        <td align="left" style="width:135Px">
            <asp:TextBox ID="txtDebt" runat="server" Enabled="false" CssClass="txtField">
            </asp:TextBox>
        </td>
        <td align="left" style="width:135Px">
            <asp:TextBox ID="txtCash" runat="server" Enabled="false" CssClass="txtField">
            </asp:TextBox>
        </td>
        <td align="left" style="width:135Px">
            <asp:TextBox ID="txtAlternate" runat="server" Enabled="false" CssClass="txtField">
            </asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width:130Px" class="leftField">
            <asp:Label ID="lblSelectedSchemes" runat="server" CssClass="FieldName" Text="Selected Schemes:"></asp:Label>
        </td>
        <td align="left">
            <asp:TextBox ID="txtEquitySelected" runat="server" Enabled="false" CssClass="txtField">
            </asp:TextBox>
        </td>
        <td align="left">
            <asp:TextBox ID="txtDebtSelected" runat="server" Enabled="false" CssClass="txtField">
            </asp:TextBox>
        </td>
        <td align="left">
            <asp:TextBox ID="txtCashSelected" runat="server" Enabled="false" CssClass="txtField">
            </asp:TextBox>
        </td>
        <td align="left">
            <asp:TextBox ID="txtAlternateSelected" runat="server" Enabled="false" CssClass="txtField">
            </asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width:130Px" class="leftField">
            <asp:Label ID="lblGap" runat="server" CssClass="FieldName" Text="Gap:"></asp:Label>
        </td>
        <td align="left">
            <asp:TextBox ID="txtEquityGap" runat="server" Enabled="false" CssClass="txtField">
            </asp:TextBox>
        </td>
        <td align="left">
            <asp:TextBox ID="txtDebtGap" runat="server" Enabled="false" CssClass="txtField">
            </asp:TextBox>
        </td>
        <td align="left">
            <asp:TextBox ID="txtCashGap" runat="server" Enabled="false" CssClass="txtField">
            </asp:TextBox>
        </td>
        <td align="left">
            <asp:TextBox ID="txtAlternateGap" runat="server" Enabled="false" CssClass="txtField">
            </asp:TextBox>
        </td>
    </tr>
</table>
<table id="tableGrid" runat="server" class="TableBackground" width="100%">

    <tr>
        <td>
    <telerik:RadGrid ID="RadGrid1" runat="server" Skin="Telerik" CssClass="RadGrid" GridLines="None" AllowPaging="True" 
    PageSize="20" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true" AllowAutomaticDeletes="false" 
    AllowAutomaticInserts="false" OnItemDataBound="RadGrid1_ItemDataBound" OnDataBound="RadGrid1_DataBound" 
    OnUpdateCommand="RadGrid1_UpdateCommand"  OnItemCommand="RadGrid1_ItemCommand" OnInsertCommand="RadGrid1_InsertCommand"
    OnPreRender="RadGrid1_PreRender" OnDeleteCommand="RadGrid1_DeleteCommand" OnItemCreated="RadGrid1_ItemCreated"
    AllowAutomaticUpdates="false" HorizontalAlign="NotSet" DataKeyNames="AMFMPD_Id">
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="AMFMPD_Id" EditMode="PopUp">
            <Columns>
                <telerik:GridButtonColumn CommandName="Delete" ShowInEditForm="true" Text="Delete" UniqueName="column">
                </telerik:GridButtonColumn>
                <%--<telerik:GridClientSelectColumn UniqueName="SelectColumn"/>--%>               
                <telerik:GridBoundColumn  DataField="PASP_SchemePlanName"  HeaderText="Name" UniqueName="PASP_SchemePlanName" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="AMFMPD_AllocationPercentage"  HeaderText="Weightage (%)" UniqueName="AMFMPD_AllocationPercentage">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn  DataField="PASP_SchemePlanCode" Visible="false" HeaderText="SchemePlanCode" UniqueName="PASP_SchemePlanCode">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top"  />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="AMFMPD_AddedOn"  HeaderText="Started Date" UniqueName="AMFMPD_AddedOn" DataType="System.Date"
                SortExpression="AMFMPD_AddedOn" DataFormatString="{0:d}" HtmlEncode="false">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top"/>
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="AMFMPD_SchemeDescription"  HeaderText="Description" UniqueName="AMFMPD_SchemeDescription">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  DataField="AMFMPD_IsActive"  HeaderText="AMFMPD_IsActive" UniqueName="AMFMPD_IsActive" Visible="false">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn  DataField="Flag"  HeaderText="Flag" UniqueName="Flag" Visible="false">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
              
                <%--<telerik:GridBoundColumn  DataField="EndDate"  HeaderText="End Date" UniqueName="EndDate">
                    <ItemStyle Width="" HorizontalAlign="right"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="ArchiveReason"  HeaderText="ArchiveReason" UniqueName="ArchiveReason">
                    <ItemStyle Width="" HorizontalAlign="right"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
         
                <%--<telerik:GridButtonColumn ButtonType="LinkButton" CommandName="Archive" UniqueName="EditCommandColumn1" ConfirmText="Are you sure you want to Archive the Scheme?"  
                ShowInEditForm="true" ImageUrl="../Images/Telerik/Delete.gif" Text="Archive" >
                </telerik:GridButtonColumn>--%>
                
                
                <%--<telerik:GridCheckBoxColumn UniqueName="GridCheckBoxColumn" DataField="Bool"  FooterText="CheckBox column footer" /> --%>
                <%--<telerik:GridTemplateColumn UniqueName="CheckBox" HeaderText="CheckBox Column">
                <ItemTemplate>
                    <asp:CheckBox ID="Chk" runat="server"/>
                </ItemTemplate>
                </telerik:GridTemplateColumn>--%>
                <telerik:GridEditCommandColumn UpdateText="Update" EditText="Archive" UniqueName="EditCommandColumn" CancelText="Cancel">                
                    <HeaderStyle Width="85px"></HeaderStyle>
                </telerik:GridEditCommandColumn>
               
            </Columns>                
            <EditFormSettings InsertCaption="Add new Scheme" FormTableStyle-HorizontalAlign="Center"
            PopUpSettings-Modal="true" PopUpSettings-ZIndex="80" CaptionFormatString="Archive Scheme:"
            CaptionDataField="AMFMPD_Id" EditFormType="Template">
                <FormTemplate>
                <table id="tblMain" cellspacing="1" cellpadding="1" runat="server" width="100%" border="0">
                <tr id="trDdlPickAMC" runat="server">
                    <td class="leftField">
                        <asp:Label ID="lblPickAMC" runat="server" CssClass="FieldName" Text="Pick an AMC:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlAMC" runat="server" CssClass="cmbField" 
                            AutoPostBack="true" onselectedindexchanged="ddlAMC_SelectedIndexChanged">
                        </asp:DropDownList><span id="Span1" class="spnRequiredField">*</span> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="Please Pick an AMC" InitialValue="Select AMC Code" 
                            ControlToValidate="ddlAMC" Display="Dynamic" runat="server"
                            CssClass="rfvPCG" ValidationGroup="Button1">
                        </asp:RequiredFieldValidator>
                    </td>
                   
                </tr>
                <tr id="trPickAMCtxt" runat="server">
                    <td class="leftField">
                        <asp:Label ID="lblAMC" runat="server" CssClass="FieldName" Text="Pick an AMC:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtAMC" runat="server" CssClass="txtField" Enabled="false" Text='<%# Bind( "PA_AMCName") %>'></asp:TextBox>
                    </td>
                </tr>                
                <tr id="trddlCategory" runat="server">
                    <td class="leftField">
                        <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" Text="Category:"></asp:Label>
                    </td>
                    <td>                        
                       
                        <asp:DropDownList CssClass="cmbField" ID="ddlCategory" runat="server" 
                            AutoPostBack="true" onselectedindexchanged="ddlCategory_SelectedIndexChanged">
                            <asp:ListItem Text="All Category" Value="All"></asp:ListItem>
                            <asp:ListItem Text="commodity" Value="MFCO"></asp:ListItem>
                            <asp:ListItem Text="Debt" Value="MFDT"></asp:ListItem>
                            <asp:ListItem Text="Equity" Value="MFEQ"></asp:ListItem>
                            <asp:ListItem Text="Hybrid" Value="MFHY"></asp:ListItem>
                        </asp:DropDownList>
                    </td> 
                </tr> 
                <tr id="trTxtCategory" runat="server">
                    <td class="leftField">
                        <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Category:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="txtField" Enabled="false" Text='<%# Bind( "PAIC_AssetInstrumentCategoryName") %>'></asp:TextBox>
                    </td>
                </tr>
                <tr id="divSubCategory" runat="server"  visible="false">
                     <td class="leftField">
                        <asp:Label ID="lblSubCategory" runat="server" CssClass="FieldName" Text="Sub Category:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="cmbField" 
                            AutoPostBack="true" 
                            onselectedindexchanged="ddlSubCategory_SelectedIndexChanged">
                        </asp:DropDownList>                       
                    </td>  
                </tr>
                <tr id="trTxtSubCategory" runat="server">
                    <td class="leftField">
                        <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Sub Category:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="txtField" Enabled="false" Text='<%# Bind( "PAISC_AssetInstrumentSubCategoryName") %>'></asp:TextBox>
                    </td>
                </tr>
                                
                <tr id="trddlScheme" runat="server">
                    <td class="leftField">
                        <asp:Label ID="lblScheme" runat="server" CssClass="FieldName" Text="Scheme:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField">
                        </asp:DropDownList><span id="Span2" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Text="Please select a scheme" InitialValue="Select" 
                            ControlToValidate="ddlScheme" Display="Dynamic" runat="server"
                            CssClass="rfvPCG" ValidationGroup="Button1">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr id="trTxtScheme" runat="server">
                    <td class="leftField">
                        <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Scheme:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="txtField" Enabled="false" Text='<%# Bind( "PASP_SchemePlanName") %>'></asp:TextBox>
                    </td>
                </tr>
                <tr id="trAddWeightage" runat="server">
                    <td class="leftField">
                        <asp:Label ID="lblWeightage" runat="server" CssClass="FieldName" Text="Weightage(%):"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtWeightage" runat="server" CssClass="txtField" Text='<%# Bind( "AMFMPD_AllocationPercentage") %>'></asp:TextBox>
                        <span id="Span3" class="spnRequiredField">*</span>
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtWeightage"
                                ErrorMessage="Enter the Weightage" Display="Dynamic" runat="server"
                                CssClass="rfvPCG" ValidationGroup="Button1">
                            </asp:RequiredFieldValidator>
                             
                            <asp:RangeValidator ID="rvWeightage" runat="server" 
                              ControlToValidate="txtWeightage" CssClass="cvPCG" Display="Dynamic" 
                              ErrorMessage="Please enter value less than 100" MaximumValue="99.9" 
                              MinimumValue="0.0" Type="Double" ValidationGroup="Button1"></asp:RangeValidator>
                    </td>                   
                </tr> 
                <%--<tr id="trEditWeightage" runat="server">
                    <td class="leftField">
                        <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Weightage(%)"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox4" runat="server" CssClass="txtField" Enabled="false" Text='<%# Bind( "AMFMPD_AllocationPercentage") %>'></asp:TextBox>                       
                    </td>                   
                </tr> --%>
                <tr id="trAddDescription" runat="server">
                    <td class="leftField">
                        <asp:Label ID="lblSchemeDescription" runat="server" CssClass="FieldName" Text="Description:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtSchemeDescription" runat="server" CssClass="txtField" 
                        Text='<%# Bind( "AMFMPD_SchemeDescription") %>' TextMode="MultiLine"></asp:TextBox>
                    </td>                    
                </tr>  
                <%--<tr id="trEditDescription" runat="server">
                    <td class="leftField">
                        <asp:Label ID="Label5" runat="server" CssClass="FieldName" Text="Description:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="TextBox5" runat="server" CssClass="txtField" 
                        Text='<%# Bind( "AMFMPD_SchemeDescription") %>' Enabled="false" TextMode="MultiLine"></asp:TextBox>
                    </td>                    
                </tr>--%>
                <tr id="trArchive" runat="server">
                    <td class="leftField">
                        <asp:Label ID="lblArchive" runat="server" CssClass="FieldName" Text="Reason for Archiving:"></asp:Label>
                    </td>
                    <td class="rightField">                         
                        <asp:DropDownList ID="ddlArchive" runat="server" CssClass="cmbField"></asp:DropDownList>
                        <span id="Span4" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Text="Please select an archive reason" InitialValue="0" 
                            ControlToValidate="ddlArchive" Display="Dynamic" runat="server"
                            CssClass="rfvPCG" ValidationGroup="Button1">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>             
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>' CssClass="PCGButton"
                          runat="server" ValidationGroup="Button1" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                        </asp:Button>&nbsp;
                        <asp:Button ID="Button2" Text="Cancel" CssClass="PCGButton" runat="server" CausesValidation="False" CommandName="Cancel">
                        </asp:Button>
                    </td>
                </tr>
            </table>
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
                </FormTemplate>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings>
         <ClientEvents />

            <%--<ClientEvents OnRowDblClick="RowDblClick" />--%>
        </ClientSettings>
    </telerik:RadGrid> 
</td>
<td>
</td> 
    </tr>
    <tr>
    <td>   
    </td>
   <td>
   </td>
    </tr>
    <tr>
    <td>
        <asp:Button ID="btnCancel" runat="server" CssClass="PCGButton" Text="Cancel" 
            onclick="btnCancel_Click"/>
        <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" Text="Submit" 
            onclick="btnSubmit_Click"/>
        <%--<asp:Button ID="btnArchive" runat="server" Text="Archive" CssClass="PCGButton" 
            onclick="btnArchive_Click" />--%>
    </td>
    <td>
        
        <%--<cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlWindow"
            TargetControlID="hdnTempId" DynamicServicePath="" BackgroundCssClass="modalBackground"
            Enabled="True" OkControlID="btnOk" PopupDragHandleControlID="pnlWindow" CancelControlID="btnCancel" Drag="true">
        </cc1:ModalPopupExtender>--%>         
    </td>
    </tr> 
    <tr>
        <td>
            <%--<asp:Panel ID="pnlWindow" runat="server" CssClass="ModelPup" >
                <table>
                    <tr>
                        <td class="leftField">
                        <asp:Label ID="lblArchive" runat="server" CssClass="FieldName" Text="Reason for Archiving:"></asp:Label>
                        </td>
                        <td class="rightField">                         
                            <asp:DropDownList ID="ddlArchive" runat="server" CssClass="cmbField" 
                                onselectedindexchanged="ddlArchive_SelectedIndexChanged">               
                            </asp:DropDownList>
                        </td>
                    </tr>                    
                    <tr id="trArchiveWeightage" runat="server">
                        <td class="leftField">
                            <asp:Label ID="lblArchiveWeightage" runat="server" CssClass="FieldName" Text="Weightage(%)"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtArchiveWeightage" runat="server" CssClass="txtField" Text='<%# Bind( "AMFMPD_AllocationPercentage") %>'></asp:TextBox>                       
                        </td>
                    </tr>                     
                    <tr>
                        <td class="leftField">
                            <asp:Label ID="lblReasonDescription" runat="server" CssClass="FieldName" Text="Archive Reason Description:"></asp:Label>
                        </td>
                        <td class="rightField">
                            <asp:TextBox ID="txtReason" runat="server" CssClass="txtField" TextMode="MultiLine"></asp:TextBox>
                        </td>                    
                    </tr>
                </table>
           
                <asp:Button ID="btnOk" runat="server" Text="Ok" CausesValidation="false" CssClass="PCGButton" />
                &nbsp;
                <asp:Button ID="btnCancel"  CausesValidation="false" runat="server" Text="Cancel" CssClass="PCGButton" />
            </asp:Panel>--%>
        </td>
    </tr> 
</table>
<table class="TableBackground" id="tblPieChart" runat="server" width="100%">
<tr>
    <td>
        <asp:Label ID="lblCategoryChart" runat="server" CssClass="HeaderTextSmall" Text="Asset allocation category-wise:"></asp:Label>
    </td>
    <td>
        <asp:Label ID="lblSubCategoryChart" runat="server" CssClass="HeaderTextSmall" Text="Asset allocation subcategory-wise:"></asp:Label>
    </td>
</tr>
<tr>
    <td style="width:"50%">
        <asp:Chart ID="ChartAsset" runat="server" BackColor="Transparent" 
            Height="250px" Width="450px">
            <Series>
                <asp:Series Name="Series"></asp:Series>
            </Series>            
            <ChartAreas>
                <asp:ChartArea BackColor="Transparent" Name="ChartArea1"></asp:ChartArea>
            </ChartAreas>
         </asp:Chart>
        
    </td>
    <td style="width:"50%">
        <asp:Chart ID="Chart1" runat="server" BackColor="Transparent" 
            Height="250px" Width="450px">
            <Series>
                <asp:Series Name="Series"></asp:Series>
            </Series>
            
            <ChartAreas>
                <asp:ChartArea BackColor="Transparent" Name="ChartArea1"></asp:ChartArea>
            </ChartAreas>
         </asp:Chart>
    </td>
</tr>    
</table>
<table id="tableNote" runat="server" style="width: 100%;" class="TableBackground">
<tr>
    <td style="width:20px">
        <asp:Label ID="Label5" runat="server" CssClass="FieldName" Text="Note:"></asp:Label>
    </td>
    <td>
        <asp:Label ID="lblCaption" runat="server" CssClass="FieldName" Text="1. The total weightage across the screen must be 100%."></asp:Label>
    </td>
</tr>
<tr>
    <td>
        &nbsp;
    </td>
    <td>
        <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="2. Use Archive to remove an existing scheme & their weightage."></asp:Label>
    </td>
</tr>
</table>
<asp:HiddenField ID="hdnSubCategory" runat="server" />  
<asp:HiddenField ID="hdnWeightage" runat="server" />  
<asp:HiddenField ID="hdnTempId" runat="server" />
</asp:Panel>
</telerik:RadPageView>

<telerik:RadPageView ID="RadPageView2" runat="server">
<asp:Panel ID="pnlHystoryGrid" runat="server">

<table class="TableBackground" style="width: 100%;">
    <td class="HeaderTextBig" colspan="2">
        <img src="../Images/helpImage.png" height="25px" width="25px" style="float: right;"
                class="flip" />            
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <div class="panel">
                <p>
                    View past schemes in the Model Portfolio.
                    <%--<br />
                    2.Match orders to the receive transactions.--%>
                </p>
            </div>
        </td>
    </tr>   
</table>
<table>
    <tr>
        <td>
               
        <telerik:RadGrid ID="histryRadGrid" runat="server" Skin="Telerik" CssClass="RadGrid" GridLines="None" AllowPaging="True" 
            PageSize="20" AllowSorting="False" AutoGenerateColumns="False" ShowStatusBar="False" AllowAutomaticDeletes="false" 
            AllowAutomaticInserts="false" AllowAutomaticUpdates="false" HorizontalAlign="NotSet" Width="100%">
        <MasterTableView>
            <Columns>                             
                <telerik:GridBoundColumn  DataField="PASP_SchemePlanName"  HeaderText="Name" UniqueName="PASP_SchemePlanName" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="AMFMPD_AllocationPercentage"  HeaderText="Weightage (%)" UniqueName="AMFMPD_AllocationPercentage">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <%--<telerik:GridBoundColumn  DataField="PASP_SchemePlanCode" Visible="false" HeaderText="SchemePlanCode" UniqueName="PASP_SchemePlanCode">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top"  />
                </telerik:GridBoundColumn>--%>
                
                <telerik:GridBoundColumn  DataField="AMFMPD_AddedOn"  HeaderText="Started Date" UniqueName="AMFMPD_AddedOn"
                SortExpression="AMFMPD_AddedOn" DataFormatString="{0:d}" HtmlEncode="False">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top"/>
                </telerik:GridBoundColumn>                
                
                <telerik:GridBoundColumn  DataField="AMFMPD_RemovedOn"  HeaderText="End Date" UniqueName="AMFMPD_RemovedOn" 
                SortExpression="AMFMPD_RemovedOn" HtmlEncode="false">
                    <ItemStyle Width="" HorizontalAlign="right"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="XAR_ArchiveReason"  HeaderText="ArchiveReason" UniqueName="XAR_ArchiveReason">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="AMFMPD_SchemeDescription"  HeaderText="Description" UniqueName="AMFMPD_SchemeDescription">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
            </Columns>            
        </MasterTableView>
        <ClientSettings>
        <ClientEvents />
            <%--<ClientEvents OnRowDblClick="RowDblClick" />--%>
        </ClientSettings>
    </telerik:RadGrid> 
        </td>
    </tr>
</table>
</asp:Panel>
<asp:Panel runat="server" ID="pnlNoRecoredFound" width="100%" Visible="false">
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
</telerik:RadPageView>

</telerik:RadMultiPage>