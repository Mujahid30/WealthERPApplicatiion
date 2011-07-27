<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerFPAnalyticsStandard.ascx.cs" Inherits="WealthERP.FP.CustomerFPAnalyticsStandard" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<style type="text/css">
    .one
    {
        border-style:solid;
        border-width:medium;
    }
    .row
    {
        font-family: Verdana,Tahoma;
            font-weight:lighter;
            font-size: 10px;
            color:Black;
            vertical-align: middle;
    }
    .header
        {
            font-family: Verdana,Tahoma;
            font-weight: bold;
            font-size: 12px;
            color: #16518A;           
        }
    .label
    {         
        text-align:center;
        width : 60px;        
    }
    .two
    {
        border-width:0px;
    }
</style>

<script type="text/javascript">
    function ExportMyChart() {
    alert('Hi')
    var chartObject = getChartFromId('myFirst');
    alert(chartObject);
        if (chartObject.hasRendered())
        alert('Inside IF')
         chartObject.exportChart();
    }
</script>   

<script src="../FusionCharts/FusionCharts.js" type="text/javascript"></script>

<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<asp:Label ID="headertitle" runat="server" CssClass="HeaderTextBig" Text="FP Analytics - Standard"></asp:Label>
<hr />


<telerik:RadTabStrip ID="RadTabStrip1" runat="server" EnableTheming="True" Skin="Telerik"
EnableEmbeddedSkins="False" MultiPageID="CustomerFPAnalyticsStandardId" 
SelectedIndex="3">
       <Tabs>
          <telerik:RadTab runat="server" Text="Financial Health"
        Value="Financial Health" TabIndex="0">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Income & Expense"
        Value="Income & Expense" TabIndex="1">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Networth"
        Value="Networth" TabIndex="2">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Life Insurance"
        Value="Life Insurance" TabIndex="3" Selected="True">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="General Insurance"
        Value="Health Insurance" TabIndex="4" Selected="True">
        </telerik:RadTab>
<%--        <telerik:RadTab runat="server" Text="Cash Flow"
        Value="Cash Flow" TabIndex="5">
        </telerik:RadTab>--%>
    </Tabs>
</telerik:RadTabStrip>


<telerik:RadMultiPage ID="CustomerFPAnalyticsStandardId" runat="server" EnableViewState="true"
SelectedIndex="3">

<telerik:RadPageView ID="RadPageFinancialHealth" runat="server">
<asp:Panel ID="pnlPageFinancialHealth" runat="server">
<table>
    <tr>
        <td>
            <asp:Repeater ID="repFinancialHealth" runat="server">        
                <HeaderTemplate></HeaderTemplate>
                <ItemTemplate>
                    <table width="100%">
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lblRatioName" runat="server" CssClass="HeaderTextSmall" 
                                Text='<%#DataBinder.Eval(Container.DataItem, "RatioName")%>'></asp:Label>
                            </td>        
                        </tr>
                        <tr>
                            <td colspan="4">
                            <b><hr /></b></td>
                        </tr>
                        <tr>
                            <td colspan="4" class="row">
                                <asp:Label ID="lblRatioPunchline" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RatioPunchLine") %>'>
                                </asp:Label>
                            </td>
                        </tr>
                        <tr><td></td></tr>
                        <tr>
                            <td style="width:30%">
                                <asp:Label ID= "lblValue" runat="server" CssClass="HeaderTextSmall" ForeColor="DarkRed" Text="Value:"></asp:Label>
                                <asp:Label ID="lblRatioValue" runat="server" CssClass="HeaderTextSmall" ForeColor="DarkRed"
                                Text='<%#DataBinder.Eval(Container.DataItem, "RatioValue") %>'></asp:Label>
                            </td>
                            <td style="width:70%">
                            
                            <table border="medium">
                                <tr>
                                    <td class="label" bgcolor="<%#DataBinder.Eval(Container.DataItem, "RatioColor")%>">
                                        <asp:Label ID= "lblRatoRange1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RatioRangeOne") %>'></asp:Label>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID= "lblRatoRange2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RatioRangeTwo") %>'></asp:Label>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID= "lblRatoRange3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RatioRangeThree") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Image ID="imgRedStatus1" ImageAlign="Middle" ImageUrl="../Images/RatioDownArrow.png" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Image ID="imgRedStatus2" ImageAlign="Middle" ImageUrl="../Images/RatioDownArrow.png" runat="server"  />
                                    </td>
                                    <td>
                                        <asp:Image ID="imgRedStatus3" ImageAlign="Middle" ImageUrl="../Images/RatioDownArrow.png" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            </td>
                        </tr>
                        <tr><td></td></tr>
                        <tr>
                            <td colspan="4" class="row">
                                <asp:Label ID="lblRatioDescription" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RatioDescription") %>'></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
                <SeparatorTemplate>
                <br />
                </SeparatorTemplate>        
            </asp:Repeater>
        </td>
    </tr>
</table>
</asp:Panel>
</telerik:RadPageView>

<telerik:RadPageView ID="RadPageIncomeExpense" runat="server">
   <asp:Panel ID="pnlIncomeExpense" runat="server">
         <table style="width: 100%;">
                <tr>
                    <br /> 
                    <td align="left" valign="top">
                        <asp:Label ID="lblIncomeChart" runat="server" Text="Income" CssClass="HeaderTextSmall"> 
                        </asp:Label><hr />
                    </td>
                    <td valign="top">
                        <asp:Label ID="lblExpenseChart" runat="server" Text="Expense" CssClass="HeaderTextSmall">
                        </asp:Label><hr />
                    </td>
                </tr>
                <tr>
                    <td>
                            <asp:Chart ID="ChartIncome" runat="server" BackColor="Transparent" 
                                Height="250px" Width="450px">
                                <Series>
                                    <asp:Series Name="Series"></asp:Series>
                                </Series>
                                
                                <ChartAreas>
                                    <asp:ChartArea BackColor="#EBEFF9" Name="ChartArea1"></asp:ChartArea>
                                </ChartAreas>
                             </asp:Chart>
                        </td>
                    <td>
                            <asp:Chart ID="ChartExpense" runat="server" BackColor="Transparent" 
                            Height="250px" Width="450px">
                                <Series>
                                <asp:Series Name="Series"></asp:Series>
                                </Series>
                        
                                <ChartAreas>
                                <asp:ChartArea BackColor="#EBEFF9" Name="ChartArea1"></asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                </tr>
                
                <tr id="tdIncomeExpenseError" runat="server">
                    <td id="tdIncomeError" align="center" runat="server" style="width:50%"> 
                        <br />                                        
                        <asp:Label ID="lblIncomeError" runat="server" CssClass="failure-msg" 
                        Text="No records found for Income"></asp:Label>
                    </td>
                    <td id="tdExpenseError" align="center" runat="server" style="width:50%">
                        <br />                        
                        <asp:Label ID="lblExpenseError" runat="server" CssClass="failure-msg" 
                        Text="No records found for Expense"></asp:Label>
                    </td>
                </tr>        
                <tr>                
                    <td id="tdgvrIncome" runat="server" valign="top">
                            <telerik:RadGrid ID="gvrIncome" Width="450px" AllowFilteringByColumn="false" CssClass="GridViewStyle"
                               Skin="Telerik" ShowFooter="true" EnableEmbeddedSkins="false" PagerStyle-EnableSEOPaging="true" runat="server">
                           
                                <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                                <HeaderStyle CssClass="HeaderStyle" />
                                <%--<FooterStyle CssClass="FooterStyle" /> --%>                               
                                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />                              
                                    <Columns>                                
                                        <telerik:GridBoundColumn DataField="IncomeCategory" HeaderText="Type"  
                                        FooterStyle-HorizontalAlign="Left" FooterText="Total">                              
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"  
                                        HeaderStyle-HorizontalAlign="Right" DataField="IncomeAmount" HeaderText="Current Value (Rs.)" FooterText=" ">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-HorizontalAlign="Right" 
                                        HeaderStyle-HorizontalAlign="Right" DataField="Percent" HeaderText="Pctg(%)" FooterText=" ">
                                        </telerik:GridBoundColumn>
                                    </Columns>               
                             </MasterTableView>                            
                                <ClientSettings>
                                <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                            </ClientSettings>
                            </telerik:RadGrid>        
                    </td>                 
                    <td id="tdRedGridExpense" runat="server" valign="top">
                            <telerik:RadGrid ID="RedGridExpense" Width="450px" AllowFilteringByColumn="false" CssClass="GridViewStyle"
                               Skin="Telerik" ShowFooter="true" EnableEmbeddedSkins="false" PagerStyle-EnableSEOPaging="true" runat="server">
                             <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                                <HeaderStyle CssClass="HeaderStyle" />
                                <%--<FooterStyle CssClass="FooterStyle" /> --%>                               
                                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />                              
                                <Columns>                                
                                <telerik:GridBoundColumn DataField="ExpenseCategory" HeaderText="Type" 
                                FooterStyle-HorizontalAlign="Left" FooterText="Total">                              
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-HorizontalAlign="Right" 
                                HeaderStyle-HorizontalAlign="Right" DataField="ExpenseAmount" HeaderText="Current Value (Rs.)" FooterText=" ">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"  
                                HeaderStyle-HorizontalAlign="Right" DataField="Percentage" HeaderText="Pctg(%)" FooterText=" ">
                                </telerik:GridBoundColumn>
                                </Columns>                      
                             </MasterTableView>
                            
                             <ClientSettings>
                                <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                            </ClientSettings>
                            </telerik:RadGrid>
                    </td>                
                </tr>            
            </table>
    
         <table width="100%">
            <tr>
                <td><br />
                    <asp:Label ID="LblCashFlowAnnual" runat="server" Text="Cash Flows (Annual)" 
                    CssClass="HeaderTextSmall"></asp:Label><hr />
                </td>
            </tr>
            <tr>
              <td>
              <br />
                  <asp:Chart ID="ChartCashFlow" runat="server" BackColor="#F1EDED" 
                   Height="300px" Width="450px">
                    <Series>
                    <asp:Series Name="Series" XValueMember="Cash Category" YValueMembers="Amount"></asp:Series>
                    </Series>
            
                    <ChartAreas>
                    <asp:ChartArea BackColor="#EBEFF9" Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                  </asp:Chart>
              </td>             
            </tr>
            <tr >
                <td align="center">
                    <asp:Label ID="lblCashFlowError" runat="server" CssClass="failure-msg" 
                            Text="No records found for Annual Cash Flow" Visible="false">
                    </asp:Label>
                </td>
            </tr>
         </table>
    
          
   </asp:Panel>
</telerik:RadPageView>

<telerik:RadPageView ID="RadPageNetworth" runat="server">
   <asp:Panel ID="pnlPageNetworth" runat="server">
      
         <table style="width: 100%">
                <tr>
                    <br /> 
                    <td align="left" valign="top" >                    
                        <asp:Label ID="lblAssetChart" runat="server" Text="Asset" CssClass="HeaderTextSmall"> 
                        </asp:Label><hr />
                    </td>
                    <td valign="top">
                        <asp:Label ID="lblLiabilitiesChart" runat="server" Text="Liabilities" CssClass="HeaderTextSmall">
                        </asp:Label><hr />
                    </td>
                </tr> 
                <tr>
                    <td>
                                <asp:Chart ID="ChartAsset" runat="server" BackColor="Transparent" 
                                    Height="250px" Width="450px">
                                <Series>
                                    <asp:Series Name="Series"></asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea BackColor="#EBEFF9" Name="ChartArea1"></asp:ChartArea>
                                </ChartAreas>
                                </asp:Chart>
                    </td>
                    <td>
                           <asp:Chart ID="ChartLiabilities" runat="server" BackColor="Transparent" 
                            Height="250px" Width="450px">
                                <Series>
                                <asp:Series Name="Series"></asp:Series>
                                </Series>
                        
                                <ChartAreas>
                                <asp:ChartArea BackColor="#EBEFF9" Name="ChartArea1"></asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                    </td>
                </tr>
                <tr id="trAssetLiabilitiesError" runat="server" style="width:100%">
                    <td align="center" id="tdAssetErrorMsg" runat="server" style="width:50%">
                        <br />                            
                            <asp:Label ID="lblAssetErrorMsg" runat="server" CssClass="failure-msg" 
                            Text="No records found for Asset"></asp:Label>
                    </td>
                    <td id="tdErrorLiabilities" align="center" runat="server" style="width:50%">
                        <br />                         
                        <asp:Label ID="lblErrorLiabilities" runat="server" CssClass="failure-msg" 
                        Text="No records found for Liabilities"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td id="tdRadGridAsset" runat="server" valign="top" >
                        <telerik:RadGrid ID="RadGridAsset" Width="450px" AllowFilteringByColumn="false" CssClass="GridViewStyle"
                               Skin="Telerik" ShowFooter="true" EnableEmbeddedSkins="false" PagerStyle-EnableSEOPaging="true" runat="server">
                             
                                                          
                             <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                                <HeaderStyle CssClass="HeaderStyle" />
                                <%--<FooterStyle CssClass="FooterStyle" /> --%>                               
                                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />                              
                                <Columns>                                
                                <telerik:GridBoundColumn DataField="AssetGroupName" HeaderText="Type" 
                                FooterStyle-HorizontalAlign="Left" FooterText="Total">                              
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-HorizontalAlign="Right" 
                                HeaderStyle-HorizontalAlign="Right" DataField="AssetValues" HeaderText="Current Value (Rs.)" FooterText=" ">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-HorizontalAlign="Right" 
                                HeaderStyle-HorizontalAlign="Right" DataField="Pctg" HeaderText="Pctg(%)" FooterText=" ">
                                </telerik:GridBoundColumn>
                                </Columns>                                
                             </MasterTableView>
                            
                             <ClientSettings>
                                <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                            </ClientSettings>
                            </telerik:RadGrid>
                    </td>
                    <td id="tdRadGridLiabilities" runat="server" valign="top">
                        <telerik:RadGrid ID="RadGridLiabilities" Width="450px" AllowFilteringByColumn="false" CssClass="GridViewStyle"
                               Skin="Telerik" ShowFooter="true" EnableEmbeddedSkins="false" PagerStyle-EnableSEOPaging="true" runat="server">
                             
                                                          
                             <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                                <HeaderStyle CssClass="HeaderStyle" />
                                <%--<FooterStyle CssClass="FooterStyle" /> --%>                               
                                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />                              
                                <Columns>                                
                                <telerik:GridBoundColumn DataField="LoanType" HeaderText="Loan" 
                                FooterStyle-HorizontalAlign="Left" FooterText="Total">                              
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-HorizontalAlign="Right" 
                                HeaderStyle-HorizontalAlign="Right" DataField="LoanValues" HeaderText="Outstanding Amount" FooterText=" ">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-HorizontalAlign="Right" 
                                HeaderStyle-HorizontalAlign="Right" DataField="Prcentage" HeaderText="Pctg(%)" FooterText=" ">
                                </telerik:GridBoundColumn>
                                </Columns>                                
                             </MasterTableView>
                            
                             <ClientSettings>
                                <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                            </ClientSettings>
                            </telerik:RadGrid>
                    </td>
                </tr>           
         </table>

   </asp:Panel>
</telerik:RadPageView>

<telerik:RadPageView ID="RadPageLifeInsurance" runat="server">
<asp:Panel ID="pnlPageLifeInsurance" runat="server">
    <table style="width: 100%">
        <tr>
            <td>
                <br /> 
                <asp:Label ID="lblLIDetails" runat="server" Text="Life Insurance Details" CssClass="HeaderTextSmall"> 
                </asp:Label><hr />
            </td>
        </tr>
        <tr id="trRadGridLifeInsurance" runat="server">
            <td>
                <telerik:RadGrid ID="RadGridLifeInsurance" Width="450px" AllowFilteringByColumn="false" CssClass="GridViewStyle"
                               Skin="Telerik" ShowFooter="true" EnableEmbeddedSkins="false" PagerStyle-EnableSEOPaging="true" runat="server">
                            <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                                <HeaderStyle CssClass="HeaderStyle" />
                                <%--<FooterStyle CssClass="FooterStyle" /> --%>                               
                                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />                              
                                <Columns>                                
                                <telerik:GridBoundColumn DataField="InsuranceCategoryName" HeaderText="Type" ItemStyle-Width="60%"  
                                FooterStyle-HorizontalAlign="Left" FooterText="Total">                              
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-HorizontalAlign="Right" ItemStyle-Width="40%"
                                HeaderStyle-HorizontalAlign="Right" DataField="InsuranceValues" HeaderText="Sum Assured(Rs.)" FooterText=" ">
                                </telerik:GridBoundColumn>                                
                                </Columns>                                
                             </MasterTableView>
                            
                             <ClientSettings>
                                <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                            </ClientSettings>
                            </telerik:RadGrid>            
                
            </td>
        </tr>
        <tr id="trErrorLifeInsurance" runat="server">
            <td id="tdErrorLifeInsurance" runat="server" align="center"> 
                <br />
                <asp:Label ID="lblErrorLifeInsurance" runat="server" CssClass="failure-msg" 
                            Text="No records found for Life Insurance Details"></asp:Label>               
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr id="trRadGridHLVAnalysis" runat="server">
            <td> 
                <telerik:RadGrid ID="RadGridHLVAnalysis" Width="450px" AllowFilteringByColumn="false" CssClass="GridViewStyle"
                               Skin="Telerik" ShowFooter="true" EnableEmbeddedSkins="false" PagerStyle-EnableSEOPaging="true" runat="server">
                             
                                                          
                             <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                                <HeaderStyle CssClass="HeaderStyle" />
                                <%--<FooterStyle CssClass="FooterStyle" /> --%>                               
                                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />                              
                                <Columns>                                
                                <telerik:GridBoundColumn DataField="HLV_Type" HeaderText="Human Life Value Analysis" ItemStyle-Width="60%">                              
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-Width="40%" DataField="HLV_Values" HeaderText="HLV Values(Rs.)">
                                </telerik:GridBoundColumn>                                
                                </Columns>                                
                             </MasterTableView>
                            
                             <ClientSettings>
                                <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                            </ClientSettings>
                            </telerik:RadGrid>                    
                
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
         <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr id="trRadGridLIGapAnalysis" runat="server">
            <td>
                <telerik:RadGrid ID="RadGridLIGapAnalysis" Width="450px" AllowFilteringByColumn="false" CssClass="GridViewStyle"
                               Skin="Telerik" ShowFooter="true" EnableEmbeddedSkins="false" PagerStyle-EnableSEOPaging="true" runat="server">
                             
                                                          
                             <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                                <HeaderStyle CssClass="HeaderStyle" />
                                <%--<FooterStyle CssClass="FooterStyle" /> --%>                               
                                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />                              
                                <Columns>                                
                                <telerik:GridBoundColumn DataField="HLVIncomeType" HeaderText="Life Insurance Gap Analysis" ItemStyle-Width="60%">                              
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="40%" 
                                DataField="HLVIncomeValue" HeaderText="[Amount in Rs.]">
                                </telerik:GridBoundColumn>                                
                                </Columns>                                
                             </MasterTableView>
                            
                             <ClientSettings>
                                <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                            </ClientSettings>
                            </telerik:RadGrid>                          
                
            </td>
        </tr>
    </table>
</asp:Panel>
</telerik:RadPageView>

<telerik:RadPageView ID="RadPageGeneralInsurance" runat="server">
    <asp:Panel ID="pnlPageGeneralInsurance" runat="server">
        <table style="width: 100%">
            <tr>
                <td><br />
                    <asp:Label ID="lblGeneralInsurance" runat="server" Text="General Insurance Details" CssClass="HeaderTextSmall"> </asp:Label>
                <hr />
                </td>
            </tr>
            <%--<tr id="trHealthFailure" runat="server">
                <td align="center">
                     <br />
                    <asp:Label ID="msgHealthFailure" runat="server" CssClass="failure-msg" 
                     Text="No records found for Health Details">
                    </asp:Label>
                </td>
            </tr>--%>
            <tr id="trHealth" runat="server">
                <td>
                <asp:Label ID="lblHealth" runat="server" Text="Health" CssClass="HeaderTextSmall"> </asp:Label>
                </td>
            </tr>
            <tr id="trRadGridGEHealth" runat="server">
                <td>
                
                    <telerik:RadGrid ID="RadGridGEHealth" Width="450px" AllowFilteringByColumn="false" CssClass="GridViewStyle"
                     Skin="Telerik" ShowFooter="true" EnableEmbeddedSkins="false" PagerStyle-EnableSEOPaging="true" runat="server">
                                                          
                     <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                        <HeaderStyle CssClass="HeaderStyle" />
                        <%--<FooterStyle CssClass="FooterStyle" /> --%>                               
                        <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />                              
                        <Columns>                                                        
                        <telerik:GridBoundColumn DataField="GEAssetCategory" HeaderText="Type" ItemStyle-Width="60%">                              
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" ItemStyle-Width="40%" HeaderStyle-HorizontalAlign="Right" 
                        DataField="GEAssetValues" HeaderText="Sum Assured(Rs.)">
                        </telerik:GridBoundColumn>                                                       
                        </Columns>                                
                     </MasterTableView>                            
                     <ClientSettings>
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                    </telerik:RadGrid>                    
                    
                    </td>
            </tr>
            <tr>
                <td><br />
                </td>
            </tr>
            <tr id="trGEInsuranceError" runat="server" align="center">
                 <td align="center">
                     <br />
                    <asp:Label ID="lblGEInsuranceError" runat="server" CssClass="failure-msg" 
                     Text="No records found for Life General Insurance Details">
                    </asp:Label>
                </td>
            </tr>
            <tr id="trRadGridGEGapAnalysis" runat="server">
                <td>
                    <telerik:RadGrid ID="RadGridGEGapAnalysis" Width="450px" AllowFilteringByColumn="false" CssClass="GridViewStyle"
                       Skin="Telerik" ShowFooter="true" EnableEmbeddedSkins="false" PagerStyle-EnableSEOPaging="true" runat="server">
                                                                                     
                        <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                        <HeaderStyle CssClass="HeaderStyle" />
                        <%--<FooterStyle CssClass="FooterStyle" /> --%>                               
                        <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" /> 
                                                     
                        <Columns>                                
                        <telerik:GridBoundColumn DataField="GEAssetCategory" HeaderText="Type" ItemStyle-Width="60%">                              
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="40%"
                         DataField="GEAssetValues" HeaderText="Sum Assured(Rs.)">
                        </telerik:GridBoundColumn>                                
                        </Columns>                    
                        </MasterTableView>
                                         
                        <ClientSettings>
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr id="trOther" runat="server">
                 <td>
                     <br />
                     <asp:Label ID="lblOther" runat="server" Text="Other" CssClass="HeaderTextSmall"> </asp:Label>
                 </td>
            </tr>
            <tr id="trRadGridGEOther" runat="server">
                <td>
                    <telerik:RadGrid ID="RadGridGEOther" Width="450px" AllowFilteringByColumn="false" CssClass="GridViewStyle"
                       Skin="Telerik" ShowFooter="true" EnableEmbeddedSkins="false" ShowStatusBar="true" PagerStyle-EnableSEOPaging="true" runat="server">
                        <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                                                        
                            <Columns>
                                <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left" 
                                ItemStyle-Width="60%" HeaderStyle-HorizontalAlign="Left" DataField="GEAssetCategory" HeaderText="Type" FooterText="Total">
                                </telerik:GridBoundColumn>
                                
                                <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                                ItemStyle-Width="40%" HeaderStyle-HorizontalAlign="Right" DataField="GEAssetValues" HeaderText="Sum Assured(Rs.)" FooterText=" ">
                                </telerik:GridBoundColumn>
                            </Columns>                                
                        </MasterTableView>
                     
                         <ClientSettings>
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                
                   
                </td>
            </tr>
         </table>
    </asp:Panel>
</telerik:RadPageView>

<telerik:RadPageView ID="RadPageCashFlow" runat="server">

<asp:Panel ID="pnlPageCashFlow" runat="server">

<table style="width: 100%">
            <tr>
                <td><br />
                    <asp:Label ID="lblCashFlowHeader" runat="server" Text="Cash Flow Charts" style="font-size: 15px;" CssClass="HeaderTextBig"> </asp:Label>
                <hr />
                </td>
            </tr>
    </table>
 

<telerik:RadTabStrip ID="CashFlowTabstrip" runat="server" EnableTheming="True" Skin="Telerik"
EnableEmbeddedSkins="False" MultiPageID="CashFlowTabMultiTabId" 
SelectedIndex="0">
    <Tabs>
          <telerik:RadTab runat="server" Text="Income, Expense and Surplus Chart"
            Value="Income, Expense and Surplus Chart" TabIndex="0">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Corpus Chart"
        Value="Corpus Chart" TabIndex="1">
        </telerik:RadTab>
    </Tabs>

</telerik:RadTabStrip>

<telerik:RadMultiPage ID="CashFlowTabMultiTabId" runat="server" EnableViewState="true"
SelectedIndex="0">

<telerik:RadPageView ID="lineChartPageView" runat="server">

<asp:Panel ID="pnllineChart" runat="server">
    <table id="tblLineChart" runat="server" style="width: 100%;">
        <tr style="width: 100%">
            <td style="width: 100%; text-align: center">
                <asp:Literal ID="literalLineChart" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    
       <table id="ErrorMessageLineChart" width="100%" cellspacing="0" cellpadding="0" runat="server" visible="false">
        <tr>
            <td align="center">
                <div class="failure-msg" id="divErrLineChart" runat="server" visible="true" align="center">
                    No Records found to show Income, Expense and Surplus Chart...
                </div>
            </td>
        </tr>
    </table>
</asp:Panel>

</telerik:RadPageView>

<telerik:RadPageView ID="areaChartPageView" runat="server">

<asp:Panel ID="pnlareaChart" runat="server">

    <table id="tblAreaChart" runat="server" style="width: 100%">
        <tr style="width: 100%">
            <td style="width: 100%; text-align: center">
                <asp:Literal ID="literalAreaChart" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    
       <table id="ErrorMessageAreaChart" width="100%" cellspacing="0" cellpadding="0" runat="server" visible="false">
        <tr>
            <td align="center">
                <div class="failure-msg" id="divErrAreaChart" runat="server" visible="true" align="center">
                    No Records found to show Corpus Chart...
                </div>
            </td>
        </tr>
    </table>

</asp:Panel>

</telerik:RadPageView>
</telerik:RadMultiPage>
   
</asp:Panel>
</telerik:RadPageView>                   
</telerik:RadMultiPage>