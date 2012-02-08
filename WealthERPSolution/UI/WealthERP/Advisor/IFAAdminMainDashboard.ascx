<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IFAAdminMainDashboard.ascx.cs" 
    Inherits="WealthERP.Advisor.IFAAdminMainDashboard" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register src="../General/Pager.ascx" tagname="Pager" tagprefix="Pager" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<table style="width: 100%;" class="TableBackground">
    <%--<tr>
        <td class="HeaderCell" colspan="3">
            <asp:Label ID="Label2" runat="server" CssClass="HeaderTextBig" Text="Advisor Dashboard"></asp:Label>
        </td>
    </tr>--%>
    <tr>
        <td align="center" colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="left" colspan="2">
            <asp:Label ID="Label1" runat="server" Text="Branch AUM" CssClass="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
    </tr>
      <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="left">
            <%--<asp:GridView ID="gvrAdminBranchPerform" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" DataKeyNames="Branch Id" EnableViewState="false" AllowPaging="True" ShowFooter="true"
                CssClass="GridViewStyle" OnRowDataBound="gvrAdminBranchPerform_RowDataBound">
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:BoundField DataField="Branch Name" HeaderText="Branch Name"  />
                    <asp:BoundField DataField="Branch Code" HeaderText="Branch Code"  />
                    <asp:BoundField DataField="Equity" HeaderText="Equity (Rs)" 
                        DataFormatString="{0:b}" HtmlEncode="false" ApplyFormatInEditMode="True" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MF" HeaderText="MF (Rs)"  HtmlEncode="false"
                        DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Insurance" HeaderText="Insurance (Rs)"
                        DataFormatString="{0:c}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="NoOfCustomers" HeaderText="No. of Customers" HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                </Columns>
            </asp:GridView>--%>
            
               <telerik:RadGrid ID="gvrAdminBranchPerform" runat="server" 
                        AllowAutomaticInserts="false" AllowPaging="True" 
                        AllowSorting="True" AutoGenerateColumns="False" EnableEmbeddedSkins="false"
                        GridLines="None" PageSize="10" ShowFooter="true" ShowStatusBar="True" 
                        Skin="Telerik" Width="100%">
                    <%--<PagerStyle Mode="NumericPages"></PagerStyle>--%>
                    <mastertableview allowmulticolumnsorting="true" autogeneratecolumns="false" CommandItemDisplay="None"
                        width="99%">
                        <Columns>
                        <telerik:GridBoundColumn DataField="Branch Name" HeaderText="Branch Name" 
                                UniqueName="Branch Name" FooterText="Total:" FooterStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                     
                        <telerik:GridBoundColumn DataField="Branch Code"
                                HeaderText="Branch Code" UniqueName="Branch Code">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn Aggregate="Sum"  DataField="Equity" DataType="System.Decimal" DataFormatString="{0:n2}"
                                HeaderText="Equity (Rs)" UniqueName="Equity"  FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn  Aggregate="Sum" DataField="MF" DataType="System.Decimal" DataFormatString="{0:n2}"
                                HeaderText="MF (Rs)" UniqueName="MF"  FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn Aggregate="Sum" DataField="Insurance" DataType="System.Decimal" DataFormatString="{0:n2}"
                                HeaderText="Insurance (Rs)" UniqueName="Insurance"  FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn Aggregate="Sum" DataField="NoOfCustomers" DataType="System.Int32" DataFormatString="{0:0}"
                                HeaderText="No. of Customers" UniqueName="NoOfCustomers" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>                  
                                                                                      
                    </Columns>
                    
                    </mastertableview>
                    <clientsettings>
                        <%--<Scrolling AllowScroll="false" UseStaticHeaders="True" SaveScrollPosition="true">
                        </Scrolling>--%>
                        <selecting allowrowselect="True" enabledragtoselectrows="True" />                           
                        <%-- <Resizing AllowColumnResize="True"></Resizing>--%>
                    </clientsettings>
                    </telerik:RadGrid>            
        </td>
        <td align="left">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
            <asp:Label ID="lblGTT" runat="server" CssClass="FieldName" Text="Grand Total (Rs):"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblGT" runat="server" CssClass="Field" Text="Label"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
                       <%-- <Pager:Pager ID="mypager" runat="server" />--%>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        <asp:HiddenField ID="hdnRecordCount" runat="server" />
        </td>
    </tr>
</table>
<table style="width: 100%;">
    <tr>
        <td align="left">
            <asp:Label ID="lblRMPerformChart" runat="server" Text="RM AUM" CssClass="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
        <td>
            <asp:Label ID="lblBranchPerformChart" runat="server" Text="Branch AUM" CssClass="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td align="left">
            <asp:Chart ID="ChartRMPerformance" runat="server" BackColor="Transparent" 
                Width="400px" Height="200px">
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
        <td style="float: right;">
            <asp:Chart ID="ChartBranchPerformance" runat="server" BackColor="#F1EDED"
                Height="300px" Width="450px">
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
<table width="100%">
<tr><td align="center">
<div class="information-msg" id="MessageReceived" runat="server" visible="false" align="center">
<br />
    <asp:Label ID="lblSuperAdmnMessage" runat="server"></asp:Label>
    <br />
</div>
</td></tr>
</table>
