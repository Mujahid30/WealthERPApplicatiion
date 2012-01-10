<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SchemeMappingToModelPortfolio.ascx.cs" Inherits="WealthERP.Research.SchemeMappingToModelPortfolio" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function() {
        if ($('.Hello').Click()) {
            alert('Hello');
        }
    });
</script>

<script type="text/javascript">


    function getControl() {
        alert(1);

        var key = document.getElementById('ctrl_SchemeMappingToModelPortfolio_RadGrid1_ctl00_ctl04_lnkArchive');

        alert(key.ID);

        var modalpop = document.getElementById("<%=ModalPopupExtender1.ClientID %>");
        modalpop.TargetControlID = key;
        
    }

</script>

<asp:ScriptManager ID="scriptmanager1" runat="server">
</asp:ScriptManager>


<%--
<script type="text/javascript">
    function showmessage() {
        if (confirm("Are you sure you want to delete this child customer?")) {
            document.getElementById("ctrl_AddProspectList_hdnMsgValue").value = 1;
            document.getElementById("ctrl_AddProspectList_hiddenassociation").click();
            return true;
        }
        else {
            document.getElementById("ctrl_AddProspectList_hdnMsgValue").value = 0;
            document.getElementById("ctrl_AddProspectList_hiddenassociation").click();
            return false;
        }

    }
</script>--%>


<table class="TableBackground" style="width: 100%;">
    <tr>
        <td>
            <asp:Label ID="lblAttatchScheme" runat="server" CssClass="HeaderTextSmall" Text="Attatch Scheme to Portfolio"></asp:Label>
        </td>
    </tr>
          
</table>
<table id="tblSelectddl" runat="server" class="TableBackground" width="40%">
<tr>
    <td><br /></td>
</tr>
<tr>
    <td class="leftField">
        <asp:Label ID="lblSelectModelPortfolio" runat="server" CssClass="FieldName" Text="Select Model Portfolio:"></asp:Label>
    </td> 
    <td class="rightField">
        <asp:DropDownList ID="ddlSelectedMP" runat="server" CssClass="cmbField" 
            AutoPostBack="true" onselectedindexchanged="ddlSelectedMP_SelectedIndexChanged">
        </asp:DropDownList>
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
               
                <telerik:GridEditCommandColumn UpdateText="Update" UniqueName="EditCommandColumn"
                                    CancelText="Cancel" ButtonType="ImageButton" CancelImageUrl="../Images/Telerik/Cancel.gif"
                                    InsertImageUrl="../Images/Telerik/Update.gif" UpdateImageUrl="../Images/Telerik/Update.gif"
                                    EditImageUrl="../Images/Telerik/Edit.gif">
                                    <HeaderStyle Width="85px"></HeaderStyle>
                 </telerik:GridEditCommandColumn>
                  <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmText="Are you sure you want to delete the Scheme?"  ShowInEditForm="true"
                ImageUrl="../Images/Telerik/Delete.gif"
                Text="Delete" UniqueName="column">
                </telerik:GridButtonColumn>
                <%--<telerik:GridClientSelectColumn UniqueName="SelectColumn"/>--%>               
                <telerik:GridBoundColumn  DataField="PASP_SchemePlanName"  HeaderText="Name" UniqueName="PASP_SchemePlanName" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="AMFMPD_AllocationPercentage"  HeaderText="Weightage" UniqueName="AMFMPD_AllocationPercentage">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn  DataField="PASP_SchemePlanCode" Visible="false" HeaderText="SchemePlanCode" UniqueName="PASP_SchemePlanCode">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top"  />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="AMFMPD_AddedOn"  HeaderText="Started Date" UniqueName="AMFMPD_AddedOn">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="AMFMPD_SchemeDescription"  HeaderText="Description" UniqueName="AMFMPD_SchemeDescription">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <%--<telerik:GridBoundColumn  DataField="EndDate"  HeaderText="End Date" UniqueName="EndDate">
                    <ItemStyle Width="" HorizontalAlign="right"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="ArchiveReason"  HeaderText="ArchiveReason" UniqueName="ArchiveReason">
                    <ItemStyle Width="" HorizontalAlign="right"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
         
              <%--  <telerik:GridButtonColumn ButtonType="LinkButton" ButtonCssClass="Hello" CommandName="Archive" UniqueName="Archive" ConfirmText="Are you sure you want to Archive the Scheme?"  
                ShowInEditForm="true" ImageUrl="../Images/Telerik/Delete.gif" Text="Archive" >
                </telerik:GridButtonColumn>
                --%>
                
                <%--<telerik:GridCheckBoxColumn UniqueName="GridCheckBoxColumn" DataField="Bool"  FooterText="CheckBox column footer" /> --%>
                <telerik:GridTemplateColumn UniqueName="Hello" HeaderText="CheckBox Column">
                <ItemTemplate>
                    <asp:CheckBox ID="Chk" runat="server"/>
                </ItemTemplate>
                </telerik:GridTemplateColumn>
          
               
            </Columns>
            
                
            <EditFormSettings InsertCaption="Add new Scheme" FormTableStyle-HorizontalAlign="Center"
            PopUpSettings-Modal="true" PopUpSettings-ZIndex="80" CaptionFormatString="Edit Risk ClassCode: {0}"
            CaptionDataField="AMFMPD_Id" EditFormType="Template">
                <FormTemplate>
                <table id="tblMain" cellspacing="1" cellpadding="1" runat="server" width="100%" border="0">
                <tr id="trDdlPickAMC" runat="server">
                    <td class="leftField">
                        <asp:Label ID="lblPickAMC" runat="server" CssClass="FieldName" Text="Pick an AMC"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlAMC" runat="server" CssClass="cmbField" 
                            AutoPostBack="true" onselectedindexchanged="ddlAMC_SelectedIndexChanged">
                        </asp:DropDownList>                        
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
                        <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" Text="Category"></asp:Label>
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
                 <%--   <div id="divSubCategory" runat="server" visible="false"> </div>      --%>
                <tr id="divSubCategory" runat="server"  visible="false">
                     <td class="leftField">
                        <asp:Label ID="lblSubCategory" runat="server" CssClass="FieldName" Text="Sub Category"></asp:Label>
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
                        <asp:Label ID="lblScheme" runat="server" CssClass="FieldName" Text="Scheme"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField">
                        </asp:DropDownList>                                          
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
                    <tr>
                    <td class="leftField">
                        <asp:Label ID="lblWeightage" runat="server" CssClass="FieldName" Text="Weightage(%)"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtWeightage" runat="server" CssClass="txtField" Text='<%# Bind( "AMFMPD_AllocationPercentage") %>'></asp:TextBox>                       
                    </td>
                    <%--<td class="leftField">
                        <asp:Label ID="lblArchive" runat="server" CssClass="FieldName" Text="Reason for Archiving:"></asp:Label>
                    </td>
                    <td class="rightField">                         
                        <asp:DropDownList ID="ddlArchive" runat="server" CssClass="cmbField">               
                        </asp:DropDownList>
                    </td>--%>
                </tr>  
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblSchemeDescription" runat="server" CssClass="FieldName" Text="Description:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtSchemeDescription" runat="server" CssClass="txtField" Text='<%# Bind( "AMFMPD_SchemeDescription") %>' TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td>
                        <%--<asp:Label ID="lblEndDate" runat="server" CssClass="FieldName" Text="EndDate"></asp:Label>--%>
                    </td>
                    <td></td>
                </tr>
               <%-- <tr>                    
                    <td></td>
                    <td class="rightField">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton"/>
                    </td>
                </tr>--%>  
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                         CssClass="PCGButton"   runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
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
           <%-- <telerik:RadWindow ID="Radwindow" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddd" runat="server">
                        <asp:ListItem Text="Hi" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </ContentTemplate>
            </telerik:RadWindow>--%>
        </td>
       
    </tr>
    <tr>
    <td>
            <asp:Button ID="btnArchive" runat="server" Text="Archive" CssClass="PCGButton" 
                onclick="btnArchive_Click" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlWindow"
                TargetControlID="hdnTempId" DynamicServicePath="" BackgroundCssClass="modalBackground"
                Enabled="True" OkControlID="btnOk" PopupDragHandleControlID="pnlWindow" CancelControlID="btnCancel" Drag="true">
            </cc1:ModalPopupExtender> 
    </td>
       <td>
            <asp:Panel ID="pnlWindow" runat="server" CssClass="ModelPup" >
            
                <table>
                    <tr>
                        <td class="leftField">
                        <asp:Label ID="lblArchive" runat="server" CssClass="FieldName" Text="Reason for Archiving:"></asp:Label>
                        </td>
                        <td class="rightField">                         
                            <asp:DropDownList ID="ddlArchive" runat="server" CssClass="cmbField">               
                            </asp:DropDownList>
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
           
                <asp:Button ID="btnOk" runat="server" Text="Download" CausesValidation="false" CssClass="PCGButton" />
                &nbsp;
                <asp:Button ID="btnCancel"  CausesValidation="false" runat="server" Text="Cancel" CssClass="PCGButton" />
            
            </asp:Panel>
       </td>
    </tr>
    <tr>
    <td>
        <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" Text="Submit" 
            onclick="btnSubmit_Click"/>
    </td>
    <td></td>
    </tr>  
</table>
<table class="TableBackground" id="tblPieChart" runat="server" width="100%">
<tr>
    <td style="width:"50%">
        <asp:Chart ID="ChartAsset" runat="server" BackColor="Transparent" 
            Height="250px" Width="450px">
            <Series>
                <asp:Series Name="Series"></asp:Series>
            </Series>            
            <ChartAreas>
                <asp:ChartArea BackColor="#EBEFF9" Name="ChartArea1"></asp:ChartArea>
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
                <asp:ChartArea BackColor="#EBEFF9" Name="ChartArea1"></asp:ChartArea>
            </ChartAreas>
         </asp:Chart>
    </td>
</tr>    
</table>
    <asp:HiddenField ID="hdnSubCategory" runat="server" />  
       <asp:HiddenField ID="hdnWeightage" runat="server" />  
       <asp:HiddenField ID="hdnTempId" runat="server" />