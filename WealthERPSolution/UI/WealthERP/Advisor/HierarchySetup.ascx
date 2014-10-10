<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HierarchySetup.ascx.cs"
    Inherits="WealthERP.Advisor.HierarchySetup" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Hierarchy Setup
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<br />
<table w>
    <tr>
        <td>
            <telerik:RadGrid ID="gvHirarchy" runat="server" CssClass="RadGrid" GridLines="None"
                Width="700px" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false"
                ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
                OnNeedDataSource="gvHirarchy_NeedDataSource" AllowAutomaticUpdates="false" Skin="Telerik"
                OnItemDataBound="gvHirarchy_ItemDataBound" EnableEmbeddedSkins="false" OnItemCommand="gvHirarchy_ItemCommand"
                EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" AllowFilteringByColumn="true">
                <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="ExistMFInvestlist">
                </ExportSettings>
                <MasterTableView DataKeyNames="AH_Id,AH_HierarchyName,AH_TitleId,AH_Teamname,AH_TeamId,AH_ReportsToId,AH_ReportsTo,AH_ChannelName,AH_ChannelId,AH_Sequence"
                    EditMode="PopUp" CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="false"
                    CommandItemSettings-AddNewRecordText="Add Hierarchy Setup">
                    <Columns>
                        <telerik:GridEditCommandColumn EditText="Edit" UniqueName="editColumn" CancelText="Cancel"
                            UpdateText="Update">
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn Visible="false" UniqueName="AH_Id" HeaderText="Type" DataField="AH_Id"
                            SortExpression="AH_Id" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="AH_HierarchyName" HeaderText="Role/Title" DataField="AH_HierarchyName"
                            SortExpression="AH_HierarchyName" AllowFiltering="true" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="AH_Teamname" HeaderText="Team" DataField="AH_Teamname"
                            SortExpression="AH_Teamname" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="AH_TeamId" Visible="false" HeaderText="Teamid"
                            DataField="AH_TeamId" SortExpression="AH_TeamId" AllowFiltering="true" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="false" UniqueName="AH_ReportsToId" HeaderText="ReportsToId"
                            DataField="AH_ReportsToId" SortExpression="Name" AllowFiltering="true" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" UniqueName="AH_ReportsTo" HeaderText="ReportingTo"
                            DataField="AH_ReportsTo" SortExpression="AH_ReportsTo" AllowFiltering="true"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="AH_ChannelName" HeaderText="Channel" DataField="AH_ChannelName"
                            SortExpression="AH_ChannelName" AllowFiltering="true" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="AH_Sequence" HeaderText="Seq in Chanel" DataField="AH_Sequence"
                            SortExpression="AH_Sequence" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete this Hierarchy?"
                    ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                    Text="Delete">
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>--%>
                    </Columns>
                    <EditFormSettings EditFormType="Template" FormTableStyle-Width="1000px">
                        <FormTemplate>
                            <table>
                                <tr>
                                    <td colspan="5">
                                        <div class="divSectionHeading" style="vertical-align: text-bottom; width: 380px">
                                            &nbsp;&nbsp;Add Hierarchy
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <table id="Table2" cellspacing="2" cellpadding="1" border="0" rules="none" style="border-collapse: collapse;
                                background: white;">
                                <tr>
                                    <td>
                                        <table id="Table3" cellspacing="1" cellpadding="1" border="0" width="390px">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label runat="server" Text="Setup for :" ID="label3"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <telerik:RadComboBox AutoPostBack="true" runat="server" ID="rcbSetup" EmptyMessage="Select"
                                                        OnSelectedIndexChanged="rcbSetup_SelectedIndexChanged">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="" Value="" />
                                                            <telerik:RadComboBoxItem Text="Team Hierarchy" Value="1" />
                                                            <telerik:RadComboBoxItem Text="Office Hierarchy" Value="2" Enabled="false" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td colspan="3">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rfvPCG"
                                                        ErrorMessage="Please select a setup Type" Display="Dynamic" ValidationGroup="btnSubmit"
                                                        ControlToValidate="rcbSetup">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label runat="server" Text="Team :" ID="label"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <telerik:RadComboBox AutoPostBack="true" runat="server" ID="rcbTeamm" EmptyMessage="Select">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td colspan="3">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="rfvPCG"
                                                        ErrorMessage="Please select a Team" Display="Dynamic" ValidationGroup="btnSubmit"
                                                        ControlToValidate="rcbTeamm" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label runat="server" Text="Channel :" ID="Label2"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <telerik:RadComboBox AutoPostBack="true" runat="server" ID="rcbChanel" EmptyMessage="Select"
                                                        OnSelectedIndexChanged="rcbChanel_SelectedIndexChanged">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr visible="false">
                                                <td align="right" width="25%">
                                                    <asp:Label Visible="false" runat="server" Text=" Titles :" ID="Label6"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <telerik:RadComboBox Visible="false" runat="server" ID="rcbTitles" Text="Show" EmptyMessage="Select">
                                                    </telerik:RadComboBox>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="rfvPCG"
                                                ErrorMessage="Please select a Title" Display="Dynamic" ValidationGroup="btnSubmit"
                                                ControlToValidate="rcbSetup"/>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="25%">
                                                    <asp:Label runat="server" Text="Title Name :" ID="Label1"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtHierarchy" runat="server"></asp:TextBox>
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="rfvPCG"
                                                ErrorMessage="Please select a Hierarchy" Display="Dynamic" ValidationGroup="btnSubmit"
                                                ControlToValidate="rcbSetup"/> --%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td colspan="3">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="rfvPCG"
                                                        ErrorMessage="Please Enter Title Name" Display="Dynamic" ValidationGroup="btnSubmit"
                                                        ControlToValidate="txtHierarchy" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="25%">
                                                    <asp:Label runat="server" Text="Available Seq No:" ID="Label7"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="TxtSeq" runat="server" Enabled="True"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="rfvPCG"
                                                        ErrorMessage="Please enter a Seq No" Display="Dynamic" ValidationGroup="btnSubmit"
                                                        ControlToValidate="TxtSeq" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right"  >
                                                <asp:CheckBox ID="chkIsSeqChange" runat="server" CssClass="txtField" Text="" AutoPostBack="true">
                                                    </asp:CheckBox>
                                                    <asp:Label runat="server" Text="Changes Applicable Existing Seq.:" ID="Label8"></asp:Label>
                                                </td>
                                               <%-- <td colspan="3">
                                                    <asp:CheckBox ID="chkIsSeqChange" runat="server" CssClass="txtField" Text="" AutoPostBack="true">
                                                    </asp:CheckBox>
                                                </td>--%>
                                            </tr>
                                            <tr id="RcbReports">
                                                <td align="right">
                                                    <asp:Label runat="server" Text=" Reports to :" ID="Label4"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox runat="server" ID="rcbReportingTo" Text="Show" EmptyMessage="Select">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label runat="server" Visible="false" Text="ops role permission :" ID="Label5"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox Visible="false" runat="server" ID="Radops" Text="Show" EmptyMessage="Select">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <%--<tr>
                                        <td align="right">
                                            <asp:Label runat="server" Text="Name :" ID="Label1"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtName" Text='<%# Bind("ZCName") %>' runat="server"></asp:TextBox>
                                             
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label Visible="false" runat="server" Text="Chanel :" ID="Label2"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox Visible="false" runat="server" ID="rcbChanel" Text="Show" EmptyMessage="Select">                                          
                                            </telerik:RadComboBox>
                                            
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trPickAZone">
                                        <td align="right">
                                            <asp:Label runat="server" ID="lblPickAZone" Text="Team :"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <telerik:RadComboBox runat="server" ID="rcbTeamm" Text="Show" EmptyMessage="Select">
                                            </telerik:RadComboBox>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label runat="server" ID="lblDescription" Text="Description :"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtDescription" Text='<%# Bind("AZOC_Description") %>' TextMode="MultiLine"
                                                runat="server"></asp:TextBox>
                                        </td>
                                    </tr>--%>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ValidationGroup="btnSubmit" ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                            runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                        </asp:Button>&nbsp;
                                        <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton"
                                            CommandName="Cancel"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </FormTemplate>
                    </EditFormSettings>
                    <HeaderStyle Width="100px" />
                </MasterTableView>
                <ClientSettings ReorderColumnsOnClient="True" AllowColumnsReorder="True" EnableRowHoverStyle="true">
                    <Scrolling AllowScroll="false" />
                    <Resizing AllowColumnResize="true" />
                    <Selecting AllowRowSelect="true" />
                </ClientSettings>
            </telerik:RadGrid>
        </td>
    </tr>
</table>
