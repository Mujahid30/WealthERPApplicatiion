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
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Banner Manager
                        </td>
                        <td align="right" id="tdGoalExport" runat="server" style="padding-bottom: 2px;">
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
            <%--
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="RadGrid1">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadGrid1"></telerik:AjaxUpdatedControl>
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>
            <telerik:RadGrid runat="server" ID="RadGrid1" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" GridLines="None" OnItemCreated="RadGrid1_ItemCreated"
                PageSize="3" OnInsertCommand="RadGrid1_InsertCommand" OnNeedDataSource="RadGrid1_NeedDataSource"
                OnDeleteCommand="RadGrid1_DeleteCommand" OnUpdateCommand="RadGrid1_UpdateCommand"
                OnItemCommand="RadGrid1_ItemCommand">
                <PagerStyle Mode="NumericPages" AlwaysVisible="true"></PagerStyle>
                <MasterTableView Width="100%" CommandItemDisplay="Top" DataKeyNames="PBD_Id">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="36px"></HeaderStyle>
                        </telerik:GridEditCommandColumn>
                        <telerik:GridTemplateColumn HeaderText="Asset Group" UniqueName="AssetGroup" SortExpression="AssetGroup">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblAssetGroupName" Text='<%# Eval("PAG_AssetGroupName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlAssetGroupName" runat="server" CssClass="cmbLongField" DataValueField='<%# Eval("PAG_AssetGroupCode") %>'>
                                    <asp:ListItem Selected="True" Value="0">SELECT</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="IP">IPO</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="FI">BOND</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="MF">Mutual Fund</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" ControlToValidate="txbName"
                                    ErrorMessage="Please,Select a AssetGroup!" Display="Dynamic" InitialValue="0"
                                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <HeaderStyle Width="30%"></HeaderStyle>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Expire Date" UniqueName="ExpireDate" DataField="PDB_ExpiryDate">
                            <ItemTemplate>
                                <asp:Label ID="lblExpireDate" runat="server" Text='<%#Eval("PDB_ExpiryDate")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadDateTimePicker runat="server" ID="dtpExpireDate" Width="100%">
                                    <Calendar ID="Calendar4" runat="server" EnableKeyboardNavigation="true">
                                    </Calendar>
                                </telerik:RadDateTimePicker>
                            </EditItemTemplate>
                            <ItemStyle VerticalAlign="Top"></ItemStyle>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn DataField="PDB_BannerImage" HeaderText="Image" UniqueName="Upload">
                            <ItemTemplate>
                                <%--<telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" DataValue='<%#Eval("Data") %>'
                                    AutoAdjustImageControlSize="false" Height="80px" Width="80px" ToolTip='<%#Eval("Name", "Photo of {0}") %>'
                                    AlternateText='<%#Eval("Name", "Photo of
                                     {0}") %>'></telerik:RadBinaryImage>
                                <asp:Image ID="imgBanner" runat="server" AlternateText='<%#Eval("PDB_BannerImage") %>'
                                    Width="100px" Height="100px" ToolTip='<%#Eval("PDB_BannerImage") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:FileUpload ID="FileUpload" runat="server" Height="22px" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="FileUpload"
                                    runat="Server" ValidationGroup="btn_Upload" ErrorMessage="Only .jpeg,.jpg, .gif and.png File allowed"
                                    Display="Dynamic" ValidationExpression="^.*\.((j|J)(p|P)(e|E)(g|G)|(j|J)(p|P)(g|G)|(g|G)(i|I)(f|F)|(p|P)(n|N)(g|G))$"
                                    CssClass="rfvPCG" />
                                <br />
                                <asp:RequiredFieldValidator ID="FileUpload_RequiredFieldValidator" ControlToValidate="FileUpload"
                                    ValidationGroup="btn_Upload" ErrorMessage="Please select a file for upload" Display="Dynamic"
                                    runat="server" CssClass="rfvPCG">
                                </asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn Text="Delete" CommandName="Delete" ButtonType="ImageButton">
                            <HeaderStyle Width="36px"></HeaderStyle>
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings>
                        <EditColumn ButtonType="ImageButton">
                        </EditColumn>
                    </EditFormSettings>
                    <PagerStyle AlwaysVisible="True"></PagerStyle>
                </MasterTableView>
            </telerik:RadGrid>
            --%>
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
                                        </asp:DropDownList> <span id="Span2" class="spnRequiredField">*</span>
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
                                    <td class="rightField" >
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
