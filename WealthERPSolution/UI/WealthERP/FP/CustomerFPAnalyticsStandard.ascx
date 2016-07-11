<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerFPAnalyticsStandard.ascx.cs"
    Inherits="WealthERP.FP.CustomerFPAnalyticsStandard" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style type="text/css">
    .one
    {
        border-style: solid;
        border-width: medium;
    }
    .row
    {
        font-family: Verdana,Tahoma;
        font-weight: lighter;
        font-size: 10px;
        color: Black;
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
        text-align: center;
        width: 60px;
    }
    .two
    {
        border-width: 0px;
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
    EnableEmbeddedSkins="False" MultiPageID="CustomerFPAnalyticsStandardId" SelectedIndex="3">
    <Tabs>
        <telerik:RadTab runat="server" Text="Financial Health" Value="Financial Health" TabIndex="0">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Income & Expense" Value="Income & Expense" TabIndex="1">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Networth" Value="Networth" TabIndex="2">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Life Insurance" Value="Life Insurance" TabIndex="3"
            Selected="True">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="General Insurance" Value="Health Insurance"
            TabIndex="4" Selected="True">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Cash Flow" Value="Cash Flow" TabIndex="5">
        </telerik:RadTab>
         <telerik:RadTab runat="server" Text="Cash Flow(After Retirement)" Value="Cash Flow(After Retirement)" TabIndex="6">
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
                        <asp:Repeater ID="repFinancialHealth" runat="server" OnItemDataBound="repFinancialHealth_RowDataBound">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table width="100%">
                                    <tr>
                                        <td colspan="4">
                                            <asp:Label ID="lblRatioName" runat="server" CssClass="HeaderTextSmall" Text='<%#DataBinder.Eval(Container.DataItem, "RatioName")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <b>
                                                <hr />
                                            </b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" class="row">
                                            <asp:Label ID="lblRatioPunchline" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RatioPunchLine") %>'>
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%">
                                            <asp:Label ID="lblValue" runat="server" CssClass="HeaderTextSmall" ForeColor="DarkRed"
                                                Text="Value:"></asp:Label>
                                            <asp:Label ID="lblRatioValue" runat="server" CssClass="HeaderTextSmall" ForeColor="DarkRed"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "RatioValue") %>'></asp:Label>
                                        </td>
                                        <td style="width: 70%">
                                            <table>
                                                <tr>
                                                    <td class="label" bgcolor="<%#DataBinder.Eval(Container.DataItem, "RatioColorOne")%>">
                                                        <asp:Label ID="lblRatoRange1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RatioRangeOne") %>'></asp:Label>
                                                    </td>
                                                    <td class="label" bgcolor="<%#DataBinder.Eval(Container.DataItem, "RatioColorTwo")%>">
                                                        <asp:Label ID="lblRatoRange2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RatioRangeTwo") %>'></asp:Label>
                                                    </td>
                                                    <td class="label" bgcolor="<%#DataBinder.Eval(Container.DataItem, "RatioColorThree")%>">
                                                        <asp:Label ID="lblRatoRange3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RatioRangeThree") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblIndicator" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "Indicator") %>'></asp:Label>
                                                        <asp:Image ID="imgRedStatus1" ImageAlign="Middle" ImageUrl="../Images/RatioDownArrow.png"
                                                            runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="imgRedStatus2" ImageAlign="Middle" ImageUrl="../Images/RatioDownArrow.png"
                                                            runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="imgRedStatus3" ImageAlign="Middle" ImageUrl="../Images/RatioDownArrow.png"
                                                            runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
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
                        <asp:Chart ID="ChartIncome" runat="server" BackColor="Transparent" Height="250px"
                            Width="450px">
                            <Series>
                                <asp:Series Name="Series">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea BackColor="#EBEFF9" Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                    <td>
                        <asp:Chart ID="ChartExpense" runat="server" BackColor="Transparent" Height="250px"
                            Width="450px">
                            <Series>
                                <asp:Series Name="Series">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea BackColor="#EBEFF9" Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
                <tr id="tdIncomeExpenseError" runat="server">
                    <td id="tdIncomeError" align="center" runat="server" style="width: 50%">
                        <br />
                        <div id="divIncomeError" runat="server" style="width: 50%" class="failure-msg">
                            No records found for Income
                        </div>
                        <%--<asp:Label ID="lblIncomeError" runat="server" CssClass="failure-msg" Style="width: 100%;"
                            Text="No records found for Income"></asp:Label>--%>
                        <br />
                        <br />
                    </td>
                    <td id="tdExpenseError" align="center" runat="server" style="width: 50%">
                        <br />
                        <div id="divExpenseError" runat="server" style="width: 50%" class="failure-msg">
                            No records found for Expense
                        </div>
                        <%-- <asp:Label ID="lblExpenseError" runat="server" CssClass="failure-msg" Text="No records found for Expense"></asp:Label>--%>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td id="tdgvrIncome" runat="server" valign="top">
                        <div id="divIncome" runat="server">
                            <telerik:RadGrid ID="gvrIncome" Width="450px" AllowFilteringByColumn="false" CssClass="GridViewStyle"
                                Skin="Telerik" ShowFooter="true" EnableEmbeddedSkins="false" PagerStyle-EnableSEOPaging="true"
                                runat="server">
                                <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <%--<FooterStyle CssClass="FooterStyle" /> --%>
                                    <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="IncomeCategory" HeaderText="Type" FooterStyle-HorizontalAlign="Left"
                                            FooterText="Total">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right" DataField="IncomeAmount" HeaderText="Current Value (Rs.)"
                                            FooterText=" " DataFormatString="{0:F0}">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right" DataField="Percent" HeaderText="Pctg(%)"
                                            FooterText=" " DataFormatString="{0:F0}">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </div>
                    </td>
                    <td id="tdRedGridExpense" runat="server" valign="top">
                        <div id="dcExpense" runat="server">
                            <telerik:RadGrid ID="RedGridExpense" Width="450px" AllowFilteringByColumn="false"
                                CssClass="GridViewStyle" Skin="Telerik" ShowFooter="true" EnableEmbeddedSkins="false"
                                PagerStyle-EnableSEOPaging="true" runat="server">
                                <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <%--<FooterStyle CssClass="FooterStyle" /> --%>
                                    <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ExpenseCategory" HeaderText="Type" FooterStyle-HorizontalAlign="Left"
                                            FooterText="Total">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right" DataField="ExpenseAmount" HeaderText="Current Value (Rs.)"
                                            FooterText=" " DataFormatString="{0:F0}">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right" DataField="Percentage" HeaderText="Pctg(%)"
                                            FooterText=" " DataFormatString="{0:F0}">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </div>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td>
                        <br />
                        <asp:Label ID="LblCashFlowAnnual" runat="server" Text="Cash Flows (Annual)" CssClass="HeaderTextSmall"></asp:Label><hr />
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <asp:Chart ID="ChartCashFlow" runat="server" BackColor="#F1EDED" Height="300px" Width="450px">
                            <Series>
                                <asp:Series Name="Series" XValueMember="Cash Category" YValueMembers="Amount">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea BackColor="#EBEFF9" Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
                <tr>
                    <td align="center" width="100%">
                        <div id="divCashFlowError" runat="server" style="width: 50%" class="failure-msg"
                            visible="false">
                            No records found for Annual Cash Flow
                        </div>
                        <%--<asp:Label ID="lblCashFlowError" runat="server" CssClass="failure-msg" Style="width: 100%"
                            Text="No records found for Annual Cash Flow" Visible="false">
                        </asp:Label>--%>
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
                    <td align="left" valign="top">
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
                        <asp:Chart ID="ChartAsset" runat="server" BackColor="Transparent" Height="250px"
                            Width="450px">
                            <Series>
                                <asp:Series Name="Series">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea BackColor="#EBEFF9" Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                    <td>
                        <asp:Chart ID="ChartLiabilities" runat="server" BackColor="Transparent" Height="250px"
                            Width="450px">
                            <Series>
                                <asp:Series Name="Series">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea BackColor="#EBEFF9" Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
                <tr id="trAssetLiabilitiesError" runat="server" style="width: 100%">
                    <td align="center" id="tdAssetErrorMsg" runat="server" style="width: 50%">
                        <br />
                        <div id="divAssetErrorMsg" runat="server" style="width: 50%" class="failure-msg">
                            No records found for Asset
                        </div>
                        <%-- <asp:Label ID="lblAssetErrorMsg" runat="server" CssClass="failure-msg" Text="No records found for Asset"></asp:Label>--%>
                    </td>
                    <td id="tdErrorLiabilities" align="center" runat="server" style="width: 50%">
                        <br />
                        <div id="divErrorLiabilities" runat="server" style="width: 50%" class="failure-msg">
                            No records found for Liabilities
                        </div>
                        <%-- <asp:Label ID="lblErrorLiabilities" runat="server" CssClass="failure-msg" Text="No records found for Liabilities"></asp:Label>--%>
                    </td>
                </tr>
                <tr>
                    <td id="tdRadGridAsset" runat="server" valign="top">
                        <telerik:RadGrid ID="RadGridAsset" Width="450px" AllowFilteringByColumn="false" CssClass="GridViewStyle"
                            Skin="Telerik" ShowFooter="true" EnableEmbeddedSkins="false" PagerStyle-EnableSEOPaging="true"
                            runat="server">
                            <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                                <HeaderStyle CssClass="HeaderStyle" />
                                <%--<FooterStyle CssClass="FooterStyle" /> --%>
                                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="AssetGroupName" HeaderText="Type" FooterStyle-HorizontalAlign="Left"
                                        FooterText="Total">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right" DataField="AssetValues" HeaderText="Current Value (Rs.)"
                                        FooterText=" " DataFormatString="{0:F0}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right" DataField="Pctg" HeaderText="Pctg(%)" FooterText=" "
                                        DataFormatString="{0:F0}">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings>
                                <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                    <td id="tdRadGridLiabilities" runat="server" valign="top">
                        <telerik:RadGrid ID="RadGridLiabilities" Width="450px" AllowFilteringByColumn="false"
                            CssClass="GridViewStyle" Skin="Telerik" ShowFooter="true" EnableEmbeddedSkins="false"
                            PagerStyle-EnableSEOPaging="true" runat="server">
                            <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                                <HeaderStyle CssClass="HeaderStyle" />
                                <%--<FooterStyle CssClass="FooterStyle" /> --%>
                                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="LoanType" HeaderText="Loan" FooterStyle-HorizontalAlign="Left"
                                        FooterText="Total">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right" DataField="LoanValues" HeaderText="Outstanding Amount"
                                        FooterText=" " DataFormatString="{0:F0}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right" DataField="Prcentage" HeaderText="Pctg(%)"
                                        FooterText=" " DataFormatString="{0:F0}">
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
                        <telerik:RadGrid ID="RadGridLifeInsurance" Width="450px" AllowFilteringByColumn="false"
                            CssClass="GridViewStyle" Skin="Telerik" ShowFooter="true" EnableEmbeddedSkins="false"
                            PagerStyle-EnableSEOPaging="true" runat="server">
                            <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                                <HeaderStyle CssClass="HeaderStyle" />
                                <%--<FooterStyle CssClass="FooterStyle" /> --%>
                                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="InsuranceCategoryName" HeaderText="Type" ItemStyle-Width="60%"
                                        FooterStyle-HorizontalAlign="Left" FooterText="Total">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="40%" HeaderStyle-HorizontalAlign="Right" DataField="InsuranceValues"
                                        HeaderText="Sum Assured(Rs.)" FooterText=" " DataFormatString="{0:F0}">
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
                        <div id="divErrorLifeInsurance" runat="server" style="width: 50%" class="failure-msg">
                            No records found for Life Insurance Details
                        </div>
                        <%-- <asp:Label ID="lblErrorLifeInsurance" runat="server" CssClass="failure-msg" Style="width: 100%"
                            Text="No records found for Life Insurance Details"></asp:Label>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr id="trRadGridHLVAnalysis" runat="server">
                    <td>
                        <telerik:RadGrid ID="RadGridHLVAnalysis" Width="450px" AllowFilteringByColumn="false"
                            CssClass="GridViewStyle" Skin="Telerik" ShowFooter="true" EnableEmbeddedSkins="false"
                            PagerStyle-EnableSEOPaging="true" runat="server">
                            <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                                <HeaderStyle CssClass="HeaderStyle" />
                                <%--<FooterStyle CssClass="FooterStyle" /> --%>
                                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="HLV_Type" HeaderText="Human Life Value Analysis"
                                        ItemStyle-Width="60%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="40%" DataField="HLV_Values" HeaderText="HLV Values(Rs.)" DataFormatString="{0:F0}">
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
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr id="trRadGridLIGapAnalysis" runat="server">
                    <td>
                        <telerik:RadGrid ID="RadGridLIGapAnalysis" Width="450px" AllowFilteringByColumn="false"
                            CssClass="GridViewStyle" Skin="Telerik" ShowFooter="true" EnableEmbeddedSkins="false"
                            PagerStyle-EnableSEOPaging="true" runat="server">
                            <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                                <HeaderStyle CssClass="HeaderStyle" />
                                <%--<FooterStyle CssClass="FooterStyle" /> --%>
                                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="HLVIncomeType" HeaderText="Life Insurance Gap Analysis"
                                        ItemStyle-Width="60%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="40%" DataField="HLVIncomeValue" HeaderText="[Amount in Rs.]"
                                        DataFormatString="{0:F0}">
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
                    <td>
                        <br />
                        <asp:Label ID="lblGeneralInsurance" runat="server" Text="General Insurance Details"
                            CssClass="HeaderTextSmall"> </asp:Label>
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
                        <telerik:RadGrid ID="RadGridGEHealth" Width="450px" AllowFilteringByColumn="false"
                            CssClass="GridViewStyle" Skin="Telerik" ShowFooter="true" EnableEmbeddedSkins="false"
                            PagerStyle-EnableSEOPaging="true" runat="server">
                            <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                                <HeaderStyle CssClass="HeaderStyle" />
                                <%--<FooterStyle CssClass="FooterStyle" /> --%>
                                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="GEAssetCategory" HeaderText="Type" ItemStyle-Width="60%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" ItemStyle-Width="40%"
                                        HeaderStyle-HorizontalAlign="Right" DataField="GEAssetValues" HeaderText="Sum Assured(Rs.)"
                                        DataFormatString="{0:F0}">
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
                        <br />
                    </td>
                </tr>
                <tr id="trGEInsuranceError" runat="server" align="center" style="width: 150%">
                    <td align="center">
                        <br />
                        <div id="divGEInsuranceError" runat="server" style="width: 50%" class="failure-msg">
                            No records found for General Insurance Details
                        </div>
                        <%-- <asp:Label ID="lblGEInsuranceError" runat="server" CssClass="failure-msg" 
                            Text="No records found for General Insurance Details">
                        </asp:Label>--%>
                    </td>
                </tr>
                <tr id="trRadGridGEGapAnalysis" runat="server">
                    <td>
                        <telerik:RadGrid ID="RadGridGEGapAnalysis" Width="450px" AllowFilteringByColumn="false"
                            CssClass="GridViewStyle" Skin="Telerik" ShowFooter="true" EnableEmbeddedSkins="false"
                            PagerStyle-EnableSEOPaging="true" runat="server">
                            <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                                <HeaderStyle CssClass="HeaderStyle" />
                                <%--<FooterStyle CssClass="FooterStyle" /> --%>
                                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="GEAssetCategory" HeaderText="Type" ItemStyle-Width="60%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="40%" DataField="GEAssetValues" HeaderText="Sum Assured(Rs.)"
                                        DataFormatString="{0:F0}">
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
                        <telerik:RadGrid ID="RadGridGEOther" Width="450px" AllowFilteringByColumn="false"
                            CssClass="GridViewStyle" Skin="Telerik" ShowFooter="true" EnableEmbeddedSkins="false"
                            ShowStatusBar="true" PagerStyle-EnableSEOPaging="true" runat="server">
                            <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                                <Columns>
                                    <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="60%" HeaderStyle-HorizontalAlign="Left" DataField="GEAssetCategory"
                                        HeaderText="Type" FooterText="Total">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="40%" HeaderStyle-HorizontalAlign="Right" DataField="GEAssetValues"
                                        HeaderText="Sum Assured(Rs.)" FooterText=" " DataFormatString="{0:F0}">
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
    <telerik:RadPageView ID="RadPageCF" runat="server">
        <asp:Panel ID="pnlCF" runat="server">
            <table style: width="100%;">
                <tr>
                    <td>
                        <div class="divPageHeading">
                            <table cellspacing="0" cellpadding="2" width="100%">
                                <tr>
                                    <td align="left">
                                    </td>
                                    <td align="right">
                                        <asp:ImageButton ID="imgBtnrgcashFlow" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                            runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportgvCashFlowList_OnClick"
                                            OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
                                    </td>
                                    <%--  td style="width: 10px" align="right">
                            <img src="../Images/helpImage.png" height="20px" width="25px" style="float: right;"
                                class="flip" />
                        </td>--%>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="gvCashFlowList" runat="server" AllowAutomaticDeletes="false"
                PageSize="20" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                ShowStatusBar="false" ShowFooter="false" AllowPaging="true" AllowSorting="true"
                OnNeedDataSource="gvCashFlowList_OnNeedDataSource" GridLines="none" AllowAutomaticInserts="false"
                Skin="Telerik" EnableHeaderContextMenu="true" Width="95%" Visible="false">
                <MasterTableView Width="100%" DataKeyNames="ID" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                    CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
                    GroupLoadMode="Client" ShowGroupFooter="true">
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="Year" DataField="YEAR" UniqueName="YEAR" SortExpression="YEAR"
                            AutoPostBackOnFilter="true" AllowFiltering="false" 
                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Age" DataField="Age" UniqueName="Age" SortExpression="Age"
                            AutoPostBackOnFilter="true" AllowFiltering="true"  ShowFilterIcon="false"
                            CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="OpeningBalance" SortExpression="OpeningBalance"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AllowFiltering="true" DataFormatString="{0:N0}" HeaderText="Opening Balance"
                            UniqueName="OpeningBalance">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SelfIncome" SortExpression="SelfIncome" DataFormatString="{0:N0}"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AllowFiltering="true" HeaderText="Self Income" UniqueName="SelfIncome">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PensionIncome" SortExpression="PensionIncome"
                            DataFormatString="{0:N0}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AllowFiltering="true" HeaderText="Pension Income" UniqueName="PensionIncome">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RentalIncome" SortExpression="RentalIncome" DataFormatString="{0:N0}"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AllowFiltering="true" HeaderText="Rental Income" UniqueName="RentalIncome">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="OtherIncome" SortExpression="OtherIncome" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                            DataFormatString="{0:N0}" HeaderText="Other Income" UniqueName="OtherIncome">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AssetMaturity" SortExpression="AssetMaturity"
                            DataFormatString="{0:N0}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AllowFiltering="true" HeaderText="Asset Maturity" UniqueName="AssetMaturity">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ToTalInflow" SortExpression="ToTalInflow" DataFormatString="{0:N0}"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AllowFiltering="true" HeaderText="Total Inflow" UniqueName="ToTalInflow">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Expense" SortExpression="Expense" DataFormatString="{0:N0}"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AllowFiltering="true" HeaderText="Expenses" UniqueName="Expense">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Liabilities" SortExpression="Liabilities" DataFormatString="{0:N0}"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AllowFiltering="true" HeaderText="Liabilities" UniqueName="Liabilities">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="LIPremiumAmount" SortExpression="LIPremiumAmount"
                            DataFormatString="{0:N0}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AllowFiltering="true" HeaderText="LI Premium Amount" UniqueName="LIPremiumAmount">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn DataField="GIPremiumAmount" SortExpression="GIPremiumAmount"
                            DataFormatString="{0:N0}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AllowFiltering="true" HeaderText="GI Premium Amount" UniqueName="GIPremiumAmount">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SIPAmount" SortExpression="SIPAmount" DataFormatString="{0:N0}"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AllowFiltering="true" HeaderText="SIP Amount" UniqueName="SIPAmount">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="GoalOutflow" SortExpression="GoalOutflow" DataFormatString="{0:N0}"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AllowFiltering="true" HeaderText="Goal Outflow" UniqueName="GoalOutflow">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TotalOutflow" SortExpression="TotalOutflow" DataFormatString="{0:N0}"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AllowFiltering="true" HeaderText="Total Outflow" UniqueName="TotalOutflow">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ClosingBalance" SortExpression="ClosingBalance"
                            DataFormatString="{0:N0}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AllowFiltering="true" HeaderText="Closing Balance" UniqueName="ClosingBalance">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                </ClientSettings>
            </telerik:RadGrid>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageCFafterRet" runat="server">
        <asp:Panel ID="Panel1" runat="server">
            <table style: width="100%;">
                <tr>
                    <td>
                        <div class="divPageHeading">
                            <table cellspacing="0" cellpadding="2" width="100%">
                                <tr>
                                    <td align="left">
                                    </td>
                                    <td align="right">
                                        <asp:ImageButton ID="ImageButton1" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                            runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportgvRetCashFlowList_OnClick"
                                            OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="gvRetCashFlowList" runat="server" AllowAutomaticDeletes="false"
                PageSize="20" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                ShowStatusBar="false" ShowFooter="false" AllowPaging="true" AllowSorting="true"
                OnNeedDataSource="gvRetCashFlowList_OnNeedDataSource" GridLines="none" AllowAutomaticInserts="false"
                Skin="Telerik" EnableHeaderContextMenu="true" Width="95%" Visible="false">
                <MasterTableView Width="100%" DataKeyNames="ID" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                    CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
                    GroupLoadMode="Client" ShowGroupFooter="true">
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="Age" DataField="Age" UniqueName="Age" SortExpression="Age"
                            AutoPostBackOnFilter="true" AllowFiltering="false" DataFormatString="{0:N0}"
                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Year" DataField="YEAR" UniqueName="YEAR" SortExpression="YEAR"
                            AutoPostBackOnFilter="true" AllowFiltering="false" 
                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText=" Retirement Corpus" DataField="RetirementCorpus" UniqueName="RetirementCorpus"
                            SortExpression="RetirementCorpus" AutoPostBackOnFilter="true" AllowFiltering="false"
                            DataFormatString="{0:N0}" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Rental Income" DataField="RentalIncome" UniqueName="RentalIncome"
                            SortExpression="RentalIncome" AutoPostBackOnFilter="true" AllowFiltering="false"
                            DataFormatString="{0:N0}" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Other Income" DataField="OtherIncome" UniqueName="OtherIncome"
                            SortExpression="OtherIncome" AutoPostBackOnFilter="true" AllowFiltering="false"
                            DataFormatString="{0:N0}" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Expenses" DataField="Expense" UniqueName="Expense"
                            SortExpression="Expense" AutoPostBackOnFilter="true" AllowFiltering="false" DataFormatString="{0:N0}"
                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Goal Outflow" DataField="GoalOutflow" UniqueName="GoalOutflow"
                            SortExpression="GoalOutflow" AutoPostBackOnFilter="true" AllowFiltering="false"
                            DataFormatString="{0:N0}" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText=" Net corpus" DataField="Netcorpus" UniqueName="Netcorpus"
                            SortExpression="Netcorpus" AutoPostBackOnFilter="true" AllowFiltering="false"
                            DataFormatString="{0:N0}" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText=" Return on Accumulated Corpus" DataField="ReturnOnNetcorpus" UniqueName="ReturnOnNetcorpus"
                            SortExpression="ReturnOnNetcorpus" AutoPostBackOnFilter="true" AllowFiltering="false"
                            DataFormatString="{0:N0}" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Closing Balance of Corpus" DataField="ClosingBalance"
                            UniqueName="ClosingBalance" SortExpression="ClosingBalance" AutoPostBackOnFilter="true"
                            AllowFiltering="false" DataFormatString="{0:N0}" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                </ClientSettings>
            </telerik:RadGrid>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageCashFlow" runat="server">
        <asp:Panel ID="pnlPageCashFlow" runat="server">
            <table style="width: 100%">
                <tr>
                    <td>
                        <br />
                        <asp:Label ID="lblCashFlowHeader" runat="server" Text="Cash Flow Charts" Style="font-size: 15px;"
                            CssClass="HeaderTextBig"> </asp:Label>
                        <hr />
                    </td>
                </tr>
            </table>
            <telerik:RadTabStrip ID="CashFlowTabstrip" runat="server" EnableTheming="True" Skin="Telerik"
                EnableEmbeddedSkins="False" MultiPageID="CashFlowTabMultiTabId" SelectedIndex="0">
                <Tabs>
                    <telerik:RadTab runat="server" Text="Income, Expense and Surplus Chart" Value="Income, Expense and Surplus Chart"
                        TabIndex="0">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Corpus Chart" Value="Corpus Chart" TabIndex="1">
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
                        <table id="ErrorMessageLineChart" width="100%" cellspacing="0" cellpadding="0" runat="server"
                            visible="false">
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
                        <table id="ErrorMessageAreaChart" width="100%" cellspacing="0" cellpadding="0" runat="server"
                            visible="false">
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