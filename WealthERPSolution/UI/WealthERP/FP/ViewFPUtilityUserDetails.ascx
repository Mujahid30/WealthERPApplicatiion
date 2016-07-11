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
<table width="100%">
    <tr>
        <td>
            <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">

                <script type="text/javascript">
                    function ShowEditForm(id, rowIndex) {
                        var grid = $find("<%= gvLeadList.ClientID %>");

                        window.radopen("UserListDialog1");
                       

                        return false;
                    }


                    function RowDblClick(sender, eventArgs) {
                        window.radopen("../InvestorOnline.aspx?EmployeeID=" + eventArgs.getDataKeyValue("CTNS_Id"), "UserListDialog");
                    }





                    var crnt = 0;
                    function PreventClicks() {

                        if (typeof (Page_ClientValidate('Button1')) == 'function') {
                            Page_ClientValidate();
                        }

                        if (Page_IsValid) {
                            if (++crnt > 1) {
                                return false;
                            }
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                </script>

            </telerik:RadCodeBlock>
            <%--<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="rgNotification" LoadingPanelID="gridLoadingPanel">
                            </telerik:AjaxUpdatedControl>
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                    <telerik:AjaxSetting AjaxControlID="gvLeadList">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="gvLeadList" LoadingPanelID="gridLoadingPanel">
                            </telerik:AjaxUpdatedControl>
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>--%>
            <telerik:RadAjaxLoadingPanel runat="server" ID="gridLoadingPanel">
            </telerik:RadAjaxLoadingPanel>
            <telerik:RadGrid ID="gvLeadList" runat="server" AllowAutomaticDeletes="false" PageSize="20"
                EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                ShowStatusBar="false" ShowFooter="false" AllowPaging="true" AllowSorting="true"
                OnNeedDataSource="gvLeadList_OnNeedDataSource" GridLines="none" AllowAutomaticInserts="false" OnItemCreated="gvLeadList_ItemCreated"
                Skin="Telerik" EnableHeaderContextMenu="true" Width="95%" Visible="false">
                <MasterTableView Width="100%" DataKeyNames="FPUUD_UserId,C_CustomerId" AllowMultiColumnSorting="True"
                    AutoGenerateColumns="false" CommandItemDisplay="None" GroupsDefaultExpanded="false"
                    ExpandCollapseColumn-Groupable="true" GroupLoadMode="Client" ShowGroupFooter="true">
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="Customer" DataField="FPUUD_Name" UniqueName="FPUUD_Name"
                            SortExpression="FPUUD_Name" AutoPostBackOnFilter="true" AllowFiltering="false"
                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="PAN" DataField="FPUUD_PAN" UniqueName="FPUUD_PAN"
                            SortExpression="FPUUD_PAN" AutoPostBackOnFilter="true" AllowFiltering="true"
                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FPUUD_EMail" SortExpression="FPUUD_EMail" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                            HeaderText="Email" UniqueName="FPUUD_EMail">
                            <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FPUUD_MobileNo" SortExpression="FPUUD_MobileNo"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AllowFiltering="true" HeaderText="Mobile" UniqueName="FPUUD_MobileNo">
                            <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FPUUD_DOB" SortExpression="FPUUD_DOB"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AllowFiltering="true" HeaderText="Date of birth" UniqueName="FPUUD_DOB">
                            <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="FPUUD_CreatedOn" SortExpression="FPUUD_CreatedOn"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AllowFiltering="true" HeaderText="Lead Date" UniqueName="FPUUD_CreatedOn">
                            <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FPUUD_ModifiedOn" SortExpression="FPUUD_ModifiedOn"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AllowFiltering="true" HeaderText="Last Login" UniqueName="FPUUD_ModifiedOn">
                            <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="stage" SortExpression="stage" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                            HeaderText="Stage" UniqueName="stage">
                            <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IsCustomerAvailable" SortExpression="IsCustomerAvailable"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AllowFiltering="true" HeaderText="Customer Status" UniqueName="IsCustomerAvailable">
                            <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                     
                        <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="RiskClass" HeaderText="Risk Class" Visible="true">
                        <ItemTemplate>
                           <asp:LinkButton ID="lnkViewProspect1" 
                                    runat="server" Text='<%# Eval("RiskClass") %>' Visible="true"></asp:LinkButton>
                        </ItemTemplate>
                        
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="ActionForProspect"
                            HeaderText="View Profile" Visible="true">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkViewProspect" OnClick="ddlActionForProspect_OnSelectedIndexChanged"
                                    runat="server" Text="View Profile" Visible='<%# Eval("FPUUD_IsProspectmarked") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                </ClientSettings>
            </telerik:RadGrid>
            <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server"
                EnableShadow="true">
                <Windows>
                    <telerik:RadWindow RenderMode="Lightweight" ID="UserListDialog1" runat="server" Title="Editing record" 
                        Height="600px" Width="950px" OnClientShow="setCustomPosition" Left="30" Top="25"
                        ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true">
                    </telerik:RadWindow>
                </Windows>
            </telerik:RadWindowManager>

            <script type="text/javascript">
                function setCustomPosition(sender, args) {
                    sender.moveTo(sender.get_left(), sender.get_top());
                }
            </script>

        </td>
    </tr>
</table>
