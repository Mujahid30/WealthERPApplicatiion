<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFDashBoard.ascx.cs" Inherits="WealthERP.BusinessMIS.MFDashBoard" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>
<table width="100%">
<tr>
<td colspan="3" style="width: 100%;">
<div class="divPageHeading">
    <table cellspacing="0"  width="100%">
        <tr>
        <td align="left">MF DashBoard</td>
        <td  align="right" id="tdGoalExport" runat="server" style="padding-bottom:2px;">
    </td>
        </tr>
    </table>
</div>
</td>
</tr>
</table>
<table>
<tr>
<td colspan="2">
<div class="divSectionHeading" style="vertical-align: text-bottom">
                MfDashBoard Count List
            </div>
</td>
</tr>
<tr>
<td colspan="2">
<table width="100%">
<tr>
    <td colspan="3">
    <asp:Panel ID="tbl" runat="server"    Visible="true">
    <table>
     <tr>
       <td>
       <div id="dvMSDashBoardCount" runat="server" style="width: 640px;">
                <telerik:RadGrid ID="gvMFDashboardCount" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false"  AllowFilteringByColumn="false"
                    AllowAutomaticInserts="false" ExportSettings-FileName="Count" >
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true" FileName="Goal MIS" Excel-Format="ExcelML">
                    </ExportSettings>
                    <MasterTableView 
                        Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                        <telerik:GridBoundColumn DataField="Type"  HeaderText="Type" UniqueName="Type"
                                SortExpression="Type" >
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="PreviousCount" HeaderText="Previous Month"
                                AllowFiltering="false" UniqueName="PreviousCount" DataFormatString="{0:N0}">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CurrentCount" HeaderText="Current Month"
                                AllowFiltering="false" UniqueName="CurrentCount" DataFormatString="{0:N0}">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Yearly" HeaderText="Yearly" DataFormatString="{0:N0}"
                                AllowFiltering="false" UniqueName="Yearly"   FooterStyle-HorizontalAlign="Right">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="Total" HeaderText="Total" DataFormatString="{0:N0}"
                                AllowFiltering="false" UniqueName="Total"   FooterStyle-HorizontalAlign="Right">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                       </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid></div>
               </td></tr></table>
            </asp:Panel>
    </td>
    </tr>
</table>
</td>
</tr>
<tr>
<td colspan="2">
<div class="divSectionHeading" style="vertical-align: text-bottom">
                MfDashBoard Amount List
            </div>
</td>
</tr>
<tr>
<td colspan="2">
<table width="100%">
<tr>
    <td colspan="3">
    <asp:Panel ID="pnlAmount" runat="server" Visible="true">
    <table>
     <tr>
       <td>
       <div id="dvMFDashboardAmount" runat="server" style="width: 640px;">
                <telerik:RadGrid ID="gvMFDashboardAmount" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false"  AllowFilteringByColumn="false"
                    AllowAutomaticInserts="false" ExportSettings-FileName="Count" >
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true" FileName="Goal MIS" Excel-Format="ExcelML">
                    </ExportSettings>
                    <MasterTableView 
                        Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                        <telerik:GridBoundColumn DataField="Type"  HeaderText="Type" UniqueName="Type"
                                SortExpression="Type" DataFormatString="{0:N0}">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="CostPrevious" HeaderText="Previous Month"
                                AllowFiltering="false" UniqueName="CostPrevious" DataFormatString="{0:N0}">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CostCurrent" HeaderText="Current Month"
                                AllowFiltering="false" UniqueName="CostCurrent" DataFormatString="{0:N0}">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CostYear" HeaderText="Yearly" DataFormatString="{0:N0}"
                                AllowFiltering="false" UniqueName="CostYear"   FooterStyle-HorizontalAlign="Right">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="CostTotal" HeaderText="Total" DataFormatString="{0:N0}"
                                AllowFiltering="false" UniqueName="CostTotal"   FooterStyle-HorizontalAlign="Right">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                       </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid></div>
               </td></tr></table>
            </asp:Panel>
    </td>
    </tr>
</table>
</td>
</tr>
<tr>
<td colspan="2"></td>
</tr>
<tr>
<td style="width:50%">
<div class="divSectionHeading" style="vertical-align: text-bottom">
                Top 5 Scheme
            </div>
</td>
<td style="width:50%">
<div class="divSectionHeading" style="vertical-align: text-bottom">
                Top 5 Branch
            </div>
</td>
</tr>
<tr>
<td style="width:50%">
<div id="dvScheme" runat="server">
                               <asp:Chart ID="chrtScheme" runat="server" BackColor="Transparent" Palette="Pastel"
                                                Width="500px" Height="250px">
                                                <Series>
                                                    <asp:Series Name="seriesMFC">
                                                    </asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Name="careaMFC">
                                                    </asp:ChartArea>
                                                </ChartAreas>
                                            </asp:Chart>
                                        </div>
</td>
<td style="width:50%">
<div id="divBranch" runat="server">
                          <asp:Chart ID="chrtBranch" runat="server" BackColor="Transparent" Palette="Pastel"
                                                Width="500px" Height="250px">
                                                <Series>
                                                    <asp:Series Name="Series1">
                                                    </asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Name="ChartArea1">
                                                    </asp:ChartArea>
                                                </ChartAreas>
                                            </asp:Chart>
                                        </div>
</td>
</tr>
<tr>
<tr>
<td style="width:50%">
<div class="divSectionHeading" style="vertical-align: text-bottom">
                Top 5 Customer
            </div>
</td>
<td style="width:50%">
<div class="divSectionHeading" style="vertical-align: text-bottom">
                Subcategory Wise AUM
            </div>
</td>
</tr>
</tr>
<tr>
<td style="width:50%">
<div id="dvCustomer" runat="server">
                          <asp:Chart ID="chrtCustomer" runat="server" BackColor="Transparent" Palette="Pastel"
                                                Width="500px" Height="250px">
                                                <Series>
                                                    <asp:Series Name="Series1">
                                                    </asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Name="ChartArea1">
                                                    </asp:ChartArea>
                                                </ChartAreas>
                             </asp:Chart>
 </div>
</td>
<td style="width:50%">

    <table>
     <tr>
       <td>
       <div id="Div1" runat="server" >
                <telerik:RadGrid ID="gvSubcategory" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false"  AllowFilteringByColumn="false"
                    AllowAutomaticInserts="false" ExportSettings-FileName="Count" >
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true" FileName="Goal MIS" Excel-Format="ExcelML">
                    </ExportSettings>
                    <MasterTableView 
                        Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                        <telerik:GridBoundColumn DataField="SubCategory"  HeaderText="SubCategory" UniqueName="SubCategory"
                                SortExpression="SubCategory" >
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="AUM" HeaderText="AUM"
                                AllowFiltering="false" UniqueName="AUM">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                       </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid></div>
               </td></tr></table>
</td>
</tr>
</table>
<%--<table id="ErrorMessage" align="center" runat="server">
    <tr>
        <td>
            <div class="failure-msg" align="center">
                No Records found.....
            </div>
        </td>
    </tr>
</table>--%>
