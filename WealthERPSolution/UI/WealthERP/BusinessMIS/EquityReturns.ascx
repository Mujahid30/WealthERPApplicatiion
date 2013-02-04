<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EquityReturns.ascx.cs" Inherits="WealthERP.BusinessMIS.EquityReturns" %>
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
        <td align="left">EQ Returns</td>
        <td  align="right">
                        <asp:ImageButton ID="imgEQReturns" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" 
                        Height="20px" Width="25px" Visible="false" onclick="imgEQReturns_Click"></asp:ImageButton>
                          
        </td>
        </tr>
    </table>
</div>
</td>
</tr>
</table>
<table width="80%">
<tr>
<td align="left">
<asp:Button ID="btnGo" runat="server" Text="GO" CssClass="PCGButton" 
        onclick="btnGo_Click"/>
        </td>
        <td>
        </td>
        <td class="rightField" style="width: 10%">
            <asp:Label ID="lblDate" runat="server" Text="As on Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 10%">
            <asp:Label ID="lblPickDate" Text="" runat="server" CssClass="FieldName"> </asp:Label>
        </td>
</tr>
</table>
<table width="100%">
<tr>
<td colspan="4">
<asp:Panel ID="pnlEQReturns" runat="server"  ScrollBars="Horizontal" Width="98%" Visible="true">
<table>
<tr><td>
<div runat="server" id="divEQReturns"  style="margin: 2px;width: 640px;">
    <telerik:RadGrid ID="gvEQReturns" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
        AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" OnNeedDataSource="gvEQReturns_OnNeedDataSource">
        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                    FileName="EQReturns" Excel-Format="ExcelML">
        </ExportSettings>
        <MasterTableView Width="100%"  AllowMultiColumnSorting="True"
            AutoGenerateColumns="false" CommandItemDisplay="None">
            <Columns>
            <telerik:GridBoundColumn  HeaderText="CustomerId"  DataField="CustomerId"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="CustomerId" SortExpression="CustomerId" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Customer"  DataField="Customer"
                    UniqueName="Customer" SortExpression="Customer" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="PAN"  DataField="PAN"
                    UniqueName="PAN" SortExpression="PAN" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn HeaderText="Group Head"  DataField="Parent"
                    UniqueName="Parent" SortExpression="Parent" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Branch"  DataField="Branch" 
                    UniqueName="Branch" SortExpression="Branch" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle  HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="RM"  DataField="RM" 
                    UniqueName="RM" SortExpression="RM" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle  HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Scrip/Company"  DataField="CompanyName" 
                    UniqueName="CompanyName" SortExpression="CompanyName" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle  HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="No of Shares"  DataField="NoOfShares"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="NoOfShares" SortExpression="NoOfShares" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}" Aggregate="Sum"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="Price"  DataField="Price"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="Price" SortExpression="Price" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N4}" Aggregate="Sum"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="Market Value"  DataField="MarketValue"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="MarketValue" SortExpression="MarketValue" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}" Aggregate="Sum"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn  HeaderText="Profit/Loss"  DataField="ProfitLoss"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="ProfitLoss" SortExpression="ProfitLoss" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right" >
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="Profit/Loss (%)" DataField="Percentage"
                   HeaderStyle-HorizontalAlign="Right" UniqueName="Percentage" SortExpression="Percentage" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                     DataFormatString="{0:N2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="Absolute Rtn" DataField="AbsRtn"
                   HeaderStyle-HorizontalAlign="Right" UniqueName="AbsRtn" SortExpression="AbsRtn" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                     DataFormatString="{0:N2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
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
</td></tr>
</table>
</asp:Panel>
</td>
</tr>
</table>
<asp:HiddenField ID="hdnbranchId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnbranchHeadId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAll" runat="server" Visible="false" />
<asp:HiddenField ID="hdnrmId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnadviserId" runat="server" />
