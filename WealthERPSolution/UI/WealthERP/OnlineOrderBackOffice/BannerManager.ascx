<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BannerManager.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.BannerManager" %>
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
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Advertisement Manager
                        </td>
                        <td align="right" id="td4" runat="server" style="padding-bottom: 2px;">
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<telerik:RadTabStrip ID="RadTabStripAdsUpload" runat="server" EnableTheming="True"
    Skin="Telerik" EnableEmbeddedSkins="False" MultiPageID="multipageAdsUpload" SelectedIndex="0">
    <Tabs>
        <telerik:RadTab runat="server" Text="Banner Management" Value="Banner" TabIndex="0">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Scroller Management" Value="Scroller">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Demo Video Management" Value="Demo">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="FAQ Management" Value="FAQ">
        </telerik:RadTab>
    </Tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="multipageAdsUpload" EnableViewState="true" runat="server">
    <telerik:RadPageView ID="rpvBanner" runat="server">
        <asp:Panel ID="Banner">
            <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

                <script type="text/javascript">
                    var uploadedFilesCount = 0;
                    var isEditMode;
                    function validateRadUpload(source, e) {
                        // When the RadGrid is in Edit mode the user is not obliged to upload file.
                        if (isEditMode == null || isEditMode == undefined) {
                            e.IsValid = false;

                            if (uploadedFilesCount > 0) {
                                e.IsValid = true;
                            }
                        }
                        isEditMode = null;
                    }

                    function OnClientFileUploaded(sender, eventArgs) {
                        uploadedFilesCount++;
                    }
             
                </script>

            </telerik:RadCodeBlock>
            <table id="tableGrid" runat="server" width="40%">
                <tr>
                    <td>
                        <telerik:RadGrid ID="RadGrid1" runat="server" Skin="Telerik" CssClass="RadGrid" GridLines="None"
                            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                            AllowAutomaticDeletes="false" AllowAutomaticInserts="false" OnItemCreated="RadGrid1_ItemCreated"
                            PageSize="3" OnInsertCommand="RadGrid1_InsertCommand" OnNeedDataSource="RadGrid1_NeedDataSource"
                            OnDeleteCommand="RadGrid1_DeleteCommand" OnUpdateCommand="RadGrid1_UpdateCommand"
                            OnItemCommand="RadGrid1_ItemCommand" OnItemDataBound="RadGrid1_ItemDataBound"
                            AllowAutomaticUpdates="false" HorizontalAlign="NotSet" DataKeyNames="PBD_Id,PDB_ExpiryDate,PAG_AssetGroupCode">
                            <MasterTableView CommandItemDisplay="Top" EditMode="PopUp" DataKeyNames="PBD_Id,PDB_ExpiryDate,PAG_AssetGroupCode">
                                <Columns>
                                    <telerik:GridEditCommandColumn EditText="Update" UniqueName="editColumn" CancelText="Cancel"
                                        UpdateText="Update">
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridBoundColumn UniqueName="PAG_AssetGroupName" HeaderText="Asset Group"
                                        DataField="PAG_AssetGroupName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="PDB_ExpiryDate" HeaderText="Expiry Date" DataField="PDB_ExpiryDate">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="PDB_BannerImage" HeaderText="image Name" DataField="PDB_BannerImage">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete this Banner?"
                                        ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                                        Text="Delete">
                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <EditFormSettings InsertCaption="Add" FormTableStyle-HorizontalAlign="Center" CaptionFormatString="Edit"
                                    FormCaptionStyle-CssClass="TableBackground" PopUpSettings-Modal="true" PopUpSettings-ZIndex="20"
                                    EditFormType="Template" FormCaptionStyle-Width="100%" PopUpSettings-Height="300px"
                                    PopUpSettings-Width="500px">
                                    <FormTemplate>
                                        <table>
                                            <tr id="trAddCategory">
                                                <td class="leftField">
                                                    <asp:Label ID="lblAssetGroup" runat="server" Text="AssetGroup:" CssClass="FieldName"></asp:Label>
                                                </td>
                                                <td class="rightField">
                                                    <asp:DropDownList ID="ddlAssetGroupName" runat="server" CssClass="cmbLongField" DataValueField='<%# Eval("PAG_AssetGroupCode") %>'>
                                                        <asp:ListItem Selected="True" Value="0">SELECT</asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="IP">IPO</asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="FI">BOND</asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="MF">Mutual Fund</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <span id="Span2" class="spnRequiredField">*</span>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" ControlToValidate="ddlAssetGroupName"
                                                        ErrorMessage="Please,Select an AssetGroup." InitialValue="0" ValidationGroup='<%# (Container is GridEditFormInsertItem) ? "btnInsertGroup" : "btnUpdateGroup" %>'
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr id="trRiskClassTxt">
                                                <td class="leftField">
                                                    <asp:Label ID="lblExpireDate" runat="server" Text="ExpireDate:" CssClass="FieldName"></asp:Label>
                                                </td>
                                                <td class="rightField">
                                                    <telerik:RadDateTimePicker runat="server" ID="dtpExpireDate">
                                                        <Calendar ID="Calendar4" runat="server" EnableKeyboardNavigation="true">
                                                        </Calendar>
                                                    </telerik:RadDateTimePicker>
                                                    <span id="Span6" class="spnRequiredField">*</span>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="dtpExpireDate"
                                                        ValidationGroup='<%# (Container is GridEditFormInsertItem) ? "btnInsertGroup" : "btnUpdateGroup" %>'
                                                        ErrorMessage="Please select an Expire Date" Display="Dynamic" runat="server"
                                                        CssClass="rfvPCG">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr id="tr1">
                                                <td class="leftField">
                                                </td>
                                                <td class="rightField">
                                                    <asp:FileUpload ID="FileUpload" runat="server" Height="22px" />
                                                    <span id="Span1" class="spnRequiredField">*</span>
                                                    <br />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="FileUpload"
                                                        runat="Server" ValidationGroup="btnInsertGroup" ErrorMessage="Only .jpeg,.jpg, .gif and.png File allowed"
                                                        Display="Dynamic" ValidationExpression="^.*\.((j|J)(p|P)(e|E)(g|G)|(j|J)(p|P)(g|G)|(g|G)(i|I)(f|F)|(p|P)(n|N)(g|G))$"
                                                        CssClass="rfvPCG" />
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="FileUpload_RequiredFieldValidator" ControlToValidate="FileUpload"
                                                        ValidationGroup="btnInsertGroup" ErrorMessage="Please select an image for upload."
                                                        Display="Dynamic" runat="server" CssClass="rfvPCG">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="2">
                                                    <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                        ValidationGroup='<%# (Container is GridEditFormInsertItem) ? "btnInsertGroup" : "btnUpdateGroup" %>'
                                                        CssClass="PCGButton" runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                    </asp:Button>&nbsp;
                                                    <asp:Button ID="Button2" CssClass="PCGButton" Text="Cancel" runat="server" CausesValidation="False"
                                                        CommandName="Cancel"></asp:Button>
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
                    <td>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdnButtonText" runat="server" />
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="rpvScroller" runat="server">
        <asp:Panel ID="Scroller">
            <telerik:RadGrid ID="RadGrid2" runat="server" Skin="Telerik" CssClass="RadGrid" GridLines="None"
                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                AllowAutomaticDeletes="false" AllowAutomaticInserts="false" PageSize="3" OnInsertCommand="RadGrid2_InsertCommand"
                OnNeedDataSource="RadGrid2_NeedDataSource" OnDeleteCommand="RadGrid2_DeleteCommand"
                OnUpdateCommand="RadGrid2_UpdateCommand" OnItemCommand="RadGrid2_ItemCommand"
                OnItemDataBound="RadGrid2_ItemDataBound" AllowAutomaticUpdates="false" HorizontalAlign="NotSet"
                DataKeyNames="PUHD_Id,PUHD_IsActive,PAG_AssetGroupCode,PUHD_HelpDetails">
                <MasterTableView CommandItemDisplay="Top" EditMode="PopUp" DataKeyNames="PUHD_Id,PUHD_IsActive,PAG_AssetGroupCode,PUHD_HelpDetails">
                    <Columns>
                        <telerik:GridEditCommandColumn EditText="Update" UniqueName="editColumn" CancelText="Cancel"
                            UpdateText="Update">
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn UniqueName="PAG_AssetGroupName" HeaderText="Asset Group"
                            DataField="PAG_AssetGroupName">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="PUHD_HelpDetails" HeaderText="Scroller Text"
                            DataField="PUHD_HelpDetails">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="PUHD_IsActive" HeaderText="Is Active" DataField="PUHD_IsActive">
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete this ?"
                            ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                            Text="Delete">
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings InsertCaption="Add" FormTableStyle-HorizontalAlign="Center" CaptionFormatString="Edit"
                        FormCaptionStyle-CssClass="TableBackground" PopUpSettings-Modal="true" PopUpSettings-ZIndex="20"
                        EditFormType="Template" FormCaptionStyle-Width="100%" PopUpSettings-Height="300px"
                        PopUpSettings-Width="500px">
                        <FormTemplate>
                            <table>
                                <tr id="trAddCategory">
                                    <td class="leftField">
                                        <asp:Label ID="lblAssetGroup1" runat="server" Text="AssetGroup:" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td class="rightField">
                                        <asp:DropDownList ID="ddlAssetGroupName1" runat="server" CssClass="cmbLongField"
                                            DataValueField='<%# Eval("PAG_AssetGroupCode") %>'>
                                            <asp:ListItem Selected="True" Value="0">SELECT</asp:ListItem>
                                            <asp:ListItem Selected="False" Value="IP">IPO</asp:ListItem>
                                            <asp:ListItem Selected="False" Value="FI">BOND</asp:ListItem>
                                            <asp:ListItem Selected="False" Value="MF">Mutual Fund</asp:ListItem>
                                        </asp:DropDownList>
                                        <span id="Span2" class="spnRequiredField">*</span>
                                        <br />
                                        <asp:RequiredFieldValidator ID="Requiredfieldvalidator3" runat="server" ControlToValidate="ddlAssetGroupName1"
                                            ErrorMessage="Please,Select an AssetGroup." InitialValue="0" ValidationGroup='<%# (Container is GridEditFormInsertItem) ? "btnInsertGroup1" : "btnUpdateGroup1" %>'
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr id="tr2">
                                    <td class="leftField">
                                        <asp:Label ID="Label1" runat="server" Text="Scroller Text:" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td class="rightField">
                                        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" CssClass="txtField"
                                            Text='<%# Eval("PUHD_HelpDetails") %>'></asp:TextBox>
                                        <span id="Span3" class="spnRequiredField">*</span>
                                        <br />
                                        <asp:RequiredFieldValidator ID="Requiredfieldvalidator4" runat="server" ControlToValidate="TextBox1"
                                            ErrorMessage="Scroller Text can't be blank." ValidationGroup='<%# (Container is GridEditFormInsertItem) ? "btnInsertGroup1" : "btnUpdateGroup1" %>'
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr id="tr3">
                                    <td class="leftField">
                                        <asp:Label ID="Label2" runat="server" Text="IS Active:" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td class="rightField">
                                        <asp:CheckBox ID="CheckBox" runat="server" Checked='<%# Convert.IsDBNull(Eval("PUHD_IsActive")) ? false :Convert.ToInt16( Eval("PUHD_IsActive"))==1 ? true : false %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                            ValidationGroup='<%# (Container is GridEditFormInsertItem) ? "btnInsertGroup1" : "btnUpdateGroup1" %>'
                                            CssClass="PCGButton" runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                        </asp:Button>&nbsp;
                                        <asp:Button ID="Button2" CssClass="PCGButton" Text="Cancel" runat="server" CausesValidation="False"
                                            CommandName="Cancel"></asp:Button>
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
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="rpvDemo" runat="server">
        <asp:Panel ID="Panel1">
            <telerik:RadGrid ID="RadGrid3" runat="server" Skin="Telerik" CssClass="RadGrid" GridLines="None"
                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                AllowAutomaticDeletes="false" AllowAutomaticInserts="false" PageSize="3" OnInsertCommand="RadGrid3_InsertCommand"
                OnNeedDataSource="RadGrid3_NeedDataSource" OnDeleteCommand="RadGrid3_DeleteCommand"
                OnUpdateCommand="RadGrid3_UpdateCommand" OnItemCommand="RadGrid3_ItemCommand"
                OnItemDataBound="RadGrid3_ItemDataBound" AllowAutomaticUpdates="false" HorizontalAlign="NotSet"
                DataKeyNames="PUHD_Id,PUHD_IsActive,PAG_AssetGroupCode,PUHD_HelpDetails">
                <MasterTableView CommandItemDisplay="Top" EditMode="PopUp" DataKeyNames="PUHD_Id,PUHD_IsActive,PAG_AssetGroupCode,PUHD_HelpDetails">
                    <Columns>
                        <telerik:GridEditCommandColumn EditText="Update" UniqueName="editColumn" CancelText="Cancel"
                            UpdateText="Update">
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn UniqueName="PAG_AssetGroupName" HeaderText="Asset Group"
                            DataField="PAG_AssetGroupName">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="PUHD_HelpDetails" HeaderText="Video Link" DataField="PUHD_HelpDetails">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="PUHD_IsActive" HeaderText="Is Active" DataField="PUHD_IsActive">
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete this ?"
                            ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                            Text="Delete">
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings InsertCaption="Add" FormTableStyle-HorizontalAlign="Center" CaptionFormatString="Edit"
                        FormCaptionStyle-CssClass="TableBackground" PopUpSettings-Modal="true" PopUpSettings-ZIndex="20"
                        EditFormType="Template" FormCaptionStyle-Width="100%" PopUpSettings-Height="300px"
                        PopUpSettings-Width="500px">
                        <FormTemplate>
                            <table>
                                <tr id="trAddCategory">
                                    <td class="leftField">
                                        <asp:Label ID="lblAssetGroup1" runat="server" Text="AssetGroup:" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td class="rightField">
                                        <asp:DropDownList ID="ddlAssetGroupName1" runat="server" CssClass="cmbLongField"
                                            DataValueField='<%# Eval("PAG_AssetGroupCode") %>'>
                                            <asp:ListItem Selected="True" Value="0">SELECT</asp:ListItem>
                                            <asp:ListItem Selected="False" Value="IP">IPO</asp:ListItem>
                                            <asp:ListItem Selected="False" Value="FI">BOND</asp:ListItem>
                                            <asp:ListItem Selected="False" Value="MF">Mutual Fund</asp:ListItem>
                                        </asp:DropDownList>
                                        <span id="Span2" class="spnRequiredField">*</span>
                                        <br />
                                        <asp:RequiredFieldValidator ID="Requiredfieldvalidator3" runat="server" ControlToValidate="ddlAssetGroupName1"
                                            ErrorMessage="Please,Select an AssetGroup." InitialValue="0" ValidationGroup='<%# (Container is GridEditFormInsertItem) ? "btnInsertGroup1" : "btnUpdateGroup1" %>'
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr id="tr2">
                                    <td class="leftField">
                                        <asp:Label ID="Label1" runat="server" Text="Video Link:" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td class="rightField">
                                        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" CssClass="txtField"
                                            Text='<%# Eval("PUHD_HelpDetails") %>'></asp:TextBox>
                                        <span id="Span3" class="spnRequiredField">*</span>
                                        <br />
                                        <asp:RequiredFieldValidator ID="Requiredfieldvalidator4" runat="server" ControlToValidate="TextBox1"
                                            ErrorMessage="Scroller Text can't be blank." ValidationGroup='<%# (Container is GridEditFormInsertItem) ? "btnInsertGroup1" : "btnUpdateGroup1" %>'
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr id="tr3">
                                    <td class="leftField">
                                        <asp:Label ID="Label2" runat="server" Text="IS Active:" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td class="rightField">
                                        <asp:CheckBox ID="CheckBox" runat="server" Checked='<%# Convert.IsDBNull(Eval("PUHD_IsActive")) ? false :Convert.ToInt16( Eval("PUHD_IsActive"))==1 ? true : false %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                            ValidationGroup='<%# (Container is GridEditFormInsertItem) ? "btnInsertGroup1" : "btnUpdateGroup1" %>'
                                            CssClass="PCGButton" runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                        </asp:Button>&nbsp;
                                        <asp:Button ID="Button2" CssClass="PCGButton" Text="Cancel" runat="server" CausesValidation="False"
                                            CommandName="Cancel"></asp:Button>
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
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="rpvFAQ" runat="server">
        <telerik:RadGrid ID="RadGrid4" runat="server" Skin="Telerik" CssClass="RadGrid" GridLines="None"
            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
            AllowAutomaticDeletes="false" AllowAutomaticInserts="false" PageSize="3" OnInsertCommand="RadGrid4_InsertCommand"
            OnNeedDataSource="RadGrid4_NeedDataSource" OnDeleteCommand="RadGrid4_DeleteCommand"
            
             AllowAutomaticUpdates="false" HorizontalAlign="NotSet"
            DataKeyNames="PUHD_Id,PUHD_IsActive,PAG_AssetGroupCode,PUHD_HelpDetails">
            <MasterTableView CommandItemDisplay="Top" EditMode="PopUp" DataKeyNames="PUHD_Id,PUHD_IsActive,PAG_AssetGroupCode,PUHD_HelpDetails">
                <Columns>
                   
                    <telerik:GridBoundColumn UniqueName="PAG_AssetGroupName" HeaderText="Asset Group"
                        DataField="PAG_AssetGroupName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="PUHD_HelpDetails" HeaderText="FAQ Pdf Name"
                        DataField="PUHD_HelpDetails">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="PUHD_IsActive" HeaderText="Is Active" DataField="PUHD_IsActive">
                    </telerik:GridBoundColumn>
                    <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete this ?"
                        ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                        Text="Delete">
                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                    </telerik:GridButtonColumn>
                </Columns>
                <EditFormSettings InsertCaption="Add" FormTableStyle-HorizontalAlign="Center" CaptionFormatString="Edit"
                    FormCaptionStyle-CssClass="TableBackground" PopUpSettings-Modal="true" PopUpSettings-ZIndex="20"
                    EditFormType="Template" FormCaptionStyle-Width="100%" PopUpSettings-Height="300px"
                    PopUpSettings-Width="500px">
                    <FormTemplate>
                        <table>
                            <tr id="trAddCategory">
                                <td class="leftField">
                                    <asp:Label ID="lblAssetGroup1" runat="server" Text="AssetGroup:" CssClass="FieldName"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:DropDownList ID="ddlAssetGroupName1" runat="server" CssClass="cmbLongField"
                                        DataValueField='<%# Eval("PAG_AssetGroupCode") %>'>
                                        <asp:ListItem Selected="True" Value="0">SELECT</asp:ListItem>
                                        <asp:ListItem Selected="False" Value="IP">IPO</asp:ListItem>
                                        <asp:ListItem Selected="False" Value="FI">BOND</asp:ListItem>
                                        <asp:ListItem Selected="False" Value="MF">Mutual Fund</asp:ListItem>
                                    </asp:DropDownList>
                                    <span id="Span2" class="spnRequiredField">*</span>
                                    <br />
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator3" runat="server" ControlToValidate="ddlAssetGroupName1"
                                        ErrorMessage="Please,Select an AssetGroup." InitialValue="0" ValidationGroup='<%# (Container is GridEditFormInsertItem) ? "btnInsertGroup1" : "btnUpdateGroup1" %>'
                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr id="tr2">
                                <td class="leftField">
                                </td>
                                <td class="rightField">
                                    <asp:FileUpload ID="FileUpload" runat="server" Height="22px" />
                                    <span id="Span1" class="spnRequiredField">*</span>
                                    <br />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="FileUpload"
                                        runat="Server" ValidationGroup="btnInsertGroup" ErrorMessage="Only .pdf File allowed"
                                        Display="Dynamic" ValidationExpression="^.*\.((p|P)(d|D)(f|F))$"
                                        CssClass="rfvPCG" />
                                    <br />
                                    <asp:RequiredFieldValidator ID="FileUpload_RequiredFieldValidator" ControlToValidate="FileUpload"
                                        ValidationGroup="btnInsertGroup" ErrorMessage="Please select an pdf for upload."
                                        Display="Dynamic" runat="server" CssClass="rfvPCG">
                                    </asp:RequiredFieldValidator>
                                </td>
                                </td>
                            </tr>
                            <tr id="tr3">
                                <td class="leftField">
                                    <asp:Label ID="Label2" runat="server" Text="IS Active:" CssClass="FieldName"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:CheckBox ID="CheckBox" runat="server" Checked='<%# Convert.IsDBNull(Eval("PUHD_IsActive")) ? false :Convert.ToInt16( Eval("PUHD_IsActive"))==1 ? true : false %>' />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                        ValidationGroup='<%# (Container is GridEditFormInsertItem) ? "btnInsertGroup1" : "btnUpdateGroup1" %>'
                                        CssClass="PCGButton" runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                    </asp:Button>&nbsp;
                                    <asp:Button ID="Button2" CssClass="PCGButton" Text="Cancel" runat="server" CausesValidation="False"
                                        CommandName="Cancel"></asp:Button>
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
    </telerik:RadPageView>
</telerik:RadMultiPage>
