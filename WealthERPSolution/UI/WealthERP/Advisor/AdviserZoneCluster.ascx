<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserZoneCluster.ascx.cs"
    Inherits="WealthERP.Advisor.AdviserZoneCluster" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />

<script src="/Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="/Scripts/jquery.colorbox-min.js" type="text/javascript"></script>

<script type="text/javascript" language="javascript">
    function AccessOnclient() {
        var grid = document.getElementById('<%= gvZoneClusterDetails.ClientID %>');
        if (grid) {
            var EditItems = grid.get_editItems();
            alert(EditItems);
            for (var i = 0; i < EditItems.length; i++) {
                var editItem = EditItems[0];
                var checkbox1 = $(editItem.get_editFormItem()).find("input[id*='rcbShow']").get(0)
                alert('hi');
            }
        }
    } 
</script>
<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Zone/Cluster details
                        </td>
                        <td align="right">
                            <asp:ImageButton Visible="false" ID="btnExportFilteredData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<br />
<div style="border: none; padding-left: 8px">
    <asp:Label ID="lblAction" runat="server" Text="Show:" CssClass="FieldName"></asp:Label>
    <telerik:RadComboBox AutoPostBack="true" runat="server" ID="rcbShow" EmptyMessage="Show"
        >
        <Items>
            <telerik:RadComboBoxItem Text="Select" Value="0" />
            <telerik:RadComboBoxItem Text="Zone" Value="1" />
            <telerik:RadComboBoxItem Text="Cluster" Value="2" />
            <telerik:RadComboBoxItem Text="ALL" Value="3" />
        </Items>
    </telerik:RadComboBox>
    <span id="Span1" class="spnRequiredField">*</span>
    <br />
    <asp:RequiredFieldValidator ControlToValidate="rcbShow" runat="server" ValidationGroup="VgBtnAction" InitialValue="0"></asp:RequiredFieldValidator>
    <asp:Button runat="server" ID="btnAction" Text="Go" OnClick="btnAction_SelectedIndexChanged" ValidationGroup="VgBtnAction" />
</div>
<br />
<div style="padding-left:8px;">
    <telerik:RadGrid ID="gvZoneClusterDetails" runat="server" CssClass="RadGrid" GridLines="None"
        Width="700px" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false"
        ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
        OnNeedDataSource="gvZoneClusterDetails_NeedDataSource" AllowAutomaticUpdates="false"
        Skin="Telerik" OnItemDataBound="gvZoneClusterDetails_ItemDataBound" EnableEmbeddedSkins="false"
        OnItemCommand="gvZoneClusterDetails_ItemCommand" EnableHeaderContextMenu="true"
        EnableHeaderContextFilterMenu="true" AllowFilteringByColumn="true">
        <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="ExistMFInvestlist">
        </ExportSettings>
        <MasterTableView DataKeyNames="A_AdviserId,AZOC_Type,AZOC_ZoneClusterId,AR_RMId,AZOC_Name"
            EditMode="PopUp" CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="false"
            CommandItemSettings-AddNewRecordText="Add Zone/Cluster">
            <Columns>
                <telerik:GridEditCommandColumn EditText="Update" UniqueName="editColumn" CancelText="Cancel"
                    UpdateText="Update">
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn Visible="false" UniqueName="AZOC_ZoneClusterId" HeaderText="Type"
                    DataField="AZOC_ZoneClusterId" SortExpression="AZOC_ZoneClusterId" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" UniqueName="AR_RMId" HeaderText="Type" DataField="AR_RMId"
                    SortExpression="AR_RMId" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="AZOC_Type" HeaderText="Type" DataField="AZOC_Type"
                    SortExpression="AZOC_Type" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="Name" HeaderText="Head" DataField="Name" SortExpression="Name"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderStyle-Width="200px"  UniqueName="AZOC_Description" HeaderText="Description" DataField="AZOC_Description"
                    SortExpression="AZOC_Description" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="Zone" HeaderText="Zone" DataField="AZOC_Name"
                    SortExpression="AZOC_Name" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn HeaderStyle-Width="110px" UniqueName="AZOC_CreatedOn" HeaderText="Created On" DataField="AZOC_CreatedOn"
                    SortExpression="AZOC_CreatedOn" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" DataFormatString="{0:d}">
                    <FilterTemplate>
                        <telerik:RadDatePicker runat="server">
                        </telerik:RadDatePicker>
                    </FilterTemplate>
                </telerik:GridDateTimeColumn>
                <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete this Zone/Cluster?"
                    ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                    Text="Delete">
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings EditFormType="Template" FormTableStyle-Width="1000px">
                <FormTemplate>
                    <table id="Table2" cellspacing="2" cellpadding="1" border="0" rules="none" style="border-collapse: collapse;
                        background: white;">
                        <tr class="EditFormHeader">
                            <td colspan="2" style="font-size: small">
                                <asp:Label ID="EditFormHeader" runat="server" CssClass="HeaderTextSmall" Text="Add Zone/Cluster"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="Table3" cellspacing="1" cellpadding="1" border="0" width="390px">
                                    <tr>
                                        <td align="right">
                                            <asp:Label runat="server" Text="Type :" ID="label"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <telerik:RadComboBox Text='<%# Bind("AZOC_Type") %>' AutoPostBack="true" OnSelectedIndexChanged="rcbEditFormAddType_SelectedIndexChanged"
                                                runat="server" ID="rcbEditFormAddType" EmptyMessage="Select">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Select" Value="0" />
                                                    <telerik:RadComboBoxItem Text="Zone" Value="1" />
                                                    <telerik:RadComboBoxItem Text="Custer" Value="2" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <span id="Span1" class="spnRequiredField">*</span>
                                            <asp:RequiredFieldValidator runat="server" CssClass="rfvPCG" ErrorMessage="Please select a type"
                                                Display="Dynamic" ValidationGroup="btnSubmit" ControlToValidate="rcbEditFormAddType"
                                                InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label runat="server" Text="Name :" ID="Label1"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtName" Text='<%# Bind("Name") %>' runat="server"></asp:TextBox>
                                            <span id="Span2" class="spnRequiredField">*</span>
                                            <asp:RequiredFieldValidator ValidationGroup="btnSubmit" ErrorMessage="Please enter a name"
                                                runat="server" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label runat="server" Text="Head :" ID="Label2"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox runat="server" ID="rcbHead" Text="Show" EmptyMessage="Select">
                                            </telerik:RadComboBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="rfvPCG"
                                                ErrorMessage="Please select a head" Display="Dynamic" ValidationGroup="btnSubmit"
                                                ControlToValidate="rcbHead" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trPickAZone">
                                        <td align="right">
                                            <asp:Label runat="server" ID="lblPickAZone" Text="Zone :"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <telerik:RadComboBox runat="server" ID="rcbPickAZone" Text="Show" EmptyMessage="Select">
                                            </telerik:RadComboBox>
                                            <span id="Span3" class="spnRequiredField">*</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rfvPCG"
                                                ErrorMessage="Please select a Zone" Display="Dynamic" ValidationGroup="btnSubmit"
                                                ControlToValidate="rcbPickAZone" InitialValue="0">
                                            </asp:RequiredFieldValidator>
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
                                    </tr>
                                </table>
                            </td>
                            <td>
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
</div>
