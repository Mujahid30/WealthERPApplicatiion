<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerCategorySetup.ascx.cs" Inherits="WealthERP.AdvsierPreferenceSettings.CustomerCategorySetup" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<script type="text/javascript" language="javascript">
    function keyPress(sender, args) {
        if (args.keyCode == 13) {
            return false;
        }
    }

</script>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager> 

<table width="100%">
<tr>
<td colspan="3" style="width: 100%;">
<div class="divPageHeading">
    <table cellspacing="0"  width="100%">
        <tr>
        <td align="left">Associate Category</td>
        <td  align="right" id="tdGoalExport" runat="server" style="padding-bottom:2px;">
        </td>
        </tr>
    </table>
</div>
</td>
</tr>
</table>

<table id="tableGrid" runat="server" width="40%">
<tr>
    <td>
     <telerik:RadGrid ID="gvCustomerCategory" runat="server" Skin="Telerik" CssClass="RadGrid" GridLines="None" AllowPaging="True" 
    PageSize="10" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true" AllowAutomaticDeletes="false" 
    AllowAutomaticInserts="false" OnItemCommand="gvCustomerCategory_ItemCommand" OnItemDataBound="gvCustomerCategory_ItemDataBound" OnNeedDataSource="gvCustomerCategory_OnNeedDataSource"
    AllowAutomaticUpdates="false" HorizontalAlign="NotSet" DataKeyNames="ACC_CustomerCategoryCode">
        <MasterTableView CommandItemDisplay="Top" EditMode="PopUp" DataKeyNames="ACC_CustomerCategoryCode">
            <Columns>
                 <telerik:GridEditCommandColumn EditText="Update" UniqueName="editColumn" CancelText="Cancel"
                        UpdateText="Update">
                    </telerik:GridEditCommandColumn>
                                 
                <telerik:GridBoundColumn UniqueName="ACC_CustomerCategoryCode" HeaderText="Category Code" DataField="ACC_CustomerCategoryCode">
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn UniqueName="ACC_customerCategoryName" HeaderText="Category Name" DataField="ACC_customerCategoryName">
                </telerik:GridBoundColumn>
                          
                <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete this Category?"
                        ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                        Text="Delete">
                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                    </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings InsertCaption="Add" FormTableStyle-HorizontalAlign="Center" CaptionFormatString="Edit" FormCaptionStyle-CssClass="TableBackground"
            PopUpSettings-Modal="true" PopUpSettings-ZIndex="20" EditFormType="Template" FormCaptionStyle-Width="100%" 
            PopUpSettings-Height="200px" PopUpSettings-Width="300px">            
                <FormTemplate>
                    <table>
                      <tr id="trAddCategory" runat="server" >
                        <td class="leftField">
                            <asp:Label ID="lblCategoryCode" runat="server" Text="CategoryCode :" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField" onkeypress="return keyPress(this, event)">
                            <asp:TextBox ID="txtCategoryCode" CssClass="txtField" Text='<%# Bind( "ACC_CustomerCategoryCode") %>' Enabled="false" runat="server">
                            </asp:TextBox>                              
                        </td>
                        
                      </tr>
                      <tr id="trRiskClassTxt" runat="server">
                        <td class="leftField">
                            <asp:Label ID="lblCategoryName" runat="server" Text="CategoryName :" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField" onkeypress="return keyPress(this, event)">
                           <asp:TextBox ID="txtCategoryName" CssClass="txtField" Text='<%# Bind( "ACC_customerCategoryName") %>'  runat="server">
                           </asp:TextBox><span id="Span6" class="spnRequiredField">*</span> 
                        </td>
                        <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCategoryName"
                            ErrorMessage="<br />Please Enter Category name " Display="Dynamic" runat="server"
                            CssClass="rfvPCG" ValidationGroup="Button1">
                        </asp:RequiredFieldValidator>
                        </td>
                      </tr>

                      <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>' ValidationGroup="Button1" 
                                  CssClass="PCGButton"   runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' >
                                </asp:Button>&nbsp;
                                <asp:Button ID="Button2" CssClass="PCGButton" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                </asp:Button>
                            </td>
                            <td>
                            </td>
                        </tr>
                      </table>
                </FormTemplate>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings>
        <ClientEvents />
        </ClientSettings>
    </telerik:RadGrid>     
    </td>
    <td></td>
    </tr>

</table>
<asp:HiddenField ID="hdnButtonText" runat="server" />
