<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewRM.ascx.cs" Inherits="WealthERP.Advisor.ViewRM"
    Debug="false" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<table class="TableBackground" width="100%">
    <tr id="trMessage" runat="server" visible="false">
        <td>
            <asp:Label ID="lblMessage" runat="server" Text="No Records Found..." CssClass="Error"></asp:Label>
        </td>
    </tr>
    <%--<tr>
        <td class="HeaderCell">
            <asp:Label ID="Label1" runat="server" Text="RM List" CssClass="HeaderTextBig"></asp:Label>
        </td>
    </tr>--%>
    <%-- <tr align="center">
        <td colspan="2" class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>--%>
    <tr id="trBMBranchs" runat="server">
        <td colspan="2" style="float: left">
            <asp:Label ID="lblChooseBr" runat="server" Font-Bold="true" Font-Size="Small" CssClass="FieldName"
                Text="Branch: "></asp:Label>
            &nbsp;&nbsp;
            <asp:DropDownList ID="ddlBMStaffList" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlBMStaffList_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr width="100%">
        <td>
            <div id="print" runat="server" width="100%">
            </div>
        </td>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
    <table width="100%">
        <tr>
            <td class="HeaderCell">
                <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="View Staff"></asp:Label>
                <hr />
            </td>
        </tr>
        <tr>
            <td>
                <asp:ImageButton ID="btnExportFilteredData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                    OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
                <telerik:RadGrid ID="gvRMList" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                    AllowAutomaticInserts="false" OnNeedDataSource="gvRMList_OnNeedDataSource">
                    <ExportSettings FileName="Staff Details" HideStructureColumns="true" ExportOnlyData="true">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="UserId" Width="100%" AllowMultiColumnSorting="True"
                        AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                            <telerik:GridTemplateColumn ItemStyle-Width="80Px" AllowFiltering="false">
                                <%--<ItemTemplate>
                                    <asp:DropDownList ID="ddlMenu" AutoPostBack="true" runat="server" CssClass="GridViewCmbField"
                                        OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged" EnableViewState="True">
                                        <asp:ListItem>Select </asp:ListItem>
                                        <asp:ListItem Text="View profile" Value="View profile">View profile</asp:ListItem>
                                        <asp:ListItem Text="Edit Profile" Value="Edit Profile">Edit Profile</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>--%>
                                <%-- <asp:TemplateField HeaderText="Action" ItemStyle-Width="80Px">--%>
                                <ItemTemplate>
                                    <telerik:RadComboBox ID="ddlMenu" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged"
                                        CssClass="cmbField" runat="server" EnableEmbeddedSkins="false" Skin="Telerik"
                                        AllowCustomText="true" Width="120px" AutoPostBack="true">
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="~/Images/Select.png" Text="Select" Value="0" Selected="true">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem Text="View profile" Value="View profile" ImageUrl="~/Images/DetailedView.png"
                                                runat="server"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="~/Images/RecordEdit.png" Text="Edit Profile" Value="Edit Profile"
                                                runat="server"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </ItemTemplate>
                                <%-- </asp:TemplateField>--%>
                            </telerik:GridTemplateColumn>
                            <%-- <telerik:GridTemplateColumn AllowFiltering="true">
                                <ItemTemplate>
                                    <asp:Label CssClass="label" ID="lblContainer" runat="server" Text='<%# Eval("RMName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridTemplateColumn> --%>
                            <telerik:GridBoundColumn DataField="RMName" AllowFiltering="true" HeaderText="Name"
                                UniqueName="Name">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn DataField="RMName" AllowFiltering="true" HeaderText=""
                                UniqueName="ActiveLevel">
                                <FilterTemplate>
                                <asp:DropDownList Visible="true" runat="server" ID="" OnSelectedIndexChanged="ddlNameFilter_OnSelectedIndexChanged" AutoPostBack="true"  CssClass="GridViewCmbField"></asp:DropDownList>
                                </FilterTemplate>
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn DataField="StaffCode" AllowFiltering="false" HeaderText="Staffcode"
                                UniqueName="ActiveLevel">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="StaffType" AllowFiltering="false" HeaderText="Type"
                                UniqueName="ActiveLevel">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="StaffRole" AllowFiltering="false" HeaderText="Role"
                                UniqueName="ActiveLevel">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="BranchList" AllowFiltering="false" HeaderText="Branch"
                                UniqueName="ActiveLevel">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Email" AllowFiltering="false" HeaderText="Email"
                                UniqueName="ActiveLevel">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Mobile Number" AllowFiltering="false" HeaderText="Mobile"
                                UniqueName="ActiveLevel">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WealthERP Id" AllowFiltering="false" HeaderText="Id"
                                UniqueName="ActiveLevel">
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
        <%--<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="View Staff"></asp:Label>
                <asp:GridView ID="gvRMList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    DataKeyNames="UserId"  OnSorting="gvRMList_Sorting" width="100%" RowStyle-Wrap="true"
                    CssClass="GridViewStyle"
                    ShowFooter="True" 
                onselectedindexchanged="gvRMList_SelectedIndexChanged">
                    <RowStyle CssClass="RowStyle" />
                    <FooterStyle CssClass="FooterStyle" />
                    <PagerStyle CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlMenu" AutoPostBack="true" runat="server" CssClass="GridViewCmbField" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged" EnableViewState="True">
                                    <asp:ListItem>Select </asp:ListItem>
                                    <asp:ListItem Text="View profile" Value="View profile">View profile</asp:ListItem>
                                     <asp:ListItem Text="Edit Profile" Value="Edit Profile">Edit Profile</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                      <asp:BoundField DataField="RMName" HeaderText="Name" SortExpression="RMName" ItemStyle-Wrap="false" />
                       
                     <asp:BoundField DataField="StaffCode" HeaderText="Staffcode" />
                        <asp:BoundField DataField="StaffType" HeaderText="Type" />
                        <asp:BoundField DataField="StaffRole" HeaderText="Role" />
                        <asp:BoundField DataField="BranchList" HeaderText="Branch" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />                        
                        <asp:BoundField DataField="Mobile Number" HeaderText="Mobile" />                        
                        <asp:BoundField DataField="WealthERP Id" HeaderText="Id"  />
                        
                    </Columns>
                </asp:GridView>
           
        </td>
    </tr>--%>
    </table>
</asp:Panel>
<%--<div id="DivPager" runat="server">
    <table style="width: 100%">
        <tr id="trPager" runat="server">
            <td>
                <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
            </td>
        </tr>
    </table>
</div>--%>
<asp:HiddenField ID="hdnCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" Value="RMName ASC" />
<%-- Hiddenfields for BranchId, BranchHeadId and all parameters --%>
<asp:HiddenField ID="hdnbranchID" runat="server" Visible="false" />
<asp:HiddenField ID="hdnbranchHeadId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnall" runat="server" Visible="false" />
<%-- End --%>