<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BMDashBoard.ascx.cs" Inherits="WealthERP.UserManagement.BMDashBoard" %>
 <html>
    <head>
    <meta http-equiv="pragma" content="no-cache" />
    </head>
    <body>
    <%--<table width="100%">
        <tr>
            <td align="right">
             <asp:HyperLink ID="hlReleases" runat="server" NavigateUrl="~/Releases.html" Target="_blank" CssClass="LinkButtons">Release Bulletin</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
            
            </td>
        </tr>
    </table>--%>
    
    <%-- BM Dashboard --%>
    
    <table width="100%">
    
        <tr>
        <br />
            <td>
            <asp:Label ID="lblChooseBr" runat="server" Font-Bold="true" Font-Size="Small" CssClass="FieldName" Text="Branch: "></asp:Label>
            <asp:DropDownList ID="ddlBMBranch" runat="server"  
                    onselectedindexchanged="ddlBMBranch_SelectedIndexChanged" CssClass="cmbField" style="vertical-align: middle" AutoPostBack="true">
            </asp:DropDownList>
            <br /> 
              <br /> 
     
            </td>
        </tr>
        </table>
        <table  style="width: 100%;">
         <tr>
        <td>
            <table id="ErrorMessage" align="center" runat="server">
                <tr>
                    <td>
                        <div class="failure-msg" align="center">
                            No Asset Records found for selected Branch...
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
         <tr>
        <td align="left">
           <asp:Label ID="lblBranchAUM" runat="server" CssClass="HeaderTextSmall" Text="Branch AUM"></asp:Label>
           <hr id="hrBranchAum" runat="server" />
        </td>
        <td>
        <asp:Label ID="lblChartBranchAUM" runat="server" CssClass="HeaderTextSmall" Text="Branch AUM"></asp:Label>
        <hr />
        </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvBMDashBoardGrid" runat="server" 
                AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                EnableViewState="false" AllowPaging="True" ShowFooter="true"  
                CssClass="GridViewStyle" Width="550px" Height="330px"
                OnRowDataBound="BMDashBoardGrid_RowDataBound" 
                    ToolTip="Branch Asset GridView" >
                    <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                    <asp:TemplateField HeaderText="Asset" ItemStyle-ForeColor="Black" >
                    <ItemStyle HorizontalAlign="left"  />
                     <FooterStyle HorizontalAlign="left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSavingReq" runat="server" CssClass="GridViewCmbFieldforBM" 
                                            Text='<%#Eval("Asset") %>' ForeColor="Black">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalText" runat="server" CssClass="Field" Font-Bold="true" 
                                            ForeColor="White" Text="">
                                        </asp:Label>
                                    </FooterTemplate>
                   </asp:TemplateField>
                     <asp:TemplateField HeaderText="Current Value (Rs.)" HeaderStyle-HorizontalAlign="Right" >
                     <ItemStyle HorizontalAlign="Right" />
                     <FooterStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSavingReq" runat="server" CssClass="GridViewCmbFieldforBM" 
                                            Text='<%#Eval("CurrentValue") %>' ForeColor="Black">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalValue" runat="server" CssClass="Field" Font-Bold="true" 
                                            ForeColor="White" Text="">
                                        </asp:Label>
                                    </FooterTemplate>
                   </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
               </td>
               <td >
        <asp:Chart ID="ChartBranchAssets" runat="server" BackColor="#F1F9FC"
                Width="550px" Height="300px"  >
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
        <br />
        </td>
        </tr>
        </table>
        <table style="width: 100%;">
        <tr>
        <td>
            <table id="ErrorMsgForTop5RMs" align="center" runat="server">
                <tr>
                    <td>
                        <div class="failure-msg" align="center">
                            No Top 5 RMs Records found for selected Branch...
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
       
        <tr>
        <td align="left">
           <asp:Label ID="lblTop5RM" runat="server" CssClass="HeaderTextSmall" Text="Top 5 RMs"></asp:Label>
           <hr id="hrTop5Rm" runat="server" />
        </td>
         <td >
           <asp:Label ID="lblTop5Rms" runat="server" CssClass="HeaderTextSmall" Text="Top 5 RMs (Customer Base)"></asp:Label>
           <hr />
        </td>
        </tr>
        <tr>
        <td style="vertical-align: top">
         <asp:GridView ID="gvRMCustNetworth" runat="server" AllowSorting="True" 
                    Width="550px" AutoGenerateColumns="False" GridLines="Both"
                CellPadding="4" EnableViewState="false" AllowPaging="True" RowStyle-Wrap="true" ShowFooter="true"
                CssClass="GridViewStyle" ToolTip="Top 5 Customers Networth GridView" >
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
               <%-- OnRowCreated="gvBMDashBoardGrid_RowCreated" --%>
                    <Columns>
                    <asp:BoundField DataField="RmName" HeaderText="Name" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Staff_Code" HeaderText="Code" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="Customer_base" HeaderText="Customer Base" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="Customer_networth" DataFormatString="{0:n}" HtmlEncode="false" HeaderText="NetWorth" ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                </asp:GridView>
        <br />
        </td>
        <td>
        <asp:Chart ID="CharttopfiveRMCustNetworth" runat="server" BackColor="#F1F9FC"
                Height="380px" Width="600px">
                <Series>
                    <asp:Series Name="RMCustNet" XValueMember="Customers" YValueMembers="NetWorth" 
                        Font="Microsoft Sans Serif, 10pt, style=Bold">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
        </asp:Chart>
        <br />
        </td>
        </tr>
        </table>
        <table style="width: 100%;">
        <tr>
        <td>
            <table id="ErrorMsgForTop5Customer" align="center" runat="server">
                <tr>
                    <td>
                        <div class="failure-msg" align="center">
                            No Top 5 Customer Records found for selected Branch...
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
        <tr>
        <td align="left">
             <asp:Label ID="lblTop5CustNetworth" runat="server" CssClass="HeaderTextSmall" Text=" Top 5 Customers by Networth"></asp:Label>
             <hr id="hrTop5Cust" runat="server" />
        </td>
        <td >
        <asp:Label ID="chartCustNetworth" runat="server" CssClass="HeaderTextSmall" Text="Top 5 Customers (Networth)"></asp:Label>
        <hr />
        </td>
        </tr>
        <tr>
        <td style="vertical-align: top">
        
         <asp:GridView ID="gvCustNetWorth" runat="server" AllowSorting="True" 
                    Width="550px" AutoGenerateColumns="False" GridLines="Both"
                CellPadding="4" EnableViewState="false" AllowPaging="True" ShowFooter="true"
                CssClass="GridViewStyle" ToolTip="Top 5 Customers Networth GridView" >
                  <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                    <Columns>
                    <asp:BoundField DataField="Customer" HeaderText="Customer" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Asset" HeaderText="Asset" DataFormatString="{0:n}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="Liabilities" DataFormatString="{0:n}" HtmlEncode="false" HeaderText="Liabilities" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="Networth" DataFormatString="{0:n}" HtmlEncode="false" HeaderText="NetWorth" ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                </asp:GridView>
        </td>
        <td>
        
        <asp:Chart ID="ChartCustomerNetworth" runat="server" BackColor="#F1F9FC"
                Height="380px" Width="600px">
                <Series>
                    <asp:Series Name="CustNetworth" Font="Microsoft Sans Serif, 10pt, style=Bold" XValueMember="Customers" YValueMembers="NetWorth">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
        </asp:Chart>
        
        
        </td>
        
        </tr>
        
    </table>
       
    </body>
</html>

 