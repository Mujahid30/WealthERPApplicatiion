<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserAssociateCategorySetup.ascx.cs"
    Inherits="WealthERP.Advisor.AdviserAssociateCategorySetup" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>


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
<table>
    <tr>
        <td>
            <asp:Label ID="lblCategory" runat="server" Text="Select Category" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbLongField" Style="vertical-align: middle"
                Width="200px" OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCategory"
                InitialValue="Select" CssClass="rfvPCG" Display="Dynamic" ValidationGroup="btnGo"
                ErrorMessage="Please Select Category"></asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" Text="Go" CssClass="PCGButton"
                ValidationGroup="btnGo" />
        </td>
    </tr>
</table>
<table style="width: 100%">
    <tr>
        <td>
            <telerik:RadGrid ID="gvAdvisorCategory" runat="server" Skin="Telerik" CssClass="RadGrid"
                GridLines="None" AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"
                ShowStatusBar="true" AllowAutomaticDeletes="false" AllowAutomaticInserts="false"
                OnNeedDataSource="gvAdvisorCategory_OnNeedDataSource" OnItemDataBound="gvAdvisorCategory_ItemDataBound"
                OnItemCommand="gvAdvisorCategory_ItemCommand" AllowAutomaticUpdates="false" HorizontalAlign="NotSet"
                DataKeyNames="" ShowFooter="true" Width="60%">
                <MasterTableView CommandItemDisplay="Top" EditMode="PopUp" DataKeyNames="A_AdviserId,AC_CategoryName,ACM_ID,AC_CategoryId"
                    EnableEmbeddedSkins="false">
                    <Columns>
                        <telerik:GridEditCommandColumn EditText="Edit" UniqueName="editColumn" CancelText="Cancel"
                            UpdateText="Update">
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Category Id" HeaderTooltip="Category Id"
                            DataField="ACM_Id" HeaderStyle-HorizontalAlign="Right" UniqueName="ACM_Id" SortExpression="ACM_Id"
                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                            DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Category Name" HeaderTooltip="Category Name"
                            DataField="AC_CategoryName" HeaderStyle-HorizontalAlign="Right" UniqueName="AC_CategoryName"
                            SortExpression="AC_CategoryName" AutoPostBackOnFilter="true" AllowFiltering="false"
                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                            FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete this Category?"
                            ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                            Text="Delete">
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings InsertCaption="Add" FormTableStyle-HorizontalAlign="Center" CaptionFormatString="Edit"
                        FormCaptionStyle-CssClass="TableBackground" PopUpSettings-Modal="true" PopUpSettings-ZIndex="20"
                        EditFormType="Template" FormCaptionStyle-Width="100%" PopUpSettings-Height="260px"
                        PopUpSettings-Width="300px">
                        <FormTemplate>
                            <table>
                                <tr>
                                    <td class="leftField">
                                        <asp:Label ID="lblCategoryName" runat="server" Text="Category Name" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCategoryName" runat="server" Style="vertical-align: middle" Width="200px">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCategoryName" runat="server" ControlToValidate="txtCategoryName"
                                            CssClass="rfvPCG" Display="Dynamic" ValidationGroup="Button1" ErrorMessage="Please Select Category">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                            ValidationGroup="Button1" CssClass="PCGButton" runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
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
    </tr>
</table>
