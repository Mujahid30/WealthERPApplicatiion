<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCDIssueList.ascx.cs" Inherits="WealthERP.OnlineOrderManagement.NCDIssueList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1" />

<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<style type="text/css">
    .table
    {
        border: 1px solid orange;
    }
    .leftLabel
    {
        width: 15%;
        text-align: right;
    }
    .rightData
    {
        width: 18%;
        text-align: left;
    }
    .rightDataTwoColumn
    {
        width: 25%;
        text-align: left;
    }
    .rightDataFourColumn
    {
        width: 50%;
        text-align: left;
    }
    .rightDataThreeColumn
    {
        width: 41%;
        text-align: left;
    }
    .tdSectionHeading
    {
        padding-bottom: 6px;
        padding-top: 6px;
        width: 100%;
    }
    .divSectionHeading table td span
    {
        padding-bottom: 5px !important;
    }
    .fltlft
    {
        float: left;
        padding-left: 3px;
        width: 20%;
    }
    .divCollapseImage
    {
        float: left;
        padding-left: 5px;
        width: 2%;
        float: right;
        text-align: right;
        cursor: pointer;
        cursor: hand;
    }
    .imgCollapse
    {
        background: Url(../Images/Section-Expand.png);
        cursor: pointer;
        cursor: hand;
    }
    .imgExpand
    {
        background: Url(../Images/Section-Collapse.png) no-repeat left top;
        cursor: pointer;
        cursor: hand;
    }
    .fltlftStep
    {
        float: left;
    }
    .StepOneContentTable, .StepTwoContentTable, .StageRequestTable, .StepThreeContentTable, .StepFourContentTable
    {
        width: 100%;
    }
    .SectionBody
    {
        width: 100%;
    }
    .collapse
    {
        text-align: right;
    }
    .divStepStatus
    {
        float: left;
        padding-left: 2px;
        padding-right: 5px;
    }
</style>

<%--<asp:UpdatePanel ID="upCMGrid" runat="server">
    <ContentTemplate>--%>
        <table width="100%">
            <tr>
                <td>
                    <div class="divPageHeading">
                        <table width="100%">
                            <tr>
                                <td align="left">
                                    BONDS 
                                </td>
                                <td align=right >
                                    <asp:ImageButton ID="ibtExportSummary" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                        runat="server" AlternateText="Excel" ToolTip="Export To Excel"  
                                        Height="25px" Width="25px"></asp:ImageButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>Sell / Buy Bond from Equity</td>
                <td>
                    <asp:DropDownList ID="ddlListOfBonds" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                
                </td>
                <td>
                    <asp:Button ID="btnEquityBond" runat="server" Text="Purchase Equity Bonds" 
                        onclick="btnEquityBond_Click" />
                </td>
                
            </tr>
        
        </table>
        
        
        <table id="tblCommissionStructureRule" runat="server" width="100%">
            <tr>
                <td>
                    <asp:Panel ID="pnlGrid" runat="server" CssClass="Landscape" Width="100%" ScrollBars="Horizontal">
                        <table width="100%">
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="gvCommMgmt" AllowSorting="True" runat="server"
                                        EnableLoadOnDemand="True" AllowPaging="True" AutoGenerateColumns="False" 
                                        EnableEmbeddedSkins="False" GridLines="None" ShowFooter="True" 
                                        PagerStyle-AlwaysVisible="true" ShowStatusBar="True" Skin="Telerik" AllowFilteringByColumn="false">
                                        
                                        <HeaderContextMenu EnableEmbeddedSkins="False"></HeaderContextMenu>

                                        <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="LiveBondList"></ExportSettings>
                                        <PagerStyle AlwaysVisible="True" />
                                        <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames="PFIIM_IssuerId" 
                                            AutoGenerateColumns="false" Width="100%">
                                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                            <Columns>
                                                                                                                                                                   

                                               <telerik:GridTemplateColumn AllowFiltering="false" DataField="" 
                                                    HeaderStyle-Width="100px" UniqueName="Details" >
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbDetails" runat="server"  Text="Details"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="100px" />
                                                    </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="AIM_SchemeName" HeaderStyle-Width="100px"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="Scrip" UniqueName="ScripName" SortExpression="Scrip">
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" 
                                                        Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PFIIM_IssuerId" HeaderStyle-Width="200px"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="Scrip ID" UniqueName="OnlIssuerId" SortExpression="IssuerId">
                                                    <HeaderStyle Width="200px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="200px" Wrap="true" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AIM_NatureOfBond" HeaderStyle-Width="100px" 
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="Nature Of Bond" UniqueName="AIM_NatureOfBond" SortExpression="NatureOfBond">
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AIM_FaceValue" HeaderStyle-Width="150px" HeaderText="Face Value"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    UniqueName="FaceValue" Visible="true">
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="150px" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PFISD_Rating" HeaderStyle-Width="150px" HeaderText="Rating"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    UniqueName="Rating" Visible="true">
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="150px" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PFISD_MinApplication" HeaderStyle-Width="150px" HeaderText="Minimum Application Amount"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    UniqueName="MinimumAmount" Visible="true">
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="150px" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PFISD_BidQty" HeaderStyle-Width="150px" HeaderText="Min Bid Quantity"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    UniqueName="BidQty" Visible="true">
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="150px" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PFISD_InMultiplesOf" HeaderStyle-Width="150px" HeaderText="Multiples allowed"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    UniqueName="Multiplesallowed" Visible="true">
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="150px" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridDateTimeColumn DataField="AIM_OpenDate" 
                                                    DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px"
                                                    CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="Issue Open Date" SortExpression="OpenDate" UniqueName="OpenDate">
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" Wrap="false" />
                                                </telerik:GridDateTimeColumn>
                                                <telerik:GridDateTimeColumn DataField="AIM_CloseDate" 
                                                    DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px" 
                                                    CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="Issue Close Date" UniqueName="CloseDate"  SortExpression="CloseDate">
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" Wrap="false" />
                                                </telerik:GridDateTimeColumn>
                                                <telerik:GridBoundColumn DataField="IsDematFacilityAvail" HeaderStyle-Width="150px" HeaderText="Is Demat Facility Avail"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    UniqueName="IsDematFacilityAvail" Visible="true">
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="150px" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                 <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px" UniqueName="Action" HeaderText="Action" >
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="llPurchase" runat="server" OnClick="llPurchase_Click"  Text="Purchase"></asp:LinkButton>
                                                    </ItemTemplate>
                                                     <HeaderStyle Width="100px" />
                                                    </telerik:GridTemplateColumn>
                                            </Columns>
                                            <editformsettings>
                                                <editcolumn cancelimageurl="Cancel.gif" editimageurl="Edit.gif" 
                                                    insertimageurl="Update.gif" updateimageurl="Update.gif">
                                                </editcolumn>
                                            </editformsettings>
                                            <PagerStyle AlwaysVisible="True" />
                                        </MasterTableView>                                        
                                        <ClientSettings>
                                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                            <Resizing AllowColumnResize="true" />                                          
                                        </ClientSettings>
                                        <FilterMenu EnableEmbeddedSkins="False"></FilterMenu>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        
<%--    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="ibtExportSummary" />
    </Triggers>
</asp:UpdatePanel>--%>