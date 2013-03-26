﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserCustomer.ascx.cs"
    Inherits="WealthERP.Advisor.AdviserCustomer" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<telerik:RadStyleSheetManager ID="RdStylesheet" runat="server">
</telerik:RadStyleSheetManager>

<script language="javascript" type="text/javascript">

    //    function GridCreated(sender, args) {
    //        var scrollArea = sender.gvCustomerList;
    //        var dataHeight = sender.get_masterTableView().get_element().clientHeight;
    //        if (dataHeight <410) {
    //            scrollArea.style.height = dataHeight + 17 + "px";
    //        }
    //    }

    window.onresize = window.onload = Resize;
    function showmessage() {
        var bool = window.confirm('Are you sure you want to delete this profile?');

        if (bool) {
            document.getElementById("ctrl_AdviserCustomer_hdnMsgValue").value = 1;
            document.getElementById("ctrl_AdviserCustomer_hiddenassociation").click();
            return false;
        }
        else {
            document.getElementById("ctrl_AdviserCustomer_hdnMsgValue").value = 0;
            document.getElementById("ctrl_AdviserCustomer_hiddenassociation").click();
            return true;
        }
    }
    function OnClientItemsRequestedHandler(sender, eventArgs) {
        //set the max allowed height of the combo  
        var MAX_ALLOWED_HEIGHT = 220;
        //this is the single item's height  
        var SINGLE_ITEM_HEIGHT = 22;

        var calculatedHeight = sender.get_items().get_count() * SINGLE_ITEM_HEIGHT;

        var RadComboRM = sender.get_dropDownElement();

        if (calculatedHeight > MAX_ALLOWED_HEIGHT) {
            setTimeout(
            function() {
                RadComboRM.firstChild.style.height = MAX_ALLOWED_HEIGHT + "px";
            }, 20
        );
        }
        else {
            setTimeout(
            function() {
                RadComboRM.firstChild.style.height = calculatedHeight + "px";
            }, 20
        );
        }
    }
    
    function showassocation() {

        var bool = window.confirm('Customer has associations, cannot be deleted');
        if (bool) {
            document.getElementById("ctrl_AdviserCustomer_hdnassociation").value = 1;
            document.getElementById("ctrl_AdviserCustomer_hiddenassociationfound").click();
            return false;
        }
        else {
            document.getElementById("ctrl_AdviserCustomer_hdnassociation").value = 0;
            document.getElementById("ctrl_AdviserCustomer_hiddenassociationfound").click();
            return true;
        }
    }
    function Print_Click(div, btnID) {
        var ContentToPrint = document.getElementById(div);
        var myWindowToPrint = window.open('', '', 'width=200,height=100,toolbar=0,scrollbars=0,status=0,resizable=0,location=0,directories=0');
        myWindowToPrint.document.write(document.getElementById(div).innerHTML);
        myWindowToPrint.document.close();
        myWindowToPrint.focus();
        myWindowToPrint.print();
        myWindowToPrint.close();
        var btn = document.getElementById(btnID);
        btn.click();
    }
    function AferExportAll(btnID) {
        var btn = document.getElementById(btnID);
        btn.click();
    }
    function getScrollBottom(p_oElem) {
        return p_oElem.scrollHeight - p_oElem.scrollTop - p_oElem.clientHeight;
    }
   

</script>

<script language="javascript" type="text/javascript">
    var ht = document.getElementById('pnlCustomerList').offsetHeight;
    var ele = document.getElementById('pnlCustomerList')
    if (ht < 410) {
        ele.style.height = ht;
    }
    else {
        ele.style.height = 410
    }
</script>

<%--<style type="text/css" runat="server">
    .rgDataDiv
    {
        height: auto;
        width: 101.5% !important;
    }
</style>--%>
<%--<style   type="text/css" runat="server">
.myPanelClass { max-height: 410px; overflow: auto; }
</style>--%>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            Customer List
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
<table width="100%">
    <tr>
        <td align="center">
            <div class="success-msg" id="CreationSuccessMessage" runat="server" visible="false"
                align="center">
                Record deleted successfully...
            </div>
        </td>
    </tr>
</table>
<table width="100%" cellspacing="0" cellpadding="0" runat="server">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
                No Records found.....
            </div>
        </td>
    </tr>
</table>
<div style="width: 100%; height: 50%;">
    <asp:Panel ID="pnlCustomerList" runat="server" class="Landscape" ScrollBars="Both"
        Visible="false" Width="100%">
        <table width="100%" cellspacing="0" cellpadding="1" style="height: 30%">
            <tr>
                <td>
                    <telerik:RadGrid ID="gvCustomerList" runat="server" AllowAutomaticDeletes="false"
                        PagerStyle-AlwaysVisible="true" EnableEmbeddedSkins="false" AllowFilteringByColumn="true"
                        AutoGenerateColumns="False" ShowStatusBar="false" ShowFooter="false" AllowPaging="true"
                        AllowSorting="true" Width="100%" GridLines="none" AllowAutomaticInserts="false"
                        Height="20%" OnItemDataBound="gvCustomerList_ItemDataBound" Skin="Telerik" EnableHeaderContextMenu="true"
                        OnNeedDataSource="gvCustomerList_OnNeedDataSource" OnPreRender="gvCustomerList_PreRender">
                        <ExportSettings HideStructureColumns="true">
                        </ExportSettings>
                        <MasterTableView DataKeyNames="CustomerId,UserId,RMId" Width="100%" AllowMultiColumnSorting="True"
                            AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="Action" DataField="Action"
                                    HeaderStyle-Width="140px">
                                    <ItemTemplate>
                                        <telerik:RadComboBox ID="ddlAction" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChanged"
                                            CssClass="cmbField" runat="server" EnableEmbeddedSkins="false" AllowCustomText="false"
                                            Width="130px" AutoPostBack="true" Height="150px">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Select" Value="0" Selected="false" />
                                                <telerik:RadComboBoxItem Text="ShortCuts" Value="QuickLinks" />
                                                <telerik:RadComboBoxItem Text="Dashboard" Value="Dashboard" />
                                                <telerik:RadComboBoxItem Text="Profile" Value="Profile" />
                                                <telerik:RadComboBoxItem Text="Assets" Value="Portfolio" />
                                                <telerik:RadComboBoxItem Text="Alerts" Value="Alerts" />
                                                <telerik:RadComboBoxItem Text="Delete Profile" Value="DeleteProfile" />
                                                <telerik:RadComboBoxItem Text="Financial Planning" Value="FinancialPlanning" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="CustomerId" UniqueName="CustomerId" HeaderText="Customer Id"
                                    ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="100px"
                                    SortExpression="CustomerId" FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Cust_Comp_Name" UniqueName="Cust_Comp_Name" HeaderText="Name" 
                                    ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="140px"
                                    SortExpression="Cust_Comp_Name" FilterControlWidth="120px" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="140px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Group" UniqueName="ParentId" HeaderText="Group"
                                    AutoPostBackOnFilter="true" ShowFilterIcon="false" AllowFiltering="true" HeaderStyle-Width="145px">
                                    <ItemStyle Width="145px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                    <%-- <FilterTemplate>
                                    <telerik:RadComboBox ID="RadComboGroup" AutoPostBack="true" AllowFiltering="true"
                                        Height="200px" CssClass="cmbField" Width="130px" IsFilteringEnabled="true" AppendDataBoundItems="true"
                                        OnPreRender="rcbgroup_PreRender" EnableViewState="true" OnSelectedIndexChanged="RadComboGroup_SelectedIndexChanged"
                                        SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("ParentId").CurrentFilterValue %>'
                                        runat="server">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="ALL" Value="" Selected="false"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

                                        <script type="text/javascript">
                                            function GroupIndexChanged(sender, args) {
                                                var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");
                                                tableView.filter("ParentId", args.get_item().get_value(), "EqualTo");
                                            }
                                        </script>

                                    </telerik:RadScriptBlock>
                                </FilterTemplate>--%>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PANNumber" UniqueName="PANNumber" HeaderText="PAN"
                                    SortExpression="PANNumber" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                                    AllowFiltering="true" HeaderStyle-Width="100px" FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="BranchName" UniqueName="BranchName" HeaderText="Branch"
                                    AutoPostBackOnFilter="true" ShowFilterIcon="false" AllowFiltering="true" HeaderStyle-Width="100px"
                                    FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AssignedRM" UniqueName="RMId" HeaderText="RM"
                                    AutoPostBackOnFilter="true" SortExpression="RMId" ShowFilterIcon="false" AllowFiltering="true"
                                    HeaderStyle-Width="140px">
                                    <ItemStyle Width="140px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                    <FilterTemplate>
                                        <telerik:RadComboBox ID="RadComboRM" AutoPostBack="true" AllowFiltering="true" CssClass="cmbField"
                                            Height="" Width="120px" IsFilteringEnabled="true" AppendDataBoundItems="true"
                                            OnClientItemsRequested="OnClientItemsRequestedHandler" DropDownWidth="130px"
                                            OnClientDropDownOpening="OnClientItemsRequestedHandler" OnPreRender="rcbRM_PreRender"
                                            EnableViewState="true" OnSelectedIndexChanged="RadComboRM_SelectedIndexChanged"
                                            SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("RMId").CurrentFilterValue %>'
                                            runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="ALL" Value="" Selected="false"></telerik:RadComboBoxItem>
                                            </Items>
                                        </telerik:RadComboBox>
                                        <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">

                                            <script type="text/javascript">
                                                function TransactionIndexChanged(sender, args) {
                                                    var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");
                                                    tableView.filter("RMId", args.get_item().get_value(), "EqualTo");
                                                }
                                            </script>

                                        </telerik:RadScriptBlock>
                                    </FilterTemplate>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="MobileNumber" UniqueName="MobileNumber" HeaderText="Mobile"
                                    SortExpression="MobileNumber" AllowFiltering="false" HeaderStyle-Width="80px"
                                    FilterControlWidth="60px" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                    <ItemStyle Width="80px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PhoneNumber" UniqueName="PhoneNumber" HeaderText="Phone"
                                    SortExpression="PhoneNumber" ShowFilterIcon="false" AllowFiltering="false" HeaderStyle-Width="100px"
                                    FilterControlWidth="60px" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Email" UniqueName="Email" HeaderText="Email"
                                    SortExpression="Email" ShowFilterIcon="false" AllowFiltering="false" HeaderStyle-Width="140px"
                                    CurrentFilterFunction="Contains">
                                    <ItemStyle Width="140px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Address" UniqueName="Address" HeaderText="Address"
                                    SortExpression="Address" ShowFilterIcon="false" AllowFiltering="false" HeaderStyle-Width="200px"
                                    FilterControlWidth="60px" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="200px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Area" UniqueName="Area" HeaderText="Area" SortExpression="Area"
                                    AutoPostBackOnFilter="true" ShowFilterIcon="false" AllowFiltering="true" HeaderStyle-Width="120px"
                                    FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="City" UniqueName="City" HeaderText="City" SortExpression="City"
                                    ShowFilterIcon="false" AllowFiltering="true" HeaderStyle-Width="85px" FilterControlWidth="70px"
                                    CurrentFilterFunction="Contains">
                                    <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Pincode" UniqueName="Pincode" HeaderText="Pincode"
                                    AutoPostBackOnFilter="true" SortExpression="Pincode" ShowFilterIcon="false" AllowFiltering="true"
                                    HeaderStyle-Width="90px" FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IsProspect" UniqueName="IsProspect" HeaderText="Is Prospect"
                                    ShowFilterIcon="false" AllowFiltering="true" AutoPostBackOnFilter="true" HeaderStyle-Width="79px">
                                    <ItemStyle Width="79px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                    <FilterTemplate>
                                        <telerik:RadComboBox ID="IsProspect" AutoPostBack="true" AllowFiltering="true" CssClass="cmbField"
                                            Width="60px" IsFilteringEnabled="true" AppendDataBoundItems="true" OnPreRender="Isprospect_Prerender"
                                            EnableViewState="true" OnSelectedIndexChanged="IsProspect_SelectedIndexChanged"
                                            SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("IsProspect").CurrentFilterValue %>'
                                            runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="ALL" Value="" Selected="false"></telerik:RadComboBoxItem>
                                                <telerik:RadComboBoxItem Text="Yes" Value="Yes" Selected="false"></telerik:RadComboBoxItem>
                                                <telerik:RadComboBoxItem Text="No" Value="No" Selected="false"></telerik:RadComboBoxItem>
                                            </Items>
                                        </telerik:RadComboBox>
                                        <telerik:RadScriptBlock ID="RadScriptBlock3" runat="server">

                                            <script type="text/javascript">
                                                function TransactionIndexChanged(sender, args) {
                                                    var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");
                                                    tableView.filter("IsProspect", args.get_item().get_value(), "EqualTo");
                                                }
                                            </script>

                                        </telerik:RadScriptBlock>
                                    </FilterTemplate>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IsActive" UniqueName="IsActive" HeaderText="Status"
                                    ShowFilterIcon="false" AllowFiltering="true" AutoPostBackOnFilter="true" HeaderStyle-Width="70px">
                                    <ItemStyle Width="70px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                    <FilterTemplate>
                                        <telerik:RadComboBox ID="Status" AutoPostBack="true" AllowFiltering="true" CssClass="cmbField"
                                            Width="60px" IsFilteringEnabled="true" AppendDataBoundItems="true" OnPreRender="Status_Prerender"
                                            EnableViewState="true" OnSelectedIndexChanged="Status_SelectedIndexChanged" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("IsActive").CurrentFilterValue %>'
                                            runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="ALL" Value="" Selected="false"></telerik:RadComboBoxItem>
                                                <telerik:RadComboBoxItem Text="Active" Value="Active" Selected="false"></telerik:RadComboBoxItem>
                                                <telerik:RadComboBoxItem Text="InActive" Value="InActive" Selected="false"></telerik:RadComboBoxItem>
                                            </Items>
                                        </telerik:RadComboBox>
                                        <telerik:RadScriptBlock ID="RadScriptBlock4" runat="server">

                                            <script type="text/javascript">
                                                function StatusIndexChanged(sender, args) {
                                                    var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");
                                                    tableView.filter("IsActive", args.get_item().get_value(), "EqualTo");
                                                }
                                            </script>

                                        </telerik:RadScriptBlock>
                                    </FilterTemplate>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ADUL_ProcessId" UniqueName="ADUL_ProcessId" HeaderText="Process Id"
                                    SortExpression="ADUL_ProcessId" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    HeaderStyle-Width="85px" FilterControlWidth="60px" CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <ItemStyle Width="55px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <%-- <Scrolling AllowScroll="true" />--%>
                            <%--<ClientEvents OnGridCreated="GridCreated" />--%>
                            <Resizing AllowColumnResize="true" />
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                    <%--</div>--%>
                </td>
            </tr>
        </table>
        <%--</div>--%>
    </asp:Panel>
</div>
<asp:HiddenField ID="hdnMsgValue" runat="server" />
<asp:HiddenField ID="hdnassociationcount" runat="server" />
<%--<div>
    <telerik:RadRotator ID="RadRotatorImage" runat="server" Width="224px" Height="112px"
        CssClass="rotatorStyle" ItemHeight="112" ItemWidth="112" ScrollDuration="500"
        RotatorType="SlideShow">
        <Items>
            <telerik:RadRotatorItem>
                <ItemTemplate>
                    <asp:Image runat="server" ID="Image" ImageUrl='~/Images/reprocess.png' CssClass="RotatorItem">
                    </asp:Image>
                </ItemTemplate>
            </telerik:RadRotatorItem>
            <telerik:RadRotatorItem>
                <ItemTemplate>
                    <asp:Image runat="server" ID="Image1" ImageUrl='~/Images/rollback.png' CssClass="RotatorItem">
                    </asp:Image>
                </ItemTemplate>
            </telerik:RadRotatorItem>
            <telerik:RadRotatorItem>
                <ItemTemplate>
                    <asp:Image runat="server" ID="Image2" ImageUrl='~/Images/ManageRejects.png' CssClass="RotatorItem">
                    </asp:Image>
                </ItemTemplate>
            </telerik:RadRotatorItem>
            <telerik:RadRotatorItem>
                <ItemTemplate>
                    <asp:Image runat="server" ID="Image3" ImageUrl='~/Images/Imagego.jpg' CssClass="RotatorItem">
                    </asp:Image>
                </ItemTemplate>
            </telerik:RadRotatorItem>
            <telerik:RadRotatorItem>
                <ItemTemplate>
                    <asp:Image runat="server" ID="Image4" ImageUrl='~/Images/mozilla.jpg' CssClass="RotatorItem">
                    </asp:Image>
                </ItemTemplate>
            </telerik:RadRotatorItem>
        </Items>
    </telerik:RadRotator>--%>
<%-- <telerik:RadTicker AutoStart="true" runat="server" ID="Radticker1" Loop="true" r>
        <Items>
            <telerik:RadTickerItem>Monday</telerik:RadTickerItem>
            <telerik:RadTickerItem>Tuesday</telerik:RadTickerItem>
            <telerik:RadTickerItem>Wednesday</telerik:RadTickerItem>
            <telerik:RadTickerItem>Thursday</telerik:RadTickerItem>
            <telerik:RadTickerItem>Friday</telerik:RadTickerItem>
            <telerik:RadTickerItem>Saturday</telerik:RadTickerItem>
            <telerik:RadTickerItem>Sunday</telerik:RadTickerItem>
        </Items>
    </telerik:RadTicker>--%>
<%--</div>--%>
<%--<telerik:RadPanelBar ID="pnlRadBar" runat="server" Skin="Telerik" AllowCollapseAllItems="true"
    ExpandAnimation-Type="InCubic" ExpandMode="SingleExpandedItem">
    <Items>
        <telerik:RadPanelItem runat="server" Text="Customer Grid">
            <Items>
            <telerik:RadPanelItem runat="server" Text ="Customer" >
            </telerik:RadPanelItem>
            <telerik:RadPanelItem runat="server" Text ="List">
            </telerik:RadPanelItem>   
            </Items>
        </telerik:RadPanelItem>
    </Items>
</telerik:RadPanelBar>--%>
<%--</asp:Panel>--%>
<%--<asp:HiddenField ID="hdnNameFilter" runat="server" Visible="false" />--%>
<%--<style>
    .HeaderStyle1
    {
        background-image: url(Images/PCGGridViewHeaderGlass2.jpg);
        background-position: center;
        position: relative;
        background-repeat: repeat-x;
        vertical-align: top;
        top: expression(this.offsetGroup.scrollTop-3);
    }
</style>
--%>
<%--<table id="Table1" class="TableBackground" width="100%" runat="server">
<tr>
<td colspan="3" style="width: 100%;">
<div class="divPageHeading">
    <table cellspacing="0"  width="100%">
        <tr>
        <td align="left">Customer List</td>
        <td  align="right" style="padding-bottom:2px;">
        <asp:ImageButton ID="imgBtnExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="imgBtnExport_Click"
                OnClientClick="setFormat('excel')" Height="25px" Width="25px" />
        </td>
        </tr>
    </table>
</div>
</td>
</tr>
    <tr>
         <td width="50%" align="Right">
        
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
       
        </td>

    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div class="success-msg" id="CreationSuccessMessage" runat="server" visible="false"
                align="center">
                Record deleted successfully...
            </div>
        </td>
    </tr>
</table>
<table id="ErrorMessage" width="100%" cellspacing="0" cellpadding="0" runat="server"
    visible="false">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage1" runat="server" visible="true" align="center">
                No Records found.....
            </div>
        </td>
    </tr>
</table>
<table id="tblExport" class="TableBackground" width="100%" runat="server" cellpadding="0" cellspacing="0">
    <tr>
        
        
    </tr>
</table>
<asp:Panel ID="tbl" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
    <table width="100%" cellspacing="0" cellpadding="0">      
        
        <tr>
            <td class="rightField" width="100%">
                <asp:GridView ID="gvCustomers" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CellPadding="4" CssClass="GridViewStyle" DataKeyNames="CustomerId,UserId,RMId"
                    OnSelectedIndexChanged="gvCustomers_SelectedIndexChanged" OnSorting="gvCustomers_Sort"
                    ShowFooter="true" ShowHeader="true" width="100%">
                    <FooterStyle CssClass="FooterStyle" />
                    <PagerSettings Visible="False" />
                    <RowStyle CssClass="RowStyle" />
                    <EditRowStyle CssClass="EditRowStyle" HorizontalAlign="Left" 
                        VerticalAlign="Top" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <%--<PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />--%>
<%--<headerstyle cssclass="HeaderStyle" />
<alternatingrowstyle cssclass="AltRowStyle" />
<columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="true" CssClass="GridViewCmbField"
                                    OnSelectedIndexChanged="ddlAction_OnSelectedIndexChange">
                                    <asp:ListItem Text="Select" Value="Select" />
                                    <asp:ListItem Text="Shortcuts" Value="QuickLinks" />
                                    <asp:ListItem Text="Dashboard" Value="Dashboard" />
                                    <asp:ListItem Text="Profile" Value="Profile" />
                                    <asp:ListItem Text="Assets" Value="Portfolio" />
                                   <%-- <asp:ListItem Text="User Details" Value="User Details" />--%>
<%--          <asp:ListItem Text="Alerts" Value="Alerts" />
                                    <asp:ListItem Text="Delete Profile" />
                                    <asp:ListItem Text="Financial Planning" Value="FinancialPlanning" />
                                    
                                </asp:DropDownList>
                            </ItemTemplate>--%>
<%-- <FooterTemplate>
                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" />
                            </FooterTemplate>--%>
<%--  </asp:TemplateField>--%>
<%--<asp:BoundField DataField="Group" HeaderText="Group" SortExpression="Group" ItemStyle-Wrap="false" />--%>
<%-- <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblCustName" runat="server" Text="Name"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtCustNameSearch" runat="server" CssClass="GridViewTxtField" 
                                    
                                    onkeydown="return JSdoPostback(event,'ctrl_AdviserCustomer_btnNameSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCustNameHeader" runat="server" 
                                    Text='<%# Eval("Cust_Comp_Name").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Wrap="true">
                            <HeaderTemplate>
                                <asp:Label ID="lblGroup" runat="server" Text="Group"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlGroup" runat="server" AutoPostBack="true" CssClass="GridViewCmbField"
                                    OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblGroupeHeader" runat="server" 
                                    Text='<%# Eval("Group").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <%-- <asp:BoundField DataField="PAN" HeaderStyle-Wrap="false" HeaderText="PAN Number" />--%>
<%-- <asp:TemplateField ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblPAN" runat="server" Text="PAN"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtPAN" runat="server" CssClass="GridViewTxtField" 
                                    
                                    onkeydown="return JSdoPostback(event,'ctrl_AdviserCustomer_btnPANSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPANHeader" runat="server" 
                                    Text='<%# Eval("PAN Number").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                           
                        </asp:TemplateField>
                         <asp:BoundField DataField="BranchName"  ItemStyle-Wrap="false" HeaderText="Branch" />
                          <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblAssignedRM" runat="server" Text="RM"></asp:Label>
                               <br/>
                                <asp:DropDownList ID="ddlAssignedRM" runat="server" AutoPostBack="true" CssClass="GridViewCmbField"
                                    OnSelectedIndexChanged="ddlAssignedRM_SelectedIndexChanged">
                                </asp:DropDownList>
                                
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAssignedRMHeader" runat="server" 
                                    Text='<%# Eval("Assigned RM").ToString() %>'></asp:Label>
                                
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                         
                        <asp:BoundField DataField="Mobile Number" HeaderText="Mobile" />
                        <asp:BoundField DataField="Phone Number" HeaderText="Phone" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Address" HeaderText="Address" 
                            ItemStyle-Wrap="false" />
                        <asp:TemplateField ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblArea" runat="server" Text="Area"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtAreaSearch" runat="server" CssClass="GridViewTxtField" 
                                    
                                    onkeydown="return JSdoPostback(event,'ctrl_AdviserCustomer_btnAreaSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAreaHeader" runat="server" 
                                    Text='<%# Eval("Area").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>--%>
<%--<asp:BoundField DataField="Area" HeaderText="Area" />--%>
<%--  <asp:BoundField DataField="City" HeaderText="City" />
                        <asp:TemplateField ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblPincode" runat="server" Text="Pincode"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtPincodeSearch" runat="server" CssClass="GridViewTxtField" 
                                    
                                    onkeydown="return JSdoPostback(event,'ctrl_AdviserCustomer_btnPincodeSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPincodeHeader" runat="server" 
                                    Text='<%# Eval("Pincode").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>--%>
<%-- <asp:BoundField DataField="IsFPClient" HeaderText="Is FPClient" />--%>
<%-- <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="Is Prospect" 
                            ItemStyle-Wrap="false">
                            <HeaderTemplate>
                             <asp:Label ID="lblHeaderIsProspect" runat="server" Text="Is Prospect"></asp:Label>
                             <br />
                             <asp:DropDownList ID="ddlIsProspect" runat="server" OnPreRender="SetValue" OnSelectedIndexChanged="ddlIsProspect_SelectedIndexChanged" AutoPostBack="true" CssClass="GridViewCmbField">
                                    <asp:ListItem Text="All" Value="2">
                                    </asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="1">
                                    </asp:ListItem>
                                    <asp:ListItem Text="No" Value="0">
                                    </asp:ListItem>
                             </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIsFpClient" runat="server" Text='<%# Eval("IsProspect").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
<%--<asp:BoundField DataField="Assigned RM" HeaderText="Assigned RM" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false" />--%>
<%-- <asp:TemplateField HeaderText="IsActive">
                            <ItemTemplate>
                                <asp:Label ID="lblIsActive" runat="server" Text='<%#Eval("IsActive") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <HeaderTemplate >
                             <asp:Label ID="lblHeaderIsActive" runat="server" Text="Status"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlActiveFilter" runat="server" AutoPostBack="true" CssClass="GridViewCmbField"
                                    OnSelectedIndexChanged="ddlActiveFilter_SelectedIndexChanged">
                                    <asp:ListItem Text="All" Value="2">
                                    </asp:ListItem>
                                    <asp:ListItem Text="Active" Value="1">
                                    </asp:ListItem>
                                    <asp:ListItem Text="InActive" Value="0">
                                    </asp:ListItem>
                                </asp:DropDownList>
                            </HeaderTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblProcessId" runat="server" Text="ProcessId"></asp:Label>
                                <br />--%>
<%--<asp:TextBox ID="txtCustNameSearch" runat="server" CssClass="GridViewTxtField" 
                                    
                                    onkeydown="return JSdoPostback(event,'ctrl_AdviserCustomer_btnNameSearch');" />--%>
<%-- </HeaderTemplate>
                              <ItemTemplate>
                                <asp:Label ID="lblProcessId" runat="server" 
                                    Text='<%# Eval("ADUL_ProcessId").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>
                    </columns>
</asp:GridView>--%>
<%--<hr />
        </td>
    </tr>
</table>
<table id="ErrorMessage" width="100%" cellspacing="0" cellpadding="0" runat="server"
    visible="false">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage1" runat="server" visible="true" align="center">
                No Records found.....
            </div>
        </td>
    </tr>
</table>
<table id="tblExport" class="TableBackground" width="100%" runat="server" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" align="left">
        <asp:ImageButton ID="imgBtnExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="imgBtnExport_Click"
                OnClientClick="setFormat('excel')" Height="25px" Width="25px" />
        </td>
        <td width="50%" align="left">
        
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
       
        </td>
    </tr>
</table>
<asp:Panel ID="tbl" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
    <table width="100%" cellspacing="0" cellpadding="0">      
        
        <tr>
            <td class="rightField" width="100%">--%>
<%--</td> </tr> </table> </asp:Panel>
<table id="tblpager" class="TableBackground" width="100%" runat="server">
    <tr id="trPager" runat="server">
        <td width="100%" align="center">
            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
        </td>
    </tr>
</table>
<table class="TableBackground" width="100%" id="tblGV" runat="server" cellspacing="0"
    cellpadding="0">
    <tr>
        <td>
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
                TargetControlID="imgBtnExport" DynamicServicePath="" BackgroundCssClass="modalBackground"
                Enabled="True" OkControlID="btnOK" CancelControlID="btnCancel" Drag="true" OnOkScript="DownloadScript();">
            </cc1:ModalPopupExtender>--%>
<%--<asp:ImageButton ID="imgBtnWord" ImageUrl="~/App_Themes/Maroon/Images/Export_Word.png"
                runat="server" AlternateText="Word" ToolTip="Export To Word" OnClick="imgBtnWord_Click"
                OnClientClick="setFormat('word')" />
            <asp:ImageButton ID="imgBtnPdf" ImageUrl="~/App_Themes/Maroon/Images/Export_Pdf.png"
                runat="server" AlternateText="PDF" OnClientClick="setFormat('pdf')" ToolTip="Export To PDF"
                OnClick="imgBtnPdf_Click" />
            <asp:ImageButton ID="imgBtnPrint" ImageUrl="~/App_Themes/Maroon/Images/Print.png"
                runat="server" AlternateText="Print" OnClientClick="setFormat('print')" ToolTip="Print"
                OnClick="imgBtnPrint_Click" />--%>
<%-- <asp:Button ID="btnPrintGrid" runat="server" Text="" OnClick="btnPrintGrid_Click"
                BorderStyle="None" BackColor="TransGroup" ToolTip="Print" />
        </td>
    </tr>
    <tr id="Tr1" runat="server">
        <td>--%>
<%--<asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Width="150px">
                <input type="radio" id="rbtnSin" runat="server" name="Radio" onclick="setPageType('single')" />
                <label for="rbtnSin" class="cmbField">Current Page</label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                <input type="radio" id="Radio1" runat="server" name="Radio" onclick="setPageType('multiple')" />
                <label for="Radio1" class="cmbField">All Pages</label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                <div align="center">
                    <asp:Button ID="btnOk" runat="server" Text="OK" CssClass="PCGButton" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="PCGButton" />
                </div>--%>
<%--   <asp:Panel ID="Panel1" runat="server" CssClass="ExortPanelpopup" Style="display: none">
                <br />
                <table width="100%">
                    <tr>
                        <td>
                            &nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="right">
                            <input id="rbtnSin" runat="server" name="Radio" onclick="setPageType('single')" type="radio" />
                        </td>
                        <td align="left">
                            <label for="rbtnSin" style="color: Black; font-family: Verdana; font-size: 8pt; text-decoration: none">
                                Current Page</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="right">
                            <input id="Radio1" runat="server" name="Radio" onclick="setPageType('multiple')"
                                type="radio" />
                        </td>
                        <td align="left">
                            <label for="Radio1" style="color: Black; font-family: Verdana; font-size: 8pt; text-decoration: none">
                                All Pages</label>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnOk" runat="server" Text="OK" CssClass="PCGButton" />
                        </td>
                        <td align="left">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="PCGButton" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Button class="ExportButton" ID="btnExportExcel" runat="server" Style="display: none"
                OnClick="btnExportExcel_Click" Height="31px" Width="35px" />
        </td>
    </tr>
    <tr id="trMessage" runat="server" visible="false">
        <td>
            <asp:Label ID="lblMessage" runat="server" CssClass="Error" Text="No Records Found..."></asp:Label>
        </td>
    </tr>
</table>--%>
<%--<asp:Button ID="btnPANSearch" runat="server" Text="" OnClick="btnPANSearch_Click"
    BorderStyle="None" BackColor="TransGroup" />
<asp:Button ID="btnPincodeSearch" runat="server" Text="" OnClick="btnPincodeSearch_Click"
    BorderStyle="None" BackColor="TransGroup" />
<asp:Button ID="btnAreaSearch" runat="server" Text="" OnClick="btnAreaSearch_Click"
    BorderStyle="None" BackColor="TransGroup" />
<asp:Button ID="btnNameSearch" runat="server" Text="" OnClick="btnNameSearch_Click"
    BorderStyle="None" BackColor="TransGroup" />
    
<asp:Button ID="hiddenassociation" runat="server" 
    onclick="hiddenassociation_Click" BorderStyle="None" BackColor="TransGroup" />
--%>
<%--<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnPincodeFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hndPAN" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAreaFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRMFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnGroupFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnReassignRM" runat="server" Visible="false" />
<asp:HiddenField ID="hdnDownloadPageType" runat="server" Visible="true" />
<asp:HiddenField ID="hdnDownloadFormat" runat="server" Visible="true" />
<asp:HiddenField ID="hdnactive" runat="server" Visible="false" />
<asp:HiddenField ID="hdnIsProspect" runat="server" Visible="false" />
<asp:HiddenField ID="hdnMsgValue" runat="server" />
<asp:HiddenField ID="hdnassociationcount" runat="server" />--%>