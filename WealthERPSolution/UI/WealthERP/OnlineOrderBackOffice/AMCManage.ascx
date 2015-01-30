<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AMCManage.ascx.cs" Inherits="WealthERP.OnlineOrderBackOffice.AMCManage" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            AMC List
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="imgexportButton" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                Visible="false" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                OnClick="btnExportData_OnClick" OnClientClick="setFormat('excel')" Height="22px"
                                Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<div style="margin-left: 50; overflow: None;">
    <telerik:RadGrid ID="gvAMCManage" runat="server" AllowPaging="true" AutoGenerateColumns="False"
        ShowStatusBar="true" ShowHeader="true" ShowFooter="true" enableloadondemand="True" Skin="Telerik" EnableEmbeddedSkins="false"
        Width="60%" AllowSorting="true" GridLines="None" PageSize="10" OnNeedDataSource="gvAMCManage_OnNeedDataSource"
        OnItemCommand="gvAMCManage_OnItemCommand">
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView AllowMultiColumnSorting="True" Width="100%" Height="20px" AutoGenerateColumns="false"
            AllowSorting="true" EditMode="PopUp" CommandItemSettings-AddNewRecordText="Add AMC"
            CommandItemSettings-ShowRefreshButton="false" AllowFilteringByColumn="true" CommandItemDisplay="Top" DataKeyNames="PA_AMCCode,PA_AMCName,PA_IsOnline">
            <Columns>
                <telerik:GridEditCommandColumn EditText="Edit" UniqueName="editColumn" CancelText="Cancel"
                    UpdateText="Update" HeaderStyle-Width="20px">
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="PA_AMCCode" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="AMC Code" UniqueName="PA_AMCCode"
                    SortExpression="PA_AMCCode" AllowFiltering="true">
                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PA_AMCName" HeaderStyle-Width="40px" CurrentFilterFunction="Contains"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="AMC Name" UniqueName="PA_AMCName"
                    SortExpression="PA_AMCName" AllowFiltering="true" Visible="true">
                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PA_IsOnline" HeaderStyle-Width="20px" CurrentFilterFunction="contains"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="IsOnline" UniqueName="PA_IsOnline"
                    SortExpression="PA_IsOnline" AllowFiltering="true" Visible="true">
                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridButtonColumn CommandName="Delete" Text="Delete" ConfirmText="Do you want to delete this rule? Click OK to proceed"
                    UniqueName="column" HeaderStyle-Width="20px" Visible="false">
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings EditFormType="Template" PopUpSettings-Height="190 px" PopUpSettings-Width="390 px"
                CaptionFormatString="Add New AMC">
                <FormTemplate>
                    <table>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblAMCName" runat="server" Text="AMC Name:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAMCName" runat="server" CssClass="cmbFielde" Width=" 200 px"
                                    Text='<% # Bind("PA_AMCName") %>'></asp:TextBox>
                                <span id="Span1" class="spnRequiredField">*</span>
                                <br />
                                <asp:RequiredFieldValidator ID="ReqtxtAMCName" runat="server" CssClass="cmbFielde"
                                    ErrorMessage="Please Enter AMC Name" Display="Dynamic" ControlToValidate="txtAMCName"
                                    ValidationGroup="btnOK" InitialValue="">                                      
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblIsOnline" runat="server" Text="Is Online:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkIsOnline" runat="server" CssClass="cmbFielde" Text="Is Online"
                                    Checked='<%# Eval("PA_IsOnline") == DBNull.Value ? false : Convert.ToBoolean(Eval("PA_IsOnline")) %>' />
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
        <ClientSettings>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
            <Resizing AllowColumnResize="true"  />
            <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="250px" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
