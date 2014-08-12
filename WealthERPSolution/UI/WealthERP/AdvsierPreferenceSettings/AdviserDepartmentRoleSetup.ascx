<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserDepartmentRoleSetup.ascx.cs"
    Inherits="WealthERP.AdvsierPreferenceSettings.AdviserDepartmentRoleSetup" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script runat="server">  
   
</script>

<div class="divPageHeading">
    <table width="100%">
        <tr>
            <td align="left">
                Adviser Department Role Setup
            </td>
            <td>
            </td>
        </tr>
    </table>
</div>
<br />
<table width="68%">
    <tr>
        <td>
            <telerik:RadGrid ID="gvAdviserList" runat="server" AllowSorting="True" enableloadondemand="True"
                PageSize="10" AutoGenerateColumns="False" EnableEmbeddedSkins="False" GridLines="None"
                ShowFooter="True" PagerStyle-AlwaysVisible="true" AllowPaging="true" ShowStatusBar="True"
                Skin="Telerik" AllowFilteringByColumn="true" OnItemDataBound="gvAdviserList_OnItemDataBound"
                OnItemCommand="gvAdviserList_OnItemCommand" OnNeedDataSource="gvAdviserList_OnNeedDataSource">
                <MasterTableView DataKeyNames="AR_RoleId,AD_DepartmentId,A_AdviserId,AR_Role" AllowFilteringByColumn="true"
                    Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="Top"
                    EditMode="PopUp">
                    <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                        AddNewRecordText="Add New Adviser Role" ShowExportToCsvButton="false" ShowAddNewRecordButton="true"
                        ShowRefreshButton="false" />
                    <Columns>
                        <telerik:GridEditCommandColumn EditText="Edit" UniqueName="editColumn" CancelText="Cancel"
                            UpdateText="Update" HeaderStyle-Width="80px">
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="AD_DepartmentName" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Department" UniqueName="AD_DepartmentName"
                            SortExpression="AD_DepartmentName" AllowFiltering="true" Visible="true">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn DataField="Rolename" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Level" UniqueName="Rolename"
                            SortExpression="Rolename" AllowFiltering="true" Visible="true">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn DataField="AR_Role" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Role" UniqueName="AR_Role"
                            SortExpression="AR_Role" AllowFiltering="true" Visible="true">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkRole" runat="server" CommandName="Select" Text='<%# Eval("AR_Role").ToString() %>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="AR_RolePurpose" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Note" UniqueName="AR_RolePurpose"
                            SortExpression="AR_RolePurpose" AllowFiltering="true">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AR_CreateOn" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Adding Date" UniqueName="AR_CreateOn"
                            SortExpression="AR_CreateOn" AllowFiltering="true">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AD_DepartmentId" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Id" UniqueName="AD_DepartmentId"
                            SortExpression="AD_DepartmentId" AllowFiltering="true">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AR_CreatedBy" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="User Id" UniqueName="AR_CreatedBy"
                            SortExpression="AR_CreatedBy" AllowFiltering="true">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete this User?"
                            ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                            Text="Delete" HeaderStyle-Width="100px" Visible="false">
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings EditFormType="Template" PopUpSettings-Height="490px" PopUpSettings-Width="350px"
                        CaptionFormatString="Add User Role">
                        <FormTemplate>
                            <table width="100%">
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lbllevel" runat="server" Text="Department:" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlDepartMent" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlDepartMent_OnSelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                        <span id="Span3" class="spnRequiredField">*</span>
                                        <br />
                                        <asp:RequiredFieldValidator ID="ReqddlLevel" runat="server" CssClass="rfvPCG" ErrorMessage="Please Enter Level"
                                            Display="Dynamic" ControlToValidate="ddlDepartMent" ValidationGroup="btnOK" InitialValue="Select">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                <td colspan="2">
                                        <asp:Label ID="lbluserassociate" runat="server" Text="User Associated:" CssClass="FieldName"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <telerik:RadGrid ID="rgRoles" runat="server" AllowSorting="True" enableloadondemand="True"
                                            PageSize="5" AllowPaging="True" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                                            GridLines="None" ShowFooter="True" PagerStyle-AlwaysVisible="false" ShowStatusBar="True"
                                            Width="100%" Skin="Telerik" AllowFilteringByColumn="true" OnNeedDataSource="rgRoles_OnNeedDataSource"
                                            EnableViewState="true">
                                            <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" AutoGenerateColumns="false"
                                                DataKeyNames="UR_RoleId" Width="100%">
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="Select" ShowFilterIcon="false" AllowFiltering="false"
                                                        runat="server" UniqueName="chkBxSelect" Visible="true">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbRoles" runat="server" Checked="false" AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    
                                                    
                                                     <telerik:GridBoundColumn DataField="UR_RoleId" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                                        ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Role Name" UniqueName="UR_RoleId"
                                                        SortExpression="UR_RoleId" AllowFiltering="true" Visible="false">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    
                                                    
                                                    
                                                    <telerik:GridBoundColumn DataField="UR_RoleName" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                                        ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Role Name" UniqueName="UR_RoleName"
                                                        SortExpression="UR_RoleName" AllowFiltering="true">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <%-- <telerik:GridTemplateColumn HeaderText="Role Name" ShowFilterIcon="false"
                                                                        AllowFiltering="false" UniqueName="UR_RoleName">
                                                                        
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtRoleName" runat="server" CssClass="txtField" Width="55px"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>--%>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                                <tr>
                                    
                                    <td>
                                        <%-- <asp:CheckBox ID="chkAdmin" runat="server" CssClass="cmbFielde" Text="Is Buy Back"
                                            AutoPostBack="true"></asp:CheckBox>--%>
                                        <asp:CheckBoxList ID="rlbUserlist" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
                                            CssClass="cmbFielde" Width="100%" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChnaged"
                                            AutoPostBack="false">
                                        </asp:CheckBoxList>
                                       
                                    </td>
                                    <%-- <td><asp:CheckBoxList ID="chklUserAssociates" runat="server"></asp:CheckBoxList></td>--%>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblRoleName" runat="server" Text="Role Name:" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRoleName" runat="server" CssClass="cmbFielde"></asp:TextBox>
                                        <span id="Span1" class="spnRequiredField">*</span>
                                        <br />
                                        <asp:RequiredFieldValidator ID="ReqtxtRoleName" runat="server" CssClass="rfvPCG"
                                            ErrorMessage="Please Enter User Role" Display="Dynamic" ControlToValidate="txtRoleName"
                                            ValidationGroup="btnOK" InitialValue="">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblNote" runat="server" Text="Note:" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNote" runat="server" CssClass="cmbFielde"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Button ID="btnOK" Text="Submit" runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                            CausesValidation="True" ValidationGroup="btnOK" />
                                    </td>
                                    <td class="rightData">
                                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                            CssClass="PCGButton" CommandName="Cancel"></asp:Button>
                                    </td>
                                    <td class="rightData">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </FormTemplate>
                    </EditFormSettings>
                </MasterTableView>
            </telerik:RadGrid>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Department:" CssClass="FieldName" Visible="false"></asp:Label>
        </td>
    </tr>
</table>
