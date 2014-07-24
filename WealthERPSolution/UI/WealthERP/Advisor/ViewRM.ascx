<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewRM.ascx.cs" Inherits="WealthERP.Advisor.ViewRM"
    Debug="false" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<script language="javascript" type="text/javascript">


    var column = null;
    var columnName = null;


    function MenuShowing(sender, args) {

        if (column == null) return;
        if (columnName == null) return;


        var menu = sender; var items = menu.get_items();


        if (columnName == "WealthERP Id") {


            var i = 0;


            while (i < items.get_count()) {


                if (!(items.getItem(i).get_value() in { 'NoFilter': '', 'Contains': '', 'EqualTo': '', 'GreaterThan': '', 'LessThan': '' })) {
                    var item = items.getItem(i);
                    if (item != null)
                        item.set_visible(false);
                }
                else {
                    var item = items.getItem(i);
                    if (item != null)
                        item.set_visible(true);
                } i++;


            }


        }
        else if (DataType = "System.String" || columnName == "RMName" || columnName == "StaffType" || columnName == "StaffRole" || columnName == "BranchList") {

            {
                var j = 0;



                while (j < items.get_count()) {

                    //                        alert(items.getItem(j).get_value());
                    if (!(items.getItem(j).get_value() in { 'NoFilter': '', 'Contains': '', 'DoesNotContain': '', 'StartsWith': '', 'EndsWith': '' })) {
                        var item = items.getItem(j);
                        if (item != null)
                            item.set_visible(false);
                    }
                    else {
                        var item = items.getItem(j);
                        if (item != null)
                            item.set_visible(true);
                    } j++;


                }


            }

        }


        column = null;
        columnName = null;


    }


    function filterMenuShowing(sender, eventArgs) {
        column = eventArgs.get_column();
        columnName = eventArgs.get_column().get_uniqueName();
    }


</script>

<table class="TableBackground" width="100%">
    <tr>
        <td colspan="4">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblTitle" runat="server" Text="View Staff"></asp:Label>
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
        <%--<tr>
            <td class="HeaderCell">
                <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="View Staff"></asp:Label>
                <hr />
            </td>
        </tr>--%>
        <tr>
            <td>
                <telerik:RadGrid ID="gvRMList" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                    AllowAutomaticInserts="false" OnNeedDataSource="gvRMList_OnNeedDataSource" OnPreRender="gvRMList_PreRender">
                    <ExportSettings FileName="Staff Details" HideStructureColumns="true" ExportOnlyData="true">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="UserId" Width="100%" AllowMultiColumnSorting="True"
                        AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                            
                            <telerik:GridTemplateColumn ItemStyle-Width="80Px" AllowFiltering="false" >
                                                <ItemTemplate>
<%--                                                    <telerik:RadComboBox ID="ddlMenu" UniqueName="ddlMenu" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged"
                                                        runat="server" EnableEmbeddedSkins="true" Skin ="Telerik"
                                                        AllowCustomText="true" Width="120px" AutoPostBack="true">
                                                        <Items>
                                                            <telerik:RadComboBoxItem ImageUrl="~/Images/Select.png" Text="Select" Value="0" Selected="true"></telerik:RadComboBoxItem>
                                                            <telerik:RadComboBoxItem Text="View" Value="View" ImageUrl="~/Images/DetailedView.png" runat="server"></telerik:RadComboBoxItem>
                                                            <telerik:RadComboBoxItem ImageUrl="~/Images/RecordEdit.png" Text="Edit" Value="Edit" runat="server"></telerik:RadComboBoxItem>
                                                        </Items>--%>
                                                    <%--</telerik:RadComboBox>--%>
                                                    <asp:DropDownList ID="ddlMenu"  OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged" runat="server" EnableEmbeddedSkins="false" AutoPostBack="true"
                                                     Width="120px" AppendDataBoundItems="true" CssClass="cmbField" >
                                                     <items>
                                                     <asp:ListItem ImageUrl="~/Images/Select.png" Text="Select" Value="0" Selected="true"></asp:ListItem>
                                                     <asp:ListItem Text="View" Value="View" ImageUrl="~/Images/DetailedView.png" runat="server"></asp:ListItem>
                                                     <asp:ListItem ImageUrl="~/Images/RecordEdit.png" Text="Edit" Value="Edit" runat="server" ></asp:ListItem>
                                                     </items>
                                                     </asp:DropDownList>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="RMName" SortExpression="RMName" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                HeaderText="Name" UniqueName="RMName">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WealthERP Id" SortExpression="WealthERP Id" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                HeaderText="Id" UniqueName="WealthERP Id">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="RoleList" SortExpression="RoleList" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                HeaderText="User Role" UniqueName="RoleList">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="StaffCode" SortExpression="StaffCode" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                HeaderText="Employee Code" UniqueName="StaffCode">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn DataField="RMName" AllowFiltering="true" HeaderText=""
                                UniqueName="ActiveLevel">
                                <FilterTemplate>
                                <asp:DropDownList Visible="true" runat="server" ID="" OnSelectedIndexChanged="ddlNameFilter_OnSelectedIndexChanged" AutoPostBack="true"  CssClass="GridViewCmbField"></asp:DropDownList>
                                </FilterTemplate>
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                           <%-- <telerik:GridBoundColumn DataField="StaffCode" AllowFiltering="false" HeaderText="Staffcode"
                                UniqueName="ActiveLevel">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn Visible="false" DataField="AAC_AgentCode" SortExpression="AAC_AgentCode" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                HeaderText="SubBrokerCode" UniqueName="AAC_AgentCode">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="StaffRole" SortExpression="StaffRole" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                HeaderText="Role" UniqueName="Role">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="BranchList" SortExpression="BranchList" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                HeaderText="Branch" UniqueName="Branch">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Email" AllowFiltering="false" HeaderText="Email"
                                UniqueName="ActiveLevel">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Mobile Number" AllowFiltering="false" HeaderText="Mobile"
                                UniqueName="ActiveLevel">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        <ClientEvents OnFilterMenuShowing="filterMenuShowing" />
                    </ClientSettings>
                    <FilterMenu OnClientShown="MenuShowing" />
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