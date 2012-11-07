<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerISARequestList.ascx.cs"
    Inherits="WealthERP.Customer.CustomerISARequestList" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<table width="100%" class="TableBackground">
    <tr>
        <td colspan="5">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            ISA Status
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="btnExportFilteredData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr id="trErrorMessage" align="center" style="width: 100%" runat="server">
        <td align="center" style="width: 100%">
            <div class="failure-msg" style="text-align: center" align="center">
                No ISAQueue Records found!!!!
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadGrid ID="gvISArequest" runat="server" CssClass="RadGrid" GridLines="None" OnNeedDataSource="gvISArequest_OnNeedDataSource"
                Width="100%" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false"
                ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
                AllowAutomaticUpdates="false" Skin="Telerik" EnableEmbeddedSkins="false" EnableHeaderContextMenu="true"
                EnableHeaderContextFilterMenu="true" AllowFilteringByColumn="true">
                <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="ISAQueuelist">
                </ExportSettings>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="AISAQ_RequestQueueid,WWFSM_StepCode,StatusCode">
                    <Columns>
                        <%--<telerik:GridTemplateColumn ItemStyle-Width="80Px" AllowFiltering="false">
                        <ItemTemplate>
                            <telerik:RadComboBox ID="ddlAction" OnSelectedIndexChanged="ddlAction_SelectedIndexChanged"
                                CssClass="cmbField" runat="server" EnableEmbeddedSkins="false" Skin="Telerik"
                                AllowCustomText="true" Width="120px" AutoPostBack="true">
                                <Items>
                                    <telerik:RadComboBoxItem ImageUrl="~/Images/Select.png" Text="Select" Value="0" Selected="true">
                                    </telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem Text="View" Value="View" ImageUrl="~/Images/DetailedView.png"
                                        runat="server"></telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem ImageUrl="~/Images/RecordEdit.png" Text="Edit" Value="Edit"
                                        runat="server"></telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem ImageUrl="~/Images/DeleteRecord.png" Text="Delete" Value="Delete"
                                        runat="server"></telerik:RadComboBoxItem>
                                </Items>
                            </telerik:RadComboBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>--%>
                       <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action">
                                <ItemTemplate>
                                    <telerik:RadComboBox ID="ddlAction" CssClass="cmbField" runat="server" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChange" EnableEmbeddedSkins="false" Skin="Telerik"
                                        AllowCustomText="true" Width="120px" AutoPostBack="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Select" Value="Select"
                                                Selected="true"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem Text="View" Value="View"
                                                runat="server"></telerik:RadComboBoxItem>                                         
                                        </Items>
                                    </telerik:RadComboBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="AISAQ_RequestQueueid" AllowFiltering="false"
                            HeaderText="ISA RequestId">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AISAQ_date" AllowFiltering="false" HeaderText="Request Date">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AISAQ_Status" AllowFiltering="false" HeaderText="Status">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AISAQ_Priority" AllowFiltering="false" HeaderText="Priority"
                            FooterStyle-HorizontalAlign="Right">
                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CustomerName" AllowFiltering="false" HeaderText="Customer Name">
                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="StepName" AllowFiltering="false"
                            HeaderText="Current Stage">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AISAQD_Status" AllowFiltering="false"
                            HeaderText="Current Stage Status">
                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="BranchName" AllowFiltering="false" HeaderText="Branch"
                            FooterStyle-HorizontalAlign="Right">
                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings ReorderColumnsOnClient="True" AllowColumnsReorder="True" EnableRowHoverStyle="true">
                  
                    <Resizing AllowColumnResize="true" />
                    <Selecting AllowRowSelect="true" />
                </ClientSettings>
            </telerik:RadGrid>
        </td>
    </tr>
</table>
