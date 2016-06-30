<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewFPUtilityUserDetails.ascx.cs"
    Inherits="WealthERP.FP.ViewFPUtilityUserDetails" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<telerik:RadStyleSheetManager ID="RdStylesheet" runat="server">
</telerik:RadStyleSheetManager>
<table width="100%" class="TableBackground">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            View Leads
                        </td>
                        <td align="right" style="padding-bottom: 2px;">
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="80%">
    <tr>
        <td>
            <telerik:RadGrid ID="gvLeadList" runat="server" AllowAutomaticDeletes="false" PageSize="10"
                EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                ShowStatusBar="false" ShowFooter="false" AllowPaging="true" AllowSorting="true" OnNeedDataSource="gvLeadList_OnNeedDataSource"
                GridLines="none" AllowAutomaticInserts="false" Skin="Telerik" EnableHeaderContextMenu="true"
                Visible="false">
                <MasterTableView Width="80%" DataKeyNames="FPUUD_UserId,C_CustomerId" AllowMultiColumnSorting="True"
                    AutoGenerateColumns="false" CommandItemDisplay="None" GroupsDefaultExpanded="false"
                    ExpandCollapseColumn-Groupable="true" GroupLoadMode="Client" ShowGroupFooter="true">
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="ActionForProspect"
                            Visible="true" HeaderStyle-Width="140px">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlActionForProspect" OnSelectedIndexChanged="ddlActionForProspect_OnSelectedIndexChanged"
                                    CssClass="cmbField" runat="server" EnableEmbeddedSkins="false" AutoPostBack="true"
                                    Visible='<%# Eval("FPUUD_IsProspectmarked") %>' Width="120px" AppendDataBoundItems="true">
                                    <Items>
                                        <asp:ListItem Text="Select" Value="Select" Selected="true" />
                                        <asp:ListItem Text="Profile" Value="Profile" />
                                    </Items>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Customer Name" DataField="FPUUD_Name"
                            UniqueName="FPUUD_Name" SortExpression="FPUUD_Name" AutoPostBackOnFilter="true"
                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="PAN No." DataField="FPUUD_PAN"
                            UniqueName="FPUUD_PAN" SortExpression="FPUUD_PAN" AutoPostBackOnFilter="true"
                            AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FPUUD_EMail" SortExpression="FPUUD_EMail" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                            HeaderText="Email Id" UniqueName="FPUUD_EMail">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FPUUD_MobileNo" SortExpression="FPUUD_MobileNo"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AllowFiltering="true" HeaderText="Mobile No." UniqueName="FPUUD_MobileNo">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FPUUD_CreatedOn" SortExpression="FPUUD_CreatedOn"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AllowFiltering="true" HeaderText="Date of Signup" UniqueName="FPUUD_CreatedOn">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FPUUD_ModifiedOn" SortExpression="FPUUD_ModifiedOn"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AllowFiltering="true" HeaderText="Last Date Entry" UniqueName="FPUUD_ModifiedOn">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="stage" SortExpression="stage" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                            HeaderText="Stage" UniqueName="stage">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IsCustomerAvailable" SortExpression="IsCustomerAvailable"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AllowFiltering="true" HeaderText="Existing Customer" UniqueName="IsCustomerAvailable">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RiskClass" SortExpression="RiskClass" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                            HeaderText="Risk Class" UniqueName="RiskClass">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </td>
    </tr>
</table>
