<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerFPDashBoard.ascx.cs" Inherits="WealthERP.Customer.CustomerFPDashBoard" %>

<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>

<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<table  style="width: 100%;">
         <tr>
        <td>
            <table id="ErrorMessage" align="center" runat="server">
                <tr>
                    <td>
                        <div class="failure-msg" align="center">
                            No Asset Records found for selected Customer...
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
         <tr>
        <td align="left">
           <asp:Label ID="lblBranchAUM" runat="server" CssClass="HeaderTextSmall" Text="Product Assets"></asp:Label>
           <hr id="hrBranchAum" runat="server" />
        </td>
        <td>
        <asp:Label ID="lblChartBranchAUM" runat="server" CssClass="HeaderTextSmall" Text="Product Asset Pie chart"></asp:Label>
        <hr runat="server" id="hrCustAsset" />
                </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <telerik:RadGrid ID="gvFPDashBoard" Width="450px" AllowFilteringByColumn="false"
                   Skin="Telerik" EnableEmbeddedSkins="false" PagerStyle-EnableSEOPaging="true" runat="server" ShowFooter="true">
                 <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                    <Columns>
                    <telerik:GridBoundColumn DataField="Asset" HeaderText="Asset">
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn  Aggregate="Sum"  ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" DataField="CurrentValue" HeaderText="Current Value (Rs.)"
                      FooterText="Total:  " FooterStyle-HorizontalAlign="Right" >
                    </telerik:GridBoundColumn>
                    </Columns>
                 </MasterTableView>
                
                 <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </ClientSettings>
                </telerik:RadGrid>
                <br />
                <table runat="server" id="AssteLiaNetworthTable">
                <tr>
                <td>
                <asp:Label ID="TotalAssets" runat="server" CssClass="HeaderTextSmall" Text="Total Assets : "></asp:Label>
                </td>
                <td>
                <asp:Label ID="TotalValue" runat="server" CssClass="HeaderTextSmall" Text=""></asp:Label>
                </td>
                 </tr>
                 <tr>
                 <td>
                <asp:Label ID="TotalLiabilities" runat="server" CssClass="HeaderTextSmall" Text="Liabilities : "></asp:Label>
                </td>
                <td>
                <asp:Label ID="TotalLiabilitiesValue" runat="server" CssClass="HeaderTextSmall" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <td>
                <asp:Label ID="TotalNetworth" runat="server" CssClass="HeaderTextSmall" Text="Networth : "></asp:Label>
                </td>
                <td>
                <asp:Label ID="NetworthValue" runat="server" CssClass="HeaderTextSmall" Text=""></asp:Label>
                </td>
                </tr>
                </table>
               </td>
               <td >
                <asp:Chart ID="ChartBranchAssets" runat="server" BackColor="Transparent"
                     Width="400px" Height="250px"  >
                    <Series>
                        <asp:Series Name="seriesBranchAssets" LabelBackColor="Red" ChartType="Pie" 
                            LegendText="#VALX" YValueType="Double">  
                         <EmptyPointStyle CustomProperties="PieLabelStyle=Outside" />
                        </asp:Series> 
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1" >
                        <area3dstyle Enable3D="True" Inclination="30" PointDepth="180" WallWidth="30" LightStyle="Simplistic" Rotation="90" Perspective="30" IsRightAngleAxes="true" IsClustered="true" />  
                        </asp:ChartArea>
                    </ChartAreas> 
                    </asp:Chart>
     
                </td>
            </tr>
            <tr>
            <td></td><td></td>
            </tr>
            <tr>
            <td></td><td></td>
            </tr>
            </table>
            <table style="width: 100%;">
            <tr>
                <td align="left">
           <asp:Label ID="Label1" runat="server" CssClass="HeaderTextSmall" Text="Current Asset Allocation"></asp:Label>
           <hr id="hr1" runat="server" />
        </td>
        <td>
        <asp:Label ID="Label2" runat="server" CssClass="HeaderTextSmall" Text="Recommended Asset Allocation"></asp:Label>
        <br />
        <asp:Label ID="lblAgeResult" runat="server" CssClass="FieldName"></asp:Label>
                              
        <hr />
        </td>
        </tr>
        
            <tr>
            <td runat="server" id="tdCurrentAssetAllocation">
            <asp:Label ID="lblCurrChartErrorDisplay" CssClass="HeaderTextSmall" runat="server" Text="No Current asset records found for selected customer!"
                                        Visible="False"></asp:Label>
                                        <br />
            
            <asp:Chart ID="ChartCurrentAsset" runat="server" BackColor="Transparent"
                    Width="400px" Height="250px"  >
                    <Series>
                        <asp:Series Name="sActualAsset" ChartArea="caActualAsset" LabelBackColor="Red" ChartType="Pie" 
                             LegendText="#VALX" YValueType="Double">  
                         <EmptyPointStyle CustomProperties="PieLabelStyle=Outside" />
                        </asp:Series> 
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="caActualAsset" >
                        <area3dstyle Enable3D="True" Inclination="30" PointDepth="180" WallWidth="30" LightStyle="Simplistic" Rotation="90" Perspective="30" IsRightAngleAxes="true" IsClustered="true" />  
                        </asp:ChartArea>
                    </ChartAreas> 
                    </asp:Chart>
           
            </td>
            <td runat="server" id="tdRecomondedAssetAllocation">
            
            <asp:Label ID="lblChartErrorDisplay" CssClass="HeaderTextSmall" runat="server" Text="No recommended asset records found for selected customer!"
                                        Visible="False"></asp:Label>
                                        <br />
            
             <asp:Chart ID="ChartRecomonedAsset" runat="server" BackColor="Transparent"
                     Width="400px" Height="250px"  >
                    <Series>
                        <asp:Series Name="sActualAsset" ChartArea="caActualAsset" LabelBackColor="Red" ChartType="Pie" 
                            LegendText="#VALX" YValueType="Double">  
                         <EmptyPointStyle CustomProperties="PieLabelStyle=Outside" />
                        </asp:Series> 
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="caActualAsset" >
                        <area3dstyle Enable3D="True" Inclination="30" PointDepth="180" WallWidth="30" LightStyle="Simplistic" Rotation="90" Perspective="30" IsRightAngleAxes="true" IsClustered="true" />  
                        </asp:ChartArea>
                    </ChartAreas> 
                    </asp:Chart>
           
            </td>
            </tr>
            
              <td colspan="2">
                                    
              </td>
        </table>
     <table width="100%" style="text-align:left">
  
     <tr>
    <td>
    
    </td>
    </tr>
    <tr>
    <td>
    
    </td>
    </tr>
    <tr>
    <td>
    
    </td>
    </tr>
        <tr>
            <td>
                <asp:Label ID="lblNote" runat="server" CssClass="HeaderTextSmall" Text="Note: Data on the screen can be updated by clicking on finance profile synchronization button."></asp:Label>
            </td>
        </tr>    
    </table>
        
<asp:HiddenField ID="lblRScore" runat="server" Visible="false" />
<asp:HiddenField ID="lblRClass" runat="server" Visible="false" />
