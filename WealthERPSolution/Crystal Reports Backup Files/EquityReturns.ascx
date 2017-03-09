<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EquityReturns.ascx.cs"
    Inherits="WealthERP.BusinessMIS.EquityReturns" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            EQ Returns (Holdings)
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="imgEQReturns" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" Height="20px"
                                Width="25px" Visible="false" OnClick="imgEQReturns_Click"></asp:ImageButton>
                            <asp:ImageButton ID="imgCompany" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" Height="20px"
                                Width="25px" Visible="false" onclick="imgCompany_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%" cellspacing="0" cellpadding="0">
    <tr>
    <td align="left" style="width:25%">
            <asp:Label ID="lblType" runat="server" Text="Select Type:" CssClass="FieldName"></asp:Label>
            <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Customer Wise" Value="CustomerWise"></asp:ListItem>
                <asp:ListItem Text="Company Wise" Value="CompanyWise"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="left" style="width:25%">
            <asp:Label ID="lblDate" runat="server" Text="As on Date:" CssClass="FieldName"></asp:Label>
            <asp:Label ID="lblPickDate" Text="" runat="server" CssClass="FieldName"> </asp:Label>
        </td>
        <td colspan="2">
        </td>
    </tr>
     <tr>
        <td colspan="4" align="left">
            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="PCGButton" 
                onclick="btnGo_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Panel ID="pnlEQReturns" runat="server" ScrollBars="Horizontal" Width="98%" Visible="true">
                <table>
                    <tr>
                        <td>
                            <div runat="server" id="divEQReturns" style="margin: 2px; width: 640px;">
                                <telerik:RadGrid ID="gvEQReturns" runat="server" GridLines="None" AutoGenerateColumns="False"
                                    PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                    AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" OnNeedDataSource="gvEQReturns_OnNeedDataSource"
                                    EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true">
                                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                        FileName="EQReturnsCompanyWise" Excel-Format="ExcelML">
                                    </ExportSettings>
                                    <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                        CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
                                        GroupLoadMode="Client" ShowGroupFooter="true">
                                        <Columns>
                                        <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Scrip/Company" DataField="CompanyName"
                                                UniqueName="CompanyName" SortExpression="CompanyName" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Sector" DataField="Sector"
                                                UniqueName="Sector" SortExpression="Sector" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            
                                             <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Group Head" DataField="Parent"
                                                UniqueName="Parent" SortExpression="Parent" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                           
                                             <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Customer" DataField="Customer"
                                                UniqueName="Customer" SortExpression="Customer" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="PAN" DataField="PAN"
                                                UniqueName="PAN" SortExpression="PAN" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="No of Shares" DataField="NoOfShares"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="NoOfShares" SortExpression="NoOfShares"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="95px" HeaderText="Price" DataField="Price"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="Price" SortExpression="Price"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="95px" HeaderText="Invested Cost" DataField="InvestedCost"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="InvestedCost" SortExpression="InvestedCost"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="95px" HeaderText="Market Value" DataField="CurrentValue"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="CurrentValue" SortExpression="CurrentValue"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="95px" HeaderText="Profit/Loss (Rs)" DataField="ProfitLoss"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="ProfitLoss" SortExpression="ProfitLoss"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                               <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Profit/Loss (%)" DataField="Percentage" HeaderStyle-HorizontalAlign="Right"
                                                UniqueName="Percentage" SortExpression="Percentage" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N2}"  FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                                 <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="RM" DataField="RM"
                                                UniqueName="RM" SortExpression="RM" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            
                                           
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Branch" DataField="Branch"
                                                UniqueName="Branch" SortExpression="Branch" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                       
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="CustomerId" DataField="CustomerId"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="CustomerId" SortExpression="CustomerId"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                           
                                         
                                           <%-- <telerik:GridTemplateColumn HeaderText="Profit/Loss (%)" AllowFiltering="False" UniqueName="Percentage"
                                                SortExpression="Percentage" ShowFilterIcon="false" FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTptalPL" runat="server" Text='<%#Eval("Percentage") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalPercentage" runat="server" Font-Bold="true" Text="">
                                                    </asp:Label>
                                                </FooterTemplate>
                                            </telerik:GridTemplateColumn>--%>
                                        </Columns>
                                    </MasterTableView>
                                    <HeaderStyle Width="150px" />
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                        <Resizing AllowColumnResize="true" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    
    <tr>
        <td colspan="4">
            <asp:Panel ID="pnlEqHoldings" runat="server" ScrollBars="Horizontal" Width="98%" Visible="true">
                <table>
                    <tr>
                        <td>
                            <div runat="server" id="divEqHoldings" style="margin: 2px; width: 640px;">
                                <telerik:RadGrid ID="gvEqHldings" runat="server" GridLines="None" AutoGenerateColumns="False"
                                    PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                    AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" OnNeedDataSource="gvEqHldings_OnNeedDataSource"
                                    EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true">
                                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                        FileName="EQReturnsCustomerWise" Excel-Format="ExcelML">
                                    </ExportSettings>
                                    <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                        CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
                                        GroupLoadMode="Client" ShowGroupFooter="true">
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="CustomerId" DataField="CustomerId"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="CustomerId" SortExpression="CustomerId"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Customer" DataField="Customer"
                                                UniqueName="Customer" SortExpression="Customer" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="PAN" DataField="PAN"
                                                UniqueName="PAN" SortExpression="PAN" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Group Head" DataField="Parent"
                                                UniqueName="Parent" SortExpression="Parent" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Branch" DataField="Branch"
                                                UniqueName="Branch" SortExpression="Branch" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="RM" DataField="RM"
                                                UniqueName="RM" SortExpression="RM" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="No of Shares" DataField="NoOfShares"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="NoOfShares" SortExpression="NoOfShares"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="95px" HeaderText="Price" DataField="Price"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="Price" SortExpression="Price"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="95px" HeaderText="Invested Cost" DataField="InvestedCost"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="InvestedCost" SortExpression="InvestedCost"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="95px" HeaderText="Market Value" DataField="CurrentValue"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="CurrentValue" SortExpression="CurrentValue"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="95px" HeaderText="Profit/Loss" DataField="ProfitLoss"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="ProfitLoss" SortExpression="ProfitLoss"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Profit/Loss (%)" DataField="Percentage" HeaderStyle-HorizontalAlign="Right"
                                                UniqueName="Percentage" SortExpression="Percentage" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N2}"  FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <HeaderStyle Width="150px" />
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                        <Resizing AllowColumnResize="true" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td>
            <asp:Label ID="LabelMainNote" runat="server" Font-Size="Small" CssClass="cmbFielde"
                Text="Note:1.You can group/ungroup or hide/unhide fields by a right click on the grid label and then making the selection."></asp:Label>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnbranchId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnbranchHeadId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAll" runat="server" Visible="false" />
<asp:HiddenField ID="hdnrmId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnadviserId" runat="server" />
