<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFDashBoard.ascx.cs"
    Inherits="WealthERP.BusinessMIS.MFDashBoard" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>
<table width="100%">
    <tr>
        <td colspan="2">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            MF DashBoard
                        </td>
                        <td align="right" id="tdGoalExport" runat="server" style="padding-bottom: 2px;">
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
<%--    <tr>
        <td colspan="2">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Customer Added
            </div>
        </td>
    </tr>--%>
    <tr>
        <td colspan="2">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="tbl" runat="server" Visible="true">
                            <table>
                                <tr>
                                    <td>
                                        <div id="dvMSDashBoardCount" runat="server" style="width: 640px;">
                                            <telerik:RadGrid ID="gvMFDashboardCount" runat="server" GridLines="None" AutoGenerateColumns="False"
                                                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                                Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="false" AllowAutomaticInserts="false"
                                                ExportSettings-FileName="Count">
                                                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                                    FileName="Goal MIS" Excel-Format="ExcelML">
                                                </ExportSettings>
                                                <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                                    CommandItemDisplay="None">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="Type" HeaderText="Customer Added" UniqueName="Type"
                                                            SortExpression="Type">
                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="PreviousCount" HeaderText="Oct" AllowFiltering="false"
                                                           HeaderStyle-HorizontalAlign="Right" UniqueName="PreviousCount" DataFormatString="{0:N0}">
                                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CurrentCount" HeaderText="Nov" AllowFiltering="false"
                                                          HeaderStyle-HorizontalAlign="Right"  UniqueName="CurrentCount" DataFormatString="{0:N0}">
                                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Yearly" HeaderText="YTD" DataFormatString="{0:N0}"
                                                          HeaderStyle-HorizontalAlign="Right"  AllowFiltering="false" UniqueName="Yearly" FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Total" HeaderText="Total" DataFormatString="{0:N0}"
                                                          HeaderStyle-HorizontalAlign="Right"  AllowFiltering="false" UniqueName="Total" FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings>
                                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                                </ClientSettings>
                                            </telerik:RadGrid></div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    
    <tr>
        <td colspan="2">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="pnlAUM" runat="server" Visible="true">
                            <table>
                                <tr>
                                    <td>
                                        <div id="divAUM" runat="server" style="width: 640px;">
                                            <telerik:RadGrid ID="gvAUM" runat="server" GridLines="None" AutoGenerateColumns="False"
                                                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                                Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="false" AllowAutomaticInserts="false"
                                                ExportSettings-FileName="Count">
                                                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                                    FileName="AUM" Excel-Format="ExcelML">
                                                </ExportSettings>
                                                <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                                    CommandItemDisplay="None">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="Type" HeaderText="AUM" UniqueName="Type"
                                                            SortExpression="Type" >
                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CostPrevious" HeaderText="Oct" AllowFiltering="false" HeaderStyle-HorizontalAlign="Right"
                                                            UniqueName="CostPrevious" DataFormatString="{0:N0}">
                                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CostCurrent" HeaderText="Nov" AllowFiltering="false"
                                                            HeaderStyle-HorizontalAlign="Right" UniqueName="CostCurrent" DataFormatString="{0:N0}">
                                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CostYear" HeaderText="YTD" DataFormatString="{0:N0}"
                                                           HeaderStyle-HorizontalAlign="Right"  AllowFiltering="false" UniqueName="CostYear" FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CostTotal" HeaderText="Total" DataFormatString="{0:N0}"
                                                            HeaderStyle-HorizontalAlign="Right" AllowFiltering="false" UniqueName="CostTotal" FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings>
                                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                                </ClientSettings>
                                            </telerik:RadGrid></div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    
<%--    <tr>
        <td colspan="2">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Business Generated
            </div>
        </td>
    </tr>--%>
    <tr>
        <td colspan="2">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="pnlAmount" runat="server" Visible="true">
                            <table>
                                <tr>
                                    <td>
                                        <div id="dvMFDashboardAmount" runat="server" style="width: 640px;">
                                            <telerik:RadGrid ID="gvMFDashboardAmount" runat="server" GridLines="None" AutoGenerateColumns="False"
                                                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                                Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="false" AllowAutomaticInserts="false"
                                                ExportSettings-FileName="Count">
                                                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                                    FileName="Goal MIS" Excel-Format="ExcelML">
                                                </ExportSettings>
                                                <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                                    CommandItemDisplay="None">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="Type" HeaderText="Business Generated" UniqueName="Type"
                                                            SortExpression="Type" >
                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CostPrevious" HeaderText="Oct" AllowFiltering="false" HeaderStyle-HorizontalAlign="Right"
                                                            UniqueName="CostPrevious" DataFormatString="{0:N0}">
                                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CostCurrent" HeaderText="Nov" AllowFiltering="false"
                                                            HeaderStyle-HorizontalAlign="Right" UniqueName="CostCurrent" DataFormatString="{0:N0}">
                                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CostYear" HeaderText="YTD" DataFormatString="{0:N0}"
                                                           HeaderStyle-HorizontalAlign="Right"  AllowFiltering="false" UniqueName="CostYear" FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CostTotal" HeaderText="Total" DataFormatString="{0:N0}"
                                                            HeaderStyle-HorizontalAlign="Right" AllowFiltering="false" UniqueName="CostTotal" FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings>
                                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                                </ClientSettings>
                                            </telerik:RadGrid></div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
        </td>
    </tr>
    <tr>
    <td colspan="2"></td>
       <%-- <td style="width: 50%">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Top 5 Scheme by AUM
            </div>
        </td>
        <td style="width: 50%">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Top 5 Branch by AUM
            </div>
        </td>--%>
    </tr>
    <tr>
        <td style="width: 50%" valign="top">
        <asp:Label ID="lblAvailableFolio" runat="server" CssClass="HeaderTextSmall" Text="Top 5 Scheme by AUM"></asp:Label>
        <br />
            <div id="dvScheme" runat="server">
                <%--<asp:Chart ID="chrtScheme" runat="server" BackColor="Transparent" Palette="Pastel"
                    Width="500px" Height="250px">
                    <Series>
                        <asp:Series Name="seriesMFC">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="careaMFC">
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>--%>
                <telerik:RadGrid ID="gvScheme" runat="server" GridLines="None" AutoGenerateColumns="False"
                                Width="100%" PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True"
                                ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="false"
                                AllowAutomaticInserts="false" ExportSettings-FileName="Count">
                                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                    FileName="Goal MIS" Excel-Format="ExcelML">
                                </ExportSettings>
                                <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                    CommandItemDisplay="None">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Scheme" HeaderText="Scheme" UniqueName="Scheme"
                                            SortExpression="Scheme">
                                            <ItemStyle Width="60%" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AUM" HeaderText="AUM" AllowFiltering="false" HeaderStyle-HorizontalAlign="Center"
                                            UniqueName="AUM" DataFormatString="{0:N0}">
                                            <ItemStyle Width="40%" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
            </div>
            <br />
            <asp:LinkButton  ID="lnkSchemeNavi" Text=" >>More" runat="server"
                            CssClass="LinkButtons" onclick="lnkSchemeNavi_Click"></asp:LinkButton>
        </td>
        <td style="width: 50%" valign="top">
        <asp:Label ID="Label1" runat="server" CssClass="HeaderTextSmall" Text="Top 5 Branch by AUM"></asp:Label>
        <br />
            <div id="divBranch" runat="server">
            <telerik:RadGrid ID="gvBranch" runat="server" GridLines="None" AutoGenerateColumns="False"
                                Width="100%" PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True"
                                ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="false"
                                AllowAutomaticInserts="false" ExportSettings-FileName="Count">
                                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                    FileName="Goal MIS" Excel-Format="ExcelML">
                                </ExportSettings>
                                <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                    CommandItemDisplay="None">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="BranchName" HeaderText="Branch" UniqueName="BranchName"
                                            SortExpression="BranchName">
                                            <ItemStyle Width="60%" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AUM" HeaderText="AUM" AllowFiltering="false" HeaderStyle-HorizontalAlign="Center"
                                            UniqueName="AUM" DataFormatString="{0:N0}">
                                            <ItemStyle Width="40%" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                <%--<asp:Chart ID="chrtBranch" runat="server" BackColor="Transparent" Palette="Pastel"
                    Width="500px" Height="250px">
                    <Series>
                        <asp:Series Name="Series1">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>--%>
                <br />
                <asp:LinkButton  ID="lnkBranchNavi" Text=" >>More" runat="server"
                            CssClass="LinkButtons" onclick="lnkBranchNavi_Click"></asp:LinkButton>
            </div>
        </td>
    </tr>
    <tr>
    <td colspan="2"></td>
    </tr>
    <tr>
        <%--<td style="width: 50%">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Top 5 Customer by AUM
            </div>
        </td>
        <td style="width: 50%">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Sub Category wise AUM
            </div>
        </td>--%>
    </tr>
    <tr>
        <td style="width: 50%" valign="top">
        <asp:Label ID="Label2" runat="server" CssClass="HeaderTextSmall" Text="Top 5 Customer by AUM"></asp:Label>
        <br />
            <div id="dvCustomer" runat="server">
                <%--<asp:Chart ID="chrtCustomer" runat="server" BackColor="Transparent" Palette="Pastel"
                    Width="500px" Height="250px">
                    <Series>
                        <asp:Series Name="Series1">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>--%>
                <telerik:RadGrid ID="gvCustomer" runat="server" GridLines="None" AutoGenerateColumns="False"
                                Width="100%" PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True"
                                ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="false"
                                AllowAutomaticInserts="false" ExportSettings-FileName="Count">
                                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                    FileName="Goal MIS" Excel-Format="ExcelML">
                                </ExportSettings>
                                <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                    CommandItemDisplay="None">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="CustomerName" HeaderText="Customer" UniqueName="CustomerName"
                                            SortExpression="CustomerName">
                                            <ItemStyle Width="60%" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AUM" HeaderText="AUM" AllowFiltering="false" HeaderStyle-HorizontalAlign="Center"
                                            UniqueName="AUM" DataFormatString="{0:N0}">
                                            <ItemStyle Width="40%" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                <br />
                <asp:LinkButton  ID="lnkFolioNavi" Text=" >>More" runat="server"
                            CssClass="LinkButtons" onclick="lnkFolioNavi_Click"></asp:LinkButton>
            </div>
        </td>
        <td style="width: 50%" valign="top">
        <asp:Label ID="Label3" runat="server" CssClass="HeaderTextSmall" Text="Subcategory wise AUM"></asp:Label>
        <br />
                             <div>   
                            <telerik:RadGrid ID="gvSubcategory" runat="server" GridLines="None" AutoGenerateColumns="False"
                                Width="100%" PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True"
                                ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="false"
                                AllowAutomaticInserts="false" ExportSettings-FileName="Count">
                                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                    FileName="Goal MIS" Excel-Format="ExcelML">
                                </ExportSettings>
                                <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                    CommandItemDisplay="None">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="SubCategory" HeaderText="SubCategory" UniqueName="SubCategory"
                                            SortExpression="SubCategory">
                                            <ItemStyle Width="60%" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AUM" HeaderText="AUM" AllowFiltering="false" HeaderStyle-HorizontalAlign="Center"
                                            UniqueName="AUM" DataFormatString="{0:N0}">
                                            <ItemStyle Width="40%" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                </ClientSettings>
                            </telerik:RadGrid><br />
                   </div> 
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