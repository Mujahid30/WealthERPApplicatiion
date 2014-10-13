<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFReturns.ascx.cs" Inherits="WealthERP.BusinessMIS.MFReturns" %>
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
                            MF Returns (Holdings)
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="imgMFReturns" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" Height="20px"
                                Width="25px" Visible="false" OnClick="imgMFReturns_Click"></asp:ImageButton>
                            <asp:ImageButton ID="imgScheme" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" Height="20px"
                                Width="25px" Visible="false" OnClick="imgScheme_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="80%">
    <tr>
        <td align="right">
            <asp:Label ID="lnlType" runat="server" Text="Select Option:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Customer Wise" Value="CustomerWise"></asp:ListItem>
                <asp:ListItem Text="Scheme Wise" Value="SchemeWise"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right">
            <asp:Label ID="lblType" runat="server" Text="Select Type:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlTypes" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Online" Value="1"></asp:ListItem>
                <asp:ListItem Text="Offline" Value="2"></asp:ListItem>
                <asp:ListItem Text="All" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right">
            <asp:Label ID="lblDate" runat="server" Text="As on Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblPickDate" Text="" runat="server" CssClass="FieldName"> </asp:Label>
        </td>
        <td>
            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="PCGButton" OnClick="btnGo_Click" />
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td colspan="4">
            <asp:Panel ID="pnlMfReturns" runat="server" ScrollBars="Horizontal" Width="98%" Visible="true">
                <table>
                    <tr>
                        <td>
                            <div runat="server" id="divMfReturns" style="margin: 2px; width: 640px;">
                                <telerik:RadGrid ID="gvMfReturns" runat="server" GridLines="None" AutoGenerateColumns="False"
                                    PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                    AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" OnNeedDataSource="gvMfReturns_OnNeedDataSource"
                                    EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true">
                                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                        FileName="MFReturns" Excel-Format="ExcelML">
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
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Invested Cost" DataField="InvestedCost"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="InvestedCost" SortExpression="InvestedCost"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Current Value" DataField="CurrentValue"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="CurrentValue" SortExpression="CurrentValue"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Profit/Loss" DataField="ProfitLoss"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="ProfitLoss" SortExpression="ProfitLoss"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Profit/Loss (%)" DataField="Percentage"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="Percentage" SortExpression="Percentage"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N2}" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <%--<telerik:GridTemplateColumn HeaderText="Profit/Loss (%)" AllowFiltering="False" UniqueName="Percentage"
                                                SortExpression="Percentage" ShowFilterIcon="false" FooterStyle-HorizontalAlign="Right" >
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
        <td colspan="4">
            <asp:Panel ID="pnlScheme" runat="server" ScrollBars="Horizontal" Width="98%" Visible="true">
                <table>
                    <tr>
                        <td>
                            <div runat="server" id="divScheme" style="margin: 2px; width: 640px;">
                                <telerik:RadGrid ID="gvMfReturnsScheme" runat="server" GridLines="None" AutoGenerateColumns="False"
                                    PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                    OnItemCommand="gvMfReturns_ItemCommand" AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true"
                                    OnNeedDataSource="gvMfReturnsScheme_OnNeedDataSource" EnableHeaderContextMenu="true"
                                    EnableHeaderContextFilterMenu="true">
                                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                        FileName="MFReturnsSchemeWise" Excel-Format="ExcelML">
                                    </ExportSettings>
                                    <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                        CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
                                        GroupLoadMode="Client" ShowGroupFooter="true" DataKeyNames="accountno,schemecode">
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
                                            <%--<telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="PAN" DataField="PAN"
                                                UniqueName="PAN" SortExpression="PAN" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>--%>
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
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Scheme" DataField="Scheme"
                                                UniqueName="Scheme" SortExpression="Scheme" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Folio No" DataField="Folio"
                                                UniqueName="Folio" SortExpression="Folio" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Invested Cost" DataField="InvestedCost"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="InvestedCost" SortExpression="InvestedCost"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <%-- <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Current Value" DataField="CurrentValue"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="CurrentValue" SortExpression="CurrentValue"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>--%>
                                            <telerik:GridTemplateColumn HeaderStyle-Width="80px" UniqueName="CurrentValue" HeaderText="Current Value"
                                                Groupable="False" ItemStyle-Wrap="false" AllowFiltering="true" SortExpression="CurrentValue"
                                                ItemStyle-HorizontalAlign="Right" DataField="CurrentValue" FooterStyle-HorizontalAlign="Right"
                                                FooterText="Sum" Aggregate="Sum">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkMF" runat="server" Text='<%# String.Format("{0:N0}", DataBinder.Eval(Container.DataItem, "CurrentValue")) %>'
                                                        CommandName="Redirect"></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Profit/Loss" DataField="ProfitLoss"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="ProfitLoss" SortExpression="ProfitLoss"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Profit/Loss (%)" DataField="Percentage"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="Percentage" SortExpression="Percentage"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N2}" FooterStyle-HorizontalAlign="Right">
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
                Text="Note: 1.You can group/ungroup or hide/unhide fields by a right click on the grid label and then making the selection."></asp:Label>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnbranchId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnbranchHeadId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAll" runat="server" Visible="false" />
<asp:HiddenField ID="hdnrmId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnadviserId" runat="server" />
<asp:HiddenField ID="hdnTotalPL" runat="server" />
