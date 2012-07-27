<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IFAAdminMainDashboard.ascx.cs"
    Inherits="WealthERP.Advisor.IFAAdminMainDashboard" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="../General/Pager.ascx" TagName="Pager" TagPrefix="Pager" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<table style="width: 100%;" padding-top="1px" class="TableBackground" cellspacing="0"
    cellpadding="0">
    <tr>
        <td colspan="3" style="padding-top:2px">
            <div class="divPageHeading" style="vertical-align: text-bottom" >
                Branch AUM
            </div>
        </td>
        
    </tr>
   <tr>
   <td colspan="3">
   
    <hr style="height: 1px; line-height: 1px;" />
   </td>
   </tr>
            
        
</table>
<table style="width: 100%;" padding-top="0" class="TableBackground">
    <tr>
        <td align="left" colspan="2" style="padding-left: 2px; padding-right: 2px">
            <telerik:RadGrid ID="gvrAdminBranchPerform" runat="server" AllowAutomaticInserts="false"
                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" EnableEmbeddedSkins="false"
                GridLines="None" PageSize="10" ShowFooter="true" ShowStatusBar="True" Skin="Telerik"
                Width="100%" OnNeedDataSource="gvrAdminBranchPerform_NeedDataSource">
                <%--<PagerStyle Mode="NumericPages"></PagerStyle>--%>
                <MasterTableView AllowMultiColumnSorting="true" AutoGenerateColumns="false" CommandItemDisplay="None"
                    Width="100%">
                    <Columns>
                        <telerik:GridBoundColumn DataField="Branch Name" HeaderText="Branch Name" UniqueName="Branch Name"
                            FooterText="Total:" FooterStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Branch Code" HeaderText="Branch Code" UniqueName="Branch Code">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" DataField="Equity" DataType="System.Decimal"
                            DataFormatString="{0:n2}" HeaderText="Equity (Rs)" UniqueName="Equity" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" DataField="MF" DataType="System.Decimal"
                            DataFormatString="{0:n2}" HeaderText="MF (Rs)" UniqueName="MF" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" DataField="Insurance" DataType="System.Decimal"
                            DataFormatString="{0:n2}" HeaderText="Insurance (Rs)" UniqueName="Insurance"
                            FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" DataField="NoOfCustomers" DataType="System.Int32"
                            DataFormatString="{0:0}" HeaderText="No. of Customers" UniqueName="NoOfCustomers"
                            FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <%--<Scrolling AllowScroll="false" UseStaticHeaders="True" SaveScrollPosition="true">
                        </Scrolling>--%>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    <%-- <Resizing AllowColumnResize="True"></Resizing>--%>
                </ClientSettings>
            </telerik:RadGrid>
        </td>
    </tr>
    <tr>
        <td align="left">
           
        </td>
        <td align="right">
            <asp:Label ID="lblGTT" runat="server" CssClass="FieldName" Text="Grand Total (Rs):"></asp:Label>
            <asp:Label ID="lblGT" runat="server" CssClass="Field" Text="Label"></asp:Label>
        </td>
    </tr>
    <tr>
    <td colspan="2"></td>
    </tr>
    
</table>      
      
<table style="width: 100%;" cellpadding="0" cellspacing="0">
    <tr>
        <td align="left" style="width : 50%">
         <div class="divPageHeading" style="vertical-align: text-bottom">
            <asp:Label ID="lblRMPerformChart" runat="server" Text="RM AUM" CssClass="HeaderTextSmall"></asp:Label>
           
            </div>
        </td>
        <td style="width : 50%">
        <div class="divPageHeading" style="vertical-align: text-bottom">
            <asp:Label ID="lblBranchPerformChart" runat="server" Text="Branch AUM" CssClass="HeaderTextSmall"></asp:Label>
          
            </div>
        </td>
    </tr>
    <tr>
        <td align="left" style="padding-left: 2px; padding-right: 2px">
            <asp:Chart ID="ChartRMPerformance" runat="server" BackColor="Transparent" Width="400px"
                Height="200px">
                <Series>
                    <asp:Series Name="Series1">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea BackColor="#EBEFF9" Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            </td>
        <td style="float: right;" style="padding-left: 2px; padding-right: 2px">
            <asp:Chart ID="ChartBranchPerformance" runat="server" BackColor="#F1EDED" Height="300px"
                Width="450px">
                <Series>
                    <asp:Series Name="Series1" XValueMember="Branch Code" YValueMembers="Aggr">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
</table>

