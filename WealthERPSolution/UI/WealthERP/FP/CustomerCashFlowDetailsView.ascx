<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerCashFlowDetailsView.ascx.cs"
    Inherits="WealthERP.FP.CustomerCashFlowDetailsView" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<telerik:RadTabStrip ID="RadTabStrip1" runat="server" EnableTheming="True" Skin="Telerik"
    EnableEmbeddedSkins="False" MultiPageID="CustomerFPCashFlow" SelectedIndex="3">
    <Tabs>
        <telerik:RadTab runat="server" Text="Cash Flow" Value="Cash Flow" TabIndex="1" Selected="true">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Cash Flow(After Retirement)" Value="Cash Flow(After Retirement)"
            TabIndex="2">
        </telerik:RadTab>
    </Tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="CustomerFPCashFlow" runat="server" EnableViewState="true"
    SelectedIndex="0">
    <telerik:RadPageView ID="RadPageCF" runat="server">
        <asp:Panel ID="pnlCF" runat="server">
            <table style: width="100%;">
                <tr>
                    <td>
                        <div class="divPageHeading">
                            <table cellspacing="0" cellpadding="2" width="100%">
                                <tr>
                                    <td align="left">
                                        Cash Flow Till Retirement
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
            <table cellspacing="0" cellpadding="2" width="100%">
                <tr>
                    <td align="right">
                    <asp:CheckBox ID="chkincludeSpouse" runat="server"  Text="Include Spouse" />
                    </td>
                    <td align="left">
                    <asp:Button ID="btnGo" Text="Submit" runat="server" OnClick="btnGo_OnClick" CssClass="PCGMediumButton" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlCustomerList" runat="server" class="Landscape" ScrollBars="Horizontal"
         Width="100%" Height="80%">
            <telerik:RadGrid ID="gvCashFlowList" runat="server" AllowAutomaticDeletes="false"
                Culture="de-De" PageSize="20" EnableEmbeddedSkins="false" AllowFilteringByColumn="true"
                AutoGenerateColumns="False" ShowStatusBar="false" ShowFooter="false" AllowPaging="true"
                AllowSorting="true" OnNeedDataSource="gvCashFlowList_OnNeedDataSource" GridLines="none"
                AllowAutomaticInserts="false" Skin="Telerik" EnableHeaderContextMenu="true" Width="95%"
                Visible="false">
                <MasterTableView Width="100%" DataKeyNames="ID" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                    CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
                    GroupLoadMode="Client" ShowGroupFooter="true">
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="Year" DataField="YEAR" UniqueName="YEAR" SortExpression="YEAR"
                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Age" DataField="Age" UniqueName="Age" SortExpression="Age"
                            AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
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
                        <telerik:GridBoundColumn DataField="SpouseIncome" SortExpression="SpouseIncome" DataFormatString="{0:N0}"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            AllowFiltering="true" HeaderText="Spouse Income" UniqueName="SpouseIncome">
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
                        <telerik:GridBoundColumn DataField="SIPAmountRecomm" SortExpression="SIPAmountRecomm"
                            DataFormatString="{0:N0}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AllowFiltering="true" HeaderText="Recommended SIP Amount"
                            UniqueName="SIPAmountRecomm">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PremiumAmountRecomm" SortExpression="PremiumAmountRecomm"
                            DataFormatString="{0:N0}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AllowFiltering="true" HeaderText="Recommended Insurance Premium"
                            UniqueName="PremiumAmountRecomm">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="LiabilitiesRecomm" SortExpression="LiabilitiesRecomm"
                            DataFormatString="{0:N0}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AllowFiltering="true" HeaderText="Recommended Liabilities"
                            UniqueName="LiabilitiesRecomm">
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
                    
                </ClientSettings>
            </telerik:RadGrid>
            </asp:Panel>
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
                                        Cash Flow From Retirement to LifeEnd
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
            <asp:Panel ID="Panel2" runat="server" class="Landscape" ScrollBars="Horizontal"
         Width="100%" Height="80%">
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
                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText=" Retirement Corpus" DataField="RetirementCorpus"
                            UniqueName="RetirementCorpus" SortExpression="RetirementCorpus" AutoPostBackOnFilter="true"
                            AllowFiltering="false" DataFormatString="{0:N0}" ShowFilterIcon="false" CurrentFilterFunction="Contains">
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
                        <telerik:GridBoundColumn HeaderText=" Return on Accumulated Corpus" DataField="ReturnOnNetcorpus"
                            UniqueName="ReturnOnNetcorpus" SortExpression="ReturnOnNetcorpus" AutoPostBackOnFilter="true"
                            AllowFiltering="false" DataFormatString="{0:N0}" ShowFilterIcon="false" CurrentFilterFunction="Contains">
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
                 
                </ClientSettings>
            </telerik:RadGrid>
            </asp:Panel>
        </asp:Panel>
    </telerik:RadPageView>
</telerik:RadMultiPage>